// <copyright file="BilingualNamedProfile.cs" company="Филипченко Марина Алексеевна">
// Copyright (c) Филипченко Марина Алексеевна 2026. Library.
// </copyright>

namespace WebAPI.Mapping.Profiles.Abstract
{
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
    public abstract class BilingualNamedProfile<TEntity, TCreateModel, TUpdateModel, TOutModel>
        : EntityProfile<TEntity, TCreateModel, TUpdateModel, TOutModel>
        where TEntity : BilingualNamedEntity<TEntity>
        where TCreateModel : class, ICreateBilingualNamedModel
        where TUpdateModel : class, IUpdateBilingualNamedModel
        where TOutModel : class, IOutBilingualNamedModel
    {
        /// <summary>
        /// Инициализирует новый экземпляр класса <see cref="BilingualNamedProfile{TEntity, TCreateModel, TUpdateModel , TOutModel}"/>.
        /// </summary>
        protected BilingualNamedProfile()
        {
            this.CreateMap = this.CreateMap
                .ForCtorParam("name", ops => ops.MapFrom(s => this.GetCyrillicName(s)))
                .ForCtorParam("originName", ops => ops.MapFrom(_ => null as string));

            this.UpdateMap = this.UpdateMap
                .ForCtorParam("name", ops => ops.MapFrom(s => this.GetCyrillicName(s)))
                .ForCtorParam("originName", ops => ops.MapFrom(_ => null as string));
        }

        /// <summary>
        /// Получает кириллическое название.
        /// </summary>
        /// <typeparam name="TInModel"> Целевой тип ВХОДНОЙ (IN) поименованной модели. </typeparam>
        /// <param name="model"> Входная модель. </param>
        /// <returns> Кириллическое название. </returns>
        protected virtual string GetCyrillicName<TInModel>(TInModel model)
            where TInModel : class, IInBilingualNamedModel
        {
            return model.Name;
        }
    }
}
