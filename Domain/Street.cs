// <copyright file="Street.cs" company="Филипченко Марина Алексеевна">
// Copyright (c) Филипченко Марина Алексеевна 2026. Library.
// </copyright>

namespace Domain
{
    using System;
    using Domain.Abstract;

    /// <summary>
    /// Улица.
    /// </summary>
    public sealed class Street : Entity<Street>, IEquatable<Street>
    {
        /// <summary>
        /// Инициализирует новый экземпляр класса <see cref="Street"/>.
        /// </summary>
        /// <param name="name"> Название улицы.</param>
        /// <param name="city"> Город. </param>
        public Street(string name, City city)
        {
            this.Name = new Title(name);

            ArgumentNullException.ThrowIfNull(city);

            city.AddStreet(this);
        }

#pragma warning disable CS8618 // Необходимо для работы с обязательными полями, получаемыми не через конструктор.

        /// <summary>
        /// Инициализирует новый экземпляр класса <see cref="Street"/>.
        /// </summary>
        [Obsolete("For ORM only", true)]
        private Street()
        {
        }
#pragma warning restore CS8618

        /// <summary>
        /// Название улицы.
        /// </summary>
        public Title Name { get; set; }

        /// <summary>
        /// Город.
        /// </summary>
        public City City { get; set; }

        /// <inheritdoc/>
        public override bool Equals(Street? other)
        {
            return ReferenceEquals(this, other)
                || ((other is not null) && (this.Name == other.Name));
        }

        /// <inheritdoc/>
        public override bool Equals(object? obj) => this.Equals(obj as Street);

        /// <inheritdoc/>
        public override int GetHashCode() => this.Name?.GetHashCode() ?? 0;

        /// <inheritdoc/>
        public override string ToString() => this.Name.ToString();
    }
}
