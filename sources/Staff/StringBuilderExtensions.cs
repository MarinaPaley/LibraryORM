// <copyright file="StringBuilderExtensions.cs" company="Филипченко Марина Алексеевна">
// Copyright (c) Филипченко Марина Алексеевна 2026. Library.
// </copyright>

namespace Staff
{
    using System.Text;

    /// <summary>
    /// Класс-расшмрение для работы со построителями строк.
    /// </summary>
    public static class StringBuilderExtensions
    {
        /// <summary>
        /// Метод-расширение добавление текста с условием.
        /// </summary>
        /// <param name="buffer">Целевой буффер.</param>
        /// <param name="condition">Условие добавление текста.</param>
        /// <param name="text">Добавляемый текст.</param>
        /// <returns>Буффер с добавленным текстом.</returns>
        public static StringBuilder AppendIf(
            this StringBuilder buffer,
            bool condition,
            string text)
        {
            if (condition)
            {
                _ = buffer.Append(text);
            }

            return buffer;
        }
    }
}
