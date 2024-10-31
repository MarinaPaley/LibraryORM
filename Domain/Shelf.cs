// <copyright file="Shelf.cs" company="Васильева Марина Алексеевна">
// Copyright (c) Васильева Марина Алексеевна 2024. Library.
// </copyright>

namespace Domain
{
    using System;
    using System.Collections.Generic;
    using System.Net.Sockets;
    using Staff;

    /// <summary>
    /// Класс Полка.
    /// </summary>
    public sealed class Shelf : IEquatable<Shelf>
    {
        /// <summary>
        /// Инициализирует новый экземпляр класса <see cref="Shelf"/>.
        /// </summary>
        /// <param name="name"> Название полки.</param>
        public Shelf(string name)
        {
            this.Id = Guid.Empty;
            this.Name = name.TrimOrNull() ?? throw new ArgumentNullException(nameof(name));
        }

        /// <summary>
        /// Идентификатор.
        /// </summary>
        public Guid Id { get; }

        /// <summary>
        /// Название полки.
        /// </summary>
        public string Name { get; }

        /// <summary>
        ///  Книги.
        /// </summary>
        public ISet<Book> Books { get; } = new HashSet<Book>();

        /// <summary>
        /// Добавляет книгу на полку.
        /// </summary>
        /// <param name="book">Книга</param>
        /// <returns><see langword="true"/> если добавили.</returns>
        public bool AddBook(Book book)
        {
            if (this.Books.Add(book))
            {
                book.Shelf = this;
                return true;
            }

            return false;
        }

        /// <summary>
        /// Снимаем книгу с полки.
        /// </summary>
        /// <param name="book">Книга.</param>
        /// <returns><see langword="true"/> если убрали.</returns>
        public bool RemoveBook(Book book)
        {
            if (this.Books.Remove(book))
            {
                book.Shelf = null;
                return true;
            }

            return false;
        }

        /// <inheritdoc />
        public bool Equals(Shelf? other)
        {
            if (other is null)
            {
                return false;
            }

            if (ReferenceEquals(this, other))
            {
                return true;
            }

            return this.Name == other.Name;
        }

        /// <inheritdoc />
        public override bool Equals(object? obj)
        {
            return this.Equals(obj as Shelf);
        }

        /// <inheritdoc/>
        public override int GetHashCode() => this.Name.GetHashCode();

        /// <inheritdoc cref="object.ToString()"/>
        public override string ToString() => $"Название полки {this.Name}";
    }
}
