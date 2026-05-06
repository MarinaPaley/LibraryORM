// <copyright file="BookConfiguration.cs" company="Филипченко Марина Алексеевна">
// Copyright (c) Филипченко Марина Алексеевна 2026. Library.
// </copyright>

namespace DataAccessLayer.Configurations
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
                .IsRequired(false)
                .HasMaxLength(100)
                .HasComment("Название книги");

            _ = builder.Property(book => book.Pages)
                .IsRequired()
                .HasComment("Количество страниц");

            _ = builder.Property(book => book.ISBN)
                .IsRequired(false)
                .HasMaxLength(25)
                .HasComment("ISBN");

            _ = builder.HasMany(book => book.Manuscripts)
                .WithMany(manuscript => manuscript.Books);

            _ = builder.HasMany(book => book.Publishers)
                .WithMany(p => p.Books);

            _ = builder.HasOne(book => book.Editor)
                .WithMany(editor => editor.Books)
                .OnDelete(DeleteBehavior.SetNull)
                .IsRequired(false);

            _ = builder.Property(book => book.Annotation)
                .IsRequired(false)
                .HasComment("Аннотация");

            _ = builder.HasOne(book => book.Seria)
                .WithMany(seria => seria.Books)
                .OnDelete(DeleteBehavior.SetNull)
                .IsRequired(false);

            _ = builder.HasOne(book => book.BookType)
                .WithMany(type => type.Books)
                .OnDelete(DeleteBehavior.SetNull)
                .IsRequired();

            _ = builder.Property(book => book.Doi)
                .IsRequired(false)
                .HasComment("DOI");

            _ = builder.Property(book => book.Url)
                .IsRequired(false)
                .HasComment("URL");

            _ = builder.Property(book => book.Volume)
                .IsRequired(false)
                .HasComment("Том");

            _ = builder.ToTable("Books");
        }
    }
}
