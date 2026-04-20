// <copyright file="AuthorConfigurationTests.cs" company="Филипченко Марина Алексеевна">
// Copyright (c) Филипченко Марина Алексеевна 2026. Library.
// </copyright>

namespace DataAccessLayer.Tests
{
    using DataAccessLayer.Tests.Abstract;
    using Domain;
    using NUnit.Framework;

    /// <summary>
    /// Тесты для <see cref="DataAccessLayer.Configurations.AuthorConfiguration"/>.
    /// </summary>
    [TestFixture]
    internal sealed class AuthorConfigurationTests : BaseConfigurationTests
    {
        [TearDown]
        public void TearDown()
        {
            this.DataContext.ChangeTracker.Clear();
        }

        [Test]
        public void AddEntityToDatabase_Success()
        {
            // arrange
            var name = new Name("Толстой", "Лев");
            var author = new Author(name);

            // act
            _ = this.DataContext.Add(author);
            _ = this.DataContext.SaveChanges(); // <-- если что-то плохо, то тут БУМ!
            this.DataContext.ChangeTracker.Clear();

            // assert
            var result = this.DataContext.Find<Author>(author.Id);

            Assert.That(result, Is.Not.Null);
            Assert.That(result!.FullName, Is.EqualTo(author.FullName));
        }
    }
}
