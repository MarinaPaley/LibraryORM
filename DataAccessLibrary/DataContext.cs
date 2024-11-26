// <copyright file="DataContext.cs" company="Васильева Марина Алексеевна">
// Copyright (c) Васильева Марина Алексеевна 2024. Library.
// </copyright>

namespace DataAccessLibrary
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
        public DbSet<Author> Authors { get; } = default!;

        /// <summary>
        /// Книги.
        /// </summary>
        public DbSet<Book> Books { get; } = default!;

        /// <summary>
        /// Полки.
        /// </summary>
        public DbSet<Shelf> Shelves { get; } = default!;

        /// <inheritdoc/>
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            _ = modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        }
    }
}
