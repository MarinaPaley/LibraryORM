// <copyright file="Category.cs" company="Филипченко Марина Алексеевна">
// Copyright (c) Филипченко Марина Алексеевна 2026. Library.
// </copyright>

namespace Domain
{
    using System;
    using System.Collections.Generic;
    using Domain.Abstract;

    /// <summary>
    /// Категория.
    /// </summary>
    public sealed class Category : BilingualNamedEntity<Category>, IEquatable<Category>
    {
        /// <summary>
        /// Инициализирует новый экземпляр класса <see cref="Category"/>.
        /// </summary>
        /// <param name="name"> Категория. </param>
        /// <param name="color"> Название цвета.</param>
        /// <param name="origin"> Оригинальное название категории. </param>
        public Category(string name, Color color, string? origin = null)
            : base(name, origin)
        {
            ArgumentNullException.ThrowIfNull(color);
            this.Color = color;
            this.Color.Category = this;
        }

#pragma warning disable CS8618 // Необходимо для работы с обязательными полями, получаемыми не через конструктор.

        /// <summary>
        /// Инициализирует новый экземпляр класса <see cref="Category"/>.
        /// </summary>
        [Obsolete("For ORM only", true)]
        private Category()
            : base("Не задано")
        {
        }
#pragma warning restore CS8618

        /// <summary>
        /// Подкатегории.
        /// </summary>
        public ISet<Tag> Tags { get; set; } = new HashSet<Tag>(BilingualNamedEntityComparer<Tag>.Instance);

        /// <summary>
        /// Цвет.
        /// </summary>
        public Color Color { get; set; }

        /// <inheritdoc/>
        public override bool Equals(Category? other)
        {
            return ReferenceEquals(this, other)
                || NamedEntityComparer<Category>.Instance.Equals(this, other);
        }

        /// <inheritdoc/>
        public override bool Equals(object? obj) => this.Equals(obj as Category);

        /// <inheritdoc/>
        public override int GetHashCode() => this.Name.GetHashCode();

        /// <inheritdoc/>
        public override string ToString() => this.Name.ToString();
    }
}
