// <copyright file="AuthorTests.cs" company="Филипченко Марина Алексеевна">
// Copyright (c) Филипченко Марина Алексеевна 2026. Library.
// </copyright>

namespace Domain.Tests
{
    using System;
    using System.Collections.Generic;
    using Domain;
    using NUnit.Framework;
    using Staff;
    using TestDataProvider;

    /// <summary>
    /// Модульные тесты для класса <see cref="Author"/>.
    /// </summary>
    [TestFixture]
    public sealed class AuthorTests
    {
        /// <summary>
        /// Проверяет, что конструктор <see cref="Author"/> успешно создает объект с различными комбинациями дат жизни.
        /// Имя и фамилия обязательно должны быть заданы, если задаем какие-либо поля у Персоны.
        /// </summary>
        /// <param name="dateBirth"> Дата рождения. </param>
        /// <param name="dateDeath"> Дата смерти. </param>
        [TestCaseSource(nameof(ValidDateData))]
        public void Ctor_WithValidDates_DoesNotThrow(DateOnly? dateBirth, DateOnly? dateDeath)
        {
            // Act & Assert
            Assert.DoesNotThrow(() => _ = TestData.ValidAuthor()
                .WithPerson(
                    TestData.ValidPerson()
                        .WithFirstName("Имя")
                        .WithFamilyName("Фамилия")
                        .WithDateBirth(dateBirth)
                        .WithDateDeath(dateDeath)
                        .Build())
                .Build());
        }

        /// <summary>
        /// Проверяет, что конструктор <see cref="Author"/> выбрасывает исключение при передаче null вместо персоны.
        /// </summary>
        [Test]
        public void Ctor_NullPerson_ThrowsArgumentNullException()
        {
            // Act & Assert
            Assert.Throws<ArgumentNullException>(() => _ = new Author(null!));
        }

        /// <summary>
        /// Проверяет, что два разных автора не равны друг другу.
        /// </summary>
        [Test]
        public void Equals_DifferentAuthors_ReturnsFalse()
        {
            // Arrange
            var author1 = TestData.ValidAuthor()
                .WithPerson(
                    TestData.ValidPerson()
                        .WithFirstName("Имя")
                        .WithFamilyName("Фамилия")
                        .WithDateBirth(new DateOnly(1828, 09, 28))
                        .Build())
                .Build();

            var author2 = TestData.ValidAuthor()
                .WithPerson(
                    TestData.ValidPerson()
                        .WithFirstName("Имя")
                        .WithFamilyName("Фамилия")
                        .WithDateBirth(new DateOnly(1799, 06, 06))
                        .Build())
                .Build();

            // Act & Assert
            Assert.That(author1, Is.Not.EqualTo(author2));
        }

        /// <summary>
        /// Проверяет, что два автора, созданные с дефолтными значениями, равны друг другу.
        /// </summary>
        [Test]
        public void Equals_SameAuthors_ReturnsTrue()
        {
            // Arrange
            var author1 = TestData.ValidAuthor().Build();
            var author2 = TestData.ValidAuthor().Build();

            // Act & Assert
            Assert.That(author1, Is.EqualTo(author2));
        }

        /// <summary>
        /// Проверяет, что авторы с разным отчеством не равны друг другу.
        /// </summary>
        [Test]
        public void Equals_SimilarAuthorsDifferentPatronymicName_ReturnsFalse()
        {
            // Arrange
            var author1 = TestData.ValidAuthor()
                .WithPerson(
                    TestData.ValidPerson()
                        .WithFirstName("Лев")
                        .WithFamilyName("Толстой")
                        .WithPatronymicName("Николаевич")
                        .Build())
                .Build();

            var author2 = TestData.ValidAuthor()
                .WithPerson(
                    TestData.ValidPerson()
                        .WithFirstName("Лев")
                        .WithFamilyName("Толстой")
                        .Build())
                .Build();

            // Act & Assert
            Assert.That(author1, Is.Not.EqualTo(author2));
        }

        /// <summary>
        /// Проверяет, что авторы с разными датами рождения не равны друг другу.
        /// </summary>
        /// <param name="dateBirth1"> Дата рождения первого автора. </param>
        /// <param name="dateBirth2"> Дата рождения второго автора. </param>
        [TestCaseSource(nameof(ValidNullDates))]
        public void Equals_SimilarAuthorsDifferentDates_ReturnsFalse(
            DateOnly? dateBirth1,
            DateOnly? dateBirth2)
        {
            // Arrange
            var author1 = TestData.ValidAuthor()
                .WithPerson(
                    TestData.ValidPerson()
                        .WithFirstName("Имя")
                        .WithFamilyName("Фамилия")
                        .WithDateBirth(dateBirth1)
                        .Build())
                .Build();

            var author2 = TestData.ValidAuthor()
                .WithPerson(
                    TestData.ValidPerson()
                        .WithFirstName("Имя")
                        .WithFamilyName("Фамилия")
                        .WithDateBirth(dateBirth2)
                        .Build())
                .Build();

            // Act
            var result = author1.Equals(author2);

            // Assert
            Assert.That(result, Is.False);
        }

        /// <summary>
        /// Проверяет корректность строкового представления автора.
        /// </summary>
        /// <param name="author"> Тестируемый автор. </param>
        /// <param name="expected"> Ожидаемая строка. </param>
        [TestCaseSource(nameof(Authors))]
        public void ToString_ValidData_ReturnsExpectedString(Author author, string expected)
        {
            // Act & Assert
            Assert.That(author.ToString(), Is.EqualTo(expected));
        }

        /// <summary>
        /// Проверяет, что при создании рукописи с автором происходит двусторонняя связь,
        /// либо что при null-названии рукописи связь не устанавливается.
        /// </summary>
        /// <param name="manuscriptTitle"> Название рукописи (может быть null). </param>
        /// <param name="expected"> Ожидаемый результат наличия рукописи в коллекции автора. </param>
        [TestCaseSource(nameof(ManuscriptsData))]
        public void CreateManuscript_WithAuthor_ShouldEstablishBidirectionalLink(
            string? manuscriptTitle,
            bool expected)
        {
            // Arrange
            var author = TestData.ValidAuthor()
                .WithPerson(
                    TestData.ValidPerson()
                        .WithFamilyName("Ильф")
                        .WithFirstName("Илья"))
                .Build();

            var language = TestData.ValidLanguage().WithName("Русский").Build();

            Manuscript? manuscript = null;
            if (manuscriptTitle is not null)
            {
                manuscript = TestData.ValidManuscript()
                    .WithName(manuscriptTitle)
                    .WithLanguages(new HashSet<Language> { language, })
                    .WithDate(new Range<DateOnly>(
                        new DateOnly(1927, 1, 9),
                        new DateOnly(1927, 1, 12)))
                    .WithAuthors(new HashSet<Author> { author, })
                    .Build();
            }

            // Act & Assert
            Assert.That(author.Manuscripts.Contains(manuscript!), Is.EqualTo(expected));
        }

        /// <summary>
        /// Предоставляет тестовые данные для проверки двусторонней связи с рукописью.
        /// </summary>
        /// <returns> Коллекция тестовых данных. </returns>
        private static IEnumerable<TestCaseData> ManuscriptsData()
        {
            yield return new TestCaseData("12 стульев", true);
            yield return new TestCaseData(null, false);
        }

        /// <summary>
        /// Предоставляет тестовые данные для проверки метода ToString.
        /// </summary>
        /// <returns> Коллекция тестовых данных. </returns>
        private static IEnumerable<TestCaseData> Authors()
        {
            yield return new TestCaseData(
                TestData.ValidAuthor()
                    .WithPerson(
                        TestData.ValidPerson()
                            .WithFirstName("Лев")
                            .WithFamilyName("Толстой")
                            .Build())
                    .Build(),
                "Толстой Лев");

            yield return new TestCaseData(
                TestData.ValidAuthor()
                    .WithPerson(
                        TestData.ValidPerson()
                            .WithFirstName("Лев")
                            .WithFamilyName("Толстой")
                            .WithPatronymicName("Николаевич")
                            .Build())
                    .Build(),
                "Толстой Лев Николаевич");

            yield return new TestCaseData(
                TestData.ValidAuthor()
                    .WithPerson(
                        TestData.ValidPerson()
                            .WithFirstName("Лев")
                            .WithFamilyName("Толстой")
                            .WithPatronymicName("Николаевич")
                            .WithDateBirth(new DateOnly(1828, 09, 28))
                            .Build())
                    .Build(),
                "Толстой Лев Николаевич Год рождения: 28.09.1828");

            yield return new TestCaseData(
                TestData.ValidAuthor()
                    .WithPerson(
                        TestData.ValidPerson()
                            .WithFirstName("Лев")
                            .WithFamilyName("Толстой")
                            .WithPatronymicName("Николаевич")
                            .WithDateBirth(new DateOnly(1828, 09, 28))
                            .WithDateDeath(new DateOnly(1910, 10, 20))
                            .Build())
                    .Build(),
                "Толстой Лев Николаевич Год рождения: 28.09.1828 Год смерти: 20.10.1910");
        }

        /// <summary>
        /// Предоставляет валидные комбинации дат для проверки конструктора.
        /// </summary>
        /// <returns> Коллекция тестовых данных. </returns>
        private static IEnumerable<TestCaseData> ValidDateData()
        {
            yield return new TestCaseData(new DateOnly(1828, 09, 28), null);
            yield return new TestCaseData(null, new DateOnly(1910, 10, 20));
            yield return new TestCaseData(null, null);
        }

        /// <summary>
        /// Предоставляет тестовые данные для проверки неравенства авторов с разными датами.
        /// </summary>
        /// <returns> Коллекция тестовых данных. </returns>
        private static IEnumerable<TestCaseData> ValidNullDates()
        {
            yield return new TestCaseData(new DateOnly(1828, 09, 28), null);
            yield return new TestCaseData(null, new DateOnly(1828, 09, 28));
        }
    }
}