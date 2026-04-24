// <copyright file="TranslatorConfigurationTests.cs" company="Филипченко Марина Алексеевна">
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
    /// Тесты для <see cref="DataAccessLayer.Configurations.TranslatorConfiguration"/>.
    /// </summary>
    [TestFixture]
    internal sealed class TranslatorConfigurationTests : BaseConfigurationTests
    {
        [Test]
        public void AddEntityToDatabase_Success()
        {
            // arrange
            var person = new Person(new Name("Лозинский", "Михаил"));
            var translator = new Translator(person);

            _ = this.DataContext.Add(translator);
            _ = this.DataContext.SaveChanges();
            this.DataContext.ChangeTracker.Clear();

            // act
            var result = this.DataContext
                .Set<Translator>()
                .Include(t => t.Person)
                .First(t => t.Id == translator.Id);

            // assert
            Assert.Multiple(() =>
            {
                Assert.That(result!.Person.FullName, Is.EqualTo(translator.Person.FullName));
                Assert.That(result, Is.InstanceOf<Translator>());
            });
        }
    }
}
