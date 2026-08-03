// <copyright file="ManuscriptConfiguration.cs" company="Филипченко Марина Алексеевна">
// Copyright (c) Филипченко Марина Алексеевна 2026. Library.
// </copyright>

namespace DataAccessLayer.Configurations
{
    using DataAccessLayer.Configurations.Abstractions;
    using Domain;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;

    /// <summary>
    /// Конфигурация правил отображения сущности <see cref="Manuscript"/> в таблицу БД.
    /// </summary>
    internal sealed class ManuscriptConfiguration
        : BaseBilingualNamedEntityConfiguration<Manuscript>
    {
        /// <summary>
        /// Инициализирует новый экземпляр класса <see cref="ManuscriptConfiguration"/>.
        /// </summary>
        public ManuscriptConfiguration()
            : base(
                tableName: "Manuscripts",
                tableComment: "Рукописи произведений",
                nameComment: "Название произведения",
                originNameComment: "Оригинальное название произведения")
        {
        }

        /// <inheritdoc/>
        public override void Configure(EntityTypeBuilder<Manuscript> builder)
        {
            base.Configure(builder);

            // 📅 Диапазон дат создания (Range<DateOnly>)
            // EF Core не умеет маппить Range<T> "из коробки", поэтому разбиваем на две колонки
            _ = builder.OwnsOne(manuscript => manuscript.Dates, dateRangeBuilder =>
            {
                dateRangeBuilder.Property(range => range.From)
                    .HasColumnName("DateFrom")
                    .IsRequired()
                    .HasComment("Дата начала написания");

                dateRangeBuilder.Property(range => range.To)
                    .HasColumnName("DateTo")
                    .IsRequired()
                    .HasComment("Дата окончания написания");
            });

            // 🌐 Язык
            _ = builder.HasMany(manuscript => manuscript.Languages)
                .WithMany(language => language.Manuscripts);

            // 🔗 Many-to-Many: Жанры
            _ = builder.HasMany(manuscript => manuscript.Genres)
                .WithMany(genre => genre.Manuscripts);

            // 🔗 Many-to-Many: Книги (где опубликована рукопись)
            _ = builder.HasMany(manuscript => manuscript.Books)
                .WithMany(book => book.Manuscripts);
        }
    }
}
