// <copyright file="MapperConfiguration.cs" company="Филипченко Марина Алексеевна">
// Copyright (c) Филипченко Марина Алексеевна 2026. Library.
// </copyright>

namespace WebAPI.Extentions
{
    using Microsoft.Extensions.DependencyInjection;

    /// <summary>
    /// Конфигуратор <c>AutoMapper</c>.
    /// </summary>
    public static class MapperConfiguration
    {
        /// <summary>
        /// Регистрирует в <c>DI</c> абстракции, необходимые для взаимодействия с <c>AutoMapper</c>.
        /// </summary>
        /// <param name="services"> Коллекция сервисов (<c>DI</c>). </param>
        /// <returns> Обновлённая коллекция сервисов (<c>DI</c>). </returns>
        public static IServiceCollection AddAutoMapper(this IServiceCollection services)
        {
            return services.AddAutoMapper(cfg => cfg.AddMaps(typeof(Program).Assembly));
        }
    }
}
