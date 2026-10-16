// <copyright file="AddressTests.cs" company="Филипченко Марина Алексеевна">
// Copyright (c) Филипченко Марина Алексеевна 2026. Library.
// </copyright>

namespace Domain.Tests
{
    using Domain;
    using NUnit.Framework;
    using TestDataProvider;

    /// <summary>
    /// Модульные тесты для класса <see cref="Address"/>.
    /// </summary>
    [TestFixture]
    internal sealed class AddressTests
    {
        /// <summary>
        /// Проверяет логику равенства двух адресов с разными городами.
        /// </summary>
        /// <param name="cityName1"> Название первого города. </param>
        /// <param name="cityName2"> Название второго города. </param>
        /// <param name="expected"> Ожидаемый результат сравнения. </param>
        [TestCase("Москва", "Москва", true)]
        [TestCase("Москва", "Санкт-Петербург", false)]
        public void Equals_DifferentCities_ReturnsExpectedResult(
            string cityName1,
            string cityName2,
            bool expected)
        {
            // Arrange
            var city1 = TestData.ValidCity().WithName(cityName1).Build();
            var street1 = TestData.ValidStreet().WithName("Ленина").WithCity(city1).Build();

            var city2 = TestData.ValidCity().WithName(cityName2).Build();
            var street2 = TestData.ValidStreet().WithName("Ленина").WithCity(city2).Build();

            var address1 = TestData.ValidAddress()
                .WithStreet(street1)
                .WithHouse(10)
                .Build();

            var address2 = TestData.ValidAddress()
                .WithStreet(street2)
                .WithHouse(10)
                .Build();

            // Act
            var result = address1.Equals(address2);

            // Assert
            Assert.That(result, Is.EqualTo(expected));
        }
    }
}