// <copyright file="SeriaConfiguration.cs" company="Филипченко Марина Алексеевна">
// Copyright (c) Филипченко Марина Алексеевна 2026. Library.
// </copyright>

namespace DataAccessLayer.Configurations
{
    using Domain;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;

    /// <summary>
    /// Конфигурация правил отображения сущности (<see cref="Seria"/> в таблицах БД.
    /// </summary>
    internal sealed class SeriaConfiguration : IEntityTypeConfiguration<Seria>
    {
        /// <inheritdoc/>
        public void Configure(EntityTypeBuilder<Seria> builder)
        {
            _ = builder.HasKey(seria => seria.Id);

            _ = builder.OwnsOne(seria => seria.SeriaName, titleBuilder =>
            {
                titleBuilder.Property(t => t.Value)
                    .HasColumnName("SeriaName")
                    .IsRequired()
                    .HasComment("Серия")
                    .HasMaxLength(200);
                titleBuilder.UsePropertyAccessMode(PropertyAccessMode.Field);

                titleBuilder.HasIndex(t => t.Value)
                    .IsUnique()
                    .HasDatabaseName("IX_Seria_Name");
            });

            _ = builder.HasMany(seria => seria.Books)
                .WithOne(book => book.Seria);
        }
    }
}
