// <copyright file="ShelfTests.cs" company="Васильева Марина Алексеевна">
// Copyright (c) Васильева Марина Алексеевна 2024. Library.
// </copyright>

namespace DomainTest
{
    using System;
    using Domain;
    using NUnit.Framework;

    /// <summary>
    /// Тесты на полку <see cref="Domain.Shelf"/>.
    /// </summary>
    [TestFixture]
    public sealed class ShelfTests
    {
        [Test]
        public void Ctor_ValidData_Success()
        {
            Assert.DoesNotThrow(() => _ = new Shelf("Полка 1"));
        }

        [Test]
        public void Ctor_NullData_ExpectedException()
        {
            Assert.Throws<ArgumentNullException>(() => _ = new Shelf(null!));
        }

        [TestCase("1", "1", true)]
        [TestCase("1", "2", false)]
        public void Equals_ValidData_Success(string name1, string name2, bool expected)
        {
            // arrange
            var shelf1 = new Shelf(name1);
            var shelf2 = new Shelf(name2);

            // act
            var actual = shelf1.Equals(shelf2);

            // assert
            Assert.That(actual, Is.EqualTo(expected));
        }

        [Test]
        public void ToString_ValidData_Success()
        {
            // arrange
            const string expected = "Название полки Полка 1";
            var shelf = new Shelf("Полка 1");

            // act
            var actual = shelf.ToString();

            // assert
            Assert.That(actual, Is.EqualTo(expected));
        }
    }
}
