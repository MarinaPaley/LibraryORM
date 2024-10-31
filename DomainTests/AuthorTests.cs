// <copyright file="AuthorTests.cs" company="Васильева Марина Алексеевна">
// Copyright (c) Васильева Марина Алексеевна 2024. Library.
// </copyright>

namespace TestDomain
{
    using System;
    using System.Collections.Generic;
    using Domain;
    using NUnit.Framework.Interfaces;

    [TestFixture]
    /// <summary>
    /// Тесты для клсса <see cref="Domain.Author"/>.
    /// </summary>
    public sealed class AuthorTests
    {
        private static readonly Name NameValue = new ("Толстой", "Лев", "Николаевич");
        private static readonly Name NullPatrioicName = new ("Толстой", "Лев");
        private static readonly Name OtherName = new ("Пушкин", "Александр", "Сергеевич");

        /// <summary>
        /// Тест на конструктор с неизвестными датами жизни.
        /// </summary>
        /// <param name="dateBirth"> Дата рождения. </param>
        /// <param name="dateDeath"> Дата смерти. </param>
        [TestCaseSource(nameof(ValidDateData))]
        public void Ctor_DateLiveNull_DoesNotThrow(DateOnly? dateBirth, DateOnly? dateDeath)
        {
            Assert.DoesNotThrow(() =>
                _ = new Author(NameValue, dateBirth, dateDeath));
        }

        [Test]
        public void CtorNullFullName_Expected_Exception()
        {
            Assert.Throws<ArgumentNullException>(() => _ = new Author(null!));
        }

        /// <summary>
        /// Сравнение двух разных авторов.
        /// </summary>
        [Test]
        public void Equals_DifferentAuthors_False()
        {
            // Arrange
            var author1 = new Author(NameValue, new DateOnly(1828, 09, 28), null);
            var author2 = new Author(OtherName, new DateOnly(1799, 06, 06), null);

            // Act & Assert
            Assert.That(author1, Is.Not.EqualTo(author2));
        }

        [Test]
        public void Equals_SameAuthors_True()
        {
            // Arrange
            var author1 = new Author(NameValue);
            var author2 = new Author(OtherName);

            // Act & Assert
            Assert.That(author1, Is.Not.EqualTo(author2));
        }

        /// <summary>
        /// Сравнение двух "разных" авторов, т.к. с точки зрения программирования они разные.
        /// </summary>
        [Test]
        public void Equals_SimilarAuthorsDiffernetPatronicName_False()
        {
            // Arrange
            var author1 = new Author(NameValue);
            var author2 = new Author(NullPatrioicName);

            // Act & Assert
            Assert.That(author1, Is.Not.EqualTo(author2));
        }

        [TestCaseSource(nameof(ValidNullDates))]
        public void Equals_SimilarAuthorsDifferentDates_False(
            DateOnly? dateBirth1,
            DateOnly? dateBirth2,
            DateOnly? dateDeath1,
            DateOnly? dateDeath2)
        {
            // Arrange
            var author1 = new Author(NameValue, dateBirth1, dateDeath1);
            var author2 = new Author(NameValue, dateBirth2, dateDeath2);

            // Act & Assert
            Assert.That(author1, Is.Not.EqualTo(author2));
        }

        [TestCaseSource(nameof(Authors))]
        public void ToString_ValidData_Success(Author author, string expected)
        {
            Assert.That(author.ToString(), Is.EqualTo(expected));
        }

        [TestCaseSource(nameof(Books))]
        public void AddBook_Data_Success(Book? book, bool expected)
        {
            var author = new Author(NameValue);
            Assert.That(expected, Is.EqualTo(author.AddBook(book!)));
        }

        [Test]
        public void RemoveBook_HasBook_True()
        {
            // Arrange
            Name tolstoy = new ("Толстой", "Лев", "Николаевич");
            Author author = new (tolstoy);
            var book = new Book("Анна Каренина", 250, "123", null, author);
            var other = new Book("12 стульев", 250, "123");

            // Act & Assert
            Assert.Multiple(() =>
            {
                Assert.That(author.RemoveBook(book), Is.True);
                Assert.That(author.RemoveBook(null!), Is.False);
                Assert.That(author.RemoveBook(other), Is.False);
            });
        }

        private static IEnumerable<TestCaseData> Books()
        {
            yield return new TestCaseData(new Book("12 стульев", 250, "123"), true);
            yield return new TestCaseData(null, false);
        }

        private static IEnumerable<TestCaseData> Authors()
        {
            yield return new TestCaseData(new Author(NullPatrioicName), "Толстой Лев");
            yield return new TestCaseData(new Author(NameValue), "Толстой Лев Николаевич");
            yield return new TestCaseData(
                new Author(NameValue, new DateOnly(1828, 09, 28)),
                "Толстой Лев Николаевич Год рождения: 28.09.1828");
            yield return new TestCaseData(
                new Author(NameValue, new DateOnly(1828, 09, 28), new DateOnly(1910, 10, 20)),
                "Толстой Лев Николаевич Год рождения: 28.09.1828 Год смерти: 20.10.1910");
        }

        private static IEnumerable<TestCaseData> ValidDateData()
        {
            yield return new TestCaseData(new DateOnly(1828, 09, 28), null);
            yield return new TestCaseData(null, new DateOnly(1910, 10, 20));
            yield return new TestCaseData(null, null);
        }

        private static IEnumerable<TestCaseData> ValidNullDates()
        {
            yield return new TestCaseData(new DateOnly(1828, 09, 28), null, new DateOnly(1910, 10, 20), new DateOnly(1910, 10, 20));
            yield return new TestCaseData(null, new DateOnly(1828, 09, 28), new DateOnly(1910, 10, 20), new DateOnly(1910, 10, 20));
            yield return new TestCaseData(new DateOnly(1828, 09, 28), null, null, null);
            yield return new TestCaseData(null, new DateOnly(1828, 09, 28), null, null);
            yield return new TestCaseData(null, null, null, new DateOnly(1910, 10, 20));
            yield return new TestCaseData(null, null, new DateOnly(1910, 10, 20), null);
            yield return new TestCaseData(new DateOnly(1828, 09, 28), new DateOnly(1828, 09, 28), new DateOnly(1910, 10, 20), null);
            yield return new TestCaseData(new DateOnly(1828, 09, 28), new DateOnly(1828, 09, 28), null, new DateOnly(1910, 10, 20));
        }
    }
}