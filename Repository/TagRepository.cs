// <copyright file="TagRepository.cs" company="Филипченко Марина Алексеевна">
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
    /// Репозиторий для класса <see cref="Tag"/>.
    /// </summary>
    public sealed class TagRepository : BaseRepository<Tag, TagRepository>, ITagRepository
    {
        /// <summary>
        /// Инициализирует новый экземпляр класса <see cref="TagRepository"/>.
        /// </summary>
        /// <param name="dataContext"> Контекст доступа к данным.</param>
        /// <param name="logger"> Логгер. </param>
        /// <exception cref="ArgumentNullException">
        /// В случае если <paramref name="dataContext"/> или <paramref name="logger"/> – <see langword="null"/>.
        /// </exception>
        public TagRepository(DataContext dataContext, ILogger<TagRepository> logger)
            : base(dataContext, logger)
        {
        }

        /// <inheritdoc/>
        public async Task<IEnumerable<Book>> GetBooksAsync(string tag)
        {
            var id = await this.GetIdAsync(tag);
            if (id is null)
            {
                return new List<Book>();
            }

            return await this.GetBooksAsync(id.Value);
        }

        /// <inheritdoc/>
        public async Task<IEnumerable<Book>> GetBooksAsync(Guid id)
        {
            var tag = await this.GetTagAsync(id);
            if (tag is null)
            {
                return new List<Book>();
            }

            return tag.Books;
        }

        /// <inheritdoc/>
        public async Task<Category?> GetCategoryAsync(string tag)
        {
            var id = await this.GetIdAsync(tag);
            if (id is null)
            {
                return null;
            }

            return await this.GetCategoryAsync(id.Value);
        }

        /// <inheritdoc/>
        public async Task<Category?> GetCategoryAsync(Guid id)
        {
            return (await this.GetTagAsync(id))
                ?.Category;
        }

        /// <inheritdoc/>
        public async Task<Guid?> GetIdAsync(string tag)
        {
            return (await this.GetAll()
                .SingleOrDefaultAsync(value => value.Name.Value == tag))
                ?.Id;
        }

        /// <inheritdoc/>
        public async Task<Tag?> GetTagAsync(Guid id)
        {
            return await this.GetAll()
                .SingleOrDefaultAsync(value => value.Id == id);
        }

        /// <inheritdoc/>
        public async Task<string?> GetTagNameAsync(Guid id)
        {
            return (await this.GetTagAsync(id))
                ?.Name.Value;
        }

        /// <inheritdoc/>
        public async Task<List<string>> GetTags()
        {
            return await this.GetAll()
                .Select(tag => tag.Name.Value)
                .ToListAsync();
        }

        /// <inheritdoc/>
        protected override IQueryable<Tag> GetAll(bool track = false)
        {
            var result = this.DataContext.Tags
                .Include(tag => tag.Category)
                .Include(tag => tag.Books)
                .IgnoreAutoIncludes();

            if (!track)
            {
                result.AsNoTracking();
            }

            return result;
        }
    }
}
