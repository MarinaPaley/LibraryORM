// <copyright file="NamedEntity.cs" company="Филипченко Марина Алексеевна">
// Copyright (c) Филипченко Марина Алексеевна 2026. Library.
// </copyright>

namespace Domain.Abstract
{
    /// <summary>
    /// Базовая именованная сущность.
    /// </summary>
    /// <typeparam name="TEntity"> Тип конкретной сущности. </typeparam>
    public abstract class NamedEntity<TEntity> : Entity<TEntity>,
        INamedEntity<TEntity>
        where TEntity : NamedEntity<TEntity>
    {
        /// <summary>
        /// Инициализирует новый экземпляр класса <see cref="NamedEntity{TEntity}"/>.
        /// </summary>
        /// <param name="name"> Название. </param>
        protected NamedEntity(string name)
        {
            this.Name = new Title(name);
        }

        /// <inheritdoc cref="INamedEntity{TEntity}.Name"/>
        public virtual Title Name { get; set; }

        /// <inheritdoc cref="object.ToString"/>
        public override string ToString() => $"{this.Name}";

        /// <inheritdoc/>
        public override bool Equals(object? obj) => this.Equals(obj as TEntity);

        /// <inheritdoc/>
        public override bool Equals(TEntity? other)
        {
            return ReferenceEquals(this, other)
                || (other is not null
                && this.Name == other?.Name);
        }

        /// <inheritdoc/>
        public override int GetHashCode() => this.Name?.GetHashCode() ?? 0;
    }
}
