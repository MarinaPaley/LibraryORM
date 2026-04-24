// <copyright file="BookTypeConfiguration.cs" company="Филипченко Марина Алексеевна">
// Copyright (c) Филипченко Марина Алексеевна 2026. Library.
// </copyright>

namespace DataAccessLayer.Configurations
{
    using Domain;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;

    /// <summary>
    /// Конфигурация правил отображения сущности (<see cref="BookType"/> в таблицах БД.
    /// </summary>
    internal sealed class BookTypeConfiguration : IEntityTypeConfiguration<BookType>
    {
        /// <inheritdoc/>
        public void Configure(EntityTypeBuilder<BookType> builder)
        {
            _ = builder.HasKey(type => type.Id);

            _ = builder.OwnsOne(type => type.BookTypeName, titleBuilder =>
            {
                titleBuilder.Property(t => t.Value)
                    .HasColumnName("BookTypeName")
                    .IsRequired()
                    .HasComment("Тип книги")
                    .HasMaxLength(200);
                titleBuilder.UsePropertyAccessMode(PropertyAccessMode.Field);

                titleBuilder.HasIndex(t => t.Value)
                    .IsUnique()
                    .HasDatabaseName("IX_BookType_BookTypeName");
            });
        }
    }
}
