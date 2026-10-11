// <copyright file="BaseOutBilingualNamedModel.cs" company="Филипченко Марина Алексеевна">
// Copyright (c) Филипченко Марина Алексеевна 2026. Library.
// </copyright>

namespace WebAPI.Mapping.Models.Abstract.Out
{
    /// <summary>
    /// Базовый класс для выходной модели именованной сущности, имеющей оригинальное название.
    /// </summary>
    public abstract class BaseOutBilingualNamedModel : BaseOutNamedEntityModel, IOutBilingualNamedModel
    {
        /// <inheritdoc/>
        public string? OriginName { get; set; }
    }
}
