// <copyright file="ColorTests.cs" company="Филипченко Марина Алексеевна">
// Copyright (c) Филипченко Марина Алексеевна 2026. Library.
// </copyright>

namespace Domain.Tests
{
    using System;
    using Domain;
    using NUnit.Framework;
    using TestDataProvider;

    /// <summary>
    /// Модульные тесты для класса <see cref="Color"/>.
    /// </summary>
    [TestFixture]
    public sealed class ColorTests
    {
        private static readonly ColorCode DefaultColorCode = new ColorCode("#FF0000");

        /// <summary>
        /// Проверяет, что конструктор <see cref="Color"/> успешно создает объект с валидными данными.
        /// </summary>
        [Test]
        public void Ctor_ValidData_Success()
        {
            // Arrange, Act & Assert
            Assert.DoesNotThrow(() => _ = TestData.ValidColor()
                .WithName("Color")
                .WithCode(DefaultColorCode)
                .Build());
        }

        /// <summary>
        /// Проверяет, что конструктор <see cref="Color"/> выбрасывает исключение при невалидном имени.
        /// </summary>
        /// <param name="name"> Некорректное имя цвета (null или пустая строка). </param>
        [TestCase(null)]
        [TestCase("")]
        public void Ctor_BadData_Throws(string? name)
        {
            // Arrange, Act & Assert
            // Для проверки guard clauses конструктора допустимо использовать прямой вызов new Color(name!, ...)
            Assert.Throws<ArgumentNullException>(() => _ = new Color(name!, DefaultColorCode));
        }

        /// <summary>
        /// Проверяет логику равенства двух экземпляров <see cref="Color"/>.
        /// </summary>
        /// <param name="first"> Название первого цвета. </param>
        /// <param name="second"> Название второго цвета. </param>
        /// <param name="expected"> Ожидаемый результат сравнения. </param>
        [TestCase("Red", "Red", true)]
        [TestCase("Green", "Red", false)]
        public void Equals_Success(string first, string second, bool expected)
        {
            // Arrange
            var left = TestData.ValidColor()
                .WithName(first)
                .WithCode(DefaultColorCode)
                .Build();

            var right = TestData.ValidColor()
                .WithName(second)
                .WithCode(DefaultColorCode)
                .Build();

            // Act
            var actual = left.Equals(right);

            // Assert
            Assert.That(actual, Is.EqualTo(expected));
        }
    }
}