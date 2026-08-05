// <copyright file="StreetTests.cs" company="Филипченко Марина Алексеевна">
// Copyright (c) Филипченко Марина Алексеевна 2026. Library.
// </copyright>

namespace Domain.Tests
{
    using System;
    using Domain;
    using NUnit.Framework;
    using TestDataProvider;

    /// <summary>
    /// Модульные тесты для класса <see cref="Street"/>.
    /// </summary>
    [TestFixture]
    public sealed class StreetTests
    {
        private City city = null!;

        /// <summary>
        /// Инициализирует тестовые данные перед каждым тестом для обеспечения изоляции.
        /// </summary>
        [SetUp]
        public void SetUp()
        {
            this.city = TestData.ValidCity().WithName("City").Build();
        }

        /// <summary>
        /// Проверяет, что конструктор <see cref="Street"/> успешно создает объект с валидными данными.
        /// </summary>
        [Test]
        public void Ctor_ValidData_Success()
        {
            // Act & Assert
            Assert.DoesNotThrow(() => _ = new Street("Street", this.city));
        }

        /// <summary>
        /// Проверяет, что конструктор <see cref="Street"/> выбрасывает исключение при невалидном имени.
        /// </summary>
        /// <param name="name"> Некорректное имя улицы. </param>
        [TestCase(null)]
        [TestCase("")]
        public void Ctor_BadNameData_Throws(string? name)
        {
            // Act & Assert
            Assert.Throws<ArgumentNullException>(() => _ = new Street(name!, this.city));
        }

        /// <summary>
        /// Проверяет, что конструктор <see cref="Street"/> выбрасывает исключение при передаче null в качестве города.
        /// </summary>
        [Test]
        public void Ctor_NullCityData_Throws()
        {
            // Act & Assert
            Assert.Throws<ArgumentNullException>(() => _ = new Street("Street", null!));
        }

        /// <summary>
        /// Проверяет логику равенства двух экземпляров <see cref="Street"/> и корректное добавление в коллекцию города.
        /// </summary>
        /// <param name="first"> Название первой улицы. </param>
        /// <param name="second"> Название второй улицы. </param>
        /// <param name="expected"> Ожидаемый результат сравнения. </param>
        /// <param name="count"> Ожидаемое количество улиц в коллекции города. </param>
        [TestCase("Street1", "Street2", false, 2)]
        public void Equals_Success(string first, string second, bool expected, int count)
        {
            // Arrange
            var newCity = TestData.ValidCity().WithName("City").Build();
            var left = new Street(first, newCity);
            var right = new Street(second, newCity);

            // Act
            var actual = left.Equals(right);

            // Assert
            using (Assert.EnterMultipleScope())
            {
                Assert.That(actual, Is.EqualTo(expected));
                Assert.That(newCity.Streets, Has.Count.EqualTo(count));
            }
        }
    }
}