// <copyright file="CityTests.cs" company="Филипченко Марина Алексеевна">
// Copyright (c) Филипченко Марина Алексеевна 2026. Library.
// </copyright>

namespace Domain.Tests
{
    using System;
    using Domain;
    using NUnit.Framework;
    using TestDataProvider;

    /// <summary>
    /// Модульные тесты для класса <see cref="City"/>.
    /// </summary>
    [TestFixture]
    public sealed class CityTests
    {
        /// <summary>
        /// Проверяет, что конструктор <see cref="City"/> успешно создает объект с валидными данными.
        /// </summary>
        [Test]
        public void Ctor_ValidData_Success()
        {
            // Arrange, Act & Assert
            Assert.DoesNotThrow(() => _ = TestData.ValidCity().WithName("City").Build());
        }

        /// <summary>
        /// Проверяет, что конструктор <see cref="City"/> выбрасывает исключение при невалидном имени.
        /// </summary>
        /// <param name="name"> Некорректное имя города (null или пустая строка). </param>
        [TestCase(null)]
        [TestCase("")]
        public void Ctor_BadData_Throws(string? name)
        {
            // Arrange, Act & Assert
            // Для проверки guard clauses конструктора допустимо использовать прямой вызов new City(name!)
            Assert.Throws<ArgumentNullException>(() => _ = new City(name!));
        }

        /// <summary>
        /// Проверяет логику равенства двух экземпляров <see cref="City"/>.
        /// </summary>
        /// <param name="first"> Название первого города. </param>
        /// <param name="second"> Название второго города. </param>
        /// <param name="expected"> Ожидаемый результат сравнения. </param>
        [TestCase("City", "City", true)]
        [TestCase("City", "Town", false)]
        public void Equals_Success(string first, string second, bool expected)
        {
            // Arrange
            var left = TestData.ValidCity().WithName(first).Build();
            var right = TestData.ValidCity().WithName(second).Build();

            // Act
            var actual = left.Equals(right);

            // Assert
            Assert.That(actual, Is.EqualTo(expected));
        }
    }
}