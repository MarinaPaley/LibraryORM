// <copyright file="ShelfController.cs" company="Филипченко Марина Алексеевна">
// Copyright (c) Филипченко Марина Алексеевна 2026. Library.
// </copyright>

namespace WebAPI.Controllers
{
    using System;
    using Domain;
    using Microsoft.AspNetCore.Mvc;
    using Repository;
    using WebAPI.Controllers.Abstract;

    /// <summary>
    /// Контроллер для Полок.
    /// </summary>
    [Route("api/shelves")]
    [ApiController]
    public class ShelfController : BaseController<ShelfRepository, Shelf>
    {
        /// <summary>
        /// Инициализирует новый экземпляр класса <see cref="ShelfController"/>.
        /// </summary>
        /// <param name="repository"> Репозиторий. </param>
        /// <exception cref="ArgumentNullException">
        /// В случае если <paramref name="repository"/> – <see langword="null"/>.
        /// </exception>
        public ShelfController(ShelfRepository repository)
            : base(repository)
        {
        }
    }
}
