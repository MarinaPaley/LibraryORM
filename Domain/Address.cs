// <copyright file="Address.cs" company="Филипченко Марина Алексеевна">
// Copyright (c) Филипченко Марина Алексеевна 2026. Library.
// </copyright>

namespace Domain
{
    using System;
    using System.Text;
    using Domain.Abstract;

    /// <summary>
    /// Издательство.
    /// </summary>
    public sealed class Address : Entity<Address>, IEquatable<Address>
    {
        /// <summary>
        /// Инициализирует новый экземпляр класса <see cref="Address"/>.
        /// </summary>
        /// <param name="street"> Улица. </param>
        /// <param name="house"> Дом. </param>
        /// <param name="buildingSuffix"> Корпус или владение. </param>
        /// <param name="floor"> Этаж. </param>
        /// <param name="apartment"> Квартира. </param>
        /// <exception cref="ArgumentNullException"> Если <see cref="City"/>.
        /// или <see cref="Street"/> равны <see langword="null"/>.</exception>
        public Address(
            Street street,
            int house,
            string? buildingSuffix = null,
            int? floor = null,
            int? apartment = null)
        {
            this.Street = street ?? throw new ArgumentNullException(nameof(street));
            this.House = house;
            this.BuildingSuffix = buildingSuffix;
            this.Floor = floor;
            this.Apartment = apartment;
        }

#pragma warning disable CS8618 // Необходимо для работы с обязательными полями, получаемыми не через конструктор.

        /// <summary>
        /// Инициализирует новый экземпляр класса <see cref="Address"/>.
        /// </summary>
        [Obsolete("For ORV only")]
        private Address()
        {
        }
#pragma warning restore CS8618

        /// <summary>
        /// Улица.
        /// </summary>
        public Street Street { get; set; }

        /// <summary>
        /// Дом.
        /// </summary>
        public int House { get; set; }

        /// <summary>
        /// Корпус.
        /// </summary>
        public string? BuildingSuffix { get; set; }

        /// <summary>
        /// Этаж.
        /// </summary>
        public int? Floor { get; set; }

        /// <summary>
        /// Квартира.
        /// </summary>
        public int? Apartment { get; set; }

        /// <inheritdoc/>
        public override bool Equals(object? obj) => this.Equals(obj as Address);

        /// <inheritdoc/>
        public override bool Equals(Address? other)
        {
            return ReferenceEquals(this, other)
                || (other is not null
                    && NamedEntityComparer<Street>.Instance.Equals(this.Street, other.Street)
                    && NamedEntityComparer<City>.Instance.Equals(this.Street.City, other.Street.City)
                    && this.House == other.House
                    && this.BuildingSuffix == other.BuildingSuffix
                    && this.Apartment == other.Apartment);
        }

        /// <inheritdoc/>
        public override int GetHashCode() => HashCode.Combine(this.Street, this.House, this.BuildingSuffix, this.Apartment);

        /// <inheritdoc cref="object.ToString()"/>
        public override string ToString()
        {
            var buildingHouse = this.BuildingSuffix is not null
                ? this.BuildingSuffix
                : string.Empty;

            var apartment = this.Apartment.HasValue
                ? $"кв. {this.Apartment}"
                : string.Empty;

            return new StringBuilder()
                .Append(this.Street.City)
                .Append(' ')
                .Append(this.Street)
                .Append(' ')
                .Append(this.House)
                .Append(' ')
                .Append(buildingHouse)
                .Append(' ')
                .Append(apartment)
                .ToString();
        }
    }
}
