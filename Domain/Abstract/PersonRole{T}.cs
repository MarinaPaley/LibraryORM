// <copyright file="PersonRole{T}.cs" company="Филипченко Марина Алексеевна">
// Copyright (c) Филипченко Марина Алексеевна 2026. Library.
// </copyright>

namespace Domain.Abstract
{
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// Базовый класс для ролей персоны (автор, редактор, переводчик, рецензент).
    /// </summary>
    /// <typeparam name="T">Конкретный тип роли (например, Author).</typeparam>
    public abstract class PersonRole<T> : Entity<T>, IPerson<T>
            where T : PersonRole<T>
        {
        /// <summary>
        /// Инициализирует новый экземпляр класса <see cref="PersonRole{T}"/>.
        /// </summary>
        /// <param name="person">Персона.</param>
        /// <exception cref="ArgumentNullException">Если <paramref name="person"/> равен null.</exception>
        protected PersonRole(Person person)
        {
            this.Person = person ?? throw new ArgumentNullException(nameof(person));
        }

#pragma warning disable CS8618 // Необходимо для работы с обязательными полями, получаемыми не через конструктор.
        /// <summary>
        /// Инициализирует новый экземпляр класса <see cref="PersonRole{T}"/>.
        /// </summary>
        [Obsolete("For ORM only", true)]
        protected PersonRole()
        {
        }
#pragma warning restore CS8618

        /// <summary>
        /// Персона, выполняющая эту роль.
        /// </summary>
        public virtual Person Person { get; set; } = null!;

        /// <summary>
        /// Внешний ключ на персону.
        /// </summary>
        public virtual Guid PersonId { get; set; }

        /// <inheritdoc/>
        public override string ToString() => this.Person?.ToString() ?? "Unknown Person";

        /// <inheritdoc/>
        public override bool Equals(T? other)
        {
            if (ReferenceEquals(this, other))
        {
            return true;
        }

            if (other is null)
        {
            return false;
        }

            return BasePersonComparer<T>.AreEqualByPerson((T?)this, other);
        }

        /// <inheritdoc/>
        public override bool Equals(object? obj)
        {
            return obj is T other && this.Equals(other);
        }

        /// <inheritdoc/>
        public override int GetHashCode() => this.PersonId.GetHashCode();
        }
}
