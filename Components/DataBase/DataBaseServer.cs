using System;
using System.Data;
using System.Data.SqlTypes;
using System.Data.SqlClient;
using System.Collections.Generic;

namespace DataBase
{
    /// <summary>
    /// Реализует работу с сервером БД с учетем наших интересов
    /// </summary>
    public class DataBaseServer
    {
        // ---- Sql запросы к БД ----

        /// <summary>
        /// Запрос на получение списка всех баз данных, которые имеются на сервере
        /// </summary>
        protected const string sql_query_select_all_db = "SELECT * FROM sys.databases";

        /// <summary>
        /// Запрос на получение списка всех баз данных, которые имеются на сервере
        /// </summary>
        protected const string sql_query_select_all_db2000 = "SELECT * FROM sysdatabases";

        /// <summary>
        /// Определяет запрос который вставляет служебную информацию в БД (необходима для совместимости)
        /// </summary>
        protected const string sql_query_insert_first_param = "INSERT INTO dbo.t_Param (id, id_param, val_param) VALUES (1, 1, \'DBase.3\')";

        // ---- данные класса ----

        private DataBaseAdapter adapter = null;                  // предоставляет сервисные функции и методы для работу с сервером БД

        /// <summary>
        /// Инициализирует новый экземпляр класса
        /// </summary>
        /// <param name="Adapter">Адаптер подключения к серверу</param>
        public DataBaseServer(DataBaseAdapter Adapter)
        {
            adapter = Adapter;
        }

        /// <summary>
        /// Список имеющихся БД на сервере.
        /// Только для чтения.
        /// </summary>
        public string[] DataBases
        {
            get
            {
                SqlConnection connection = null;
                try
                {
                    connection = new SqlConnection();
                    connection.ConnectionString = adapter.ConnectionStringToServer;

                    connection.Open();
                    if (connection.State == ConnectionState.Open)
                    {
                        SqlCommand cmd = new SqlCommand(sql_query_select_all_db);

                        cmd.Connection = connection;
                        SqlDataReader sql_reader = cmd.ExecuteReader();

                        if (sql_reader != null && !sql_reader.IsClosed)
                        {
                            if (sql_reader.HasRows)
                            {
                                List<string> names = new List<string>();
                                while (sql_reader.Read())
                                {
                                    names.Add(sql_reader.GetString(0));
                                }
                                return
                                    names.ToArray();
                            }
                        }
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
                            SqlConnection.ClearPool(connection);
                        }

                        connection.Dispose();
                    }
                }
                return null;
            }
        }

        /// <summary>
        /// получить список БД из SQL Server 2000
        /// </summary>
        /// <returns></returns>
        protected string[] GetDataBasesFrom2000()
        {
            SqlConnection connection = null;
            try
            {
                connection = new SqlConnection();
                connection.ConnectionString = adapter.ConnectionStringToServer;

                connection.Open();
                if (connection.State == ConnectionState.Open)
                {
                    SqlCommand cmd = new SqlCommand(sql_query_select_all_db2000);

                    cmd.Connection = connection;
                    SqlDataReader sql_reader = cmd.ExecuteReader();

                    if (sql_reader != null && !sql_reader.IsClosed)
                    {
                        if (sql_reader.HasRows)
                        {
                            List<string> names = new List<string>();
                            while (sql_reader.Read())
                            {
                                names.Add(sql_reader.GetString(0));
                            }
                            return
                                names.ToArray();
                        }
                    }
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
                        SqlConnection.ClearPool(connection);
                    }

                    connection.Dispose();
                }
            }
            return null;

        }

        /// <summary>
        /// Создать новую БД
        /// </summary>
        /// <param name="dbName">Имя создаваемой БД</param>
        public void CreateNewDB(string dbName)
        {
            SqlConnection connection = null;
            try
            {
                connection = new SqlConnection(adapter.ConnectionStringToServer);
                connection.Open();

                if (connection.State == ConnectionState.Open)
                {                    
                    string sql_query = string.Format("CREATE DATABASE {0}", dbName);
                    SqlCommand cmd = connection.CreateCommand();

                    cmd.CommandText = sql_query;
                    cmd.ExecuteNonQuery();

                    adapter.InitialCatalog = dbName;
                    CreateTables();
                }
                else
                    throw new Exception("Не удалось подключиться к серверу.");
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
                        SqlConnection.ClearPool(connection);
                    }

                    connection.Dispose();
                }
            }
        }

        /// <summary>
        /// Создать таблицы в БД.
        /// В БД не должно быть ни одной таблицы!
        /// </summary>
        /// <param name="dbName">Имя БД в которой создать таблицы</param>
        protected void CreateTables()
        {
            SqlConnection connection = null;
            try
            {
                connection = new SqlConnection(adapter.ConnectionString);
                connection.Open();

                if (connection.State == ConnectionState.Open)
                {
                    string[] sql_queries = { adapter.Structure.MainTable.SqlQueryForCreateTable, adapter.Structure.MarkerTable.SqlQueryForCreateTable,
                                               adapter.Structure.TimeTable.SqlQueryForCreateTable, adapter.Structure.OpListTable.SqlQueryForCreateTable,
                                               adapter.Structure.ParametersTable.SqlQueryForCreateTable };

                    foreach (var sql_query in sql_queries)
                    {
                        SqlCommand command = new SqlCommand(sql_query, connection);
                        command.ExecuteNonQuery();
                    }

                    SqlCommand cmd = new SqlCommand(sql_query_insert_first_param, connection);
                    if (cmd.ExecuteNonQuery() != 1)
                    {
                        throw new Exception("Не удалось создать дополнительный параметр.");
                    }
                    else
                        cmd.Dispose();
                }
                else
                    throw new Exception("Не удалось подключиться к серверу.Таблицы не созданы.");
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
                        SqlConnection.ClearPool(connection);
                    }

                    connection.Dispose();
                }
            }
        }

        /// <summary>
        /// Удалить БД
        /// </summary>
        /// <param name="dbName">Имя удаляемой БД</param>
        public void DeleteDB(string dbName)
        {
            SqlConnection connection = null;
            try
            {
                connection = new SqlConnection(adapter.ConnectionStringToServer);
                connection.Open();

                if (connection.State == ConnectionState.Open)
                {
                    string sql_query = string.Format("DROP DATABASE {0}", dbName);
                    SqlCommand cmd = new SqlCommand(sql_query);

                    cmd.Connection = connection;
                    cmd.ExecuteNonQuery();
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
                        SqlConnection.ClearPool(connection);
                    }

                    connection.Dispose();
                }
            }
        }
    }
}