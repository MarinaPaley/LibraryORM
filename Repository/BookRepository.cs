// <copyright file="BookRepository.cs" company="Васильева Марина Алексеевна">
// Copyright (c) Васильева Марина Алексеевна 2024. Library.
// </copyright>
namespace Repository
{
    using System;
    using System.Linq;
    using DataAccessLayer;
    using Domain;

    /// <summary>
    /// Репозиторий для класса <see cref="Domain.Book"/>.
    /// </summary>
    public sealed class BookRepository : BaseRepository<Book>
    {
        /// <summary>
        /// Инициализирует новый экземпляр класса <see cref="BookRepository"/>.
        /// </summary>
        /// <param name="dataContext">Контекст доступа к данным.</param>
        /// /// <exception cref="ArgumentNullException">
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
        public string? GetTitle(Guid id) => this.Find(book => book.Id == id)?.Title;

        /// <summary>
        /// Получает идентификатор по названию книги.
        /// </summary>
        /// <param name="title">Название книги.</param>
        /// <returns>Идентификатор.</returns>
        public Guid? GetId(string title) => this.Find(book => book.Title == title)?.Id;

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
        /// Получает все книги.
        /// </summary>
        /// <returns>Книги.</returns>
        public override IQueryable<Book> GetAll() => this.DataContext.Books;
    }
}
