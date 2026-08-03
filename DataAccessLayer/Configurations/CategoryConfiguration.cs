// <copyright file="CategoryConfiguration.cs" company="Филипченко Марина Алексеевна">
// Copyright (c) Филипченко Марина Алексеевна 2026. Library.
// </copyright>

namespace DataAccessLayer.Configurations
{
    using DataAccessLayer.Configurations.Abstractions;
    using Domain;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;

    /// <summary>
    /// Конфигурация правил отображения сущности (<see cref="Category"/> в таблицах БД.
    /// </summary>
    internal sealed class CategoryConfiguration : BaseBilingualNamedEntityConfiguration<Category>
    {
        /// <summary>
        /// Инициализирует новый экземпляр класса <see cref="CategoryConfiguration"/>.
        /// </summary>
        public CategoryConfiguration()
            : base(
                  tableName: "Categories",
                  nameComment: "Название категории",
                  tableComment: "Категории",
                  originNameComment: "Оригинальное название категории",
                  nameIsUnique: true)
        {
        }

        /// <inheritdoc/>
        public override void Configure(EntityTypeBuilder<Category> builder)
        {
            base.Configure(builder);

            _ = builder.HasOne(category => category.Color)
                .WithOne(color => color.Category)
                .HasForeignKey<Category>(category => category.Id)
                .IsRequired();

            _ = builder.HasMany(category => category.Tags)
                .WithOne(tag => tag.Category);
        }
    }
}
