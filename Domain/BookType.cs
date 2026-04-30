// <copyright file="BookType.cs" company="Филипченко Марина Алексеевна">
// Copyright (c) Филипченко Марина Алексеевна 2026. Library.
// </copyright>

namespace Domain
{
    using System;
    using System.Collections.Generic;
    using Domain.Abstract;

    /// <summary>
    /// Тип напечатанного произведения (книга, методичка, статья).
    /// </summary>
    public sealed class BookType : NamedEntity<BookType>
    {
        /// <summary>
        /// Инициализирует новый экземпляр класса <see cref="BookType"/>.
        /// </summary>
        /// <param name="name"> Жанр. </param>
        public BookType(string name)
            : base(name)
        {
        }

        [Obsolete("For ORM only")]
        private BookType()
            : base("Не задано")
        {
        }

        /// <summary>
        /// Книги.
        /// </summary>
        public ISet<Book> Books { get; } = new HashSet<Book>(EntityComparer<Book>.Instance);
    }
}
