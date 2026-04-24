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
    public sealed class Author : Contributor, IEquatable<Author>
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
        /// Рукописи.
        /// </summary>
        public ISet<Manuscript> Manuscripts { get; } = new HashSet<Manuscript>();

        /// <summary>
        /// Добавляем книгу автору.
        /// </summary>
        /// <param name="manuscript"> Книга. </param>
        /// <returns><see langword="true"/> если добавили, иначе <see langword="false"/>.</returns>
        public bool AddManuscript(Manuscript manuscript)
        {
            return manuscript is not null
                && this.Manuscripts.Add(manuscript)
                && manuscript.Authors.Add(this);
        }

        /// <summary>
        /// Удаляем рукопись у автора.
        /// </summary>
        /// <param name="manuscript"> Книга. </param>
        /// <returns><see langword="true"/> если убрали, иначе <see langword="false"/>.</returns>
        public bool RemoveBook(Manuscript manuscript)
        {
            return manuscript is not null
                && this.Manuscripts.Remove(manuscript)
                && manuscript.Authors.Remove(this);
        }

        /// <inheritdoc/>
        public bool Equals(Author? other)
        {
            return ReferenceEquals(this, other) || ((other is not null) && this.Person?.Equals(other.Person) == true);
        }

        /// <inheritdoc/>
        public override bool Equals(object? obj) => this.Equals(obj as Author);

        /// <inheritdoc/>
        public override int GetHashCode() => HashCode.Combine(this.GetType(), this.Person);
    }
}
