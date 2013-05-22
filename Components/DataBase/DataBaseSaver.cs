using System;
using System.Data;
using System.Threading;
using System.Data.SqlTypes;
using System.Data.SqlClient;
using System.Collections.Generic;

namespace DataBase
{
    /// <summary>
    /// Реализует сохранение параметров в реальном времени с заданным интервалом
    /// </summary>
    public class DataBaseSaver
    {
        // ---- данные класса ----
                
        private t_measuring measuring = null;                           // осуществляет работу с таблицей времени
        private DataBaseAdapter adapter = null;                         // адаптер БД

        private Timer timer = null;                                     // генерирует событие по которому выполнять сохранение параметров в БД

        private Mutex callBackMutex = null;                             // для предотвращения наложения процедуры таймера
        private Mutex in_out_mutex = null;                              // синхронизуем двойную буферизацию

        private List<DataBaseParameterValue> input = null;              // входной буфер для сохранения
        private List<DataBaseParameterValue> output = null;             // выходной буфер для записи в БД

        private int timeToBuffer = 1000;                                // частота сохранения параметров в БД
        private DataBaseParameters parameters = null;                   // загруженные параметры из БД

        private Mutex mutex = null;                                     // синхронизуем доступ к состоянию
        private SaverState state = SaverState.Stopped;                  // текущее состояние

        /// <summary>
        /// Инициализирует новый класс
        /// </summary>
        /// <param name="Adapted">Адаптер БД</param>
        public DataBaseSaver(DataBaseAdapter Adapted, DataBaseParameters Parameters)
        {
            adapter = Adapted;
            parameters = Parameters;            

            in_out_mutex = new Mutex();
            callBackMutex = new Mutex();

            input = new List<DataBaseParameterValue>();
            output = new List<DataBaseParameterValue>();

            callBackMutex = new Mutex();
            timer = new Timer(TimerCallback, null, Timeout.Infinite, timeToBuffer);

            mutex = new Mutex();
        }

        /// <summary>
        /// Осуществляет запись параметров в БД
        /// </summary>
        /// <param name="state"></param>
        public void TimerCallback(object state)
        {
            bool blocked = false;
            try
            {
                if (callBackMutex.WaitOne(0))
                {
                    blocked = true;
                    Flush();
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
            finally
            {
                if (blocked) callBackMutex.ReleaseMutex();
            }
        }

        /// <summary>
        /// Сохранить параметры в БД
        /// </summary>
        private void Flush()
        {
            try
            {
                if (in_out_mutex.WaitOne(100))
                {
                    output.AddRange(input);
                    input.Clear();

                    in_out_mutex.ReleaseMutex();
                }

                foreach (DataBaseParameterValue parameter in output)
                {
                    int index = measuring.GetTimeIndex(parameter.Time);
                    try
                    {
                        DataBaseParameter p = parameters.GetParameter(parameter.Identifier);
                        SaveParameter(p.tblValues, index, parameter.Value);
                    }
                    catch
                    {
                        // --- не удалось сохранить значение параметра ----
                    }
                }

                output.Clear();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
        }

        /// <summary>
        /// Сохранить значение параметра в БД
        /// </summary>
        /// <param name="table">Таблица в которую сохранить</param>
        /// <param name="id">id времени</param>
        /// <param name="value">Значение параметра</param>
        private void SaveParameter(string table, int id, float value)
        {
            SqlConnection connection = null;
            try
            {
                if (!float.IsNaN(value) && !float.IsInfinity(value) &&
                    !float.IsNegativeInfinity(value) && !float.IsPositiveInfinity(value))
                {
                    connection = new SqlConnection(adapter.ConnectionString);
                    connection.Open();

                    if (connection.State == ConnectionState.Open)
                    {
                        SqlCommand command = connection.CreateCommand();

                        command.Parameters.Add(new SqlParameter("id", id));
                        command.Parameters[0].SqlDbType = SqlDbType.Int;

                        command.Parameters.Add(new SqlParameter("val", value));
                        command.Parameters[1].SqlDbType = SqlDbType.Real;

                        command.CommandText = string.Format("Insert Into dbo.{0} (id, val_prm) Values (@id, @val)", table, id, value);
                        if (command.ExecuteNonQuery() != 1)
                        {
                            throw new Exception("Не удалось сохранить параметр в БД");
                        }
                    }
                    else
                        throw new Exception("Не удалось установить соединение с БД");
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
            finally
            {
                if (connection != null)
                {
                    if (connection.State == ConnectionState.Open)
                    {
                        connection.Close();
                    }

                    connection.Dispose();
                }
            }
        }

        /// <summary>
        /// Запустить процедуру сохранения
        /// </summary>
        public void Start()
        {
            bool blocked = false;
            try
            {
                if (mutex.WaitOne(1000))
                {
                    blocked = true;
                    state = SaverState.Started;

                    measuring = t_measuring.Virtualize(adapter);
                    timer.Change(0, timeToBuffer);

                    state = SaverState.Started;
                }
            }
            finally
            {
                if (blocked) mutex.ReleaseMutex();
            }
        }

        /// <summary>
        /// Остановить процедуру сохранения
        /// </summary>
        public void Stop()
        {
            bool blocked = false;
            try
            {
                if (mutex.WaitOne(1000))
                {
                    blocked = true;
                    state = SaverState.Stopped;

                    timer.Change(Timeout.Infinite, timeToBuffer);

                    if (callBackMutex.WaitOne(3000))
                    {
                        measuring = null;
                        callBackMutex.ReleaseMutex();
                    }

                    state = SaverState.Stopped;
                }
            }
            finally
            {
                if (blocked) mutex.ReleaseMutex();
            }
        }

        /// <summary>
        /// Добавить параметр на запись в БД
        /// </summary>
        /// <param name="value">Параметр для записи</param>
        public void ToWrite(DataBaseParameterValue value)
        {
            bool blocked = false;
            try
            {
                if (mutex.WaitOne(1000))
                {
                    if (state == SaverState.Started)
                    {
                        if (in_out_mutex.WaitOne(1000))
                        {
                            blocked = true;
                            input.Add(value);
                        }
                    }

                    mutex.ReleaseMutex();
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
            finally
            {
                if (blocked) in_out_mutex.ReleaseMutex();
            }
        }

        /// <summary>
        /// Инициализировать 
        /// </summary>
        /// <param name="adap">Адаптер БД</param>
        /// <param name="parames">Параметры загруженные из БД</param>
        internal void Inialize(DataBaseAdapter adap, DataBaseParameters parames)
        {
            adapter = adap;
            parameters = parames;
        }
    }

    /// <summary>
    /// Реализует класс, осуществляющий сохранение параметра для записи его в БД
    /// </summary>
    public class DataBaseParameterValue
    {
        // ---- данные класса ----

        private Guid guid;              // идентификатор параметра
        private long time;              // время параметра

        private float p_value;          // значение параметра в указанное время

        /// <summary>
        /// Инициализирует новый экземпляр класса
        /// </summary>
        public DataBaseParameterValue()
        {
            guid = new Guid();
            time = 0;

            p_value = 0.0f;
        }

        /// <summary>
        /// Инициализирует новый экземпляр класса
        /// </summary>
        /// <param name="identifier">Идентификатор параметра</param>
        /// <param name="p_time">Время параметра</param>
        /// <param name="val">Значение параметра в указанное время</param>
        public DataBaseParameterValue(Guid identifier, long p_time, float val)
        {

            guid = new Guid(identifier.ToByteArray());
            time = p_time;

            p_value = val;
        }

        /// <summary>
        /// Определяет идентификатор параметра
        /// </summary>
        public Guid Identifier
        {
            get { return guid; }
        }

        /// <summary>
        /// Определяет время параметра
        /// </summary>
        public long Time
        {
            get { return time; }
        }

        /// <summary>
        /// Определчет значение параметра в указанное время
        /// </summary>
        public float Value
        {
            get { return p_value; }
        }
    }

    /// <summary>
    /// Текущее состояние
    /// </summary>
    public enum SaverState
    {
        /// <summary>
        /// Сохраняет
        /// </summary>
        Started,

        /// <summary>
        /// Не сохраняет
        /// </summary>
        Stopped
    }
}