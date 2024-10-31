// <copyright file="BookTests.cs" company="Васильева Марина Алексеевна">
// Copyright (c) Васильева Марина Алексеевна 2024. Library.
// </copyright>

namespace DomainTest
{
    using System;
    using Domain;
    using NUnit.Framework;

    /// <summary>
    /// Тесты для класса <see cref="Domain.Book"/>.
    /// </summary>
    [TestFixture]
    public sealed class BookTests
    {
        [Test]
        public void Ctor_NullTitle_ExpectedArgumentNullException()
        {
            Assert.Throws<ArgumentNullException>(
                () => _ = new Book(null!, 100, "1"));
        }

        [Test]
        public void Ctor_NullISBN_ExpectedArgumentNullException()
        {
            Assert.Throws<ArgumentNullException>(
                () => _ = new Book("Тестовое название", 100, null!));
        }

        [TestCase(0)]
        [TestCase(-1)]
        public void Ctor_NegativePages_ExpectedArgumentOutOfRangeException(int pages)
        {
            Assert.Throws<ArgumentOutOfRangeException>(
                () => _ = new Book("Тестовое название", pages, "1"));
        }
    }
}