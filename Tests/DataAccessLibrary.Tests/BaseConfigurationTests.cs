// <copyright file="BaseConfigurationTests.cs" company="Васильева Марина Алексеевна">
// Copyright (c) Васильева Марина Алексеевна 2024. Library.
// </copyright>

namespace DataAccessLibrary.Tests
{
    using System;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.Logging;

    /// <summary>
    /// Базовый тип для реализации модульных тестов конфигураций (<see cref="IEntityTypeConfiguration{TEntity}"/>).
    /// </summary>
    internal abstract class BaseConfigurationTests
    {
        private readonly IServiceCollection services;

        protected BaseConfigurationTests(LogLevel minimumLogLevel = LogLevel.Debug)
        {
            this.services = new ServiceCollection()
                .AddDbContext<DataContext>(
                    options => options
                        .UseInMemoryDatabase($"InMemoryDB_{Guid.NewGuid()}")
                        .EnableDetailedErrors()
                        .EnableSensitiveDataLogging()
                        .LogTo(Console.WriteLine, minimumLogLevel));

            this.DataContext = this.services.BuildServiceProvider().GetService<DataContext>()
                ?? throw new Exception("Cannot get DataContext");
        }

        protected DataContext DataContext { get; }
    }
}
