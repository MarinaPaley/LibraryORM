// <copyright file="IShelfRepository.cs" company="Филипченко Марина Алексеевна">
// Copyright (c) Филипченко Марина Алексеевна 2026. Library.
// </copyright>

namespace Repository.Abstract
{
    using System;
    using System.Threading.Tasks;

    /// <summary>
    /// Интерфейс для репозитория с полками.
    /// </summary>
    public interface IShelfRepository
    {
        /// <summary>
        /// Показать количество книг, стоящих на данной полке.
        /// </summary>
        /// <param name="id">Идентификатор полки.</param>
        /// <returns> Количество книг.</returns>
        public Task<int?> GetCountBooksAsync(Guid id);

        /// <summary>
        /// Показать количество книг, стоящих на полке.
        /// </summary>
        /// <param name="name"> Название полки.</param>
        /// <returns> Количество книг.</returns>
        public Task<int?> GetCountBooksAsync(string name);

        /// <summary>
        /// Найти идентификатор по имени.
        /// </summary>
        /// <param name="name"> Название полки.</param>
        /// <returns> Идентификатор.</returns>
        public Task<Guid?> GetIdByName(string name);
    }
}