using System;
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

        // ---- тестовое решение ----

        protected ReaderWriterLockSlim slim;    // синхронизатор
        protected List<GraphicValue> values;    // базовые значения точек для отрисовки ТЕСТОВЫЙ ВАРИАНТ

        /// <summary>
        /// инициализирует новый экземпляр класса
        /// </summary>
        public Graphic()
        {
            locker = new ReaderWriterLockSlim(LockRecursionPolicy.SupportsRecursion);

            desc = string.Empty;
            units = string.Empty;

            range = new Range();

            color = Color.Black;
            font = new Font(FontFamily.GenericSansSerif, 8.5f, FontStyle.Regular);

            current = float.NaN;

            // ---- ТЕСТОВОЕ РЕШЕНИЕ ----

            slim = new ReaderWriterLockSlim(LockRecursionPolicy.SupportsRecursion);
            values = new List<GraphicValue>();
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

        /// <summary>
        /// Пересчитать точки на отрисовку
        /// </summary>
        /// <param name="pt">Начало координат для отрисовки</param>
        /// <param name="size">Размер области в которую осуществляется отрисовка</param>
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
                        float wX = (vals[index].Value - Range.Min) * WK + pt.X;
                        float tY = (vals[index].Time.Ticks - panel.StartTime.Ticks) * HK + pt.Y;

                        pts[index] = new PointF(wX, tY);
                    }

                    return Array.FindAll(pts, Predicate);
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