// <copyright file="AddressConfiguration.cs" company="Филипченко Марина Алексеевна">
// Copyright (c) Филипченко Марина Алексеевна 2026. Library.
// </copyright>

namespace DataAccessLayer.Configurations
{
    using Domain;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;

    /// <summary>
    /// Конфигурация правил отображения сущности <see cref="Address"/> в таблицу БД.
    /// </summary>
    internal sealed class AddressConfiguration : IEntityTypeConfiguration<Address>
    {
        /// <inheritdoc/>
        public void Configure(EntityTypeBuilder<Address> builder)
        {
            _ = builder.HasKey(address => address.Id);

            _ = builder.Property(address => address.House)
                .IsRequired()
                .HasComment("Номер дома");

            _ = builder.Property(address => address.BuildingSuffix)
                .HasMaxLength(50)
                .HasComment("Корпус или владение");

            _ = builder.Property(address => address.Apartment)
                .HasComment("Номер квартиры");

            _ = builder.HasOne(address => address.City)
                .WithMany()
                .HasForeignKey("CityId")
                .IsRequired()
                .OnDelete(DeleteBehavior.Restrict);

            _ = builder.HasOne(address => address.Street)
                .WithMany()
                .HasForeignKey("StreetId")
                .IsRequired()
                .OnDelete(DeleteBehavior.Restrict);

        }
    }
}