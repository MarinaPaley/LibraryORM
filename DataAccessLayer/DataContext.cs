// <copyright file="DataContext.cs" company="Филипченко Марина Алексеевна">
// Copyright (c) Филипченко Марина Алексеевна 2026. Library.
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

        /// <summary>
        /// Города.
        /// </summary>
        public DbSet<City> Cities { get; init; }

        /// <summary>
        /// Улицы.
        /// </summary>
        public DbSet<Street> Streets { get; init; }

        /// <summary>
        /// Адреса.
        /// </summary>
        public DbSet<Address> Adresses { get; set; }

        /// <summary>
        /// Издательства.
        /// </summary>
        public DbSet<Publisher> Publishers { get; set; }

        /// <inheritdoc/>
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            _ = modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        }
    }
}
