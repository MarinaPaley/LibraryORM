// <copyright file="EditorConfiguration.cs" company="Филипченко Марина Алексеевна">
// Copyright (c) Филипченко Марина Алексеевна 2026. Library.
// </copyright>

namespace DataAccessLayer.Configurations
{
    using DataAccessLayer.Configurations.Abstractions;
    using Domain;

    /// <summary>
    /// Конфигурация правил отображения сущности (<see cref="Editor"/>) в таблицу БД.
    /// </summary>
    internal sealed class EditorConfiguration : BasePersonRoleConfiguration<Editor>
    {
        /// <summary>
        /// Инициализирует новый экземпляр класса <see cref="EditorConfiguration"/>.
        /// </summary>
        public EditorConfiguration()
            : base(
                personExpression: person => person.Editor,
                tableName: "Editors",
                tableComment: "Редакторы")
        {
        }
    }
}
