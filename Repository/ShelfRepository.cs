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
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Query;
    using Repository.Abstract;

    /// <summary>
    /// Репозиторий для класса <see cref="Domain.Shelf"/>.
    /// </summary>
    public sealed class ShelfRepository : BaseRepository<Shelf>
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

        /// <summary>
        /// Показать количество книг, стоящих на данной полке.
        /// </summary>
        /// <param name="id">Идентификатор полки.</param>
        /// <returns> Количество книг.</returns>
        public async Task<int?> GetBooksCountAsync(Guid id) => (await this.GetAsync(id))?.Books.Count;

        /// <summary>
        /// Показать количество книг, стоящих на полке.
        /// </summary>
        /// <param name="name"> Название полки.</param>
        /// <returns> Количество книг.</returns>
        public async Task<int?> GetCountBooksAsync(string name)
        {
            return (await this.GetAll()
                .FirstOrDefaultAsync(shelf => shelf.Name.Value == name))
                ?.Books
                .Count;
        }

        /// <summary>
        /// Найти идентификатор по имени.
        /// </summary>
        /// <param name="name"> Название полки.</param>
        /// <returns> Идентификатор.</returns>
        public async Task<Guid?> GetIdByName(string name)
        {
            return (await this.FindAsync(shelf => shelf.Name.Value == name))
                ?.Id;
        }

        /// <inheritdoc/>
        // @NOTE: IgnoreAutoIncludes()
        protected override IQueryable<Shelf> GetAll()
        {
            return this.DataContext.Shelves;
        }
    }
}
