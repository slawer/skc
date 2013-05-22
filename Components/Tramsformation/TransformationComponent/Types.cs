using System;

namespace DeviceManager
{
    /// <summary>
    /// Определяет что выводить на график
    /// </summary>
    public enum Draw
    {
        /// <summary>
        /// Только точку
        /// </summary>
        PointOnly,

        /// <summary>
        /// Только график
        /// </summary>
        PointsOnly,

        /// <summary>
        /// Точку и график
        /// </summary>
        PointAndPoints,

        /// <summary>
        /// Ничего не рислвать
        /// </summary>
        Nothing
    }
}