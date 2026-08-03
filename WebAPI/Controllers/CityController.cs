// <copyright file="CityController.cs" company="Филипченко Марина Алексеевна">
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
    /// Контроллер для Городов.
    /// </summary>
    [ApiController]
    [Produces(MediaTypeNames.Application.Json)]
    [Route(CityController.ControllerUrl)]
    public sealed class CityController
        : BaseController<CityRepository, City, CityCreateModel, CityUpdateModel, CityOutModel, CityController>
    {
        private const string ControllerUrl = "api/cities";

        /// <summary>
        /// Инициализирует новый экземпляр класса <see cref="CityController"/>.
        /// </summary>
        /// <param name="repository"> Репозиторий. </param>
        /// <param name="mapper"> Маппер. </param>
        /// <param name="logger"> Логгер. </param>
        /// <exception cref="ArgumentNullException">
        /// В случае если <paramref name="repository"/>, <paramref name="mapper"/>
        /// или <paramref name="logger"/> – <see langword="null"/>.
        /// </exception>
        public CityController(
            CityRepository repository,
            IMapper mapper,
            ILogger<CityController> logger)
            : base(repository, mapper, logger, CityController.ControllerUrl)
        {
        }
    }
}
