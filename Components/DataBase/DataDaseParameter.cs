using System;
using System.Collections.Generic;

namespace DataBase
{
    /// <summary>
    /// Реализует параметр, который храниться в БД
    /// </summary>
    public class DataBaseParameter
    {
        // ---- данные класса ----    

        private List<DataBaseDescription> descriptions;     // записи содержащие описание параметра

        private String t_history = String.Empty;            // название таблицы в которой храниться описание параметра
        private String t_values = String.Empty;             // название таблицы в которой храняться значения параметра

        private Guid guid;                                  // Глобальноуникальный идентификатор параметра (соответствует идентификатору параметра конвейера)
        private DateTime created;                           // время создания параметра в БД

        private int id = -1;                                // поле id параметра в главной таблице БД
        private int numbe_prm = -1;                         // поле numbe_prm  в главной таблице БД

        private string project;                             // поле project в главной таблице
        private string p_work;                              // поле p_work в главной таблице

        /// <summary>
        /// Инициализирует новый экземпляр класса
        /// </summary>
        public DataBaseParameter()
        {
            project = string.Empty;
            p_work = string.Empty;

            descriptions = new List<DataBaseDescription>();
        }

        /// <summary>
        /// Определяет  записи содержащие описание параметра
        /// </summary>
        public List<DataBaseDescription> Descriptions
        {
            get { return descriptions; }
        }

        /// <summary>
        /// Определяет  название таблицы в которой храниться описание параметра
        /// </summary>
        public string tblHistory
        {
            get { return t_history; }
            set { t_history = value; }
        }

        /// <summary>
        /// Определяет  название таблицы в которой храняться значения параметра
        /// </summary>
        public string tblValues
        {
            get { return t_values; }
            set { t_values = value; }
        }

        /// <summary>
        /// Определяет глобально уникальный идентификатор параметра (соответствует идентификатору параметра конвейера)
        /// </summary>
        public Guid Identifier
        {
            get { return guid; }
            set
            {
                guid = new Guid(value.ToByteArray());
            }
        }

        /// <summary>
        /// Определяет время создания параметра в БД
        /// </summary>
        public DateTime Created
        {
            get { return created; }
            set { created = value; }
        }

        /// <summary>
        /// Определяет поле id параметра в главной таблице БД.
        /// Не идентификатор параметра.
        /// </summary>
        public int ID
        {
            get { return id; }
            set { id = value; }
        }

        /// <summary>
        /// Определяет поле numbe_prm  в главной таблице БД.
        /// Два параметра не должны иметь равные значения данного поля (необходимо для совместимости).
        /// </summary>
        public int Numbe_Prm
        {
            get { return numbe_prm; }
            set { numbe_prm = value; }
        }

        /// <summary>
        /// Определяет название проекта к которому относится данный параметр
        /// </summary>
        public string Project
        {
            get { return project; }
            set { project = value; }
        }

        /// <summary>
        /// Определяет работу к которой относится данный параметр
        /// </summary>
        public string P_work
        {
            get { return p_work; }
            set { p_work = value; }
        }

        /// <summary>
        /// Текстовое представление параметра БД.
        /// Отображается имя параметра.
        /// </summary>
        /// <returns>Имя параметра</returns>
        public override string ToString()
        {
            if (descriptions != null)
            {
                if (descriptions.Count > 0)
                {
                    return descriptions[0].NameParameter;
                }
            }
            return "Имя параметра не определено";
        }
    }

    /// <summary>
    /// Реализует настройки параметра из БД
    /// </summary>
    public class DataBaseDescription
    {
        // ---- данные класса ----

        private int id;                                     // id строки таблицы

        private DateTime dt_create;                         // время создания записи
        private int main_key;                               // ссылка на таблицу t_main. t_main.id == main_key
 
        private int numbe_prm;                              // номер параметра. соответствует номеру параметра в КРС (не используется, но должен иметь корректное значение)
        private string name_prm;                            // текстовое описание параметра

        private string type_prm;                            // текстовое описание единиц измерения параметра
        private float val_block_up = 0.0f;                         // верхнее блокировочное значение

        private float val_block_down = 0.0f;                       // нижнее блокировочное значение
        private float val_avar = 0.0f;                             // аварийное значение

        private float val_max = 0.0f;                              // максимальное значение параметра
        private float val_min = 0.0f;                              // минимальное значение параметра

        private float calibr_1;                             // первое калибровочное значение (не используется)
        private float calibr_2;                             // второе калибровочное значение (не используется)
        private float calibr_3;                             // третье калибровочное значение (не используется)
        private float calibr_4;                             // четвертое калибровочное значение (не используется)
        private float calibr_5;                             // пятое калибровочное значение (не используется)
        private float calibr_6;                             // шестое калибровочное значение (не используется)
        private float calibr_7;                             // седьмое калибровочное значение (не используется)
        private float calibr_8;                             // восьмое калибровочное значение (не используется)
        private float calibr_9;                             // девятое калибровочное значение (не используется)
        private float calibr_10;                            // десятое калибровочное значение (не используется)

        private string snd_avar;                            // имя файла, который проигрывается при превышении аварийного значения
        private string snd_max;                             // имя файла, который проигрывается при превышении максимального значения

        private int graf_switch;                            // не известно (не используется)
        private float graf_diapz;                           // не известно (не используется)

        private float graf_min;                             // не известно (не используется)
        private float graf_max;                             // не известно (не используется)

        private int contr_par;                              // не известно (не используется)
        private string res_str;                             // резервная строка для расширения функций

        private float res_float1;                           // первая резервная переменная для расширения функциональности, может быть использованна произвольным образом
        private float res_float2;                           // вторая резервная переменная для расширения функциональности, может быть использованна произвольным образом

        private int res_int1;                               // первая резервная переменная для расширения функциональности, может быть использованна произвольным образом
        private int res_int2;                               // вторая резервная переменная для расширения функциональности, может быть использованна произвольным образом

        /// <summary>
        /// Инициализирует новый экземпляр класса
        /// </summary>
        public DataBaseDescription()
        {
            snd_avar = string.Empty;
            snd_max = string.Empty;

            name_prm = string.Empty;
            type_prm = string.Empty;

            res_str = string.Empty;

            val_block_up = 0.0f;

            val_block_down = 0.0f;
            val_avar = 0.0f;

            val_max = 0.0f;
            val_min = 0.0f;
        }

        /// <summary>
        /// Определяет id строки
        /// </summary>
        public int ID
        {
            get { return id; }
            set { id = value; }
        }

        /// <summary>
        /// Определяет время создания записи
        /// </summary>
        public DateTime dtCreate
        {
            get { return dt_create; }
            set { dt_create = value; }
        }

        /// <summary>
        /// Определяет ссылку на таблицу t_main. t_main.id == main_key
        /// </summary>
        public int MainKey
        {
            get { return main_key; }
            set { main_key = value; }
        }
        
        /// <summary>
        /// Определяет номер параметра. соответствует номеру параметра в КРС (не используется, но должен иметь корректное значение)
        /// </summary>
        public int NumberParameter
        {
            get { return numbe_prm; }
            set { numbe_prm = value; }
        }

        /// <summary>
        /// Определяет текстовое описание параметра
        /// </summary>
        public string NameParameter
        {
            get { return name_prm; }
            set { name_prm = value; }
        }

        /// <summary>
        /// Определяет текстовое описание единиц измерения параметра
        /// </summary>
        public string TypeParameter
        {
            get { return type_prm; }
            set { type_prm = value; }
        }

        /// <summary>
        /// Определяет верхнее блокировочное значение
        /// </summary>
        public float Val_block_up
        {
            get { return val_block_up; }
            set { val_block_up = value; }
        }

        /// <summary>
        /// Определяет нижнее блокировочное значение
        /// </summary>
        public float Val_block_down
        {
            get { return val_block_down; }
            set { val_block_down = value; }
        }

        /// <summary>
        /// Определяет аварийное значение
        /// </summary>
        public float Val_avar
        {
            get { return val_avar; }
            set { val_avar = value; }
        }

        /// <summary>
        /// Определяет максимальное значение параметра
        /// </summary>
        public float Val_max
        {
            get { return val_max; }
            set { val_max = value; }
        }

        /// <summary>
        /// Определяет минимальное значение параметра
        /// </summary>
        public float Val_min
        {
            get { return val_min; }
            set { val_min = value; }
        }

        /// <summary>
        /// Определяет первое калибровочное значение (не используется)
        /// </summary>
        public float Calibr_1
        {
            get { return calibr_1; }
            set { calibr_1 = value; }
        }

        /// <summary>
        /// Определяет второе калибровочное значение (не используется)
        /// </summary>
        public float Calibr_2
        {
            get { return calibr_2; }
            set { calibr_2 = value; }
        }

        /// <summary>
        /// Определяет третье калибровочное значение (не используется)
        /// </summary>
        public float Calibr_3
        {
            get { return calibr_3; }
            set { calibr_3 = value; }
        }

        /// <summary>
        /// Определяет четвертое калибровочное значение (не используется)
        /// </summary>
        public float Calibr_4
        {
            get { return calibr_4; }
            set { calibr_4 = value; }
        }

        /// <summary>
        /// Определяет пятое калибровочное значение (не используется)
        /// </summary>
        public float Calibr_5
        {
            get { return calibr_5; }
            set { calibr_5 = value; }
        }

        /// <summary>
        /// Определяет шестое калибровочное значение (не используется)
        /// </summary>
        public float Calibr_6
        {
            get { return calibr_6; }
            set { calibr_6 = value; }
        }

        /// <summary>
        /// Определяет седьмое калибровочное значение (не используется)
        /// </summary>
        public float Calibr_7
        {
            get { return calibr_7; }
            set { calibr_7 = value; }
        }

        /// <summary>
        /// Определяет восьмое калибровочное значение (не используется)
        /// </summary>
        public float Calibr_8
        {
            get { return calibr_8; }
            set { calibr_8 = value; }
        }

        /// <summary>
        /// Определяет девятое калибровочное значение (не используется)
        /// </summary>
        public float Calibr_9
        {
            get { return calibr_9; }
            set { calibr_9 = value; }
        }

        /// <summary>
        /// Определяет десятое калибровочное значение (не используется)
        /// </summary>
        public float Calibr_10
        {
            get { return calibr_10; }
            set { calibr_10 = value; }
        }

        /// <summary>
        /// Определяет имя файла, который проигрывается при превышении аварийного значения
        /// </summary>
        public string Snd_avar
        {
            get { return snd_avar; }
            set { snd_avar = value; }
        }

        /// <summary>
        /// Определяет имя файла, который проигрывается при превышении максимального значения
        /// </summary>
        public string Snd_max
        {
            get { return snd_max; }
            set { snd_max = value; }
        }

        /// <summary>
        /// Не известно (не используется)
        /// </summary>
        public int Graf_switch
        {
            get { return graf_switch; }
            set { graf_switch = value; }
        }

        /// <summary>
        /// 
        /// </summary>
        public float Graf_diapz
        {
            get { return graf_diapz; }
            set { graf_diapz = value; }
        }

        /// <summary>
        /// Не известно (не используется)
        /// </summary>
        public float Graf_min
        {
            get { return graf_min; }
            set { graf_min = value; }
        }

        /// <summary>
        /// Не известно (не используется)
        /// </summary>
        public float Graf_max
        {
            get { return graf_max; }
            set { graf_max = value; }
        }

        /// <summary>
        /// Не известно (не используется)
        /// </summary>
        public int Contr_par
        {
            get { return contr_par; }
            set { contr_par = value; }
        }

        /// <summary>
        /// Резервная строка для расширения функций
        /// </summary>
        public string Res_str
        {
            get { return res_str; }
            set { res_str = value; }
        }

        /// <summary>
        /// Первая резервная переменная для расширения функциональности, может быть использованна произвольным образом
        /// </summary>
        public float Res_float1
        {
            get { return res_float1; }
            set { res_float1 = value; }
        }

        /// <summary>
        /// Вторая резервная переменная для расширения функциональности, может быть использованна произвольным образом
        /// </summary>
        public float Res_float2
        {
            get { return res_float2; }
            set { res_float2 = value; }
        }

        /// <summary>
        /// Первая резервная переменная для расширения функциональности, может быть использованна произвольным образом
        /// </summary>
        public int Res_int1
        {
            get { return res_int1; }
            set { res_int1 = value; }
        }

        /// <summary>
        /// Вторая резервная переменная для расширения функциональности, может быть использованна произвольным образом
        /// </summary>
        public int Res_int2
        {
            get { return res_int2; }
            set { res_int2 = value; }
        }

        /// <summary>
        /// Получить копию объекта
        /// </summary>
        /// <returns></returns>
        public DataBaseDescription Clone()
        {
            DataBaseDescription description = new DataBaseDescription();

            description.id = id;                    

            description.dt_create = dt_create;
            description.main_key = main_key;      

            description.numbe_prm = numbe_prm;
            description.name_prm = name_prm;

            description.type_prm = type_prm;
            description.val_block_up = val_block_up;  

            description.val_block_down = val_block_down;
            description.val_avar = val_avar;

            description.val_max = val_max; 
            description.val_min = val_min; 

            description.calibr_1 = calibr_1;
            description.calibr_2 = calibr_2;
            description.calibr_3 = calibr_3;
            description.calibr_4 = calibr_4;   
            description.calibr_5 = calibr_5;   
            description.calibr_6 = calibr_6;   
            description.calibr_7 = calibr_7;   
            description.calibr_8 = calibr_8;   
            description.calibr_9 = calibr_9;   
            description.calibr_10 = calibr_10;  

            description.snd_avar = snd_avar;   
            description.snd_max = snd_max;    

            description.graf_switch = graf_switch;
            description.graf_diapz = graf_diapz;

            description.graf_min = graf_min;  
            description.graf_max = graf_max;  

            description.contr_par = contr_par; 
            description.res_str = res_str;   

            description.res_float1 = res_float1;
            description.res_float2 = res_float2;

            description.res_int1 = res_int1;
            description.res_int2 = res_int2;

            return description;
        }

        public override string ToString()
        {
            return name_prm;
        }
    }
}