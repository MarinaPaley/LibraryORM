// <copyright file="ShelfRepositoryTests.cs" company="Филипченко Марина Алексеевна">
// Copyright (c) Филипченко Марина Алексеевна 2026. Library.
// </copyright>

namespace Repository.Tests
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Domain;
    using NUnit.Framework;

    /// <summary>
    /// Модульные тесты для <see cref="ShelfRepository"/>.
    /// </summary>
    [TestFixture]
    internal sealed class ShelfRepositoryTests
        : BaseReposytoryTests<ShelfRepository, Shelf>
    {
        private static readonly Person Person = new ("Толстой", "Лев", birthYear: 1828, deathYear: 1910);
        private static readonly Author Author = new (Person);
        private static readonly ISet<Author> Authors = new HashSet<Author>() { Author };
        private static readonly Language Language = new ("Русский");
        private static readonly ISet<Language> Languages = new HashSet<Language>() { Language };
        private static readonly Publisher Publisher = new ("Издательство");
        private static readonly BookType BookType = new ("Книга");
        private static readonly Manuscript Manuscript1 = new ("Война и мир", Languages, Authors);
        private static readonly Manuscript Manuscript2 = new ("Анна Каренина", Languages, Authors);
        private static readonly Book Book1 = new (
                "Война и мир",
                1000,
                "1",
                BookType,
                Publisher,
                2024,
                new HashSet<Manuscript>() { Manuscript1 });

        private static readonly Book Book2 = new (
            "Анна Каренина",
            500,
            "1",
            BookType,
            Publisher,
            2024,
            new HashSet<Manuscript>() { Manuscript2 });

        [SetUp]
        public void SetUp()
        {
            _ = this.DataContext.Database.EnsureCreated();
        }

        [TearDown]
        public void TearDown()
        {
            _ = this.DataContext.Database.EnsureDeleted();
        }

        [Test]
        public void Create_ValidData_Success()
        {
            // arrange
            var shelf = new Shelf("Тестовая");

            // act
            _ = this.Repository.CreateAsync(shelf);

            // arrange
            var result = this.DataContext.Find<Shelf>(shelf.Id);

            Assert.That(result, Is.Not.Null);
            Assert.That(result!.Name, Is.EqualTo(shelf.Name));
        }

        [Test]
        public async Task Get_ValidData_Success()
        {
            // arrange
            var shelf = new Shelf("Тестовая");

            this.DataContext.Add(shelf);
            _ = this.DataContext.SaveChangesAsync();

            // act
            var result = await this.Repository.GetAsync(shelf.Id);

            // assert
            Assert.That(result, Is.Not.Null);
            Assert.That(result!.Name, Is.EqualTo(shelf.Name));
        }

        [Test]
        public async Task Update_ValidData_Success()
        {
            // arrange
            var newName = "Новое имя";

            var shelf = new Shelf("Тестовая");

            this.DataContext.Add(shelf);
            _ = this.DataContext.SaveChangesAsync();

            // act
            shelf.Name = new Title(newName);
            var result = await this.Repository.UpdateAsync(shelf);

            // assert
            Assert.That(result, Is.Not.Null);
            Assert.That(result!.Name.Value, Is.EqualTo(newName));
        }

        [Test]
        public void Delete_ValidData_Success()
        {
            // arrange
            var shelf = new Shelf("Тестовая");

            this.DataContext.Add(shelf);
            this.DataContext.SaveChanges();

            // act
            _ = this.Repository.DeleteAsync(shelf);

            // assert
            var result = this.DataContext.Find<Shelf>(shelf.Id);

            Assert.That(result, Is.Null);
        }

        [Test]
        public async Task GetBooksCountAsync_ValidData_Success()
        {
            // arrange
            var shelf = new Shelf("Тестовая");

            var item1 = new Item(Book1);
            var item2 = new Item(Book2);

            shelf.AddBook(item1);
            shelf.AddBook(item2);

            this.DataContext.Add(shelf);
            _ = this.DataContext.SaveChangesAsync();

            // act
            var result = await this.Repository.GetCountBooksAsync(shelf.Id);

            // assret
            Assert.AreEqual(result, 2);
        }

        [Test]
        public async Task GetBooksCountByShelfName_ValidData_Success()
        {
            // arrange
            var shelf = new Shelf("Тестовая");

            var item1 = new Item(Book1);
            var item2 = new Item(Book2);

            shelf.AddBook(item1);
            shelf.AddBook(item2);

            _ = await this.DataContext.AddAsync(shelf);
            _ = await this.DataContext.SaveChangesAsync();

            // act
            var result = await this.Repository.GetCountBooksAsync("Тестовая");

            // assret
            Assert.AreEqual(result, 2);
        }

        [Test]
        public async Task GetIdByName_ValidData_Success()
        {
            // arrange
            var shelf = new Shelf("Тестовая");

            var item1 = new Item(Book1);
            var item2 = new Item(Book2);

            shelf.AddBook(item1);
            shelf.AddBook(item2);

            _ = await this.DataContext.AddAsync(shelf);
            _ = await this.DataContext.SaveChangesAsync();

            // act
            var result = await this.Repository.GetIdByName("Тестовая");

            // assert
            Assert.That(result, Is.Not.Null);
            Assert.AreEqual(shelf.Id, result.Value);
        }
    }
}
