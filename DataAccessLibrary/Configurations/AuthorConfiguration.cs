// <copyright file="AuthorConfiguration.cs" company="Васильева Марина Алексеевна">
// Copyright (c) Васильева Марина Алексеевна 2024. Library.
// </copyright>

namespace DataAccessLibrary.Configurations
{
    using Domain;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;

    /// <summary>
    /// Конфигурация правил отображения сущности (<see cref="Author"/>) в таблицу БД.
    /// </summary>
    internal sealed class AuthorConfiguration : IEntityTypeConfiguration<Author>
    {
        /// <inheritdoc/>
        public void Configure(EntityTypeBuilder<Author> builder)
        {
            _ = builder.HasKey(author => author.Id);

            _ = builder.OwnsOne(
                author => author.FullName,
                nameBuilder =>
                {
                    _ = nameBuilder.Property(name => name.FamilyName)
                        .HasMaxLength(100)
                        .IsRequired()
                        .HasComment("Фамилия");

                    _ = nameBuilder.Property(name => name.FirstName)
                        .HasMaxLength(100)
                        .IsRequired()
                        .HasComment("Имя");

                    _ = nameBuilder.Property(name => name.PatronicName)
                        .HasMaxLength(100)
                        .IsRequired(false)
                        .HasComment("Отчество");
                });
        }
    }
}
