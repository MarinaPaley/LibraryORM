// <copyright file="IEntity{TEntity}.cs" company="Филипченко Марина Алексеевна">
// Copyright (c) Филипченко Марина Алексеевна 2026. Library.
// </copyright>

namespace Domain.Abstract
{
    /// <summary>
    /// Интерфейс базовой сущности.
    /// </summary>
    /// <typeparam name="TEntity"> Тип конкретной сущности. </typeparam>
    public interface IEntity<TEntity> : IEntity
        where TEntity : class, IEntity<TEntity>
    {
    }
}
