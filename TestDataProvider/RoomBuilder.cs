// <copyright file="RoomBuilder.cs" company="Филипченко Марина Алексеевна">
// Copyright (c) Филипченко Марина Алексеевна 2026. Library.
// </copyright>

namespace TestDataProvider
{
    using Domain;

    /// <summary>
    /// Строитель для создания тестовых экземпляров <see cref="Room"/>.
    /// </summary>
    public sealed class RoomBuilder
    {
        /// <summary>
        /// Инициализирует новый экземпляр класса <see cref="RoomBuilder"/> с дефолтными значениями.
        /// </summary>
        public RoomBuilder()
        {
            this.Name = "Тестовая комната";
            this.Address = new AddressBuilder();
        }

        private string Name { get; set; }

        private Address Address { get; set; }

        /// <summary>
        /// Неявное преобразование строителя в сущность <see cref="Room"/>.
        /// </summary>
        /// <param name="builder"> Экземпляр строителя. </param>
        public static implicit operator Room(RoomBuilder builder) => builder.Build();

        /// <summary>
        /// Устанавливает название комнаты.
        /// </summary>
        /// <param name="name"> Название комнаты. </param>
        /// <returns> Текущий экземпляр строителя. </returns>
        public RoomBuilder WithName(string name)
        {
            this.Name = name;
            return this;
        }

        /// <summary>
        /// Устанавливает адрес комнаты.
        /// </summary>
        /// <param name="address"> Экземпляр адреса. </param>
        /// <returns> Текущий экземпляр строителя. </returns>
        public RoomBuilder WithAddress(Address address)
        {
            this.Address = address;
            return this;
        }

        /// <summary>
        /// Создает и возвращает экземпляр <see cref="Room"/>.
        /// </summary>
        /// <returns> Настроенная комната. </returns>
        public Room Build() => new Room(this.Address, this.Name);
    }
}