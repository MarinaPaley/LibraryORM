// <copyright file="SeriaConfiguration.cs" company="Филипченко Марина Алексеевна">
// Copyright (c) Филипченко Марина Алексеевна 2026. Library.
// </copyright>

namespace DataAccessLayer.Configurations
{
    using DataAccessLayer.Configurations.Abstractions;
    using Domain;

    /// <summary>
    /// Конфигурация правил отображения сущности (<see cref="Seria"/> в таблицах БД.
    /// </summary>
    internal sealed class SeriaConfiguration : BaseNamedEntityConfiguration<Seria>
    {
        /// <summary>
        /// Инициализирует новый экземпляр класса <see cref="SeriaConfiguration"/>.
        /// </summary>
        public SeriaConfiguration()
            : base(tableName: "Serias", nameComment: "Серия")
        {
        }
    }
}
