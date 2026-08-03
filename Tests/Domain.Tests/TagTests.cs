// <copyright file="TagTests.cs" company="Филипченко Марина Алексеевна">
// Copyright (c) Филипченко Марина Алексеевна 2026. Library.
// </copyright>

namespace Domain.Tests
{
    using System;
    using Domain;
    using NUnit.Framework;

    [TestFixture]
    public sealed class TagTests
    {
        private static readonly ColorCode ColorCode = new ColorCode("#FF0000");
        private static readonly Color Color = new Color("Red", ColorCode);
        private static readonly Category Category = new Category("Category", Color);

        [Test]
        public void Ctor_ValidData_Success()
        {
            Assert.DoesNotThrow(() => _ = new Tag("Tag", Category));
        }

        [TestCase(null)]
        [TestCase("")]
        public void Ctor_BadData_Throws(string? name)
        {
            Assert.Throws<ArgumentNullException>(() => _ = new Tag(name!, Category));
        }

        [TestCase("Tag", "New", false)]
        public void Equals_Success(string thirst, string second, bool expected)
        {
            // Arrange
            var colorCode = new ColorCode("#FF0000");
            var color = new Color("Red", colorCode);
            var category = new Category("Category", color);
            var left = new Tag(thirst, category);
            var right = new Tag(second, category);

            // Act
            var actual = left.Equals(right);

            // Assert
            Assert.That(actual, Is.EqualTo(expected));
        }
    }
}
