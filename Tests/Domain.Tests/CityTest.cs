// <copyright file="CityTest.cs" company="Филипченко Марина Алексеевна">
// Copyright (c) Филипченко Марина Алексеевна 2026. Library.
// </copyright>

namespace Domain.Tests
{
    using System;
    using Domain;
    using NUnit.Framework;

    /// <summary>
    /// Модульные тесты для класса <see cref="City"/>.
    /// </summary>
    [TestFixture]
    public sealed class CityTest
    {
        [Test]
        public void Ctor_ValidData_Success()
        {
            Assert.DoesNotThrow(() => _ = new City("City"));
        }

        [TestCase(null)]
        [TestCase("")]
        public void Ctor_BadData_Throws(string? name)
        {
            Assert.Throws<ArgumentNullException>(() => _ = new City(name!));
        }

        [TestCase("City", "City", true)]
        [TestCase("City", "Town", false)]
        public void Equals_Success(string thirst, string second, bool expected)
        {
            // Arrange
            var left = new City(thirst);
            var right = new City(second);

            // Act
            var actual = left.Equals(right);

            // Assert
            Assert.That(actual, Is.EqualTo(expected));
        }
    }
}
