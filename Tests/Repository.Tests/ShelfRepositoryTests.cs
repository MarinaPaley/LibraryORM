// <copyright file="ShelfRepositoryTests.cs" company="Филипченко Марина Алексеевна">
// Copyright (c) Филипченко Марина Алексеевна 2026. Library.
// </copyright>

namespace Repository.Tests
{
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
    }
}
