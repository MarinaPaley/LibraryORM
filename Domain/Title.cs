// <copyright file="Title.cs" company="Филипченко Марина Алексеевна">
// Copyright (c) Филипченко Марина Алексеевна 2026. Library.
// </copyright>

namespace Domain
{
    using System;
    using Staff;

    /// <summary>
    /// Value Object "Название".
    /// </summary>
    public sealed class Title : IEquatable<Title>
    {
        /// <summary>
        /// Инициализирует новый экземпляр класса <see cref="Title"/>.
        /// </summary>
        /// <param name="value"> Значение названия. </param>
        /// <exception cref="ArgumentNullException">
        /// В случае если <paramref name="value"/> равно <see langword="null"/> или состоит только из пробелов.
        /// </exception>
        public Title(string value)
        {
            this.Value = value;
        }

        /// <summary>
        /// Значение названия.
        /// </summary>
        public string Value
        {
            get => field;
            private set => field = value.TrimOrNull() ?? throw new ArgumentNullException(nameof(value));
        }

        /// <summary>
        /// Определяет равенство двух экземпляров <see cref="Title"/>.
        /// </summary>
        /// <param name="left"> Левый операнд. </param>
        /// <param name="right"> Правый операнд. </param>
        /// <returns> <see langword="true"/>, если значения равны; в противном случае — <see langword="false"/>. </returns>
        public static bool operator ==(Title? left, Title? right) => Equals(left, right);

        /// <summary>
        /// Определяет неравенство двух экземпляров <see cref="Title"/>.
        /// </summary>
        /// <param name="left"> Левый операнд. </param>
        /// <param name="right"> Правый операнд. </param>
        /// <returns> <see langword="true"/>, если значения не равны; в противном случае — <see langword="false"/>. </returns>
        public static bool operator !=(Title? left, Title? right) => !Equals(left, right);

        /// <inheritdoc/>
        public bool Equals(Title? other)
        {
            return ReferenceEquals(this, other)
                || (other is not null && this.Value == other.Value);
        }

        /// <inheritdoc/>
        public override bool Equals(object? obj) => this.Equals(obj as Title);

        /// <inheritdoc/>
        public override int GetHashCode() => StringComparer.Ordinal.GetHashCode(this.Value);

        /// <inheritdoc cref="object.ToString()"/>
        public override string ToString() => this.Value;
    }
}