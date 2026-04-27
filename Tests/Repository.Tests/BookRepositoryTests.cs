// <copyright file="BookRepositoryTests.cs" company="Филипченко Марина Алексеевна">
// Copyright (c) Филипченко Марина Алексеевна 2026. Library.
// </copyright>

namespace Repository.Tests
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Domain;
    using NUnit.Framework;

    /// <summary>
    /// Модульные тесты для <see cref="BookRepository"/>.
    /// </summary>
    [TestFixture]
    internal sealed class BookRepositoryTests
        : BaseReposytoryTests<BookRepository, Book>
    {
        private BookRepository repository = null!;

        [SetUp]
        public void SetUp()
        {
            this.repository = new BookRepository(this.DataContext);
            _ = this.DataContext.Database.EnsureDeleted();
            _ = this.DataContext.Database.EnsureCreated();
        }

        [TearDown]
        public void TearDown()
        {
            _ = this.DataContext.Database.EnsureDeleted();
        }

        [Test]
        public async Task Create_ValidData_Success()
        {
            // arrange
            var publisher = new Publisher("Издательство");
            var bookType = new BookType("Книга");
            var book = new Book(
                "Книга",
                100,
                "1",
                bookType,
                publisher,
                2024,
                new HashSet<Manuscript>());

            // act
            var result = await this.repository.CreateAsync(book);

            // assert
            Assert.That(result, Is.Not.Null);
            using (Assert.EnterMultipleScope())
            {
                Assert.That(result.Id, Is.Not.EqualTo(Guid.Empty));
                Assert.That(result.Title, Is.EqualTo("Книга"));
            }
        }

        [Test]
        public async Task Update_ValidData_Success()
        {
            // arrange
            var publisher = new Publisher("Издательство");
            var book = new Book(
                "Книга",
                100,
                "1",
                new BookType("Книга"),
                publisher,
                2024,
                new HashSet<Manuscript>());

            _ = this.repository.CreateAsync(book);
            this.DataContext.ChangeTracker.Clear();

            // act
            var loaded = await this.repository.GetAsync(book.Id);
            var editorPerson = new Person(new Name("Редактор", "Тестовый"));
            var editor = new Editor(editorPerson);
            loaded!.AddEditor(editor);
            var result = await this.repository.Update(loaded);

            // assert
            Assert.That(result.Editor, Is.Not.Null);
            Assert.That(result.Editor.Person.FullName.FamilyName, Is.EqualTo("Редактор"));
        }

        [Test]
        public void Delete_ValidData_Success()
        {
            // arrange
            var publisher = new Publisher("Издательство");
            var book = new Book(
                "Книга",
                100,
                "1",
                new BookType("Книга"),
                publisher,
                2024,
                new HashSet<Manuscript>());

            _ = this.repository.CreateAsync(book);
            this.DataContext.ChangeTracker.Clear();

            // act
            var result = this.repository.DeleteAsync(book);

            // assert
            using (Assert.EnterMultipleScope())
            {
                Assert.That(result, Is.True);
                Assert.That(this.repository.GetAsync(book.Id), Is.Null);
            }
        }

        [Test]
        public async Task GetShelf_ValidData_Success()
        {
            // arrange
            var shelf = new Shelf("1");
            var publisher = new Publisher("Издательство");
            var book = new Book(
                "Книга",
                100,
                "1",
                new BookType("Книга"),
                publisher,
                2024,
                new HashSet<Manuscript>(),
                shelf);

            _ = this.DataContext.Add(book);
            _ = this.DataContext.SaveChangesAsync();
            this.DataContext.ChangeTracker.Clear();

            // act
            var result = await this.repository.GetShelfAsync(book.Title!);

            // assert
            Assert.That(result, Is.EqualTo(shelf));
        }

        [Test]
        public async Task GetId_ValidData_Success()
        {
            // arrange
            var title = "Книга";
            var publisher = new Publisher("Издательство");
            var book = new Book(
                title,
                100,
                "1",
                new BookType("Книга"),
                publisher,
                2024,
                new HashSet<Manuscript>());

            _ = this.DataContext.Add(book);
            _ = this.DataContext.SaveChangesAsync();
            this.DataContext.ChangeTracker.Clear();

            // act
            var result = await this.repository.GetIdAsync(title);

            // assert
            Assert.That(result, Is.EqualTo(book.Id));
        }

        [Test]
        public async Task GetTitle_ValidData_Success()
        {
            // arrange
            var title = "Книга";
            var publisher = new Publisher("Издательство");
            var book = new Book(
                title,
                100,
                "1",
                new BookType("Книга"),
                publisher,
                2024,
                new HashSet<Manuscript>());

            _ = this.DataContext.Add(book);
            _ = this.DataContext.SaveChangesAsync();
            this.DataContext.ChangeTracker.Clear();

            // act
            var result = await this.repository.GetTitleAsync(book.Id);

            // assert
            Assert.That(result, Is.EqualTo(title));
        }
    }
}