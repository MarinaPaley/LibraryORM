// <copyright file="EntityOutProfile.cs" company="Филипченко Марина Алексеевна">
// Copyright (c) Филипченко Марина Алексеевна 2026. Library.
// </copyright>

namespace WebAPI.Mapping.Profiles.Abstract
{
    using AutoMapper;
    using Domain.Abstract;
    using WebAPI.Mapping.Models.Abstract.Out;

    /// <summary>
    /// Профиль, настраивающий правила отображения
    /// ИЗ целевого типа сущности (<typeparamref name="TEntity"/>)
    /// В целевой тип ВЫХОДНОЙ (OUT) модели (<typeparamref name="TOutModel"/>).
    /// </summary>
    /// <typeparam name="TEntity"> Целевой тип сущности. </typeparam>
    /// <typeparam name="TOutModel">  Целевой тип ВЫХОДНОЙ (OUT) модели. </typeparam>
    public abstract class EntityOutProfile<TEntity, TOutModel> : Profile
        where TEntity : class, IEntity
        where TOutModel : class, IOutModel
    {
        /// <summary>
        /// Инициализирует новый экземпляр класса <see cref="EntityOutProfile{TEntity, TOutModel}"/>.
        /// </summary>
        protected EntityOutProfile()
        {
            this.OutputMap = this.CreateMap<TEntity, TOutModel>();
        }

        /// <summary>
        /// Выражение, описывающее правила отображения
        /// ИЗ целевого типа ВХОДНОЙ (IN) модели обновления (UPDATE) сущности (<typeparamref name="TUpdateModel"/>)
        /// НА целевой тип сущности (<typeparamref name="TEntity"/>).
        /// </summary>
        protected IMappingExpression<TEntity, TOutModel> OutputMap { get; set; }
    }
}
