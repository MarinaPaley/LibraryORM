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
    using Repository.Abstract;

    /// <summary>
    /// Репозиторий для класса <see cref="City"/>.
    /// </summary>
    public sealed class CityRepository : BaseRepository<City>
    {
        /// <summary>
        /// Инициализирует новый экземпляр класса <see cref="CityRepository"/>.
        /// </summary>
        /// <param name="dataContext"> Контекст доступа к данным.</param>
        /// <exception cref="ArgumentNullException">
        /// В случае если <paramref name="dataContext"/> – <see langword="null"/>.
        /// </exception>
        public CityRepository(DataContext dataContext)
            : base(dataContext)
        {
        }

        /// <summary>
        /// Получает список городов, в которых есть указанная улица.
        /// </summary>
        /// <param name="street"> Название улицы.</param>
        /// <returns> Список городов.</returns>
        public IEnumerable<City> GetCities(string street)
        {
            return this.GetAll()
                .Where(city => city.Streets
                    .Any(s => s.Name.Value.Contains(street)));
        }

        /// <summary>
        /// Получает идентификатор города.
        /// </summary>
        /// <param name="cityName"> Город. </param>
        /// <returns> Идентификатор города. </returns>
        public async Task<Guid?> GetIdAsync(string cityName)
        {
            return (await this.GetAll()
                .FirstOrDefaultAsync(city => city.Name.Value == cityName))?.Id;
        }

        /// <summary>
        /// Получает название города.
        /// </summary>
        /// <param name="id"> Идентификатор города. </param>
        /// <returns> Название города. </returns>
        public async Task<string?> GetCityAsync(Guid id)
        {
            return (await this.GetAll()
                .FirstOrDefaultAsync(city => city.Id == id))
                ?.Name?
                .Value;
        }

        /// <summary>
        /// Получает список улиц указанного города.
        /// </summary>
        /// <param name="id"> Идентификатор города. </param>
        /// <returns> Список улиц. </returns>
        public async Task<IEnumerable<Street>?> GetStreetsAsync(Guid id)
        {
            return (await this.GetAll()
                .FirstOrDefaultAsync(city => city.Id == id))
                ?.Streets
                ?? Enumerable.Empty<Street>();
        }

        /// <inheritdoc/>
        protected override IQueryable<City> GetAll()
        {
            return this.DataContext.Cities
                .Include(city => city.Streets);
        }
    }
}
