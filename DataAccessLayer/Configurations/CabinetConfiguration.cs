// <copyright file="CabinetConfiguration.cs" company="Филипченко Марина Алексеевна">
// Copyright (c) Филипченко Марина Алексеевна 2026. Library.
// </copyright>

namespace DataAccessLayer.Configurations
{
    using DataAccessLayer.Configurations.Abstractions;
    using Domain;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;

    /// <summary>
    /// Конфигурация правил отображения сущности <see cref="Cabinet"/> в таблицу БД.
    /// </summary>
    internal sealed class CabinetConfiguration : BaseNamedEntityConfiguration<Cabinet>
    {
        /// <summary>
        /// Инициализирует новый экземпляр класса <see cref="CabinetConfiguration"/>.
        /// </summary>
        public CabinetConfiguration()
            : base(
                tableName: "Cabinets",
                tableComment: "Шкафы в комнатах",
                nameComment: "Название шкафа")
        {
        }

        /// <inheritdoc/>
        public override void Configure(EntityTypeBuilder<Cabinet> builder)
        {
            base.Configure(builder);

            // @NOTE: Связь необязательна для возможности перестановки шкафа в другую комнату.
            _ = builder.HasOne(cabinet => cabinet.Room)
                .WithMany(room => room.Cabinets)
                .IsRequired(false)
                .OnDelete(DeleteBehavior.SetNull);
        }
    }
}
