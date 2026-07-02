// <copyright file="CityController.cs" company="Филипченко Марина Алексеевна">
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
    /// Контроллер для Городов.
    /// </summary>
    [Route("api/cities")]
    [ApiController]
    public class CityController : BaseController<CityRepository, City>
    {
        /// <summary>
        /// Инициализирует новый экземпляр класса <see cref="CityController"/>.
        /// </summary>
        /// <param name="repository"> Репозиторий. </param>
        /// <exception cref="ArgumentNullException">
        /// В случае если <paramref name="repository"/> – <see langword="null"/>.
        /// </exception>
        public CityController(CityRepository repository)
            : base(repository)
        {
        }
    }
}
