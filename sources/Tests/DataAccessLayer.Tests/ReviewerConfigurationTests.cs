// <copyright file="ReviewerConfigurationTests.cs" company="Филипченко Марина Алексеевна">
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
    /// Тесты для <see cref="DataAccessLayer.Configurations.ReviwerConfiguration"/>.
    /// </summary>
    [TestFixture]
    internal sealed class ReviewerConfigurationTests : BaseConfigurationTests
    {
        [Test]
        public void AddEntityToDatabase_Success()
        {
            var person = new Person(new Name("Горький", "Максим"));
            var reviwer = new Reviewer(person);

            _ = this.DataContext.Add(reviwer);
            _ = this.DataContext.SaveChanges();
            this.DataContext.ChangeTracker.Clear();

            var result = this.DataContext
                .Set<Reviewer>()
                .Include(e => e.Person)
                .First(e => e.Id == reviwer.Id);

            Assert.Multiple(() =>
            {
                Assert.That(result!.Person.FullName, Is.EqualTo(reviwer.Person.FullName));
                Assert.That(result, Is.InstanceOf<Reviewer>());
            });
        }
    }
}
