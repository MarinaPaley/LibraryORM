// <copyright file="BaseUpdateBilingualNamedModel.cs" company="Филипченко Марина Алексеевна">
// Copyright (c) Филипченко Марина Алексеевна 2026. Library.
// </copyright>

namespace WebAPI.Mapping.Models.Abstract.In
{
    /// <summary>
    /// Базовый класс для update модели именованной сущности, имеющей оригинальное название.
    /// </summary>
    public abstract class BaseUpdateBilingualNamedModel : BaseUpdateNamedModel, IUpdateBilingualNamedModel
    {
        /// <inheritdoc/>
        public string? OriginName { get; set; }
    }
}
