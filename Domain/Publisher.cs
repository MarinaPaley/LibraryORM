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
    public sealed class Publisher : BilingualNamedEntity<Publisher>, IEquatable<Publisher>
    {
        /// <summary>
        /// Инициализирует новый экземпляр класса <see cref="Publisher"/>.
        /// </summary>
        /// <param name="name"> Название. </param>
        /// <param name="address"> Адрес. </param>
        public Publisher(string name, Address? address = null)
            : base(name)
        {
            this.Address = address;
        }

#pragma warning disable CS8618 // Необходимо для работы с обязательными полями, получаемыми не через конструктор.
        [Obsolete("For ORM only")]
        private Publisher()
            : base("Не задано")
        {
        }
#pragma warning restore CS8618

        /// <summary>
        /// Адрес.
        /// </summary>
        public Address? Address { get; set; }

        /// <summary>
        /// Книги.
        /// </summary>
        public ISet<Book> Books { get; } = new HashSet<Book>(EntityComparer<Book>.Instance);

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

        /// <inheritdoc/>
        public override bool Equals(object? obj) => this.Equals(obj as Publisher);

        /// <inheritdoc/>
        public override bool Equals(Publisher? other)
        {
            return ReferenceEquals(this, other)
                || BilingualNamedEntityComparer<Publisher>.Instance.Equals(this, other);
        }

        /// <inheritdoc/>
        public override int GetHashCode() => HashCode.Combine(this.Name, this.OriginName);
    }
}
