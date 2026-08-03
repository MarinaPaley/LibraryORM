// <copyright file="AddressConfiguration.cs" company="Филипченко Марина Алексеевна">
// Copyright (c) Филипченко Марина Алексеевна 2026. Library.
// </copyright>

namespace DataAccessLayer.Configurations
{
    using DataAccessLayer.Configurations.Abstractions;
    using Domain;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;

    /// <summary>
    /// Конфигурация правил отображения сущности <see cref="Address"/> в таблицу БД.
    /// </summary>
    internal sealed class AddressConfiguration : BaseEntityConfiguration<Address>
    {
        /// <inheritdoc/>
        public override void Configure(EntityTypeBuilder<Address> builder)
        {
            base.Configure(builder);

            _ = builder.Property(address => address.House)
                .IsRequired()
                .HasComment("Номер дома");

            _ = builder.Property(address => address.BuildingSuffix)
                .HasMaxLength(50)
                .HasComment("Корпус или владение");

            _ = builder.Property(address => address.Apartment)
                .HasComment("Номер квартиры");

            _ = builder.HasOne(address => address.Street)
                .WithMany()
                .IsRequired()
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
