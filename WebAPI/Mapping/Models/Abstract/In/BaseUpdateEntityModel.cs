// <copyright file="BaseUpdateEntityModel.cs" company="Филипченко Марина Алексеевна">
// Copyright (c) Филипченко Марина Алексеевна 2026. Library.
// </copyright>

namespace WebAPI.Mapping.Models.Abstract.In
{
    using System;

    /// <summary>
    /// Базовый класс для модели модификации сущности с идентификатором.
    /// </summary>
    public abstract class BaseUpdateEntityModel : IUpdateModel
    {
        /// <inheritdoc/>
        public Guid Id { get; set; }
    }
}
