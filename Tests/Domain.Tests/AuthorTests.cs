// <copyright file="AuthorTests.cs" company="Филипченко Марина Алексеевна">
// Copyright (c) Филипченко Марина Алексеевна 2026. Library.
// </copyright>

namespace Domain.Tests
{
    using System;
    using System.Collections.Generic;
    using Domain;
    using NUnit.Framework;

    /// <summary>
    /// Модульные тесты для класса <see cref="Author"/>.
    /// </summary>
    [TestFixture]
    public sealed class AuthorTests
    {
        private static readonly Name NameValue = new ("Толстой", "Лев", "Николаевич");

        private static readonly Name NullPatronicName = new ("Толстой", "Лев");

        private static readonly Name OtherName = new ("Пушкин", "Александр", "Сергеевич");

        /// <summary>
        /// Тест на конструктор с неизвестными датами жизни.
        /// </summary>
        /// <param name="dateBirth"> Дата рождения. </param>
        /// <param name="dateDeath"> Дата смерти. </param>
        [TestCaseSource(nameof(ValidDateData))]
        public void Ctor_DateLiveNull_DoesNotThrow(DateOnly? dateBirth, DateOnly? dateDeath)
        {
            Assert.DoesNotThrow(() => _ = new Author(new Person(NameValue, dateBirth, dateDeath)));
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
            var author1 = new Author(new Person(NameValue, new DateOnly(1828, 09, 28), null));
            var author2 = new Author(new Person(OtherName, new DateOnly(1799, 06, 06), null));

            // Act & Assert
            Assert.That(author1, Is.Not.EqualTo(author2));
        }

        [Test]
        public void Equals_SameAuthors_True()
        {
            // Arrange
            var author1 = new Author(new Person(NameValue));
            var author2 = new Author(new Person(OtherName));

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
            var author1 = new Author(new Person(NameValue));
            var author2 = new Author(new Person(NullPatronicName));

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
            var author1 = new Author(new Person(NameValue, dateBirth1, dateDeath1));
            var author2 = new Author(new Person(NameValue, dateBirth2, dateDeath2));

            // Act & Assert
            Assert.That(author1, Is.Not.EqualTo(author2));
        }

        [TestCaseSource(nameof(Authors))]
        public void ToString_ValidData_Success(Author author, string expected)
        {
            Assert.That(author.ToString(), Is.EqualTo(expected));
        }

        [TestCaseSource(nameof(Books))]
        public void AddBook_Data_Success(Manuscript? book, bool expected)
        {
            var author = new Author(new Person(NameValue));
            Assert.That(expected, Is.EqualTo(author.AddManuscript(book!)));
        }

        [Test]
        public void RemoveBook_HasBook_True()
        {
            // Arrange
            Name tolstoy = new ("Толстой", "Лев", "Николаевич");
            Author author = new (new Person(tolstoy));
            var language = new Language("Русский");

            Author otherAuthor = new Author(new Person(new Name("Илья", "Ильф")));

            var book = new Manuscript("Анна Каренина", language, new DateOnly(1873, 1, 1), new DateOnly(1877, 1, 1), null,  author);
            var other = new Manuscript("12 стульев", language, new DateOnly(1927, 1, 9), new DateOnly(1927, 1, 12), null, otherAuthor);

            // Act & Assert
            using (Assert.EnterMultipleScope())
            {
                Assert.That(author.RemoveBook(book), Is.True);
                Assert.That(author.RemoveBook(null!), Is.False);
                Assert.That(author.RemoveBook(other), Is.False);
            }
        }

        private static IEnumerable<TestCaseData> Books()
        {
            yield return new TestCaseData(new Manuscript("12 стульев", new Language("Русский"), new DateOnly(1927, 1, 9), new DateOnly(1927, 1, 12), null, new Author(new Person(new Name("Ильф", "Илья")))), true);
            yield return new TestCaseData(null, false);
        }

        private static IEnumerable<TestCaseData> Authors()
        {
            yield return new TestCaseData(
                new Author(new Person(NullPatronicName)),
                "Толстой Лев");

            yield return new TestCaseData(
                new Author(new Person(NameValue)),
                "Толстой Лев Николаевич");

            yield return new TestCaseData(
                new Author(new Person(NameValue, new DateOnly(1828, 09, 28))),
                "Толстой Лев Николаевич Год рождения: 28.09.1828");

            yield return new TestCaseData(
                new Author(new Person(NameValue, new DateOnly(1828, 09, 28), new DateOnly(1910, 10, 20))),
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
