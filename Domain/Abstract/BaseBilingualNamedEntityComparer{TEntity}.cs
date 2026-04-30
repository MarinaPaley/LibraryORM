// <copyright file="BaseBilingualNamedEntityComparer{TEntity}.cs" company="Филипченко Марина Алексеевна">
// Copyright (c) Филипченко Марина Алексеевна 2026. Library.
// </copyright>

namespace Domain.Abstract
{
    using System.Diagnostics.CodeAnalysis;

    /// <summary>
    /// Базовый компаратор для именованных сущностей с переводом.
    /// </summary>
    /// <typeparam name="TEntity">  Тип конкретной сущности. </typeparam>
    public abstract class BaseBilingualNamedEntityComparer<TEntity> : BaseNamedEntityComparer<TEntity>
        where TEntity : class, IBilingualNamedEntity<TEntity>
    {
        /// <inheritdoc/>
        public override bool Equals(TEntity? x, TEntity? y)
        {
            return base.Equals(x, y)
                && (x?.OriginName == y?.OriginName);
        }

        /// <inheritdoc/>
        public override int GetHashCode([DisallowNull] TEntity obj) => obj.OriginName?.GetHashCode() ?? 0;
    }
}
