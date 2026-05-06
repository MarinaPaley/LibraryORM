// <copyright file="EditorConfiguration.cs" company="Филипченко Марина Алексеевна">
// Copyright (c) Филипченко Марина Алексеевна 2026. Library.
// </copyright>

namespace DataAccessLayer.Configurations
{
    using Domain;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;

    /// <summary>
    /// Конфигурация правил отображения сущности (<see cref="Editor"/>) в таблицу БД.
    /// </summary>
    internal sealed class EditorConfiguration : IEntityTypeConfiguration<Editor>
    {
        /// <inheritdoc/>
        public void Configure(EntityTypeBuilder<Editor> builder)
        {
            _ = builder.HasKey(e => e.Id);

            _ = builder.HasOne(a => a.Person)
                .WithOne(p => p.Editor)
                .HasForeignKey<Editor>(a => a.PersonId)
                .IsRequired()
                .OnDelete(DeleteBehavior.Restrict);

            _ = builder.ToTable("Editors");
            _ = builder.HasIndex(a => a.PersonId).IsUnique();
        }
    }
}