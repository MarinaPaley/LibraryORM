// <copyright file="INamedModel.cs" company="Филипченко Марина Алексеевна">
// Copyright (c) Филипченко Марина Алексеевна 2026. Library.
// </copyright>

namespace WebAPI.Mapping.Models.Abstract
{
    /// <summary>
    /// Модель именованной сущности.
    /// </summary>
    public interface INamedModel : IModel
    {
        /// <summary>
        /// Получает или задает имя сущности.
        /// </summary>
        string Name { get; set; }
    }
}
