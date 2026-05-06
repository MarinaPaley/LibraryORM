// <copyright file="ItemConfiguration.cs" company="Филипченко Марина Алексеевна">
// Copyright (c) Филипченко Марина Алексеевна 2026. Library.
// </copyright>

namespace DataAccessLayer.Configurations
{
    using Domain;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;

    /// <summary>
    /// Конфигурация правил отображения сущности (<see cref="Item"/>) в таблицу БД.
    /// </summary>
    internal sealed class ItemConfiguration : IEntityTypeConfiguration<Item>
    {
        /// <inheritdoc/>
        public void Configure(EntityTypeBuilder<Item> builder)
        {
            _ = builder.HasKey(item => item.Id);

            _ = builder.HasOne(item => item.Shelf)
                .WithMany(shelf => shelf.Items)
                .OnDelete(DeleteBehavior.SetNull)
                .IsRequired(false);

            _ = builder.HasOne(item => item.Book)
                .WithMany(book => book.Items)
                .OnDelete(DeleteBehavior.SetNull)
                .IsRequired();

            _ = builder.ToTable("Items");
        }
    }
}
