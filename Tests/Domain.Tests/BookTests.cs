// <copyright file="BookTests.cs" company="Филипченко Марина Алексеевна">
// Copyright (c) Филипченко Марина Алексеевна 2026. Library.
// </copyright>

namespace Domain.Tests
{
    using System;
    using System.Collections.Generic;
    using Domain;
    using NUnit.Framework;

    /// <summary>
    /// Тесты для <see cref="Domain.Book"/>.
    /// </summary>
    [TestFixture]
    internal sealed class BookTests
    {
        [Test]
        public void Ctor_ValidData_Success()
        {
            var language = new Language("Русский");
            var publisher = new Publisher("Издательство");
            var author = new Author(new Person(new Name("Толстой", "Лев")));
            var bookType = new BookType("Книга");
            var manuscript = new Manuscript("Война и мир", language, new HashSet<Author>() { author });

            Assert.DoesNotThrow(() => _ = new Book(null, 1234, "12345", bookType, publisher, 2026, new HashSet<Manuscript>() { manuscript }));
        }

        [Test]
        public void Ctor_NullISBN_ExpectedArgumentNullException()
        {
            var language = new Language("Русский");
            var publisher = new Publisher("Издательство");
            var author = new Author(new Person(new Name("фамилия", "Имя")));
            var bookType = new BookType("Книга");
            var manuscript = new Manuscript("Название", language, new HashSet<Author>() { author });

            Assert.Throws<ArgumentNullException>(() => _ = new Book("Тестовое название", 123, null!, bookType, publisher, 2026, new HashSet<Manuscript>() { manuscript }));
        }

        [TestCase(0)]
        [TestCase(-1)]
        public void Ctor_NegativePages_ExpectedArgumentOutOfRangeException(int pages)
        {
            var language = new Language("Русский");
            var publisher = new Publisher("Издательство");
            var author = new Author(new Person(new Name("фамилия", "Имя")));
            var bookType = new BookType("Книга");
            var manuscript = new Manuscript("Название", language, new HashSet<Author>() { author });

            Assert.Throws<ArgumentOutOfRangeException>(() => _ = new Book("Тестовое название", pages, null!, bookType, publisher, 2026, new HashSet<Manuscript>() { manuscript }));
        }
    }
}
