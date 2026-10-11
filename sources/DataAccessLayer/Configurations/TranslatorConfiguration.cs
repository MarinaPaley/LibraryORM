// <copyright file="TranslatorConfiguration.cs" company="Филипченко Марина Алексеевна">
// Copyright (c) Филипченко Марина Алексеевна 2026. Library.
// </copyright>

namespace DataAccessLayer.Configurations
{
    using DataAccessLayer.Configurations.Abstractions;
    using Domain;

    /// <summary>
    /// Конфигурация правил отображения сущности (<see cref="Translator"/>) в таблицу БД.
    /// </summary>
    internal sealed class TranslatorConfiguration : BasePersonRoleConfiguration<Translator>
    {
        /// <summary>
        /// Инициализирует новый экземпляр класса <see cref="TranslatorConfiguration"/>.
        /// </summary>
        public TranslatorConfiguration()
            : base(
                person => person.Translator,
                translator => translator.Manuscripts,
                manuscript => manuscript.Translators,
                tableName: "Translators")
        {
        }
    }
}
