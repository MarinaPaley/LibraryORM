// <copyright file="BookRepository.cs" company="Васильева Марина Алексеевна">
// Copyright (c) Васильева Марина Алексеевна 2024. Library.
// </copyright>

namespace Repository.Tests
{
    using System;
    using System.Linq;
    using DataAccessLayer;
    using Domain;

    /// <summary>
    /// Инициализирует новый экземпляр класса <see cref="BookRepository"/>.
    /// </summary>
    /// <exception cref="ArgumentNullException">
    /// В случае если <paramref title="dataContext"/> – <see langword="null"/>.
    /// </exception>
    public sealed class BookRepository : BaseRepository<Book>
    {
        public BookRepository(DataContext dataContext)
            : base(dataContext)
        {
        }

        public string? GetTitle(Guid id) => this.Find(book => book.Id == id)?.Title;

        public Guid? GetId(string title) => this.Find(book => book.Title == title)?.Id;

        public Shelf? GetShelf(string title)
        {
            var id = this.GetId(title);
            return id.HasValue
                ? this.Get(id.Value)?.Shelf
                : null;
        }

        public override IQueryable<Book> GetAll() => this.DataContext.Books;
    }
}
