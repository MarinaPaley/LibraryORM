// <copyright file="IOutModel.cs" company="Филипченко Марина Алексеевна">
// Copyright (c) Филипченко Марина Алексеевна 2026. Library.
// </copyright>

namespace WebAPI.Models.Abstract
{
    /// <summary>
    /// Выходная модель.
    /// </summary>
    public interface IOutModel : IModel
    {
        /// <summary>
        /// Идентификатор.
        /// </summary>
        Guid Id { get; set; }
    }
}
