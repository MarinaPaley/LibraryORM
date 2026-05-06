// <copyright file="BaseNamedEntityComparer{TEntity}.cs" company="Филипченко Марина Алексеевна">
// Copyright (c) Филипченко Марина Алексеевна 2026. Library.
// </copyright>

namespace Domain.Abstract
{
    using System.Diagnostics.CodeAnalysis;

    /// <summary>
    /// Базовый компаратор для именованных сущностей.
    /// </summary>
    /// <typeparam name="TEntity"> Тип конкретной сущности. </typeparam>
    public abstract class BaseNamedEntityComparer<TEntity> : BaseEntityComparer<TEntity>
        where TEntity : class, INamedEntity<TEntity>
    {
        /// <inheritdoc/>
        public override bool Equals(TEntity? x, TEntity? y)
        {
            return ReferenceEquals(x, y)
                || ((x is not null)
                && (y is not null)
                && (x.Name == y.Name));
        }

        /// <inheritdoc/>
        public override int GetHashCode([DisallowNull] TEntity obj) => obj.Name.GetHashCode();
    }
}
