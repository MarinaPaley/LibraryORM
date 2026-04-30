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
    public sealed class City : NamedEntity<City>
    {
        /// <summary>
        /// Инициализирует новый экземпляр класса <see cref="City"/>.
        /// </summary>
        /// <param name="name"> Название города. </param>
        /// <exception cref="ArgumentNullException">
        /// В случае если <paramref name="name"/> – <see langword="null"/>.
        /// </exception>
        public City(string name)
            : base(name)
        {
        }

#pragma warning disable CS8618 // Необходимо для работы с обязательными полями, получаемыми не через конструктор.

        /// <summary>
        /// Инициализирует новый экземпляр класса <see cref="City"/>.
        /// </summary>
        [Obsolete("For ORM only", true)]
        private City()
            : base("Не задано")
        {
        }

#pragma warning restore CS8618

        /// <summary>
        /// Улицы.
        /// </summary>
        public ISet<Street> Streets { get; } = new HashSet<Street>(NamedEntityComparer<Street>.Instance);

        /// <summary>
        /// Добавить улицу.
        /// </summary>
        /// <param name="street"> Улица. </param>
        /// <returns> <see langword="true"/>, если добавили, иначе - <see langword="false"/>. </returns>
        public bool AddStreet(Street street)
        {
            if (street is not null)
            {
                street!.City = this;
                return this.Streets.Add(street);
            }

            return false;
        }
    }
}
