// <copyright file="AuthorRepository.cs" company="Васильева Марина Алексеевна">
// Copyright (c) Васильева Марина Алексеевна 2024. Library.
// </copyright>

namespace Repository
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using DataAccessLayer;
    using Domain;
    using Microsoft.EntityFrameworkCore;

    /// <summary>
    /// Репозиторий для класса <see cref="Domain.Author"/>.
    /// </summary>
    public sealed class AuthorRepository : BaseRepository<Author>
    {
        /// <summary>
        /// Инициализирует новый экземпляр класса <see cref="AuthorRepository"/>.
        /// </summary>
        /// <param name="dataContext">Контекст доступа к данным.</param>
        /// <exception cref="ArgumentNullException">
        /// В случае если <paramref name="dataContext"/> – <see langword="null"/>.
        /// </exception>
        public AuthorRepository(DataContext dataContext)
            : base(dataContext)
        {
        }

        /// <summary>
        /// Получает всех авторов.
        /// </summary>
        /// <returns>Авторы.</returns>
        public override IQueryable<Author> GetAll()
        {
            return this.DataContext.Authors
                .Include(author => author.Books);
        }

        /// <summary>
        /// Найти идентификатор автора по его фамилии.
        /// </summary>
        /// <param name="familyName">Фамилия автора.</param>
        /// <returns>Идентификатор.</returns>
        public Guid? GetIdByName(string familyName)
        {
            return this.Find(author => author.FullName.FamilyName == familyName)?.Id;
        }

        /// <summary>
        /// Получить список книг автора по идентификатору.
        /// </summary>
        /// <param name="id">Идентификатор.</param>
        /// <returns>Книги автора.</returns>
        public ISet<Book> GetBooksByAuthorId(Guid id) => this.Get(id)?.Books ?? new HashSet<Book>();

        /// <summary>
        /// Показать соавторов указанного автора.
        /// </summary>
        /// <param name="id">Идентификатор автора.</param>
        /// <returns>Соавторов данного автора.</returns>
        public ISet<Author> GetCoAuthors(Guid id)
        {
            return this.GetBooksByAuthorId(id)
                .SelectMany(book => book.Authors)
                .ToHashSet();
        }
    }
}
