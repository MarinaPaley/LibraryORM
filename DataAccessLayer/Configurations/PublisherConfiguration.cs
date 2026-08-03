// <copyright file="PublisherConfiguration.cs" company="Филипченко Марина Алексеевна">
// Copyright (c) Филипченко Марина Алексеевна 2026. Library.
// </copyright>

namespace DataAccessLayer.Configurations
{
    using DataAccessLayer.Configurations.Abstractions;
    using Domain;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;

    /// <summary>
    /// Конфигурация правил отображения сущности <see cref="Publisher"/> в таблицу БД.
    /// </summary>
    internal sealed class PublisherConfiguration : BaseBilingualNamedEntityConfiguration<Publisher>
    {
        /// <summary>
        /// Инициализирует новый экземпляр класса <see cref="PublisherConfiguration"/>.
        /// </summary>
        public PublisherConfiguration()
            : base(
                tableName: "Publishers",
                nameComment: "Название издательства",
                nameIsUnique: true,
                originNameComment: "Оригинальное название издательства",
                originNameIsUnique: true)
        {
        }

        /// <inheritdoc/>
        public override void Configure(EntityTypeBuilder<Publisher> builder)
        {
            base.Configure(builder);

            _ = builder.HasOne(publisher => publisher.Address)
                .WithMany()
                .IsRequired(false)
                .OnDelete(DeleteBehavior.SetNull);
        }
    }
}
