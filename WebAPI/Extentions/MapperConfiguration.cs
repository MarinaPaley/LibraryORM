// <copyright file="MapperConfiguration.cs" company="Филипченко Марина Алексеевна">
// Copyright (c) Филипченко Марина Алексеевна 2026. Library.
// </copyright>

namespace WebAPI.Extentions
{
    using Microsoft.Extensions.DependencyInjection;

    public static class MapperConfiguration
    {
        public static IServiceCollection AddAutoMapper(this IServiceCollection services)
        {
            return services.AddAutoMapper(cfg => cfg.AddMaps(typeof(Program).Assembly));
        }
    }
}
