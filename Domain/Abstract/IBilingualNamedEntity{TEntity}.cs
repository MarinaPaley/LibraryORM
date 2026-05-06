// <copyright file="IBilingualNamedEntity{TEntity}.cs" company="Филипченко Марина Алексеевна">
// Copyright (c) Филипченко Марина Алексеевна 2026. Library.
// </copyright>

namespace Domain.Abstract
{
    /// <summary>
    /// Интерфейс базовой сущности, имеющей оригинальное и переводное имя.
    /// </summary>
    /// <typeparam name="TEntity">   Тип конкретной сущности. </typeparam>
    public interface IBilingualNamedEntity<TEntity> : INamedEntity<TEntity>, IBilingualNamedEntity
        where TEntity : class, IBilingualNamedEntity<TEntity>
    {
    }
}
