// <copyright file="Author.cs" company="Филипченко Марина Алексеевна">
// Copyright (c) Филипченко Марина Алексеевна 2026. Library.
// </copyright>

namespace Domain
{
    using System;
    using System.Collections.Generic;
    using Domain.Abstract;

    /// <summary>
    /// Автор.
    /// </summary>
    public sealed class Author : Entity<Author>, IPerson<Author>
    {
        /// <summary>
        /// Инициализирует новый экземпляр класса <see cref="Author"/>.
        /// </summary>
        /// <param name="person"> Персона. </param>
        /// <exception cref="ArgumentNullException">
        /// Если Полное имя <see langword="null"/>.
        /// </exception>
        public Author(Person person)
        {
            this.Person = person ?? throw new ArgumentNullException(nameof(person));
        }

        /// <summary>
        /// Инициализирует новый экземпляр класса <see cref="Author"/>.
        /// </summary>
        [Obsolete("For ORM only", true)]
        private Author()
        {
        }

        /// <summary>
        /// Рукописи.
        /// </summary>
        public ISet<Manuscript> Manuscripts { get; } = new HashSet<Manuscript>(BilingualNamedEntityComparer<Manuscript>.Instance);

        /// <summary>
        /// Персона.
        /// </summary>
        public Person Person { get; set; } = null!;

        /// <summary>
        /// Явный внешний ключ (обязательно!).
        /// </summary>
        public Guid PersonId { get; set; }

        /// <inheritdoc cref="object.ToString()"/>
        public override string ToString() => this.Person.ToString();

        /// <inheritdoc/>
        public override bool Equals(Author? other)
        {
            return ReferenceEquals(this, other)
                || PersonComparer<Author>.Instance.Equals(this, other);
        }
    }
}
