// <copyright file="IBilingualNamedModel.cs" company="Филипченко Марина Алексеевна">
// Copyright (c) Филипченко Марина Алексеевна 2026. Library.
// </copyright>

namespace WebAPI.Mapping.Models.Abstract
{
    /// <summary>
    /// Модель переводной сущности.
    /// </summary>
    public interface IBilingualNamedModel : INamedModel
    {
        /// <summary>
        /// Получает или задает оригинальное имя.
        /// </summary>
        string? OriginName { get; set; }
    }
}
