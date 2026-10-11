// <copyright file="ShelfController.cs" company="Филипченко Марина Алексеевна">
// Copyright (c) Филипченко Марина Алексеевна 2026. Library.
// </copyright>

namespace WebAPI.Controllers
{
    using System;
    using System.Net.Mime;
    using AutoMapper;
    using Domain;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Extensions.Logging;
    using Repository;
    using WebAPI.Controllers.Abstract;
    using WebAPI.Mapping.Models.InModels;
    using WebAPI.Mapping.OutModels;

    /// <summary>
    /// Контроллер для Полок.
    /// </summary>
    [ApiController]
    [Produces(MediaTypeNames.Application.Json)]
    [Route(ShelfController.ControllerUrl)]
    public sealed class ShelfController
        : BaseController<ShelfRepository, Shelf, ShelfCreateModel, ShelfUpdateModel, ShelfOutModel, ShelfController>
    {
        private const string ControllerUrl = "api/shelves";

        /// <summary>
        /// Инициализирует новый экземпляр класса <see cref="ShelfController"/>.
        /// </summary>
        /// <param name="repository"> Репозиторий. </param>
        /// <param name="mapper"> Маппер. </param>
        /// <param name="logger"> Логгер. </param>
        /// <exception cref="ArgumentNullException">
        /// В случае если <paramref name="repository"/>, <paramref name="mapper"/>
        /// или <paramref name="logger"/> – <see langword="null"/>.
        /// </exception>
        public ShelfController(
            ShelfRepository repository,
            IMapper mapper,
            ILogger<ShelfController> logger)
            : base(repository, mapper, logger, ShelfController.ControllerUrl)
        {
        }
    }
}
