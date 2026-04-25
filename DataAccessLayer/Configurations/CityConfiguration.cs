// <copyright file="CityConfiguration.cs" company="Филипченко Марина Алексеевна">
// Copyright (c) Филипченко Марина Алексеевна 2026. Library.
// </copyright>

namespace DataAccessLayer.Configurations
{
    using Domain;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;

    /// <summary>
    /// Конфигурация правил отображения сущности (<see cref="City"/> в таблицах БД.
    /// </summary>
    internal sealed class CityConfiguration : IEntityTypeConfiguration<City>
    {
        /// <inheritdoc/>
        public void Configure(EntityTypeBuilder<City> builder)
        {
            _ = builder.HasKey(city => city.Id);

            _ = builder.OwnsOne(city => city.Name, titleBuilder =>
            {
                titleBuilder.Property(t => t.Value)
                    .HasColumnName("CityName")
                    .IsRequired()
                    .HasComment("Название города")
                    .HasMaxLength(200);
                titleBuilder.UsePropertyAccessMode(PropertyAccessMode.Field);

                titleBuilder.HasIndex(t => t.Value)
                    .IsUnique()
                    .HasDatabaseName("IX_City_Name");
            });

            _ = builder.HasMany(city => city.Streets)
                .WithOne(street => street.City);
        }
    }
}
