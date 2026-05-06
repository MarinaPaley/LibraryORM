// <copyright file="IEntityComparer.cs" company="Филипченко Марина Алексеевна">
// Copyright (c) Филипченко Марина Алексеевна 2026. Library.
// </copyright>

namespace Domain.Abstract
{
    using System.Collections.Generic;

    /// <summary>
    /// Базовый интерфейс для компараторов сущности.
    /// </summary>
    /// <typeparam name="TEntity"> Тип конкретной сущности. </typeparam>
    public interface IEntityComparer<in TEntity> : IEqualityComparer<TEntity>
        where TEntity : class, IEntity<TEntity>
    {
    }
}