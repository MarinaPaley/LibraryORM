// <copyright file="PersonConfiguration.cs" company="Филипченко Марина Алексеевна">
// Copyright (c) Филипченко Марина Алексеевна 2026. Library.
// </copyright>

namespace DataAccessLayer.Configurations
{
    using Domain;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;

    /// <summary>
    /// Конфигурация правил отображения сущности <see cref="Person"/> в таблицу БД.
    /// </summary>
    internal sealed class PersonConfiguration : IEntityTypeConfiguration<Person>
    {
        /// <inheritdoc/>
        public void Configure(EntityTypeBuilder<Person> builder)
        {
            // 🔑 Первичный ключ (наследуется от Entity<Person>)
            _ = builder.HasKey(person => person.Id);

            // 📝 Owned Type: FullName (Name)
            _ = builder.OwnsOne(person => person.FullName, nameBuilder =>
            {
                // Фамилия
                nameBuilder.Property(n => n.FamilyName)
                    .HasColumnName("FamilyName")
                    .IsRequired()
                    .HasComment("Фамилия персоны")
                    .HasMaxLength(100);

                // Имя
                nameBuilder.Property(n => n.FirstName)
                    .HasColumnName("FirstName")
                    .IsRequired()
                    .HasComment("Имя персоны")
                    .HasMaxLength(100);

                // Отчество (опционально)
                nameBuilder.Property(n => n.PatronymicName)
                    .HasColumnName("PatronymicName")
                    .HasComment("Отчество персоны")
                    .HasMaxLength(100);

                // 🔑 Пишем напрямую в поля, обходя валидацию при загрузке из БД
                nameBuilder.UsePropertyAccessMode(PropertyAccessMode.Field);
            });

            // 📅 Дата рождения
            _ = builder.Property(person => person.DateBirth)
                .HasComment("Дата рождения")
                .HasColumnType("date");

            // 📅 Дата смерти
            _ = builder.Property(person => person.DateDeath)
                .HasComment("Дата смерти")
                .HasColumnType("date");

            // 🗂 Имя таблицы и комментарий
            _ = builder.ToTable("Persons", t => t.HasComment("Персоны: авторы, переводчики, редакторы"));
        }
    }
}