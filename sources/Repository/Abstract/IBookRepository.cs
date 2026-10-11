// <copyright file="IBookRepository.cs" company="Филипченко Марина Алексеевна">
// Copyright (c) Филипченко Марина Алексеевна 2026. Library.
// </copyright>
namespace Repository.Abstract
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Domain;

    /// <summary>
    /// Интерфейс для репозитория с книгами.
    /// </summary>
    public interface IBookRepository
    {
        /// <summary>
        /// Получает идентификатор по названию книги.
        /// </summary>
        /// <param name="title"> Название книги. </param>
        /// <returns> Идентификатор. </returns>
        Task<Guid?> GetIdAsync(string title);

        /// <summary>
        /// Получает полку, на которой стоит книга (по названию книги).
        /// </summary>
        /// <param name="title"> Название книги.</param>
        /// <returns> Полка.</returns>
        Task<List<Shelf>> GetShelfAsync(string title);

        /// <summary>
        /// Показать полки, на которых есть книги с указанной рукописью.
        /// </summary>
        /// <param name="title"> Название рукописи. </param>
        /// <returns> Полки. </returns>
        Task<List<Shelf>> GetShelvesByManucriptNameAsync(string title);

        /// <summary>
        /// Найти название книги по идентификатору.
        /// </summary>
        /// <param name="id"> Идентификатор.</param>
        /// <returns> Название книги.</returns>
        Task<string?> GetTitleAsync(Guid id);
    }
}
