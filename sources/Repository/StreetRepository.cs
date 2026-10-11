// <copyright file="StreetRepository.cs" company="Филипченко Марина Алексеевна">
// Copyright (c) Филипченко Марина Алексеевна 2026. Library.
// </copyright>
namespace Repository
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using DataAccessLayer;
    using Domain;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.Logging;
    using Repository.Abstract;

    /// <summary>
    /// Репозиторий для класса <see cref="Street"/>.
    /// </summary>
    public sealed class StreetRepository : BaseRepository<Street, StreetRepository>, IStreetRepository
    {
        /// <summary>
        /// Инициализирует новый экземпляр класса <see cref="StreetRepository"/>.
        /// </summary>
        /// <param name="dataContext"> Контекст доступа к данным.</param>
        /// <param name="logger"> Логгер. </param>
        /// <exception cref="ArgumentNullException">
        /// В случае если <paramref name="dataContext"/> или <paramref name="logger"/> – <see langword="null"/>.
        /// </exception>
        public StreetRepository(DataContext dataContext, ILogger<StreetRepository> logger)
            : base(dataContext, logger)
        {
        }

        /// <inheritdoc/>
        public async Task<IEnumerable<City>> GetCities(string streetName)
        {
            return await this.GetAll()
                .Where(street => street.Name.Value.Contains(streetName))
                .Select(street => street.City)
                .Distinct()
                .ToListAsync();
        }

        /// <inheritdoc/>
        protected override IQueryable<Street> GetAll(bool track = false)
        {
            var result = this.DataContext.Streets
                .Include(street => street.City);

            if (!track)
            {
                result.AsNoTracking();
            }

            return result;
        }
    }
}
