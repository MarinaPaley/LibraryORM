// <copyright file="CityConfigurationTests.cs" company="Филипченко Марина Алексеевна">
// Copyright (c) Филипченко Марина Алексеевна 2026. Library.
// </copyright>

namespace DataAccessLayer.Tests
{
    using DataAccessLayer.Tests.Abstract;
    using Domain;
    using NUnit.Framework;

    /// <summary>
    /// Тесты для <see cref="DataAccessLayer.Configurations.CityConfiguration"/>.
    /// </summary>
    [TestFixture]
    internal sealed class CityConfigurationTests : BaseConfigurationTests
    {
        [Test]
        public void AddEntityToDatabase_Success()
        {
            // arrange
            var city = new City("Город");

            // act
            _ = this.DataContext.Add(city);
            _ = this.DataContext.SaveChanges(); // <-- если что-то плохо, то тут БУМ!
            this.DataContext.ChangeTracker.Clear();

            // assert
            var result = this.DataContext.Find<City>(city.Id);

            Assert.That(result, Is.Not.Null);
            Assert.That(result!.Name, Is.EqualTo(city.Name));
        }
    }
}
