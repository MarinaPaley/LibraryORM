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
        /// Инициализирует новый экземпляр класса <see cref="Address"./>
        /// </summary>
        /// <param name="city"> Город. </param>
        /// <param name="street"> Улица. </param>
        /// <param name="house"> Дом. </param>
        /// <param name="buildingSuffix"> Корпус или владение. </param>
        /// <param name="apartment"> Квартира. </param>
        /// <exception cref="ArgumentNullException"> Если <see cref="City"/> 
        /// или <see cref="Street"/> равны <see langword="null"/>.</exception>
        public Address(
            City city,
            Street street,
            int house,
            string? buildingSuffix = null,
            int? apartment = null)
        {
            this.City = city ?? throw new ArgumentNullException(nameof(city));
            this.Street = street ?? throw new ArgumentNullException(nameof(street));
            this.House = house;
            this.BuildingSuffix = buildingSuffix;
            this.Apartment = apartment;
        }

        [Obsolete("For ORV only")]
        private Address()
        {
        }

        /// <summary>
        /// Город.
        /// </summary>
        public City City { get; set; }

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
        /// Квартира.
        /// </summary>
        public int? Apartment { get; set; }

        /// <inheritdoc/>
        public override bool Equals(object? obj) => this.Equals(obj as Address);

        /// <inheritdoc/>
        public override bool Equals(Address? other)
        {
            return ReferenceEquals(this, other) || ((other is not null) && (this.Id == other.Id));
        }

        /// <inheritdoc/>
        public override int GetHashCode() => this.Id.GetHashCode();

        /// <inheritdoc/>
        public override string ToString()
        {
            var buildingHouse = this.BuildingSuffix is not null
                ? this.BuildingSuffix
                : string.Empty;
            var apartment = this.Apartment.HasValue
                ? $"кв. {this.Apartment}"
                : string.Empty;

            return new StringBuilder()
                .Append(this.City)
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
