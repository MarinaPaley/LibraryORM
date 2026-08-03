// <copyright file="TagConfiguration.cs" company="Филипченко Марина Алексеевна">
// Copyright (c) Филипченко Марина Алексеевна 2026. Library.
// </copyright>

namespace DataAccessLayer.Configurations
{
    using DataAccessLayer.Configurations.Abstractions;
    using Domain;

    /// <summary>
    /// Конфигурация правил отображения сущности (<see cref="Tag"/> в таблицах БД.
    /// </summary>
    internal sealed class TagConfiguration : BaseBilingualNamedEntityConfiguration<Tag>
    {
        /// <summary>
        /// Инициализирует новый экземпляр класса <see cref="TagConfiguration"/>.
        /// </summary>
        public TagConfiguration()
            : base(
                  tableName: "Tags",
                  tableComment: "Теги",
                  nameComment: "Название тега",
                  originNameComment: "Оригинальное название тега",
                  nameIsUnique: true)
        {
        }
    }
}
