// <copyright file="StreetController.cs" company="Филипченко Марина Алексеевна">
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
    /// Контроллер для Улиц.
    /// </summary>
    [Route("api/streets")]
    [ApiController]
    public class StreetController : BaseController<StreetRepository, Street>
    {
        /// <summary>
        /// Инициализирует новый экземпляр класса <see cref="StreetController"/>.
        /// </summary>
        /// <param name="repository"> Репозиторий. </param>
        /// <exception cref="ArgumentNullException">
        /// В случае если <paramref name="repository"/> – <see langword="null"/>.
        /// </exception>
        public StreetController(StreetRepository repository)
            : base(repository)
        {
        }
    }
}
