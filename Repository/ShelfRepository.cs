// <copyright file="ShelfRepository.cs" company="Васильева Марина Алексеевна">
// Copyright (c) Васильева Марина Алексеевна 2024. Library.
// </copyright>

namespace Repository
{
    using System;
    using System.Linq;
    using DataAccessLayer;
    using Domain;

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

        /// <inheritdoc/>
        // @NOTE: IgnoreAutoIncludes()
        public override IQueryable<Shelf> GetAll() => this.DataContext.Shelves;

        /// <summary>
        /// Показать количество книг, стоящих на данной полке.
        /// </summary>
        /// <param name="id">Идентификатор полки.</param>
        /// <returns> Количество книг.</returns>
        public int? GetCountBooks(Guid id) => this.Get(id)?.Books.Count;

        /// <summary>
        /// Показать количество книг, стоящих на полке.
        /// </summary>
        /// <param name="name">Название полки.</param>
        /// <returns>Количество книг.</returns>
        public int? GetCountBooks(string name)
        {
            var id = this.GetIdByName(name);

            return id.HasValue
                ? this.GetCountBooks(id.Value)
                : null;
        }

        /// <summary>
        /// Найти идентификатор по имени.
        /// </summary>
        /// <param name="name">Название полки.</param>
        /// <returns>Идентификатор.</returns>
        public Guid? GetIdByName(string name) => this.Find(shelf => shelf.Name == name)?.Id;
    }
}
