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
    /// <typeparam name="TEntity"> Тип конкретной сущности. </typeparam>
    public abstract class Contributor<TEntity> : Entity<Contributor<TEntity>>, IPerson<TEntity>
        where TEntity : class, IPerson<TEntity>
    {
        /// <summary>
        /// Инициализирует новый экземпляр класса <see cref="Contributor{TEntity}"/>.
        /// </summary>
        /// <param name="person"> Персона. </param>
        /// <exception cref="ArgumentNullException">
        /// В случае если <paramref name="person"/> – <see langword="null"/>.
        /// </exception>
        protected Contributor(Person person)
        {
            this.Person = person ?? throw new ArgumentNullException(nameof(person));
        }

#pragma warning disable CS8618 // Необходимо для работы с обязательными полями, получаемыми не через конструктор.

        /// <summary>
        /// Инициализирует новый экземпляр класса <see cref="Contributor{TEntity}"/>.
        /// </summary>
        [Obsolete("For ORM only", true)]
        protected Contributor()
        {
        }

#pragma warning restore CS8618

        /// <summary>
        /// Персона.
        /// </summary>
        public Person Person { get; set; }

        /// <inheritdoc cref="object.ToString()"/>
        public override string ToString() => this.Person.ToString();
    }
}
