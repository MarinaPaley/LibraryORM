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
    public abstract class Contributor : Entity<Contributor>, IEquatable<Contributor>
    {
        /// <summary>
        /// Инициализирует новый экземпляр класса <see cref="Contributor"/>.
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
        /// Инициализирует новый экземпляр класса <see cref="Contributor"/>.
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

        /// <inheritdoc/>
        public override bool Equals(Contributor? other)
        {
            return ReferenceEquals(this, other)
                || (other is not null
                    && this.GetType() == other.GetType()
                    && this.Person?.Equals(other.Person) == true);
        }

        /// <inheritdoc/>
        public override bool Equals(object? obj) => this.Equals(obj as Contributor);

        /// <inheritdoc/>
        public override int GetHashCode() => HashCode.Combine(this.GetType(), this.Person);

        /// <inheritdoc cref="object.ToString()"/>
        public override string ToString() => this.Person.ToString();
    }
}
