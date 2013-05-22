using System;
using System.Drawing;
using System.Threading;

namespace DisplayComponent
{
    /// <summary>
    /// Реализует число компонента
    /// </summary>
    public class Digit
    {
        protected Font font;            // шрифт которым отрисовывать число
        protected Color color;          // цвет которым отрисовывать число

        protected string format;        // формат числа
                
        protected float c_value;        // текущее значение параметра
        protected string d_value;       // описание параметра для отрисовки

        protected int index;            // номер параметра в списке для отрисовки

        /// <summary>
        /// синхронизатор
        /// </summary>
        protected ReaderWriterLockSlim slim;

        /// <summary>
        /// Инициализирует новый экземпляр класса
        /// </summary>
        public Digit()
        {
            format = "{0:F3}";
            color = Color.Black;

            font = new Font(FontFamily.GenericSansSerif, 11.0f, FontStyle.Regular);
            slim = new ReaderWriterLockSlim(LockRecursionPolicy.SupportsRecursion);
        }

        /// <summary>
        /// Определяет цвет которым отрисовывать число
        /// </summary>
        public Color Color
        {
            get
            {
                if (slim.TryEnterReadLock(300))
                {
                    try
                    {
                        return color;
                    }
                    finally
                    {
                        slim.ExitReadLock();
                    }
                }

                return Color.Black;
            }

            set
            {
                if (slim.TryEnterWriteLock(500))
                {
                    try
                    {
                        color = value;
                    }
                    finally
                    {
                        slim.ExitWriteLock();
                    }
                }
            }
        }

        /// <summary>
        /// Определяет шрифт которым выполнять отрисовку числа
        /// </summary>
        public Font Font
        {
            get
            {
                if (slim.TryEnterReadLock(300))
                {
                    try
                    {
                        return font;
                    }
                    finally
                    {
                        slim.ExitReadLock();
                    }
                }

                return null;
            }
        }

        /// <summary>
        /// Определяет формат отображаемого числа
        /// </summary>
        public string Format
        {
            get
            {
                if (slim.TryEnterReadLock(300))
                {
                    try
                    {
                        return format;
                    }
                    finally
                    {
                        slim.ExitReadLock();
                    }
                }

                return "{0:F3}";
            }

            set
            {
                if (slim.TryEnterWriteLock(500))
                {
                    try
                    {
                        format = value;
                    }
                    finally
                    {
                        slim.ExitWriteLock();
                    }
                }
            }
        }

        /// <summary>
        /// Определяет номер параметра в списке для отрисовки
        /// </summary>
        public int Index
        {
            get
            {
                if (slim.TryEnterReadLock(300))
                {
                    try
                    {
                        return index;
                    }
                    finally
                    {
                        slim.ExitReadLock();
                    }
                }

                return -1;
            }

            set
            {
                if (slim.TryEnterWriteLock(500))
                {
                    try
                    {
                        index = value;
                    }
                    finally
                    {
                        slim.ExitWriteLock();
                    }
                }
            }
        }

        /// <summary>
        /// Определяет текстовое описание числа
        /// </summary>
        public string Description
        {
            get
            {
                if (slim.TryEnterReadLock(300))
                {
                    try
                    {
                        return d_value;
                    }
                    finally
                    {
                        slim.ExitReadLock();
                    }                    
                }

                return string.Empty;
            }

            set
            {
                if (slim.TryEnterWriteLock(500))
                {
                    try
                    {
                        d_value = value;
                    }
                    finally
                    {
                        slim.ExitWriteLock();
                    }
                }
            }
        }

        /// <summary>
        /// Определяет текущее значение
        /// </summary>
        public float Current
        {
            get
            {
                if (slim.TryEnterReadLock(300))
                {
                    try
                    {
                        return c_value;
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
                        c_value = value;
                    }
                    finally
                    {
                        slim.ExitWriteLock();
                    }
                }
            }
        }

        /// <summary>
        /// Возвращяет отформатированное значение числа
        /// </summary>
        public string FormatedValue
        {
            get
            {
                if (slim.TryEnterReadLock(300))
                {
                    try
                    {
                        float Val = c_value;
                        if (!float.IsNaN(Val) && !float.IsInfinity(Val) &&
                                !float.IsNegativeInfinity(Val) && !float.IsPositiveInfinity(Val))
                        {
                            return string.Format(format, Val);
                        }
                        else
                            return "-----";
                    }
                    finally
                    {
                        slim.ExitReadLock();
                    }
                }

                return string.Empty;
            }
        }
    }
}