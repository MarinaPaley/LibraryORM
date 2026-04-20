// <copyright file="IRepository.cs" company="Филипченко Марина Алексеевна">
// Copyright (c) Филипченко Марина Алексеевна 2026. Library.
// </copyright>

namespace Repository.Abstract
{
    using System;
    using System.Collections.Generic;
    using System.Linq.Expressions;
    using Domain.Abstract;

    /// <summary>
    /// Интерфейс репозитория.
    /// </summary>
    /// <typeparam name="TEntity"> Целевой тип сущности. </typeparam>
    public interface IRepository<TEntity>
        where TEntity : class, IEntity<TEntity>
    {
        /// <summary>
        /// Находит сущность по идентификатору.
        /// </summary>
        /// <param name="id"> Идентификатор сущности. </param>
        /// <returns> Сущность. </returns>
        TEntity? Get(Guid id);

        /// <summary>
        /// Обновдение сущности.
        /// </summary>
        /// <param name="entity"> Сущность. </param>
        /// <param name="saveNow"> Надо ли сохранять сущность после изменения. </param>
        /// <returns> Контекст доступа к сущности.</returns>
        TEntity Update(TEntity entity, bool saveNow = true);

        /// <summary>
        /// Удаление сущности.
        /// </summary>
        /// <param name="entity"> Сущность. </param>
        /// <param name="saveNow"> Надо ли сохранять сущность после изменения. </param>
        /// <returns> <see langword="true"/>, если удалили, иначе - <see langword="false"/>.</returns>
        bool Delete(TEntity entity, bool saveNow = true);

        /// <summary>
        /// Создает сущность.
        /// </summary>
        /// <param name="entity"> Сущность.</param>
        /// <param name="saveNow"> Надо ли сохранять сущность после изменения. </param>
        /// <returns> Контекст доступа к сущности.</returns>
        TEntity Create(TEntity entity, bool saveNow = true);

        /// <summary>
        /// Поиск множества сущностей по предикату (<paramref name="predicate"/>).
        /// </summary>
        /// <param name="predicate"> Предикат, которому должна удовлетворять сушность.</param>
        /// <returns> Множество (<see cref="IEnumerable{TEntity}"/>) всех сущностей.</returns>
        IEnumerable<TEntity> Filter(Expression<Func<TEntity, bool>>? predicate = null);

        /// <summary>
        /// Поиск сущности по предикату (<paramref name="predicate"/>).
        /// </summary>
        /// <param name="predicate"> Предикат, которому должна удовлетворять сущность.</param>
        /// <returns> Сущность или <see langword="null"/>.</returns>
        TEntity? Find(Expression<Func<TEntity, bool>> predicate);
    }
}
