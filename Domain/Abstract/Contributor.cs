// <copyright file="Contributor.cs" company="Филипченко Марина Алексеевна">
// Copyright (c) Филипченко Марина Алексеевна 2026. Library.
// </copyright>

namespace Domain.Abstract
{
    using System;
    using Domain;

    /// <summary>
    /// Базовый класс для участников создания произведения.
    /// </summary>
    public abstract class Contributor : Entity<Contributor>
    {
        /// <summary>
        /// Инициализирует новый экземпляр класса <see cref="Contributor"/>.
        /// </summary>
        /// <param name="person"> Персона. </param>
        /// <exception cref="ArgumentNullException"> если персона <see langword="null"/>. </exception>
        protected Contributor(Person person)
        {
            this.Person = person ?? throw new ArgumentNullException(nameof(person));
        }

        [Obsolete("For ORM only", true)]
        protected Contributor() { }

        /// <summary>
        /// Персона.
        /// </summary>
        public Person Person { get; set; }
    }
}