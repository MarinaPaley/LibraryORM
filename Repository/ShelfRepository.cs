// <copyright file="ShelfRepository.cs" company="Филипченко Марина Алексеевна">
// Copyright (c) Филипченко Марина Алексеевна 2026. Library.
// </copyright>

namespace Repository
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;
    using DataAccessLayer;
    using Domain;
    using Repository.Abstract;

    /// <summary>
    /// Репозиторий для класса <see cref="Shelf"/>.
    /// </summary>
    public sealed class ShelfRepository : BaseRepository<Shelf>, IShelfRepository
    {
        /// <summary>
        /// Инициализирует новый экземпляр класса <see cref="ShelfRepository"/>.
        /// </summary>
        /// <param name="dataContext"> Контекст доступа к данным.</param>
        /// <exception cref="ArgumentNullException">
        /// В случае если <paramref name="dataContext"/> – <see langword="null"/>.
        /// </exception>
        public ShelfRepository(DataContext dataContext)
            : base(dataContext)
        {
        }

        /// <inheritdoc/>
        public async Task<int?> GetCountBooksAsync(Guid id)
        {
            return (await this.GetAsync(id))
                ?.Items
                .Count;
        }

        /// <inheritdoc/>
        public async Task<int?> GetCountBooksAsync(string name)
        {
            var id = await this.GetIdByName(name);

            return id.HasValue
                ? await this.GetCountBooksAsync(id.Value)
                : null;
        }

        /// <inheritdoc/>
        public async Task<Guid?> GetIdByName(string name)
        {
            var result = await this.FindAsync(shelf => shelf.Name == new Title(name));

            return result?.Id;
        }

        /// <inheritdoc/>
        // @NOTE: IgnoreAutoIncludes()
        protected override IQueryable<Shelf> GetAll()
        {
            return this.DataContext.Shelves;
        }
    }
}
