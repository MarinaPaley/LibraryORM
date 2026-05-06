// <copyright file="Translator.cs" company="Филипченко Марина Алексеевна">
// Copyright (c) Филипченко Марина Алексеевна 2026. Library.
// </copyright>

namespace Domain
{
    using System;
    using System.Collections.Generic;
    using Domain.Abstract;

    /// <summary>
    /// Переводчик.
    /// </summary>
    public sealed class Translator : PersonRole<Translator>
    {
        /// <summary>
        /// Инициализирует новый экземпляр класса <see cref="Translator"/>.
        /// </summary>
        /// <param name="person"> Персона. </param>
        /// <exception cref="ArgumentNullException">
        /// Если Полное имя <see langword="null"/>.
        /// </exception>
        public Translator(Person person)
            : base(person)
        {
        }

        /// <summary>
        /// Инициализирует новый экземпляр класса <see cref="Translator"/>.
        /// </summary>
        [Obsolete("For ORM only", true)]
        private Translator()
        {
        }

        /// <summary>
        /// Рукописи.
        /// </summary>
        public ISet<Manuscript> Manuscripts { get; } = new HashSet<Manuscript>(BilingualNamedEntityComparer<Manuscript>.Instance);

        /// <summary>
        /// Добавляем рукопись переводчику.
        /// </summary>
        /// <param name="manuscript"> Рукопись. </param>
        /// <returns><see langword="true"/> если добавили, иначе <see langword="false"/>.</returns>
        public bool AddManuscript(Manuscript manuscript)
        {
            return manuscript is not null
                && this.Manuscripts.Add(manuscript)
                && manuscript.Translators.Add(this);
        }

        /// <summary>
        /// Удаляем рукопись у переводчика.
        /// </summary>
        /// <param name="manuscript"> Рукопись. </param>
        /// <returns><see langword="true"/> если убрали, иначе <see langword="false"/>.</returns>
        public bool RemoveManuscript(Manuscript manuscript)
        {
            return manuscript is not null
                && this.Manuscripts.Remove(manuscript)
                && manuscript.Translators.Remove(this);
        }
    }
}
