// <copyright file="IllustratorConfiguration.cs" company="Филипченко Марина Алексеевна">
// Copyright (c) Филипченко Марина Алексеевна 2026. Library.
// </copyright>

namespace DataAccessLayer.Configurations
{
    using DataAccessLayer.Configurations.Abstractions;
    using Domain;

    /// <summary>
    /// Конфигурация правил отображения сущности (<see cref="Illustrator"/>) в таблицу БД.
    /// </summary>
    internal sealed class IllustratorConfiguration : BasePersonRoleConfiguration<Illustrator>
    {
        /// <summary>
        /// Инициализирует новый экземпляр класса <see cref="IllustratorConfiguration"/>.
        /// </summary>
        public IllustratorConfiguration()
            : base(
                  personExpression: person => person.Illustrator,
                  tableName: "Illustrators",
                  tableComment: "Художники")
        {
        }
    }
}
