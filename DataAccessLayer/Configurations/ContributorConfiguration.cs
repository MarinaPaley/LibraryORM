// <copyright file="ContributorConfiguration.cs" company="Филипченко Марина Алексеевна">
// Copyright (c) Филипченко Марина Алексеевна 2026. Library.
// </copyright>

namespace DataAccessLayer.Configurations
{
    using Domain;
    using Domain.Abstract;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;

    /// <summary>
    /// Конфигурация правил отображения сущности (<see cref="Contributor"/>) в таблицу БД.
    /// </summary>
    internal sealed class ContributorConfiguration : IEntityTypeConfiguration<Contributor>
    {
        /// <inheritdoc/>
        public void Configure(EntityTypeBuilder<Contributor> builder)
        {
            _ = builder.HasKey(c => c.Id);

            // 🔑 Настраиваем дискриминатор
            _ = builder
                .HasDiscriminator<string>("ContributorType")
                .HasValue<Author>("Author")
                .HasValue<Translator>("Translator")
                .HasValue<Editor>("Editor");

            // 📦 Общие поля
            _ = builder.HasOne(c => c.Person)
                .WithMany()
                .HasForeignKey("PersonId")
                .IsRequired()
                .OnDelete(DeleteBehavior.Restrict);

            // 🗂 Имя таблицы
            _ = builder.ToTable("Contributors");
        }
    }
}