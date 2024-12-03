// <copyright file="BaseReposytoryTests.cs" company="Васильева Марина Алексеевна">
// Copyright (c) Васильева Марина Алексеевна 2024. Library.
// </copyright>

namespace Repository.Tests
{
    using System;
    using DataAccessLayer;
    using Domain;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.Logging;

    /// <summary>
    /// Базовый тип тестов для репозиториев.
    /// </summary>
    /// <typeparam name="TRepository"> Целевой тип тестируемого репозитория. </typeparam>
    /// <typeparam name="TEntity"> Целевой тип сущности тестируемого репозитория. </typeparam>
    internal abstract class BaseReposytoryTests<TRepository, TEntity>
        where TRepository : BaseRepository<TEntity>
        where TEntity : class, IEntity<TEntity>
    {
        private static readonly string ConnectionString = @"Data Source=.\tests.db";

        private readonly ServiceProvider serviceProvider;

        protected BaseReposytoryTests()
        {
            this.serviceProvider = new ServiceCollection()
                .AddDbContext<DataContext>(
                    builder => builder.UseSqlite(ConnectionString)
                        .EnableDetailedErrors()
                        .EnableSensitiveDataLogging()
                        .LogTo(Console.WriteLine, LogLevel.Error))
                .AddScoped<TRepository>()
                .BuildServiceProvider();
        }

        protected DataContext DataContext
        {
            get => this.serviceProvider.GetService<DataContext>()
                ?? throw new Exception($"Cannot get {typeof(DataContext).Name}");
        }

        protected TRepository Repository
        {
            get => this.serviceProvider.GetService<TRepository>()
                ?? throw new Exception($"Cannot get {typeof(TRepository).Name}");
        }
    }
}
