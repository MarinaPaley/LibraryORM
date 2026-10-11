// <copyright file="BaseController.cs" company="Филипченко Марина Алексеевна">
// Copyright (c) Филипченко Марина Алексеевна 2026. Library.
// </copyright>

namespace WebAPI.Controllers.Abstract
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;
    using AutoMapper;
    using Domain.Abstract;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Extensions.Logging;
    using Repository.Abstract;
    using WebAPI.Mapping.Models.Abstract.In;
    using WebAPI.Mapping.Models.Abstract.Out;

    /// <summary>
    /// Базовый абстрактный класс для контроллеров.
    /// </summary>
    /// <typeparam name="TRepository"> Целевой тип репозитория. </typeparam>
    /// <typeparam name="TEntity"> Целевой тип сущности.</typeparam>
    /// <typeparam name="TCreateModel"> Целевой тип СОЗДАВАЕМОЙ модели. </typeparam>
    /// <typeparam name="TUpdateModel" > Целевой тип ИЗМЕНЯЕМОЙ модели. </typeparam>
    /// <typeparam name="TOutModel"> Целевой тип ВЫХОДНОЙ модели. </typeparam>
    /// <typeparam name="TController"> Целевой тип контроллера. </typeparam>
    [ApiController]
    public abstract class BaseController<TRepository, TEntity, TCreateModel, TUpdateModel, TOutModel, TController> : ControllerBase
        where TRepository : BaseRepository<TEntity, TRepository>
        where TEntity : Entity<TEntity>
        where TCreateModel : class, ICreateModel
        where TUpdateModel : class, IUpdateModel
        where TOutModel : class, IOutModel
        where TController : BaseController<TRepository, TEntity, TCreateModel, TUpdateModel, TOutModel, TController>
    {
        private readonly string controllerUrl;

        /// <summary>
        /// Инициализирует новый экземпляр класса <see cref="BaseController{TRepository, TEntity, TCreateModel, TUpdateModel, TOutModel, TController}"/>.
        /// </summary>
        /// <param name="repository"> Репозиторий. </param>
        /// <param name="mapper"> Маппер. </param>
        /// <param name="logger"> Логгер. </param>
        /// <param name="controllerUrl"> Целевой <c>URL</c> контроллера. </param>
        /// <exception cref="ArgumentNullException">
        /// В случае если <paramref name="repository"/>, <paramref name="mapper"/>
        /// или <paramref name="logger"/> – <see langword="null"/>.
        /// </exception>
        /// <exception cref="ArgumentOutOfRangeException">
        /// В случае если <paramref name="controllerUrl"/> – <see langword="null"/>,
        /// <see cref="string.Empty"/> или строка состоящая из пробельных символов (whitespaces).
        /// </exception>
        protected BaseController(
            TRepository repository,
            IMapper mapper,
            ILogger<TController> logger,
            string controllerUrl)
        {
            this.Repository = repository ?? throw new ArgumentNullException(nameof(repository));
            this.Mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            this.Logger = logger ?? throw new ArgumentNullException(nameof(logger));

            // @TODO: Место для улучшения
            ArgumentOutOfRangeException.ThrowIfNullOrWhiteSpace(controllerUrl);
            this.controllerUrl = controllerUrl;
        }

        /// <summary>
        /// Репозиторий.
        /// </summary>
        protected TRepository Repository { get; }

        /// <summary>
        /// Маппер.
        /// </summary>
        protected IMapper Mapper { get; }

        /// <summary>
        /// Логгер.
        /// </summary>
        protected ILogger<TController> Logger { get; }

        /// <summary>
        /// Показывает все сущности.
        /// </summary>
        /// <returns> Результат выполнения запроса. </returns>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public virtual IActionResult GetAll()
        {
            var result = this.Repository
                .Filter()
                .Select(this.Mapper.Map<TEntity, TOutModel>);

            return this.Ok(result);
        }

        /// <summary>
        /// Получает сущность по идентификатору.
        /// </summary>
        /// <param name="id"> Идентификатор сущности. </param>
        /// <returns> Сущность. </returns>
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public virtual async Task<IActionResult> GetAsync([FromRoute] Guid id)
        {
            var entity = await this.Repository.GetAsync(id);

            return entity is not null
                ? this.Ok(this.Mapper.Map<TEntity, TOutModel>(entity))
                : this.NotFound(id);
        }

        /// <summary>
        /// Удаляет сущность.
        /// </summary>
        /// <param name="id"> Идентификатор удаляемой сущности.</param>
        /// <returns>
        /// <see langword="true"/>, если удалили, иначе <see langword="false"/>.
        /// </returns>
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public virtual async Task<IActionResult> DeleteAsync([FromRoute] Guid id)
        {
            var result = await this.Repository.DeleteAsync(id);

            return this.Ok(result);
        }

        /// <summary>
        /// Создает сущность.
        /// </summary>
        /// <param name="model"> Модель. </param>
        /// <returns> Созданная сущность. </returns>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        public virtual async Task<IActionResult> CreateAsync([FromBody] TCreateModel model)
        {
            if (!this.ModelState.IsValid)
            {
                this.Logger.LogError("Модель {Model} не удовлетворяет ограничениям.", model);

                return this.BadRequest($"Модель {model} не удовлетворяет ограничениям.");
            }

            var entity = this.Mapper.Map<TCreateModel, TEntity>(model);

            var result = await this.Repository.CreateAsync(entity);

            return this.Created($"{this.controllerUrl}/{result.Id}", result);
        }

        /// <summary>
        /// Обновляет сущность.
        /// </summary>
        /// <remarks>
        /// @TODO: Стоит использовать метод <c>PATCH</c> (<see cref="HttpPatchAttribute"/>).
        /// Более подробно описан <see href="https://developer.mozilla.org/ru/docs/Web/HTTP/Reference/Methods/PATCH"/>.
        /// </remarks>
        /// <param name="model"> Модель.</param>
        /// <returns> Обновленная сущность. </returns>
        [HttpPost("update")] // [HttpPatch]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public virtual async Task<IActionResult> UpdateAsync([FromBody] TUpdateModel model)
        {
            if (!this.ModelState.IsValid)
            {
                this.Logger.LogError("Модель {Model} не удовлетворяет ограничениям.", model);

                return this.BadRequest($"Модель {model} не удовлетворяет ограничениям.");
            }

            var entity = this.Mapper.Map<TUpdateModel, TEntity>(model);

            var result = await this.Repository.UpdateAsync(entity);

            return this.Ok(result);
        }
    }
}
