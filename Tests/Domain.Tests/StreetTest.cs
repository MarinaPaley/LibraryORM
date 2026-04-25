// <copyright file="StreetTest.cs" company="Филипченко Марина Алексеевна">
// Copyright (c) Филипченко Марина Алексеевна 2026. Library.
// </copyright>

namespace Domain.Tests
{
    using System;
    using Domain;
    using NUnit.Framework;

    /// <summary>
    /// Модульные тесты для класса <see cref="Street"/>.
    /// </summary>
    [TestFixture]
    public sealed class StreetTest
    {
        private readonly City city = new ("City");

        [Test]
        public void Ctor_ValidData_Success()
        {
            Assert.DoesNotThrow(() => _ = new Street("Street", this.city));
        }

        [TestCase(null)]
        [TestCase("")]
        public void Ctor_BadData_Throws(string? name)
        {
            Assert.Throws<ArgumentNullException>(() => _ = new City(name!));
        }

        [TestCase("City", "City", true, 1)]
        [TestCase("City", "Town", false, 2)]
        public void Equals_Success(string thirst, string second, bool expected, int count)
        {
            // Arrange
            var newCity = new City("city");
            var left = new Street(thirst, newCity);
            var right = new Street(second, newCity);

            // Act
            var actual = left.Equals(right);

            // Assert
            Assert.That(actual, Is.EqualTo(expected));
            Assert.That(newCity.Streets, Has.Count.EqualTo(count));
        }
    }
}
