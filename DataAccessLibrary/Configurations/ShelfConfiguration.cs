// <copyright file="ShelfConfiguration.cs" company="Васильева Марина Алексеевна">
// Copyright (c) Васильева Марина Алексеевна 2024. Library.
// </copyright>

namespace DataAccessLibrary.Configurations
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
                .HasComment("Название полки");
        }
    }
}
