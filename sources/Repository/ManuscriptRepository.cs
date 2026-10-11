// <copyright file="ManuscriptRepository.cs" company="Филипченко Марина Алексеевна">
// Copyright (c) Филипченко Марина Алексеевна 2026. Library.
// </copyright>
namespace Repository
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using DataAccessLayer;
    using Domain;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.Logging;
    using Repository.Abstract;

    /// <summary>
    /// Репозиторий для класса <see cref="Manuscript"/>.
    /// </summary>
    public sealed class ManuscriptRepository : BaseRepository<Manuscript, ManuscriptRepository>, IManuscriptRepository
    {
        /// <summary>
        /// Инициализирует новый экземпляр класса <see cref="ManuscriptRepository"/>.
        /// </summary>
        /// <param name="dataContext"> Контекст доступа к БД.</param>
        /// <param name="logger"> Логгер. </param>
        /// <exception cref="ArgumentNullException">
        /// В случае если <paramref name="dataContext"/> или <paramref name="logger"/> – <see langword="null"/>.
        /// </exception>
        public ManuscriptRepository(DataContext dataContext, ILogger<ManuscriptRepository> logger)
            : base(dataContext, logger)
        {
        }

        /// <summary>
        /// Получает список авторов по идентификатору.
        /// </summary>
        /// <param name="id"> Идентификатор книги.</param>
        /// <returns> Список авторов.</returns>
        public async Task<ISet<Author>> GetAuthorsAsync(Guid id)
        {
            var manuscript = await this.FindAsync(manuscript => manuscript.Id == id, true);

            return manuscript?.Authors
                ?? new HashSet<Author>();
        }

        /// <summary>
        /// Показать все книги, написанные авторами выбранной рукописи.
        /// </summary>
        /// <param name="id"> Идентификатор рукописи.</param>
        /// <returns> Множество книг авторов данной рукописи.</returns>
        public async Task<ISet<Manuscript>?> GetAllBooksCoAuthors(Guid id)
        {
            var authors = await this.GetAuthorsAsync(id);
            if (authors is null || authors.Count == 0)
            {
                return new HashSet<Manuscript>();
            }

            var authorIds = authors.Select(a => a.Id).ToList();
            return await this.DataContext.Manuscripts
                .Include(m => m.Authors)
                .Where(m => m.Id != id && m.Authors.Any(a => authorIds.Contains(a.Id)))
                .ToHashSetAsync();
        }

        /// <inheritdoc/>
        protected override IQueryable<Manuscript> GetAll(bool track = false)
        {
            var result = this.DataContext.Manuscripts
                .Include(m => m.Authors)
                    .ThenInclude(a => a.Person)
                .Include(m => m.Translators)
                    .ThenInclude(t => t.Person)
                .Include(m => m.Genres)
                .Include(m => m.Books);

            if (!track)
            {
                result.AsNoTracking();
            }

            return result;
        }
    }
}
