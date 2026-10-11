// <copyright file="BaseConfigurationTests.cs" company="Филипченко Марина Алексеевна">
// Copyright (c) Филипченко Марина Алексеевна 2026. Library.
// </copyright>

namespace DataAccessLayer.Tests.Abstract
{
    using System;
    using DataAccessLayer;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.Logging;
    using NUnit.Framework;

    /// <summary>
    /// Базовый тип для реализации модульных тестов конфигураций (<see cref="IEntityTypeConfiguration{TEntity}"/>).
    /// </summary>
    internal abstract class BaseConfigurationTests : IDisposable
    {
        private bool disposed; // 1. Добавляем флаг, чтобы избежать двойного вызова Dispose

        /// <summary>
        /// Инициализирует новый экземпляр класса <see cref="BaseConfigurationTests"/>.
        /// </summary>
        /// <param name="minimumLogLevel"> Минимальный уровень логируемых сообщений. </param>
        protected BaseConfigurationTests(LogLevel minimumLogLevel = LogLevel.Debug)
        {
            this.DataContext = new ServiceCollection()
                .AddDbContext<DataContext>(
                    options => options
                        .UseInMemoryDatabase($"InMemoryDB_{Guid.NewGuid()}")
                        .EnableDetailedErrors()
                        .EnableSensitiveDataLogging()
                        .LogTo(Console.WriteLine, minimumLogLevel))
                .BuildServiceProvider()
                .GetService<DataContext>()
                ?? throw new Exception($"Cannot get {typeof(DataContext).FullName} from DI");
        }

        /// <summary>
        /// Контекст доступа к данным.
        /// </summary>
        protected DataContext DataContext { get; }

        /// <summary>
        /// Очищает трекер изменений после каждого теста.
        /// </summary>
        [TearDown]
        public void TearDown()
        {
            this.DataContext.ChangeTracker.Clear();
        }

        /// <inheritdoc/>
        public void Dispose()
        {
            this.Dispose(true);
            GC.SuppressFinalize(this); // 2. Стандартная практика, даже если финализатора нет
        }

        /// <summary>
        /// Освобождает управляемые ресурсы. Может быть переопределен в классах-наследниках.
        /// </summary>
        /// <param name="disposing"> Если <see langword="true"/>, освобождаются управляемые ресурсы. </param>
        protected virtual void Dispose(bool disposing)
        {
            if (this.disposed)
            {
                return;
            }

            if (disposing)
            {
                // 3. Освобождаем только управляемые ресурсы
                this.DataContext?.Dispose();
            }

            this.disposed = true;
        }
    }
}
