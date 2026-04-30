// <copyright file="PersonComparer{TEntity}.cs" company="Филипченко Марина Алексеевна">
// Copyright (c) Филипченко Марина Алексеевна 2026. Library.
// </copyright>

namespace Domain.Abstract
{
    /// <summary>
    /// Компаратор для сущностей типа <see cref="Person"/>.
    /// </summary>
    /// <typeparam name="TEntity"> Тип конкретной сущности.</typeparam>
    public sealed class PersonComparer<TEntity> : BasePersonComparer<TEntity>
        where TEntity : class, IPerson<TEntity>
    {
        /// <summary>
        /// Инициализирует новый экземпляр класса <see cref="PersonComparer{TEntity}"/>.
        /// </summary>
        private PersonComparer()
        {
        }

        /// <summary>
        /// Сущность.
        /// </summary>
        public static PersonComparer<TEntity> Instance { get; } = new ();
    }
}
