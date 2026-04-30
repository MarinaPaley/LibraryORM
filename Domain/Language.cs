// <copyright file="Language.cs" company="Филипченко Марина Алексеевна">
// Copyright (c) Филипченко Марина Алексеевна 2026. Library.
// </copyright>

namespace Domain
{
    using System;
    using System.Collections.Generic;
    using Domain.Abstract;

    /// <summary>
    /// Язык.
    /// </summary>
    public sealed class Language : NamedEntity<Language>
    {
        /// <summary>
        /// Инициализирует новый экземпляр класса <see cref="Language"/>.
        /// </summary>
        /// <param name="name"> Язык. </param>
        public Language(string name)
            : base(name)
        {
        }

#pragma warning disable CS8618 // Необходимо для работы с обязательными полями, получаемыми не через конструктор.
        [Obsolete("For ORM only")]
        private Language()
            : base("Не задано")
        {
        }
#pragma warning restore CS8618

        /// <summary>
        /// Рукописи.
        /// </summary>
        public ISet<Manuscript> Manuscripts { get; } = new HashSet<Manuscript>(BilingualNamedEntityComparer<Manuscript>.Instance);
    }
}
