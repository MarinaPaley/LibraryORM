// <copyright file="Range.cs" company="Филипченко Марина Алексеевна">
// Copyright (c) Филипченко Марина Алексеевна 2026. Library.
// </copyright>

#pragma warning disable SA1507 // Code should not contain multiple blank lines in a row
#pragma warning disable SA1515 // Single-line comment should be preceded by blank line
// Ignore Spelling: Satelles сериализованного
#pragma warning restore SA1515
#pragma warning restore SA1507

namespace Staff
{
    using System;

    /// <summary>
    /// Диапазон.
    /// </summary>
    /// <typeparam name="T"> Тип значений диапазона. </typeparam>
    public sealed class Range<T> : IEquatable<Range<T>>
        where T : struct, IComparable<T>
    {
        /// <summary>
        /// Левая включенная граница сериализованного представления.
        /// </summary>
        public static readonly char LeftIncludeBorder = '[';

        /// <summary>
        /// Правая исключенная граница сериализованного представления.
        /// </summary>
        public static readonly char RightExcludeBorder = ')';

        /// <summary>
        /// Левая исключенная граница сериализованного представления.
        /// </summary>
        public static readonly char LeftExcludeBorder = '(';

        /// <summary>
        /// Правая включенная граница сериализованного представления.
        /// </summary>
        public static readonly char RightIncludeBorder = ']';

        /// <summary>
        /// Знак бесконечности (∞).
        /// </summary>
        public static readonly char Infinity = '\u221e';

        /// <summary>
        /// Отрицательная бесконечность.
        /// </summary>
        public static readonly string LeftInfinity = $"-{Infinity}";

        /// <summary>
        /// Положительная бесконечность.
        /// </summary>
        public static readonly string RightInfinity = $"+{Infinity}";

        /// <summary>
        /// Разделитель для сериализованного представления.
        /// </summary>
        public static readonly string Separator = "; ";

        /// <summary>
        /// Инициализирует новый экземпляр класса <see cref="Range{T}"/>.
        /// </summary>
        /// <param name="from"> Начало диапазона. </param>
        /// <param name="to"> Конец диапазона. </param>
        public Range(T? from, T? to)
        {
            if (from.HasValue && to.HasValue && from.Value.CompareTo(to.Value) > 0)
            {
                throw new ArgumentException($"Parameter {nameof(from)} have not to be greater than {nameof(to)}.", nameof(from));
            }

            this.From = from;
            this.To = to;
        }

        /// <summary>
        /// Начало диапазона ("от").
        /// </summary>
        public T? From { get; private set; }

        /// <summary>
        /// Конец диапазона ("до").
        /// </summary>
        public T? To { get; private set; }

        /// <summary>
        /// Проверяет (не исключительную) принадлежность значения <paramref name="value"/> текущему диапазону.
        /// </summary>
        /// <param name="value"> Проверяемое значение. </param>
        /// <returns>
        /// <see langword="true"/> в случае если значение принадлежит диапазону (включительно),
        /// <see langword="false"/> в противном случае.
        /// </returns>
        public bool Contains(T value)
        {
            var result = true;

            if (this.From.HasValue)
            {
                result &= this.From.Value.CompareTo(value) <= 0;
            }

            if (this.To.HasValue)
            {
                result &= this.To.Value.CompareTo(value) >= 0;
            }

            return result;
        }

        /// <inheritdoc/>
        public override bool Equals(object? obj)
        {
            var other = obj as Range<T>;
            return other is not null
                && this.Equals(other);
        }

        /// <inheritdoc/>
        public bool Equals(Range<T>? other)
        {
            if (ReferenceEquals(other, this))
            {
                return true;
            }

            return other is not null
                && Nullable.Compare(this.From, other.From) == 0
                && Nullable.Compare(this.To, other.To) == 0;
        }

        /// <inheritdoc/>
        public override int GetHashCode() => HashCode.Combine(this.From, this.To);

        /// <inheritdoc cref="object.ToString"/>
        public override string ToString()
        {
            var result = this.From.HasValue
                ? $"{LeftIncludeBorder}{this.From}"
                : $"{LeftExcludeBorder}{LeftInfinity}";

            result += Separator;

            result += this.To.HasValue
                ? $"{this.To}{RightIncludeBorder}"
                : $"{RightInfinity}{RightExcludeBorder}";

            return result;
        }
    }
}