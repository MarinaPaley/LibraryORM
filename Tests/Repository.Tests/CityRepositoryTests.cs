// <copyright file="CityRepositoryTests.cs" company="Филипченко Марина Алексеевна">
// Copyright (c) Филипченко Марина Алексеевна 2026. Library.
// </copyright>

namespace Repository.Tests
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Domain;
    using NUnit.Framework;

    /// <summary>
    /// Модульные тесты для <see cref="CityRepository"/>.
    /// </summary>
    [TestFixture]
    internal sealed class CityRepositoryTests
        : BaseReposytoryTests<CityRepository, City>
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
            var city = new City("Город");

            // act
            _ = this.Repository.CreateAsync(city);

            // assert
            var result = this.DataContext.Find<City>(city.Id);

            Assert.That(result, Is.EqualTo(city));
        }

        [Test]
        public void Update_ValidData_Success()
        {
            // arrange
            var city = new City("Город");
            var street = new Street("Улица", city);

            this.DataContext.Add(city);
            this.DataContext.Add(street);
            this.DataContext.SaveChanges();

            // act
            _ = this.Repository.Update(city);

            // assert
            var result = this.DataContext.Find<City>(city.Id);
            Assert.That(result?.Streets.Count, Is.EqualTo(1));
            Assert.That(result?.Streets.Contains(street), Is.True);
        }

        [Test]
        public void Delete_ValidData_Success()
        {
            // arrange
            var city = new City("Город");
            var street = new Street("Улица", city);

            this.DataContext.Add(city);
            this.DataContext.Add(street);
            this.DataContext.SaveChanges();

            // act
            _ = this.Repository.DeleteAsync(city);

            // assert
            var result = this.DataContext.Find<City>(city.Id);

            Assert.That(result, Is.Null);
        }

        [Test]
        public async Task GetId_ValidData_Success()
        {
            // arrange
            var name = "Город";
            var city = new City(name);
            var street = new Street("Улица", city);

            _ = this.DataContext.Add(city);
            _ = this.DataContext.Add(street);
            _ = this.DataContext.SaveChangesAsync();

            this.DataContext.ChangeTracker.Clear();

            // act
            var result = await this.Repository.GetIdAsync(name);

            // assert
            Assert.That(result, Is.EqualTo(city.Id));
        }

        [Test]
        public async Task GetCity_ValidData_Success()
        {
            // arrange
            var name = "Город";
            var city = new City(name);

            _ = this.DataContext.Add(city);
            _ = this.DataContext.SaveChangesAsync();
            this.DataContext.ChangeTracker.Clear();

            // act
            var result = await this.Repository.GetCityAsync(city.Id);

            // assert
            Assert.That(result, Is.EqualTo(name));
        }

        [Test]
        public async Task GetStreets_ValidData_Success()
        {
            // arrange
            var name = "Город";
            var city = new City(name);
            var street = new Street("Улица", city);
            var streets = new List<Street> { street };

            _ = this.DataContext.Add(city);
            _ = this.DataContext.Add(city);
            _ = this.DataContext.SaveChangesAsync();
            this.DataContext.ChangeTracker.Clear();

            // act
            var result = await this.Repository.GetStreetsAsync(city.Id);

            // assert
            Assert.That(result, Is.EqualTo(streets));
        }
    }
}
