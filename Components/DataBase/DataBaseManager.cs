using System;
using System.Data;
using System.Threading;
using System.Data.SqlClient;
using System.Runtime.Serialization;

namespace DataBase
{
    /// <summary>
    /// Ркализует управление работой с БД
    /// </summary>
    [Serializable]
    public class DataBaseManager : ISerializable
    {
        // ---- данные класса ----

        [NonSerialized]
        private DataBaseState state;                    // текущее состояние БД

        [NonSerialized]
        private Mutex mutex = null;                     // обеспечивает потокобезопасность

        private DataBaseAdapter adapter;                // адаптер соединения с БД

        [NonSerialized]
        private DataBase dataBase;                      // база данных

        [NonSerialized]
        private DataBaseServer server;                  // сервер БД

        /// <summary>
        /// Инициализирует новый экземпляр класса
        /// </summary>
        public DataBaseManager()
        {
            mutex = new Mutex();
            state = DataBaseState.Default;

            adapter = new DataBaseAdapter();
            server = new DataBaseServer(adapter);

            dataBase = new DataBase(adapter);
            server = new DataBaseServer(adapter);
        }

        /// <summary>
        /// Инициализирует новый экземпляр класса
        /// </summary>
        /// <param name="info">Объект System.Runtime.Serialization.SerializationInfo для извлечения данных.</param>
        /// <param name="context">Целевое местоположение сериализации.</param>
        protected DataBaseManager(SerializationInfo info, StreamingContext context)
        {
            mutex = new Mutex();
            state = DataBaseState.Default;

            adapter = new DataBaseAdapter();

            adapter.UserID = info.GetString("UserID");
            adapter.Password = info.GetString("Password");

            server = new DataBaseServer(adapter);

            dataBase = new DataBase(adapter);
            server = new DataBaseServer(adapter);
        }

        /// <summary>
        /// Заполняет объект System.Runtime.Serialization.SerializationInfo данными,
        /// необходимыми для сериализации целевого объекта.
        /// </summary>
        /// <param name="info">Объект System.Runtime.Serialization.SerializationInfo для заполнения данными.</param>
        /// <param name="context">Целевое местоположение сериализации.</param>
        public void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            info.AddValue("UserID", adapter.UserID);
            info.AddValue("Password", adapter.Password);
        }

        /// <summary>
        /// Осуществляет проверку подключения к серверу БД
        /// </summary>
        public bool IsConnectValid
        {
            get
            {
                SqlConnection connection = null;
                try
                {
                    connection = new SqlConnection(adapter.ConnectionStringToServer);
                    connection.Open();

                    if (connection.State == ConnectionState.Open)
                    {
                        return true;
                    }
                    else
                        return false;
                }
                catch
                {
                    return false;
                }
                finally
                {
                    if (connection != null)
                    {
                        if (connection.State == ConnectionState.Open)
                        {
                            connection.Close();
                            SqlConnection.ClearPool(connection);
                        }

                        connection.Dispose();
                    }
                }
            }
        }

        /// <summary>
        /// Определяет текущее состояние БД
        /// </summary>
        public DataBaseState State
        {
            get
            {
                bool blocked = false;
                try
                {
                    if (mutex.WaitOne(1000))
                    {
                        blocked = true;
                        return state;
                    }
                    else
                        return DataBaseState.Default;
                }
                finally
                {
                    if (blocked) mutex.ReleaseMutex();
                }
            }
        }

        /// <summary>
        /// Определяет имя сервера БД (localhost)
        /// </summary>
        public string DataSource
        {
            get
            {
                bool blocked = false;
                try
                {
                    if (mutex.WaitOne(1000))
                    {
                        blocked = true;
                        return adapter.DataSource;
                    }
                    else
                        return string.Empty;
                }
                finally
                {
                    if (blocked) mutex.ReleaseMutex();
                }
            }

            set
            {
                bool blocked = false;
                try
                {
                    if (mutex.WaitOne(1000))
                    {
                        blocked = true;
                        if (state == DataBaseState.Default)
                        {
                            adapter.DataSource = value;
                        }
                        else
                            throw new InvalidOperationException("Текущее состояние не позволяет присвоить згачение данному свойству");
                    }
                    else
                        throw new TimeoutException();
                }
                finally
                {
                    if (blocked) mutex.ReleaseMutex();
                }
            }
        }

        /// <summary>
        /// Определяет каталог к торомому подключиться (имя БД с которой работать)
        /// </summary>
        public string InitialCatalog
        {
            get
            {
                bool blocked = false;
                try
                {
                    if (mutex.WaitOne(1000))
                    {
                        blocked = true;
                        return adapter.InitialCatalog;
                    }
                    else
                        throw new TimeoutException();
                }
                finally
                {
                    if (blocked) mutex.ReleaseMutex();
                }
            }
            
            set 
            {
                bool blocked = false;
                try
                {
                    if (mutex.WaitOne(1000))
                    {
                        blocked = true;
                        if (state == DataBaseState.Default)
                        {
                            adapter.InitialCatalog = value;
                        }
                        else
                            throw new InvalidOperationException("Текущее состояние не позволяет присвоить згачение данному свойству");
                    }
                    else
                        throw new TimeoutException();
                }
                finally
                {
                    if (blocked) mutex.ReleaseMutex();
                }
            }
        }

        /// <summary>
        /// Определяет пользователя БД, от имени которого выполнять работу с БД
        /// </summary>
        public string UserID
        {
            get
            {
                bool blocked = false;
                try
                {
                    if (mutex.WaitOne(1000))
                    {
                        blocked = true;
                        return adapter.UserID;
                    }
                    else
                        throw new TimeoutException();
                }
                finally
                {
                    if (blocked) mutex.ReleaseMutex();
                }
            }
            
            set
            {
                bool blocked = false;
                try
                {
                    if (mutex.WaitOne(1000))
                    {
                        blocked = true;
                        if (state == DataBaseState.Default)
                        {
                            adapter.UserID = value;
                        }
                        else
                            throw new InvalidOperationException("Текущее состояние не позволяет присвоить згачение данному свойству");
                    }
                    else
                        throw new TimeoutException();
                }
                finally
                {
                    if (blocked) mutex.ReleaseMutex();
                }
            }
        }

        /// <summary>
        /// Определяет пароль пользователя, от имени которого осуществляется работа с БД
        /// </summary>
        public string Password
        {
            get
            {
                bool blocked = false;
                try
                {
                    if (mutex.WaitOne(1000))
                    {
                        blocked = true;
                        return adapter.Password;
                    }
                    else
                        throw new TimeoutException();
                }
                finally
                {
                    if (blocked) mutex.ReleaseMutex();
                }
            }

            set
            {
                bool blocked = false;
                try
                {
                    if (mutex.WaitOne(1000))
                    {
                        blocked = true;
                        if (state == DataBaseState.Default)
                        {
                            adapter.Password = value;
                        }
                        else
                            throw new InvalidOperationException("Текущее состояние не позволяет присвоить згачение данному свойству");
                    }
                    else
                        throw new TimeoutException();
                }
                finally
                {
                    if (blocked) mutex.ReleaseMutex();
                }  
            }
        }

        /// <summary>
        /// Возвращяет список БД, которые имеются на сервере
        /// </summary>
        public string[] DataBases
        {
            get
            {
                bool blocked = false;
                try
                {
                    if (mutex.WaitOne(1000))
                    {
                        blocked = true;
                        return server.DataBases;
                    }
                    else
                        throw new TimeoutException();
                }
                catch (Exception ex)
                {
                    throw new Exception(ex.Message, ex);
                }
                finally
                {
                    if (blocked) mutex.ReleaseMutex();
                }
            }
        }

        /// <summary>
        /// Создать новую БД
        /// </summary>
        /// <param name="dbName">Имя создаваемой БД</param>
        public void CreateBD(string dbName)
        {
            bool blocked = false;
            try
            {
                if (mutex.WaitOne(1000))
                {
                    blocked = true;
                    server.CreateNewDB(dbName);
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
            finally
            {
                if (blocked) mutex.ReleaseMutex();
            }
        }

        /// <summary>
        /// Удалить БД
        /// </summary>
        /// <param name="dbName">Имя удаляемой БД</param>
        public void RemoveDB(string dbName)
        {
            bool blocked = false;
            try
            {
                if (mutex.WaitOne(1000))
                {
                    blocked = true;

                    if (state == DataBaseState.Default)
                    {
                        server.DeleteDB(dbName);
                    }
                    else
                        throw new Exception("Текущее состояние не позволяет удалять БД");
                }
                else
                    throw new TimeoutException();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
            finally
            {
                if (blocked) mutex.ReleaseMutex();
            }
        }

        /// <summary>
        /// Загрузить БД
        /// </summary>
        public void LoadDB()
        {
            bool blocked = false;
            try
            {
                if (mutex.WaitOne(1000))
                {
                    blocked = true;
                    if (state == DataBaseState.Default)
                    {
                        dataBase.LoadDB();
                        state = DataBaseState.Loaded;
                    }
                    else
                        throw new Exception("Текущее состояние не позволяет загрузить БД");
                }
                else
                    throw new TimeoutException();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
            finally
            {
                if (blocked) mutex.ReleaseMutex();
            }
        }

        /// <summary>
        /// Загрузить БД
        /// </summary>
        /// <param name="dbName">Имя загружаемой БД</param>
        public void LoadDB(string dbName)
        {
            bool blocked = false;
            string old_dbName = string.Empty;

            try
            {
                if (mutex.WaitOne(1000))
                {
                    blocked = true;
                    old_dbName = adapter.InitialCatalog;

                    if (state == DataBaseState.Default)
                    {
                        adapter.InitialCatalog = dbName;
                        dataBase.LoadDB();

                        state = DataBaseState.Loaded;
                    }
                }
                else
                    throw new TimeoutException();
            }
            catch (Exception ex)
            {
                adapter.InitialCatalog = old_dbName;
                throw new Exception(ex.Message, ex);
            }
            finally
            {
                if (blocked) mutex.ReleaseMutex();
            }
        }

        /// <summary>
        /// Закрыть загруженную БД
        /// </summary>
        public void CloseDB()
        {
            bool blocked = false;
            try
            {
                if (mutex.WaitOne(1000))
                {
                    blocked = true;
                    if (state == DataBaseState.Loaded || state == DataBaseState.Saving)
                    {
                        dataBase.CloseDB();
                        state = DataBaseState.Default;
                    }
                    else
                        throw new InvalidOperationException("База данных не загружена.");
                }
                else
                    throw new TimeoutException();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
            finally
            {
                if (blocked) mutex.ReleaseMutex();
            }
        }

        /// <summary>
        /// Возвращяет список параметров загруженной БД.
        /// </summary>
        public DataBaseParameters Parameters
        {
            get
            {
                bool blocked = false;
                try
                {
                    if (mutex.WaitOne(1000))
                    {
                        blocked = true;
                        if (state == DataBaseState.Loaded || state == DataBaseState.Saving)
                        {
                            return dataBase.Parameters;
                        }
                        else
                            throw new InvalidOperationException("База данных не загружена.");
                    }
                    else
                        throw new TimeoutException();
                }
                catch (Exception ex)
                {
                    throw new Exception(ex.Message, ex);
                }
                finally
                {
                    if (blocked) mutex.ReleaseMutex();
                }
            }
        }

        /// <summary>
        /// Добавить параметр в загруженную БД
        /// </summary>
        /// <param name="parameter">Параметр который сохранить</param>
        public void InsertParameter(DataBaseParameter parameter)
        {
            bool blocked = false;
            try
            {
                if (mutex.WaitOne(1000))
                {
                    blocked = true;
                    if (state == DataBaseState.Loaded || state == DataBaseState.Saving)
                    {
                        dataBase.Insert(parameter);
                    }
                    else
                        throw new InvalidOperationException("База данных не загружена.");
                }
                else
                    throw new TimeoutException();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
            finally
            {
                if (blocked) mutex.ReleaseMutex();
            }
        }

        /// <summary>
        /// Добавить параметр в загруженную БД
        /// </summary>
        /// <param name="Identifier">Идентификатор параметра</param>
        public void InsertParameter(Guid Identifier)
        {
            bool blocked = false;
            try
            {
                if (mutex.WaitOne(1000))
                {
                    blocked = true;
                    if (state == DataBaseState.Loaded || state == DataBaseState.Saving)
                    {
                        DataBaseParameter parameter = new DataBaseParameter();
                        DataBaseDescription description = new DataBaseDescription();

                        parameter.Identifier = Identifier;
                        parameter.Descriptions.Add(description);

                        dataBase.Insert(parameter);
                    }
                    else
                        throw new InvalidOperationException("База данных не загружена.");
                }
                else
                    throw new TimeoutException();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
            finally
            {
                if (blocked) mutex.ReleaseMutex();
            }
        }

        /// <summary>
        /// Добавить параметр в загруженную БД
        /// </summary>
        /// <param name="Identifier">Идентификатор параметра</param>
        /// <param name="Name">Имя параметра</param>
        public void InsertParameter(Guid Identifier, String Name)
        {
            bool blocked = false;
            try
            {
                if (mutex.WaitOne(1000))
                {
                    blocked = true;
                    if (state == DataBaseState.Loaded || state == DataBaseState.Saving)
                    {
                        DataBaseParameter parameter = new DataBaseParameter();
                        DataBaseDescription description = new DataBaseDescription();
                        
                        parameter.Identifier = Identifier;
                        
                        description.NameParameter = Name;
                        parameter.Descriptions.Add(description);

                        dataBase.Insert(parameter);
                    }
                    else
                        throw new InvalidOperationException("База данных не загружена.");
                }
                else
                    throw new TimeoutException();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
            finally
            {
                if (blocked) mutex.ReleaseMutex();
            }
        }

        /// <summary>
        /// Удалить параметр в загруженной БД
        /// </summary>
        /// <param name="Identifier">Идентификатор параметра</param>
        public void RemoveParameter(Guid Identifier)
        {
            bool blocked = false;
            try
            {
                if (mutex.WaitOne(1000))
                {
                    blocked = true;
                    if (state == DataBaseState.Loaded || state == DataBaseState.Saving)
                    {
                        dataBase.Remove(Identifier);
                    }
                    else
                        throw new InvalidOperationException("База данных не загружена.");
                }
                else
                    throw new TimeoutException();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
            finally
            {
                if (blocked) mutex.ReleaseMutex();
            }
        }

        /// <summary>
        /// Обновить свойства параметра
        /// </summary>
        /// <param name="Identifier">Идентификатор параметра</param>
        /// <param name="Description">Свойства параметра</param>
        public void UpdateParameterDescription(Guid Identifier, DataBaseDescription Description)
        {
            bool blocked = false;
            try
            {
                if (mutex.WaitOne(1000))
                {
                    blocked = true;
                    if (state == DataBaseState.Loaded || state == DataBaseState.Saving)
                    {
                        dataBase.UpdateParameter(Description, Identifier);
                    }
                    else
                        throw new InvalidOperationException("База данных не загружена.");
                }
                else
                    throw new TimeoutException();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
            finally
            {
                if (blocked) mutex.ReleaseMutex();
            }
        }

        /// <summary>
        /// Загрузить значения параметра
        /// </summary>
        /// <param name="Identifier">Идентификатор параметра</param>
        /// <returns>В случае успеха массив значений параметра, в противном случае null</returns>
        public DataBaseParameterValue[] ReadParameterValues(Guid Identifier)
        {
            bool blocked = false;
            try
            {
                if (mutex.WaitOne(1000))
                {
                    blocked = true;
                    if (state == DataBaseState.Loaded || state == DataBaseState.Saving)
                    {
                        return dataBase.GetParameterValues(Identifier);
                    }
                    else
                        throw new InvalidOperationException("База данных не загруженна");
                }
                else
                    throw new TimeoutException();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
            finally
            {
                if (blocked) mutex.ReleaseMutex();
            }
        }

        /// <summary>
        /// Загрузить значения параметра
        /// </summary>
        /// <param name="Identifier">Идентификатор параметра</param>
        /// <param name="StartTime">Начальное время</param>
        /// <param name="FinishTime">Конечное время</param>
        /// <returns>В случае успеха массив значений параметра, в противном случае null</returns>
        public DataBaseParameterValue[] ReadParameterValues(Guid Identifier, long StartTime, long FinishTime)
        {
            bool blocked = false;
            try
            {
                if (mutex.WaitOne(1000))
                {
                    blocked = true;
                    if (state == DataBaseState.Loaded || state == DataBaseState.Saving)
                    {
                        return dataBase.GetParameterValues(Identifier, StartTime, FinishTime);
                    }
                    else
                        throw new InvalidOperationException("База данных не загруженна");
                }
                else
                    throw new TimeoutException();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
            finally
            {
                if (blocked) mutex.ReleaseMutex();
            }
        }


        /// <summary>
        /// Получить агента, позволяющего сохранять значения параметра
        /// </summary>
        /// <returns>В случае успеха агент для сохранения значений параметра, в противном случае null</returns>
        public DataBaseSaverAgent CreateAgent()
        {
            bool blocked = false;
            try
            {
                if (mutex.WaitOne(1000))
                {
                    blocked = true;
                    return new DataBaseSaverAgent(dataBase.DataBaseSaver);
                }
                else
                    throw new TimeoutException();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
            finally
            {
                if (blocked) mutex.ReleaseMutex();
            }
        }

        /// <summary>
        /// Переключить в режим записи значений параметров
        /// </summary>
        public void TurnOnToSavingMode()
        {
            bool blocked = false;
            try
            {
                if (mutex.WaitOne(1000))
                {
                    blocked = true;

                    if (state == DataBaseState.Loaded)
                    {
                        dataBase.DataBaseSaver.Start();
                        state = DataBaseState.Saving;
                    }
                    else
                        throw new InvalidOperationException();
                }
                else
                    throw new TimeoutException();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
            finally
            {
                if (blocked) mutex.ReleaseMutex();
            }
        }

        /// <summary>
        /// Переключить в режим работы с загруженной БД (остановить запись параметров в БД)
        /// </summary>
        public void TurnOffFromSavingMode()
        {
            bool blocked = false;
            try
            {
                if (mutex.WaitOne(1000))
                {
                    blocked = true;
                    if (state == DataBaseState.Saving)
                    {
                        dataBase.DataBaseSaver.Stop();
                        state = DataBaseState.Loaded;
                    }
                    else
                        throw new InvalidOperationException();
                }
                else
                    throw new TimeoutException();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
            finally
            {
                if (blocked) mutex.ReleaseMutex();
            }
        }
    }

    /// <summary>
    /// Перечисление состояний БД
    /// </summary>
    public enum DataBaseState
    {
        /// <summary>
        /// Загруженна БД
        /// </summary>
        Loaded,

        /// <summary>
        /// Выполняется сохранение значений параметров в БД
        /// </summary>
        Saving,

        /// <summary>
        /// По умолчанию.
        /// </summary>
        Default
    }
}