// <copyright file="CityProfile.cs" company="Филипченко Марина Алексеевна">
// Copyright (c) Филипченко Марина Алексеевна 2026. Library.
// </copyright>

namespace WebAPI.Mapping.Profiles
{
    using Domain;
    using WebAPI.Mapping.Models.InModels;
    using WebAPI.Mapping.OutModels;
    using WebAPI.Mapping.Profiles.Abstract;

    /// <summary>
    /// Класс профиля для <see cref="City"/>.
    /// </summary>
    public sealed class CityProfile : NamedProfile<City, CityCreateModel, CityUpdateModel, CityOutModel>
    {
        /// <summary>
        /// Инициализирует новый экземпляр класса <see cref="CityProfile"/>.
        /// </summary>
        public CityProfile()
            : base()
        {
            this.CreateMap.ForMember(d => d.Streets, opt => opt.Ignore());
            this.UpdateMap.ForMember(d => d.Streets, opt => opt.Ignore());
            this.OutputMap.ForSourceMember(s => s.Streets, opt => opt.DoNotValidate());
        }
    }
}
