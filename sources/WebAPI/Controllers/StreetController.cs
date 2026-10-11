// <copyright file="StreetController.cs" company="Филипченко Марина Алексеевна">
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
    /// Контроллер для Улиц.
    /// </summary>
    [ApiController]
    [Produces(MediaTypeNames.Application.Json)]
    [Route(StreetController.ControllerUrl)]
    public sealed class StreetController
        : BaseController<StreetRepository, Street, StreetCreateModel, StreetUpdateModel, StreetOutMode, StreetController>
    {
        private const string ControllerUrl = "api/streets";

        /// <summary>
        /// Инициализирует новый экземпляр класса <see cref="StreetController"/>.
        /// </summary>
        /// <param name="repository"> Репозиторий. </param>
        /// <param name="mapper"> Маппер. </param>
        /// <param name="logger"> Логгер. </param>
        /// <exception cref="ArgumentNullException">
        /// В случае если <paramref name="repository"/>, <paramref name="mapper"/>
        /// или <paramref name="logger"/> – <see langword="null"/>.
        public StreetController(
            StreetRepository repository,
            IMapper mapper,
            ILogger<StreetController> logger)
            : base(repository, mapper, logger, StreetController.ControllerUrl)
        {
        }
    }
}
