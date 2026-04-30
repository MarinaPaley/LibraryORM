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
    public sealed class Street : NamedEntity<Street>
    {
        /// <summary>
        /// Инициализирует новый экземпляр класса <see cref="Street"/>.
        /// </summary>
        /// <param name="name"> Название улицы.</param>
        /// <param name="city"> Город. </param>
        public Street(string name, City city)
            : base(name)
        {
            ArgumentNullException.ThrowIfNull(city);
            city.AddStreet(this);
        }

#pragma warning disable CS8618 // Необходимо для работы с обязательными полями, получаемыми не через конструктор.

        /// <summary>
        /// Инициализирует новый экземпляр класса <see cref="Street"/>.
        /// </summary>
        [Obsolete("For ORM only", true)]
        private Street()
            : base("Не задано")
        {
        }
#pragma warning restore CS8618

        /// <summary>
        /// Город.
        /// </summary>
        public City City { get; set; }
    }
}
