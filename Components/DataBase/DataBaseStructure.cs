using System;
using System.Data;
using System.Data.SqlTypes;
using System.Data.SqlClient;
using System.Collections.Generic;

namespace DataBase
{
    /// <summary>
    /// Реализует логическую структуру БД
    /// </summary>
    public class DataBaseStructure
    {
        // ---- данные класса ----

        private DataBaseTable mainTable;                 // главная таблица в БД
        private DataBaseTable timeTable;                 // таблица, содержащая время

        private DataBaseTable opListTable;               // таблица маркеров
        private DataBaseTable parametersTable;           // таблица служебных параметров

        private DataBaseTable markerTable;               // таблица маркеров

        private DataBaseTable historyTable;              // таблица описания параметра
        private DataBaseTable valuesTable;               // таблица значений параметра

        /// <summary>
        /// Инициализирует новый экземпляр класса
        /// </summary>
        public DataBaseStructure()
        {
            mainTable = new DataBaseTable();
            timeTable = new DataBaseTable();

            parametersTable = new DataBaseTable();
            historyTable = new DataBaseTable();

            valuesTable = new DataBaseTable();
            opListTable = new DataBaseTable();

            markerTable = new DataBaseTable();

            InitializeTables();
        }

        /// <summary>
        /// Возвращяет главную таблицу.
        /// Только для чтения.
        /// </summary>
        public DataBaseTable MainTable
        {
            get { return mainTable; }
        }

        /// <summary>
        /// Возвращяет таблицу времени.
        /// Только для чтения.
        /// </summary>
        public DataBaseTable TimeTable
        {
            get { return timeTable; }
        }

        /// <summary>
        /// Возвращяет таблицу служебных параметров.
        /// Только для чтения.
        /// </summary>
        public DataBaseTable ParametersTable
        {
            get { return parametersTable; }
        }

        /// <summary>
        /// Возвращяет таблицу настроек параметра.
        /// Тодбко для чтения.
        /// </summary>
        public DataBaseTable HistoryTable
        {
            get { return historyTable; }
        }

        /// <summary>
        /// Возвращяет таблицу значений параметра.
        /// Только для чтения.
        /// </summary>
        public DataBaseTable ValuesTable
        {
            get { return valuesTable; }
        }

        /// <summary>
        /// Возвращяет таблицу операций
        /// </summary>
        public DataBaseTable OpListTable
        {
            get { return opListTable; }
        }

        /// <summary>
        /// Возвращяет таблицу маркеров
        /// </summary>
        public DataBaseTable MarkerTable
        {
            get { return markerTable; }
        }

        /// <summary>
        /// Инициализировать таблицы
        /// </summary>
        protected void InitializeTables()
        {
            InitializeMainTable();
            InitializeTimeTable();

            InitializeParametersTable();

            InitializeHistoryTable();
            InitializeValuesTable();

            InitializeOpListTable();
            InitializeMarkerList();
        }

        /// <summary>
        /// Инициализировать главную таблицу
        /// </summary>
        protected void InitializeMainTable()
        {
            DataBaseTableField id_main = new DataBaseTableField("id", SqlDbType.Int, 0);
            DataBaseTableField dtCreate = new DataBaseTableField("dtCreate", SqlDbType.Float, 1);

            DataBaseTableField numbe_prm = new DataBaseTableField("numbe_prm", SqlDbType.Int, 2);
            DataBaseTableField tab_hist = new DataBaseTableField("tab_hist", SqlDbType.VarChar, 3);

            DataBaseTableField tab_val = new DataBaseTableField("tab_val", SqlDbType.VarChar, 4);
            DataBaseTableField guid = new DataBaseTableField("guid", SqlDbType.VarChar, 5);

            mainTable.InsertField(id_main);
            mainTable.InsertField(dtCreate);

            mainTable.InsertField(numbe_prm);
            mainTable.InsertField(tab_hist);

            mainTable.InsertField(tab_val);
            mainTable.InsertField(guid);

            mainTable.Name = "dbo.t_main";

            mainTable.SqlQueryForCreateTable = "CREATE TABLE dbo.t_main(id int PRIMARY KEY NOT NULL, dtCreate float NULL, numbe_prm int NULL," +
                                                         "tab_hist varchar(63) NULL, tab_val varchar(63) NULL, guid varchar(128) NOT NULL)";

            mainTable.SqlQueryForSelectAll = string.Format("Select * From {0}", mainTable.Name); 

        }

        /// <summary>
        /// Инициализировать таблицу времени
        /// </summary>
        protected void InitializeTimeTable()
        {
            DataBaseTableField id = new DataBaseTableField("id", SqlDbType.Int, 0);
            DataBaseTableField val_Time = new DataBaseTableField("val_Time", SqlDbType.Float, 1);

            DataBaseTableField val_depth = new DataBaseTableField("val_depth", SqlDbType.Real, 2);

            timeTable.InsertField(id);
            timeTable.InsertField(val_Time);


            timeTable.InsertField(val_depth);
            timeTable.Name = "dbo.t_measuring";

            timeTable.SqlQueryForCreateTable = "CREATE TABLE dbo.t_measuring(id int Primary Key NOT NULL, val_Time float NULL, val_depth real NULL)";
            timeTable.SqlQueryForSelectAll = string.Format("Select * From {0}", timeTable.Name); 
        }

        /// <summary>
        /// Инициализировать таблицу служебных параметров
        /// </summary>
        protected void InitializeParametersTable()
        {
            DataBaseTableField id = new DataBaseTableField("id", SqlDbType.Int, 0);
            DataBaseTableField id_param = new DataBaseTableField("id_param", SqlDbType.Int, 1);

            DataBaseTableField val_param = new DataBaseTableField("val_param", SqlDbType.VarChar, 2);

            parametersTable.InsertField(id);
            parametersTable.InsertField(id_param);

            parametersTable.InsertField(val_param);
            parametersTable.Name = "dbo.t_Param";

            parametersTable.SqlQueryForCreateTable = "CREATE TABLE dbo.t_Param(id int primary key NOT NULL, id_param int NULL, val_param varchar(128) NULL)";
            parametersTable.SqlQueryForSelectAll = string.Format("Select * From dbo.{0}", parametersTable.Name);
        }

        protected void InitializeHistoryTable()
        {
            DataBaseTableField id = new DataBaseTableField("id", SqlDbType.Int, 0);
            DataBaseTableField dtCreate = new DataBaseTableField("dtCreate", SqlDbType.Float, 1);

            DataBaseTableField main_key = new DataBaseTableField("main_key", SqlDbType.Int, 2);
            DataBaseTableField numbe_prm = new DataBaseTableField("numbe_prm", SqlDbType.Int, 3);

            DataBaseTableField name_prm = new DataBaseTableField("name_prm", SqlDbType.VarChar, 4);
            DataBaseTableField type_prm = new DataBaseTableField("type_prm", SqlDbType.VarChar, 5);

            DataBaseTableField val_block_up = new DataBaseTableField("val_block_up", SqlDbType.Real, 6);
            DataBaseTableField val_block_down = new DataBaseTableField("val_block_down", SqlDbType.Real, 7);

            DataBaseTableField val_avar = new DataBaseTableField("val_avar", SqlDbType.Real, 8);
            DataBaseTableField val_max = new DataBaseTableField("val_max", SqlDbType.Real, 9);

            DataBaseTableField val_min = new DataBaseTableField("val_min", SqlDbType.Real, 10);

            DataBaseTableField calibr_1 = new DataBaseTableField("calibr_1", SqlDbType.Real, 11);
            DataBaseTableField calibr_2 = new DataBaseTableField("calibr_2", SqlDbType.Real, 12);
            DataBaseTableField calibr_3 = new DataBaseTableField("calibr_3", SqlDbType.Real, 13);
            DataBaseTableField calibr_4 = new DataBaseTableField("calibr_4", SqlDbType.Real, 14);
            DataBaseTableField calibr_5 = new DataBaseTableField("calibr_5", SqlDbType.Real, 15);
            DataBaseTableField calibr_6 = new DataBaseTableField("calibr_6", SqlDbType.Real, 16);
            DataBaseTableField calibr_7 = new DataBaseTableField("calibr_7", SqlDbType.Real, 17);
            DataBaseTableField calibr_8 = new DataBaseTableField("calibr_8", SqlDbType.Real, 18);
            DataBaseTableField calibr_9 = new DataBaseTableField("calibr_9", SqlDbType.Real, 19);
            DataBaseTableField calibr_10 = new DataBaseTableField("calibr_10", SqlDbType.Real, 20);

            DataBaseTableField snd_avar = new DataBaseTableField("snd_avar", SqlDbType.VarChar, 21);
            DataBaseTableField snd_max = new DataBaseTableField("snd_max", SqlDbType.VarChar, 22);

            DataBaseTableField graf_switch = new DataBaseTableField("graf_switch", SqlDbType.Int, 23);
            DataBaseTableField graf_diapz = new DataBaseTableField("graf_diapz", SqlDbType.Real, 24);

            DataBaseTableField graf_min = new DataBaseTableField("graf_min", SqlDbType.Real, 25);
            DataBaseTableField graf_max = new DataBaseTableField("graf_max", SqlDbType.Real, 26);

            DataBaseTableField contr_par = new DataBaseTableField("contr_par", SqlDbType.Int, 27);
            DataBaseTableField res_str = new DataBaseTableField("res_str", SqlDbType.VarChar, 28);

            DataBaseTableField res_float1 = new DataBaseTableField("res_float1", SqlDbType.Real, 29);
            DataBaseTableField res_float2 = new DataBaseTableField("res_float2", SqlDbType.Real, 30);

            DataBaseTableField res_int1 = new DataBaseTableField("res_int1", SqlDbType.Int, 31);
            DataBaseTableField res_int2 = new DataBaseTableField("res_int2", SqlDbType.Int, 32);

            historyTable.InsertField(id);
            historyTable.InsertField(dtCreate);

            historyTable.InsertField(main_key);
            historyTable.InsertField(numbe_prm);

            historyTable.InsertField(name_prm);
            historyTable.InsertField(type_prm);

            historyTable.InsertField(val_block_up);
            historyTable.InsertField(val_block_down);

            historyTable.InsertField(val_avar);
            historyTable.InsertField(val_max);

            historyTable.InsertField(val_min);

            historyTable.InsertField(calibr_1);
            historyTable.InsertField(calibr_2);
            historyTable.InsertField(calibr_3);
            historyTable.InsertField(calibr_4);
            historyTable.InsertField(calibr_5);
            historyTable.InsertField(calibr_6);
            historyTable.InsertField(calibr_7);
            historyTable.InsertField(calibr_8);
            historyTable.InsertField(calibr_9);
            historyTable.InsertField(calibr_10);

            historyTable.InsertField(snd_avar);
            historyTable.InsertField(snd_max);

            historyTable.InsertField(graf_switch);
            historyTable.InsertField(graf_diapz);

            historyTable.InsertField(graf_min);
            historyTable.InsertField(graf_max);

            historyTable.InsertField(contr_par);
            historyTable.InsertField(res_str);

            historyTable.InsertField(res_float1);
            historyTable.InsertField(res_float2);

            historyTable.InsertField(res_int1);
            historyTable.InsertField(res_int2);

            historyTable.Name = "dbo.History_{0}";

            historyTable.SqlQueryForCreateTable = "Create Table dbo.{0} (id int primary key not null, dtCreate float, main_key int, " +
                    "numbe_prm int, name_prm varchar(255), type_prm varchar(31), val_block_up real, val_block_down real, " +
                    "val_avar real, val_max real, val_min real, calibr_1 real,calibr_2 real,calibr_3 real,calibr_4 real,calibr_5 real, " +
                    "calibr_6 real,calibr_7 real, calibr_8 real,calibr_9 real,calibr_10 real,snd_avar varchar(255), snd_max varchar(255), " +
                    "graf_switch int, graf_diapz real, graf_min real, graf_max real, contr_par int, res_str varchar(255), res_float1 real, " +
                    "res_float2 real, res_int1 int, res_int2 int)";

            historyTable.SqlQueryForSelectAll = "Select * From {0}";
        }

        /// <summary>
        /// Инициализировать таблицу значений параметра
        /// </summary>
        protected void InitializeValuesTable()
        {
            DataBaseTableField id = new DataBaseTableField("id", SqlDbType.Int, 0);
            DataBaseTableField val_prm = new DataBaseTableField("val_prm", SqlDbType.Real, 1);

            valuesTable.InsertField(id);
            valuesTable.InsertField(val_prm);

            valuesTable.Name = "dbo.Values_{0}";

            valuesTable.SqlQueryForCreateTable = "Create Table {0} (id int primary key not null, val_prm real NULL)";
            valuesTable.SqlQueryForSelectAll = "Select * From {0}";
        }

        /// <summary>
        /// Инициализирует таблицу маркеров
        /// </summary>
        protected void InitializeOpListTable()
        {
            DataBaseTableField id = new DataBaseTableField("id", SqlDbType.Int, 0);
            DataBaseTableField dtCreate = new DataBaseTableField("dtCreate", SqlDbType.Float, 1);

            DataBaseTableField number_opr = new DataBaseTableField("number_opr", SqlDbType.Int, 2);
            DataBaseTableField descript = new DataBaseTableField("descript", SqlDbType.VarChar, 3);

            DataBaseTableField reserve = new DataBaseTableField("reserve", SqlDbType.VarChar, 4);

            opListTable.InsertField(id);
            opListTable.InsertField(dtCreate);

            opListTable.InsertField(number_opr);
            opListTable.InsertField(descript);

            opListTable.InsertField(reserve);
            opListTable.Name = "dbo.t_OpList";

            opListTable.SqlQueryForCreateTable = "CREATE TABLE dbo.t_OpList(id int Primary Key NOT NULL, dtCreate float NULL," +
                                                            "number_opr int NULL, descript varchar(128) NULL, reserve varchar(128) NULL)";
        }

        /// <summary>
        /// Инициализирует таблицу маркеров
        /// </summary>
        protected void InitializeMarkerList()
        {
            DataBaseTableField id = new DataBaseTableField("id", SqlDbType.Int, 0);
            DataBaseTableField dtCreate = new DataBaseTableField("dtCreate", SqlDbType.Float, 1);

            DataBaseTableField id_opr = new DataBaseTableField("id_opr", SqlDbType.Int, 2);
            DataBaseTableField id_meass = new DataBaseTableField("id_meass", SqlDbType.Int, 3);

            DataBaseTableField dop_desc = new DataBaseTableField("dop_desc", SqlDbType.VarChar, 4);
            DataBaseTableField reserve = new DataBaseTableField("reserve", SqlDbType.VarChar, 5);

            markerTable.InsertField(id);
            markerTable.InsertField(dtCreate);

            markerTable.InsertField(id_opr);
            markerTable.InsertField(id_meass);

            markerTable.InsertField(dop_desc);
            markerTable.InsertField(reserve);

            markerTable.Name = "dbo.t_Marker";
            markerTable.SqlQueryForCreateTable = "CREATE TABLE dbo.t_Marker(id int Primary Key NOT NULL, dtCreate float NULL, id_opr int NULL," +
                                                           "id_meass int NULL, dop_desc varchar(128) NULL, reserve varchar(128) NULL)";
        }
    }

    /// <summary>
    /// Реализует таблицу БД
    /// </summary>
    public class DataBaseTable
    {
        // ---- данные класса ----

        private string name;                        // имя таблицы
        private List<DataBaseTableField> fields;         // поля таблицы

        private string sql_query_for_create;
        private string sql_query_for_select_all;

        /// <summary>
        /// Инициализирует новый экземпляр класса
        /// </summary>
        public DataBaseTable()
        {
            name = string.Empty;
            fields = new List<DataBaseTableField>();
        }

        /// <summary>
        /// Возвращяет шаблон из которого можно получить имя таблицы
        /// </summary>
        public string Name
        {
            get { return name; }
            set { name = value; }
        }

        /// <summary>
        /// Получить поле таблицы по его индексу
        /// </summary>
        /// <param name="index">Индекс поля, отсчитывая с нуля</param>
        /// <returns>Поле таблицы</returns>
        public DataBaseTableField this[int index]
        {
            get
            {
                if (index > -1 && index < fields.Count)
                {
                    return fields[index];
                }
                else
                    return null;
            }
        }

        /// <summary>
        /// Получить поле таблицы по его имени
        /// </summary>
        /// <param name="name">Имя поля таблицы</param>
        /// <returns>Поле таблицы</returns>
        public DataBaseTableField this[string name]
        {
            get
            {
                foreach (var field in fields)
                {
                    if (field.Name == name)
                    {
                        return field;
                    }
                }
                return null;
            }
        }

        /// <summary>
        /// Возвращяет Sql запрос на создание таблицы
        /// </summary>
        public string SqlQueryForCreateTable
        {
            get { return sql_query_for_create; }
            set { sql_query_for_create = value; }
        }

        /// <summary>
        /// Возвращяет Sql запрос на выборку всех строк таблицы
        /// </summary>
        public string SqlQueryForSelectAll
        {
            get { return sql_query_for_select_all; }
            set { sql_query_for_select_all = value; }
        }

        /// <summary>
        /// Добавить поле в таблицу
        /// </summary>
        /// <param name="Field">Добавляемое поле</param>
        public void InsertField(DataBaseTableField Field)
        {
            foreach (var field in fields)
            {
                if (field.Name == Field.Name)
                {
                    throw new ArgumentException("Не допустимое поле. Поле с таким именем уже существует.");
                }
            }

            fields.Add(Field);
        }
    }

    /// <summary>
    /// Реализует поле таблицы БД
    /// </summary>
    public class DataBaseTableField
    {
        // ---- данные класса ----
                
        private string f_name;              // имя поля
        private SqlDbType f_type;           // тип поля

        private int indexInTable = -1;      // индекс в таблице

        /// <summary>
        /// Инициализирует новый экземпляр класса
        /// </summary>
        /// <param name="fieldName">Имя поля</param>
        /// <param name="fieldType">Тип поля</param>
        public DataBaseTableField(string fieldName, SqlDbType fieldType, int index)
        {
            f_name = fieldName;
            f_type = fieldType;

            indexInTable = index;
        }

        /// <summary>
        /// Возвращяет имя поля
        /// </summary>
        public string Name
        {
            get { return f_name; }
        }

        /// <summary>
        /// Возвращяет тип поля
        /// </summary>
        public SqlDbType Type
        {
            get { return f_type; }
        }

        /// <summary>
        /// Возвращяет индекс в таблице.
        /// </summary>
        public int IndexInTable
        {
            get { return indexInTable; }
        }

        /// <summary>
        /// Возвращяет параметр для параметризованного запроса
        /// </summary>
        public SqlParameter Parameter
        {
            get
            {
                SqlParameter parameter = new SqlParameter(f_name, f_type);
                return parameter;
            }
        }
    }
}