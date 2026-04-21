// <copyright file="ShelfConfiguration.cs" company="Филипченко Марина Алексеевна">
// Copyright (c) Филипченко Марина Алексеевна 2026. Library.
// </copyright>

namespace DataAccessLayer.Configurations
{
    using Domain;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;

    /// <summary>
    /// Конфигурация правил отображения сущности (<see cref="Shelf"/>) в таблицу БД.
    /// </summary>
    internal sealed class ShelfConfiguration : IEntityTypeConfiguration<Shelf>
    {
        /// <inheritdoc/>
        public void Configure(EntityTypeBuilder<Shelf> builder)
        {
            _ = builder.HasKey(shelf => shelf.Id);

            _ = builder.Property(shelf => shelf.Name)
                .IsRequired()
                .HasConversion(
                    v => v.Value,
                    v => new Title(v))
                .HasComment("Название полки");

            _ = builder.HasIndex(shelf => shelf.Name)
                .IsUnique();
        }
    }
}
