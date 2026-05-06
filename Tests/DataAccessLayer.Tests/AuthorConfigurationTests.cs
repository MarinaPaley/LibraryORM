// <copyright file="AuthorConfigurationTests.cs" company="Филипченко Марина Алексеевна">
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
    /// Тесты для <see cref="DataAccessLayer.Configurations.AuthorConfiguration"/>.
    /// </summary>
    [TestFixture]
    internal sealed class AuthorConfigurationTests : BaseConfigurationTests
    {
        [Test]
        public void AddEntityToDatabase_Success()
        {
            // arrange
            var name = new Name("Толстой", "Лев");
            var person = new Person(name);
            var author = new Author(person);

            // act
            _ = this.DataContext.Add(author);
            _ = this.DataContext.SaveChanges(); // <-- если что-то плохо, то тут БУМ!
            this.DataContext.ChangeTracker.Clear();

            // assert
            var result = this.DataContext
                .Authors
                .Include(a => a.Person)
                .First(a => a.Id == author.Id);

            Assert.That(result, Is.Not.Null);
            Assert.That(result!.Person.FullName, Is.EqualTo(author.Person.FullName));
        }
    }
}
