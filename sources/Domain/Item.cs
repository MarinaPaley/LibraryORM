// <copyright file="Item.cs" company="Филипченко Марина Алексеевна">
// Copyright (c) Филипченко Марина Алексеевна 2026. Library.
// </copyright>

namespace Domain
{
    using System;
    using Domain.Abstract;

    /// <summary>
    /// Экземпляр книги.
    /// </summary>
    public sealed class Item : Entity<Item>, IEquatable<Item>
    {
        /// <summary>
        /// Инициализирует новый экземпляр класса <see cref="Item"/>.
        /// </summary>
        /// <param name="book"> Книга. </param>
        /// <exception cref="ArgumentNullException"> Если <c> книга</c> <see langword="null"/>. </exception>
        public Item(Book book)
        {
            this.Book = book ?? throw new ArgumentNullException(nameof(book));
            book.Items.Add(this);
        }

#pragma warning disable CS8618 // Необходимо для работы с обязательными полями, получаемыми не через конструктор.
        /// <summary>
        /// Инициализирует новый экземпляр класса <see cref="Item"/>.
        /// </summary>
        [Obsolete("For ORM only", true)]
        private Item()
        {
        }
#pragma warning restore CS8618

        /// <summary>
        /// Книга.
        /// </summary>
        public Book Book { get; set; }

        /// <summary>
        /// Полка.
        /// </summary>
        public Shelf? Shelf { get; set; }

        /// <inheritdoc/>
        public override bool Equals(Item? other)
        {
            return ReferenceEquals(this, other)
                || (other is not null
                && this.Book.Equals(other.Book));
        }

        /// <inheritdoc/>
        public override bool Equals(object? obj) => this.Equals(obj as Item);

        /// <inheritdoc/>
        public override int GetHashCode() => this.Book.GetHashCode();

        /// <inheritdoc/>
        public override string ToString() => this.Book.ToString();
    }
}