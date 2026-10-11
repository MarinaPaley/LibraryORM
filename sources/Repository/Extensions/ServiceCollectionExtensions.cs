// <copyright file="ServiceCollectionExtensions.cs" company="Филипченко Марина Алексеевна">
// Copyright (c) Филипченко Марина Алексеевна 2026. Library.
// </copyright>

namespace Repository.Extensions
{
    using Microsoft.Extensions.DependencyInjection;

    /// <summary>
    /// Коллекция методов расширений для <see cref="IServiceCollection"/>.
    /// </summary>
    public static class ServiceCollectionExtensions
    {
        /// <summary>
        /// Регистрирует в DI все необходимые сервисы.
        /// </summary>
        /// <param name="services"> Коллекция сервисов (DI). </param>
        /// <returns> Обновлённая коллекция сервисов (DI). </returns>
        public static IServiceCollection AddServices(this IServiceCollection services)
        {
            _ = services.AddScoped<ShelfRepository>();
            _ = services.AddScoped<CityRepository>();
            _ = services.AddScoped<StreetRepository>();
            return services;
        }
    }
}
