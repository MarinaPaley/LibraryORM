// <copyright file="ColorRepository.cs" company="Филипченко Марина Алексеевна">
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
    /// Репозиторий для класса <see cref="Color"/>.
    /// </summary>
    public sealed class ColorRepository : BaseRepository<Color, ColorRepository>, IColorRepository
    {
        /// <summary>
        /// Инициализирует новый экземпляр класса <see cref="ColorRepository"/>.
        /// </summary>
        /// <param name="dataContext"> Контекст доступа к данным.</param>
        /// <param name="logger"> Логгер. </param>
        /// <exception cref="ArgumentNullException">
        /// В случае если <paramref name="dataContext"/> или <paramref name="logger"/> – <see langword="null"/>.
        /// </exception>
        public ColorRepository(DataContext dataContext, ILogger<ColorRepository> logger)
            : base(dataContext, logger)
        {
        }

        /// <inheritdoc/>
        public async Task<string?> GetColorAsync(Guid id)
        {
            return (await this.GetAll()
                .SingleOrDefaultAsync(color => color.Id == id))
                ?.Name.Value;
        }

        /// <inheritdoc/>
        public async Task<List<string>> GetColors()
        {
            return await this.GetAll()
                .Select(color => color.Name.Value)
                .ToListAsync();
        }

        /// <inheritdoc/>
        public async Task<Guid?> GetIdAsync(string color)
        {
            return (await this.GetAll()
                .SingleOrDefaultAsync(value => value.Name.Value == color))
                ?.Id;
        }

        /// <inheritdoc/>
        protected override IQueryable<Color> GetAll(bool track = false)
        {
            var result = this.DataContext.Colors;

            if (!track)
            {
                result.AsNoTracking();
            }

            return result;
        }
    }
}
