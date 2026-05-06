// <copyright file="IInModel.cs" company="Филипченко Марина Алексеевна">
// Copyright (c) Филипченко Марина Алексеевна 2026. Library.
// </copyright>

namespace WebAPI.Models.Abstract
{
    /// <summary>
    /// Входная модель.
    /// </summary>
    public interface IInModel : IModel
    {
        /// <summary>
        /// Идентификатор.
        /// </summary>
        Guid Id { get; set; }
    }
}
