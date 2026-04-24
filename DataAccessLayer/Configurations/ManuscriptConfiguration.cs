// <copyright file="ManuscriptConfiguration.cs" company="Филипченко Марина Алексеевна">
// Copyright (c) Филипченко Марина Алексеевна 2026. Library.
// </copyright>

namespace DataAccessLayer.Configurations
{
    using System;
    using System.Collections.Generic;
    using Domain;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;

    /// <summary>
    /// Конфигурация правил отображения сущности <see cref="Manuscript"/> в таблицу БД.
    /// </summary>
    internal sealed class ManuscriptConfiguration : IEntityTypeConfiguration<Manuscript>
    {
        /// <inheritdoc/>
        public void Configure(EntityTypeBuilder<Manuscript> builder)
        {
            // 🔑 Первичный ключ
            _ = builder.HasKey(manuscript => manuscript.Id);

            // 📝 Owned Type: Название (Title)
            _ = builder.OwnsOne(manuscript => manuscript.Title, titleBuilder =>
            {
                titleBuilder.Property(t => t.Value)
                    .HasColumnName("ManuscriptTitle")
                    .IsRequired()
                    .HasComment("Название произведения")
                    .HasMaxLength(200);

                // 🔑 Пишем напрямую в поле 'value', обходя валидацию при загрузке из БД
                titleBuilder.UsePropertyAccessMode(PropertyAccessMode.Field);

                titleBuilder.HasIndex(t => t.Value)
                    .HasDatabaseName("IX_Manuscript_Title");
            });

            // 📅 Диапазон дат создания (Range<DateOnly>)
            // EF Core не умеет маппить Range<T> "из коробки", поэтому разбиваем на две колонки
            _ = builder.OwnsOne(manuscript => manuscript.Dates, dateRangeBuilder =>
            {
                dateRangeBuilder.Property(r => r.From)
                    .HasColumnName("DateFrom")
                    .IsRequired()
                    .HasComment("Дата начала написания");

                dateRangeBuilder.Property(r => r.To)
                    .HasColumnName("DateTo")
                    .IsRequired()
                    .HasComment("Дата окончания написания");
            });

            // 🌐 Язык
            _ = builder.HasOne(manuscript => manuscript.Language)
                .WithMany(l => l.Manuscripts)
                .IsRequired();

            // 🔗 Many-to-Many: Авторы
            _ = builder.HasMany(m => m.Authors)
                .WithMany(author => author.Manuscripts);

            // 🔗 Many-to-Many: Переводчики
            _ = builder.HasMany(m => m.Translators)
                .WithMany(translator => translator.Manuscripts);

            // 🔗 Many-to-Many: Жанры
            _ = builder.HasMany(m => m.Genres)
                .WithMany(g => g.Manuscripts);

            // 🔗 Many-to-Many: Книги (где опубликована рукопись)
            _ = builder.HasMany(m => m.Books)
                .WithMany(b => b.Manuscripts);

            // 🧹 Настройки таблицы
            _ = builder.ToTable("Manuscripts", t => t.HasComment("Рукописи произведений"));
        }
    }
}