// <copyright file="ShelfConfiguration.cs" company="Филипченко Марина Алексеевна">
// Copyright (c) Филипченко Марина Алексеевна 2026. Library.
// </copyright>

namespace DataAccessLayer.Configurations
{
    using Domain;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.ChangeTracking;
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

            builder.Property(shelf => shelf.Name)
                .IsRequired()
                .HasConversion(
                    title => title.Value,
                    value => new Title(value))
                .HasComment("Название полки")
                .Metadata.SetValueComparer(
                    new ValueComparer<Title>(
                        (lha, rha) => lha.Equals(rha),     // Твой Equals
                        title => title.GetHashCode(),      // Твой GetHashCode
                        title => new Title(title.Value))); // Метод клонирования (Snapshot)

            // 🔗 Один-ко-многим: полки в шкафу
            _ = builder.HasOne(shelf => shelf.Cabinet)
                .WithMany(cabinet => cabinet.Shelves)
                .OnDelete(DeleteBehavior.SetNull);

            _ = builder.ToTable("Shelves");
        }
    }
}
