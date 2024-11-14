// <copyright file="Author.cs" company="Васильева Марина Алексеевна">
// Copyright (c) Васильева Марина Алексеевна 2024. Library.
// </copyright>

namespace Domain
{
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// Класс Автор.
    /// </summary>
    public sealed class Author : Person
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
        /// Книги.
        /// </summary>
        public ISet<Book> Books { get; } = new HashSet<Book>();

        /// <summary>
        /// Добавляем книгу автору.
        /// </summary>
        /// <param name="book"> Книга. </param>
        /// <returns><see langword="true"/> если добавили, иначе <see langword="false"/>.. </returns>
        public bool AddBook(Book book)
        {
            if (book is null)
            {
                return false;
            }

            if (this.Books.Add(book))
            {
                _ = book.Authors.Add(this);
                return true;
            }

            return false;
        }

        /// <summary>
        /// Удаляем книгу у Автора.
        /// </summary>
        /// <param name="book">Книга.</param>
        /// <returns><see langword="true"/> если убрали, иначе <see langword="false"/>..</returns>
        public bool RemoveBook(Book book)
        {
            if (book is null)
            {
                return false;
            }

            if (this.Books.Remove(book))
            {
                book.Authors.Remove(this);
                return true;
            }

            return false;
        }

        /// <inheritdoc/>
        public override bool Equals(object? obj) => this.Equals(obj as Author);

        /// <inheritdoc/>
        public override int GetHashCode() => base.GetHashCode();
    }
}
