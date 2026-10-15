// <copyright file="NameTests.cs" company="Филипченко Марина Алексеевна">
// Copyright (c) Филипченко Марина Алексеевна 2026. Library.
// </copyright>

namespace Domain.Tests
{
    using System;
    using System.Collections.Generic;
    using Domain;
    using NUnit.Framework;

    /// <summary>
    /// Модульные тесты для класса <see cref="Name"/>.
    /// </summary>
    [TestFixture]
    public sealed class NameTests
    {
        /// <summary>
        /// Проверяет, что конструктор <see cref="Name"/> успешно создает объект с валидными данными.
        /// </summary>
        [Test]
        public void Ctor_NotNullData_Success()
        {
            // Arrange, Act & Assert
            Assert.DoesNotThrow(() => _ = new Name("Толстой", "Лев", "Николаевич"));
        }

        /// <summary>
        /// Проверяет, что конструктор <see cref="Name"/> допускает передачу null или валидного отчества.
        /// </summary>
        /// <param name="patronymicName"> Отчество (может быть null). </param>
        [TestCase("Николаевич")]
        [TestCase(null)]
        public void Ctor_ValidPatronymicName_DoesNotThrow(string? patronymicName)
        {
            // Arrange, Act & Assert
            Assert.DoesNotThrow(() => _ = new Name("Толстой", "Лев", patronymicName));
        }

        /// <summary>
        /// Проверяет, что конструктор <see cref="Name"/> выбрасывает исключение при невалидных имени или фамилии.
        /// </summary>
        /// <param name="familyName"> Фамилия. </param>
        /// <param name="firstName"> Имя. </param>
        [TestCase(null, "")]
        [TestCase("", null)]
        [TestCase(" Антон ", "")]
        [TestCase("", " ")]
        public void Ctor_WrongData_ExpectedException(string? familyName, string? firstName)
        {
            // Arrange, Act & Assert
            Assert.Throws<ArgumentNullException>(() => _ = new Name(familyName!, firstName!));
        }

        /// <summary>
        /// Проверяет, что два разных имени не равны друг другу.
        /// </summary>
        [Test]
        public void Equals_DifferentNames_ReturnsFalse()
        {
            // Arrange
            var name1 = new Name("Толстой", "Лев", "Николаевич");
            var name2 = new Name("Пушкин", "Александр", "Сергеевич");

            // Act & Assert
            Assert.That(name1, Is.Not.EqualTo(name2));
        }

        /// <summary>
        /// Проверяет, что имена с разным отчеством не равны друг другу.
        /// </summary>
        [Test]
        public void Equals_DifferentPatronymicName_ReturnsFalse()
        {
            // Arrange
            var name1 = new Name("Толстой", "Лев", "Николаевич");
            var name2 = new Name("Толстой", "Лев", null);

            // Act & Assert
            Assert.That(name1, Is.Not.EqualTo(name2));
        }

        /// <summary>
        /// Проверяет корректность строкового представления имени.
        /// </summary>
        /// <param name="patronymicName"> Отчество (может быть null). </param>
        /// <param name="expected"> Ожидаемая строка. </param>
        [TestCase("Николаевич", "Толстой Лев Николаевич")]
        [TestCase(null, "Толстой Лев")]
        public void ToString_ValidData_Success(string? patronymicName, string expected)
        {
            // Arrange
            var name = new Name("Толстой", "Лев", patronymicName);

            // Act
            var actual = name.ToString();

            // Assert
            Assert.That(actual, Is.EqualTo(expected));
        }

        /// <summary>
        /// Проверяет корректность работы оператора равенства (==) для различных комбинаций имен.
        /// </summary>
        /// <param name="name1"> Первое имя. </param>
        /// <param name="name2"> Второе имя. </param>
        /// <returns> Результат работы оператора ==. </returns>
        [TestCaseSource(nameof(ValidNamesForEquals))]
        public bool EqualsOperator_ValidData_Success(Name? name1, Name? name2) => name1 == name2;

        /// <summary>
        /// Проверяет корректность работы оператора неравенства (!=) для различных комбинаций имен.
        /// </summary>
        /// <param name="name1"> Первое имя. </param>
        /// <param name="name2"> Второе имя. </param>
        /// <returns> Результат работы оператора !=. </returns>
        [TestCaseSource(nameof(ValidNamesForNotEquals))]
        public bool NotEqualsOperator_ValidData_Success(Name? name1, Name? name2) => name1 != name2;

        /// <summary>
        /// Предоставляет тестовые данные для проверки оператора равенства (==).
        /// </summary>
        /// <returns> Коллекция тестовых данных с ожидаемыми результатами. </returns>
        private static IEnumerable<TestCaseData> ValidNamesForEquals()
        {
            yield return new TestCaseData(
                    new Name("Толстой", "Лев", "Николаевич"),
                    new Name("Толстой", "Лев", "Николаевич"))
                .Returns(true);

            yield return new TestCaseData(
                    new Name("Толстой", "Лев", "Николаевич"),
                    new Name("Пушкин", "Александр", "Сергеевич"))
                .Returns(false);

            yield return new TestCaseData(
                    new Name("Толстой", "Лев", "Николаевич"),
                    null)
                .Returns(false);

            yield return new TestCaseData(
                    null,
                    new Name("Пушкин", "Александр", "Сергеевич"))
                .Returns(false);
        }

        /// <summary>
        /// Предоставляет тестовые данные для проверки оператора неравенства (!=).
        /// </summary>
        /// <returns> Коллекция тестовых данных с ожидаемыми результатами. </returns>
        private static IEnumerable<TestCaseData> ValidNamesForNotEquals()
        {
            yield return new TestCaseData(
                    new Name("Толстой", "Лев", "Николаевич"),
                    new Name("Толстой", "Лев", "Николаевич"))
                .Returns(false); // Равны, поэтому != вернет false

            yield return new TestCaseData(
                    new Name("Толстой", "Лев", "Николаевич"),
                    new Name("Пушкин", "Александр", "Сергеевич"))
                .Returns(true); // Разные, поэтому != вернет true

            yield return new TestCaseData(
                    new Name("Толстой", "Лев", "Николаевич"),
                    null)
                .Returns(true);

            yield return new TestCaseData(
                    null,
                    new Name("Пушкин", "Александр", "Сергеевич"))
                .Returns(true);
        }
    }
}