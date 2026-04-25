// <copyright file="AddressTests.cs" company="Филипченко Марина Алексеевна">
// Copyright (c) Филипченко Марина Алексеевна 2026. Library.
// </copyright>

namespace Domain.Tests
{
    using Domain;
    using NUnit.Framework;

    /// <summary>
    /// Тесты на <see cref="Domain.Address"/>.
    /// </summary>
    [TestFixture]
    internal sealed class AddressTests
    {
        [Test]
        public void Address_Equals_SameFieldsDifferentId_ReturnsTrue()
        {
            // arrange
            var city = new City("Москва");
            var street = new Street("Ленина", city);

            var address1 = new Address(city, street, 10);
            var address2 = new Address(city, street, 10);

            // act & assert
            Assert.That(address1, Is.EqualTo(address2));
        }

        [Test]
        public void Address_Equals_DifferentCity_ReturnsFalse()
        {
            // arrange
            var address1 = new Address(new City("Москва"), new Street("Ленина", new City("Москва")), 10);
            var address2 = new Address(new City("Санкт-Петербург"), new Street("Ленина", new City("Санкт-Петербург")), 10);

            // act & assert
            Assert.That(address1, Is.Not.EqualTo(address2));
        }
    }
}
