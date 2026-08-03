// <copyright file="AuthorConfiguration.cs" company="Филипченко Марина Алексеевна">
// Copyright (c) Филипченко Марина Алексеевна 2026. Library.
// </copyright>

namespace DataAccessLayer.Configurations
{
    using DataAccessLayer.Configurations.Abstractions;
    using Domain;

    /// <summary>
    /// Конфигурация правил отображения сущности (<see cref="Author"/>) в таблицу БД.
    /// </summary>
    internal sealed class AuthorConfiguration : BasePersonRoleConfiguration<Author>
    {
        /// <summary>
        /// Инициализирует новый экземпляр класса <see cref="AuthorConfiguration"/>.
        /// </summary>
        public AuthorConfiguration()
            : base(
                person => person.Author,
                author => author.Manuscripts,
                manuscript => manuscript.Authors)
        {
        }
    }
}
