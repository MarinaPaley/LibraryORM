// <copyright file="ManuscriptRepositoryTests.cs" company="Филипченко Марина Алексеевна">
// Copyright (c) Филипченко Марина Алексеевна 2026. Library.
// </copyright>

namespace Repository.Tests
{
    using Domain;
    using NUnit.Framework;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    /// <summary>
    /// Модульные тесты для <see cref="ManuscriptRepository"/>.
    /// </summary>
    internal sealed class ManuscriptRepositoryTests
        : BaseRepositoryTests<ManuscriptRepository, Manuscript>
    {
        [Test]
        public async Task GetAuthors_FullNameIsLoaded()
        {
            // arrange
            var person = new Person("Толстой", "Лев", birthYear: 1828, deathYear: 1910);
            var author = new Author(person);

            var manuscript = new Manuscript(
                "Тест",
                new HashSet<Language>() { new ("Русский") },
                new HashSet<Author> { author });

            _ = await this.DataContext.AddAsync(manuscript);
            _ = await this.DataContext.SaveChangesAsync();

            // act
            var result = await this.Repository.GetAuthorsAsync(manuscript.Id);

            // assert
            Assert.That(result, Is.Not.Null);

            var x = result.FirstOrDefault();
            Assert.That(x, Is.Not.Null);
            Assert.That(x.Person, Is.Not.Null);
            Assert.That(x.Person.FullName.FamilyName, Is.EqualTo("Толстой"));
        }

        [Test]
        public async Task GetAuthors_ValidData_Success()
        {
            // arrange
            var person = new Person(new Name("Фамилий", "Имён"));
            var author = new Author(person);

            var manuscript = new Manuscript(
                "Произведение",
                new HashSet<Language>() { new ("Русский") },
                new HashSet<Author> { author });

            _ = await this.DataContext.AddAsync(manuscript);
            _ = await this.DataContext.SaveChangesAsync();

            // act
            var result = await this.Repository.GetAuthorsAsync(manuscript.Id);

            // assert
            Assert.That(result, Is.Not.Null);
            Assert.That(result, Has.Count.EqualTo(1));
            Assert.That(result.Any(a => a.Person.FullName.FamilyName == "Фамилий"), Is.True);
        }

        [Test]
        public async Task GetAllBooksCoAuthors_ValidData_Success()
        {
            // arrange
            var language = new HashSet<Language>() { new ("Русский") };

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

            await this.DataContext.AddRangeAsync(csvManuscript, iscsManuscript, termManuscript, articleManuscript);
            _ = await this.DataContext.SaveChangesAsync();
            this.DataContext.ChangeTracker.Clear();

            // act
            var result = await this.Repository.GetAllBooksCoAuthors(articleManuscript.Id);

            // assert
            Assert.That(result, Is.Not.Null);
            Assert.That(result, Has.Count.EqualTo(3)); // csv, iscs, term (исключая саму article)
            using (Assert.EnterMultipleScope())
            {
                Assert.That(result.Any(m => m.Id == csvManuscript.Id), Is.True);
                Assert.That(result.Any(m => m.Id == iscsManuscript.Id), Is.True);
                Assert.That(result.Any(m => m.Id == termManuscript.Id), Is.True);
                Assert.That(result.Any(m => m.Name.Value == "Статья"), Is.False);
            }
        }
    }
}
