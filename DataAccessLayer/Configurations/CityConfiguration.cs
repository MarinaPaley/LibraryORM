// <copyright file="CityConfiguration.cs" company="Филипченко Марина Алексеевна">
// Copyright (c) Филипченко Марина Алексеевна 2026. Library.
// </copyright>

namespace DataAccessLayer.Configurations
{
    using DataAccessLayer.Configurations.Abstractions;
    using Domain;

    /// <summary>
    /// Конфигурация правил отображения сущности (<see cref="City"/> в таблицах БД.
    /// </summary>
    internal sealed class CityConfiguration : BaseNamedEntityConfiguration<City>
    {
        /// <summary>
        /// Инициализирует новый экземпляр класса <see cref="CityConfiguration"/>.
        /// </summary>
        public CityConfiguration()
            : base(nameComment: "Назване города", nameIsUnique: true)
        {
        }
    }
}
