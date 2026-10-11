// <copyright file="IEntity.cs" company="Филипченко Марина Алексеевна">
// Copyright (c) Филипченко Марина Алексеевна 2026. Library.
// </copyright>

namespace Domain.Abstract
{
    using System;

    /// <summary>
    /// Базовый интерфейс сущности.
    /// </summary>
    public interface IEntity
    {
        /// <summary>
        /// Идентификатор.
        /// </summary>
        Guid Id { get; }
    }
}
