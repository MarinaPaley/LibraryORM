// <copyright file="StreetRepositoryTests.cs" company="Филипченко Марина Алексеевна">
// Copyright (c) Филипченко Марина Алексеевна 2026. Library.
// </copyright>

namespace Repository.Tests
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Domain;
    using NUnit.Framework;

    /// <summary>
    /// Модульные тесты для <see cref="StreetRepository"/>.
    /// </summary>
    [TestFixture]
    internal sealed class StreetRepositoryTests
        : BaseRepositoryTests<StreetRepository, Street>
    {
        [Test]
        public async Task Delete_ValidData_Success()
        {
            // arrange
            var city = new City("Город");
            var street = new Street("Улица", city);

            _ = await this.DataContext.AddAsync(city);
            _ = await this.DataContext.SaveChangesAsync();

            // act
            _ = this.Repository.DeleteAsync(street);

            // assert
            var result = await this.DataContext.FindAsync<Street>(street.Id);

            Assert.That(result, Is.Null);
        }

        [Test]
        public async Task GetCities_ValidData_Success()
        {
            // arrange
            var name = "Улица";
            var city = new City("Город");
            var cities = new List<City> { city };
            var street = new Street("Улица", city);

            _ = await this.DataContext.AddAsync(city);
            _ = await this.DataContext.AddAsync(street);
            _ = await this.DataContext.SaveChangesAsync();

            // act
            var result = await this.Repository.GetCities(name);

            // assert
            Assert.That(result, Is.EqualTo(cities));
        }
    }
}
