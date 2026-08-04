// <copyright file="CategoryRepository.cs" company="Филипченко Марина Алексеевна">
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
    /// Репозиторий для класса <see cref="Category"/>.
    /// </summary>
    public sealed class CategoryRepository
        : BaseRepository<Category, CategoryRepository>,
        ICategoryRepository
    {
        /// <summary>
        /// Инициализирует новый экземпляр класса <see cref="CategoryRepository"/>.
        /// </summary>
        /// <param name="dataContext"> Контекст доступа к данным.</param>
        /// <param name="logger"> Логгер. </param>
        /// <exception cref="ArgumentNullException">
        /// В случае если <paramref name="dataContext"/> или <paramref name="logger"/> – <see langword="null"/>.
        /// </exception>
        public CategoryRepository(DataContext dataContext, ILogger<CategoryRepository> logger)
            : base(dataContext, logger)
        {
        }

        /// <inheritdoc/>
        public async Task<List<string>> GetCategories()
        {
            return await this.GetAll()
                .Select(category => category.Name.Value)
                .ToListAsync();
        }

        /// <inheritdoc/>
        public async Task<Category?> GetCategoryAsync(Guid id)
        {
            return await this.GetAll()
                .SingleOrDefaultAsync(category => category.Id == id);
        }

        /// <inheritdoc/>
        public async Task<string?> GetCategoryNameAsync(Guid id)
        {
            return (await this.GetCategoryAsync(id))
                ?.Name.Value;
        }

        /// <inheritdoc/>
        public async Task<ColorCode?> GetColorCodeAsync(Guid id)
        {
            return (await this.GetCategoryAsync(id))
                ?.Color.Code;
        }

        /// <inheritdoc/>
        public async Task<ColorCode?> GetColorCodeAsync(string category)
        {
            var id = await this.GetIdAsync(category);

            if (id is null)
            {
                return null;
            }

            return await this.GetColorCodeAsync(id.Value);
        }

        /// <inheritdoc/>
        public async Task<Guid?> GetIdAsync(string category)
        {
            return (await this.GetAll()
                .SingleOrDefaultAsync(value => value.Name.Value == category))
                ?.Id;
        }

        /// <inheritdoc/>
        public Task<List<Tag>> GetTagsAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        /// <inheritdoc/>
        public Task<List<Tag>> GetTagsAsync(string category)
        {
            throw new NotImplementedException();
        }

        /// <inheritdoc/>
        protected override IQueryable<Category> GetAll(bool track = false)
        {
            var result = this.DataContext.Categories
                .Include(category => category.Color)
                .Include(category => category.Tags);

            if (!track)
            {
                result.AsNoTracking();
            }

            return result;
        }
    }
}

