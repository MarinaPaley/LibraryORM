// <copyright file="RoomConfiguration.cs" company="Филипченко Марина Алексеевна">
// Copyright (c) Филипченко Марина Алексеевна 2026. Library.
// </copyright>

namespace DataAccessLayer.Configurations
{
    using DataAccessLayer.Configurations.Abstractions;
    using Domain;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;

    /// <summary>
    /// Конфигурация правил отображения сущности <see cref="Room"/> в таблицу БД.
    /// </summary>
    internal sealed class RoomConfiguration : BaseNamedEntityConfiguration<Room>
    {
        /// <summary>
        /// Инициализирует новый экземпляр класса <see cref="RoomConfiguration"/>.
        /// </summary>
        public RoomConfiguration()
            : base(
                tableName: "Rooms",
                tableComment: "Комнаты",
                nameComment: "Название комнаты")
        {
        }

        /// <inheritdoc/>
        public override void Configure(EntityTypeBuilder<Room> builder)
        {
            base.Configure(builder);

            // 🔗 Связь с адресом (обязательная)
            _ = builder.HasOne(room => room.Address)
                .WithMany()
                .IsRequired()
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
