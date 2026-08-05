// <copyright file="CityBuilder.cs" company="Филипченко Марина Алексеевна">
// Copyright (c) Филипченко Марина Алексеевна 2026. Library.
// </copyright>

namespace TestDataProvider
{
    using Domain;

    /// <summary>
    /// Строитель для создания тестовых экземпляров <see cref="City"/>.
    /// </summary>
    public sealed class CityBuilder
    {
        /// <summary>
        /// Инициализирует новый экземпляр класса <see cref="CityBuilder"/>.
        /// </summary>
        public CityBuilder()
        {
        }

        private string Name { get; set; } = "Москва";

        /// <summary>
        /// Неявное преобразование строителя в сущность <see cref="City"/>.
        /// </summary>
        /// <param name="builder"> Экземпляр строителя. </param>
        public static implicit operator City(CityBuilder builder) => builder.Build();

        /// <summary>
        /// Устанавливает название города.
        /// </summary>
        /// <param name="name"> Название города. </param>
        /// <returns> Текущий экземпляр строителя. </returns>
        public CityBuilder WithName(string name)
        {
            this.Name = name;
            return this;
        }

        /// <summary>
        /// Создает и возвращает экземпляр <see cref="City"/>.
        /// </summary>
        /// <returns> Настроенный город. </returns>
        public City Build() => new City(this.Name);
    }
}