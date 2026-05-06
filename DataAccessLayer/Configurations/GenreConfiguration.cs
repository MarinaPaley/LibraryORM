// <copyright file="GenreConfiguration.cs" company="Филипченко Марина Алексеевна">
// Copyright (c) Филипченко Марина Алексеевна 2026. Library.
// </copyright>

namespace DataAccessLayer.Configurations
{
    using Domain;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;

    /// <summary>
    /// Конфигурация правил отображения сущности (<see cref="Genre"/> в таблицах БД.
    /// </summary>
    internal sealed class GenreConfiguration : IEntityTypeConfiguration<Genre>
    {
        /// <inheritdoc/>
        public void Configure(EntityTypeBuilder<Genre> builder)
        {
            _ = builder.HasKey(genre => genre.Id);

            _ = builder.OwnsOne(genre => genre.Name, titleBuilder =>
            {
                titleBuilder.Property(t => t.Value)
                    .HasColumnName("GenreName")
                    .IsRequired()
                    .HasComment("Жанр")
                    .HasMaxLength(200);
                titleBuilder.UsePropertyAccessMode(PropertyAccessMode.Field);
            });

            _ = builder.ToTable("Genres");
        }
    }
}
