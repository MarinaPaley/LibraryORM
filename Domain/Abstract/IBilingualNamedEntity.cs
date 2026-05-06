// <copyright file="IBilingualNamedEntity.cs" company="Филипченко Марина Алексеевна">
// Copyright (c) Филипченко Марина Алексеевна 2026. Library.
// </copyright>

namespace Domain.Abstract
{
    /// <summary>
    /// Интерфейс базовой сущности, имеющей оригинальное и переводное имя.
    /// </summary>
    public interface IBilingualNamedEntity : INamedEntity
    {
        /// <summary>
        /// Оригинальное имя.
        /// </summary>
        public Title? OriginName { get; set; }
    }
}
