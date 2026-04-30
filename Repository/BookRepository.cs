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
    public sealed class BookRepository : BaseRepository<Book>
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

        /// <summary>
        /// Найти название книги по идентификатору.
        /// </summary>
        /// <param name="id">Идентификатор.</param>
        /// <returns>Название книги.</returns>
        public async Task<string?> GetTitleAsync(Guid id) => (await this.FindAsync(book => book.Id == id))?.Title;

        /// <summary>
        /// Получает идентификатор по названию книги.
        /// </summary>
        /// <param name="title"> Название книги. </param>
        /// <returns> Идентификатор. </returns>
        public async Task<Guid?> GetIdAsync(string title)
            => (await this.FindAsync(book => book.Title == title))?.Id;

        /// <summary>
        /// Получает полку, на которой стоит книга (по названию книги).
        /// </summary>
        /// <param name="title"> Название книги.</param>
        /// <returns>Полка.</returns>
        public async Task<Shelf?> GetShelfAsync(string title)
        {
            return (await this.FindAsync(book => book.Title == title))?.Shelf;
        }

        /// <summary>
        /// Показать полки, на которых есть книги с указанной рукописью.
        /// </summary>
        /// <param name="title"> Название рукописи. </param>
        /// <returns> Полки. </returns>
        public Task<List<Shelf>> GetShelvesByManucriptNameAsync(string title)
        {
            return this.GetAll()
                .Where(book => book.Manuscripts.Any(manuscript => manuscript.Name.Value == title))
                .Select(book => book.Shelf)
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
                .Include(book => book.Shelf);
        }
    }
}
