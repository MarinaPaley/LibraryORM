// <copyright file="PublisherConfigurationTests.cs" company="Филипченко Марина Алексеевна">
// Copyright (c) Филипченко Марина Алексеевна 2026. Library.
// </copyright>

namespace DataAccessLayer.Tests
{
    using DataAccessLayer.Tests.Abstract;
    using Domain;
    using NUnit.Framework;

    /// <summary>
    /// Тесты для <see cref="DataAccessLayer.Configurations.PublisherConfiguration"/>.
    /// </summary>
    [TestFixture]
    internal sealed class PublisherConfigurationTests : BaseConfigurationTests
    {
        [Test]
        public void AddEntityToDatabase_Success()
        {
            // arrange
            var publisher = new Publisher("Издательство");

            // act
            _ = this.DataContext.Add(publisher);
            _ = this.DataContext.SaveChanges(); // <-- если что-то плохо, то тут БУМ!
            this.DataContext.ChangeTracker.Clear();

            // assert
            var result = this.DataContext.Find<Publisher>(publisher.Id);

            Assert.That(result, Is.Not.Null);
            Assert.That(result!.ToString(), Is.EqualTo(publisher.ToString()));
        }
    }
}
