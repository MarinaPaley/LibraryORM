// <copyright file="ManuscriptRepositoryTests.cs" company="Филипченко Марина Алексеевна">
// Copyright (c) Филипченко Марина Алексеевна 2026. Library.
// </copyright>

namespace Repository.Tests
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Domain;
    using NUnit.Framework;

    internal sealed class ManuscriptRepositoryTests : BaseReposytoryTests<ManuscriptRepository, Manuscript>
    {
        private ManuscriptRepository repository = null!;

        [SetUp]
        public void SetUp()
        {
            this.repository = new ManuscriptRepository(this.DataContext);
            _ = this.DataContext.Database.EnsureDeleted();
            _ = this.DataContext.Database.EnsureCreated();
        }

        [TearDown]
        public void TearDown()
        {
            _ = this.DataContext.Database.EnsureDeleted();
        }

        [Test]
        public async Task GetAuthors_FullNameIsLoaded()
        {
            // arrange
            var person = new Person(new Name("Толстой", "Лев"));
            var author = new Author(person);
            var manuscript = new Manuscript("Тест", new Language("Русский"), new HashSet<Author> { author });

            this.DataContext.Add(manuscript);
            _ = this.DataContext.SaveChangesAsync();
            this.DataContext.ChangeTracker.Clear();

            // act
            var result = (await this.repository.GetAuthorsAsync(manuscript.Id))
                ?.FirstOrDefault();

            // assert
            Assert.That(result, Is.Not.Null);
            Assert.That(result?.Person.FullName.FamilyName, Is.EqualTo("Толстой"));
        }

        [Test]
        public async Task GetAuthors_ValidData_Success()
        {
            // arrange
            var person = new Person(new Name("Толстой", "Лев"));
            var author = new Author(person);

            var manuscript = new Manuscript(
                "Произведение",
                new Language("Русский"),
                new HashSet<Author> { author });

            _ = this.DataContext.Add(manuscript);
            _ = this.DataContext.SaveChangesAsync();
            this.DataContext.ChangeTracker.Clear();

            // act
            var result = await this.repository.GetAuthorsAsync(manuscript.Id);

            // assert
            Assert.That(result, Is.Not.Null);
            Assert.That(result, Has.Count.EqualTo(1));
            Assert.That(result.Any(a => a.Person.FullName.FamilyName == "Толстой"), Is.True);
        }

        [Test]
        public async Task GetAllBooksCoAuthors_ValidData_Success()
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

            await this.DataContext.AddRangeAsync(csvManuscript, iscsManuscript, termManuscript, articleManuscript);
            _ = await this.DataContext.SaveChangesAsync();
            this.DataContext.ChangeTracker.Clear();

            // act
            var result = await this.repository.GetAllBooksCoAuthors(articleManuscript.Id);

            // assert
            Assert.That(result, Is.Not.Null);
            Assert.That(result, Has.Count.EqualTo(3)); // csv, iscs, term (исключая саму article)
            using (Assert.EnterMultipleScope())
            {
                Assert.That(result.Contains(csvManuscript), Is.True);
                Assert.That(result.Contains(iscsManuscript), Is.True);
                Assert.That(result.Contains(termManuscript), Is.True);
                Assert.That(result.Any(m => m.Name.Value == "Статья"), Is.False);
            }
        }
    }
}