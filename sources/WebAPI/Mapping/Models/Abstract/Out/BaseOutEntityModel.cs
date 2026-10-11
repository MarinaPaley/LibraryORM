// <copyright file="BaseOutEntityModel.cs" company="Филипченко Марина Алексеевна">
// Copyright (c) Филипченко Марина Алексеевна 2026. Library.
// </copyright>

namespace WebAPI.Mapping.Models.Abstract.Out
{
    using System;

    /// <summary>
    /// Базовый класс для выходной модели сущности.
    /// </summary>
    public abstract class BaseOutEntityModel : IOutModel
    {
        /// <inheritdoc/>
        public Guid Id { get; set; }
    }
}
