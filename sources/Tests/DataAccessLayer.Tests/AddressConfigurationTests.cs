// <copyright file="AddressConfigurationTests.cs" company="Филипченко Марина Алексеевна">
// Copyright (c) Филипченко Марина Алексеевна 2026. Library.
// </copyright>

namespace DataAccessLayer.Tests
{
    using System.Linq;
    using DataAccessLayer.Tests.Abstract;
    using Domain;
    using Microsoft.EntityFrameworkCore;
    using NUnit.Framework;

    /// <summary>
    /// Тесты для <see cref="DataAccessLayer.Configurations.AddressConfiguration"/>.
    /// </summary>
    [TestFixture]
    internal sealed class AddressConfigurationTests : BaseConfigurationTests
    {
        [Test]
        public void AddEntityToDatabase_Success()
        {
            // arrange
            var city = new City("Город");
            var street = new Street("Улица", city);
            var address = new Address(street, 21, "корп. 1", 34);

            // act
            _ = this.DataContext.Add(address);
            _ = this.DataContext.SaveChanges(); // <-- если что-то плохо, то тут БУМ!
            this.DataContext.ChangeTracker.Clear();

            // assert
            var result = this.DataContext
                .Set<Address>()
                .Include(a => a.Street)
                    .ThenInclude(c => c.Name)
                .Include(a => a.Street)
                    .ThenInclude(c => c.City)
                        .ThenInclude(c => c.Name)
                .First(a => a.Id == address.Id);

            Assert.That(result, Is.Not.Null);
            Assert.That(result!.ToString(), Is.EqualTo(address.ToString()));
        }
    }
}
