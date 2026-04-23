// <copyright file="PublisherConfiguration.cs" company="Филипченко Марина Алексеевна">
// Copyright (c) Филипченко Марина Алексеевна 2026. Library.
// </copyright>

namespace DataAccessLayer.Configurations
{
    using Domain;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;

    /// <summary>
    /// Конфигурация правил отображения сущности <see cref="Publisher"/> в таблицу БД.
    /// </summary>
    internal sealed class PublisherConfiguration : IEntityTypeConfiguration<Publisher>
    {
        /// <inheritdoc/>
        public void Configure(EntityTypeBuilder<Publisher> builder)
        {
            _ = builder.HasKey(publisher => publisher.Id);

            _ = builder.OwnsOne(publisher => publisher.Name, titleBuilder =>
            {
                titleBuilder.Property(t => t.Value)
                    .HasColumnName("Name")
                    .IsRequired()
                    .HasComment("Название издательства")
                    .HasMaxLength(200);
            });

            _ = builder.HasOne(publisher => publisher.Address)
                .WithMany()
                .HasForeignKey("AddressId")
                .IsRequired()
                .OnDelete(DeleteBehavior.Restrict);

            _ = builder.HasIndex(publisher => publisher.Name.Value)
                .IsUnique()
                .HasDatabaseName("IX_Publisher_Name");
        }
    }
}