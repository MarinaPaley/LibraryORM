// <copyright file="AuthorRepositotyTests.cs" company="Васильева Марина Алексеевна">
// Copyright (c) Васильева Марина Алексеевна 2024. Library.
// </copyright>

namespace Repository.Tests
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
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
            var author = new Author(name);

            // act
            _ = this.Repository.Create(author);

            // assert
            var result = this.DataContext.Find<Author>(author.Id);

            Assert.That(result, Is.EqualTo(author));
        }

        [Test]
        public void Update_ValidData_Success()
        {
            // arrange
            var name = new Name("Толстой", "Лев");
            var author = new Author(name);
            _ = this.DataContext.Add(author);
            _ = this.DataContext.SaveChanges();

            // act
            author.DateBirth = new DateOnly(1828, 09, 28);
            _ = this.Repository.Update(author);

            // assert
            var result = this.DataContext.Find<Author>(author.Id)?.DateBirth;

            Assert.That(result, Is.EqualTo(author.DateBirth));
        }

        [Test]
        public void Delete_ValidData_Success()
        {
            // arrange
            var name = new Name("Толстой", "Лев");
            var author = new Author(name);
            _ = this.DataContext.Add(author);
            _ = this.DataContext.SaveChanges();

            // act
            _ = this.Repository.Delete(author);

            // assert
            var result = this.DataContext.Find<Author>(author.Id);

            Assert.That(result, Is.Null);
        }

        [Test]
        public void GetIdByName_FamilyName_Success()
        {
            // arrange
            var familyName = "Толстой";

            var authors = new[]
            {
                new Author(new Name(familyName, "Лев", "Николаевич")),
                new Author(new Name(familyName, "Алексей", "Константинович")),
                new Author(new Name(familyName, "Алексей", "Николаевич")),
            };

            this.DataContext.AddRange(authors);
            _ = this.DataContext.SaveChanges();
            this.DataContext.ChangeTracker.Clear();

            // act
            var result = this.Repository.GetIdByName(familyName);

            // Act
            Assert.That(
                authors.Select(author => author.Id),
                Has.One.EqualTo(result),
                message: "Полученный идентификатор входит в множество идентификаторов целевых авторов");
        }

        [Test]
        public void GetBooksById_ValidData_Success()
        {
            var name = new Name("Толстой", "Лев");
            var author = new Author(name);
            var shelf = new Shelf("1");

            var books = new HashSet<Book>
            {
                new ("Анна Каренина", 100, "1", shelf, author),
                new ("Война и мир", 1000, "2", shelf, author),
            };

            _ = this.DataContext.Add(author);
            _ = this.DataContext.SaveChanges();
            this.DataContext.ChangeTracker.Clear();

            // act
            var result = this.Repository.GetBooksByAuthorId(author.Id);

            // assert
            Assert.That(result, Is.EquivalentTo(books));
        }

        public void GetCoAuthors_ValidData_Success()
        {
            // arrange
            var marina = new Name("Васильева", "Марина", "Алексеевна");
            var constantin = new Name("Филипченко", "Константин", "Михайлович");
            var ekaterina = new Name("Балакина", "Екатерина", "Петровна");
            var vasilyeva = new Author(marina);
            var philipchenko = new Author(constantin);
            var balakina = new Author(ekaterina);
            var authors = new HashSet<Author> { balakina, philipchenko };

            var csv = new Book("Система контроля версий", 200, "1", null, vasilyeva, philipchenko);
            var iscs = new Book("Информационное обеспечение систем управления", 150, "2", null, vasilyeva, philipchenko, balakina);
            var term = new Book("Методические указания к курсовому проектированию", 50, "3", null, vasilyeva, balakina);
            var article = new Book("Статья", 5, "4", null, vasilyeva);
            var books = new HashSet<Book> { csv, iscs, term, article };

            this.DataContext.AddRange(books);
            _ = this.DataContext.SaveChanges();
            this.DataContext.ChangeTracker.Clear();

            // act
            var result = this.Repository.GetCoAuthors(vasilyeva.Id);

            // assert
            Assert.That(result, Is.EquivalentTo(authors));
        }
    }
}
