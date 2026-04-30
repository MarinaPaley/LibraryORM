// <copyright file="BasePersonComparer{TEntity}.cs" company="Филипченко Марина Алексеевна">
// Copyright (c) Филипченко Марина Алексеевна 2026. Library.
// </copyright>

namespace Domain.Abstract
{
    using System;
    using System.Diagnostics.CodeAnalysis;

    /// <summary>
    /// Базовый компаратор для сущностей, агрегирующих <see cref="Person"/>.
    /// </summary>
    /// <typeparam name="TEntity"> Тип конкретной сущности. </typeparam>
    public abstract class BasePersonComparer<TEntity> : BaseEntityComparer<TEntity>
        where TEntity : class, IPerson<TEntity>
    {
        /// <inheritdoc/>
        public override bool Equals(TEntity? x, TEntity? y)
        {
            return ReferenceEquals(x, y)
                || ((x is not null)
                && (y is not null)
                && (x.Person.FullName == y.Person.FullName)
                && (x.Person.DateBirth == y.Person.DateBirth));
        }

        /// <inheritdoc/>
        public override int GetHashCode([DisallowNull] TEntity obj)
        {
            return HashCode.Combine(obj.Person.FullName.GetHashCode(), obj.Person.DateBirth);
        }
    }
}
