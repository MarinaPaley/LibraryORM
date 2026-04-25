// <copyright file="StreetConfiguration.cs" company="Филипченко Марина Алексеевна">
// Copyright (c) Филипченко Марина Алексеевна 2026. Library.
// </copyright>

namespace DataAccessLayer.Configurations
{
    using Domain;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;

    /// <summary>
    ///  Конфигурация правил отображения сущности (<see cref="Street"/> в таблицк БД.
    /// </summary>
    internal sealed class StreetConfiguration : IEntityTypeConfiguration<Street>
    {
        /// <inheritdoc/>
        public void Configure(EntityTypeBuilder<Street> builder)
        {
            _ = builder.HasKey(street => street.Id);

            _ = builder.OwnsOne(street => street.Name, titleBuilder =>
            {
                titleBuilder.Property(t => t.Value)
                    .HasColumnName("StreetName")
                    .IsRequired()
                    .HasComment("Название улицы")
                    .HasMaxLength(200);
                titleBuilder.UsePropertyAccessMode(PropertyAccessMode.Field);
            });
        }
    }
}
