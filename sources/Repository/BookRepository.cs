// <copyright file="BookRepository.cs" company="Филипченко Марина Алексеевна">
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
    /// Репозиторий для класса <see cref="Book"/>.
    /// </summary>
    public sealed class BookRepository : BaseRepository<Book, BookRepository>, IBookRepository
    {
        /// <summary>
        /// Инициализирует новый экземпляр класса <see cref="BookRepository"/>.
        /// </summary>
        /// <param name="dataContext"> Контекст доступа к данным.</param>
        /// <param name="logger"> Логгер. </param>
        /// <exception cref="ArgumentNullException">
        /// В случае если <paramref name="dataContext"/> или <paramref name="logger"/> – <see langword="null"/>.
        /// </exception>
        public BookRepository(DataContext dataContext, ILogger<BookRepository> logger)
            : base(dataContext, logger)
        {
        }

        /// <inheritdoc/>
        public async Task<string?> GetTitleAsync(Guid id)
        {
            return (await this.FindAsync(book => book.Id == id))?.Title;
        }

        /// <inheritdoc/>
        public async Task<Guid?> GetIdAsync(string title)
            => (await this.FindAsync(book => book.Title == title))?.Id;

        /// <inheritdoc/>
        public Task<List<Shelf>> GetShelfAsync(string title)
        {
            return
             this.GetAll()
                .Where(book => book.Title == title)
                    .SelectMany(book => book.Items)
                        .Select(item => item.Shelf)
                        .OfType<Shelf>()
                .ToListAsync();
        }

        /// <inheritdoc/>
        public Task<List<Shelf>> GetShelvesByManucriptNameAsync(string title)
        {
            return this.GetAll()
                .Where(book => book.Manuscripts.Any(manuscript => manuscript.Name.Value == title))
                .SelectMany(book => book.Items)
                .Select(item => item.Shelf)
                .OfType<Shelf>()
                .ToListAsync();
        }

        /// <inheritdoc/>
        protected override IQueryable<Book> GetAll(bool track = false)
        {
            var result = this.DataContext.Books
                .Include(book => book.Manuscripts)
                    .ThenInclude(manuscript => manuscript.Authors)
                .Include(book => book.Items)
                    .ThenInclude(item => item.Shelf);

            if (!track)
            {
                result.AsNoTracking();
            }

            return result;
        }
    }
}
