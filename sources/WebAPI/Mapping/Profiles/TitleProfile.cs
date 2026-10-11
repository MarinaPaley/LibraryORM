// <copyright file="TitleProfile.cs" company="Филипченко Марина Алексеевна">
// Copyright (c) Филипченко Марина Алексеевна 2026. Library.
// </copyright>

namespace WebAPI.Mapping.Profiles
{
    using AutoMapper;
    using Domain;

    /// <summary>
    /// Профиль для <see cref="Title"/>.
    /// </summary>
    public sealed class TitleProfile : Profile
    {
        /// <summary>
        /// Инициализирует новый экземпляр класса <see cref="TitleProfile"/>.
        /// </summary>
        public TitleProfile()
        {
            this.CreateMap<Title, string>()
                .ConvertUsing(s => s.Value);

            this.CreateMap<string, Title>()
                .ForCtorParam("value", opt => opt.MapFrom(s => s));
        }
    }
}
