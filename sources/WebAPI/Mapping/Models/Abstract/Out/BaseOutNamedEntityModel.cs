// <copyright file="BaseOutNamedEntityModel.cs" company="Филипченко Марина Алексеевна">
// Copyright (c) Филипченко Марина Алексеевна 2026. Library.
// </copyright>
namespace WebAPI.Mapping.Models.Abstract.Out
{
    using System;

    /// <summary>
    /// Базовая OUT именованая модель.
    /// </summary>
    public abstract class BaseOutNamedEntityModel : NamedModel, IOutNamedModel
    {
        /// <inheritdoc/>
        public Guid Id { get; set; }
    }
}
