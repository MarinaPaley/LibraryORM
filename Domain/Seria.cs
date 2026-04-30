// <copyright file="Seria.cs" company="Филипченко Марина Алексеевна">
// Copyright (c) Филипченко Марина Алексеевна 2026. Library.
// </copyright>

namespace Domain
{
    using System;
    using System.Collections.Generic;
    using Domain.Abstract;

    /// <summary>
    /// Серия.
    /// </summary>
    public sealed class Seria : NamedEntity<Seria>
    {
        /// <summary>
        /// Инициализирует новый экземпляр класса <see cref="Seria"/>.
        /// </summary>
        /// <param name="seriaName"> Название серии. </param>
        /// <exception cref="ArgumentNullException">
        /// В случае если <paramref name="seriaName"/> – <see langword="null"/>.
        /// </exception>
        public Seria(string seriaName)
            : base(seriaName)
        {
        }

#pragma warning disable CS8618 // Необходимо для работы с обязательными полями, получаемыми не через конструктор.

        /// <summary>
        /// Инициализирует новый экземпляр класса <see cref="Seria"/>.
        /// </summary>
        [Obsolete("For ORM only", true)]
        private Seria()
            : base("Не задано")
        {
        }

#pragma warning restore CS8618

        /// <summary>
        /// Название серии.
        /// </summary>
        public Title SeriaName { get; set; }

        /// <summary>
        /// Книги данной серии.
        /// </summary>
        public ISet<Book> Books { get; } = new HashSet<Book>(EntityComparer<Book>.Instance);

        /// <summary>
        /// Добавить книгу в серию.
        /// </summary>
        /// <param name="book"> Книга. </param>
        /// <returns>
        /// <see langword="true"/>, если добавили, иначе - <see langword="false"/>.
        /// </returns>
        public bool AddBook(Book book)
        {
            var result = book is not null && this.Books.Add(book);
            if (result)
            {
                book!.Seria = this;
            }

            return result;
        }

        /// <summary>
        /// Удаление книги из серии.
        /// </summary>
        /// <param name="book"> Книга.</param>
        /// <returns>
        /// <see langword="true"/>, если удалили, иначе - <see langword="false"/>.
        /// </returns>
        public bool RemoveBook(Book book)
        {
            var result = book is not null && this.Books.Remove(book);
            if (result)
            {
                book?.Seria = null;
            }

            return result;
        }
    }
}
