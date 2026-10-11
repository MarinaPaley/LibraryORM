// <copyright file="CategoryTest.cs" company="Филипченко Марина Алексеевна">
// Copyright (c) Филипченко Марина Алексеевна 2026. Library.
// </copyright>

namespace Domain.Tests
{
    using System;
    using Domain;
    using NUnit.Framework;

    [TestFixture]
    public sealed class CategoryTest
    {
        private static readonly ColorCode ColorCode = new ColorCode("#FF0000");
        private static readonly Color Color = new Color("Red", ColorCode);

        [Test]
        public void Ctor_ValidData_Success()
        {
            Assert.DoesNotThrow(() => _ = new Category("Category", Color));
        }

        [TestCase(null)]
        [TestCase("")]
        public void Ctor_BadData_Throws(string? name)
        {
            Assert.Throws<ArgumentNullException>(() => _ = new Category(name!, Color));
        }

        [TestCase("Category", "Category", true)]
        [TestCase("Category", "New", false)]
        public void Equals_Success(string thirst, string second, bool expected)
        {
            // Arrange
            var left = new Category(thirst, Color);
            var right = new Category(second, Color);

            // Act
            var actual = left.Equals(right);

            // Assert
            Assert.That(actual, Is.EqualTo(expected));
        }
    }
}
