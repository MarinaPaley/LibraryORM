// <copyright file="City.cs" company="Филипченко Марина Алексеевна">
// Copyright (c) Филипченко Марина Алексеевна 2026. Library.
// </copyright>

namespace Domain
{
    using System;
    using System.Collections.Generic;
    using Domain.Abstract;

    /// <summary>
    /// Город.
    /// </summary>
    public sealed class City : Entity<City>, IEquatable<City>
    {
        /// <summary>
        /// Инициализирует новый экземпляр класса <see cref="City"/>.
        /// </summary>
        /// <param name="name"> Название города.</param>
        public City(string name)
        {
            this.Name = new Title(name);
        }

#pragma warning disable CS8618 // Необходимо для работы с обязательными полями, получаемыми не через конструктор.

        /// <summary>
        /// Инициализирует новый экземпляр класса <see cref="City"/>.
        /// </summary>
        [Obsolete("For ORM only", true)]
        private City()
        {
        }
#pragma warning restore CS8618

        /// <summary>
        /// Название города.
        /// </summary>
        public Title Name { get; set; }

        /// <summary>
        /// Улицы.
        /// </summary>
        public ISet<Street> Streets { get; } = new HashSet<Street>();

        /// <inheritdoc/>
        public override bool Equals(City? other)
        {
            return ReferenceEquals(this, other) || ((other is not null) && (this.Name == other.Name));
        }

        /// <inheritdoc/>
        public override bool Equals(object? obj) => this.Equals(obj as City);

        /// <inheritdoc/>
        public override int GetHashCode() => this.Name?.GetHashCode() ?? 0;

        /// <inheritdoc/>
        public override string ToString() => this.Name.ToString();

        /// <summary>
        /// Добавить улицу.
        /// </summary>
        /// <param name="street"> Улица. </param>
        /// <returns> <see langword="true"/>, если добавили, иначе - <see langword="false"/>. </returns>
        public bool AddStreet(Street street)
        {
            var result = street is not null
                && this.Streets.Add(street);

            if (result)
            {
                street!.City = this;
            }

            return result;
        }
    }
}
