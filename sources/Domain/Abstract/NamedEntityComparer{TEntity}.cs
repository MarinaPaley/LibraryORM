// <copyright file="NamedEntityComparer{TEntity}.cs" company="Филипченко Марина Алексеевна">
// Copyright (c) Филипченко Марина Алексеевна 2026. Library.
// </copyright>

namespace Domain.Abstract
{
    /// <summary>
    /// Базовый компаратор для сравнения именованных сущностей.
    /// </summary>
    /// <typeparam name="TEntity"> Тип конкретной сущности. </typeparam>
    public sealed class NamedEntityComparer<TEntity> : BaseNamedEntityComparer<TEntity>
        where TEntity : class, INamedEntity<TEntity>
    {
        /// <summary>
        /// Инициализирует новый экземпляр класса <see cref="NamedEntityComparer{TEntity}"/>.
        /// </summary>
        private NamedEntityComparer()
        {
        }

        /// <summary>
        /// Сущность.
        /// </summary>
        public static NamedEntityComparer<TEntity> Instance { get; } = new ();
    }
}
