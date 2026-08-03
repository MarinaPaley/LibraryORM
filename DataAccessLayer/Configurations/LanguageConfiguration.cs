// <copyright file="LanguageConfiguration.cs" company="Филипченко Марина Алексеевна">
// Copyright (c) Филипченко Марина Алексеевна 2026. Library.
// </copyright>

namespace DataAccessLayer.Configurations
{
    using DataAccessLayer.Configurations.Abstractions;
    using Domain;

    /// <summary>
    /// Конфигурация правил отображения сущности (<see cref="Language"/> в таблицах БД.
    /// </summary>
    internal sealed class LanguageConfiguration : BaseNamedEntityConfiguration<Language>
    {
        /// <summary>
        /// Инициализирует новый экземпляр класса <see cref="LanguageConfiguration"/>.
        /// </summary>
        public LanguageConfiguration()
            : base(tableName: "Languages", nameComment: "Язык", nameIsUnique: true)
        {
        }
    }
}
