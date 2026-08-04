// <copyright file="IColorRepository.cs" company="Филипченко Марина Алексеевна">
// Copyright (c) Филипченко Марина Алексеевна 2026. Library.
// </copyright>
namespace Repository.Abstract
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Domain;

    /// <summary>
    /// Интерфейс для работы с репозитрием цветов.
    /// </summary>
    public interface IColorRepository
    {
        /// <summary>
        /// Получает список цветов.
        /// </summary>
        /// <returns> Список цветов.</returns>
        public Task<List<string>> GetColors();

        /// <summary>
        /// Получает идентификатор цвета.
        /// </summary>
        /// <param name="color"> Цвет. </param>
        /// <returns> Идентификатор цвета. </returns>
        public Task<Guid?> GetIdAsync(string color);

        /// <summary>
        /// Получает название цвета.
        /// </summary>
        /// <param name="id"> Идентификатор цвета. </param>
        /// <returns> Название цвета. </returns>
        public Task<string?> GetColorAsync(Guid id);
    }
}