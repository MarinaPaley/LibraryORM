// <copyright file="AuthorConfiguration.cs" company="Филипченко Марина Алексеевна">
// Copyright (c) Филипченко Марина Алексеевна 2026. Library.
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
            _ = builder.HasKey(a => a.Id);

            _ = builder.HasOne(a => a.Person)
            .WithOne(p => p.Author)
            .HasForeignKey<Author>(a => a.PersonId)
            .IsRequired()
            .OnDelete(DeleteBehavior.Restrict);

            _ = builder.HasMany(a => a.Manuscripts)
                .WithMany(m => m.Authors);

            _ = builder.ToTable("Authors");
            _ = builder.HasIndex(a => a.PersonId).IsUnique();
        }
    }
}