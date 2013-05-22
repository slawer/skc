using System;
using System.Xml;
using System.Drawing;
using System.Threading;
using System.Collections.Generic;

namespace GraphicComponent
{
    /// <summary>
    /// Реализует график для отображения в графическом компоненте
    /// </summary>
    public class Graphic
    {
        protected ReaderWriterLockSlim locker;  // синхронизатор

        protected Font font;                    // шрифт которым отрисовывать описание графика
        protected Color color;                  // цвет которым отрисовывать график

        protected Range range;                  // диапазон отображения графика

        protected string desc;                  // текстовое описание графика
        protected string units;                 // текстовое описание единиц измерения отображаемого параметра        

        protected float current;                // текуущее значение графика
        protected float passive;                // отображаемое значение графика в режиме просмотра

        protected float width;                  // ширина линии графика

        protected PointF[] tmpPnts = new PointF[10000]; // Динамический Список точек графика, вставляемы в результате прореживания

        // ---- тестовое решение ----

        protected ReaderWriterLockSlim slim;    // синхронизатор
        protected List<GraphicValue> values;    // базовые значения точек для отрисовки ТЕСТОВЫЙ ВАРИАНТ

        protected bool actived = true;          // отрисовывать график или нет
        
        protected DateTime _minT;                // минимальное время графика

        /// <summary>
        /// инициализирует новый экземпляр класса
        /// </summary>
        public Graphic()
        {
            locker = new ReaderWriterLockSlim(LockRecursionPolicy.SupportsRecursion);

            desc = string.Empty;
            units = string.Empty;

            range = new Range();

            color = SystemColors.Control; //Color.Black;
            font = new Font(FontFamily.GenericSansSerif, 8.5f, FontStyle.Regular);

            current = float.NaN;

            // ---- ТЕСТОВОЕ РЕШЕНИЕ ----

            slim = new ReaderWriterLockSlim(LockRecursionPolicy.SupportsRecursion);
            values = new List<GraphicValue>();

            _minT = DateTime.MaxValue;
        }

        /// <summary>
        /// Минимальное значение времени графика
        /// </summary>
        public DateTime Tmin
        {
            get
            {
                if (locker.TryEnterReadLock(100))
                {
                    try
                    {
                        return _minT;
                    }
                    finally
                    {
                        locker.ExitReadLock();
                    }
                }

                return DateTime.MinValue;
            }

            set
            {
                if (locker.TryEnterWriteLock(300))
                {
                    try
                    {
                        _minT = value;
                    }
                    finally
                    {
                        locker.ExitWriteLock();
                    }
                }
            }
        }

        public float Width
        {
            get
            {
                if (locker.TryEnterReadLock(300))
                {
                    try
                    {
                        return width;
                    }
                    finally
                    {
                        locker.ExitReadLock();
                    }
                }

                return 1;
            }

            set
            {
                if (locker.TryEnterWriteLock(500))
                {
                    try
                    {
                        width = value;
                    }
                    finally
                    {
                        locker.ExitWriteLock();
                    }
                }
            }
        }

        /// <summary>
        /// Определяет фон которым отоброфать текстовое описание графика
        /// </summary>
        public Font Font
        {
            get
            {
                if (locker.TryEnterReadLock(300))
                {
                    try
                    {
                        return font;
                    }
                    finally
                    {
                        locker.ExitReadLock();
                    }
                }
                return null;
            }

            set
            {
                if (locker.TryEnterWriteLock(500))
                {
                    try
                    {
                        font = value;
                    }
                    finally
                    {
                        locker.ExitWriteLock();
                    }
                }
            }
        }

        /// <summary>
        /// Определяет тексотовое описание графика
        /// </summary>
        public string Description
        {
            get
            {
                if (locker.TryEnterReadLock(300))
                {
                    try
                    {
                        return desc;
                    }
                    finally
                    {
                        locker.ExitReadLock();
                    }
                }
                return string.Empty;
            }

            set
            {
                if (locker.TryEnterWriteLock(500))
                {
                    try
                    {
                        desc = value;
                    }
                    finally
                    {
                        locker.ExitWriteLock();
                    }
                }
            }
        }

        /// <summary>
        /// Определяет строку к которой содиржиться информация о том 
        /// в каких единицах измеряется отображаемый параметр
        /// </summary>
        public string Units
        {
            get
            {
                if (locker.TryEnterReadLock(300))
                {
                    try
                    {
                        return units;
                    }
                    finally
                    {
                        locker.ExitReadLock();
                    }
                }
                return string.Empty;
            }

            set
            {
                if (locker.TryEnterWriteLock(500))
                {
                    try
                    {
                        units = value;
                    }
                    finally
                    {
                        locker.ExitWriteLock();
                    }
                }
            }
        }

        /// <summary>
        /// Определяет цвет которым отрисовывать график
        /// </summary>
        public Color Color
        {
            get
            {
                if (locker.TryEnterReadLock(300))
                {
                    try
                    {
                        return color;
                    }
                    finally
                    {
                        locker.ExitReadLock();
                    }
                }
                return Color.Empty;
            }

            set 
            {
                if (locker.TryEnterWriteLock(500))
                {
                    try
                    {
                        color = value;
                    }
                    finally
                    {
                        locker.ExitWriteLock();
                    }
                }
            }
        }

        /// <summary>
        /// определяет текущее значение параметра для отображения на шкале
        /// </summary>
        public float Current
        {
            get
            {
                if (locker.TryEnterReadLock(300))
                {
                    try
                    {
                        return current;
                    }
                    finally
                    {
                        locker.ExitReadLock();
                    }
                }
                return float.NaN;
            }

            set
            {
                if (locker.TryEnterWriteLock(500))
                {
                    try
                    {
                        current = value;
                    }
                    finally
                    {
                        locker.ExitWriteLock();
                    }
                }
            }
        }

        /// <summary>
        /// определяет текущее значение параметра для отображения на шкале в пассивном режиме
        /// </summary>
        public float Passive
        {
            get
            {
                if (locker.TryEnterReadLock(300))
                {
                    try
                    {
                        return passive;
                    }
                    finally
                    {
                        locker.ExitReadLock();
                    }
                }
                return float.NaN;
            }

            set
            {
                if (locker.TryEnterWriteLock(500))
                {
                    try
                    {
                        passive = value;
                    }
                    finally
                    {
                        locker.ExitWriteLock();
                    }
                }
            }
        }

        /// <summary>
        /// Возвращяет диапазон отображаемых значений для графика
        /// </summary>
        public Range Range
        {
            get { return range; }
        }

        /// <summary>
        /// Возвращяет цвет которым отрисовывать график. Только для чтения
        /// </summary>
        public SolidBrush Brush
        {
            get
            {
                return new SolidBrush(Color);
            }
        }

        // ---- тестовое решение ----

        /// <summary>
        /// Добавить точку для отрисовки
        /// </summary>
        /// <param name="_time">Время</param>
        /// <param name="_value">Значение</param>
        public void Insert(DateTime _time, Single _value)
        {
            if (slim.TryEnterWriteLock(500))
            {
                try
                {
                    values.Add(new GraphicValue(_time, _value));
                    current = _value;
                }
                finally
                {
                    slim.ExitWriteLock();
                }
            }
        }

        /// <summary>
        /// Очистить список точек на отрисовку
        /// </summary>
        public void Clear()
        {
            if (slim.TryEnterWriteLock(500))
            {
                try
                {
                    values.Clear();
                    current = float.NaN;
                }
                finally
                {
                    slim.ExitWriteLock();
                }
            }
        }

        /// <summary>
        /// Возвращяет стартовое время для графика
        /// </summary>
        public DateTime StartTimeOfGraphic
        {
            get
            {
                if (slim.TryEnterReadLock(300))
                {
                    try
                    {
                        DateTime _min = DateTime.MaxValue;
                        foreach (GraphicValue val in values)
                        {
                            if (val.Time < _min)
                            {
                                _min = val.Time;
                            }
                        }
                        
                        return _min;
                    }
                    finally
                    {
                        slim.ExitReadLock();
                    }
                }

                return DateTime.MaxValue;
            }
        }

        /// <summary>
        /// Возвращяет конечное время для графика
        /// </summary>
        public DateTime FinishTimeOfGraphic
        {
            get
            {
                if (slim.TryEnterReadLock(300))
                {
                    try
                    {
                        DateTime _max = DateTime.MinValue;
                        foreach (GraphicValue val in values)
                        {
                            if (val.Time > _max)
                            {
                                _max = val.Time;
                            }
                        }
                        return _max;
                    }
                    finally
                    {
                        slim.ExitReadLock();
                    }
                }

                return DateTime.MaxValue;
            }
        }

        /// <summary>
        /// Получить значение по времени
        /// </summary>
        /// <param name="Time">Время</param>
        /// <returns>Найденное значение</returns>
        public float GetValueFromTime(DateTime Time)
        {
            if (slim.TryEnterReadLock(300))
            {
                try
                {
                    if (values != null)
                    {
                        foreach (GraphicValue gvalue in values)
                        {
                            long a = gvalue.Time.Ticks / 10000000;
                            long b = Time.Ticks / 10000000;

                            if (a == b)
                            {
                                return gvalue.Value;
                            }
                        }
                    }
                }
                finally
                {
                    slim.ExitReadLock();
                }
            }

            return float.NaN;
        }

        // -------------------------------------

        private GraphicValue[] GetValues()
        {
            if (slim.TryEnterReadLock(300))
            {
                try
                {
                    return values.ToArray();
                }
                finally
                {
                    slim.ExitReadLock();
                }
            }

            return null;
        }

        // ==========
        // 07.12.2011
        /// <summary>
        /// Выборка точек из буфера в заданном интервале времени
        /// </summary>
        /// <param name="start">Время начала интервала</param>
        /// <param name="finish">Время конца интервала</param>
        /// <returns>Массив отобранных точек</returns>
        private GraphicValue[] GetValues(DateTime start, DateTime finish)
        {
            if (slim.TryEnterReadLock(300))
            {
                try
                {
                    if (values != null && values.Count > 0)
                    {
                        // Поиск начала и конца массива
                        if (values[0].Time > finish) return null;
                        int jCount = values.Count;
                        if (values[jCount - 1].Time < start) return null;

                        int j1 = -1;
                        for (int j = 0; j < jCount; j++)
                        {
                            if (values[j].Time <= start) continue;
                            j1 = j;
                            break;
                        }
                        if (j1 == -1) return null;
                        int j2 = j1;
                        for (int j = j1 + 1; j < jCount; j++)
                        {
                            if (values[j].Time > finish) break;
                            j2 = j;
                        }

                        // копирование выделенного участка данных
                        GraphicValue[] tmp = new GraphicValue[j2 - j1 + 1];
                        int jPos = 0;
                        for (int j = j1; j <= j2; j++)
                        {
                            tmp[jPos] = values[j];
                            jPos++;
                        }

                        return tmp;
                    }
                }
                finally
                {
                    slim.ExitReadLock();
                }
            }

            return null;
        }
        // ==========

        /// <summary>
        /// Пересчитать точки на отрисовку
        /// </summary>
        /// <param name="pt">Начало координат для отрисовки</param>
        /// <param name="size">Размер области в которую осуществляется отрисовка</param>
        /// <returns>Массив точек графика в координатах экрана</returns>
        public PointF[] Calculate(PointF pt, SizeF size, Panel panel)
        {
            try
            {
                GraphicValue[] vals = GetValues();
                if (vals != null)
                {
                    float HK = panel.GridHeight / panel.IntervalInCell.Ticks;

                    float WRK = Range.Max - Range.Min;
                    float WK = size.Width / WRK;

                    PointF[] pts = new PointF[vals.Length];
                    for (int index = 0; index < vals.Length; index++)
                    {
                        float wX = float.NaN;
                        if (!float.IsNaN(vals[index].Value))
                            wX = (vals[index].Value - Range.Min) * WK + pt.X;
                        float tY = (vals[index].Time.Ticks - panel.StartTime.Ticks) * HK + pt.Y;

                        pts[index] = new PointF(wX, tY);
                    }

                    //                    return Array.FindAll(pts, Predicate);
                    return pts;
                }
            }
            catch { }
            return null;
        }


        /// <summary>
        /// Пересчитать точки на отрисовку cо сжатием
        /// </summary>
        /// <param name="pt">Начало координат для отрисовки</param>
        /// <param name="size">Размер области в которую осуществляется отрисовка</param>
        /// <returns>Массив точек графика в координатах экрана</returns>
        public PointF[] CalculateReduce(PointF pt, SizeF size, Panel panel)
        {
            try
            {
                if (panel.StartTime > panel.FinishTime) return null;
                GraphicValue[] vals = GetValues(panel.StartTime, panel.FinishTime);
                //GraphicValue[] vals = GetValues();

                if (vals != null)
                {
                    float HK = panel.GridHeight / panel.IntervalInCell.Ticks;

                    float WRK = Range.Max - Range.Min;
                    float WK = size.Width / WRK;


                    int IndexBeg = -1;
                    // Ищем первую значимую точку
                    for (int index = 0; index < vals.Length; index++)
                    {
                        if (float.IsNaN(vals[index].Value)) continue;
                        IndexBeg = index;
                        break;
                    }
                    if ((IndexBeg == -1) || ((IndexBeg + 1) == vals.Length))
                    {
                        return null; // Рисовать нечего
                    }


//                    PointF[] tmp = new PointF[10000]; // Динамический Список точек графика, вставляемы в результате прореживания
                    int ind_tmp = 0;

                    float wX = (vals[IndexBeg].Value - Range.Min) * WK + pt.X;
                    float tY = (vals[IndexBeg].Time.Ticks - panel.StartTime.Ticks) * HK + pt.Y;
                    PointF Pnt = new PointF(wX, tY);
                    this.tmpPnts[0] = Pnt;
                    int gridPoint = (int)Math.Ceiling(tY); // ближайшая точка сетки
                    float floatPoint = gridPoint; // Эквивалент в типе float
                    if (floatPoint == tY) floatPoint += 1;
                    int intrv = 0; // Длина найденного интервала
                    bool bNan = false; //Признак того что NaN уже вставлен
                    float val = 0; // Накопленное значение для усреднения


                    // Просмотр массива - выявление интервалов длиной 1 пиксел по времени
                    for (int index = IndexBeg+1; index < vals.Length; index++)
                    {
                        if (vals[index].Time.Ticks > panel.FinishTime.Ticks) break;

                        wX = vals[index].Value;
                        if (float.IsNaN(wX))
                        {
                            if (bNan)
                            {
                                continue;
                            }
                            else
                            {
                                if (intrv > 0)
                                {
                                    // создаём новую точку в списке
                                    val = val / intrv;
                                    Pnt.X = (val - Range.Min) * WK + pt.X;
                                    Pnt.Y = floatPoint;
                                    this.tmpPnts[++ind_tmp] = Pnt;
                                }

                                // Добавляем в список NaN c той же координатой
                                wX = float.NaN;
                                Pnt.X = wX;
                                Pnt.Y = floatPoint;
                                this.tmpPnts[++ind_tmp] = Pnt;

                                intrv = 0;
                                val = 0;
                                bNan = true;
                                continue;
                            }
                        }

                        tY = (vals[index].Time.Ticks - panel.StartTime.Ticks) * HK + pt.Y;
                        // Проверка на выход в новую клетку
                        if (tY <= floatPoint)
                        {
                            if (bNan)
                            {
                                continue;
                            }
                            else
                            {
                                val += wX;
                                intrv++;
                            }
                        }
                        else
                        {
                            if (bNan)
                            {
                                bNan = false;
                                val = wX;
                                intrv = 1;
                                gridPoint = (int)Math.Ceiling(tY); // ближайшая точка сетки
                                floatPoint = gridPoint; // Эквивалент в типе float
                                continue;
                            }
                            else
                            {
                                if (intrv > 0)
                                {
                                    // создаём новую точку в списке
                                    val = val / intrv;
                                    Pnt.X = (val - Range.Min) * WK + pt.X;
                                    Pnt.Y = floatPoint;
                                    this.tmpPnts[++ind_tmp] = Pnt;
                                }
                                val = wX;
                                intrv = 1;
                                gridPoint = (int)Math.Ceiling(tY); // ближайшая точка сетки
                                floatPoint = gridPoint; // Эквивалент в типе float
                                continue;
                            }
                        }
                    }

                    if (intrv > 0) // Последнюю точку не забыть бы
                    {
                        // создаём новую точку в списке
                        Pnt.X = (wX - Range.Min) * WK + pt.X;
                        Pnt.Y = tY;
                        this.tmpPnts[++ind_tmp] = Pnt;
                    }


                    PointF[] pts = new PointF[ind_tmp+1];
                    for (int index = 0; index <= ind_tmp; index++)
                    {
                        pts[index] = this.tmpPnts[index];
                    }

                    //                    return Array.FindAll(pts, Predicate);
                    return pts;
                }
            }
            catch { }
            return null;
        }

        protected DateTime lastTimeTimer = DateTime.MaxValue;

        /// <summary>
        /// Пересчитать точки на отрисовку cо сжатием
        /// </summary>
        /// <param name="pt">Начало координат для отрисовки</param>
        /// <param name="size">Размер области в которую осуществляется отрисовка</param>
        /// <returns>Массив точек графика в координатах экрана</returns>
        public PointF[] CalculateReduceTimer(PointF pt, SizeF size, Panel panel, DateTime currentTime)
        {
            try
            {
                if (lastTimeTimer == DateTime.MaxValue) lastTimeTimer = currentTime;

                DateTime startTime = panel.dateTimeCell(currentTime);
                DateTime finishTime = startTime + panel.IntervalInCell;

                if (lastTimeTimer <= startTime) startTime = lastTimeTimer;

                GraphicValue[] vals = GetValues(startTime, currentTime);
                if (vals != null)
                {
                    //lastTimeTimer = currentTime;
                    lastTimeTimer = vals[vals.Length - 1].Time;
                    
                    float HK = panel.GridHeight / panel.IntervalInCell.Ticks;

                    float WRK = Range.Max - Range.Min;
                    float WK = size.Width / WRK;


                    int IndexBeg = -1;
                    // Ищем первую значимую точку
                    for (int index = 0; index < vals.Length; index++)
                    {
                        if (float.IsNaN(vals[index].Value)) continue;
                        IndexBeg = index;
                        break;
                    }
                    if ((IndexBeg == -1) || ((IndexBeg + 1) == vals.Length))
                    {
                        return null; // Рисовать нечего
                    }


                    //                    PointF[] tmp = new PointF[10000]; // Динамический Список точек графика, вставляемы в результате прореживания
                    int ind_tmp = 0;

                    float wX = (vals[IndexBeg].Value - Range.Min) * WK + pt.X;
                    float tY = (vals[IndexBeg].Time.Ticks - panel.StartTime.Ticks) * HK + pt.Y;
                    PointF Pnt = new PointF(wX, tY);
                    this.tmpPnts[0] = Pnt;
                    int gridPoint = (int)Math.Ceiling(tY); // ближайшая точка сетки
                    float floatPoint = gridPoint; // Эквивалент в типе float
                    if (floatPoint == tY) floatPoint += 1;
                    int intrv = 0; // Длина найденного интервала
                    bool bNan = false; //Признак того что NaN уже вставлен
                    float val = 0; // Накопленное значение для усреднения


                    // Просмотр массива - выявление интервалов длиной 1 пиксел по времени
                    for (int index = IndexBeg + 1; index < vals.Length; index++)
                    {
                        if (vals[index].Time.Ticks > panel.FinishTime.Ticks) break;

                        wX = vals[index].Value;
                        if (float.IsNaN(wX))
                        {
                            if (bNan)
                            {
                                continue;
                            }
                            else
                            {
                                if (intrv > 0)
                                {
                                    // создаём новую точку в списке
                                    val = val / intrv;
                                    Pnt.X = (val - Range.Min) * WK + pt.X;
                                    Pnt.Y = floatPoint;
                                    this.tmpPnts[++ind_tmp] = Pnt;
                                }

                                // Добавляем в список NaN c той же координатой
                                wX = float.NaN;
                                Pnt.X = wX;
                                Pnt.Y = floatPoint;
                                this.tmpPnts[++ind_tmp] = Pnt;

                                intrv = 0;
                                val = 0;
                                bNan = true;
                                continue;
                            }
                        }

                        tY = (vals[index].Time.Ticks - panel.StartTime.Ticks) * HK + pt.Y;
                        // Проверка на выход в новую клетку
                        if (tY <= floatPoint)
                        {
                            if (bNan)
                            {
                                continue;
                            }
                            else
                            {
                                val += wX;
                                intrv++;
                            }
                        }
                        else
                        {
                            if (bNan)
                            {
                                bNan = false;
                                val = wX;
                                intrv = 1;
                                gridPoint = (int)Math.Ceiling(tY); // ближайшая точка сетки
                                floatPoint = gridPoint; // Эквивалент в типе float
                                continue;
                            }
                            else
                            {
                                if (intrv > 0)
                                {
                                    // создаём новую точку в списке
                                    val = val / intrv;
                                    Pnt.X = (val - Range.Min) * WK + pt.X;
                                    Pnt.Y = floatPoint;
                                    this.tmpPnts[++ind_tmp] = Pnt;
                                }
                                val = wX;
                                intrv = 1;
                                gridPoint = (int)Math.Ceiling(tY); // ближайшая точка сетки
                                floatPoint = gridPoint; // Эквивалент в типе float
                                continue;
                            }
                        }
                    }

                    if (intrv > 0) // Последнюю точку не забыть бы
                    {
                        // создаём новую точку в списке
                        Pnt.X = (wX - Range.Min) * WK + pt.X;
                        Pnt.Y = tY;
                        this.tmpPnts[++ind_tmp] = Pnt;
                    }


                    PointF[] pts = new PointF[ind_tmp + 1];
                    for (int index = 0; index <= ind_tmp; index++)
                    {
                        pts[index] = this.tmpPnts[index];
                    }

                    //                    return Array.FindAll(pts, Predicate);
                    return pts;
                }
            }
            catch { }
            return null;
        }
        /// <summary>
        /// Условие выборки точек на отрисовку
        /// </summary>
        /// <param name="pt"></param>
        /// <returns></returns>
        private bool Predicate(PointF pt)
        {
            if (!float.IsNaN(pt.X) && !float.IsNaN(pt.Y))
            {
                return true;
            }

            return false;
        }

        // --------------------------------
        /// <summary>
        /// сохранить график
        /// </summary>
        /// <param name="doc">документ куда сохранять</param>
        /// <param name="root_name">имя параметра</param>
        /// <returns></returns>
        public XmlNode SerializeToXml(XmlDocument doc, String root_name)
        {
            try
            {
                XmlNode root = doc.CreateElement(root_name);

                XmlNode colorNode = doc.CreateElement("color");

                XmlNode rangeNodeMin = doc.CreateElement("range_min");
                XmlNode rangeNodeMax = doc.CreateElement("range_max");

                XmlNode descNode = doc.CreateElement("desc");
                XmlNode unitsNode = doc.CreateElement("units");
                XmlNode widthNode = doc.CreateElement("width");

                colorNode.InnerText = color.ToArgb().ToString();

                rangeNodeMin.InnerText = range.Min.ToString();
                rangeNodeMax.InnerText = range.Max.ToString();

                descNode.InnerText = desc;
                unitsNode.InnerText = units;

                widthNode.InnerText = width.ToString();

                root.AppendChild(colorNode);

                root.AppendChild(rangeNodeMin);
                root.AppendChild(rangeNodeMax);

                root.AppendChild(descNode);
                root.AppendChild(unitsNode);
                root.AppendChild(widthNode);

                return root;
            }
            catch { }
            return null;
        }

        // --------------------------------
        /// <summary>
        /// загрузить график
        /// </summary>
        /// <param name="doc">документ куда сохранять</param>
        /// <param name="root_name">имя параметра</param>
        /// <returns></returns>
        public void DeSerializeToXml(XmlNode root)
        {
            try
            {
                if (root != null && root.HasChildNodes)
                {
                    foreach (XmlNode child in root.ChildNodes)
                    {
                        switch (child.Name)
                        {
                            case "color":

                                try
                                {
                                    color = Color.FromArgb(int.Parse(child.InnerText));
                                }
                                catch { }
                                break;

                            case "range_min":

                                try
                                {
                                    range.Min = float.Parse(child.InnerText);
                                }
                                catch { }
                                break;

                            case "range_max":

                                try
                                {
                                    range.Max = float.Parse(child.InnerText);
                                }
                                catch { }
                                break;

                            case "desc":

                                try
                                {
                                    desc = child.InnerText;
                                }
                                catch { }
                                break;

                            case "units":

                                try
                                {
                                    units = child.InnerText;
                                }
                                catch { }
                                break;

                            case "width":

                                try
                                {
                                    width = float.Parse(child.InnerText);
                                }
                                catch { }
                                break;
                                
                            default:
                                break;
                        }
                    }
                }



            }
            catch { }
        }
    }

    /// <summary>
    /// Реализует хранение диапазона допустимых значений для параметра
    /// </summary>
    public class Range
    {
        protected ReaderWriterLockSlim slim;     // синхронизатор

        protected float _min;                   // минимальное значение диапазона
        protected float _max;                   // максимальное значение диапазона

        /// <summary>
        /// Инициализирует новый экземпляр класса
        /// </summary>
        public Range()
        {
            slim = new ReaderWriterLockSlim();

            _min = -10;
            _max = 75;
        }

        /// <summary>
        /// Инициализирует новый экземпляр класса
        /// </summary>
        /// <param name="MinValue">Минимальное значение диапазона</param>
        /// <param name="MaxValue">Максимальное значение диапазона</param>
        public Range(float MinValue, float MaxValue)
        {
            slim = new ReaderWriterLockSlim();

            if (MinValue <= MaxValue)
            {
                _min = MinValue;
                _max = MaxValue;
            }
            else
            {
                _min = MaxValue;
                _max = MinValue;
            }
        }

        /// <summary>
        /// Определяет минимальное значение диапазона
        /// </summary>
        public float Min
        {
            get
            {
                if (slim.TryEnterReadLock(300))
                {
                    try
                    {
                        return _min;
                    }
                    finally
                    {
                        slim.ExitReadLock();
                    }
                }
                return float.NaN;
            }

            set
            {
                if (slim.TryEnterWriteLock(500))
                {
                    try
                    {
                        if (value <= _max)
                        {
                            _min = value;
                        }
                        else
                        {
                            _min = _max;
                            _max = value;
                        }
                    }
                    finally
                    {
                        slim.ExitWriteLock();
                    }
                }
            }
        }

        /// <summary>
        /// Определяет максимальное значение диапазона
        /// </summary>
        public float Max
        {
            get
            {
                if (slim.TryEnterReadLock(300))
                {
                    try
                    {
                        return _max;
                    }
                    finally
                    {
                        slim.ExitReadLock();
                    }
                }
                return float.NaN;
            }

            set
            {
                if (slim.TryEnterWriteLock(500))
                {
                    try
                    {
                        if (value >= _min)
                        {
                            _max = value;
                        }
                        else
                        {
                            _max = _min;
                            _min = value;
                        }
                    }
                    finally
                    {
                        slim.ExitWriteLock();
                    }
                }
            }
        }
    }

    /// <summary>
    /// реализует пару значений для отображения на графике
    /// </summary>
    public class GraphicValue
    {
        //protected ReaderWriterLockSlim locker = null;       // синхронизатор

        protected DateTime __time;      // время
        protected Single __value;       // значение

        /// <summary>
        /// Инициализирует новый экземпляр класса
        /// </summary>
        public GraphicValue()
        {
            //locker = new ReaderWriterLockSlim();

            __time = DateTime.Now;
            __value = Single.NaN;
        }

        /// <summary>
        /// Инициализирует новый экземпляр класса
        /// </summary>
        /// <param name="time">Время</param>
        /// <param name="value">Значение</param>
        public GraphicValue(DateTime time, Single value)
            : this()
        {
            __time = time;
            __value = value;
        }

        /// <summary>
        /// Определяет время
        /// </summary>
        public DateTime Time
        {
            get
            {
                //if (locker.TryEnterReadLock(300))
                {
                    try
                    {
                        return __time;
                    }
                    finally
                    {
                        //locker.ExitReadLock();
                    }
                }

                //return DateTime.MinValue;
            }

            set
            {
                //if (locker.TryEnterWriteLock(500))
                {
                    try
                    {
                        __time = value;
                    }
                    finally
                    {
                  //      locker.ExitWriteLock();
                    }
                }
            }
        }

        /// <summary>
        /// Определет значение
        /// </summary>
        public Single Value
        {
            get
            {
                //if (locker.TryEnterReadLock(300))
                {
                    try
                    {
                        return __value;
                    }
                    finally
                    {
                  //      locker.ExitReadLock();
                    }
                }

                //return Single.NaN;
            }

            set
            {
                //if (locker.TryEnterWriteLock(500))
                {
                    try
                    {
                        __value = value;
                    }
                    finally
                    {
                  //      locker.ExitWriteLock();
                    }
                }
            }
        }
    }
}