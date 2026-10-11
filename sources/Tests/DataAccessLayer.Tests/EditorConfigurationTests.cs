// <copyright file="EditorConfigurationTests.cs" company="Филипченко Марина Алексеевна">
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
    /// Тесты для <see cref="DataAccessLayer.Configurations.EditorConfiguration"/>.
    /// </summary>
    [TestFixture]
    internal sealed class EditorConfigurationTests : BaseConfigurationTests
    {
        // EditorConfigurationTests.cs
        [Test]
        public void AddEntityToDatabase_Success()
        {
            // arrange
            var person = new Person(new Name("Горький", "Максим"));
            var editor = new Editor(person);

            // act
            _ = this.DataContext.Add(editor);
            _ = this.DataContext.SaveChanges();
            this.DataContext.ChangeTracker.Clear();

            // assert
            var result = this.DataContext
                .Set<Editor>()
                .Include(e => e.Person)
                .First(e => e.Id == editor.Id);

            using (Assert.EnterMultipleScope())
            {
                Assert.That(result.Person.FullName, Is.EqualTo(editor.Person.FullName));
                Assert.That(result, Is.InstanceOf<Editor>());
            }
        }
    }
}
