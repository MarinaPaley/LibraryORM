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
    public sealed class Shelf : NamedEntity<Shelf>
    {
        /// <summary>
        /// Инициализирует новый экземпляр класса <see cref="Shelf"/>.
        /// </summary>
        /// <param name="name"> Название полки. </param>
        public Shelf(string name)
            : base(name)
        {
        }

#pragma warning disable CS8618 // Необходимо для работы с обязательными полями, получаемыми не через конструктор.

        [Obsolete("For ORM only")]
        private Shelf()
            : base("Не задано")
        {
        }

#pragma warning restore CS8618

        /// <summary>
        /// Шкаф.
        /// </summary>
        public Cabinet? Cabinet { get; set; }

        /// <summary>
        ///  Книги.
        /// </summary>
        public ISet<Book> Books { get; } = new HashSet<Book>(EntityComparer<Book>.Instance);

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
            var result = ReferenceEquals(this, other)
                || (other is not null
                && this.Name == other.Name);

            if (this.Cabinet is not null)
            {
                result &= this.Cabinet.Equals(other?.Cabinet);
            }

            return result;
        }

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
