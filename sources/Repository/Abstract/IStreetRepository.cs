// <copyright file="IStreetRepository.cs" company="Филипченко Марина Алексеевна">
// Copyright (c) Филипченко Марина Алексеевна 2026. Library.
// </copyright>
namespace Repository.Abstract
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Domain;

    /// <summary>
    /// Интерфейс для репозитрия с улицами.
    /// </summary>
    internal interface IStreetRepository
    {
        /// <summary>
        /// Показать список городов, в которых встречается улица с указанным именем.
        /// </summary>
        /// <param name="streetName"> Название улицы. </param>
        /// <returns> Список городов, в которых имеется улица с указанным именем. </returns>
        public Task<IEnumerable<City>> GetCities(string streetName);
    }
}