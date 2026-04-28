// <copyright file="StreetRepositoryTests.cs" company="Филипченко Марина Алексеевна">
// Copyright (c) Филипченко Марина Алексеевна 2026. Library.
// </copyright>

namespace Repository.Tests
{
    using System.Collections.Generic;
    using Domain;
    using NUnit.Framework;

    /// <summary>
    /// Модульные тесты для <see cref="StreetRepository"/>.
    /// </summary>
    [TestFixture]
    internal sealed class StreetRepositoryTests
        : BaseReposytoryTests<StreetRepository, Street>
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
        public void Delete_ValidData_Success()
        {
            // arrange
            var city = new City("Город");
            var street = new Street("Улица", city);

            this.DataContext.Add(city);
            this.DataContext.Add(street);
            this.DataContext.SaveChanges();

            // act
            _ = this.Repository.DeleteAsync(street);

            // assert
            var result = this.DataContext.Find<Street>(street.Id);

            Assert.That(result, Is.Null);
        }

        [Test]
        public void GetCities_ValidData_Success()
        {
            // arrange
            var name = "Улица";
            var city = new City("Город");
            var cities = new List<City> { city };
            var street = new Street("Улица", city);

            _ = this.DataContext.Add(city);
            _ = this.DataContext.Add(street);
            _ = this.DataContext.SaveChanges();
            this.DataContext.ChangeTracker.Clear();

            // act
            var result = this.Repository.GetCities(name);

            // assert
            Assert.That(result, Is.EqualTo(cities));
        }
    }
}
