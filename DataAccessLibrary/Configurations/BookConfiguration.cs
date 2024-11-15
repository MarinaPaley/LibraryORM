// <copyright file="BookConfiguration.cs" company="Васильева Марина Алексеевна">
// Copyright (c) Васильева Марина Алексеевна 2024. Library.
// </copyright>

namespace DataAccessLibrary.Configurations
{
    using Domain;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;

    /// <summary>
    /// Конфигурация правил отображения сущности (<see cref="Book"/>) в таблицу БД.
    /// </summary>
    internal sealed class BookConfiguration : IEntityTypeConfiguration<Book>
    {
        /// <inheritdoc/>
        public void Configure(EntityTypeBuilder<Book> builder)
        {
            _ = builder.HasKey(book => book.Id);

            _ = builder.Property(book => book.Title)
                .IsRequired()
                .HasComment("Название книги");

            _ = builder.Property(book => book.Pages)
                .IsRequired()
                .HasComment("Количество страниц");

            _ = builder.Property(book => book.IBSN)
                .IsRequired()
                .HasComment("IBSN");

            _ = builder.HasOne(book => book.Shelf)
                .WithMany(shelf => shelf.Books)
                .HasForeignKey()
                .IsRequired(false);

            _ = builder.HasMany(book => book.Authors)
                .WithMany(author => author.Books);
        }
    }
}
