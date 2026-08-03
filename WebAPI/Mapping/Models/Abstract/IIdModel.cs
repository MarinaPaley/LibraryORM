// <copyright file="IIdModel.cs" company="Филипченко Марина Алексеевна">
// Copyright (c) Филипченко Марина Алексеевна 2026. Library.
// </copyright>

namespace WebAPI.Mapping.Models.Abstract
{
    using System;

    /// <summary>
    /// Маркерный интерфейс для моделей с идентификатором.
    /// </summary>
    public interface IIdModel : IModel
    {
        /// <summary>
        /// Получает или задает Идентификатор.
        /// </summary>
        Guid Id { get; set; }
    }
}
