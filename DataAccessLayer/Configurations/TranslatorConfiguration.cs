// <copyright file="TranslatorConfiguration.cs" company="Филипченко Марина Алексеевна">
// Copyright (c) Филипченко Марина Алексеевна 2026. Library.
// </copyright>

namespace DataAccessLayer.Configurations
{
    using Domain;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;

    /// <summary>
    /// Конфигурация правил отображения сущности (<see cref="Translator"/>) в таблицу БД.
    /// </summary>
    internal sealed class TranslatorConfiguration : IEntityTypeConfiguration<Translator>
    {
        /// <inheritdoc/>
        public void Configure(EntityTypeBuilder<Translator> builder)
        {
            _ = builder.HasKey(t => t.Id);

            _ = builder.HasOne(t => t.Person)
                .WithOne(p => p.Translator)
                .HasForeignKey<Translator>(a => a.PersonId)
                .IsRequired()
                .OnDelete(DeleteBehavior.Restrict);

            _ = builder.HasMany(t => t.Manuscripts)
                .WithMany(m => m.Translators);

            _ = builder.ToTable("Translators");
            _ = builder.HasIndex(a => a.PersonId).IsUnique();
        }
    }
}