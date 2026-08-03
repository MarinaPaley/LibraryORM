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
    using Microsoft.Extensions.Logging;

    /// <summary>
    /// Базовый класс репозиториев.
    /// </summary>
    /// <typeparam name="TEntity"> Целевой тип сущности. </typeparam>
    /// <typeparam name="TRepository"> Целевой тип репозитория. </typeparam>
    public abstract class BaseRepository<TEntity, TRepository> : IRepository<TEntity>
        where TEntity : class, IEntity
        where TRepository : BaseRepository<TEntity, TRepository>
    {
        /// <summary>
        /// Инициализирует новый экземпляр класса <see cref="BaseRepository{TEntity,TRepository}"/>.
        /// </summary>
        /// <param name="dataContext"> Контекст доступа к данным. </param>
        /// <param name="logger"> Логгер. </param>
        /// <exception cref="ArgumentNullException">
        /// В случае если <paramref name="dataContext"/> или <paramref name="logger"/> – <see langword="null"/>.
        /// </exception>
        protected BaseRepository(DataContext dataContext, ILogger<TRepository> logger)
        {
            this.DataContext = dataContext
                ?? throw new ArgumentNullException(nameof(dataContext));

            this.Logger = logger
                ?? throw new ArgumentNullException(nameof(logger));
        }

        /// <summary>
        /// Контекст доступа к данным.
        /// </summary>
        protected DataContext DataContext { get; }

        /// <summary>
        /// Логгер.
        /// </summary>
        protected ILogger<TRepository> Logger { get; }

        /// <inheritdoc/>
        public async Task<TEntity> CreateAsync(TEntity entity, bool saveNow = true)
        {
            var result = this.DataContext.Add(entity).Entity;
            _ = await this.SaveAsync(saveNow);
            return result;
        }

        /// <inheritdoc/>
        public async Task<bool> DeleteAsync(Guid id, bool saveNow = true)
        {
            this.Logger.LogTrace("Начинаем удалять сущность с ID = {Id}.", id);

            try
            {
                var existing = await this.FindAsync(e => e.Id == id, true);
                if (existing is null)
                {
                    this.Logger.LogWarning(
                        "Entity with ID = {Id} was not found while deleting.",
                        id);

                    return false;
                }

                var entry = this.DataContext.Entry(existing);
                if (entry.State == EntityState.Detached)
                {
                    this.DataContext.Attach(existing);
                }

                _ = this.DataContext.Remove(existing);
                return await this.SaveAsync(saveNow) != 0;
            }
            catch (Exception exception)
            {
                this.Logger.LogError(
                    exception,
                    "Error while deleting entity with ID = {Id}.",
                    id);

                return false;
            }
        }

        /// <inheritdoc/>
        public async Task<bool> DeleteAsync(TEntity entity, bool saveNow = true)
        {
            if (entity is null)
            {
                return false;
            }

            return await this.DeleteAsync(entity.Id, saveNow);
        }

        /// <inheritdoc/>
        public IEnumerable<TEntity> Filter(Expression<Func<TEntity, bool>>? predicate = null)
        {
            return predicate is not null
                ? this.GetAll().Where(predicate)
                : this.GetAll();
        }

        /// <inheritdoc/>
        public async Task<TEntity?> FindAsync(Expression<Func<TEntity, bool>> predicate, bool track = false)
        {
            return await this.GetAll(track).FirstOrDefaultAsync(predicate);
        }

        /// <inheritdoc/>
        public async Task<TEntity?> GetAsync(Guid id, bool track = false)
        {
            return await this.FindAsync(entity => entity.Id == id, track);
        }

        /// <inheritdoc/>
        public async Task<TEntity> UpdateAsync(TEntity entity, bool saveNow = true)
        {
            var result = this.DataContext.Update(entity).Entity;
            _ = await this.SaveAsync(saveNow);
            return result;
        }

        /// <summary>
        /// Получение всех сущностей.
        /// </summary>
        /// <param name="track"> Отслеживать ли изменения?. </param>
        /// <returns> Множество (<see cref="IQueryable{TEntity}"/>) всех сущностей.</returns>
        protected abstract IQueryable<TEntity> GetAll(bool track = false);

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
