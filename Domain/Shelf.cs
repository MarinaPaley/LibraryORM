// <copyright file="Shelf.cs" company="Филипченко Марина Алексеевна">
// Copyright (c) Филипченко Марина Алексеевна 2026. Library.
// </copyright>

namespace Domain
{
    using System;
    using System.Collections.Generic;
    using Domain.Abstract;
    using Staff;

    /// <summary>
    /// Полка.
    /// </summary>
    public sealed class Shelf : Entity<Shelf>, IEquatable<Shelf>
    {
        /// <summary>
        /// Инициализирует новый экземпляр класса <see cref="Shelf"/>.
        /// </summary>
        /// <param name="name"> Название полки. </param>
        public Shelf(string name)
        {
            this.Name = new Title(name);
        }

        [Obsolete("For ORM only")]
        private Shelf()
        {
        }

        /// <summary>
        /// Название полки.
        /// </summary>
        public Title Name { get; set; }

        /// <summary>
        /// Шкаф.
        /// </summary>
        public Cabinet? Cabinet { get; set; }

        /// <summary>
        ///  Книги.
        /// </summary>
        public ISet<Book> Books { get; } = new HashSet<Book>();

        /// <summary>
        /// Добавляет книгу на полку.
        /// </summary>
        /// <param name="book">Книга. </param>
        /// <returns><see langword="true"/> если добавили, иначе <see langword="false"/>.</returns>
        public bool AddBook(Book book)
        {
            var result = book is not null
                && this.Books.Add(book);

            if (result)
            {
                book!.Shelf = this;
            }

            return result;
        }

        /// <summary>
        /// Снимаем книгу с полки.
        /// </summary>
        /// <param name="book">Книга.</param>
        /// <returns><see langword="true"/> если убрали, иначе <see langword="false"/>.</returns>
        public bool RemoveBook(Book book)
        {
            var result = book is not null
                && this.Books.Remove(book);

            if (result)
            {
                book!.Shelf = null;
            }

            return result;
        }

        /// <inheritdoc />
        public override bool Equals(Shelf? other)
        {
            return ReferenceEquals(this, other)
                || (other is not null
                && this.Name == other.Name
                && this.Cabinet is not null
                && this.Cabinet.Equals(other.Cabinet));
        }

        /// <inheritdoc />
        public override bool Equals(object? obj) => this.Equals(obj as Shelf);

        /// <inheritdoc/>
        public override int GetHashCode() => this.Name?.GetHashCode() ?? 0;

        /// <inheritdoc cref="object.ToString()"/>
        public override string ToString()
        {
            var location = this.Cabinet is not null
                ? $" ({this.Cabinet.Name} → {this.Cabinet.Room?.Name})"
                : string.Empty;

            return this.Books.Count == 0
                ? $"Полка: {this.Name}{location}"
                : $"Полка: {this.Name}{location} | Книги: {this.Books.Join()}";
        }
    }
}
