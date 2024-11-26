﻿// <copyright file="AuthorConfigurationTests.cs" company="Васильева Марина Алексеевна">
// Copyright (c) Васильева Марина Алексеевна 2024. Library.
// </copyright>

namespace DataAccessLibrary.Tests
{
    using Domain;
    using NUnit.Framework;

    /// <summary>
    /// Тесты для <see cref="DataAccessLibrary.Configurations.AuthorConfiguration"/>.
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
