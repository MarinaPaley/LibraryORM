// <copyright file="StreetProfile.cs" company="Филипченко Марина Алексеевна">
// Copyright (c) Филипченко Марина Алексеевна 2026. Library.
// </copyright>

namespace WebAPI.Mapping.Profiles
{
    using Domain;
    using WebAPI.Mapping.Models.InModels;
    using WebAPI.Mapping.OutModels;
    using WebAPI.Mapping.Profiles.Abstract;

    /// <summary>
    /// Класс профиля для <see cref="Street"/>.
    /// </summary>
    public sealed class StreetProfile : NamedProfile<Street, StreetCreateModel, StreetUpdateModel, StreetOutMode>
    {
    }
}
