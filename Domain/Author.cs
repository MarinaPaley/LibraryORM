// <copyright file="Author.cs" company="Васильева Марина Алексеевна">
// Copyright (c) Васильева Марина Алексеевна 2024. Library.
// </copyright>

namespace Domain
{
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// Автор.
    /// </summary>
    public sealed class Author : Person<Author>
    {
        /// <summary>
        /// Инициализирует новый экземпляр класса <see cref="Author"/>.
        /// </summary>
        /// <param name="fullName"> Полное имя.</param>
        /// <param name="dateBirth"> Дата рождения. </param>
        /// <param name="dateDeath"> Дата смерти. </param>
        /// <exception cref="ArgumentNullException">
        /// Если Полное имя <see langword="null"/>.
        /// </exception>
        public Author(
            Name fullName,
            DateOnly? dateBirth = null,
            DateOnly? dateDeath = null)
            : base(fullName, dateBirth, dateDeath)
        {
        }

        /// <summary>
        /// Инициализирует новый экземпляр класса <see cref="Author"/>.
        /// </summary>
        [Obsolete("For ORM only", true)]
        private Author()
            : base(Name.Unknown)
        {
        }

        /// <summary>
        /// Книги.
        /// </summary>
        public ISet<Book> Books { get; } = new HashSet<Book>();

        /// <summary>
        /// Добавляем книгу автору.
        /// </summary>
        /// <param name="book"> Книга. </param>
        /// <returns><see langword="true"/> если добавили, иначе <see langword="false"/>.</returns>
        public bool AddBook(Book book)
        {
            return book is not null
                && this.Books.Add(book)
                && book.Authors.Add(this);
        }

        /// <summary>
        /// Удаляем книгу у автора.
        /// </summary>
        /// <param name="book"> Книга. </param>
        /// <returns><see langword="true"/> если убрали, иначе <see langword="false"/>.</returns>
        public bool RemoveBook(Book book)
        {
            return book is not null
                && this.Books.Remove(book)
                && book.Authors.Remove(this);
        }
    }
}
