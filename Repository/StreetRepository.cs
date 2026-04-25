// <copyright file="StreetRepository.cs" company="Филипченко Марина Алексеевна">
// Copyright (c) Филипченко Марина Алексеевна 2026. Library.
// </copyright>
namespace Repository
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using DataAccessLayer;
    using Domain;
    using Microsoft.EntityFrameworkCore;
    using Repository.Abstract;

    /// <summary>
    /// Репозиторий для класса <see cref="Street"/>.
    /// </summary>
    public sealed class StreetRepository : BaseRepository<Street>
    {
        /// <summary>
        /// Инициализирует новый экземпляр класса <see cref="StreetRepository"/>.
        /// </summary>
        /// <param name="dataContext"> Контекст доступа к данным.</param>
        /// <exception cref="ArgumentNullException">
        /// В случае если <paramref name="dataContext"/> – <see langword="null"/>.
        /// </exception>
        public StreetRepository(DataContext dataContext)
            : base(dataContext)
        {
        }

        /// <summary>
        /// Показать список городов, имеющая указанную улицу.
        /// </summary>
        /// <param name="street"> Улица.</param>
        /// <returns> Список городов, в которых имеется указанная улица. </returns>
        public IEnumerable<City> GetCities(string street)
        {
            return this.DataContext.Streets
                .Where(s => s.Name.Value.Contains(street))
                .Select(s => s.City)
                .Distinct()
                .ToList();
        }

        /// <inheritdoc/>
        protected override IQueryable<Street> GetAll()
        {
            return this.DataContext.Streets
                .Include(street => street.City);
        }
    }
}
