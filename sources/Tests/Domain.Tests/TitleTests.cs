// <copyright file="TitleTests.cs" company="Филипченко Марина Алексеевна">
// Copyright (c) Филипченко Марина Алексеевна 2026. Library.
// </copyright>

namespace Domain.Tests
{
    using System;
    using Domain;
    using NUnit.Framework;

    /// <summary>
    /// Модульные тесты для класса <see cref="Title"/>.
    /// </summary>
    [TestFixture]
    public sealed class TitleTests
    {
        /// <summary>
        /// Проверяет, что конструктор <see cref="Title"/> успешно создает объект с валидными данными.
        /// </summary>
        [Test]
        public void Ctor_ValidData_Success()
        {
            // Arrange, Act & Assert
            Assert.DoesNotThrow(() => _ = new Title("Valid data"));
        }

        /// <summary>
        /// Проверяет, что конструктор <see cref="Title"/> выбрасывает исключение при передаче null или пустой строки.
        /// </summary>
        /// <param name="value"> Некорректное значение (null или пустая строка). </param>
        [TestCase(null)]
        [TestCase("")]
        public void Ctor_NullOrEmptyData_Throws(string? value)
        {
            // Arrange, Act & Assert
            Assert.Throws<ArgumentNullException>(() => _ = new Title(value!));
        }

        /// <summary>
        /// Проверяет логику равенства двух экземпляров <see cref="Title"/>.
        /// </summary>
        /// <param name="leftValue"> Значение первого экземпляра. </param>
        /// <param name="rightValue"> Значение второго экземпляра. </param>
        /// <param name="expected"> Ожидаемый результат сравнения. </param>
        [TestCase("Same data", "Same data", true)]
        [TestCase("Different data", "Other", false)]
        public void Equals_Success(string leftValue, string rightValue, bool expected)
        {
            // Arrange
            var leftTitle = new Title(leftValue);
            var rightTitle = new Title(rightValue);

            // Act
            var actual = leftTitle.Equals(rightTitle);

            // Assert
            Assert.That(actual, Is.EqualTo(expected));
        }
    }
}