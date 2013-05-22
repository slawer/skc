using System;
using System.Xml;
using System.Threading;
using System.Collections.Generic;

namespace SKC
{
    /// <summary>
    /// реализует технологию цементирования
    /// </summary>
    public class Tech
    {
        private TechParameter tech_consumption;         // расход
        private TechParameter tech_volume;              // объем

        private TechParameter tech_density;             //плотность
        private TechParameter tech_pressure;            //давление

        private TechParameter tech_temperature;         //температура
        private TechParameter tech_proccessVolume;      // объем процесса

        protected TechStages stages = null;             // этапы работы
        protected TechMode mode = TechMode.Technology;  // режим в котором работает технология

        protected RgrList rgrs;                         // список расходомеров

        /// <summary>
        /// Возникает когда вычисленны технологические параметры
        /// </summary>
        public event EventHandler onCalculate;

        /// <summary>
        /// Возникает когда наладчик джоп, нууу очень сильно джооооопанутый
        /// </summary>
        public event EventHandler onJop;

        /// <summary>
        /// Инициализирует новый экземпляр класса
        /// </summary>
        public Tech()
        {
            stages = new TechStages(this);

            tech_volume = new TechParameter();
            tech_volume.Format = "{0:F2}";

            tech_consumption = new TechParameter();
            tech_consumption.Format = "{0:F1}";

            tech_density = new TechParameter();
            tech_density.Format = "{0:F3}";

            tech_pressure = new TechParameter();
            tech_pressure.Format = "{0:F1}";

            tech_temperature = new TechParameter();
            tech_temperature.Format = "{0:F3}";

            tech_proccessVolume = new TechParameter();

            tech_volume.gType = "ADDDFF1B-92CE-42C4-BB4B-E50B8382A531";
            tech_consumption.gType = "6919E0F5-31FE-4386-9328-54C0E76D198C";

            tech_density.gType = "D20E521B-A0FD-4EBB-BD49-AE8E706F50EC";
            tech_pressure.gType = "999C94B9-DA92-4010-ACBF-EB885D9BDC7C";

            tech_temperature.gType = "D5A1B201-81C9-4702-A78F-FE35C5C72B3A";
            tech_proccessVolume.gType = "574E8874-42A3-4262-A251-523399E2EA79";

            rgrs = new RgrList();
            for (int i = 0; i < 8; i++)
            {
                rgrs.Add(new Rgr());
            }
        }

        /// <summary>
        /// Список расходомеров
        /// </summary>
        public RgrList Rgrs
        {
            get { return rgrs; }
        }

        /// <summary>
        /// Данные были обновленны.
        /// Данная процедура не синхронизованна!
        /// </summary>
        public void Updated(Parameter[] parameters)
        {
            switch (mode)
            {
                case TechMode.Technology:

                    BackingUpDataTech(parameters);
                    break;

                case TechMode.Default:

                    BackingUpData(parameters);
                    break;

                default:
                    break;
            }
        
        }

        /// <summary>
        /// Выполнить простое копирование данных
        /// </summary>
        private void BackingUpData(Parameter[] parameters)
        {
            if (parameters != null)
            {
                // ----------- копируем значения каналов в технологическую примочку ---------------

                if (tech_consumption.Index > 0 && tech_consumption.Index < parameters.Length)
                {
                    float Val = parameters[tech_consumption.Index].CurrentValue;
                    if (!float.IsNaN(Val) && !float.IsInfinity(Val) &&
                            !float.IsNegativeInfinity(Val) && !float.IsPositiveInfinity(Val))
                    {
                        tech_consumption.Value = Val;
                    }
                }

                if (tech_volume.Index > 0 && tech_volume.Index < parameters.Length)
                {
                    float Val = parameters[tech_volume.Index].CurrentValue;
                    if (!float.IsNaN(Val) && !float.IsInfinity(Val) &&
                            !float.IsNegativeInfinity(Val) && !float.IsPositiveInfinity(Val))
                    {
                        tech_volume.Value = Val;
                    }
                }

                if (tech_density.Index > 0 && tech_density.Index < parameters.Length)
                {
                    float Val = parameters[tech_density.Index].CurrentValue;
                    if (!float.IsNaN(Val) && !float.IsInfinity(Val) &&
                            !float.IsNegativeInfinity(Val) && !float.IsPositiveInfinity(Val))
                    {
                        tech_density.Value = Val;
                    }
                }

                if (tech_pressure.Index > 0 && tech_pressure.Index < parameters.Length)
                {
                    float Val = parameters[tech_pressure.Index].CurrentValue;
                    if (!float.IsNaN(Val) && !float.IsInfinity(Val) &&
                            !float.IsNegativeInfinity(Val) && !float.IsPositiveInfinity(Val))
                    {
                        tech_pressure.Value = Val;
                    }
                }

                if (tech_temperature.Index > 0 && tech_temperature.Index < parameters.Length)
                {
                    float Val = parameters[tech_temperature.Index].CurrentValue;
                    if (!float.IsNaN(Val) && !float.IsInfinity(Val) &&
                            !float.IsNegativeInfinity(Val) && !float.IsPositiveInfinity(Val))
                    {
                        tech_temperature.Value = Val;
                    }
                }

                if (onCalculate != null)
                {
                    onCalculate(this, EventArgs.Empty);
                }

                // ---------------------------------------------------------------------------------
            }
        }

        /// <summary>
        /// Выполнить копирование с учетеом технологии
        /// </summary>
        private void BackingUpDataTech(Parameter[] parameters)
        {
            if (parameters != null)
            {
                // ----------- копируем значения каналов в технологическую примочку ---------------

                if (tech_consumption.Index >= 0 && tech_consumption.Index < parameters.Length)
                {
                    float Val = parameters[tech_consumption.Index].CurrentValue;
                    if (!float.IsNaN(Val) && !float.IsInfinity(Val) &&
                            !float.IsNegativeInfinity(Val) && !float.IsPositiveInfinity(Val))
                    {
                        tech_consumption.Value = Val;
                        tech_consumption.FormattedValue = string.Format(tech_consumption.Format, Val);
                    }
                    else
                    {
                        tech_consumption.FormattedValue = "-----";
                    }
                }

                if (tech_volume.Index >= 0 && tech_volume.Index < parameters.Length)
                {
                    float Val = parameters[tech_volume.Index].CurrentValue;
                    if (!float.IsNaN(Val) && !float.IsInfinity(Val) &&
                            !float.IsNegativeInfinity(Val) && !float.IsPositiveInfinity(Val))
                    {
                        tech_volume.Value = Val;
                        tech_volume.FormattedValue = string.Format(tech_volume.Format, Val);
                    }
                    else
                    {
                        tech_volume.FormattedValue = "-----";
                    }
                }

                if (tech_density.Index >= 0 && tech_density.Index < parameters.Length)
                {
                    float Val = parameters[tech_density.Index].CurrentValue;
                    if (!float.IsNaN(Val) && !float.IsInfinity(Val) &&
                            !float.IsNegativeInfinity(Val) && !float.IsPositiveInfinity(Val))
                    {
                        tech_density.Value = Val;
                        tech_density.FormattedValue = string.Format(tech_density.Format, Val);
                    }
                    else
                    {
                        tech_density.FormattedValue = "-----";
                    }
                }

                if (tech_pressure.Index >= 0 && tech_pressure.Index < parameters.Length)
                {
                    float Val = parameters[tech_pressure.Index].CurrentValue;
                    if (!float.IsNaN(Val) && !float.IsInfinity(Val) &&
                            !float.IsNegativeInfinity(Val) && !float.IsPositiveInfinity(Val))
                    {
                        tech_pressure.Value = Val;
                        tech_pressure.FormattedValue = string.Format(tech_pressure.Format, Val);
                    }
                    else
                        tech_pressure.FormattedValue = "-----";
                }

                if (tech_temperature.Index >= 0 && tech_temperature.Index < parameters.Length)
                {
                    float Val = parameters[tech_temperature.Index].CurrentValue;
                    if (!float.IsNaN(Val) && !float.IsInfinity(Val) &&
                            !float.IsNegativeInfinity(Val) && !float.IsPositiveInfinity(Val))
                    {
                        tech_temperature.Value = Val;
                        tech_temperature.FormattedValue = string.Format(tech_temperature.Format, Val);
                    }
                    else
                        tech_temperature.FormattedValue = "-----";
                }

                // ---------- тестовое ----------

                foreach (Rgr rgr in rgrs)
                {
                    if (rgr.Volume.Index >= 0 && rgr.Volume.Index < parameters.Length)
                    {
                        float Val = parameters[rgr.Volume.Index].CurrentValue;
                        if (!float.IsNaN(Val) && !float.IsInfinity(Val) &&
                                !float.IsNegativeInfinity(Val) && !float.IsPositiveInfinity(Val))
                        {
                            rgr.Volume.Value = Val;
                        }
                    }

                    if (rgr.Consumption.Index >= 0 && rgr.Consumption.Index < parameters.Length)
                    {
                        float Val = parameters[rgr.Consumption.Index].CurrentValue;
                        if (!float.IsNaN(Val) && !float.IsInfinity(Val) &&
                                !float.IsNegativeInfinity(Val) && !float.IsPositiveInfinity(Val))
                        {
                            rgr.Consumption.Value = Val;
                        }
                    }
                }

                // ---------------------------------------------------------------------------------

                TechStage current = Stages.Current;
                if (current != null)
                {
                    is_finished = true;                    
                    current.Calculate(rgrs);
                    //rgr.Calculate(stages.CorrectionFactor, current.StateRGR);

                    if (onCalculate != null)
                    {
                        onCalculate(this, EventArgs.Empty);
                    }
                }
                else
                {
                    if (!is_finished)
                    {
                        if (onJop != null)
                        {
                            onJop(this, EventArgs.Empty);
                        }
                    }
                }
            }
        }

        public bool is_finished = false;

        /// <summary>
        /// Определяет расход
        /// </summary>
        public TechParameter Consumption
        {
            get
            {
                return tech_consumption;
            }
        }

        /// <summary>
        /// Определяет объем
        /// </summary>
        public TechParameter Volume
        {
            get
            {
                return tech_volume;
            }
        }

        /// <summary>
        /// Определяет объем процесса
        /// </summary>
        public TechParameter ProccessVolume
        {
            get
            {
                return tech_proccessVolume;
            }
        }

        /// <summary>
        /// Определяет плотность
        /// </summary>
        public TechParameter Density
        {
            get
            {
                return tech_density;
            }
        }

        /// <summary>
        /// Определяет давление
        /// </summary>
        public TechParameter Pressure
        {
            get
            {
                return tech_pressure;
            }
        }


        /// <summary>
        /// Определяет температуру
        /// </summary>
        public TechParameter Temperature
        {
            get
            {
                return tech_temperature;
            }
        }

        /// <summary>
        /// Реализует этапы работы
        /// </summary>
        public TechStages Stages
        {
            get
            {
                return stages;
            }
        }

        /// <summary>
        /// имя узла в котором храняться настройки технологии
        /// </summary>
        public const string TechRoot = "tech_root";

        /// <summary>
        /// Сохранить настройки технологии
        /// </summary>
        public void Save(XmlDocument doc, XmlNode root)
        {
            try
            {
                if (doc != null)
                {
                    if (doc != null && root != null)
                    {
                        XmlNode tech_root = doc.CreateElement(TechRoot);

                        /*int index_old_consumption = -1;
                        int index_old_volume = -1;

                        foreach (Rgr rgr in rgrs)
                        {
                            if (rgr.IsMain)
                            {
                                index_old_volume = rgr.Volume.gType;
                                index_old_consumption = rgr.Consumption.gType;
                            }
                        }*/

                        TechParameter[] parameters = 
                        {
                             tech_consumption, tech_volume, tech_density,
                             tech_pressure, tech_temperature, tech_proccessVolume
                        };

                        foreach (TechParameter parameter in parameters)
                        {
                            XmlNode t_node = parameter.SerializeToXml(doc);
                            if (t_node != null)
                            {
                                tech_root.AppendChild(t_node);
                            }
                        }

                        XmlNode rgrsNode = rgrs.Save(doc);
                        if (rgrsNode != null)
                        {
                            tech_root.AppendChild(rgrsNode);
                        }

                        root.AppendChild(tech_root);
                    }
                }
            }
            catch { }
        }

        /// <summary>
        /// Загрузить настройки технологии
        /// </summary>
        public void Load(XmlNode root)
        {
            try
            {
                if (root != null && root.Name == TechRoot)
                {
                    if (root.HasChildNodes)
                    {
                        if (root.ChildNodes.Count == 6)
                        {
                            tech_consumption.DeserializeFromXml(root.ChildNodes.Item(0));
                            tech_volume.DeserializeFromXml(root.ChildNodes.Item(1));
                            tech_density.DeserializeFromXml(root.ChildNodes.Item(2));
                            tech_pressure.DeserializeFromXml(root.ChildNodes.Item(3));
                            tech_temperature.DeserializeFromXml(root.ChildNodes.Item(4));
                            tech_proccessVolume.DeserializeFromXml(root.ChildNodes.Item(5));
                        }
                        else
                        {
                            if (root.ChildNodes.Count == 7)
                            {
                                tech_consumption.DeserializeFromXml(root.ChildNodes.Item(0));
                                tech_volume.DeserializeFromXml(root.ChildNodes.Item(1));
                                tech_density.DeserializeFromXml(root.ChildNodes.Item(2));
                                tech_pressure.DeserializeFromXml(root.ChildNodes.Item(3));
                                tech_temperature.DeserializeFromXml(root.ChildNodes.Item(4));
                                tech_proccessVolume.DeserializeFromXml(root.ChildNodes.Item(5));

                                rgrs = RgrList.Load(root.ChildNodes.Item(6));
                                if (rgrs.Count < 8)
                                {
                                    int n_count = 8 - rgrs.Count;
                                    for (int i = 0; i < n_count; i++)
                                    {
                                        rgrs.Add(new Rgr());
                                    }
                                }
                            }
                        }
                    }
                }
            }
            catch { }
        }
    }

    /// <summary>
    /// Режимы в которых может работать приложение
    /// </summary>
    public enum TechMode
    {
        /// <summary>
        /// Технологический режим
        /// </summary>
        Technology,

        /// <summary>
        /// Простой режим (не выполняются технологические преобразования данных)
        /// </summary>
        Default
    }
}