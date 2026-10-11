// <copyright file="BaseRepositoryTests{TRepository,TEntity}.cs" company="Филипченко Марина Алексеевна">
// Copyright (c) Филипченко Марина Алексеевна 2026. Library.
// </copyright>

namespace Repository.Tests
{
    using System;
    using DataAccessLayer;
    using Domain.Abstract;
    using Microsoft.Data.Sqlite;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.Logging;
    using NUnit.Framework;
    using Repository.Abstract;

    /// <summary>
    /// Базовый тип тестов для репозиториев с использованием In-Memory SQLite.
    /// </summary>
    /// <typeparam name="TRepository"> Целевой тип тестируемого репозитория. </typeparam>
    /// <typeparam name="TEntity"> Целевой тип сущности тестируемого репозитория. </typeparam>
    internal abstract class BaseRepositoryTests<TRepository, TEntity>
        where TRepository : BaseRepository<TEntity, TRepository>
        where TEntity : class, IEntity
    {
        private readonly SqliteConnection sharedConnection;
        private readonly ServiceProvider serviceProvider;
        private IServiceScope scope;
        private DataContext dataContext;
        private TRepository repository;

        protected BaseRepositoryTests()
        {
            // Инициализируем и открываем соединение. Оно должно оставаться открытым на весь цикл тестов.
            this.sharedConnection = new SqliteConnection("Data Source=:memory:");
            this.sharedConnection.Open();

            this.serviceProvider = new ServiceCollection()
                .AddLogging(loggingBuilder =>
                {
                    loggingBuilder.ClearProviders();
                    loggingBuilder.AddConsole();
                    loggingBuilder.SetMinimumLevel(LogLevel.Warning);
                })
                .AddDbContext<DataContext>(options =>
                {
                    // Передаем именно объект соединения, а не строку подключения.
                    options.UseSqlite(this.sharedConnection)
                           .EnableDetailedErrors()
                           .EnableSensitiveDataLogging()
                           .LogTo(Console.WriteLine, LogLevel.Trace);
                })
                .AddScoped<TRepository>()
                .BuildServiceProvider();

            // Создаем схему БД один раз для всего класса тестов.
            using var initContext = this.serviceProvider.GetRequiredService<DataContext>();
            _ = initContext.Database.EnsureCreated();
        }

        protected DataContext DataContext => this.dataContext;

        protected TRepository Repository => this.repository;

        [SetUp]
        public void SetUp()
        {
            this.scope = this.serviceProvider.CreateScope();
            this.dataContext = this.scope.ServiceProvider.GetRequiredService<DataContext>();
            this.repository = this.scope.ServiceProvider.GetRequiredService<TRepository>();

            _ = this.dataContext.Database.BeginTransaction();
        }

        [TearDown]
        public void TearDown()
        {
            var transaction = this.dataContext.Database.CurrentTransaction;
            transaction?.Rollback();

            this.dataContext.ChangeTracker.Clear();

            this.scope.Dispose();
            this.dataContext?.Dispose();
        }

        [OneTimeTearDown]
        public void OneTimeTearDown()
        {
            this.serviceProvider.Dispose();

            // Закрытие соединения уничтожает in-memory базу данных.
            // Вызывать EnsureDeleted() здесь не нужно, так как файла на диске нет.
            this.sharedConnection.Close();
            this.sharedConnection.Dispose();
        }
    }
}