// <copyright file="BookTypeConfiguration.cs" company="Филипченко Марина Алексеевна">
// Copyright (c) Филипченко Марина Алексеевна 2026. Library.
// </copyright>

namespace DataAccessLayer.Configurations
{
    using DataAccessLayer.Configurations.Abstractions;
    using Domain;

    /// <summary>
    /// Конфигурация правил отображения сущности (<see cref="BookType"/> в таблицах БД.
    /// </summary>
    internal sealed class BookTypeConfiguration : BaseNamedEntityConfiguration<BookType>
    {
        /// <summary>
        /// Инициализирует новый экземпляр класса <see cref="BookTypeConfiguration"/>.
        /// </summary>
        public BookTypeConfiguration()
            : base(nameComment: "Тип книги", nameIsUnique: true)
        {
        }
    }
}
