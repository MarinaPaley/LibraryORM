// <copyright file="StreetBuilder.cs" company="Филипченко Марина Алексеевна">
// Copyright (c) Филипченко Марина Алексеевна 2026. Library.
// </copyright>

namespace TestDataProvider
{
    using Domain;

    /// <summary>
    /// Строитель для создания тестовых экземпляров <see cref="Street"/>.
    /// </summary>
    public sealed class StreetBuilder
    {
        /// <summary>
        /// Инициализирует новый экземпляр класса <see cref="StreetBuilder"/> с дефолтными значениями.
        /// </summary>
        public StreetBuilder()
        {
            this.Name = "Тестовая улица";
            this.City = new CityBuilder();
        }

        private string Name { get; set; }

        private City City { get; set; }

        /// <summary>
        /// Неявное преобразование строителя в сущность <see cref="Street"/>.
        /// </summary>
        /// <param name="builder"> Экземпляр строителя. </param>
        public static implicit operator Street(StreetBuilder builder) => builder.Build();

        /// <summary>
        /// Устанавливает название улицы.
        /// </summary>
        /// <param name="name"> Название улицы. </param>
        /// <returns> Текущий экземпляр строителя. </returns>
        public StreetBuilder WithName(string name)
        {
            this.Name = name;
            return this;
        }

        /// <summary>
        /// Устанавливает город, в котором находится улица.
        /// </summary>
        /// <param name="city"> Экземпляр города. </param>
        /// <returns> Текущий экземпляр строителя. </returns>
        public StreetBuilder WithCity(City city)
        {
            this.City = city;
            return this;
        }

        /// <summary>
        /// Создает и возвращает экземпляр <see cref="Street"/>.
        /// </summary>
        /// <returns> Настроенная улица. </returns>
        public Street Build() => new Street(this.Name, this.City);
    }
}