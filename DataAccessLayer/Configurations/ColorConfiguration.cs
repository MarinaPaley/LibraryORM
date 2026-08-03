// <copyright file="ColorConfiguration.cs" company="Филипченко Марина Алексеевна">
// Copyright (c) Филипченко Марина Алексеевна 2026. Library.
// </copyright>

namespace DataAccessLayer.Configurations
{
    using DataAccessLayer.Configurations.Abstractions;
    using Domain;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;

    /// <summary>
    /// Конфигурация правил отображения сущности (<see cref="Color"/> в таблицах БД.
    /// </summary>
    internal sealed class ColorConfiguration : BaseNamedEntityConfiguration<Color>
    {
        /// <summary>
        /// Инициализирует новый экземпляр класса <see cref="ColorConfiguration"/>.
        /// </summary>
        public ColorConfiguration()
            : base(nameComment: "Цвет", nameIsUnique: true)
        {
        }

        /// <inheritdoc/>
        public override void Configure(EntityTypeBuilder<Color> builder)
        {
            base.Configure(builder);

            _ = builder.Property(colorCode => colorCode.Code)
                .HasConversion(
                    v => v.Value,
                    v => new ColorCode(v));

            _ = builder.HasIndex(colorCode => colorCode.Code)
                .IsUnique();
        }
    }
}
