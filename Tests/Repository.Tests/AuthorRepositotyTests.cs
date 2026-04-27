// <copyright file="AuthorRepositotyTests.cs" company="Филипченко Марина Алексеевна">
// Copyright (c) Филипченко Марина Алексеевна 2026. Library.
// </copyright>

namespace Repository.Tests
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Domain;
    using NUnit.Framework;

    internal sealed class AuthorRepositotyTests
        : BaseReposytoryTests<AuthorRepository, Author>
    {
        [SetUp]
        public void SetUp()
        {
            _ = this.DataContext.Database.EnsureCreated();
        }

        [TearDown]
        public void TearDown()
        {
            _ = this.DataContext.Database.EnsureDeleted();
        }

        [Test]
        public void Create_ValidData_Success()
        {
            // arrange
            var name = new Name("Толстой", "Лев");
            var person = new Person(name);
            var author = new Author(person);

            // act
            _ = this.Repository.CreateAsync(author);

            // assert
            var result = this.DataContext.Find<Author>(author.Id);

            Assert.That(result, Is.EqualTo(author));
        }

        [Test]
        public void Update_ValidData_Success()
        {
            // arrange
            var name = new Name("Толстой", "Лев");
            var person = new Person(name);
            var author = new Author(person);
            _ = this.DataContext.Add(author);
            _ = this.DataContext.SaveChanges();

            // act
            author.Person.DateBirth = new DateOnly(1828, 09, 28);
            _ = this.Repository.UpdateAsync(author);

            // assert
            var result = this.DataContext.Find<Author>(author.Id)?.Person.DateBirth;

            Assert.That(result, Is.EqualTo(author.Person.DateBirth));
        }

        [Test]
        public void Delete_ValidData_Success()
        {
            // arrange
            var name = new Name("Толстой", "Лев");
            var person = new Person(name);
            var author = new Author(person);
            _ = this.DataContext.Add(author);
            _ = this.DataContext.SaveChanges();

            // act
            _ = this.Repository.DeleteAsync(author);

            // assert
            var result = this.DataContext.Find<Author>(author.Id);

            Assert.That(result, Is.Null);
        }

        [Test]
        public async Task GetIdByName_FamilyName_Success()
        {
            // arrange
            var familyName = "Толстой";

            var authors = new[]
            {
                new Author(new Person(new Name(familyName, "Лев", "Николаевич"))),
                new Author(new Person(new Name(familyName, "Алексей", "Константинович"))),
                new Author(new Person(new Name(familyName, "Алексей", "Николаевич"))),
            };

            await this.DataContext.AddRangeAsync(authors);
            _ = this.DataContext.SaveChangesAsync();
            this.DataContext.ChangeTracker.Clear();

            // act
            var result = await this.Repository.GetIdByNameAsync(familyName);

            // Act
            Assert.That(
                authors.Select(author => author.Id),
                Has.One.EqualTo(result),
                message: "Полученный идентификатор входит в множество идентификаторов целевых авторов");
        }

        [Test]
        public async Task GetBooksById_ValidData_Success()
        {
            var name = new Name("Толстой", "Лев");
            var person = new Person(name);
            var author = new Author(person);
            var language = new Language("Русский");

            var manuscript1 = new Manuscript("Анна Каренина", language, new DateOnly(1873, 1, 1), new DateOnly(1877, 1, 1), author);
            var manuscript2 = new Manuscript("Война и мир", language, new DateOnly(1863, 1, 1), new DateOnly(1869, 1, 1), author);

            var manuscripts = new HashSet<Manuscript>
            {
                manuscript1,
                manuscript2,
            };

            _ = this.DataContext.Add(author);
            _ = await this.DataContext.SaveChangesAsync();
            this.DataContext.ChangeTracker.Clear();

            // act
            var result = await this.Repository.GetBooksByAuthorId(author.Id);

            // assert
            Assert.That(result, Is.EquivalentTo(manuscripts));
        }

        [Test]
        public async Task GetCoAuthors_ValidData_Success()
        {
            // arrange
            var language = new Language("Русский");

            var marina = new Person(new Name("Васильева", "Марина", "Алексеевна"), new DateOnly(1976, 1, 11));
            var constantin = new Person(new Name("Филипченко", "Константин", "Михайлович"), new DateOnly(1990, 4, 6));
            var ekaterina = new Person(new Name("Балакина", "Екатерина", "Петровна"), new DateOnly(1982, 3, 22));
            var vasilyeva = new Author(marina);
            var philipchenko = new Author(constantin);
            var balakina = new Author(ekaterina);
            var authors = new HashSet<Author> { balakina, philipchenko };

            var csv = new Manuscript("Система контроля версий", language, new HashSet<Author>() { vasilyeva, philipchenko });
            var iscs = new Manuscript("Информационное обеспечение систем управления", language, new HashSet<Author>() { vasilyeva, philipchenko, balakina });
            var term = new Manuscript("Методические указания к курсовому проектированию", language, new HashSet<Author>() { vasilyeva, balakina });
            var article = new Manuscript("Статья", language, new HashSet<Author>() { vasilyeva });
            var manuscripts = new HashSet<Manuscript> { csv, iscs, term, article };

            await this.DataContext.AddRangeAsync(manuscripts);
            _ = await this.DataContext.SaveChangesAsync();
            this.DataContext.ChangeTracker.Clear();

            // act
            var result = await this.Repository.GetCoAuthorsAsync(vasilyeva.Id);

            // assert
            Assert.That(result, Is.EquivalentTo(authors));
        }
    }
}
