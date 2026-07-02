// <copyright file="BaseController.cs" company="Филипченко Марина Алексеевна">
// Copyright (c) Филипченко Марина Алексеевна 2026. Library.
// </copyright>

namespace WebAPI.Controllers.Abstract
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Domain.Abstract;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Mvc;
    using Repository.Abstract;

    /// <summary>
    /// Базовый абстрактный класс для контроллеров.
    /// </summary>
    /// <typeparam name="TRepository"> Целевой тип репозитория. </typeparam>
    /// <typeparam name="TEntity"> Целевой тип сущности.</typeparam>
    public abstract class BaseController<TRepository, TEntity> : ControllerBase
        where TEntity : Entity<TEntity>
        where TRepository : BaseRepository<TEntity>
    {
        /// <summary>
        /// Репозиторий.
        /// </summary>
        protected readonly TRepository repository;

        /// <summary>
        /// Инициализирует новый экземпляр класса <see cref="BaseController{TRepository, TEntity}"/>.
        /// </summary>
        /// <param name="repository"> Репозиторий. </param>
        /// <exception cref="ArgumentNullException">
        /// В случае если <paramref name="repository"/> – <see langword="null"/>.
        /// </exception>
        protected BaseController(TRepository repository)
        {
            this.repository = repository ?? throw new ArgumentNullException(nameof(repository));
        }

        /// <summary>
        /// Показывает все сущности.
        /// </summary>
        /// <returns> Результат выполнения запроса. </returns>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        protected IActionResult GetAll()
        {
            var result = this.repository.Filter().ToList();

            return this.Ok(result);
        }

        /// <summary>
        /// Получает сущность по идентификатору.
        /// </summary>
        /// <param name="id"> Идентификатор сущности. </param>
        /// <returns> Сущность. </returns>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        protected IActionResult GetById(Guid id)
        {
            var result = this.repository.GetAsync(id);

            return this.Ok(result);
        }

        /// <summary>
        /// Удаляет сущность.
        /// </summary>
        /// <param name="entity"> Сущность.</param>
        /// <returns> <see langword="true"/>, если удалили, иначе <see langword="false"/>.</returns>
        [HttpDelete]
        [ProducesResponseType(StatusCodes.Status200OK)]
        protected IActionResult Delete(TEntity entity)
        {
            var result = this.repository.DeleteAsync(entity);

            return this.Ok(result);
        }

        /// <summary>
        /// Сщздает сущность.
        /// </summary>
        /// <param name="entity"> Сущность. </param>
        /// <returns> Созданная сущность. </returns>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        protected IActionResult Create(TEntity entity)
        {
            var result = this.repository.CreateAsync(entity);

            return this.Ok(result);
        }

        /// <summary>
        /// Обновляет сущность.
        /// </summary>
        /// <param name="entity"> Сущность.</param>
        /// <returns> Обновленная сущность. </returns>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        protected IActionResult Update(TEntity entity)
        {
            var result = this.repository.UpdateAsync(entity);

            return this.Ok(result);
        }
    }
}
