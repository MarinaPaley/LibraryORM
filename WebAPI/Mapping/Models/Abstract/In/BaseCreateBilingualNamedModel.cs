// <copyright file="BaseCreateBilingualNamedModel.cs" company="Филипченко Марина Алексеевна">
// Copyright (c) Филипченко Марина Алексеевна 2026. Library.
// </copyright>

namespace WebAPI.Mapping.Models.Abstract.In
{
    /// <summary>
    /// Базовый класс для create модели именованной сущности, имеющей оригинальное название.
    /// </summary>
    public abstract class BaseCreateBilingualNamedModel : BaseCreateNamedEntityModel, ICreateBilingualNamedModel
    {
        /// <inheritdoc/>
        public string? OriginName { get; set; }
    }
}
