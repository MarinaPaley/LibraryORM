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
                    .HasColumnName("PublisherName")
                    .IsRequired()
                    .HasComment("Название издательства")
                    .HasMaxLength(200);
                titleBuilder.UsePropertyAccessMode(PropertyAccessMode.Field);
            });

            _ = builder.OwnsOne(publisher => publisher.OriginName, titleBuilder =>
            {
                titleBuilder.Property(t => t.Value)
                    .HasColumnName("PublisheOriginName")
                    .IsRequired(false)
                    .HasComment("Оригинальное название издательства")
                    .HasMaxLength(200);
                titleBuilder.UsePropertyAccessMode(PropertyAccessMode.Field);
            });

            _ = builder.HasOne(p => p.Address)
                .WithMany()
                .IsRequired(false)
                .OnDelete(DeleteBehavior.SetNull);

            _ = builder.ToTable("Publishers");
        }
    }
}