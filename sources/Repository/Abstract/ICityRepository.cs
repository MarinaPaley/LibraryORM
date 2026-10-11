// <copyright file="ICityRepository.cs" company="Филипченко Марина Алексеевна">
// Copyright (c) Филипченко Марина Алексеевна 2026. Library.
// </copyright>
namespace Repository.Abstract
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Domain;

    /// <summary>
    /// Интерфейс для работы с репозитрием городов.
    /// </summary>
    internal interface ICityRepository
    {
        /// <summary>
        /// Получает список городов, в которых есть указанная улица.
        /// </summary>
        /// <param name="street"> Название улицы.</param>
        /// <returns> Список городов.</returns>
        public Task<IEnumerable<City>> GetCities(string street);

        /// <summary>
        /// Получает идентификатор города.
        /// </summary>
        /// <param name="cityName"> Город. </param>
        /// <returns> Идентификатор города. </returns>
        public Task<Guid?> GetIdAsync(string cityName);

        /// <summary>
        /// Получает название города.
        /// </summary>
        /// <param name="id"> Идентификатор города. </param>
        /// <returns> Название города. </returns>
        public Task<string?> GetCityAsync(Guid id);

        /// <summary>
        /// Получает список улиц указанного города.
        /// </summary>
        /// <param name="id"> Идентификатор города. </param>
        /// <returns> Список улиц. </returns>
        public Task<IEnumerable<Street>?> GetStreetsAsync(Guid id);
    }
}