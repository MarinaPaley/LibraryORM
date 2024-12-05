// <copyright file="DataContext.cs" company="Васильева Марина Алексеевна">
// Copyright (c) Васильева Марина Алексеевна 2024. Library.
// </copyright>

namespace DataAccessLayer
{
    using System.Reflection;
    using Domain;
    using Microsoft.EntityFrameworkCore;

    /// <summary>
    /// Контекст доступа к данным.
    /// </summary>
    public sealed class DataContext : DbContext
    {
        private static readonly string ConnectionString = "User ID=postgres;Password=1;Host=localhost;Port=5432;Database=Library;";

        private static readonly DbContextOptions<DataContext> Options = new DbContextOptionsBuilder<DataContext>()
            .UseNpgsql(ConnectionString)
            .EnableDetailedErrors()
            .EnableSensitiveDataLogging()
            .LogTo(System.Console.WriteLine, Microsoft.Extensions.Logging.LogLevel.Error)
            .Options;

        /// <summary>
        /// Инициализирует новый экземпляр класса <see cref="DataContext"/>.
        /// </summary>
        public DataContext()
            : this(Options)
        {
        }

        /// <summary>
        /// Инициализирует новый экземпляр класса <see cref="DataContext"/>.
        /// </summary>
        /// <param name="options"> Опции настройки контекста. </param>
        public DataContext(DbContextOptions<DataContext> options)
            : base(options)
        {
        }

        /// <summary>
        /// Авторы.
        /// </summary>
        public DbSet<Author> Authors { get; init; }

        /// <summary>
        /// Книги.
        /// </summary>
        public DbSet<Book> Books { get; init; }

        /// <summary>
        /// Полки.
        /// </summary>
        public DbSet<Shelf> Shelves { get; init; }

        /// <inheritdoc/>
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            _ = modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        }
    }
}
