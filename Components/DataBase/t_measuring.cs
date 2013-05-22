using System;
using System.Data;
using System.Data.SqlTypes;
using System.Data.SqlClient;

namespace DataBase
{
    /// <summary>
    /// Реализует виртуальную таблицу времени БД
    /// </summary>
    internal class t_measuring
    {
        // ---- данные класса ----

        private int lastId;                             // последний добавленный идентификатор записи
        private long lastTime;                          // последнее добавленное значение времени в таблицу

        private float lastDept;                         // последнее добавленное значение глубины
        private DataBaseAdapter adapter = null;         // реализует базовые функции при работе с БД

        /// <summary>
        /// Инициализирует новый экземпляр класса
        /// </summary>
        /// <param name="Adapter">Адаптер БД</param>
        internal t_measuring(DataBaseAdapter Adapter)
        {
            lastId = -1;
            lastDept = -1;

            lastTime = 0;

            adapter = Adapter;
        }

        /// <summary>
        /// Определяет последний добавленный идентификатор записи
        /// </summary>
        public int LastId
        {
            get { return lastId; }
            set { lastId = value; }
        }

        /// <summary>
        /// Определяет последнее добавленное значение времени в таблицу
        /// </summary>
        public long LastTime
        {
            get { return lastTime; }
            set { lastTime = value; }
        }

        /// <summary>
        /// Определяет последнее добавленное значение глубины
        /// </summary>
        public float LastDept
        {
            get { return lastDept; }
            set { lastDept = value; }
        }

        /// <summary>
        /// Проверить корректность времени
        /// </summary>
        /// <param name="time"></param>
        /// <returns>Индекс от указанного времени</returns>
        public int GetTimeIndex(long time)
        {
            DateTime last = DateTime.FromBinary(lastTime);
            DateTime current = DateTime.FromBinary(time);

            try
            {
                if (last == current) 
                    
                    return lastId;

                else
                    if (last < current)
                    {
                        // ---- необходимо добавить новое время в БД ----

                        InsertNewTime(current, (lastId + 1));

                        lastTime = time;
                        lastId = lastId + 1;

                        return lastId;
                    }
                    else
                        if (last > current)
                        {
                            // ---- не оч правильная ситуация, но обработать нужно ----

                            return -1;
                        }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }

            throw new Exception("Невероятная ситуация!!!! Такого быть не должно в принципе!!!");
        }

        /// <summary>
        /// Добавить новое время в таблицу, хранящей врема поступления значений параметров
        /// </summary>
        /// <param name="time"></param>
        /// <param name="id"></param>
        private void InsertNewTime(DateTime time, int id)
        {
            SqlConnection connection = null;

            try
            {
                connection = new SqlConnection(adapter.ConnectionString);
                connection.Open();

                if (connection.State == ConnectionState.Open)
                {
                    SqlCommand command = connection.CreateCommand();

                    command.Parameters.Add(new SqlParameter("id", id));
                    command.Parameters[0].SqlDbType = SqlDbType.Int;

                    command.Parameters.Add(new SqlParameter("val_Time", time.ToOADate()));
                    command.Parameters[1].SqlDbType = SqlDbType.Float;

                    command.CommandText = string.Format("Insert Into {0} (id, val_Time, val_depth) Values (@id, @val_Time, 0)", "dbo.t_measuring");
                    if (command.ExecuteNonQuery() != 1)
                    {
                        throw new Exception("Не удалось добавить новое время в БД.");
                    }
                }
                else
                    throw new Exception("Не удалось установить соединение с сервером БД");
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
        /// Виртуализируем таблицу времени
        /// </summary>
        /// <param name="Adapter">Объет определяющий подключение к БД</param>
        /// <returns>В случае успеха виртуализировнная таблица времени, в противном случае null</returns>
        public static t_measuring Virtualize(DataBaseAdapter Adapter)
        {
            SqlConnection connection = null;
            try
            {
                connection = new SqlConnection(Adapter.ConnectionString);
                connection.Open();

                if (connection.State == ConnectionState.Open)
                {
                    SqlCommand command = connection.CreateCommand();

                    command.CommandText = string.Format("Select * From {0} Order By id Desc", "dbo.t_measuring");
                    SqlDataReader reader = command.ExecuteReader();

                    t_measuring measuring = new t_measuring(Adapter);

                    measuring.LastId = 0;
                    measuring.LastTime = DateTime.Now.Ticks;

                    measuring.LastDept = 0.0f;

                    if (reader != null)
                    {
                        if (reader.IsClosed == false)
                        {
                            while (reader.Read())
                            {
                                measuring.LastId = reader.GetInt32(0);

                                DateTime t = DateTime.FromOADate(reader.GetDouble(1));
                                measuring.LastTime = t.Ticks;

                                measuring.LastDept = 0.0f;
                                break;
                            }
                        }

                        return measuring;
                    }
                    else
                        throw new Exception("Не удалось загрузить таблицу времени из БД");
                }
                else
                    throw new Exception("Не удалось установить соединение с БД");
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
    }
}