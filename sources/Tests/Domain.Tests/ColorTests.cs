// <copyright file="ColorTests.cs" company="Филипченко Марина Алексеевна">
// Copyright (c) Филипченко Марина Алексеевна 2026. Library.
// </copyright>

namespace Domain.Tests
{
    using System;
    using Domain;
    using NUnit.Framework;

    /// <summary>
    /// Модульные тесты для класса <see cref="Color"/>.
    /// </summary>
    [TestFixture]
    public sealed class ColorTests
    {
        private static readonly ColorCode ColorCode = new ColorCode("#FF0000");

        [Test]
        public void Ctor_ValidData_Success()
        {
            Assert.DoesNotThrow(() => _ = new Color("Color", ColorCode));
        }

        [TestCase(null)]
        [TestCase("")]
        public void Ctor_BadData_Throws(string? name)
        {
            Assert.Throws<ArgumentNullException>(() => _ = new Color(name!, ColorCode));
        }

        [TestCase("Red", "Red", true)]
        [TestCase("Green", "Red", false)]
        public void Equals_Success(string thirst, string second, bool expected)
        {
            // Arrange
            var left = new Color(thirst, ColorCode);
            var right = new Color(second, ColorCode);

            // Act
            var actual = left.Equals(right);

            // Assert
            Assert.That(actual, Is.EqualTo(expected));
        }
    }
}
