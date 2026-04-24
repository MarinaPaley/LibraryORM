// <copyright file="ShelfTests.cs" company="Филипченко Марина Алексеевна">
// Copyright (c) Филипченко Марина Алексеевна 2026. Library.
// </copyright>

namespace Domain.Tests
{
    using System;
    using System.Collections.Generic;
    using Domain;
    using NUnit.Framework;

    /// <summary>
    /// Модульные тесты для класса <see cref="Shelf"/>.
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
            // Arrange
            var shelf1 = new Shelf(name1);
            var shelf2 = new Shelf(name2);

            // Act
            var actual = shelf1.Equals(shelf2);

            // Assert
            Assert.That(actual, Is.EqualTo(expected));
        }

        [Test]
        public void ToString_NoBook_Success()
        {
            // Arrange
            const string expected = "Название полки: Полка 1";
            var shelf = new Shelf("Полка 1");

            // Act
            var actual = shelf.ToString();

            // Assert
            Assert.That(actual, Is.EqualTo(expected));
        }

        [Test]
        public void ToString_WithBooksNoAuthors_Success()
        {
            // Arrange
            const string expected = "Название полки: Полка 1 Книги: Анна Каренина, 12 стульев";
            var shelf = new Shelf("Полка 1");
            var language = new Language("Русский");
            var publisher = new Publisher("Издательство");
            var author = new Author(new Person(new Name("Толстой", "Лев")));
            var bookType = new BookType("Книга");
            var manuscript1 = new Manuscript("Анна Каренина", language, new HashSet<Author>());
            var manuscript2 = new Manuscript("12 стульев", language, new HashSet<Author>());

            var book = new Book(null, 1234, "12345", bookType, publisher, 2026, new HashSet<Manuscript>() { manuscript1 });
            var other = new Book(null, 1234, "12345", bookType, publisher, 2026, new HashSet<Manuscript>() { manuscript2 });

            _ = shelf.AddBook(book);
            _ = shelf.AddBook(other);

            // Act
            var actual = shelf.ToString();

            // Assert
            Assert.That(actual, Is.EqualTo(expected));
        }

        [Test]
        public void ToString_WithBooks_Success()
        {
            // Arrange
            const string expected = "Название полки: Полка 1 Книги: Анна Каренина Толстой Лев Николаевич, " +
                "12 стульев Ильф Илья, Петров Евгений";

            var shelf = new Shelf("Полка 1");
            Person tolstoy = new (new Name("Толстой", "Лев", "Николаевич"));
            Author author1 = new (tolstoy);
            Person ilf = new (new Name ("Ильф", "Илья"));
            Person petrov = new (new Name("Петров", "Евгений"));
            Author author2 = new (ilf);
            Author author3 = new (petrov);
            var language = new Language("Русский");
            var publisher = new Publisher("Издательство");
            var bookType = new BookType("Книга");

            var manuscript1 = new Manuscript("Анна Каренина", language, new HashSet<Author>() { author1 });
            var manuscript2 = new Manuscript("12 стульев", language, new HashSet<Author>() { author2, author3 });

            _ = new Book(null, 250, "123", bookType, publisher, 1925, new HashSet<Manuscript>() { manuscript1 }, shelf);
            _ = new Book(null, 250, "123", bookType, publisher, 1925, new HashSet<Manuscript>() { manuscript2 }, shelf);

            // Act
            var actual = shelf.ToString();

            // Assert
            Assert.That(actual, Is.EqualTo(expected));
        }

        [TestCaseSource(nameof(Books))]
        public void AddBook_Book_Success(Book? book, bool expected)
        {
            // Arrange
            var shelf = new Shelf("Полка 1");

            // Act & Assert
            Assert.That(shelf.AddBook(book!), Is.EqualTo(expected));
        }

        [Test]
        public void RemoveBook_ValidData_Success()
        {
            // Arrange
            var shelf = new Shelf("Полка 1");
            var language = new Language("Русский");
            var publisher = new Publisher("Издательство");
            var author = new Author(new Person(new Name("Толстой", "Лев")));
            var bookType = new BookType("Книга");
            var manuscript1 = new Manuscript("Анна Каренина", language, new HashSet<Author>() { author });
            var manuscript2 = new Manuscript("12 стульев", language, new HashSet<Author>() { author });

            var book = new Book(null, 1234, "12345", bookType, publisher, 2026, new HashSet<Manuscript>() { manuscript1 }, shelf);

            var other = new Book(null, 1234, "12345", bookType, publisher, 2026, new HashSet<Manuscript>() { manuscript2 });

            // Act & Assert
            using (Assert.EnterMultipleScope())
            {
                Assert.That(shelf.RemoveBook(book), Is.True);
                Assert.That(shelf.RemoveBook(null!), Is.False);
                Assert.That(shelf.RemoveBook(other), Is.False);
            }
        }

        private static IEnumerable<TestCaseData> Books()
        {
            yield return new TestCaseData(
                new Book(null, 1234, "12345", new BookType("Книга"), 
                new Publisher("Издательство"),
                2026, 
                new HashSet<Manuscript>() { new Manuscript("Анна Каренина", new Language("Русский"), new HashSet<Author>() { new Author(new Person(new Name("Толстой", "Лев"))) } )}), true);
            yield return new TestCaseData(null, false);
        }
    }
}
