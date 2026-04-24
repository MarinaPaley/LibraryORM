// <copyright file="Publisher.cs" company="Филипченко Марина Алексеевна">
// Copyright (c) Филипченко Марина Алексеевна 2026. Library.
// </copyright>

namespace Domain
{
    using System;
    using System.Collections.Generic;
    using Domain.Abstract;

    /// <summary>
    /// Издательство.
    /// </summary>
    public sealed class Publisher : Entity<Publisher>, IEquatable<Publisher>
    {
        /// <summary>
        /// Инициализирует новый экземпляр класса <see cref="Publisher"/>.
        /// </summary>
        /// <param name="name"> Название. </param>
        /// <param name="address"> Адрес. </param>
        public Publisher(string name, Address? address = null)
        {
            this.Name = new Title(name);
            this.Address = address;
        }

        [Obsolete("For ORM only")]
        private Publisher()
        {
        }

        /// <summary>
        /// Название.
        /// </summary>
        public Title Name { get; set; }

        /// <summary>
        /// Адрес.
        /// </summary>
        public Address? Address { get; set; }

        /// <summary>
        /// Книги.
        /// </summary>
        public ISet<Book> Books { get; } = new HashSet<Book>();

        /// <inheritdoc/>
        public override bool Equals(Publisher? other)
        {
            return ReferenceEquals(this, other) || ((other is not null) && (this.Id == other.Id));
        }

        /// <inheritdoc/>
        public override bool Equals(object? obj) => this.Equals(obj as Publisher);

        /// <inheritdoc/>
        public override int GetHashCode() => this.Id.GetHashCode();

        /// <inheritdoc/>
        public override string ToString() => $"{this.Name}";

        /// <summary>
        /// Добавить книгу.
        /// </summary>
        /// <param name="book"> Книга.</param>
        /// <returns> <see langword="true"/>, если добавили, иначе - <see langword="false"/>.</returns>
        public bool AddBook(Book book)
        {
            return book is not null
                && this.Books.Add(book)
                && book.Publishers.Add(this);
        }

        /// <summary>
        /// Удаление книги из серии.
        /// </summary>
        /// <param name="book"> Книга.</param>
        /// <returns> <see langword="true"/>, если удалили, иначе - <see langword="false"/>.</returns>
        public bool RemoveBook(Book book)
        {
            return book is not null
                && this.Books.Remove(book)
                && book.Publishers.Remove(this);
        }
    }
}
