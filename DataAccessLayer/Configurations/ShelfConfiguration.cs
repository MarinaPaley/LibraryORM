// <copyright file="ShelfConfiguration.cs" company="Филипченко Марина Алексеевна">
// Copyright (c) Филипченко Марина Алексеевна 2026. Library.
// </copyright>

namespace DataAccessLayer.Configurations
{
    using System;
    using System.Linq;
    using DataAccessLayer.Configurations.Abstractions;
    using Domain;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;

    /// <summary>
    /// Конфигурация правил отображения сущности (<see cref="Shelf"/>) в таблицу БД.
    /// </summary>
    internal sealed class ShelfConfiguration : BaseNamedEntityConfiguration<Shelf>
    {
        /// <summary>
        /// Инициализирует новый экземпляр класса <see cref="ShelfConfiguration"/>.
        /// </summary>
        public ShelfConfiguration()
            : base(
                tableName: "Shelves",
                tableComment: "Книжные полки",
                nameComment: "Название полки")
        {
        }

        /// <inheritdoc/>
        public override void Configure(EntityTypeBuilder<Shelf> builder)
        {
            base.Configure(builder);

            // 🔗 Один-ко-многим: полки в шкафу
            _ = builder.HasOne(shelf => shelf.Cabinet)
                .WithMany(cabinet => cabinet.Shelves)
                .HasForeignKey(shelf => shelf.CabinetId)
                .IsRequired(false)
                .OnDelete(DeleteBehavior.SetNull);
        }
    }
}
