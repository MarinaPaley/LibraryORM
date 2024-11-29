using DataAccessLayer;
using Domain;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Linq.Expressions;
// <copyright file="ShelfRepository.cs" company="Васильева Марина Алексеевна">
// Copyright (c) Васильева Марина Алексеевна 2024. Library.
// </copyright>

namespace Repository
{
    public class ShelfRepositoryBase
    {

        /// <summary>
        /// Контекст доступа к данным.
        /// </summary>
        public DataContext DataContext { get; }

        /// <summary>
        /// Создает полку.
        /// </summary>
        /// <param name="entity">Полка.</param>
        /// <returns>Контекст доступа к сущности Полка.</returns>
        public Shelf Create(Shelf entity) => this.DataContext.Add(entity).Entity;

        /// <summary>
        /// Удаляет полку.
        /// </summary>
        /// <param name="shelf">Полка.</param>
        /// <returns>Измененный контекст доступа к сущности Полка.</returns>
        public Shelf Delete(Shelf shelf) => this.DataContext.Remove(shelf).Entity;

        /// <summary>
        /// Поиск множества полок по предикату (<paramref name="predicate"/>).
        /// </summary>
        /// <param name="predicate">Предикат, которому должна удовлетворять полка.</param>
        /// <returns>Множество (<see cref="IQueryable{Shelf}"/>) всех полок.</returns>
        public IQueryable<Shelf> Filter(Expression<Func<Shelf, bool>> predicate) => this.GetAll().Where(predicate);

        /// <summary>
        /// Поиск полки по предикату (<paramref name="predicate"/>).
        /// </summary>
        /// <param name="predicate">Предикат, которому должна удовлетворять полка.</param>
        /// <returns>Полка или <see langword="null"/>.</returns>
        public Shelf? Find(Expression<Func<Shelf, bool>> predicate) => this.GetAll().FirstOrDefault(predicate);

        /// <summary>
        /// Получение конкретной полки по её идентификатору.
        /// </summary>
        /// <param name="id">Идентификатор полки.</param>
        /// <returns>Полка.</returns>
        public Shelf? Get(Guid id) => this.GetAll().SingleOrDefault(entity => entity.Id == id);

        /// <summary>
        /// Получение всех полок.
        /// </summary>
        /// <returns> Множество (<see cref="IQueryable{Shelf}"/>) всех полок.</returns>
        public IQueryable<Shelf> GetAll() => this.DataContext.Shelves.IgnoreAutoIncludes();

        /// <summary>
        /// Сохраняет контекст в БД.
        /// </summary>
        /// <returns>Количество измененных сущностей.</returns>
        public int Save() => this.DataContext.SaveChanges();

        /// <summary>
        /// Изменяет полку.
        /// </summary>
        /// <param name="shelf">Полка.</param>
        /// <returns>Измененный контекст доступа к сущности Полка.</returns>
        public Shelf Update(Shelf shelf) => this.DataContext.Update(shelf).Entity;
    }
}