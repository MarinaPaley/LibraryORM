// <copyright file="BilingualNamedEntityComparer.cs" company="Филипченко Марина Алексеевна">
// Copyright (c) Филипченко Марина Алексеевна 2026. Library.
// </copyright>

namespace Domain.Abstract
{
    /// <summary>
    /// Базовый компаратор для сравнения именованных сущностей, имеющих перевод.
    /// </summary>
    /// <typeparam name="TEntity"> Тип конкретной сущности. </typeparam>
    public sealed class BilingualNamedEntityComparer<TEntity> : BaseBilingualNamedEntityComparer<TEntity>
        where TEntity : class, IBilingualNamedEntity<TEntity>
    {
        /// <summary>
        /// Инициализирует новый экземпляр класса <see cref="BilingualNamedEntityComparer{TEntity}"/>.
        /// </summary>
        private BilingualNamedEntityComparer()
        {
        }

        /// <summary>
        /// Сущность.
        /// </summary>
        public static BilingualNamedEntityComparer<TEntity> Instance { get; } = new ();
    }
}
