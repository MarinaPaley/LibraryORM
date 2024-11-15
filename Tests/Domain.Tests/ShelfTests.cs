// <copyright file="ShelfTests.cs" company="Васильева Марина Алексеевна">
// Copyright (c) Васильева Марина Алексеевна 2024. Library.
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
            var book = new Book("Анна Каренина", 250, "123", shelf);
            var other = new Book("12 стульев", 250, "123", shelf);

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
            Name tolstoy = new ("Толстой", "Лев", "Николаевич");
            Author author1 = new (tolstoy);
            Name ilf = new ("Ильф", "Илья");
            Name petrov = new ("Петров", "Евгений");
            Author author2 = new (ilf);
            Author author3 = new (petrov);
            _ = new Book("Анна Каренина", 250, "123", shelf, author1);
            _ = new Book("12 стульев", 250, "123", shelf, author2, author3);

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
            var book = new Book("Анна Каренина", 250, "123", shelf);
            var other = new Book("12 стульев", 250, "123");

            // Act & Assert
            Assert.Multiple(() =>
            {
                Assert.That(shelf.RemoveBook(book), Is.True);
                Assert.That(shelf.RemoveBook(null!), Is.False);
                Assert.That(shelf.RemoveBook(other), Is.False);
            });
        }

        private static IEnumerable<TestCaseData> Books()
        {
            yield return new TestCaseData(new Book("12 стульев", 250, "123"), true);
            yield return new TestCaseData(null, false);
        }
    }
}
