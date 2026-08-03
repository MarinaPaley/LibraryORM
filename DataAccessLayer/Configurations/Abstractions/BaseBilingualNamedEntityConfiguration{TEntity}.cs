// <copyright file="BaseBilingualNamedEntityConfiguration{TEntity}.cs" company="Филипченко Марина Алексеевна">
// Copyright (c) Филипченко Марина Алексеевна 2026. Library.
// </copyright>

namespace DataAccessLayer.Configurations.Abstractions
{
    using Domain.Abstract;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;

    /// <summary>
    /// Базовая конфигурация для поименованной сущности с переводом.
    /// </summary>
    /// <typeparam name="TEntity"> Целевой тип сущности. </typeparam>
    internal abstract class BaseBilingualNamedEntityConfiguration<TEntity>
        : BaseNamedEntityConfiguration<TEntity>
        where TEntity : BilingualNamedEntity<TEntity>
    {
        private readonly string? comment;

        private readonly bool isUnique;

        /// <summary>
        /// Инициализирует новый экземпляр класса <see cref="BaseBilingualNamedEntityConfiguration{TEntity}"/>.
        /// </summary>
        /// <param name="tableName"> Название таблицы. </param>
        /// <param name="tableComment"> Комментарий к таблице. </param>
        /// <param name="nameComment"> Комментарий для свойства <see cref="TEntity.Name"/>. </param>
        /// <param name="nameIsUnique">
        /// Показывает необходимость включения ограничения на уникальность для свойства <see cref="TEntity.Name"/>.
        /// </param>
        /// <param name="originNameComment"> Комментарий для свойства <see cref="TEntity.OriginName"/>. </param>
        /// <param name="originNameIsUnique">
        /// Показывает необходимость включения ограничения на уникальность для свойства <see cref="TEntity.OriginName"/>.
        /// </param>
        protected BaseBilingualNamedEntityConfiguration(
            string? tableName = null,
            string? tableComment = null,
            string? nameComment = null,
            bool nameIsUnique = false,
            string? originNameComment = null,
            bool originNameIsUnique = false)
            : base(tableName, tableComment, nameComment, nameIsUnique)
        {
            this.comment = originNameComment;
            this.isUnique = originNameIsUnique;
        }

        /// <inheritdoc/>
        public override void Configure(EntityTypeBuilder<TEntity> builder)
        {
            base.Configure(builder);

            // 📝 Owned Type: Оригинальное название (OriginTitle)
            _ = builder.OwnsOne(entity => entity.OriginName, titleBuilder =>
            {
                titleBuilder.Property(title => title.Value)
                    .HasColumnName("OriginTitle")
                    .IsRequired(false)
                    .HasComment(this.comment ?? "Оригинальное название")
                    .HasMaxLength(200);

                if (this.isUnique)
                {
                    _ = titleBuilder.HasIndex(title => title.Value)
                        .IsUnique();
                }

                // 🔑 Пишем напрямую в поле 'value', обходя валидацию при загрузке из БД
                titleBuilder.UsePropertyAccessMode(PropertyAccessMode.Field);
            });
        }
    }
}
