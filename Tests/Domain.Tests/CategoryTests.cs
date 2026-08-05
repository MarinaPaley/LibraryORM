// <copyright file="CategoryTests.cs" company="Филипченко Марина Алексеевна">
// Copyright (c) Филипченко Марина Алексеевна 2026. Library.
// </copyright>

namespace Domain.Tests
{
    using System;
    using Domain;
    using NUnit.Framework;
    using TestDataProvider;

    /// <summary>
    /// Модульные тесты для класса <see cref="Category"/>.
    /// </summary>
    [TestFixture]
    public sealed class CategoryTests
    {
        /// <summary>
        /// Проверяет, что конструктор <see cref="Category"/> успешно создает объект с валидными данными.
        /// </summary>
        [Test]
        public void Ctor_ValidData_Success()
        {
            // Arrange
            var color = TestData.ValidColor().Build();

            // Act & Assert
            Assert.DoesNotThrow(() => _ = new Category("Category", color));
        }

        /// <summary>
        /// Проверяет, что конструктор <see cref="Category"/> выбрасывает исключение при невалидном имени.
        /// </summary>
        /// <param name="name"> Некорректное имя категории (null или пустая строка). </param>
        [TestCase(null)]
        [TestCase("")]
        public void Ctor_BadData_Throws(string? name)
        {
            // Arrange
            var color = TestData.ValidColor().Build();

            // Act & Assert
            Assert.Throws<ArgumentNullException>(() => _ = new Category(name!, color));
        }

        /// <summary>
        /// Проверяет логику равенства двух экземпляров <see cref="Category"/>.
        /// </summary>
        /// <param name="first"> Название первой категории. </param>
        /// <param name="second"> Название второй категории. </param>
        /// <param name="expected"> Ожидаемый результат сравнения. </param>
        [TestCase("Category", "Category", true)]
        [TestCase("Category", "New", false)]
        public void Equals_Success(string first, string second, bool expected)
        {
            // Arrange
            var color = TestData.ValidColor().Build();
            var left = new Category(first, color);
            var right = new Category(second, color);

            // Act
            var actual = left.Equals(right);

            // Assert
            Assert.That(actual, Is.EqualTo(expected));
        }
    }
}