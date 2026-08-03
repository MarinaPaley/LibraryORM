// <copyright file="ItemConfiguration.cs" company="Филипченко Марина Алексеевна">
// Copyright (c) Филипченко Марина Алексеевна 2026. Library.
// </copyright>

namespace DataAccessLayer.Configurations
{
    using DataAccessLayer.Configurations.Abstractions;
    using Domain;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;

    /// <summary>
    /// Конфигурация правил отображения сущности (<see cref="Item"/>) в таблицу БД.
    /// </summary>
    internal sealed class ItemConfiguration : BaseEntityConfiguration<Item>
    {
        /// <summary>
        /// Инициализирует новый экземпляр класса <see cref="ItemConfiguration"/>.
        /// </summary>
        public ItemConfiguration()
            : base(tableName: "Items", tableComment: "Экземпляры книг")
        {
        }

        /// <inheritdoc/>
        public override void Configure(EntityTypeBuilder<Item> builder)
        {
            base.Configure(builder);

            _ = builder.HasOne(item => item.Shelf)
                .WithMany(shelf => shelf.Items)
                .OnDelete(DeleteBehavior.SetNull)
                .IsRequired(false);

            _ = builder.HasOne(item => item.Book)
                .WithMany(book => book.Items)
                .OnDelete(DeleteBehavior.SetNull)
                .IsRequired();
        }
    }
}
