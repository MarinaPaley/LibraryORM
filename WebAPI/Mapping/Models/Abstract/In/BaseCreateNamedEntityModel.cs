// <copyright file="BaseCreateNamedEntityModel.cs" company="Филипченко Марина Алексеевна">
// Copyright (c) Филипченко Марина Алексеевна 2026. Library.
// </copyright>

namespace WebAPI.Mapping.Models.Abstract.In
{
    /// <summary>
    /// Базовый класс для create модели именованной сущности.
    /// </summary>
    public abstract class BaseCreateNamedEntityModel : BaseCreateEntityModel, ICreateNamedModel
    {
        /// <inheritdoc/>
        public string Name { get; set; }
    }
}
