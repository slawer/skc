using System;
using System.Threading;
using System.Collections.Generic;

using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;

namespace Buffering
{
    /// <summary>
    /// Реализует кольцевой буффер при буферризации данных
    /// </summary>
    [Serializable]
    public class RSliceBuffer
    {
        protected int last;                     // индекс последней заполненной строки
        protected int first;                    // индекс первой заполненной строки

        protected int size;                     // размер массива
        protected int c_size;                   // текущий размер

        protected Slice[] array;                // массив данных

        [NonSerialized]
        protected ReaderWriterLockSlim slim;    // синхронизатор

        /// <summary>
        /// Инициализирует новый экземпляр класса
        /// </summary>        
        public RSliceBuffer()
        {
            slim = new ReaderWriterLockSlim(LockRecursionPolicy.SupportsRecursion);
            
            last = -1;
            first = 0;

            size = 72000;
            c_size = 0;

            array = new Slice[size];
            for (int index = 0; index < array.Length; index++)
            {
                array[index] = default(Slice);
            }
        }

        /// <summary>
        /// Инициализирует новый экземпляр класса
        /// </summary>
        /// <param name="Lenght">Длинна буфера</param>
        public RSliceBuffer(int Lenght)
        {
            if (Lenght > 0)
            {
                slim = new ReaderWriterLockSlim(LockRecursionPolicy.SupportsRecursion);

                last = -1;
                first = 0;

                size = Lenght;
                c_size = 0;

                array = new Slice[size];
                for (int index = 0; index < array.Length; index++)
                {
                    array[index] = default(Slice);
                }
            }
            else
            {
                throw new ArgumentException();
            }
        }

        /// <summary>
        /// Добавить элемент в буфер
        /// </summary>
        /// <param name="Element">Добавляемый элемент</param>
        public void Append(Slice Element)
        {
            if (slim.TryEnterWriteLock(555))
            {
                try
                {
                    if (Element.slice != null)
                    {
                        CalculateIndexes();

                        array[last] = Element;
                        array[last].index = last;
                    }
                }
                finally
                {
                    slim.ExitWriteLock();
                }
            }
        }

        /// <summary>
        /// Очистить буфер
        /// </summary>
        public void Clear()
        {
            if (slim.TryEnterWriteLock(500))
            {
                try
                {
                    last = -1;
                    first = 0;
                    c_size = 0;
                    for (int index = 0; index < array.Length; index++)
                    {
                        array[index] = default(Slice);
                    }
                }
                finally
                {
                    slim.ExitWriteLock();
                }
            }
        }

        [OnDeserialized]
        internal void OnDeserializedMethod(StreamingContext context)
        {
            slim = new ReaderWriterLockSlim(LockRecursionPolicy.SupportsRecursion);
        }

        /// <summary>
        /// Получить срезы данных за указанный период времени с начала массива
        /// </summary>
        /// <param name="start">Время начало выборки данных</param>
        /// <param name="finish">Время конца выборки данных</param>
        /// <returns>Список данных из указанного диапазона</returns>
        public Slice[] FindFromBegin(DateTime start, DateTime finish)
        {
            if (slim.TryEnterReadLock(300))
            {
                try
                {
                    if (last >= 0)
                    {
                        if (array[last]._date < start) return null;
                        if (array[first]._date > finish) return null;

                        int ind1 = -1;
                        {
                            int ind = first;
                            for (int j = 0; j < c_size; j++)
                            {
                                if (array[ind]._date < start)
                                {
                                    if (ind == last) break;
                                    ind = ind + 1;

                                    if (ind >= size) ind = 0;
                                }
                                else
                                {
                                    ind1 = ind;
                                    break;
                                }
                            }
                        }

                        if (ind1 != -1)
                        {
                            List<Slice> result = new List<Slice>();
                            if (result != null)
                            {
                                int ind = ind1;

                                for (int j = 0; j < c_size; j++)
                                {
                                    if (array[ind]._date > finish)
                                    {
                                        break;
                                    }

                                    // добавить элемент к result
                                    if (array[ind].slice != null)
                                    {
                                        result.Add(array[ind]);
                                    }

                                    if (ind == last) break;

                                    ind++;
                                    if (ind >= size) ind = 0;
                                }
                            }

                            return result.ToArray();
                        }
                    }
                }
                finally
                {
                    slim.ExitReadLock();
                }
            }
            return null;
        }

        /// <summary>
        /// Получить параметры за указанный период времени с начала массива
        /// </summary>
        /// <param name="start">Время начало выборки данных</param>
        /// <param name="finish">Время конца выборки данных</param>
        /// <param name="p1, p2, p3, p4, p5">Номера параметров от 0 до №-1</param>
        /// <returns>Список данных из указанного диапазона</returns>
        public Slice[] FindFromBegin(DateTime start, DateTime finish, int p1, int p2, int p3, int p4, int p5)
        {
            if (slim.TryEnterReadLock(300))
            {
                try
                {
                    int[] Prms = { p1, p2, p3, p4, p5 };
                    if (last >= 0)
                    {
                        if (array[last]._date < start) return null;
                        if (array[first]._date > finish) return null;

                        int ind1 = -1;
                        {
                            int ind = first;
                            for (int j = 0; j < c_size; j++)
                            {
                                if (array[ind]._date < start)
                                {
                                    if (ind == last) break;
                                    ind = ind + 1;

                                    if (ind >= size) ind = 0;
                                }
                                else
                                {
                                    ind1 = ind;
                                    break;
                                }
                            }
                        }

                        if (ind1 != -1)
                        {
                            List<Slice> result = new List<Slice>(5);
                            if (result != null)
                            {
                                int ind = ind1;

                                for (int j = 0; j < c_size; j++)
                                {
                                    if (array[ind]._date > finish)
                                    {
                                        break;
                                    }

                                    // добавить элемент к result
                                    if (array[ind].slice != null)
                                    {
                                        Slice buff = new Slice(5);
                                        buff._date = array[ind]._date;
                                        for (int jPrms = 0; j < Prms.Length; jPrms++)
                                        {
                                            if (Prms[jPrms] >= 0)
                                            {
                                                buff.slice[jPrms] = array[ind].slice[Prms[jPrms]];
                                            }
                                            else
                                            {
                                                buff.slice[jPrms] = float.NaN;
                                            }
                                        }
                                        result.Add(buff);
                                        // result.Add(array[ind]);
                                    }

                                    if (ind == last) break;

                                    ind++;
                                    if (ind >= size) ind = 0;
                                }
                            }

                            return result.ToArray();
                        }
                    }
                }
                finally
                {
                    slim.ExitReadLock();
                }
            }
            return null;
        }

        /// <summary>
        /// Получить срезы данных за указанный период времени с конца массива
        /// </summary>
        /// <param name="start">Время начало выборки данных</param>
        /// <param name="finish">Время конца выборки данных</param>
        /// <returns>Список данных из указанного диапазона</returns>
        public Slice[] FindFromEnd(DateTime start, DateTime finish)
        {
            if (slim.TryEnterReadLock(300))
            {
                try
                {
                    if (last >= 0)
                    {
                        if (array[last]._date < start) return null;
                        if (array[first]._date > finish) return null;

                        int ind1 = -1;
                        {
                            int ind = last;
                            for (int j = 0; j < c_size; j++)
                            {
                                if (array[ind]._date > finish)
                                {
                                    if (ind == first) break;
                                    ind = ind - 1;

                                    if (ind < 0) ind = size - 1;
                                }
                                else
                                {
                                    ind1 = ind;
                                    break;
                                }
                            }
                        }

                        if (ind1 != -1)
                        {
                            List<Slice> result = new List<Slice>();
                            if (result != null)
                            {
                                int ind = ind1;
                                for (int j = 0; j < c_size; j++)
                                {
                                    if (array[ind]._date < start)
                                    {
                                        break;
                                    }

                                    // добавить элемент к result
                                    if (array[ind].slice != null)
                                    {
                                        result.Add(array[ind]);
                                    }

                                    if (ind == first) break;

                                    ind = ind - 1;
                                    if (ind < 0) ind = size - 1;
                                }
                            }

                            result.Reverse();
                            return result.ToArray();
                        }
                    }
                }
                finally
                {
                    slim.ExitReadLock();
                }
            }
            return null;
        }

        /// <summary>
        /// Получить параметры за указанный период времени с конца массива
        /// </summary>
        /// <param name="start">Время начало выборки данных</param>
        /// <param name="finish">Время конца выборки данных</param>
        /// <param name="p1, p2, p3, p4, p5">Номера параметров от 0 до №-1</param>
        /// <returns>Список данных из указанного диапазона</returns>
        public Slice[] FindFromEnd(DateTime start, DateTime finish, int p1, int p2, int p3, int p4, int p5)
        {
            if (slim.TryEnterReadLock(300))
            {
                try
                {
                    int[] Prms = { p1, p2, p3, p4, p5 };
                    if (last >= 0)
                    {
                        if (array[last]._date < start) return null;
                        if (array[first]._date > finish) return null;

                        int ind1 = -1;
                        {
                            int ind = last;
                            for (int j = 0; j < c_size; j++)
                            {
                                if (array[ind]._date > finish)
                                {
                                    if (ind == first) break;
                                    ind = ind - 1;

                                    if (ind < 0) ind = size - 1;
                                }
                                else
                                {
                                    ind1 = ind;
                                    break;
                                }
                            }
                        }

                        if (ind1 != -1)
                        {
                            List<Slice> result = new List<Slice>(5);
                            if (result != null)
                            {
                                int ind = ind1;
                                for (int j = 0; j < c_size; j++)
                                {
                                    if (array[ind]._date < start)
                                    {
                                        break;
                                    }

                                    // добавить элемент к result
                                    if (array[ind].slice != null)
                                    {
                                        Slice buff = new Slice(5);
                                        buff._date = array[ind]._date;
                                        for (int jPrms = 0; jPrms < Prms.Length; jPrms++)
                                        {
                                            if (Prms[jPrms] >= 0)
                                            {
                                                buff.slice[jPrms] = array[ind].slice[Prms[jPrms]];
                                            }
                                            else
                                            {
                                                buff.slice[jPrms] = float.NaN;
                                            }
                                        }
                                        result.Add(buff);
                                        // result.Add(array[ind]);
                                    }

                                    if (ind == first) break;

                                    ind = ind - 1;
                                    if (ind < 0) ind = size - 1;
                                }
                            }

                            result.Reverse();
                            return result.ToArray();
                        }
                    }
                }
                finally
                {
                    slim.ExitReadLock();
                }
            }
            return null;
        }

        // -----------

        /// <summary>
        /// Вычислить индексы
        /// </summary>
        protected void CalculateIndexes()
        {
            if (last < 0)
            {
                last = 0;
                first = 0;

                c_size = 1;
            }
            else
            {
                last = last + 1;
                if (last == size) last = 0;

                if (last == first)
                {
                    first = first + 1;
                    if (first == size) first = 0;
                }
                else
                    c_size = c_size + 1;
            }
        }

        /// <summary>
        /// Получить минимальное значение параметра в массиве данных
        /// </summary>
        /// <param name="number">Номер параметра</param>
        /// <returns>Время параметра</returns>
        public DateTime GetMinTime()
        {
            try
            {
                if (first > -1 && first < array.Length)
                {
                    return array[first]._date;
                }
            }
            catch { }
            return DateTime.MinValue;
        }
    }
}