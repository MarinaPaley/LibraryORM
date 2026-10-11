// <copyright file="BaseUpdateNamedModel.cs" company="Филипченко Марина Алексеевна">
// Copyright (c) Филипченко Марина Алексеевна 2026. Library.
// </copyright>

namespace WebAPI.Mapping.Models.Abstract.In
{
    /// <summary>
    /// Базовый класс для update модели именованной сущности.
    /// </summary>
    public abstract class BaseUpdateNamedModel : BaseUpdateEntityModel, IUpdateNamedModel
    {
        /// <inheritdoc/>
        public string Name { get; set; }
    }
}
