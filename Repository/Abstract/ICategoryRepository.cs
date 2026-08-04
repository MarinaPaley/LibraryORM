// <copyright file="ICategoryRepository.cs" company="Филипченко Марина Алексеевна">
// Copyright (c) Филипченко Марина Алексеевна 2026. Library.
// </copyright>
namespace Repository.Abstract
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Domain;

    /// <summary>
    /// Интерфейс для работы с репозитрием категорий.
    /// </summary>
    public interface ICategoryRepository
    {
        /// <summary>
        /// Получает список названий категорий.
        /// </summary>
        /// <returns> Список названий категорий.</returns>
        public Task<List<string>> GetCategories();

        /// <summary>
        /// Получает идентификатор категории.
        /// </summary>
        /// <param name="category"> Категория. </param>
        /// <returns> Идентификатор категории. </returns>
        public Task<Guid?> GetIdAsync(string category);

        /// <summary>
        /// Получает название категории.
        /// </summary>
        /// <param name="id"> Идентификатор категории. </param>
        /// <returns> Название категории. </returns>
        public Task<string?> GetCategoryNameAsync(Guid id);

        /// <summary>
        /// Получает категорию.
        /// </summary>
        /// <param name="id"> Идентификатор категории. </param>
        /// <returns> Категория.</returns>
        public Task<Category?> GetCategoryAsync(Guid id);

        /// <summary>
        /// Получает цвет категории по идентификатору категории.
        /// </summary>
        /// <param name="id"> Идентификатор категории.</param>
        /// <returns> Цвет категории. </returns>
        public Task<ColorCode?> GetColorCodeAsync(Guid id);

        /// <summary>
        /// Получает цвет категории по названию категории.
        /// </summary>
        /// <param name="category"> Название категории.</param>
        /// <returns> Цвет категории. </returns>
        public Task<ColorCode?> GetColorCodeAsync(string category);

        /// <summary>
        /// Получает список всех подкатегорий по идентификатору категории.
        /// </summary>
        /// <param name="id"> Идентификатор категории.</param>
        /// <returns> Список подкатегорий.</returns>
        public Task<List<Tag>> GetTagsAsync(Guid id);

        /// <summary>
        /// Получает список всех подкатегорий по названию категории.
        /// </summary>
        /// <param name="category"> Название категории. </param>
        /// <returns> Список подкатегорий.</returns>
        public Task<List<Tag>> GetTagsAsync(string category);
    }
}