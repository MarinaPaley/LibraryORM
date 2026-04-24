// <copyright file="LanguageConfiguration.cs" company="Филипченко Марина Алексеевна">
// Copyright (c) Филипченко Марина Алексеевна 2026. Library.
// </copyright>

namespace DataAccessLayer.Configurations
{
    using Domain;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;

    /// <summary>
    /// Конфигурация правил отображения сущности (<see cref="Language"/> в таблицах БД.
    /// </summary>
    internal sealed class LanguageConfiguration : IEntityTypeConfiguration<Language>
    {
        /// <inheritdoc/>
        public void Configure(EntityTypeBuilder<Language> builder)
        {
            _ = builder.HasKey(language => language.Id);

            _ = builder.OwnsOne(language => language.Name, titleBuilder =>
            {
                titleBuilder.Property(t => t.Value)
                    .HasColumnName("LanguageName")
                    .IsRequired()
                    .HasComment("Язык")
                    .HasMaxLength(200);
                titleBuilder.UsePropertyAccessMode(PropertyAccessMode.Field);

                titleBuilder.HasIndex(t => t.Value)
                    .IsUnique()
                    .HasDatabaseName("IX_Language_Name");
            });
        }
    }
}
