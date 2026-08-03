// <copyright file="NamedModel.cs" company="Филипченко Марина Алексеевна">
// Copyright (c) Филипченко Марина Алексеевна 2026. Library.
// </copyright>
namespace WebAPI.Mapping.Models.Abstract.Out
{
    /// <summary>
    /// Именованая модель.
    /// </summary>
    public abstract class NamedModel : INamedModel
    {
        /// <inheritdoc/>
        public string Name { get; set; }
    }
}
