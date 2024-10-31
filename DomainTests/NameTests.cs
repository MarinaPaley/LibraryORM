// <copyright file="NameTests.cs" company="Васильева Марина Алексеевна">
// Copyright (c) Васильева Марина Алексеевна 2024. Library.
// </copyright>

namespace TestDomain
{
    using System;
    using System.Collections.Generic;
    using System.Xml.Linq;
    using Domain;

    [TestFixture]
    /// <summary>
    /// Тесты для клсса <see cref="Domain.Name"/>.
    /// </summary>
    public sealed class NameTests
    {
        /// <summary>
        /// Тест на конструктор с правильными данными.
        /// </summary>
        [Test]
        public void Ctor_NotNullData_Success()
        {
            Assert.DoesNotThrow(() => _ = new Name("Толстой", "Лев", "Николаевич"));
        }

        /// <summary>
        /// Тест на конструктор с правильными параметрами.
        /// </summary>
        /// <param name="patronicName"> Отчество.</param>
        [TestCase("Николаевич")]
        [TestCase(null)]
        public void Ctor_ValidPatronicName_DoesNotThrow(string? patronicName)
        {
            Assert.DoesNotThrow(() =>
            _ = new Name("Толстой", "Лев", patronicName));
        }

        /// <summary>
        /// Тест на конструктор с <see langword="null"/> значениями.
        /// </summary>
        /// <param name="familyName"> Фамилия.</param>
        /// <param name="firstName"> Имя.</param>
        [TestCase(null, "")]
        [TestCase("", null)]
        public void Ctor_WrongData_ExpectedException(string? familyName, string? firstName) =>
            Assert.Throws<ArgumentNullException>(
                  () => _ = new Name(familyName!, firstName!));

        [Test]
        public void Equals_SameNames_True()
        {
            // Arrange
            var name1 = new Name("Толстой", "Лев", "Николаевич");
            var name2 = new Name("Пушкин", "Александр", "Сергеевич");

            // Act & Assert
            Assert.That(name1, Is.Not.EqualTo(name2));
        }

        /// <summary>
        /// Сравнение двух "разных" имен, т.к. с точки зрения программирования они разные.
        /// </summary>
        [Test]
        public void Equals_SimilarAuthorsDiffernetPatronicName_False()
        {
            // Arrange
            var name1 = new Name("Толстой", "Лев", "Николаевич");
            var name2 = new Name("Толстой", "Лев", null);

            // Act & Assert
            Assert.That(name1, Is.Not.EqualTo(name2));
        }

        [TestCase("Николаевич", "Толстой Лев Николаевич")]
        [TestCase(null, "Толстой Лев")]
        public void ToString_VallidData_Success(string? patronicName, string expected)
        {
            // arrange
            var author = new Name("Толстой", "Лев", patronicName);

            // act
            var actual = author.ToString();

            // assert
            Assert.That(actual, Is.EqualTo(expected));
        }

        [TestCaseSource(nameof(ValidNames))]
        public void EqualsOperator_ValidData_Success(Name? name1, Name? name2, bool expected) =>
            Assert.That(name1 == name2, Is.EqualTo(expected));

        [TestCaseSource(nameof(ValidNames))]
        public void NotEqualsOperator_ValidData_Success(Name? name1, Name? name2, bool expected) =>
            Assert.That(name1 != name2, !Is.EqualTo(expected));

        private static IEnumerable<TestCaseData> ValidNames()
        {
            yield return new TestCaseData(new Name("Толстой", "Лев", "Николаевич"), new Name("Толстой", "Лев", "Николаевич"), true);
            yield return new TestCaseData(new Name("Толстой", "Лев", "Николаевич"), new Name("Пушкин", "Александр", "Сергеевич"), false);
            yield return new TestCaseData(new Name("Толстой", "Лев", "Николаевич"), null, false);
            yield return new TestCaseData(null, new Name("Пушкин", "Александр", "Сергеевич"), false);
        }
    }
}
