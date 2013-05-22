using System;
using System.Data;
using System.Data.SqlTypes;
using System.Data.SqlClient;
using System.Collections.Generic;

using SKC;

namespace DataBase
{
    /// <summary>
    /// Реализует работу с конкретной БД
    /// </summary>
    public class DataBase
    {
        // ---- данные класса ----

        private DataBaseAdapter adapter = null;                         // посредник между БД и компонентом
        private DataBaseParameters parameters = null;                   // загруженные из БД параметры

        private DataBaseSaver saver = null;                             // сохраняет значения параметров в БД
        private DataBaseStructure structure = null;                     // хранить структуру БД

        /// <summary>
        /// Инициализирует новый экземпляр класса
        /// </summary>
        public DataBase(DataBaseAdapter adap)
        {
            adapter = adap;

            structure = new DataBaseStructure();
            parameters = new DataBaseParameters(1024);

            saver = new DataBaseSaver(adapter, parameters);
        }

        /// <summary>
        /// Возвращяет адаптер подключения к БД
        /// </summary>
        public DataBaseAdapter Adapter
        {
            get { return adapter; }
        }

        /// <summary>
        /// Возвращяет параметры, которые содержаться в БД
        /// </summary>
        public DataBaseParameters Parameters
        {
            get { return parameters; }
        }

        /// <summary>
        /// Загружает БД
        /// </summary>
        public void LoadDB()
        {
            SqlConnection connection = null;
            try
            {
                connection = new SqlConnection(adapter.ConnectionString);
                connection.Open();

                if (connection.State == ConnectionState.Open)
                {
                    SqlCommand command = new SqlCommand(structure.MainTable.SqlQueryForSelectAll, connection);

                    SqlDataReader result = command.ExecuteReader();
                    if (result != null)
                    {
                        if (result.IsClosed == false)
                        {
                            //parameters = new DataBaseParameters(1024);
                            while (result.Read())
                            {
                                DataBaseParameter db_parameter = new DataBaseParameter();

                                db_parameter.ID = result.GetInt32(structure.MainTable[0].IndexInTable);
                                db_parameter.Created = DateTime.FromOADate(result.GetDouble(structure.MainTable[1].IndexInTable));

                                db_parameter.Numbe_Prm = result.GetInt32(structure.MainTable[2].IndexInTable);
                                db_parameter.tblHistory = result.GetString(structure.MainTable[3].IndexInTable);

                                db_parameter.tblValues = result.GetString(structure.MainTable[4].IndexInTable);

                                try
                                {
                                    db_parameter.Identifier = new Guid(result.GetString(structure.MainTable[5].IndexInTable));
                                }
                                catch
                                {
                                }

                                ParameterDescriptionLoad(db_parameter);

                                if (parameters == null)
                                {
                                    parameters = new DataBaseParameters(1024);
                                }
                                parameters.Insert(db_parameter);

                                //saver.Inialize(adapter, parameters);                                
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                //ErrorHandler.WriteToLog(this, new ErrorArgs(ex.Message, ErrorType.NotFatal));
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
        /// Закрыть БД
        /// </summary>
        public void CloseDB()
        {
            SqlConnection.ClearAllPools();
            if (saver != null)
            {
                saver.Stop();
            }

            parameters = null;
        }

        /// <summary>
        /// Загрузить описание параметра из БД
        /// </summary>
        /// <param name="db_parameter">Параметр для которого загрузить описание</param>
        private void ParameterDescriptionLoad(DataBaseParameter db_parameter)
        {
            SqlConnection connection = null;
            try
            {
                string sql_query = string.Format("SELECT * FROM {0}", db_parameter.tblHistory);

                connection = new SqlConnection(adapter.ConnectionString);
                connection.Open();

                if (connection.State == ConnectionState.Open)
                {
                    SqlCommand command = new SqlCommand(sql_query, connection);
                    SqlDataReader result = command.ExecuteReader();

                    if (result != null)
                    {
                        if (result.IsClosed == false)
                        {
                            while (result.Read())
                            {
                                DataBaseDescription db_description = new DataBaseDescription();

                                db_description.ID = result.GetInt32(structure.HistoryTable["id"].IndexInTable);
                                db_description.dtCreate = DateTime.FromOADate(result.GetDouble(structure.HistoryTable["dtCreate"].IndexInTable));

                                db_description.MainKey = result.GetInt32(structure.HistoryTable["main_key"].IndexInTable);
                                db_description.NumberParameter = result.GetInt32(structure.HistoryTable["numbe_prm"].IndexInTable);

                                db_description.NameParameter = result.GetString(structure.HistoryTable["name_prm"].IndexInTable);
                                db_description.TypeParameter = result.GetString(structure.HistoryTable["type_prm"].IndexInTable);

                                db_description.Val_block_up = result.GetFloat(structure.HistoryTable["val_block_up"].IndexInTable);
                                db_description.Val_block_down = result.GetFloat(structure.HistoryTable["val_block_down"].IndexInTable);

                                db_description.Val_avar = result.GetFloat(structure.HistoryTable["val_avar"].IndexInTable);
                                db_description.Val_max = result.GetFloat(structure.HistoryTable["val_max"].IndexInTable);

                                db_description.Val_min = result.GetFloat(structure.HistoryTable["val_min"].IndexInTable);

                                db_description.Calibr_1 = result.GetFloat(structure.HistoryTable["calibr_1"].IndexInTable);
                                db_description.Calibr_2 = result.GetFloat(structure.HistoryTable["calibr_2"].IndexInTable);

                                db_description.Calibr_3 = result.GetFloat(structure.HistoryTable["calibr_3"].IndexInTable);
                                db_description.Calibr_4 = result.GetFloat(structure.HistoryTable["calibr_4"].IndexInTable);

                                db_description.Calibr_5 = result.GetFloat(structure.HistoryTable["calibr_5"].IndexInTable);
                                db_description.Calibr_6 = result.GetFloat(structure.HistoryTable["calibr_6"].IndexInTable);

                                db_description.Calibr_7 = result.GetFloat(structure.HistoryTable["calibr_7"].IndexInTable);
                                db_description.Calibr_8 = result.GetFloat(structure.HistoryTable["calibr_8"].IndexInTable);

                                db_description.Calibr_9 = result.GetFloat(structure.HistoryTable["calibr_9"].IndexInTable);
                                db_description.Calibr_10 = result.GetFloat(structure.HistoryTable["calibr_10"].IndexInTable);

                                db_description.Snd_avar = result.GetString(structure.HistoryTable["snd_avar"].IndexInTable);
                                db_description.Snd_max = result.GetString(structure.HistoryTable["snd_max"].IndexInTable);

                                db_description.Graf_switch = result.GetInt32(structure.HistoryTable["graf_switch"].IndexInTable);
                                db_description.Graf_diapz = result.GetFloat(structure.HistoryTable["graf_diapz"].IndexInTable);

                                db_description.Graf_min = result.GetFloat(structure.HistoryTable["graf_min"].IndexInTable);
                                db_description.Graf_max = result.GetFloat(structure.HistoryTable["graf_max"].IndexInTable);

                                db_description.Contr_par = result.GetInt32(structure.HistoryTable["contr_par"].IndexInTable);
                                db_description.Res_str = result.GetString(structure.HistoryTable["res_str"].IndexInTable);

                                db_description.Res_float1 = result.GetFloat(structure.HistoryTable["res_float1"].IndexInTable);
                                db_description.Res_float2 = result.GetFloat(structure.HistoryTable["res_float2"].IndexInTable);

                                db_description.Res_int1 = result.GetInt32(structure.HistoryTable["res_int1"].IndexInTable);
                                db_description.Res_int2 = result.GetInt32(structure.HistoryTable["res_int2"].IndexInTable);

                                db_parameter.Descriptions.Add(db_description);
                            }
                        }
                    }
                    else
                        throw new Exception("Не удалось считать данные описания параметра");
                }
                else
                    throw new Exception("Не удалось подключиться к серверу БД.");
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
        /// Добавить параметр в БД
        /// </summary>
        /// <param name="parameter">Параметр, который необходимо добавить</param>
        public void Insert(DataBaseParameter parameter)
        {
            SqlConnection connection = null;
            SqlTransaction transaction = null;

            try
            {
                connection = new SqlConnection(adapter.ConnectionString);
                connection.Open();

                if (connection.State == ConnectionState.Open)
                {
                    if (Peek(parameter.Identifier) == false)
                    {
                        if (Peek(parameter.ID) == false)
                        {
                            transaction = connection.BeginTransaction();

                            CreateParameterInMainTable(connection, transaction, parameter);
                            CreateParameterHistoryTable(connection, transaction, parameter);

                            CreateParameterValuesTable(connection, transaction, parameter);
                            transaction.Commit();

                            if (parameters == null)
                            {
                                parameters = new DataBaseParameters(1024);
                            }

                            parameters.Insert(parameter);
                            //saver.Inialize(adapter, parameters);
                        }
                        else
                            throw new Exception("Параметр с указанным номером существует в БД.");
                    }
                    else
                        throw new Exception("Параметр с указанным Guid существует в БД");
                }
                else
                    throw new Exception("Не удалось подключиться к БД.");
            }
            catch (Exception ex)
            {
                try
                {
                    transaction.Rollback();
                }
                catch (Exception ex1)
                {
                    throw new Exception(ex1.Message, ex1);
                }

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

                if (transaction != null)
                    transaction.Dispose();
            }
        }

        /// <summary>
        /// Удалить параметр по его идентификатору
        /// </summary>
        /// <param name="Identifier">Идентификатор удаляемого параметра</param>
        public void Remove(Guid Identifier)
        {
            SqlConnection connection = null;
            SqlTransaction transaction = null;

            try
            {
                connection = new SqlConnection(adapter.ConnectionString);
                connection.Open();

                if (connection.State == ConnectionState.Open)
                {
                    transaction = connection.BeginTransaction();

                    SqlCommand fParameter = connection.CreateCommand();
                    fParameter.Transaction = transaction;

                    fParameter.Parameters.Add(structure.MainTable["guid"].Parameter);
                    fParameter.Parameters[0].Value = Identifier.ToString();

                    fParameter.CommandText = string.Format("Select * From {0} Where guid = @guid", structure.MainTable.Name);

                    SqlDataReader reader = fParameter.ExecuteReader();
                    if (reader != null)
                    {
                        if (reader.IsClosed == false)
                        {
                            string tblHistory = string.Empty;
                            string tblValues = string.Empty;

                            while (reader.Read())
                            {
                                tblHistory = reader.GetString(structure.MainTable["tab_hist"].IndexInTable);
                                tblValues = reader.GetString(structure.MainTable["tab_val"].IndexInTable);
                            }

                            reader.Close();

                            if (tblHistory != string.Empty)
                            {
                                SqlCommand drop_hist = connection.CreateCommand();
                                drop_hist.Transaction = transaction;

                                drop_hist.CommandText = string.Format("Drop Table {0}", tblHistory);
                                drop_hist.ExecuteNonQuery();
                            }

                            if (tblValues != string.Empty)
                            {
                                SqlCommand drop_values = connection.CreateCommand();
                                drop_values.Transaction = transaction;

                                drop_values.CommandText = string.Format("Drop Table {0}", tblValues);
                                drop_values.ExecuteNonQuery();
                            }

                            SqlCommand eraser = connection.CreateCommand();
                            eraser.Transaction = transaction;

                            eraser.Parameters.Add(new SqlParameter("guid", Identifier.ToString()));
                            eraser.Parameters[0].SqlDbType = SqlDbType.VarChar;

                            eraser.CommandText = string.Format("Delete From {0} Where guid = @guid", structure.MainTable.Name);
                            eraser.ExecuteNonQuery();
                        }
                    }

                    transaction.Commit();

                    if (parameters != null)
                    {
                        parameters.Remove(Identifier);
                    }
                }
                else
                    throw new Exception("Не удалось подключиться к БД.");
            }
            catch (Exception ex)
            {
                try
                {
                    transaction.Rollback();
                }
                catch (Exception ex1)
                {
                    throw new Exception(ex1.Message, ex1);
                }

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

                if (transaction != null)
                    transaction.Dispose();
            }
        }

        /// <summary>
        /// Обновить значение свойств параметра
        /// </summary>
        /// <param name="Description">Обновляемое свойство параметра</param>
        /// <param name="Identifier">Идентификатор параметра</param>
        public void UpdateParameter(DataBaseDescription Description, Guid Identifier)
        {
            SqlConnection connection = null;
            SqlTransaction transaction = null;

            try
            {
                connection = new SqlConnection(adapter.ConnectionString);
                connection.Open();

                if (connection.State == ConnectionState.Open)
                {
                    if (Peek(Identifier))
                    {
                        transaction = connection.BeginTransaction();

                        SqlCommand fParameter = connection.CreateCommand();
                        fParameter.Transaction = transaction;

                        fParameter.Parameters.Add(new SqlParameter("guid", Identifier.ToString()));
                        fParameter.Parameters[0].SqlDbType = SqlDbType.VarChar;

                        fParameter.CommandText = string.Format("Select * From {0} Where guid = @guid", structure.MainTable.Name);

                        SqlDataReader reader = fParameter.ExecuteReader();
                        if (reader != null)
                        {
                            if (reader.IsClosed == false)
                            {
                                string tblHistory = string.Empty;
                                while (reader.Read())
                                {
                                    tblHistory = reader.GetString(structure.MainTable["tab_hist"].IndexInTable);
                                }

                                reader.Close();
                                Description.dtCreate = DateTime.Now;

                                UpdateParameterHistory(connection, transaction, Description, tblHistory);
                                transaction.Commit();
                            }
                        }
                    }
                    else
                        throw new Exception("Данного параметра нет в БД.");
                }
                else
                    throw new Exception("Не удалось установить соединение с БД.");
            }
            catch (Exception ex)
            {
                try
                {
                    transaction.Rollback();
                }
                catch (Exception ex1)
                {
                    throw new Exception(ex1.Message, ex1);
                }

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

                if (transaction != null)
                    transaction.Dispose();
            }
        }

        /// <summary>
        /// Проверить наличие параметра в БД по его Guid
        /// </summary>
        /// <param name="guid">Идентификатор параметра</param>
        /// <returns>
        /// Булево значение показывающее имеется ли в БД параметр или нет. 
        /// true - в БД имеется параметр с указанным Guid, false - параметр отсутствует в БД.
        /// </returns>
        public bool Peek(Guid guid)
        {
            SqlConnection connection = null;
            try
            {
                connection = new SqlConnection(adapter.ConnectionString);
                connection.Open();

                if (connection.State == ConnectionState.Open)
                {
                    SqlCommand command = new SqlCommand("SELECT COUNT (*) FROM dbo.t_main WHERE guid = @guid", connection);

                    SqlParameter p = new SqlParameter("guid", guid.ToString());
                    p.SqlDbType = SqlDbType.VarChar;

                    command.Parameters.Add(p);

                    int count = (Int32)command.ExecuteScalar();

                    switch (count)
                    {
                        case 0:

                            return false;

                        case 1:

                            return true;

                        case -1:

                            return false;

                        default:

                            throw new Exception("Значение Guid не корректно. Не может быть два одинаковых идентификатора.");
                    }
                }
                else
                    throw new Exception("Не удалось подключиться к БД.");
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
        /// Проверить наличие параметра в БД по его номеру
        /// </summary>
        /// <param name="numbe_prm">Номер параметра</param>
        /// <returns>
        /// Булево значение показывающее имеется ли в БД параметр или нет. 
        /// true - в БД имеется параметр с указанным Guid, false - параметр отсутствует в БД.
        /// </returns>
        public bool Peek(int numbe_prm)
        {
            SqlConnection connection = null;
            try
            {
                connection = new SqlConnection(adapter.ConnectionString);
                connection.Open();

                if (connection.State == ConnectionState.Open)
                {
                    SqlCommand command = new SqlCommand("SELECT COUNT (*) FROM dbo.t_main WHERE numbe_prm = @numbe_prm", connection);

                    SqlParameter p = new SqlParameter("numbe_prm", numbe_prm);
                    p.SqlDbType = SqlDbType.Int;

                    command.Parameters.Add(p);

                    int count = (Int32)command.ExecuteScalar();

                    switch (count)
                    {
                        case 0:

                            return false;

                        case 1:

                            return true;

                        case -1:

                            return false;

                        default:

                            throw new Exception("Значение numbe_prm не корректно. Не может быть два одинаковых идентификатора.");
                    }
                }
                else
                    throw new Exception("Не удалось подключиться к БД.");
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
        /// Получить все значения параметра из БД
        /// </summary>
        /// <param name="Identifier">Идентификатор параметра</param>
        /// <returns>В случае успеха массив значений параметра, в противном случае null</returns>
        public DataBaseParameterValue[] GetParameterValues(Guid Identifier)
        {
            SqlConnection connection = null;
            try
            {
                connection = new SqlConnection(adapter.ConnectionString);
                connection.Open();

                if (connection.State == ConnectionState.Open)
                {
                    if (Peek(Identifier))
                    {
                        SqlCommand command = connection.CreateCommand();

                        command.Parameters.Add(structure.MainTable["guid"].Parameter);
                        command.Parameters[0].Value = Identifier.ToString();

                        command.CommandText = string.Format("Select tab_val From {0} Where guid = @guid", structure.MainTable.Name);

                        object result = command.ExecuteScalar();
                        if (result != null)
                        {
                            string tbl = string.Format("dbo.{0}", (string)result);

                            SqlCommand cmd = connection.CreateCommand();

                            cmd.CommandText = string.Format("Select dbo.t_measuring.val_Time, {0}.val_prm From dbo.t_measuring, {0} Where " +
                                "dbo.t_measuring.id = {0}.id", tbl);

                            SqlDataReader reader = cmd.ExecuteReader();
                            if (reader != null)
                            {
                                if (reader.IsClosed == false)
                                {
                                    List<DataBaseParameterValue> values = new List<DataBaseParameterValue>();
                                    while (reader.Read())
                                    {
                                        DateTime time = DateTime.FromOADate(reader.GetDouble(0));
                                        Single value = reader.GetFloat(1);

                                        DataBaseParameterValue val = new DataBaseParameterValue(Identifier, time.Ticks, value);
                                        values.Add(val);
                                    }

                                    return values.ToArray();
                                }
                                else
                                    return null;
                            }
                            else
                                throw new Exception("Не удалось считать данные параметра");
                        }
                        else
                            throw new Exception("Не удалось считать данные параметра");
                    }
                    else
                        throw new Exception("Указанного параметр отсутствует в БД");
                }
                else
                    throw new Exception("Не удалось подключиться к БД");
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
        /// Получить все значения параметра из БД
        /// </summary>
        /// <param name="Identifier">Идентификатор параметра</param>
        /// <param name="StartTime">Начальное время</param>
        /// <param name="FinishTime">Конечное время</param>
        /// <returns>В случае успеха массив значений параметра, в противном случае null</returns>
        public DataBaseParameterValue[] GetParameterValues(Guid Identifier, long StartTime, long FinishTime)
        {
            SqlConnection connection = null;
            try
            {
                connection = new SqlConnection(adapter.ConnectionString);
                connection.Open();

                if (connection.State == ConnectionState.Open)
                {
                    if (Peek(Identifier))
                    {
                        SqlCommand command = connection.CreateCommand();

                        command.Parameters.Add(structure.MainTable["guid"].Parameter);
                        command.Parameters[0].Value = Identifier.ToString();

                        command.CommandText = string.Format("Select tab_val From {0} Where guid = @guid", structure.MainTable.Name);

                        object result = command.ExecuteScalar();
                        if (result != null)
                        {
                            string tbl = string.Format("dbo.{0}", (string)result);

                            SqlCommand cmd = connection.CreateCommand();

                            cmd.CommandText = string.Format("Select dbo.t_measuring.val_Time, {0}.val_prm From dbo.t_measuring, {0} Where " +
                                "dbo.t_measuring.val_Time >= @StartTime AND dbo.t_measuring.val_Time <= @FinishTime AND dbo.t_measuring.id = {0}.id", tbl);

                            DateTime startTime = DateTime.FromBinary(StartTime);
                            DateTime finishTime = DateTime.FromBinary(FinishTime);

                            cmd.Parameters.Add(new SqlParameter("StartTime", startTime.ToOADate()));
                            cmd.Parameters[0].SqlDbType = SqlDbType.Float;

                            cmd.Parameters.Add(new SqlParameter("FinishTime", finishTime.ToOADate()));
                            cmd.Parameters[1].SqlDbType = SqlDbType.Float;

                            SqlDataReader reader = cmd.ExecuteReader();
                            if (reader != null)
                            {
                                if (reader.IsClosed == false)
                                {
                                    List<DataBaseParameterValue> values = new List<DataBaseParameterValue>();
                                    while (reader.Read())
                                    {
                                        DateTime time = DateTime.FromOADate(reader.GetDouble(0));
                                        Single value = reader.GetFloat(1);

                                        DataBaseParameterValue val = new DataBaseParameterValue(Identifier, time.Ticks, value);
                                        values.Add(val);
                                    }

                                    return values.ToArray();
                                }
                                else
                                    return null;
                            }
                            else
                                throw new Exception("Не удалось считать данные параметра");
                        }
                        else
                            throw new Exception("Не удалось считать данные параметра");
                    }
                    else
                        throw new Exception("Указанного параметр отсутствует в БД");
                }
                else
                    throw new Exception("Не удалось подключиться к БД");
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
        /// Возвращяет объект который позволяет сохранять значения параметрв в БД.
        /// При обращении создается новый объект!
        /// </summary>
        public DataBaseSaver DataBaseSaver
        {
            get
            {
                return saver;
            }
        }

        /// <summary>
        /// Получить следующий id записи
        /// </summary>
        /// <param name="table">Таблица в которой искать</param>
        /// <param name="field">Название поля в котором искать</param>
        /// <returns>В случае успеха не отрицательное число</returns>
        protected int GetNextId(string table, string field)
        {
            SqlConnection connection = null;
            try
            {
                connection = new SqlConnection(adapter.ConnectionString);
                connection.Open();

                if (connection.State == ConnectionState.Open)
                {
                    string sql_query = string.Format("SELECT {0} FROM {1} ORDER BY {0} ASC", field, table);

                    SqlCommand command = new SqlCommand(sql_query, connection);
                    SqlDataReader reader = command.ExecuteReader();

                    if (reader != null && !reader.IsClosed)
                    {
                        if (reader.HasRows)
                        {
                            int last = -1;
                            while (reader.Read())
                            {
                                // ---- перебираем значения полей ----

                                if (last == -1)
                                {
                                    last = reader.GetInt32(0);
                                }
                                else
                                {
                                    int current = reader.GetInt32(0);
                                    if (current > (last + 1))
                                    {
                                        return (last + 1);
                                    }
                                    else
                                        last = current;
                                }
                            }

                            return (last + 1);
                        }
                        else
                            return 0;
                    }
                }
                else
                    throw new Exception("Не удалось подключиться к БД");
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

            return -1;

        }

        /// <summary>
        /// Создать запись о параметре в главной таблице БД
        /// </summary>
        /// <param name="connection">Соединение с которым работать</param>
        /// <param name="transaction">Транзакция через которую осуществлять запись в БД</param>
        /// <param name="db_parameter">Параметр, который добавить в БД</param>
        protected void CreateParameterInMainTable(SqlConnection connection, SqlTransaction transaction,
            DataBaseParameter db_parameter)
        {
            try
            {
                db_parameter.ID = GetNextId(structure.MainTable.Name, "id");
                db_parameter.Numbe_Prm = db_parameter.ID;

                db_parameter.Created = DateTime.Now;

                db_parameter.tblHistory = string.Format("History_{0}", db_parameter.Numbe_Prm);
                db_parameter.tblValues = string.Format("Values_{0}", db_parameter.Numbe_Prm);

                SqlCommand command = connection.CreateCommand();
                command.Transaction = transaction;

                command.Parameters.Add(structure.MainTable["id"].Parameter);
                command.Parameters[0].Value = db_parameter.ID;

                command.Parameters.Add(structure.MainTable["dtCreate"].Parameter);
                command.Parameters[1].Value = db_parameter.Created.ToOADate();

                command.Parameters.Add(structure.MainTable["numbe_prm"].Parameter);
                command.Parameters[2].Value = db_parameter.Numbe_Prm;

                command.Parameters.Add(structure.MainTable["tab_hist"].Parameter);
                command.Parameters[3].Value = db_parameter.tblHistory;

                command.Parameters.Add(structure.MainTable["tab_val"].Parameter);
                command.Parameters[4].Value = db_parameter.tblValues;

                command.Parameters.Add(structure.MainTable["guid"].Parameter);
                command.Parameters[5].Value = db_parameter.Identifier.ToString();

                command.CommandText = string.Format("Insert Into {0} Values (@id, @dtCreate, @numbe_prm, @tab_hist, @tab_val, @guid)", structure.MainTable.Name);
                if (command.ExecuteNonQuery() != 1)
                {
                    throw new Exception("Не удалось создать запись параметра в главной таблице БД.");
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
        }

        /// <summary>
        /// Создать таблицу описания параметра
        /// </summary>
        /// <param name="connection">Соединение с которым работать</param>
        /// <param name="transaction">Транзакция через которую осуществлять запись в БД</param>
        /// <param name="db_parameter">Параметр, который добавить в БД</param>
        protected void CreateParameterHistoryTable(SqlConnection connection, SqlTransaction transaction,
            DataBaseParameter db_parameter)
        {
            try
            {
                if (db_parameter.Descriptions.Count == 0)
                {
                    DataBaseDescription description = new DataBaseDescription();

                    description.dtCreate = db_parameter.Created;
                    description.ID = 0;

                    description.MainKey = db_parameter.ID;
                    description.NameParameter = string.Empty;

                    description.NumberParameter = db_parameter.Numbe_Prm;
                    description.TypeParameter = string.Empty;

                    description.NameParameter = "Параметр " + db_parameter.ID.ToString();
                    db_parameter.Descriptions.Add(description);
                }
                else
                    foreach (DataBaseDescription desc in db_parameter.Descriptions)
                    {
                        desc.NumberParameter = db_parameter.ID;
                        desc.MainKey = db_parameter.ID;

                        desc.dtCreate = db_parameter.Created;
                    }

                SqlCommand tblCommand = connection.CreateCommand();
                tblCommand.Transaction = transaction;

                tblCommand.CommandText = string.Format(structure.HistoryTable.SqlQueryForCreateTable, db_parameter.tblHistory);

                tblCommand.ExecuteNonQuery();
                foreach (DataBaseDescription desc in db_parameter.Descriptions)
                {
                    SqlCommand command = connection.CreateCommand();
                    command.Transaction = transaction;

                    command.Parameters.Add(structure.HistoryTable["id"].Parameter);
                    command.Parameters[0].Value = desc.ID;

                    command.Parameters.Add(structure.HistoryTable["dtCreate"].Parameter);
                    command.Parameters[1].Value = desc.dtCreate.ToOADate();

                    command.Parameters.Add(structure.HistoryTable["main_key"].Parameter);
                    command.Parameters[2].Value = desc.MainKey;

                    command.Parameters.Add(new SqlParameter("numbe_prm", desc.NumberParameter));
                    command.Parameters[3].SqlDbType = SqlDbType.Int;

                    command.Parameters.Add(structure.HistoryTable["name_prm"].Parameter);
                    command.Parameters[4].Value = desc.NameParameter;

                    command.Parameters.Add(structure.HistoryTable["type_prm"].Parameter);
                    command.Parameters[5].Value = desc.TypeParameter;

                    command.Parameters.Add(structure.HistoryTable["val_block_up"].Parameter);
                    command.Parameters[6].Value = desc.Val_block_up;

                    command.Parameters.Add(structure.HistoryTable["val_block_down"].Parameter);
                    command.Parameters[7].Value = desc.Val_block_down;

                    command.Parameters.Add(structure.HistoryTable["val_avar"].Parameter);
                    command.Parameters[8].Value = desc.Val_avar;

                    command.Parameters.Add(structure.HistoryTable["val_max"].Parameter);
                    command.Parameters[9].Value = desc.Val_max;

                    command.Parameters.Add(structure.HistoryTable["val_min"].Parameter);
                    command.Parameters[10].Value = desc.Val_min;

                    command.Parameters.Add(structure.HistoryTable["calibr_1"].Parameter);
                    command.Parameters[11].Value = desc.Calibr_1;

                    command.Parameters.Add(structure.HistoryTable["calibr_2"].Parameter);
                    command.Parameters[12].Value = desc.Calibr_2;

                    command.Parameters.Add(structure.HistoryTable["calibr_3"].Parameter);
                    command.Parameters[13].Value = desc.Calibr_3;

                    command.Parameters.Add(structure.HistoryTable["calibr_4"].Parameter);
                    command.Parameters[14].Value = desc.Calibr_4;

                    command.Parameters.Add(structure.HistoryTable["calibr_5"].Parameter);
                    command.Parameters[15].Value = desc.Calibr_5;

                    command.Parameters.Add(structure.HistoryTable["calibr_6"].Parameter);
                    command.Parameters[16].Value = desc.Calibr_6;

                    command.Parameters.Add(structure.HistoryTable["calibr_7"].Parameter);
                    command.Parameters[17].Value = desc.Calibr_7;

                    command.Parameters.Add(structure.HistoryTable["calibr_8"].Parameter);
                    command.Parameters[18].Value = desc.Calibr_8;

                    command.Parameters.Add(structure.HistoryTable["calibr_9"].Parameter);
                    command.Parameters[19].Value = desc.Calibr_9;

                    command.Parameters.Add(structure.HistoryTable["calibr_10"].Parameter);
                    command.Parameters[20].Value = desc.Calibr_10;

                    command.Parameters.Add(structure.HistoryTable["snd_avar"].Parameter);
                    command.Parameters[21].Value = desc.Snd_avar;

                    command.Parameters.Add(structure.HistoryTable["snd_max"].Parameter);
                    command.Parameters[22].Value = desc.Snd_max;

                    command.Parameters.Add(structure.HistoryTable["graf_switch"].Parameter);
                    command.Parameters[23].Value = desc.Graf_switch;

                    command.Parameters.Add(structure.HistoryTable["graf_diapz"].Parameter);
                    command.Parameters[24].Value = desc.Graf_diapz;

                    command.Parameters.Add(structure.HistoryTable["graf_min"].Parameter);
                    command.Parameters[25].Value = desc.Graf_min;

                    command.Parameters.Add(structure.HistoryTable["graf_max"].Parameter);
                    command.Parameters[26].Value = desc.Graf_max;

                    command.Parameters.Add(structure.HistoryTable["contr_par"].Parameter);
                    command.Parameters[27].Value = desc.Contr_par;

                    command.Parameters.Add(structure.HistoryTable["res_str"].Parameter);
                    command.Parameters[28].Value = desc.Res_str;

                    command.Parameters.Add(structure.HistoryTable["res_float1"].Parameter);
                    command.Parameters[29].Value = desc.Res_float1;

                    command.Parameters.Add(structure.HistoryTable["res_float2"].Parameter);
                    command.Parameters[30].Value = desc.Res_float2;

                    command.Parameters.Add(structure.HistoryTable["res_int1"].Parameter);
                    command.Parameters[31].Value = desc.Res_int1;

                    command.Parameters.Add(structure.HistoryTable["res_int2"].Parameter);
                    command.Parameters[32].Value = desc.Res_int2;

                    command.CommandText = string.Format("Insert Into dbo.{0} Values (@id, @dtCreate, @main_key, @numbe_prm, @name_prm, " +
                        "@type_prm, @val_block_up, @val_block_down, @val_avar, @val_max, @val_min, @calibr_1, @calibr_2, @calibr_3, @calibr_4, " +
                        "@calibr_5, @calibr_6, @calibr_7, @calibr_8, @calibr_9, @calibr_10, @snd_avar, @snd_max, @graf_switch, @graf_diapz, " +
                        "@graf_min, @graf_max, @contr_par, @res_str, @res_float1, @res_float2, @res_int1, @res_int2)", db_parameter.tblHistory);

                    if (command.ExecuteNonQuery() != 1)
                    {
                        throw new Exception("Не удалось добавить описание параметра в БД.");
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
        }

        /// <summary>
        /// Добавить свойства параметра. Свойство добавляется как есть, то есть поля не актуализируются
        /// поэтому создавать свойство лучше на основе имеющегося свойства настроенного!!!
        /// </summary>
        /// <param name="connection">Соединение с которым работать</param>
        /// <param name="transaction">Транзакция через которую осуществлять запись в БД</param>
        /// <param name="db_description">Свойство, которое добавить</param>
        /// <param name="table">Таблица, в которую добавить свойство</param>
        protected void UpdateParameterHistory(SqlConnection connection, SqlTransaction transaction,
            DataBaseDescription db_description, string table)
        {
            try
            {
                SqlCommand command = connection.CreateCommand();
                command.Transaction = transaction;

                db_description.ID = GetNextId(table, "id");
                db_description.dtCreate = DateTime.Now;

                command.Parameters.Add(structure.HistoryTable["id"].Parameter);
                command.Parameters[0].Value = db_description.ID;

                command.Parameters.Add(structure.HistoryTable["dtCreate"].Parameter);
                command.Parameters[1].Value = db_description.dtCreate.ToOADate();

                command.Parameters.Add(structure.HistoryTable["main_key"].Parameter);
                command.Parameters[2].Value = db_description.MainKey;

                command.Parameters.Add(structure.HistoryTable["numbe_prm"].Parameter);
                command.Parameters[3].Value = db_description.NumberParameter;

                command.Parameters.Add(structure.HistoryTable["name_prm"].Parameter);
                command.Parameters[4].Value = db_description.NameParameter;

                command.Parameters.Add(structure.HistoryTable["type_prm"].Parameter);
                command.Parameters[5].Value = db_description.TypeParameter;

                command.Parameters.Add(structure.HistoryTable["val_block_up"].Parameter);
                command.Parameters[6].Value = db_description.Val_block_up;

                command.Parameters.Add(structure.HistoryTable["val_block_down"].Parameter);
                command.Parameters[7].Value = db_description.Val_block_down;

                command.Parameters.Add(structure.HistoryTable["val_avar"].Parameter);
                command.Parameters[8].Value = db_description.Val_avar;

                command.Parameters.Add(structure.HistoryTable["val_max"].Parameter);
                command.Parameters[9].Value = db_description.Val_max;

                command.Parameters.Add(structure.HistoryTable["val_min"].Parameter);
                command.Parameters[10].Value = db_description.Val_min;

                command.Parameters.Add(structure.HistoryTable["calibr_1"].Parameter);
                command.Parameters[11].Value = db_description.Calibr_1;

                command.Parameters.Add(structure.HistoryTable["calibr_2"].Parameter);
                command.Parameters[12].Value = db_description.Calibr_2;

                command.Parameters.Add(structure.HistoryTable["calibr_3"].Parameter);
                command.Parameters[13].Value = db_description.Calibr_3;

                command.Parameters.Add(structure.HistoryTable["calibr_4"].Parameter);
                command.Parameters[14].Value = db_description.Calibr_4;

                command.Parameters.Add(structure.HistoryTable["calibr_5"].Parameter);
                command.Parameters[15].Value = db_description.Calibr_5;

                command.Parameters.Add(structure.HistoryTable["calibr_6"].Parameter);
                command.Parameters[16].Value = db_description.Calibr_6;

                command.Parameters.Add(structure.HistoryTable["calibr_7"].Parameter);
                command.Parameters[17].Value = db_description.Calibr_7;

                command.Parameters.Add(structure.HistoryTable["calibr_8"].Parameter);
                command.Parameters[18].Value = db_description.Calibr_8;

                command.Parameters.Add(structure.HistoryTable["calibr_9"].Parameter);
                command.Parameters[19].Value = db_description.Calibr_9;

                command.Parameters.Add(structure.HistoryTable["calibr_10"].Parameter);
                command.Parameters[20].Value = db_description.Calibr_10;

                command.Parameters.Add(structure.HistoryTable["snd_avar"].Parameter);
                command.Parameters[21].Value = db_description.Snd_avar;

                command.Parameters.Add(structure.HistoryTable["snd_max"].Parameter);
                command.Parameters[22].Value = db_description.Snd_max;

                command.Parameters.Add(structure.HistoryTable["graf_switch"].Parameter);
                command.Parameters[23].Value = db_description.Graf_switch;

                command.Parameters.Add(structure.HistoryTable["graf_diapz"].Parameter);
                command.Parameters[24].Value = db_description.Graf_diapz;

                command.Parameters.Add(structure.HistoryTable["graf_min"].Parameter);
                command.Parameters[25].Value = db_description.Graf_min;

                command.Parameters.Add(structure.HistoryTable["graf_max"].Parameter);
                command.Parameters[26].Value = db_description.Graf_max;

                command.Parameters.Add(structure.HistoryTable["contr_par"].Parameter);
                command.Parameters[27].Value = db_description.Contr_par;

                command.Parameters.Add(structure.HistoryTable["res_str"].Parameter);
                command.Parameters[28].Value = db_description.Res_str;

                command.Parameters.Add(structure.HistoryTable["res_float1"].Parameter);
                command.Parameters[29].Value = db_description.Res_float1;

                command.Parameters.Add(structure.HistoryTable["res_float2"].Parameter);
                command.Parameters[30].Value = db_description.Res_float2;

                command.Parameters.Add(structure.HistoryTable["res_int1"].Parameter);
                command.Parameters[31].Value = db_description.Res_int1;

                command.Parameters.Add(structure.HistoryTable["res_int2"].Parameter);
                command.Parameters[32].Value = db_description.Res_int2;

                command.CommandText = string.Format("Insert Into dbo.{0} Values (@id, @dtCreate, @main_key, @numbe_prm, @name_prm, " +
                    "@type_prm, @val_block_up, @val_block_down, @val_avar, @val_max, @val_min, @calibr_1, @calibr_2, @calibr_3, @calibr_4, " +
                    "@calibr_5, @calibr_6, @calibr_7, @calibr_8, @calibr_9, @calibr_10, @snd_avar, @snd_max, @graf_switch, @graf_diapz, " +
                    "@graf_min, @graf_max, @contr_par, @res_str, @res_float1, @res_float2, @res_int1, @res_int2)", table);

                if (command.ExecuteNonQuery() != 1)
                {
                    throw new Exception("Не удалось добавить описание параметра в БД.");
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
        }

        /// <summary>
        /// Создать таблицу значений параметра
        /// </summary>
        /// <param name="connection">Соединение с которым работать</param>
        /// <param name="transaction">Транзакция через которую осуществлять запись в БД</param>
        /// <param name="db_parameter">Параметр, который добавить в БД</param>
        protected void CreateParameterValuesTable(SqlConnection connection, SqlTransaction transaction,
            DataBaseParameter db_parameter)
        {
            try
            {
                SqlCommand command = connection.CreateCommand();
                command.Transaction = transaction;

                command.CommandText = string.Format(structure.ValuesTable.SqlQueryForCreateTable, db_parameter.tblValues);
                command.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
        }
    }
}