using System;
using System.Data;
using System.Data.SqlTypes;
using System.Data.SqlClient;
using System.Runtime.Serialization;

namespace DataBase
{
    /// <summary>
    /// Реализует способ соединения с логической структурой БД
    /// </summary>
    [Serializable]
    public class DataBaseAdapter : ISerializable
    {
        // ---- данные класса ----

        private string db_server = string.Empty;                    // имя сервера БД
        private string db_name = string.Empty;                      // каталог к торомому подключиться

        private string db_user = string.Empty;                      // учетная запись под которой подключаться
        private string db_password = string.Empty;                  // пароль учетной записи

        [NonSerialized]
        private DataBaseStructure db_structure;                     // структура БД

        /// <summary>
        /// Инициализирует новый экземпляр класса
        /// </summary>
        public DataBaseAdapter()
        {
            db_server = "localhost";
            db_structure = new DataBaseStructure();
        }

        /// <summary>
        /// Инициализирует новый экземпляр класса
        /// </summary>
        /// <param name="info">Объект System.Runtime.Serialization.SerializationInfo для извлечения данных.</param>
        /// <param name="context">Целевое местоположение сериализации.</param>
        protected DataBaseAdapter(SerializationInfo info, StreamingContext context)
        {
            db_server = info.GetString("db_server");
            db_name = info.GetString("db_name");

            db_user = info.GetString("db_user");
            db_password = info.GetString("db_password");

            db_structure = new DataBaseStructure();
        }

        /// <summary>
        /// Заполняет объект System.Runtime.Serialization.SerializationInfo данными,
        /// необходимыми для сериализации целевого объекта.
        /// </summary>
        /// <param name="info">Объект System.Runtime.Serialization.SerializationInfo для заполнения данными.</param>
        /// <param name="context">Целевое местоположение сериализации.</param>
        public virtual void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            info.AddValue("db_server", db_server);
            info.AddValue("db_name", db_name);
            info.AddValue("db_user", db_user);
            info.AddValue("db_password", db_password);
        }

        /// <summary>
        /// Инициализирует новый экземпляр класса
        /// </summary>
        /// <param name="Host">Адрес сервера к которому подключиться</param>
        /// <param name="Catalog">Имя БД к которой подключиться</param>
        /// <param name="User">Пользователь БД, от имени которого выполнять работу с БД</param>
        /// <param name="Passcode">Пароль пользователя</param>
        public DataBaseAdapter(string Host, string Catalog, string User, string Passcode)
        {
            db_server = Host;
            db_name = Catalog;

            db_user = User;
            db_password = Passcode;

            db_structure = new DataBaseStructure();
        }

        /// <summary>
        /// Определяет имя сервера БД (localhost)
        /// </summary>
        public string DataSource
        {
            get { return db_server; }
            set { db_server = value; }            
        }

        /// <summary>
        /// Определяет каталог к торомому подключиться (имя БД с которой работать)
        /// </summary>
        public string InitialCatalog
        {
            get { return db_name; }
            set { db_name = value; }
        }

        /// <summary>
        /// Определяет пользователя БД, от имени которого выполнять работу с БД
        /// </summary>
        public string UserID
        {
            get { return db_user; }
            set { db_user = value; }
        }

        /// <summary>
        /// Определяет пароль пользователя, от имени которого осуществляется работа с БД
        /// </summary>
        public string Password
        {
            get { return db_password; }
            set { db_password = value; }
        }

        /// <summary>
        /// Возвращяет строку подключения к серверу БД
        /// </summary>
        public string ConnectionStringToServer
        {
            get
            {
                SqlConnectionStringBuilder builber = new SqlConnectionStringBuilder();

                builber.DataSource = db_server;
                builber.IntegratedSecurity = true;

                builber.UserID = db_user;
                builber.Password = db_password;

                return builber.ConnectionString;
            }
        }

        /// <summary>
        /// Возвращяет строку подключения к БД
        /// </summary>
        public string ConnectionString
        {
            get
            {
                SqlConnectionStringBuilder builber = new SqlConnectionStringBuilder();

                builber.DataSource = DataSource;
                builber.InitialCatalog = InitialCatalog;

                builber.UserID = UserID;
                builber.Password = Password;

                return builber.ConnectionString;
            }
        }

        /// <summary>
        /// Возвращяет структуру БД на основе которой осуществляется работа с БД.
        /// Только для чтения.
        /// </summary>
        public DataBaseStructure Structure
        {
            get { return db_structure; }
        }
    }
}