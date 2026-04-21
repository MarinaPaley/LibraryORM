// <copyright file="BookRepository.cs" company="Филипченко Марина Алексеевна">
// Copyright (c) Филипченко Марина Алексеевна 2026. Library.
// </copyright>
namespace Repository
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
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
        public string? GetTitle(Guid id) => this.Find(book => book.Id == id)?.Title.Value;

        /// <summary>
        /// Получает идентификатор по названию книги.
        /// </summary>
        /// <param name="title"> Название книги. </param>
        /// <returns> Идентификатор. </returns>
        public Guid? GetId(string title)
            => this.Find(book => book.Title.Value == title)?.Id;

        /// <summary>
        /// Получает полку, на которой стоит книга.
        /// </summary>
        /// <param name="title">Название книги.</param>
        /// <returns>Полка.</returns>
        public Shelf? GetShelf(string title)
        {
            var id = this.GetId(title);
            return id.HasValue
                ? this.Get(id.Value)?.Shelf
                : null;
        }

        /// <summary>
        /// Получает список авторов по идентификатору.
        /// </summary>
        /// <param name="id">Идентификатор книги.</param>
        /// <returns>Список авторов.</returns>
        public ISet<Author>? GetAuthors(Guid id) => this.Find(book => book.Id == id)?.Authors;

        /// <summary>
        /// Показать все книги, написанные авторами выбранной книги.
        /// </summary>
        /// <param name="id">Идентификатор книги.</param>
        /// <returns>Множество книг авторов данной книги.</returns>
        public ISet<Book>? GetAllBooksCoAuthors(Guid id) =>
            this.GetAuthors(id)?
            .SelectMany(author => author.Books)
            .ToHashSet();

        /// <summary>
        /// Получает все книги.
        /// </summary>
        /// <returns>Книги.</returns>
        protected override IQueryable<Book> GetAll()
        {
            return this.DataContext.Books
                .Include(book => book.Authors)
                    .ThenInclude(author => author.Books)
                .Include(book => book.Shelf);
        }
    }
}
