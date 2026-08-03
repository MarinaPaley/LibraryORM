// <copyright file="EntityProfile{TEntity,TCreateModel,TUpdateModel,TOutModel}.cs" company="Филипченко Марина Алексеевна">
// Copyright (c) Филипченко Марина Алексеевна 2026. Library.
// </copyright>

namespace WebAPI.Mapping.Profiles.Abstract
{
    using AutoMapper;
    using Domain.Abstract;
    using WebAPI.Mapping.Models.Abstract.In;
    using WebAPI.Mapping.Models.Abstract.Out;

    /// <summary>
    /// Профиль, настраивающий правила отображения
    /// ИЗ целевого типа сущности (<typeparamref name="TEntity"/>)
    /// В целевой тип ВЫХОДНОЙ (OUT) модели (<typeparamref name="TOutModel"/>),
    /// а также правила отображения
    /// ИЗ целевого типа ВХОДНОЙ (IN) модели создания (CREATE) сущности (<typeparamref name="TCreateModel"/>)
    /// В целевой тип сущности (<typeparamref name="TEntity"/>) и
    /// ИЗ целевого типа ВХОДНОЙ (IN) модели обновления (UPDATE) сущности (<typeparamref name="TUpdateModel"/>)
    /// В целевой тип сущности (<typeparamref name="TEntity"/>).
    /// </summary>
    /// <typeparam name="TEntity"> Целевой тип сущности. </typeparam>
    /// <typeparam name="TCreateModel"> Целевой тип ВХОДНОЙ (IN) модели создания сущности. </typeparam>
    /// <typeparam name="TUpdateModel"> Целевой тип ВХОДНОЙ (IN) модели обновления сущности. </typeparam>
    /// <typeparam name="TOutModel"> Целевой тип ВЫХОДНОЙ (OUT) модели. </typeparam>
    public abstract class EntityProfile<TEntity, TCreateModel, TUpdateModel, TOutModel>
        : EntityOutProfile<TEntity, TOutModel>
        where TEntity : class, IEntity
        where TCreateModel : class, ICreateModel
        where TUpdateModel : class, IUpdateModel
        where TOutModel : class, IOutModel
    {
        /// <summary>
        /// Инициализирует новый экземпляр класса <see cref="EntityProfile{TEntity, TCreateModel, TUpdateModel, TOutModel}"/>.
        /// </summary>
        protected EntityProfile()
        {
            this.CreateMap = this.CreateMap<TCreateModel, TEntity>()
                .ForMember(d => d.Id, ops => ops.Ignore());

            this.UpdateMap = this.CreateMap<TUpdateModel, TEntity>()
                .ForMember(d => d.Id, ops => ops.MapFrom(s => s.Id));
        }

        /// <summary>
        /// Выражение, описывающее правила отображения
        /// ИЗ целевого типа ВХОДНОЙ (IN) модели СОЗДАНИЯ (CREATE) сущности (<typeparamref name="TCreateModel"/>)
        /// НА целевой тип сущности (<typeparamref name="TEntity"/>).
        /// </summary>
        protected IMappingExpression<TCreateModel, TEntity> CreateMap { get; set; }

        /// <summary>
        /// Выражение, описывающее правила отображения
        /// ИЗ целевого типа ВХОДНОЙ (IN) модели обновления (UPDATE) сущности (<typeparamref name="TUpdateModel"/>)
        /// НА целевой тип сущности (<typeparamref name="TEntity"/>).
        /// </summary>
        protected IMappingExpression<TUpdateModel, TEntity> UpdateMap { get; set; }
    }
}
