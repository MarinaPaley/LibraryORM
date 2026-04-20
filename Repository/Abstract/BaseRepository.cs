// <copyright file="BaseRepository.cs" company="Филипченко Марина Алексеевна">
// Copyright (c) Филипченко Марина Алексеевна 2026. Library.
// </copyright>

namespace Repository.Abstract
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Linq.Expressions;
    using DataAccessLayer;
    using Domain.Abstract;

    /// <summary>
    /// Базовый класс репозиториев.
    /// </summary>
    /// <typeparam name="TEntity"> Целевой тип сущности. </typeparam>
    public abstract class BaseRepository<TEntity>
        : IRepository<TEntity>
        where TEntity : class, IEntity<TEntity>
    {
        /// <summary>
        /// Инициализирует новый экземпляр класса <see cref="BaseRepository{TEntity}"/>.
        /// </summary>
        /// <param name="dataContext">Контекст доступа к данным.</param>
        protected BaseRepository(DataContext dataContext)
        {
            this.DataContext = dataContext
                ?? throw new ArgumentNullException(nameof(dataContext));
        }

        /// <summary>
        /// Контекст доступа к данным.
        /// </summary>
        protected DataContext DataContext { get; }

        /// <inheritdoc/>
        public TEntity Create(TEntity entity, bool saveNow = true)
        {
            var result = this.DataContext.Add(entity).Entity;
            _ = this.Save(saveNow);
            return result;
        }

        /// <inheritdoc/>
        public bool Delete(TEntity entity, bool saveNow = true)
        {
            try
            {
                _ = this.DataContext.Remove(entity);
                return this.Save(saveNow) != 0;
            }
            catch
            {
                // @TODO: Что-то залогировать через логгер (добавить!).
                return false;
            }
        }

        /// <inheritdoc/>
        public IEnumerable<TEntity> Filter(Expression<Func<TEntity, bool>>? predicate = null)
            => predicate is not null
            ? this.GetAll().Where(predicate)
            : this.GetAll();

        /// <inheritdoc/>
        public TEntity? Find(Expression<Func<TEntity, bool>> predicate) => this.GetAll().FirstOrDefault(predicate);

        /// <inheritdoc/>
        public TEntity? Get(Guid id) => this.GetAll().SingleOrDefault(entity => entity.Id == id);

        /// <inheritdoc/>
        public TEntity Update(TEntity entity, bool saveNow = true)
        {
            var result = this.DataContext.Update(entity).Entity;
            _ = this.Save(saveNow);
            return result;
        }

        /// <summary>
        /// Получение всех сущностей.
        /// </summary>
        /// <returns> Множество (<see cref="IQueryable{TEntity}"/>) всех сущностей.</returns>
        protected abstract IQueryable<TEntity> GetAll();

        /// <summary>
        /// Сохраняет контекст в БД.
        /// </summary>
        /// <param name="saveNow"> Надо ли сохранять сущность после изменения. </param>
        /// <returns> Количество измененных сущностей. </returns>
        private int Save(bool saveNow = true)
        {
            return saveNow
                ? this.DataContext.SaveChanges()
                : 0;
        }
    }
}
