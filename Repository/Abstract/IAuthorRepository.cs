// <copyright file="IAuthorRepository.cs" company="Филипченко Марина Алексеевна">
// Copyright (c) Филипченко Марина Алексеевна 2026. Library.
// </copyright>

namespace Repository.Abstract
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Domain;

    /// <summary>
    /// Интерфейс для репозитория Автор.
    /// </summary>
    public interface IAuthorRepository
    {
        /// <summary>
        /// Найти идентификатор автора по его фамилии.
        /// </summary>
        /// <param name="familyName"> Фамилия автора.</param>
        /// <returns> Идентификатор.</returns>
        public Task<Guid?> GetIdByNameAsync(string familyName);

        /// <summary>
        /// Получить список книг автора по идентификатору.
        /// </summary>
        /// <param name="id"> Идентификатор автора.</param>
        /// <returns> Книги автора.</returns>
        public Task<ISet<Manuscript>> GetBooksByAuthorId(Guid id);

        /// <summary>
        /// Показать соавторов указанного автора.
        /// </summary>
        /// <param name="id"> Идентификатор автора.</param>
        /// <returns> Соавторов данного автора.</returns>
        public Task<ISet<Author>> GetCoAuthorsAsync(Guid id);
    }
}