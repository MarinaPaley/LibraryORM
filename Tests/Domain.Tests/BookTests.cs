// <copyright file="BookTests.cs" company="Филипченко Марина Алексеевна">
// Copyright (c) Филипченко Марина Алексеевна 2026. Library.
// </copyright>

namespace Domain.Tests
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using NUnit.Framework;

    /// <summary>
    /// Тесты для <see cref="Domain.Book"/>.
    /// </summary>
    [TestFixture]
    internal sealed class BookTests
    {
        #region Helper methods

        private static BookType CreateBookType(string name = "Книга") => new (name);

        private static Publisher CreatePublisher(string name = "Издательство") => new (name);

        private static Language CreateLanguage(string name = "Русский") => new (name);

        private static Author CreateAuthor(string family = "Фамилия", string given = "Имя") =>
            new (new Person(new Name(family, given)));

        private static Manuscript CreateManuscript(string title = "Произведение", params Author[] authors)
        {
            if (authors.Length != 0)
            {
                return new (title, CreateLanguage(), new HashSet<Author>(authors));
            }

            return new Manuscript(title, CreateLanguage(), new HashSet<Author>() { CreateAuthor() });
        }

        #endregion

        #region Constructor validation tests

        [Test]
        public void Ctor_ValidData_Success()
        {
            // arrange
            var publisher = CreatePublisher();
            var bookType = CreateBookType();
            var manuscript = CreateManuscript();

            // act & assert
            Assert.DoesNotThrow(() =>
            {
                _ = new Book(
                    "Тестовая книга",
                    300,
                    "978-5-123456-78-9",
                    bookType,
                    publisher,
                    2024,
                    new HashSet<Manuscript> { manuscript });
            });
        }

        [Test]
        public void Ctor_NullTitle_Allowed()
        {
            // arrange
            var publisher = CreatePublisher();
            var bookType = CreateBookType();
            var manuscript = CreateManuscript();

            // act & assert
            Assert.DoesNotThrow(() =>
            {
                var book = new Book(
                    null,
                    300,
                    "978-5-123456-78-9",
                    bookType,
                    publisher,
                    2024,
                    new HashSet<Manuscript> { manuscript });

                Assert.That(book.Title, Is.Null);
            });
        }

        [Test]
        public void Ctor_NullISBN_ThrowsArgumentNullException()
        {
            // arrange
            var publisher = CreatePublisher();
            var bookType = CreateBookType();
            var manuscript = CreateManuscript();

            // act & assert
            Assert.Throws<ArgumentNullException>(() =>
                _ = new Book("Книга", 300, null!, bookType, publisher, 2024, new HashSet<Manuscript> { manuscript }));
        }

        [Test]
        public void Ctor_EmptyISBN_AfterTrim_ThrowsArgumentNullException()
        {
            // arrange
            var publisher = CreatePublisher();
            var bookType = CreateBookType();
            var manuscript = CreateManuscript();

            // act & assert
            Assert.Throws<ArgumentNullException>(() =>
                _ = new Book("Книга", 300, "   ", bookType, publisher, 2024, new HashSet<Manuscript> { manuscript }));
        }

        [Test]
        public void Ctor_NullPublisher_ThrowsArgumentNullException()
        {
            // arrange
            var bookType = CreateBookType();
            var manuscript = CreateManuscript();

            // act & assert
            Assert.Throws<ArgumentNullException>(() =>
                _ = new Book("Книга", 300, "123", bookType, null!, 2024, new HashSet<Manuscript> { manuscript }));
        }

        [Test]
        public void Ctor_NullBookType_ThrowsArgumentNullException()
        {
            // arrange
            var publisher = CreatePublisher();
            var manuscript = CreateManuscript();

            // act & assert
            Assert.Throws<ArgumentNullException>(() =>
                _ = new Book("Книга", 300, "123", null!, publisher, 2024, new HashSet<Manuscript> { manuscript }));
        }

        [Test]
        public void Ctor_NullManuscripts_ThrowsArgumentNullException()
        {
            // arrange
            var publisher = CreatePublisher();
            var bookType = CreateBookType();

            // act & assert
            Assert.Throws<ArgumentNullException>(() =>
                _ = new Book("Книга", 300, "123", bookType, publisher, 2024, null!));
        }

        [TestCase(0)]
        [TestCase(-1)]
        [TestCase(-100)]
        public void Ctor_NegativeOrZeroPages_ThrowsArgumentOutOfRangeException(int pages)
        {
            // arrange
            var publisher = CreatePublisher();
            var bookType = CreateBookType();
            var manuscript = CreateManuscript();

            // act & assert
            Assert.Throws<ArgumentOutOfRangeException>(() =>
                _ = new Book("Книга", pages, "123", bookType, publisher, 2024, new HashSet<Manuscript> { manuscript }));
        }

        [TestCase(0)]
        [TestCase(-1)]
        public void Ctor_NegativeOrZeroYear_ThrowsArgumentOutOfRangeException(int year)
        {
            // arrange
            var publisher = CreatePublisher();
            var bookType = CreateBookType();
            var manuscript = CreateManuscript();

            // act & assert
            Assert.Throws<ArgumentOutOfRangeException>(() =>
                _ = new Book("Книга", 300, "123", bookType, publisher, year, new HashSet<Manuscript> { manuscript }));
        }

        [Test]
        public void Ctor_YearGreaterThanCurrent_ThrowsArgumentOutOfRangeException()
        {
            // arrange
            var publisher = CreatePublisher();
            var bookType = CreateBookType();
            var manuscript = CreateManuscript();
            var futureYear = DateTime.Now.Year + 1;

            // act & assert
            Assert.Throws<ArgumentOutOfRangeException>(() =>
                _ = new Book("Книга", 300, "123", bookType, publisher, futureYear, new HashSet<Manuscript> { manuscript }));
        }

        [TestCase(0)]
        [TestCase(-1)]
        public void Ctor_NegativeVolume_ThrowsArgumentOutOfRangeException(int? volume)
        {
            // arrange
            var publisher = CreatePublisher();
            var bookType = CreateBookType();
            var manuscript = CreateManuscript();

            // act & assert
            Assert.Throws<ArgumentOutOfRangeException>(() =>
                _ = new Book("Книга", 300, "123", bookType, publisher, 2024,
                    new HashSet<Manuscript> { manuscript }, volume: volume));
        }

        [Test]
        public void Ctor_NullVolume_Allowed()
        {
            // arrange
            var publisher = CreatePublisher();
            var bookType = CreateBookType();
            var manuscript = CreateManuscript();

            // act
            var book = new Book("Книга", 300, "123", bookType, publisher, 2024,
                new HashSet<Manuscript> { manuscript }, volume: null);

            // assert
            Assert.That(book.Volume, Is.Null);
        }

        #endregion

        #region Business logic tests

        [Test]
        public void Ctor_Shelf_AddsBookToShelf()
        {
            // arrange
            var shelf = new Shelf("A1");
            var publisher = CreatePublisher();
            var bookType = CreateBookType();
            var manuscript = CreateManuscript();

            // act
            var book = new Book("Книга", 300, "123", bookType, publisher, 2024,
                new HashSet<Manuscript> { manuscript }, shelf: shelf);

            // assert
            Assert.That(shelf.Books, Contains.Item(book));
        }

        [Test]
        public void Ctor_Editor_AddsBookToEditor()
        {
            // arrange
            var editor = new Editor(new Person(new Name("Редактор", "Тестовый")));
            var publisher = CreatePublisher();
            var bookType = CreateBookType();
            var manuscript = CreateManuscript();

            // act
            var book = new Book("Книга", 300, "123", bookType, publisher, 2024,
                new HashSet<Manuscript> { manuscript }, editor: editor);

            // assert
            Assert.That(editor.Books, Contains.Item(book));
        }

        [Test]
        public void Ctor_Seria_AddsBookToSeria()
        {
            // arrange
            var seria = new Seria("Научная серия");
            var publisher = CreatePublisher();
            var bookType = CreateBookType();
            var manuscript = CreateManuscript();

            // act
            var book = new Book("Книга", 300, "123", bookType, publisher, 2024,
                new HashSet<Manuscript> { manuscript }, seria: seria);

            // assert
            Assert.That(seria.Books, Contains.Item(book));
        }

        [Test]
        public void Ctor_Publisher_AddsBookToPublisher()
        {
            // arrange
            var publisher = CreatePublisher();
            var bookType = CreateBookType();
            var manuscript = CreateManuscript();

            // act
            var book = new Book("Книга", 300, "123", bookType, publisher, 2024,
                new HashSet<Manuscript> { manuscript });

            // assert
            Assert.That(publisher.Books, Contains.Item(book));
        }

        [Test]
        public void Ctor_Manuscripts_AddsBookToManuscripts()
        {
            // arrange
            var publisher = CreatePublisher();
            var bookType = CreateBookType();
            var manuscript1 = CreateManuscript("Произведение 1");
            var manuscript2 = CreateManuscript("Произведение 2");
            var manuscripts = new HashSet<Manuscript> { manuscript1, manuscript2 };

            // act
            var book = new Book("Книга", 300, "123", bookType, publisher, 2024, manuscripts);

            // assert
            Assert.That(manuscript1.Books, Contains.Item(book));
            Assert.That(manuscript2.Books, Contains.Item(book));
        }

        [Test]
        public void AddEditor_ValidEditor_AddsBookToEditor()
        {
            // arrange
            var book = CreateMinimalBook();
            var editor = new Editor(new Person(new Name("Новый", "Редактор")));

            // act
            var result = book.AddEditor(editor);

            // assert
            Assert.That(result, Is.True);
            Assert.That(book.Editor, Is.SameAs(editor));
            Assert.That(editor.Books, Contains.Item(book));
        }

        [Test]
        public void AddEditor_NullEditor_ReturnsFalse()
        {
            // arrange
            var book = CreateMinimalBook();

            // act
            var result = book.AddEditor(null!);

            // assert
            Assert.That(result, Is.False);
            Assert.That(book.Editor, Is.Null);
        }

        [Test]
        public void RemoveEditor_ExistingEditor_RemovesBookFromEditor()
        {
            // arrange
            var editor = new Editor(new Person(new Name("Редактор", "Тестовый")));
            var book = CreateMinimalBook();
            book.AddEditor(editor);

            // act
            var result = book.RemoveEditor(editor);

            // assert
            using (Assert.EnterMultipleScope())
            {
                Assert.That(result, Is.True);
                Assert.That(book.Editor, Is.Null);
                Assert.That(editor.Books, Does.Not.Contain(book));
            }
        }

        [Test]
        public void RemoveEditor_NullEditor_ReturnsFalse()
        {
            // arrange
            var book = CreateMinimalBook();

            // act
            var result = book.RemoveEditor(null!);

            // assert
            Assert.That(result, Is.False);
        }

        #endregion

        #region Equality tests

        [Test]
        public void Equals_SameReference_ReturnsTrue()
        {
            // arrange
            var book = CreateMinimalBook();

            // act & assert
            Assert.That(book.Equals(book), Is.True);
        }

        [Test]
        public void Equals_Null_ReturnsFalse()
        {
            // arrange
            var book = CreateMinimalBook();

            // act & assert
            Assert.That(book.Equals(null), Is.False);
        }

        [Test]
        public void Equals_DifferentType_ReturnsFalse()
        {
            // arrange
            var book = CreateMinimalBook();

            // act & assert
            Assert.That(book.Equals("not a book"), Is.False);
        }

        [Test]
        public void Equals_SameTitle_ReturnsTrue()
        {
            // arrange
            var publisher = CreatePublisher();
            var bookType = CreateBookType();
            var manuscript = CreateManuscript();

            var book1 = new Book("Одинаковое название", 300, "111", bookType, publisher, 2024, new HashSet<Manuscript> { manuscript });
            var book2 = new Book("Одинаковое название", 400, "222", bookType, publisher, 2024, new HashSet<Manuscript> { manuscript });

            // act & assert
            Assert.That(book1.Equals(book2), Is.True);
        }

        [Test]
        public void Equals_DifferentTitle_ReturnsFalse()
        {
            // arrange
            var publisher = CreatePublisher();
            var bookType = CreateBookType();
            var manuscript = CreateManuscript();

            var book1 = new Book("Книга 1", 300, "111", bookType, publisher, 2024, new HashSet<Manuscript> { manuscript });
            var book2 = new Book("Книга 2", 300, "111", bookType, publisher, 2024, new HashSet<Manuscript> { manuscript });

            // act & assert
            Assert.That(book1.Equals(book2), Is.False);
        }

        [Test]
        public void GetHashCode_SameTitle_DifferentHashCode()
        {
            // arrange
            var publisher = CreatePublisher();
            var bookType = CreateBookType();
            var manuscript = CreateManuscript();

            var book1 = new Book("Одинаковое название", 300, "111", bookType, publisher, 2024, new HashSet<Manuscript> { manuscript });
            var book2 = new Book("Одинаковое название", 400, "222", bookType, publisher, 2024, new HashSet<Manuscript> { manuscript });

            // act & assert
            Assert.That(book1.GetHashCode(), Is.Not.EqualTo(book2.GetHashCode()));
        }

        #endregion

        #region ToString tests

        [Test]
        public void ToString_WithTitleAndManuscripts_ReturnsFormattedString()
        {
            // arrange
            var publisher = CreatePublisher();
            var bookType = CreateBookType();
            var author = CreateAuthor("Толстой", "Лев");
            var manuscript = CreateManuscript("Война и мир", author);

            var book = new Book("Классика", 1000, "123", bookType, publisher, 1869, new HashSet<Manuscript> { manuscript });

            // act
            var result = book.ToString();

            // assert
            Assert.That(result, Does.Contain("Классика"));
            Assert.That(result, Does.Contain("Война и мир"));
            Assert.That(result, Does.Contain("Толстой"));
        }

        [Test]
        public void ToString_WithoutTitle_ReturnsManuscriptsOnly()
        {
            // arrange
            var publisher = CreatePublisher();
            var bookType = CreateBookType();
            var manuscript = CreateManuscript("Без названия");

            var book = new Book(null, 200, "456", bookType, publisher, 2024, new HashSet<Manuscript> { manuscript });

            // act
            var result = book.ToString();

            // assert
            Assert.That(result, Does.Not.StartWith(" "));
            Assert.That(result, Does.Contain("Без названия"));
        }

        [Test]
        public void ToString_EmptyManuscripts_ReturnsTitleOnly()
        {
            // arrange
            var publisher = CreatePublisher();
            var bookType = CreateBookType();

            var book = new Book("Только название", 100, "789", bookType, publisher, 2024, new HashSet<Manuscript>());

            // act
            var result = book.ToString();

            // assert
            Assert.That(result, Is.EqualTo("Только название "));
        }

        #endregion

        #region Helper methods

        private static Book CreateMinimalBook()
        {
            return new Book(
                "Минимальная книга",
                100,
                "000",
                CreateBookType(),
                CreatePublisher(),
                2024,
                new HashSet<Manuscript> { CreateManuscript() });
        }

        #endregion
    }
}