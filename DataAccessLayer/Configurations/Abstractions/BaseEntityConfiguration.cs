// <copyright file="BaseEntityConfiguration.cs" company="Филипченко Марина Алексеевна">
// Copyright (c) Филипченко Марина Алексеевна 2026. Library.
// </copyright>

namespace DataAccessLayer.Configurations.Abstractions
{
    using Domain.Abstract;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;

    /// <summary>
    /// Базовая конфигурация для сущности.
    /// </summary>
    /// <typeparam name="TEntity"> Целевой тип сущности. </typeparam>
    internal abstract class BaseEntityConfiguration<TEntity> : IEntityTypeConfiguration<TEntity>
        where TEntity : class, IEntity<TEntity>
    {
        private readonly string? tableName;

        private readonly string? tableComment;

        /// <summary>
        /// Инициализирует новый экземпляр класса <see cref="BaseEntityConfiguration{TEntity}"/>.
        /// </summary>
        /// <param name="tableName"> Название таблицы. </param>
        /// <param name="tableComment"> Комментарий к таблице. </param>
        protected BaseEntityConfiguration(string? tableName = null, string? tableComment = null)
        {
            this.tableName = tableName?.Trim();
            this.tableComment = tableComment;
        }

        /// <inheritdoc/>
        public virtual void Configure(EntityTypeBuilder<TEntity> builder)
        {
            _ = builder.HasKey(entity => entity.Id);

            if (!string.IsNullOrWhiteSpace(this.tableName))
            {
                _ = builder.ToTable(this.tableName, t => t.HasComment(this.tableComment));
            }
        }
    }
}
