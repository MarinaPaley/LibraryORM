// <copyright file="AuthorRepository.cs" company="Филипченко Марина Алексеевна">
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
    /// Репозиторий для класса <see cref="Author"/>.
    /// </summary>
    public sealed class AuthorRepository : BaseRepository<Author, AuthorRepository>, IAuthorRepository
    {
        /// <summary>
        /// Инициализирует новый экземпляр класса <see cref="AuthorRepository"/>.
        /// </summary>
        /// <param name="dataContext">Контекст доступа к данным.</param>
        /// <param name="logger"> Логгер. </param>
        /// <exception cref="ArgumentNullException">
        /// В случае если <paramref name="dataContext"/> или <paramref name="logger"/> – <see langword="null"/>.
        /// </exception>
        public AuthorRepository(DataContext dataContext, ILogger<AuthorRepository> logger)
            : base(dataContext, logger)
        {
        }

        /// <inheritdoc/>
        public async Task<Guid?> GetIdByNameAsync(string familyName)
        {
            return (await this.FindAsync(author => author.Person.FullName.FamilyName == familyName))?.Id;
        }

        /// <inheritdoc/>
        public async Task<ISet<Manuscript>> GetManuscriptsByAuthorId(Guid id)
        {
            return (await this.GetAsync(id))?.Manuscripts
                ?? new HashSet<Manuscript>();
        }

        /// <inheritdoc/>
        public async Task<ISet<Author>> GetCoAuthorsAsync(Guid id)
        {
            return await this.GetAll()
                .Where(coAuthor =>
                    coAuthor.Id != id &&
                    coAuthor.Manuscripts.Any(m => m.Authors.Any(a => a.Id == id)))
                .Include(a => a.Person)
                .ToHashSetAsync();
        }

        /// <inheritdoc/>
        protected override IQueryable<Author> GetAll(bool track = false)
        {
            var result = this.DataContext.Authors
                .Include(author => author.Person)
                    .ThenInclude(person => person.FullName)
                .Include(author => author.Manuscripts);

            if (!track)
            {
                result.AsNoTracking();
            }

            return result;
        }
    }
}
