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
    using Repository.Abstract;

    /// <summary>
    /// Репозиторий для класса <see cref="Book"/>.
    /// </summary>
    public sealed class BookRepository : BaseRepository<Book>, IBookRepository
    {
        /// <summary>
        /// Инициализирует новый экземпляр класса <see cref="BookRepository"/>.
        /// </summary>
        /// <param name="dataContext"> Контекст доступа к данным.</param>
        /// <exception cref="ArgumentNullException">
        /// В случае если <paramref name="dataContext"/> – <see langword="null"/>.
        /// </exception>
        public BookRepository(DataContext dataContext)
            : base(dataContext)
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

        /// <summary>
        /// Получает все книги.
        /// </summary>
        /// <returns> Книги.</returns>
        protected override IQueryable<Book> GetAll()
        {
            return this.DataContext.Books
                .Include(book => book.Manuscripts)
                    .ThenInclude(author => author.Books)
                .Include(book => book.Items)
                    .ThenInclude(item => item.Shelf);
        }
    }
}
