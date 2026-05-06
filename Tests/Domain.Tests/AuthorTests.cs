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

        private static readonly Author Ilf = new Author(new Person(new Name("Ильф", "Илья")));

        private static readonly Author Petrov = new Author(new Person(new Name("Петров", "Евгений")));

        private static readonly Author Tolstoy = new Author(new Person(NameValue));

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
            var author2 = new Author(new Person(NameValue));

            // Act & Assert
            Assert.That(author1, Is.EqualTo(author2));
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
            DateOnly? dateBirth2)
        {
            // Arrange
            var author1 = new Author(new Person(NameValue, dateBirth1));
            var author2 = new Author(new Person(NameValue, dateBirth2));
            var result = author1.Equals(author2);

            // Act & Assert
            Assert.That(result, Is.False);
        }

        [TestCaseSource(nameof(Authors))]
        public void ToString_ValidData_Success(Author author, string expected)
        {
            Assert.That(author.ToString(), Is.EqualTo(expected));
        }

        [TestCaseSource(nameof(Books))]
        public void AddBook_Data_Success(Manuscript? book, bool expected)
        {
            var author = Ilf;
            Assert.That(expected, Is.EqualTo(author.Manuscripts.Contains(book!)));
        }

        private static IEnumerable<TestCaseData> Books()
        {
            yield return new TestCaseData(new Manuscript("12 стульев", new HashSet<Language>() { new ("Русский") }, new DateOnly(1927, 1, 9), new DateOnly(1927, 1, 12), null, Ilf), true);
            yield return new TestCaseData(null, false);
        }

        private static IEnumerable<TestCaseData> Authors()
        {
            yield return new TestCaseData(
                new Author(new Person(NullPatronicName)),
                "Толстой Лев");

            yield return new TestCaseData(Tolstoy, "Толстой Лев Николаевич");

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

        /// <summary>
        /// Сравнение авторов происходит по ФИО и дате рождения.
        /// </summary>
        /// <returns> Набор тестовых данных. </returns>
        private static IEnumerable<TestCaseData> ValidNullDates()
        {
            yield return new TestCaseData(new DateOnly(1828, 09, 28), null);
            yield return new TestCaseData(null, new DateOnly(1828, 09, 28));
        }
    }
}
