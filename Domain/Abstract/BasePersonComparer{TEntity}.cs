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
        /// <summary>
        /// Статический метод сравнения двух объектов типа <see cref="Person"/>.
        /// </summary>
        /// <param name="x"> Левый операнд. </param>
        /// <param name="y"> Правый операнд. </param>
        /// <returns> <see langword="true"/>, если равны, иначе <see langword="false"/>. </returns>
        public static bool AreEqualByPerson(TEntity? x, TEntity? y)
        {
            if (ReferenceEquals(x, y))
            {
                return true;
            }

            if (x is null || y is null)
            {
                return false;
            }

            if (x.Person is null && y.Person is null)
            {
                return true;
            }

            if (x.Person is null || y.Person is null)
            {
                return false;
            }

            return object.Equals(x.Person.FullName, y.Person.FullName)
                && object.Equals(x.Person.DateBirth, y.Person.DateBirth);
        }

        /// <inheritdoc/>
        public override bool Equals(TEntity? x, TEntity? y) => AreEqualByPerson(x, y);

        /// <inheritdoc/>
        public override int GetHashCode([DisallowNull] TEntity obj)
        {
            if (obj?.Person?.FullName is null)
            {
                return obj?.Id.GetHashCode() ?? 0;
            }

            return HashCode.Combine(obj.Person.FullName, obj.Person.DateBirth);
        }
    }
}
