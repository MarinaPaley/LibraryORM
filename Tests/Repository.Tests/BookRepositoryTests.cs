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

            _ = await this.repository.CreateAsync(book);
            this.DataContext.ChangeTracker.Clear();

            // act
            var loaded = await this.repository.GetAsync(book.Id);
            var editorPerson = new Person(new Name("Редактор", "Тестовый"));
            var editor = new Editor(editorPerson);
            loaded!.AddEditor(editor);
            var result = await this.repository.UpdateAsync(loaded);

            // assert
            Assert.That(result.Editor, Is.Not.Null);
            Assert.That(result.Editor.Person.FullName.FamilyName, Is.EqualTo("Редактор"));
        }

        [Test]
        public async Task Delete_ValidData_Success()
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

            _ = await this.repository.CreateAsync(book);
            this.DataContext.ChangeTracker.Clear();

            // act
            var result = await this.repository.DeleteAsync(book);

            // assert
            using (Assert.EnterMultipleScope())
            {
                Assert.That(result, Is.True);
                Assert.That(await this.repository.GetAsync(book.Id), Is.Null);
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
                new HashSet<Manuscript>());
            var item = new Item(book);
            shelf.AddBook(item);
            var expected = new List<Shelf>() { shelf };

            _ = await this.DataContext.AddAsync(book);
            _ = await this.DataContext.SaveChangesAsync();
            this.DataContext.ChangeTracker.Clear();

            // act
            var result = await this.repository.GetShelfAsync(book.Title!);

            // assert
            Assert.That(result, Is.EquivalentTo(expected));
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

            _ = await this.DataContext.AddAsync(book);
            _ = await this.DataContext.SaveChangesAsync();
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

            _ = await this.DataContext.AddAsync(book);
            _ = await this.DataContext.SaveChangesAsync();
            this.DataContext.ChangeTracker.Clear();

            // act
            var result = await this.repository.GetTitleAsync(book.Id);

            // assert
            Assert.That(result, Is.EqualTo(title));
        }

        [TestCaseSource(nameof(Books))]
        public async Task GetShelfByManuscriptName_ValidData_Success(List<Book> books, string toFind, HashSet<Shelf> expected)
        {
            await this.DataContext.AddRangeAsync(books);
            _ = await this.DataContext.SaveChangesAsync();
            this.DataContext.ChangeTracker.Clear();

            // act
            var actual = await this.repository.GetShelvesByManucriptNameAsync(toFind);

            // assert
            Assert.That(actual, Is.EquivalentTo(expected));
        }

        private static IEnumerable<TestCaseData> Books()
        {
            // 🔑 Фабрика для создания "свежих" сущностей с новыми Id
            static Shelf NewShelf(string name) => new (name);
            static Publisher NewPublisher(string name) => new (name);
            static BookType NewBookType(string name) => new (name);
            static Language NewLanguage(string name) => new (name);
            static Person NewPerson(string family, string given, string patronymic) => new (new Name(family, given, patronymic));
            static Author NewAuthor(string family, string given, string patronymic) => new (NewPerson(family, given, patronymic));
            static Manuscript NewManuscript(string title, ISet<Language> language, params Author[] authors)
                => new (title, language, new HashSet<Author>(authors));

            // ── Тест-кейс 1: "Рукопись 1" → Полка 1, Полка 2 ──
            {
                var shelf1 = NewShelf("Полка 1");
                var shelf2 = NewShelf("Полка 2");
                var publisher = NewPublisher("Издательство");
                var bookType = NewBookType("Книга");
                var language = new HashSet<Language>() { NewLanguage("Русский") };
                var author = NewAuthor("Фамилия", "Имя", "Отчество");

                var manuscript1 = NewManuscript("Рукопись 1", language, author);
                var manuscript2 = NewManuscript("Рукопись 2", language, author);
                var manuscript3 = NewManuscript("Рукопись 3", language, author);

                var book1 = new Book("Книга", 120, "12345", bookType, publisher, 2026, new HashSet<Manuscript> { manuscript1, manuscript2, manuscript3 });
                var item1 = new Item(book1);
                shelf1.AddBook(item1);

                var book3 = new Book(null, 50, "123", bookType, publisher, 2025, new HashSet<Manuscript> { manuscript1 });
                var item2 = new Item(book3);
                shelf2.AddBook(item2);

                yield return new TestCaseData(
                    new List<Book> { book1, book3 },
                    "Рукопись 1",
                    new HashSet<Shelf> { shelf1, shelf2 });
            }

            // ── Тест-кейс 2: "Рукопись 3" → Полка 1 ──
            {
                var shelf1 = NewShelf("Полка 1");
                var publisher = NewPublisher("Издательство1");
                var bookType = NewBookType("Книга");
                var language = new HashSet<Language>() { NewLanguage("Русский") };
                var author = NewAuthor("Фамилия", "Имя", "Отчество");

                var manuscript3 = NewManuscript("Рукопись 3", language, author);
                var book2 = new Book(null, 100, "12", bookType, publisher, 2025, new HashSet<Manuscript> { manuscript3 });
                var item = new Item(book2);
                shelf1.AddBook(item);

                yield return new TestCaseData(
                    new List<Book> { book2 },
                    "Рукопись 3",
                    new HashSet<Shelf> { shelf1 });
            }

            // ── Тест-кейс 3: "Рукопись 2" → Полка 1 (исправлено!) ──
            {
                var shelf1 = NewShelf("Полка 1");
                var shelf2 = NewShelf("Полка 2");
                var publisher = NewPublisher("Издательство2");
                var bookType = NewBookType("Книга");
                var language = new HashSet<Language>() { NewLanguage("Русский") };
                var author = NewAuthor("Фамилия", "Имя", "Отчество");

                var manuscript1 = NewManuscript("Рукопись 1", language, author);
                var manuscript2 = NewManuscript("Рукопись 2", language, author);

                var book1 = new Book("Книга", 120, "12345", bookType, publisher, 2026, new HashSet<Manuscript> { manuscript1, manuscript2 });
                var item1 = new Item(book1);
                shelf1.AddBook(item1);

                var book3 = new Book(null, 50, "123", bookType, publisher, 2025, new HashSet<Manuscript> { manuscript1 });
                var item3 = new Item(book3);
                shelf2.AddBook(item3);

                yield return new TestCaseData(
                    new List<Book> { book1, book3 },
                    "Рукопись 2",
                    new HashSet<Shelf> { shelf1 });
            }

            // ── Тест-кейс 4: "Рукопись 2" → пусто ──
            {
                var shelf1 = NewShelf("Полка 1");
                var publisher = NewPublisher("Издательство3");
                var bookType = NewBookType("Книга");
                var language = new HashSet<Language>() { NewLanguage("Русский") };
                var author = NewAuthor("Фамилия", "Имя", "Отчество");

                var manuscript3 = NewManuscript("Рукопись 3", language, author);
                var book2 = new Book(null, 100, "12", bookType, publisher, 2025, new HashSet<Manuscript> { manuscript3 });
                var item = new Item(book2);
                shelf1.AddBook(item);

                yield return new TestCaseData(
                    new List<Book> { book2 },
                    "Рукопись 2",
                    new HashSet<Shelf>());  // ✅ manuscript2 нет в book2
            }
        }
    }
}