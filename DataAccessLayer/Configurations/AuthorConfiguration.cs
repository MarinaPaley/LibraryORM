// <copyright file="AuthorConfiguration.cs" company="Васильева Марина Алексеевна">
// Copyright (c) Васильева Марина Алексеевна 2024. Library.
// </copyright>

namespace DataAccessLayer.Configurations
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

            _ = builder.Property(author => author.DateBirth)
                .IsRequired(false)
                .HasComment("Дата рождения");

            _ = builder.Property(author => author.DateDeath)
                .IsRequired(false)
                .HasComment("Дата смерти");

            _ = builder.OwnsOne(
                author => author.FullName,
                nameBuilder =>
                {
                    _ = nameBuilder.Property(name => name.FamilyName)
                        .HasColumnName(nameof(Name.FamilyName))
                        .HasMaxLength(100)
                        .IsRequired()
                        .HasComment("Фамилия");

                    _ = nameBuilder.Property(name => name.FirstName)
                        .HasColumnName(nameof(Name.FirstName))
                        .HasMaxLength(100)
                        .IsRequired()
                        .HasComment("Имя");

                    _ = nameBuilder.Property(name => name.PatronicName)
                        .HasColumnName(nameof(Name.PatronicName))
                        .HasMaxLength(100)
                        .IsRequired(false)
                        .HasComment("Отчество");
                });
        }
    }
}
