// <copyright file="Genre.cs" company="Филипченко Марина Алексеевна">
// Copyright (c) Филипченко Марина Алексеевна 2026. Library.
// </copyright>

namespace Domain
{
    using System;
    using System.Collections.Generic;
    using Domain.Abstract;

    /// <summary>
    /// Жанр.
    /// </summary>
    public sealed class Genre : NamedEntity<Genre>
    {
        /// <summary>
        /// Инициализирует новый экземпляр класса <see cref="Genre"/>.
        /// </summary>
        /// <param name="name"> Жанр. </param>
        public Genre(string name)
            : base(name)
        {
        }

#pragma warning disable CS8618 // Необходимо для работы с обязательными полями, получаемыми не через конструктор.
        [Obsolete("For ORM only")]
        private Genre()
            : base("Не задано")
        {
        }
#pragma warning restore CS8618

        /// <summary>
        /// Рукописи.
        /// </summary>
        public ISet<Manuscript> Manuscripts { get; } = new HashSet<Manuscript>(BilingualNamedEntityComparer<Manuscript>.Instance);

        /// <summary>
        /// Добавить книгу к жанрам.
        /// </summary>
        /// <param name="book"> Рукопись.</param>
        /// <returns> <see langword="true"/>, если добавили, иначе - <see langword="false"/>.</returns>
        public bool AddManuscropt(Manuscript book)
        {
            return book is not null
                && this.Manuscripts.Add(book)
                && book.Genres.Add(this);
        }

        /// <summary>
        /// Удаление рукописи из жанра.
        /// </summary>
        /// <param name="book"> Рукопись. </param>
        /// <returns> <see langword="true"/>, если удалили, иначе - <see langword="false"/>.</returns>
        public bool RemoveManuscript(Manuscript book)
        {
            return book is not null
                && this.Manuscripts.Remove(book)
                && book.Genres.Remove(this);
        }
    }
}
