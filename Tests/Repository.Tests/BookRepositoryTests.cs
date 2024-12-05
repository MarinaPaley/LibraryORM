// <copyright file="BookRepositoryTests.cs" company="Васильева Марина Алексеевна">
// Copyright (c) Васильева Марина Алексеевна 2024. Library.
// </copyright>

namespace Repository.Tests
{
    using System.Collections.Generic;
    using Domain;
    using NUnit.Framework;

    /// <summary>
    /// Модульные тесты для <see cref="BookRepository"/>.
    /// </summary>
    [TestFixture]
    internal sealed class BookRepositoryTests
        : BaseReposytoryTests<BookRepository, Book>
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
            var book = new Book("Книга", 100, "1");

            // act
            _ = this.Repository.Create(book);

            // assert
            var result = this.DataContext.Find<Book>(book.Id);

            Assert.That(result, Is.EqualTo(book));
        }

        [Test]
        public void Update_ValidData_Success()
        {
            // arrange
            var book = new Book("Книга", 100, "1");
            var name = new Name("Толстой", "Лев");
            var author = new Author(name);
            this.DataContext.Add(book);
            this.DataContext.SaveChanges();
            book.Authors.Add(author);

            // act
            _ = this.Repository.Update(book);

            // assert
            var result = this.DataContext.Find<Book>(book.Id);
            Assert.That(result?.Authors.Count, Is.EqualTo(1));
            Assert.That(result?.Authors.Contains(author), Is.True);
        }

        [Test]
        public void Delete_ValidData_Success()
        {
            // arrange
            var book = new Book("Книга", 100, "1");
            this.DataContext.Add(book);
            this.DataContext.SaveChanges();

            // act
            _ = this.Repository.Delete(book);

            // assert
            var result = this.DataContext.Find<Book>(book.Id);

            Assert.That(result, Is.Null);
        }

        [Test]
        public void GetShelf_ValidData_Success()
        {
            // arrange
            var shelf = new Shelf("1");

            var book = new Book("Книга", 100, "1", shelf);

            _ = this.DataContext.Add(book);
            _ = this.DataContext.SaveChanges();
            this.DataContext.ChangeTracker.Clear();

            // act
            var result = this.Repository.GetShelf(book.Title);

            // assert
            Assert.That(result, Is.EqualTo(shelf));
        }

        [Test]
        public void GetId_ValidData_Success()
        {
            // arrange
            var title = "Книга";
            var book = new Book(title, 100, "1");
            _ = this.DataContext.Add(book);
            _ = this.DataContext.SaveChanges();
            this.DataContext.ChangeTracker.Clear();

            // act
            var result = this.Repository.GetId(title);

            // assert
            Assert.That(result, Is.EqualTo(book.Id));
        }

        [Test]
        public void GetTitle_ValidData_Success()
        {
            // arrange
            var title = "Книга";
            var book = new Book(title, 100, "1");
            _ = this.DataContext.Add(book);
            _ = this.DataContext.SaveChanges();
            this.DataContext.ChangeTracker.Clear();

            // act
            var result = this.Repository.GetTitle(book.Id);

            // assert
            Assert.That(result, Is.EqualTo(title));
        }

        [Test]
        public void GetAuthos_ValidData_Success()
        {
            // arrange
            var name = new Name("Толстой", "Лев");
            var author = new Author(name);
            var authors = new HashSet<Author>();
            _ = authors.Add(author);
            var book = new Book("Книга", 100, "1", null, author);
            _ = this.DataContext.Add(book);
            _ = this.DataContext.SaveChanges();
            this.DataContext.ChangeTracker.Clear();

            // act
            var result = this.Repository.GetAuthors(book.Id);

            // assert
            Assert.That(result, Is.EqualTo(authors));
        }

        [Test]
        public void GetAllBooksCoAuthors_ValidData_Success()
        {
            // arrange
            var marina = new Name("Васильева", "Марина", "Алексеевна");
            var constantin = new Name("Филипченко", "Константин", "Михайлович");
            var ekaterina = new Name("Балакина", "Екатерина", "Петровна");
            var vasilyeva = new Author(marina);
            var philipchenko = new Author(constantin);
            var balakina = new Author(ekaterina);

            var csv = new Book("Система контроля версий", 200, "1", null, vasilyeva, philipchenko);
            var iscs = new Book("Информационное обеспечение систем управления", 150, "2", null, vasilyeva, philipchenko, balakina);
            var term = new Book("Методические указания к курсовому проектированию", 50, "3", null, vasilyeva, balakina);
            var article = new Book("Статья", 5, "4", null, vasilyeva);
            var books = new HashSet<Book> { csv, iscs, term, article };

            this.DataContext.AddRange(books);
            _ = this.DataContext.SaveChanges();
            this.DataContext.ChangeTracker.Clear();

            // act
            var result = this.Repository.GetAllBooksCoAuthors(article.Id);

            // assert
            Assert.That(result, Is.EquivalentTo(books));
        }
    }
}
