// <copyright file="INamedEntity.cs" company="Филипченко Марина Алексеевна">
// Copyright (c) Филипченко Марина Алексеевна 2026. Library.
// </copyright>

namespace Domain.Abstract
{
    /// <summary>
    /// Интерфейс базовой именованной сущности.
    /// </summary>
    public interface INamedEntity : IEntity
    {
        /// <summary>
        /// Наименование.
        /// </summary>
        public Title Name { get; set; }
    }
}
