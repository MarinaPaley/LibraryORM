// <copyright file="StreetConfigurationTests.cs" company="Филипченко Марина Алексеевна">
// Copyright (c) Филипченко Марина Алексеевна 2026. Library.
// </copyright>

namespace DataAccessLayer.Tests
{
    using DataAccessLayer.Tests.Abstract;
    using Domain;
    using NUnit.Framework;

    /// <summary>
    /// Тесты для <see cref="DataAccessLayer.Configurations.StreetConfiguration"/>.
    /// </summary>
    [TestFixture]
    internal sealed class StreetConfigurationTests : BaseConfigurationTests
    {
        [Test]
        public void AddEntityToDatabase_Success()
        {
            // arrange
            var city = new City("Город");
            var street = new Street("Город", city);

            // act
            _ = this.DataContext.Add(street);
            _ = this.DataContext.SaveChanges(); // <-- если что-то плохо, то тут БУМ!
            this.DataContext.ChangeTracker.Clear();

            // assert
            var result = this.DataContext.Find<Street>(street.Id);

            Assert.That(result, Is.Not.Null);
            Assert.That(result!.Name, Is.EqualTo(street.Name));
        }
    }
}
