// <copyright file="ReviwerConfiguration.cs" company="Филипченко Марина Алексеевна">
// Copyright (c) Филипченко Марина Алексеевна 2026. Library.
// </copyright>

namespace DataAccessLayer.Configurations
{
    using DataAccessLayer.Configurations.Abstractions;
    using Domain;

    /// <summary>
    /// Конфигурация правил отображения сущности (<see cref="Reviewer"/>) в таблицу БД.
    /// </summary>
    internal sealed class ReviwerConfiguration : BasePersonRoleConfiguration<Reviewer>
    {
        /// <summary>
        /// Инициализирует новый экземпляр класса <see cref="ReviwerConfiguration"/>.
        /// </summary>
        public ReviwerConfiguration()
            : base(
                person => person.Reviewer,
                reviewer => reviewer.Manuscripts,
                manuscript => manuscript.Reviewers,
                tableName: "Reviwers")
        {
        }
    }
}
