// <copyright file="DataContext.cs" company="Филипченко Марина Алексеевна">
// Copyright (c) Филипченко Марина Алексеевна 2026. Library.
// </copyright>

namespace DataAccessLayer
{
    using System.Reflection;
    using Domain;
    using Domain.Abstract;
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
        /// Авторы / Редакторы/ Переводчики.
        /// </summary>
        public DbSet<Contributor> Contributors { get; init; }

        /// <summary>
        /// Книги.
        /// </summary>
        public DbSet<Book> Books { get; init; }

        /// <summary>
        /// Полки.
        /// </summary>
        public DbSet<Shelf> Shelves { get; init; }

        /// <summary>
        /// Шкафы.
        /// </summary>
        public DbSet<Cabinet> Cabinets { get; init; }

        /// <summary>
        /// Комнаты.
        /// </summary>
        public DbSet<Room> Rooms { get; init; }

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
        public DbSet<Address> Addresses { get; set; }

        /// <summary>
        /// Издательства.
        /// </summary>
        public DbSet<Publisher> Publishers { get; set; }

        /// <summary>
        /// Рукописи.
        /// </summary>
        public DbSet<Manuscript> Manuscripts { get; set; }

        /// <summary>
        /// Языки.
        /// </summary>
        public DbSet<Language> Languages { get; set; }

        /// <summary>
        /// Типы изданий.
        /// </summary>
        public DbSet<BookType> BookTypes { get; set; }

        /// <summary>
        /// Серии.
        /// </summary>
        public DbSet<Seria> Serias { get; set; }

        /// <summary>
        /// Жанры.
        /// </summary>
        public DbSet<Genre> Genres { get; set; }

        /// <summary>
        /// Персоны.
        /// </summary>
        public DbSet<Person> Persons { get; set; }

        /// <inheritdoc/>
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            _ = modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
            modelBuilder.Ignore<Title>();
        }
    }
}
