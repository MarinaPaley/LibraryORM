// <copyright file="Tag.cs" company="Филипченко Марина Алексеевна">
// Copyright (c) Филипченко Марина Алексеевна 2026. Library.
// </copyright>

namespace Domain
{
    using System;
    using System.Collections.Generic;
    using Domain.Abstract;

    /// <summary>
    /// Тег.
    /// </summary>
    public sealed class Tag : BilingualNamedEntity<Tag>, IEquatable<Tag>
    {
        /// <summary>
        /// Инициализирует новый экземпляр класса <see cref="Tag"/>.
        /// </summary>
        /// <param name="name"> Название тега.</param>
        /// <param name="category"> Категория. </param>
        /// <param name="origin"> Оригинальное название тега. </param>
        public Tag(string name, Category category, string? origin = null)
            : base(name, origin)
        {
            ArgumentNullException.ThrowIfNull(category);
            this.Category = category;
            var result = this.Category.Tags.Add(this);
            if (!result)
            {
                throw new ArgumentException(null, nameof(category));
            }
        }
#pragma warning disable CS8618 // Необходимо для работы с обязательными полями, получаемыми не через конструктор.

        /// <summary>
        /// Инициализирует новый экземпляр класса <see cref="Tag"/>.
        /// </summary>
        [Obsolete("For ORM only", true)]
        private Tag()
            : base("Не задано")
        {
        }
#pragma warning restore CS8618

        /// <summary>
        /// Категория.
        /// </summary>
        public Category Category { get; set; }

        /// <summary>
        /// Книги.
        /// </summary>
        public ISet<Book> Books { get; set; } = new HashSet<Book>(EntityComparer<Book>.Instance);

        /// <inheritdoc/>
        public override bool Equals(Tag? other)
        {
            return ReferenceEquals(this, other)
                || NamedEntityComparer<Tag>.Instance.Equals(this, other);
        }

        /// <inheritdoc/>
        public override bool Equals(object? obj) => this.Equals(obj as Tag);

        /// <inheritdoc/>
        public override int GetHashCode() => this.Name.GetHashCode();

        /// <inheritdoc/>
        public override string ToString() => this.Name.ToString();
    }
}
