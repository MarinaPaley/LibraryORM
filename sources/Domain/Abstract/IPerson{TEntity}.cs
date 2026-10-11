// <copyright file="IPerson{TEntity}.cs" company="Филипченко Марина Алексеевна">
// Copyright (c) Филипченко Марина Алексеевна 2026. Library.
// </copyright>

namespace Domain.Abstract
{
    /// <summary>
    /// Интерфейс сущности, агрегирующей класс <see cref="Person"/>.
    /// </summary>
    /// <typeparam name="TEntity"> ТИп конкретной сущности.</typeparam>
    public interface IPerson<TEntity> : IEntity<TEntity>
        where TEntity : class, IPerson<TEntity>
    {
        /// <summary>
        /// Персона.
        /// </summary>
        public Person Person { get; set; }
    }
}