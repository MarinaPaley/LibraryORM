// <copyright file="StreetTests.cs" company="Филипченко Марина Алексеевна">
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
    public sealed class StreetTests
    {
        private readonly City city = new ("City");

        [Test]
        public void Ctor_ValidData_Success()
        {
            // Arrange, Act & Assert
            Assert.DoesNotThrow(() => _ = new Street("Street", this.city));
        }

        [TestCase(null)]
        [TestCase("")]
        public void Ctor_BadData_Throws(string? name)
        {
            // Arrange, Act & Assert
            Assert.Throws<ArgumentNullException>(() => _ = new City(name!));
        }

        [TestCase("Street1", "Street2", false, 2)]
        public void Equals_Success(string first, string second, bool expected, int count)
        {
            // Arrange
            var newCity = new City("City"); // Нужно так, чтобы не влияли другие тесты
            var left = new Street(first, newCity);
            var right = new Street(second, newCity);

            // Act
            var actual = left.Equals(right);

            // Assert
            using (Assert.EnterMultipleScope())
            {
                Assert.That(actual, Is.EqualTo(expected));
                Assert.That(newCity.Streets, Has.Count.EqualTo(count));
            }
        }
    }
}
