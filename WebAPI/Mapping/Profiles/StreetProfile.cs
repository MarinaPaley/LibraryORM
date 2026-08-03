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
        /// <summary>
        /// Инициализирует новый экземпляр класса <see cref="StreetProfile"/>.
        /// </summary>
        public StreetProfile()
        {
            this.CreateMap.ForMember(d => d.City, ops => ops.MapFrom(s => s.Name));
            this.UpdateMap.ForMember(d => d.City, ops => ops.MapFrom(s => s.Name));
        }
    }
}
