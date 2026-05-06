// <copyright file="AuthorRepository.cs" company="Филипченко Марина Алексеевна">
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
        /// Найти идентификатор автора по его фамилии.
        /// </summary>
        /// <param name="familyName"> Фамилия автора.</param>
        /// <returns> Идентификатор.</returns>
        public async Task<Guid?> GetIdByNameAsync(string familyName)
        {
            return (await this.FindAsync(author => author.Person.FullName.FamilyName == familyName))?.Id;
        }

        /// <summary>
        /// Получить список книг автора по идентификатору.
        /// </summary>
        /// <param name="id"> Идентификатор автора.</param>
        /// <returns> Книги автора.</returns>
        public async Task<ISet<Manuscript>> GetBooksByAuthorId(Guid id)
        {
            return (await this.GetAsync(id))?.Manuscripts
                ?? new HashSet<Manuscript>();
        }

        /// <summary>
        /// Показать соавторов указанного автора.
        /// </summary>
        /// <param name="id"> Идентификатор автора.</param>
        /// <returns> Соавторов данного автора.</returns>
        public async Task<ISet<Author>> GetCoAuthorsAsync(Guid id)
        {
            return await this.GetAll()
                .Where(coAuthor =>
                    coAuthor.Id != id &&
                    coAuthor.Manuscripts.Any(m => m.Authors.Any(a => a.Id == id)))
                .Include(a => a.Person)
                .ToHashSetAsync();
        }

        /// <summary>
        /// Получает всех авторов.
        /// </summary>
        /// <returns> Авторы.</returns>
        protected override IQueryable<Author> GetAll()
        {
            return this.DataContext.Authors
                .Include(author => author.Person)
                    .ThenInclude(person => person.FullName)
                .Include(author => author.Manuscripts);
        }
    }
}
