// <copyright file="Editor.cs" company="Филипченко Марина Алексеевна">
// Copyright (c) Филипченко Марина Алексеевна 2026. Library.
// </copyright>

namespace Domain
{
    using System;
    using System.Collections.Generic;
    using Domain.Abstract;

    /// <summary>
    /// Редактор.
    /// </summary>
    public sealed class Editor : Contributor
    {
        /// <summary>
        /// Инициализирует новый экземпляр класса <see cref="Editor"/>.
        /// </summary>
        /// <param name="person"> Персона. </param>
        /// <exception cref="ArgumentNullException">
        /// Если Полное имя <see langword="null"/>.
        /// </exception>
        public Editor(Person person)
            : base(person)
        {
        }

        /// <summary>
        /// Инициализирует новый экземпляр класса <see cref="Editor"/>.
        /// </summary>
        [Obsolete("For ORM only", true)]
        private Editor()
        {
        }

        /// <summary>
        /// Книги.
        /// </summary>
        public ISet<Book> Books { get; } = new HashSet<Book>();

        /// <summary>
        /// Добавляем книгу редактору.
        /// </summary>
        /// <param name="book"> Книга. </param>
        /// <returns><see langword="true"/> если добавили, иначе <see langword="false"/>.</returns>
        public bool AddBook(Book book)
        {
            if (book is not null)
            {
                _ = this.Books.Add(book);
                book.Editor = this;

                return true;
            }

            return false;
        }

        /// <summary>
        /// Удаляем книгу у редактора.
        /// </summary>
        /// <param name="book"> Книга. </param>
        /// <returns><see langword="true"/> если убрали, иначе <see langword="false"/>.</returns>
        public bool RemoveBook(Book book)
        {
            if (book is not null)
            {
                _ = this.Books.Remove(book);
                book.Editor = this;

                return true;
            }

            return false;
        }
    }
}
