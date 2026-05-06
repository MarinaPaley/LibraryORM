// <copyright file="RoomConfiguration.cs" company="Филипченко Марина Алексеевна">
// Copyright (c) Филипченко Марина Алексеевна 2026. Library.
// </copyright>

namespace DataAccessLayer.Configurations
{
    using Domain;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;

    /// <summary>
    /// Конфигурация правил отображения сущности <see cref="Room"/> в таблицу БД.
    /// </summary>
    internal sealed class RoomConfiguration : IEntityTypeConfiguration<Room>
    {
        /// <inheritdoc/>
        public void Configure(EntityTypeBuilder<Room> builder)
        {
            // 🔑 Первичный ключ
            _ = builder.HasKey(room => room.Id);

            // 📝 Owned Type: Название комнаты
            _ = builder.OwnsOne(room => room.Name, titleBuilder =>
            {
                titleBuilder.Property(t => t.Value)
                    .HasColumnName("RoomName")
                    .IsRequired()
                    .HasComment("Название комнаты")
                    .HasMaxLength(200);

                // 🔑 Пишем напрямую в поле, обходя валидацию при загрузке
                titleBuilder.UsePropertyAccessMode(PropertyAccessMode.Field);
            });

            // 🔗 Связь с адресом (обязательная)
            _ = builder.HasOne(room => room.Address)
                .WithMany()
                .IsRequired()
                .OnDelete(DeleteBehavior.Restrict);

            // 🗂 Имя таблицы
            _ = builder.ToTable("Rooms", t => t.HasComment("Комнаты"));
        }
    }
}