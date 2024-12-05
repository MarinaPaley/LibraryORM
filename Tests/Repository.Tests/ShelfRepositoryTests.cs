// <copyright file="ShelfRepositoryTests.cs" company="Васильева Марина Алексеевна">
// Copyright (c) Васильева Марина Алексеевна 2024. Library.
// </copyright>

namespace Repository.Tests
{
    using Domain;
    using NUnit.Framework;

    /// <summary>
    /// Модульные тесты для <see cref="ShelfRepository"/>.
    /// </summary>
    [TestFixture]
    internal sealed class ShelfRepositoryTests
        : BaseReposytoryTests<ShelfRepository, Shelf>
    {
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
            _ = this.Repository.Create(shelf);

            // arrange
            var result = this.DataContext.Find<Shelf>(shelf.Id);

            Assert.That(result, Is.Not.Null);
            Assert.That(result!.Name, Is.EqualTo(shelf.Name));
        }

        [Test]
        public void Get_ValidData_Success()
        {
            // arrange
            var shelf = new Shelf("Тестовая");

            this.DataContext.Add(shelf);
            this.DataContext.SaveChanges();

            // act
            var result = this.Repository.Get(shelf.Id);

            // assert
            Assert.That(result, Is.Not.Null);
            Assert.That(result!.Name, Is.EqualTo(shelf.Name));
        }

        [Test]
        public void Update_ValidData_Success()
        {
            // arrange
            var newName = "Новое имя";

            var shelf = new Shelf("Тестовая");

            this.DataContext.Add(shelf);
            this.DataContext.SaveChanges();

            // act
            shelf.Name = newName;
            var result = this.Repository.Update(shelf);

            // assert
            Assert.That(result, Is.Not.Null);
            Assert.That(result!.Name, Is.EqualTo(newName));
        }

        [Test]
        public void Delete_ValidData_Success()
        {
            // arrange
            var shelf = new Shelf("Тестовая");

            this.DataContext.Add(shelf);
            this.DataContext.SaveChanges();

            // act
            _ = this.Repository.Delete(shelf);

            // assert
            var result = this.DataContext.Find<Shelf>(shelf.Id);

            Assert.That(result, Is.Null);
        }
    }
}
