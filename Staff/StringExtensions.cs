// <copyright file="StringExtensions.cs" company="Васильева Марина Алексеевна">
// Copyright (c) Васильева Марина Алексеевна 2024. Library.
// </copyright>

namespace Staff
{
    /// <summary>
    /// Расширение для строкового типа.
    /// </summary>
    public static class StringExtensions
    {
        /// <summary>
        /// Обертка над <see cref="string.IsNullOrEmpty(string?)"/>.
        /// </summary>
        /// <param name="value">  Параметр. </param>
        /// <returns> <see langword="true"/> если параметр
        /// <see langword="null"/>или пустая строка. </returns>
        public static bool IsNullOrEmpty(this string? value) => string.IsNullOrEmpty(value);

        /// <summary>
        /// Возвращает или без начальных и конечных пробелов строку, или <see langword="null"/>.
        /// </summary>
        /// <param name="value"> Строка.</param>
        /// <returns> Или без начальных и конечных пробелов строка, или <see langword="null"/>.</returns>
        public static string? TrimOrNull(this string value)
        {
            var trimmed = value?.Trim();
            return trimmed.IsNullOrEmpty()
                 ? null
                 : trimmed;
        }
    }
}