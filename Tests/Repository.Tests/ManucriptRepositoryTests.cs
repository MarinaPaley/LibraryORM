// <copyright file="ManucriptRepositoryTests.cs" company="Филипченко Марина Алексеевна">
// Copyright (c) Филипченко Марина Алексеевна 2026. Library.
// </copyright>

namespace Repository.Tests
{
    using Domain;
    using Microsoft.EntityFrameworkCore;
    using NUnit.Framework;
    using System;
    using System.Collections.Generic;
    using System.Linq;

    internal sealed class ManucriptRepositoryTests : BaseReposytoryTests<ManuscriptRepository, Manuscript>
    {
        private ManuscriptRepository repository = null!;

        [SetUp]
        public void SetUp()
        {
            this.repository = new ManuscriptRepository(this.DataContext);
            _ = this.DataContext.Database.EnsureCreated();
        }

        [TearDown]
        public void TearDown()
        {
            _ = this.DataContext.Database.EnsureDeleted();
        }

        [Test]
        public void GetAuthors_FullNameIsLoaded()
        {
            // arrange
            var person = new Person(new Name("Толстой", "Лев"));
            var author = new Author(person);
            var manuscript = new Manuscript("Тест", new Language("Русский"), new HashSet<Author> { author });

            this.DataContext.Add(manuscript);
            _ = this.DataContext.SaveChanges();
            this.DataContext.ChangeTracker.Clear();

            // act
            var result = this.repository.GetAuthors(manuscript.Id)?.FirstOrDefault();

            // assert
            Assert.That(result?.Person.FullName.FamilyName, Is.EqualTo("Толстой"));
        }

        [Test]
        public void GetAuthors_ValidData_Success()
        {
            // arrange
            var person = new Person(new Name("Толстой", "Лев"));
            var author = new Author(person);

            var manuscript = new Manuscript(
                "Произведение",
                new Language("Русский"),
                new HashSet<Author> { author });

            _ = this.DataContext.Add(manuscript);
            _ = this.DataContext.SaveChanges();
            this.DataContext.ChangeTracker.Clear();

            // После SaveChanges() и Clear()
            var debugManuscript = this.DataContext.Manuscripts
                .Include(m => m.Authors)
                .FirstOrDefault(m => m.Id == manuscript.Id);

            Console.WriteLine($"🔍 Manuscript found: {debugManuscript != null}");
            Console.WriteLine($"🔍 Authors count: {debugManuscript?.Authors?.Count ?? 0}");
            if (debugManuscript?.Authors != null)
            {
                foreach (var a in debugManuscript.Authors)
                    Console.WriteLine($"  • Author: {a.Person.FullName}");
            }

            // act
            var result = this.repository.GetAuthors(manuscript.Id);

            // assert
            Assert.That(result, Has.Count.EqualTo(1));
            Assert.That(result.Any(a => a.Person.FullName.FamilyName == "Толстой"), Is.True);
        }

        [Test]
        public void GetAllBooksCoAuthors_ValidData_Success()
        {
            // arrange
            var language = new Language("Русский");

            var marina = new Person(new Name("Васильева", "Марина", "Алексеевна"));
            var constantin = new Person(new Name("Филипченко", "Константин", "Михайлович"));
            var ekaterina = new Person(new Name("Балакина", "Екатерина", "Петровна"));

            var vasilyeva = new Author(marina);
            var philipchenko = new Author(constantin);
            var balakina = new Author(ekaterina);

            var csvManuscript = new Manuscript(
                "Система контроля версий",
                language,
                new HashSet<Author> { vasilyeva, philipchenko });

            var iscsManuscript = new Manuscript(
                "Информационное обеспечение систем управления",
                language,
                new HashSet<Author> { vasilyeva, philipchenko, balakina });

            var termManuscript = new Manuscript(
                "Методические указания к курсовому проектированию",
                language,
                new HashSet<Author> { vasilyeva, balakina });

            var articleManuscript = new Manuscript(
                "Статья",
                language,
                new HashSet<Author> { vasilyeva });

            this.DataContext.AddRange(csvManuscript, iscsManuscript, termManuscript, articleManuscript);
            _ = this.DataContext.SaveChanges();
            this.DataContext.ChangeTracker.Clear();

            // act
            var result = this.repository.GetAllBooksCoAuthors(articleManuscript.Id);

            // assert
            Assert.That(result, Has.Count.EqualTo(3)); // csv, iscs, term (исключая саму article)
            Assert.Multiple(() =>
            {
                Assert.That(result.Contains(csvManuscript), Is.True);
                Assert.That(result.Contains(iscsManuscript), Is.True);
                Assert.That(result.Contains(termManuscript), Is.True);
            });
        }
    }
}