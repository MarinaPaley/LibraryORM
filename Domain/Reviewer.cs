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
    public sealed class Reviewer : Contributor
    {
        /// <summary>
        /// Инициализирует новый экземпляр класса <see cref="Reviewer"/>.
        /// </summary>
        /// <param name="person"> Персона. </param>
        /// <exception cref="ArgumentNullException">
        /// Если Полное имя <see langword="null"/>.
        /// </exception>
        public Reviewer(Person person)
            : base(person)
        {
        }

        /// <summary>
        /// Инициализирует новый экземпляр класса <see cref="Reviewer"/>.
        /// </summary>
        [Obsolete("For ORM only", true)]
        private Reviewer()
        {
        }

        /// <summary>
        /// Рукописи.
        /// </summary>
        public ISet<Manuscript> Manuscripts { get; } = new HashSet<Manuscript>();

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
