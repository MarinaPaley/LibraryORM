// <copyright file="BaseNamedEntityConfiguration{TEntity}.cs" company="Филипченко Марина Алексеевна">
// Copyright (c) Филипченко Марина Алексеевна 2026. Library.
// </copyright>

namespace DataAccessLayer.Configurations.Abstractions
{
    using Domain;
    using Domain.Abstract;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.ChangeTracking;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;

    /// <summary>
    /// Базовая конфигурация для поименованной сущности.
    /// </summary>
    /// <typeparam name="TEntity"> Целевой тип сущности. </typeparam>
    internal abstract class BaseNamedEntityConfiguration<TEntity> : BaseEntityConfiguration<TEntity>
        where TEntity : NamedEntity<TEntity>
    {
        private readonly string? comment;

        private readonly bool isUnique;

        /// <summary>
        /// Инициализирует новый экземпляр класса <see cref="BaseNamedEntityConfiguration{TEntity}"/>.
        /// </summary>
        /// <param name="tableName"> Название таблицы. </param>
        /// <param name="tableComment"> Комментарий к таблице. </param>
        /// <param name="nameComment"> Комментарий для свойства <see cref="TEntity.Name"/>. </param>
        /// <param name="nameIsUnique">
        /// Показывает необходимость включения ограничения на уникальность для свойства <see cref="TEntity.Name"/>.
        /// </param>
        protected BaseNamedEntityConfiguration(
            string? tableName = null,
            string? tableComment = null,
            string? nameComment = null,
            bool nameIsUnique = false)
            : base(tableName, tableComment)
        {
            this.comment = nameComment;
            this.isUnique = nameIsUnique;
        }

        /// <inheritdoc/>
        public override void Configure(EntityTypeBuilder<TEntity> builder)
        {
            base.Configure(builder);

            _ = builder.OwnsOne(entity => entity.Name, titleBuilder =>
            {
                titleBuilder.Property(title => title.Value)
                    .HasColumnName("Name")
                    .HasMaxLength(200)
                    .IsRequired()
                    .HasComment(this.comment ?? "Название");
                /*
                    .Metadata.SetValueComparer(
                        new ValueComparer<Title>(
                            (lha, rha) => lha.Equals(rha),     // Твой Equals
                            title => title.GetHashCode(),      // Твой GetHashCode
                            title => new Title(title.Value))); // Метод клонирования (Snapshot)
                */

                if (this.isUnique)
                {
                    _ = titleBuilder.HasIndex(title => title.Value)
                        .IsUnique();
                }

                _ = titleBuilder.UsePropertyAccessMode(PropertyAccessMode.Field);
            });
        }
    }
}
