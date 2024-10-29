// <copyright file="AuthorTests.cs" company="Васильева Марина Алексеевна">
// Copyright (c) Васильева Марина Алексеевна 2024. Library.
// </copyright>

namespace TestDomain
{
    using System;
    using System.Collections.Generic;
    using Domain;

    /// <summary>
    /// Тесты для клсса <see cref="Domain.Author"/>.
    /// </summary>
    public sealed class AuthorTests
    {
        /// <summary>
        /// Тест на конструктор с правильными данными.
        /// </summary>
        [Test]
        public void Ctor_NotNullData_Success()
        {
            // arrange
            var birthDate = new DateOnly(1828, 09, 28);
            var deathDate = new DateOnly(1910, 11, 7);

            // act & assert
            Assert.DoesNotThrow(() => _ = new Author("Толстой", "Лев", "Николаевич", birthDate, deathDate));
        }

        /// <summary>
        /// Тест на конструктор с правильными параметрами.
        /// </summary>
        /// <param name="patronicName"> Отчество.</param>
        [TestCase("Николаевич")]
        [TestCase(null)]
        public void Ctor_ValidPatronicName_DoesNotThrow(string? patronicName)
        {
            var dateBirth = new DateOnly(1828, 09, 28);
            var dateDeath = new DateOnly(1910, 10, 20);
            Assert.DoesNotThrow(() =>
            _ = new Author("Толстой", "Лев", patronicName, dateBirth, dateDeath));
        }

        /// <summary>
        /// Тест на конструктор с неизвестными датами жизни.
        /// </summary>
        /// <param name="dateBirth"> Дата рождения. </param>
        /// <param name="dateDeath"> Дата смерти. </param>
        [TestCaseSource(nameof(ValidDateData))]
        public void Ctor_DateLiveNull_DoesNotThrow(DateOnly? dateBirth, DateOnly? dateDeath)
        {
            Assert.DoesNotThrow(() =>
                _ = new Author("Толстой", "Лев", "Николаевич", dateBirth, dateDeath));
        }

        /// <summary>
        /// Тест на конструктор с <see langword="null"/> значениями.
        /// </summary>
        /// <param name="familyName"> Фамилия.</param>
        /// <param name="firstName"> Имя.</param>
        [TestCase(null, "")]
        [TestCase("", null)]
        public void Ctor_WrongData_ExpectedException(string? familyName, string? firstName)
        {
            var dateBirth = new DateOnly(1828, 09, 28);
            var dateDeath = new DateOnly(1910, 10, 20);
            Assert.Throws<ArgumentNullException>(
                () => _ = new Author(familyName!, firstName!, null, dateBirth, dateDeath));
        }

        /// <summary>
        /// Сравнение двух разных авторов.
        /// </summary>
        [Test]
        public void Equals_ValidData_Success()
        {
            // Arrange
            var author1 = new Author("Толстой", "Лев", "Николаевич", new DateOnly(1828, 09, 28), null);
            var author2 = new Author("Пушкин", "Александр", "Сергеевич", new DateOnly(1799, 06, 06), null);

            // Act & Assert
            Assert.That(author1, Is.Not.EqualTo(author2));
        }

        /// <summary>
        /// Сравнение двух "разных" авторов, т.к. с точки зрения программирования они разные.
        /// </summary>
        [Test]
        public void Equals_SimilarAuthorsDiffernetPatronicName_NotEqual()
        {
            // Arrange
            var author1 = new Author("Толстой", "Лев", "Николаевич", new DateOnly(1828, 09, 28), null);
            var author2 = new Author("Толстой", "Лев", null, new DateOnly(1828, 09, 28), null);

            // Act & Assert
            Assert.That(author1, Is.Not.EqualTo(author2));
        }

        [TestCaseSource(nameof(ValidNullDates))]
        public void Equals_SimilarAuthorsDifferentDates_NotEqual(
            DateOnly? dateBirth1,
            DateOnly? dateBirth2,
            DateOnly? dateDeath1,
            DateOnly? dateDeath2)
        {
            // Arrange
            var author1 = new Author("Толстой", "Лев", "Николаевич", dateBirth1, dateDeath1);
            var author2 = new Author("Толстой", "Лев", "Николаевич", dateBirth2, dateDeath2);

            // Act & Assert
            Assert.That(author1, Is.Not.EqualTo(author2));
        }

        [TestCase("Николаевич", "Толстой Лев Николаевич")]
        [TestCase(null, "Толстой Лев")]
        public void ToString_VallidData_Success(string? patronicName, string expected)
        {
            // arrange
            var author = new Author("Толстой", "Лев", patronicName);

            // act
            var actual = author.ToString();

            // assert
            Assert.That(actual, Is.EqualTo(expected));
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