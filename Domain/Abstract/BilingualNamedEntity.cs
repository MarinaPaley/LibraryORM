// <copyright file="BilingualNamedEntity.cs" company="Филипченко Марина Алексеевна">
// Copyright (c) Филипченко Марина Алексеевна 2026. Library.
// </copyright>

namespace Domain.Abstract
{
    /// <summary>
    /// Базовая именованная сущность с переводом.
    /// </summary>
    /// <typeparam name="TEntity"> Тип конкретной сущности. </typeparam>
    public abstract class BilingualNamedEntity<TEntity> : NamedEntity<TEntity>,
        IBilingualNamedEntity<TEntity>
        where TEntity : BilingualNamedEntity<TEntity>
    {
        /// <summary>
        /// Инициализирует новый экземпляр класса <see cref="BilingualNamedEntity{TEntity}"/>.
        /// </summary>
        /// <param name="name"> Переводное имя. </param>
        /// <param name="origin"> Оригинальное имя. </param>
        protected BilingualNamedEntity(string name, string? origin = null)
            : base(name)
        {
            this.OriginName = origin is not null ? new Title(origin) : null;
        }

        /// <inheritdoc/>
        public Title? OriginName { get; set; }

        /// <inheritdoc cref="object.ToString"/>
        public override string ToString()
        {
            if (this.OriginName is not null)
            {
                return $"{this.Name} {this.OriginName}";
            }

            return base.ToString();
        }
    }
}
