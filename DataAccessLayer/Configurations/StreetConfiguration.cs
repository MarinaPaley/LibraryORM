// <copyright file="StreetConfiguration.cs" company="Филипченко Марина Алексеевна">
// Copyright (c) Филипченко Марина Алексеевна 2026. Library.
// </copyright>

namespace DataAccessLayer.Configurations
{
    using DataAccessLayer.Configurations.Abstractions;
    using Domain;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;

    /// <summary>
    ///  Конфигурация правил отображения сущности (<see cref="Street"/> в таблице БД.
    /// </summary>
    internal sealed class StreetConfiguration : BaseNamedEntityConfiguration<Street>
    {
        /// <summary>
        /// Инициализирует новый экземпляр класса <see cref="StreetConfiguration"/>.
        /// </summary>
        public StreetConfiguration()
            : base(tableName: "Streets", nameComment: "Назание улицы")
        {
        }

        /// <inheritdoc/>
        public override void Configure(EntityTypeBuilder<Street> builder)
        {
            base.Configure(builder);

            _ = builder.HasOne(street => street.City)
                .WithMany(city => city.Streets)
                .IsRequired()
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
