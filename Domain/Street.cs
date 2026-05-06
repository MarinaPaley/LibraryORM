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
    public sealed class Street : NamedEntity<Street>, IEquatable<Street>
    {
        /// <summary>
        /// Инициализирует новый экземпляр класса <see cref="Street"/>.
        /// </summary>
        /// <param name="name"> Название улицы.</param>
        /// <param name="city"> Город. </param>
        /// <exception cref="ArgumentNullException"> В случае, если <see cref="City"/> <see langword="null"/>.</exception>
        /// <exception cref="ArgumentException">В случае, если город уже имеет такую улицу. </exception>
        public Street(string name, City city)
            : base(name)
        {
            ArgumentNullException.ThrowIfNull(city);
            this.City = city;
            var result = city.Streets.Add(this);

            // НЕ создаем улицу, если она уже есть у Города.
            if (!result)
            {
                throw new ArgumentException(null, nameof(city));
            }
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

        /// <inheritdoc/>
        public override bool Equals(Street? other)
        {
            return ReferenceEquals(this, other)
                || NamedEntityComparer<Street>.Instance.Equals(this, other);
        }

        /// <inheritdoc/>
        public override bool Equals(object? obj) => this.Equals(obj as Street);

        /// <inheritdoc/>
        public override int GetHashCode() => this.Name.GetHashCode();
    }
}
