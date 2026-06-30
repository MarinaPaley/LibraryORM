// <copyright file="IManuscriptRepository.cs" company="Филипченко Марина Алексеевна">
// Copyright (c) Филипченко Марина Алексеевна 2026. Library.
// </copyright>
namespace Repository.Abstract
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Domain;

    /// <summary>
    /// Интерфейс для репозитория рукописей.
    /// </summary>
    public interface IManuscriptRepository
    {
        /// <summary>
        /// Получает список авторов по идентификатору.
        /// </summary>
        /// <param name="id"> Идентификатор книги.</param>
        /// <returns> Список авторов.</returns>
        public Task<ISet<Author>> GetAuthorsAsync(Guid id);

        /// <summary>
        /// Показать все книги, написанные авторами выбранной рукописи.
        /// </summary>
        /// <param name="id"> Идентификатор рукописи.</param>
        /// <returns> Множество книг авторов данной рукописи.</returns>
        public Task<ISet<Manuscript>?> GetAllBooksCoAuthors(Guid id);
    }
}