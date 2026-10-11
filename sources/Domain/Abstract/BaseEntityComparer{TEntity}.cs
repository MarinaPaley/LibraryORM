// <copyright file="BaseEntityComparer{TEntity}.cs" company="Филипченко Марина Алексеевна">
// Copyright (c) Филипченко Марина Алексеевна 2026. Library.
// </copyright>

namespace Domain.Abstract
{
    using System.Diagnostics.CodeAnalysis;

    /// <summary>
    /// Базовый компаратор для сущностей.
    /// </summary>
    /// <typeparam name="TEntity"> Тип конкретной сущности. </typeparam>
    public abstract class BaseEntityComparer<TEntity> : IEntityComparer<TEntity>
        where TEntity : class, IEntity<TEntity>
    {
        /// <inheritdoc/>
        public virtual bool Equals(TEntity? x, TEntity? y)
        {
            return ReferenceEquals(x, y)
                || ((x is not null)
                    && (y is not null)
                    && (x.Id == y.Id));
        }

        /// <inheritdoc/>
        public virtual int GetHashCode([DisallowNull] TEntity obj) => obj.Id.GetHashCode();
    }
}
