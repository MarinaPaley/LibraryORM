// <copyright file="ITagRepository.cs" company="Филипченко Марина Алексеевна">
// Copyright (c) Филипченко Марина Алексеевна 2026. Library.
// </copyright>
namespace Repository.Abstract
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Domain;

    /// <summary>
    /// Интерфейс для работы с репозитрием тегов.
    /// </summary>
    public interface ITagRepository
    {
        /// <summary>
        /// Получает список названий категорий.
        /// </summary>
        /// <returns> Список названий категорий.</returns>
        public Task<List<string>> GetTags();

        /// <summary>
        /// Получает идентификатор категории.
        /// </summary>
        /// <param name="tag"> Категория. </param>
        /// <returns> Идентификатор категории. </returns>
        public Task<Guid?> GetIdAsync(string tag);

        /// <summary>
        /// Получает название категории.
        /// </summary>
        /// <param name="id"> Идентификатор категории. </param>
        /// <returns> Название категории. </returns>
        public Task<string?> GetTagNameAsync(Guid id);

        /// <summary>
        /// Получает категорию.
        /// </summary>
        /// <param name="id"> Идентификатор категории. </param>
        /// <returns> Категория.</returns>
        public Task<Tag?> GetTagAsync(Guid id);

        /// <summary>
        /// Получает категорию по названию тега.
        /// </summary>
        /// <param name="tag"> Тег. </param>
        /// <returns> Название категории. </returns>
        public Task<Category?> GetCategoryAsync(string tag);

        /// <summary>
        /// Получает категорию по идентификатору тега.
        /// </summary>
        /// <param name="id"> Идентификатор тега.</param>
        /// <returns> Название категории. </returns>
        public Task<Category?> GetCategoryAsync(Guid id);

        /// <summary>
        /// Получает список книг по тегу.
        /// </summary>
        /// <param name="tag"> Название тега. </param>
        /// <returns> Список книг. </returns>
        public Task<IEnumerable<Book>> GetBooksAsync(string tag);

        /// <summary>
        /// Получает список книг по идентификатору.
        /// </summary>
        /// <param name="id"> Идентификатор тега.</param>
        /// <returns> Список книг. </returns>
        public Task<IEnumerable<Book>> GetBooksAsync(Guid id);
    }
}