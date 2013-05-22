using System;

namespace DataBase
{
    /// <summary>
    /// Реализует передаче значения параметра на сохранение в БД
    /// </summary>
    public class DataBaseSaverAgent
    {
        // ---- данные класса ----

        private DataBaseSaver saver = null;             // реализует сохранение значения параметра

        /// <summary>
        /// Инициализирует новый экземпляр класса
        /// </summary>
        /// <param name="Saver">Объек выполняющий сохранение значения параметров</param>
        internal DataBaseSaverAgent(DataBaseSaver Saver)
        {
            saver = Saver;
        }

        /// <summary>
        /// Передать на сохранение значение параметра
        /// </summary>
        /// <param name="Identifier">Идентификатор параметра</param>
        /// <param name="Value">Значение параметра которое сохранить</param>
        /// <param name="Time">Время когда было получено данное значение параметра</param>
        public void Save(Guid Identifier, float Value, long Time)
        {
            try
            {
                saver.ToWrite(new DataBaseParameterValue(Identifier, Time, Value)); 
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
        }
    }
}