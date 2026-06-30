// <copyright file="StreetController.cs" company="Филипченко Марина Алексеевна">
// Copyright (c) Филипченко Марина Алексеевна 2026. Library.
// </copyright>

namespace WebAPI.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Domain;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Mvc;
    using Repository;

    /// <summary>
    /// Контроллер для Улиц.
    /// </summary>
    [Route("api/streets")]
    [ApiController]
    public class StreetController : ControllerBase
    {
        private readonly StreetRepository repository;

        /// <summary>
        /// Инициализирует новый экземпляр класса <see cref="StreetController"/>.
        /// </summary>
        /// <param name="repository"> Репозиторий. </param>
        /// <exception cref="ArgumentNullException">
        /// В случае если <paramref name="repository"/> – <see langword="null"/>.
        /// </exception>
        public StreetController(StreetRepository repository)
        {
            this.repository = repository ?? throw new ArgumentNullException(nameof(repository)); ;
        }

        /// <summary>
        /// Показывает все улицы.
        /// </summary>
        /// <returns> Результат выполнения запроса. </returns>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<Street>))]
        public IActionResult Index()
        {
            var result = this.repository.Filter().ToList();

            return this.Ok(result);
        }
    }
}
