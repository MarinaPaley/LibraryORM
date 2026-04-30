// <copyright file="INamedEntity{TEntity}.cs" company="Филипченко Марина Алексеевна">
// Copyright (c) Филипченко Марина Алексеевна 2026. Library.
// </copyright>

namespace Domain.Abstract
{
    /// <summary>
    /// Интерфейс базовой именованной сущности.
    /// </summary>
    /// <typeparam name="TEntity">  Тип конкретной сущности. </typeparam>
    public interface INamedEntity<TEntity> : IEntity<TEntity>, INamedEntity
        where TEntity : class, INamedEntity<TEntity>
    {
    }
}
