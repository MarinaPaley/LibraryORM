// <copyright file="BaseRepository.cs" company="Филипченко Марина Алексеевна">
// Copyright (c) Филипченко Марина Алексеевна 2026. Library.
// </copyright>

namespace Repository.Abstract
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Linq.Expressions;
    using System.Threading.Tasks;
    using DataAccessLayer;
    using Domain.Abstract;
    using Microsoft.EntityFrameworkCore;

    /// <summary>
    /// Базовый класс репозиториев.
    /// </summary>
    /// <typeparam name="TEntity"> Целевой тип сущности. </typeparam>
    public abstract class BaseRepository<TEntity> : IRepository<TEntity>
        where TEntity : class, IEntity
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
        public async Task<TEntity> CreateAsync(TEntity entity, bool saveNow = true)
        {
            var result = this.DataContext.Add(entity).Entity;
            _ = this.SaveAsync(saveNow);
            return result;
        }

        /// <inheritdoc/>
        public async Task<bool> DeleteAsync(TEntity entity, bool saveNow = true)
        {
            try
            {
                _ = this.DataContext.Remove(entity);
                return await this.SaveAsync(saveNow) != 0;
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
        public async Task<TEntity?> FindAsync(Expression<Func<TEntity, bool>> predicate)
        {
            return await this.GetAll().FirstOrDefaultAsync(predicate);
        }

        /// <inheritdoc/>
        public async Task<TEntity?> GetAsync(Guid id) => await this.GetAll().SingleOrDefaultAsync(entity => entity.Id == id);

        /// <inheritdoc/>
        public async Task<TEntity> Update(TEntity entity, bool saveNow = true)
        {
            var result = this.DataContext.Update(entity).Entity;
            _ = this.SaveAsync(saveNow);
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
        private async Task<int> SaveAsync(bool saveNow = true)
        {
            return saveNow
                ? await this.DataContext.SaveChangesAsync()
                : 0;
        }
    }
}
