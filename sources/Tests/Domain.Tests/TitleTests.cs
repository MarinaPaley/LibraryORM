// <copyright file="TitleTests.cs" company="Филипченко Марина Алексеевна">
// Copyright (c) Филипченко Марина Алексеевна 2026. Library.
// </copyright>

namespace Domain.Tests
{
    using System;
    using Domain;
    using NUnit.Framework;

    /// <summary>
    /// Модульные тесты для класса <see cref="Title"/>.
    /// </summary>
    [TestFixture]
    public sealed class TitleTests
    {
        [Test]
        public void Ctor_ValidData_Success()
        {
            Assert.DoesNotThrow(() => _ = new Title("Valid data"));
        }

        [TestCase(null)]
        [TestCase("")]
        public void Ctor_NullOrEmptyData_Throws(string? value)
        {
            Assert.Throws<ArgumentNullException>(() => _ = new Title(value!));
        }

        [TestCase("Same data", "Same data", true)]
        [TestCase("Different data", "Other", false)]
        public void Equals_Success(string left, string right, bool expected)
        {
            // Arrange
            var thirst = new Title(left);
            var second = new Title(right);

            // Act
            var actual = thirst.Equals(second);

            // Assert
            Assert.That(actual, Is.EqualTo(expected));
        }
    }
}
