// <copyright file="ShelfController.cs" company="Филипченко Марина Алексеевна">
// Copyright (c) Филипченко Марина Алексеевна 2026. Library.
// </copyright>

namespace WebAPI.Controllers
{
    using Microsoft.AspNetCore.Mvc;
    using Repository;

    /// <summary>
    /// Контроллер для Полок.
    /// </summary>
    [Route("api/shelves")]
    [ApiController]
    public class ShelfController : ControllerBase
    {
        private readonly ShelfRepository repository;

        /// <summary>
        /// Инициализирует новый экземпляр класса <see cref="ShelfController"/>.
        /// </summary>
        /// <param name="repository"> Репозиторий. </param>
        public ShelfController(ShelfRepository repository)
        {
            this.repository = repository;
        }

        /// <summary>
        /// Показывает все полки.
        /// </summary>
        /// <returns> Результат выполнения запроса. </returns>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<Domain.Shelf>))]
        public IActionResult Index()
        {
            var result = this.repository.Filter().ToList();

            return this.Ok(result);
        }
    }
}
