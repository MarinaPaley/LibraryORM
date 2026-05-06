// <copyright file="Reviewer.cs" company="Филипченко Марина Алексеевна">
// Copyright (c) Филипченко Марина Алексеевна 2026. Library.
// </copyright>

namespace Domain
{
    using System;
    using System.Collections.Generic;
    using Domain.Abstract;

    /// <summary>
    /// Рецензент.
    /// </summary>
    public sealed class Reviewer : Entity<Reviewer>, IPerson<Reviewer>
    {
        /// <summary>
        /// Инициализирует новый экземпляр класса <see cref="Reviewer"/>.
        /// </summary>
        /// <param name="person"> Персона. </param>
        /// <exception cref="ArgumentNullException">
        /// Если Полное имя <see langword="null"/>.
        /// </exception>
        public Reviewer(Person person)
        {
            this.Person = person ?? throw new ArgumentNullException(nameof(person));
        }

#pragma warning disable CS8618 // Необходимо для работы с обязательными полями, получаемыми не через конструктор.
        /// <summary>
        /// Инициализирует новый экземпляр класса <see cref="Reviewer"/>.
        /// </summary>
        [Obsolete("For ORM only", true)]
        private Reviewer()
        {
        }
#pragma warning restore CS8618

        /// <summary>
        /// Персона.
        /// </summary>
        public Person Person { get; set; }

        /// <summary>
        /// Явный внешний ключ (обязательно!).
        /// </summary>
        public Guid PersonId { get; set; }

        /// <summary>
        /// Рукописи.
        /// </summary>
        public ISet<Manuscript> Manuscripts { get; } = new HashSet<Manuscript>(BilingualNamedEntityComparer<Manuscript>.Instance);

        /// <inheritdoc cref="object.ToString()"/>
        public override string ToString() => this.Person.ToString();

        /// <inheritdoc/>
        public override bool Equals(Reviewer? other)
        {
            return ReferenceEquals(this, other)
                || PersonComparer<Reviewer>.Instance.Equals(this, other);
        }

        /// <summary>
        /// Добавляем рукопись рецензенту.
        /// </summary>
        /// <param name="manuscript"> Книга. </param>
        /// <returns><see langword="true"/> если добавили, иначе <see langword="false"/>.</returns>
        public bool AddManuscript(Manuscript manuscript)
        {
            return manuscript is not null
                && this.Manuscripts.Add(manuscript)
                && manuscript.Reviewers.Add(this);
        }

        /// <summary>
        /// Удаляем рукопись у рецензента.
        /// </summary>
        /// <param name="manuscript"> Книга. </param>
        /// <returns><see langword="true"/> если убрали, иначе <see langword="false"/>.</returns>
        public bool RemoveBook(Manuscript manuscript)
        {
            return manuscript is not null
                && this.Manuscripts.Remove(manuscript)
                && manuscript.Reviewers.Remove(this);
        }
    }
}
