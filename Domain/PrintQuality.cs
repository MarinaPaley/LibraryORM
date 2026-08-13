// <copyright file="PrintQuality.cs" company="Филипченко Марина Алексеевна">
// Copyright (c) Филипченко Марина Алексеевна 2026. Library.
// </copyright>

namespace Domain
{
    /// <summary>
    /// Качество печати книги.
    /// </summary>
    public enum PrintQuality
    {
        /// <summary>
        /// Типографское качество (по умолчанию).
        /// </summary>
        PrintingHouse = 0,

        /// <summary>
        /// Самиздат.
        /// </summary>
        SelfPublished = 1,

        /// <summary>
        /// Рукопись.
        /// </summary>
        Manuscript = 2,
    }
}