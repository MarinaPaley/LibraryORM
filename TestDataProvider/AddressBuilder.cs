// <copyright file="AddressBuilder.cs" company="Филипченко Марина Алексеевна">
// Copyright (c) Филипченко Марина Алексеевна 2026. Library.
// </copyright>

namespace TestDataProvider
{
    using Domain;

    /// <summary>
    /// Строитель для создания тестовых экземпляров <see cref="Address"/>.
    /// </summary>
    public sealed class AddressBuilder
    {
        /// <summary>
        /// Инициализирует новый экземпляр класса <see cref="AddressBuilder"/> с дефолтными значениями.
        /// </summary>
        public AddressBuilder()
        {
            this.Street = new StreetBuilder();
            this.House = 1;
            this.BuildingSuffix = null;
            this.Floor = null;
            this.Apartment = null;
        }

        private Street Street { get; set; }

        private int House { get; set; }

        private string? BuildingSuffix { get; set; }

        private int? Floor { get; set; }

        private int? Apartment { get; set; }

        /// <summary>
        /// Неявное преобразование строителя в сущность <see cref="Address"/>.
        /// </summary>
        /// <param name="builder"> Экземпляр строителя. </param>
        public static implicit operator Address(AddressBuilder builder) => builder.Build();

        /// <summary>
        /// Устанавливает улицу.
        /// </summary>
        /// <param name="street"> Экземпляр улицы. </param>
        /// <returns> Текущий экземпляр строителя. </returns>
        public AddressBuilder WithStreet(Street street)
        {
            this.Street = street;
            return this;
        }

        /// <summary>
        /// Устанавливает номер дома.
        /// </summary>
        /// <param name="house"> Номер дома. </param>
        /// <returns> Текущий экземпляр строителя. </returns>
        public AddressBuilder WithHouse(int house)
        {
            this.House = house;
            return this;
        }

        /// <summary>
        /// Устанавливает корпус или строение.
        /// </summary>
        /// <param name="buildingSuffix"> Обозначение корпуса или строения. </param>
        /// <returns> Текущий экземпляр строителя. </returns>
        public AddressBuilder WithBuildingSuffix(string? buildingSuffix)
        {
            this.BuildingSuffix = buildingSuffix;
            return this;
        }

        /// <summary>
        /// Устанавливает этаж.
        /// </summary>
        /// <param name="floor"> Номер этажа. </param>
        /// <returns> Текущий экземпляр строителя. </returns>
        public AddressBuilder WithFloor(int? floor)
        {
            this.Floor = floor;
            return this;
        }

        /// <summary>
        /// Устанавливает номер квартиры.
        /// </summary>
        /// <param name="apartment"> Номер квартиры. </param>
        /// <returns> Текущий экземпляр строителя. </returns>
        public AddressBuilder WithApartment(int? apartment)
        {
            this.Apartment = apartment;
            return this;
        }

        /// <summary>
        /// Создает и возвращает экземпляр <see cref="Address"/>.
        /// </summary>
        /// <returns> Настроенный адрес. </returns>
        public Address Build() => new Address(this.Street, this.House, this.BuildingSuffix, this.Floor, this.Apartment);
    }
}