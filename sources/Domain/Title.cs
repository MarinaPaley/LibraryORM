// <copyright file="Title.cs" company="Филипченко Марина Алексеевна">
// Copyright (c) Филипченко Марина Алексеевна 2026. Library.
// </copyright>

namespace Domain
{
    using System;
    using Staff;

    /// <summary>
    /// Value Object Название.
    /// </summary>
    public sealed class Title : IEquatable<Title>
    {
        /// <summary>
        /// Инициализирует новый экземпляр класса <see cref="Title"/>.
        /// </summary>
        /// <param name="value"> Значение. </param>
        /// <exception cref="ArgumentNullException">
        /// В случае если <paramref name="value"/> – <see langword="null"/>.
        /// </exception>
        public Title(string value)
        {
            this.Value = value;
        }

        /// <summary>
        /// Значение.
        /// </summary>
        /// <exception cref="ArgumentNullException">
        /// В случае если входное значение <see langword="null"/>.
        /// </exception>
        public string Value
        {
            get => field;
            private set => field = value.TrimOrNull() ?? throw new ArgumentNullException(nameof(value));
        }

        /// <summary>
        /// Оператор равенства.
        /// </summary>
        /// <param name="lha"> Левый операнд. </param>
        /// <param name="rha"> Правый операнд. </param>
        /// <returns>  В случае равенства – <see langword="true"/>. </returns>
        public static bool operator ==(Title? lha, Title? rha) => Equals(lha, rha);

        /// <summary>
        /// Оператор неравенства.
        /// </summary>
        /// <param name="lha"> Левый операнд.</param>
        /// <param name="rha"> Правый операнд.</param>
        /// <returns> В случае неравенства – <see langword="true"/>. </returns>
        public static bool operator !=(Title? lha, Title? rha) => !Equals(lha, rha);

        /// <inheritdoc/>
        public bool Equals(Title? other)
        {
            return ReferenceEquals(this, other)
                || ((other is not null) && (this.Value == other.Value));
        }

        /// <inheritdoc/>
        public override bool Equals(object? obj) => this.Equals(obj as Title);

        /// <inheritdoc/>
        public override int GetHashCode() => this.Value.GetHashCode();

        /// <inheritdoc cref="object.ToString()"/>
        public override string ToString() => this.Value;
    }
}
