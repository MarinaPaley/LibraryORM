// <copyright file="CabinetConfiguration.cs" company="Филипченко Марина Алексеевна">
// Copyright (c) Филипченко Марина Алексеевна 2026. Library.
// </copyright>

namespace DataAccessLayer.Configurations
{
    using Domain;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;

    /// <summary>
    /// Конфигурация правил отображения сущности <see cref="Cabinet"/> в таблицу БД.
    /// </summary>
    internal sealed class CabinetConfiguration : IEntityTypeConfiguration<Cabinet>
    {
        /// <inheritdoc/>
        public void Configure(EntityTypeBuilder<Cabinet> builder)
        {
            // 🔑 Первичный ключ
            _ = builder.HasKey(cabinet => cabinet.Id);

            // 📝 Owned Type: Название шкафа
            _ = builder.OwnsOne(cabinet => cabinet.Name, titleBuilder =>
            {
                titleBuilder.Property(t => t.Value)
                    .HasColumnName("CabinetName")
                    .IsRequired()
                    .HasComment("Название шкафа")
                    .HasMaxLength(200);

                titleBuilder.UsePropertyAccessMode(PropertyAccessMode.Field);
            });

            // 🔗 Связь с комнатой
            _ = builder.HasOne(cabinet => cabinet.Room)
                .WithMany(room => room.Cabinets)
                .IsRequired(false)
                .OnDelete(DeleteBehavior.SetNull);

            // 🗂 Имя таблицы
            _ = builder.ToTable("Cabinets", t => t.HasComment("Шкафы в комнатах"));
        }
    }
}