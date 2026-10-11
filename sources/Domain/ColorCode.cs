// <copyright file="ColorCode.cs" company="Филипченко Марина Алексеевна">
// Copyright (c) Филипченко Марина Алексеевна 2026. Library.
// </copyright>

namespace Domain
{
    using System;
    using System.Text.RegularExpressions;

    /// <summary>
    /// Код цвета в HEX-формате (#RRGGBB или #RRGGBBAA).
    /// </summary>
    public sealed partial class ColorCode : IEquatable<ColorCode>
    {
        /// <summary>
        /// Инициализирует новый экземпляр класса <see cref="ColorCode"/>.
        /// </summary>
        /// <param name="value"> HEX-код цвета (например, "#FF0000" или "#FF0000FF"). </param>
        /// <exception cref="ArgumentException"> Если формат некорректен. </exception>
        public ColorCode(string value)
        {
            if (string.IsNullOrWhiteSpace(value))
            {
                throw new ArgumentException("Код цвета не может быть пустым.", nameof(value));
            }

            if (!IsHexColorValid(value))
            {
                throw new ArgumentException(
                    $"Некорректный HEX-формат цвета: '{value}'. Ожидается #RGB, #RGBA, #RRGGBB или #RRGGBBAA.",
                    nameof(value));
            }

            // Нормализуем к верхнему регистру для единообразия (каноническая форма)
            this.Value = value.ToUpperInvariant();
        }

        /// <summary>
        /// HEX-код цвета.
        /// </summary>
        public string Value { get; }

        /// <summary>
        /// Определяет равенство двух экземпляров <see cref="ColorCode"/>.
        /// </summary>
        /// <param name="left"> Левый операнд. </param>
        /// <param name="right"> Правый операнд. </param>
        public static bool operator ==(ColorCode? left, ColorCode? right) => Equals(left, right);

        /// <summary>
        /// Определяет неравенство двух экземпляров <see cref="ColorCode"/>.
        /// </summary>
        /// /// <param name="left"> Левый операнд. </param>
        /// <param name="right"> Правый операнд. </param>
        public static bool operator !=(ColorCode? left, ColorCode? right) => !Equals(left, right);

        /// <inheritdoc/>
        public override bool Equals(object? obj) => this.Equals(obj as ColorCode);

        /// <inheritdoc/>
        public bool Equals(ColorCode? other)
        {
            if (other is null)
            {
                return false;
            }

            if (ReferenceEquals(this, other))
            {
                return true;
            }

            return string.Equals(this.Value, other.Value, StringComparison.Ordinal);
        }

        /// <inheritdoc/>
        public override int GetHashCode() => StringComparer.Ordinal.GetHashCode(this.Value);

        /// <inheritdoc/>
        public override string ToString() => this.Value;

        /// <summary>
        /// Проверяет валидность HEX-строки с помощью сгенерированного регулярного выражения.
        /// </summary>
        [GeneratedRegex(@"^#([A-Fa-f0-9]{3,4}|[A-Fa-f0-9]{6,8})$", RegexOptions.IgnoreCase | RegexOptions.Compiled)]
        private static partial Regex HexColorRegex();

        private static bool IsHexColorValid(string input) => HexColorRegex().IsMatch(input);
    }
}