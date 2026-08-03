// <copyright file="CityRepository.cs" company="Филипченко Марина Алексеевна">
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
    /// Репозиторий для класса <see cref="City"/>.
    /// </summary>
    public sealed class CityRepository : BaseRepository<City, CityRepository>, ICityRepository
    {
        /// <summary>
        /// Инициализирует новый экземпляр класса <see cref="CityRepository"/>.
        /// </summary>
        /// <param name="dataContext"> Контекст доступа к данным.</param>
        /// <param name="logger"> Логгер. </param>
        /// <exception cref="ArgumentNullException">
        /// В случае если <paramref name="dataContext"/> или <paramref name="logger"/> – <see langword="null"/>.
        /// </exception>
        public CityRepository(DataContext dataContext, ILogger<CityRepository> logger)
            : base(dataContext, logger)
        {
        }

        /// <inheritdoc/>
        public async Task<IEnumerable<City>> GetCities(string street)
        {
            return this.GetAll()
                .Where(city => city.Streets
                    .Any(s => s.Name.Value.Contains(street)));
        }

        /// <inheritdoc/>
        public async Task<Guid?> GetIdAsync(string cityName)
        {
            return (await this.GetAll()
                .FirstOrDefaultAsync(city => city.Name.Value == cityName))?.Id;
        }

        /// <inheritdoc/>
        public async Task<string?> GetCityAsync(Guid id)
        {
            return (await this.GetAll()
                .FirstOrDefaultAsync(city => city.Id == id))
                ?.Name?
                .Value;
        }

        /// <inheritdoc/>
        public async Task<IEnumerable<Street>?> GetStreetsAsync(Guid id)
        {
            return (await this.GetAll()
                .FirstOrDefaultAsync(city => city.Id == id))
                ?.Streets
                ?? Enumerable.Empty<Street>();
        }

        /// <inheritdoc/>
        protected override IQueryable<City> GetAll(bool track = false)
        {
            var result = this.DataContext.Cities
                .Include(city => city.Streets);

            if (!track)
            {
                result.AsNoTracking();
            }

            return result;
        }
    }
}
