// <copyright file="ShelfProfile.cs" company="Филипченко Марина Алексеевна">
// Copyright (c) Филипченко Марина Алексеевна 2026. Library.
// </copyright>

namespace WebAPI.Mapping.Profiles
{
    using Domain;
    using WebAPI.Mapping.Models.InModels;
    using WebAPI.Mapping.OutModels;
    using WebAPI.Mapping.Profiles.Abstract;

    /// <summary>
    /// Класс профиля для <see cref="Shelf"/>.
    /// </summary>
    public sealed class ShelfProfile : NamedProfile<Shelf, ShelfCreateModel, ShelfUpdateModel, ShelfOutModel>
    {
        /// <summary>
        /// Инициализирует новый экземпляр класса <see cref="ShelfProfile"/>.
        /// </summary>
        public ShelfProfile()
            : base()
        {
            this.CreateMap.ForMember(d => d.Cabinet, opt => opt.Ignore());
            this.UpdateMap.ForMember(d => d.Cabinet, opt => opt.Ignore());
            this.OutputMap.ForSourceMember(s => s.Cabinet, opt => opt?.ToString());

            this.UpdateMap.ForMember(d => d.Items, opt => opt.Ignore());
            this.CreateMap.ForMember(d => d.Items, opt => opt.Ignore());

            // @TODO: пока так
            this.OutputMap.ForSourceMember(s => s.Items, opt => opt.DoNotValidate());
        }
    }
}
