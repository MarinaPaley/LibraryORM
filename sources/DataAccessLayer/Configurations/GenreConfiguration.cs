// <copyright file="GenreConfiguration.cs" company="Филипченко Марина Алексеевна">
// Copyright (c) Филипченко Марина Алексеевна 2026. Library.
// </copyright>

namespace DataAccessLayer.Configurations
{
    using DataAccessLayer.Configurations.Abstractions;
    using Domain;

    /// <summary>
    /// Конфигурация правил отображения сущности (<see cref="Genre"/> в таблицах БД.
    /// </summary>
    internal sealed class GenreConfiguration : BaseNamedEntityConfiguration<Genre>
    {
        /// <summary>
        /// Инициализирует новый экземпляр класса <see cref="GenreConfiguration"/>.
        /// </summary>
        public GenreConfiguration()
            : base(nameComment: "Жанр", nameIsUnique: true)
        {
        }
    }
}
