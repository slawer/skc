using System;
using System.Drawing;

namespace GraphicComponent
{
    /// <summary>
    /// Определяет интерфей который реализует все компоненты графического элемента
    /// </summary>
    public interface IElement
    {
        /// <summary>
        /// Возвращяет родительский элемент для текущего компонента
        /// </summary>
        IElement Parent { get; }

        /// <summary>
        /// Возвращяет графический объект ассоциированный с областью отрисовки компонента
        /// </summary>
        GraphicDrawter Drawter { get; }

        /// <summary>
        /// Возвращяет размер области родительского элемента
        /// </summary>
        RectangleF ClientRectangle { get; }

        /// <summary>
        /// Определяет размер одной ячейки в пикселах
        /// </summary>
        float GridHeight { get; set; }

        /// <summary>
        /// Определяет интервал времени отображаемый в одной ячейке
        /// </summary>
        TimeSpan IntervalInCell { get; set; }

        /// <summary>
        /// Определяет базовую величину ширины
        /// </summary>
        float BaseWidth { get; set; }
        
        /// <summary>
        /// Определяет базовую величину высоты
        /// </summary>
        float BaseHeight { get; set; }

        /// <summary>
        /// Определяет базовое значение коэффициента для вычисления ширины
        /// </summary>
        float WidthCoef { get; set; }

        /// <summary>
        /// Определяет базовое значение коэффициента для вычисления высоты
        /// </summary>
        float HeightCoef { get; set; }

        /// <summary>
        /// Определяет количество делений сетки
        /// </summary>
        int GradCount { get; set; }

        /// <summary>
        /// Возвращяет список графиков
        /// </summary>
        Graphic[] Graphics { get; }

        /// <summary>
        /// Определяет цвет панели
        /// </summary>
        Color Color { get; set; }

        /// <summary>
        /// Определяет размер надписи шкалы времени
        /// </summary>
        SizeF TimeLabelSize { get; set; }

        /// <summary>
        /// Определяет размер области вывода шкалы времени
        /// </summary>
        SizeF TimeAreaSizeF { get; set; }

        /// <summary>
        /// Размер области в которую выводятся линии шкал графиков
        /// </summary>
        RectangleF ScaleLineSize { get; set; }

        /// <summary>
        /// определяет каким образом отрисовывать график
        /// </summary>
        Orientation Orientation { get; set; }

        /// <summary>
        /// Определяет стартовое время отображаемое на панели
        /// </summary>
        DateTime StartTime { get; set; }

        /// <summary>
        /// Определяет конечное время отображаемое на панели
        /// </summary>
        DateTime FinishTime { get; set; }

        /// <summary>
        /// Определяет фактический размер области вывода меток времени
        /// </summary>
        SizeF ActualTimeSize { get; set; }

        /// <summary>
        /// Делает недействительной всю поверхность элемента управления
        /// и вызывает его перерисовку.
        /// </summary>
        void InvalidatePanel();

        /// <summary>
        /// Делает недействительной всю поверхность элемента управления
        /// и вызывает его перерисовку. при отрисовки по таймеру.
        /// </summary>
        void InvalidatePanelTimer(DateTime currentTime);

        /// <summary>
        /// Инициализировать панель
        /// </summary>
        void InitializePanel();
    }
}