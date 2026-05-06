// <copyright file="Author.cs" company="Филипченко Марина Алексеевна">
// Copyright (c) Филипченко Марина Алексеевна 2026. Library.
// </copyright>

namespace Domain
{
    using Domain.Abstract;
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// Автор.
    /// </summary>
    public sealed class Author : PersonRole<Author>
    {
        /// <summary>
        /// Инициализирует новый экземпляр класса <see cref="Author"/>.
        /// </summary>
        /// <param name="person"> Персона. </param>
        /// <exception cref="ArgumentNullException">
        /// Если Полное имя <see langword="null"/>.
        /// </exception>
        public Author(Person person)
            : base(person)
        {
        }

        /// <summary>
        /// Инициализирует новый экземпляр класса <see cref="Author"/>.
        /// </summary>
        [Obsolete("For ORM only", true)]
        private Author()
        {
        }

        /// <summary>
        /// Рукописи, связанные с этой ролью.
        /// </summary>
        public ISet<Manuscript> Manuscripts { get; } =
                new HashSet<Manuscript>(BilingualNamedEntityComparer<Manuscript>.Instance);
    }
}
