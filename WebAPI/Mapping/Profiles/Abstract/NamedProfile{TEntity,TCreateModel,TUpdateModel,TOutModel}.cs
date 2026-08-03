// <copyright file="NamedProfile{TEntity,TCreateModel,TUpdateModel,TOutModel}.cs" company="Филипченко Марина Алексеевна">
// Copyright (c) Филипченко Марина Алексеевна 2026. Library.
// </copyright>

namespace WebAPI.Mapping.Profiles.Abstract
{
    using AutoMapper;
    using Domain;
    using Domain.Abstract;
    using WebAPI.Mapping.Models.Abstract;
    using WebAPI.Mapping.Models.Abstract.In;
    using WebAPI.Mapping.Models.Abstract.Out;
    using WebAPI.Mapping.Models.InModels;

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
    public abstract class NamedProfile<TEntity, TCreateModel, TUpdateModel, TOutModel>
        : EntityProfile<TEntity, TCreateModel, TUpdateModel, TOutModel>
        where TEntity : NamedEntity<TEntity>
        where TCreateModel : class, ICreateNamedModel
        where TUpdateModel : class, IUpdateNamedModel
        where TOutModel : class, IOutNamedModel
    {
        /// <summary>
        /// Инициализирует новый экземпляр класса <see cref="NamedProfile{TEntity, TCreateModel, TUpdateModel , TOutModel}"/>.
        /// </summary>
        protected NamedProfile()
        {
            this.CreateMap = this.CreateMap
                .ForCtorParam("name", ops => ops.MapFrom(s => this.GetCyrillicName(s)));

            this.UpdateMap = this.UpdateMap
                .ForCtorParam("name", ops => ops.MapFrom(s => this.GetCyrillicName(s)));
        }

        /// <summary>
        /// Получает кириллическое название.
        /// </summary>
        /// <typeparam name="TInModel"> Целевой тип ВХОДНОЙ (IN) поименованной модели. </typeparam>
        /// <param name="model"> Входная модель. </param>
        /// <returns> Кириллическое название. </returns>
        protected virtual string GetCyrillicName<TInModel>(TInModel model)
            where TInModel : class, IInNamedModel
        {
            return model.Name;
        }
    }
}
