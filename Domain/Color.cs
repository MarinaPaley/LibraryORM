// <copyright file="Color.cs" company="Филипченко Марина Алексеевна">
// Copyright (c) Филипченко Марина Алексеевна 2026. Library.
// </copyright>

namespace Domain
{
    using System;
    using Domain.Abstract;

    /// <summary>
    /// Цвет.
    /// </summary>
    public sealed class Color : NamedEntity<Color>, IEquatable<Color>
    {
        /// <summary>
        /// Инициализирует новый экземпляр класса <see cref="Color"/>.
        /// </summary>
        /// <param name="name"> Название цвета. </param
        /// <param name="code"> Код цвета. </param>
        public Color(string name, ColorCode code)
            : base(name)
        {
            this.Code = code;
        }

#pragma warning disable CS8618 // Необходимо для работы с обязательными полями, получаемыми не через конструктор.
        /// <summary>
        /// Инициализирует новый экземпляр класса <see cref="Color"/>.
        /// </summary>
        [Obsolete("For ORM only", true)]
        private Color()
            : base("Не задано")
        {
        }
#pragma warning restore CS8618

        /// <summary>
        /// Категория.
        /// </summary>
        public Category Category { get; set; }

        /// <summary>
        /// HEX код.
        /// </summary>
        public ColorCode Code { get; set; }

        /// <inheritdoc/>
        public override bool Equals(Color? other)
        {
            return ReferenceEquals(this, other)
                || NamedEntityComparer<Color>.Instance.Equals(this, other);
        }

        /// <inheritdoc/>
        public override bool Equals(object? obj) => this.Equals(obj as Color);

        /// <inheritdoc/>
        public override int GetHashCode() => this.Name.GetHashCode();

        /// <inheritdoc/>
        public override string ToString() => this.Name.ToString();
    }
}
