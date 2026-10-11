// <copyright file="EntityComparer{TEntity}.cs" company="Филипченко Марина Алексеевна">
// Copyright (c) Филипченко Марина Алексеевна 2026. Library.
// </copyright>

namespace Domain.Abstract
{
    /// <summary>
    /// Компаратор по умолчанию для сущностей.
    /// </summary>
    /// <typeparam name="TEntity"> Тип конкретной сущности. </typeparam>
    public sealed class EntityComparer<TEntity> : BaseEntityComparer<TEntity>
         where TEntity : class, IEntity<TEntity>
    {
        /// <summary>
        /// Инициализирует новый экземпляр класса <see cref="EntityComparer{TEntity}"/>.
        /// </summary>
        private EntityComparer()
        {
        }

        /// <summary>
        /// Сущность.
        /// </summary>
        public static EntityComparer<TEntity> Instance { get; } = new ();
    }
}
