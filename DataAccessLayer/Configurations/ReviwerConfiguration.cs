// <copyright file="ReviwerConfiguration.cs" company="Филипченко Марина Алексеевна">
// Copyright (c) Филипченко Марина Алексеевна 2026. Library.
// </copyright>

namespace DataAccessLayer.Configurations
{
    using Domain;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;

    /// <summary>
    /// Конфигурация правил отображения сущности (<see cref="Reviewer"/>) в таблицу БД.
    /// </summary>
    internal sealed class ReviwerConfiguration : IEntityTypeConfiguration<Reviewer>
    {
        /// <inheritdoc/>
        public void Configure(EntityTypeBuilder<Reviewer> builder)
        {
            _ = builder.HasKey(r => r.Id);

            _ = builder.HasOne(a => a.Person)
                .WithOne(p => p.Reviewer)
                .HasForeignKey<Reviewer>(a => a.PersonId)
                .IsRequired()
                .OnDelete(DeleteBehavior.Restrict);

            _ = builder.HasMany(r => r.Manuscripts)
                .WithMany(m => m.Reviewers);

            _ = builder.ToTable("Reviwers");
            _ = builder.HasIndex(a => a.PersonId).IsUnique();
        }
    }
}