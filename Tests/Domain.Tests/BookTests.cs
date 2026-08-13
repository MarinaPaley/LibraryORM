// <copyright file="BookTests.cs" company="Филипченко Марина Алексеевна">
// Copyright (c) Филипченко Марина Алексеевна 2026. Library.
// </copyright>

namespace Domain.Tests
{
    using System;
    using System.Collections.Generic;
    using Domain;
    using NUnit.Framework;
    using TestDataProvider;

    /// <summary>
    /// Модульные тесты для класса <see cref="Book"/>.
    /// </summary>
    [TestFixture]
    internal sealed class BookTests
    {
        // ==========================================
        // Конструктор: валидация данных
        // ==========================================

        /// <summary>
        /// Проверяет, что конструктор <see cref="Book"/> успешно создает объект с валидными данными.
        /// </summary>
        [Test]
        public void Ctor_ValidData_Success()
        {
            // Arrange
            var publisher = TestData.ValidPublisher().Build();
            var bookType = TestData.ValidBookType().Build();
            var manuscript = TestData.ValidManuscript().Build();

            // Act & Assert
            Assert.DoesNotThrow(() =>
            {
                _ = new Book(
                    "Тестовая книга",
                    300,
                    bookType,
                    publisher,
                    2024,
                    new HashSet<Manuscript> { manuscript, },
                    "978-5-123456-78-9");
            });
        }

        /// <summary>
        /// Проверяет, что конструктор <see cref="Book"/> допускает передачу null в качестве названия.
        /// </summary>
        [Test]
        public void Ctor_NullTitle_Allowed()
        {
            // Arrange
            var publisher = TestData.ValidPublisher().Build();
            var bookType = TestData.ValidBookType().Build();
            var manuscript = TestData.ValidManuscript().Build();

            // Act
            var book = new Book(
                null,
                300,
                bookType,
                publisher,
                2024,
                new HashSet<Manuscript> { manuscript, },
                "978-5-123456-78-9");

            // Assert
            Assert.That(book.Title, Is.Null);
        }

        /// <summary>
        /// Проверяет, что конструктор <see cref="Book"/> допускает передачу null в качестве ISBN.
        /// </summary>
        [Test]
        public void Ctor_NullISBN_DoesNotThrow()
        {
            // Arrange
            var publisher = TestData.ValidPublisher().Build();
            var bookType = TestData.ValidBookType().Build();
            var manuscript = TestData.ValidManuscript().Build();

            // Act & Assert
            Assert.DoesNotThrow(() =>
                _ = new Book(
                    "Книга",
                    300,
                    bookType,
                    publisher,
                    2024,
                    new HashSet<Manuscript> { manuscript, }));
        }

        /// <summary>
        /// Проверяет, что конструктор <see cref="Book"/> корректно обрабатывает пустую строку ISBN после обрезки пробелов.
        /// </summary>
        [Test]
        public void Ctor_EmptyISBN_AfterTrim_DoesNotThrow()
        {
            // Arrange
            var publisher = TestData.ValidPublisher().Build();
            var bookType = TestData.ValidBookType().Build();
            var manuscript = TestData.ValidManuscript().Build();

            // Act & Assert
            Assert.DoesNotThrow(
                () =>
                _ = new Book(
                    "Книга",
                    300,
                    bookType,
                    publisher,
                    2024,
                    new HashSet<Manuscript> { manuscript, }),
                "   ");
        }

        /// <summary>
        /// Проверяет, что конструктор <see cref="Book"/> выбрасывает исключение при передаче null вместо издателя.
        /// </summary>
        [Test]
        public void Ctor_NullPublisher_ThrowsArgumentNullException()
        {
            // Arrange
            var bookType = TestData.ValidBookType().Build();
            var manuscript = TestData.ValidManuscript().Build();

            // Act & Assert
            Assert.Throws<ArgumentNullException>(
                () =>
                _ = new Book(
                    "Книга",
                    300,
                    bookType,
                    null!,
                    2024,
                    new HashSet<Manuscript> { manuscript, }),
                "123");
        }

        /// <summary>
        /// Проверяет, что конструктор <see cref="Book"/> выбрасывает исключение при передаче null вместо типа книги.
        /// </summary>
        [Test]
        public void Ctor_NullBookType_ThrowsArgumentNullException()
        {
            // Arrange
            var publisher = TestData.ValidPublisher().Build();
            var manuscript = TestData.ValidManuscript().Build();

            // Act & Assert
            Assert.Throws<ArgumentNullException>(
                () =>
                _ = new Book(
                    "Книга",
                    300,
                    null!,
                    publisher,
                    2024,
                    new HashSet<Manuscript> { manuscript, }),
                "123");
        }

        /// <summary>
        /// Проверяет, что конструктор <see cref="Book"/> выбрасывает исключение при передаче null вместо коллекции рукописей.
        /// </summary>
        [Test]
        public void Ctor_NullManuscripts_ThrowsArgumentNullException()
        {
            // Arrange
            var publisher = TestData.ValidPublisher().Build();
            var bookType = TestData.ValidBookType().Build();

            // Act & Assert
            Assert.Throws<ArgumentNullException>(() =>
                _ = new Book(
                    "Книга",
                    300,
                    bookType,
                    publisher,
                    2024,
                    null!));
        }

        /// <summary>
        /// Проверяет, что конструктор <see cref="Book"/> выбрасывает исключение при неположительном количестве страниц.
        /// </summary>
        /// <param name="pages"> Количество страниц. </param>
        [TestCase(0)]
        [TestCase(-1)]
        [TestCase(-100)]
        public void Ctor_NegativeOrZeroPages_ThrowsArgumentOutOfRangeException(int pages)
        {
            // Arrange
            var publisher = TestData.ValidPublisher().Build();
            var bookType = TestData.ValidBookType().Build();
            var manuscript = TestData.ValidManuscript().Build();

            // Act & Assert
            Assert.Throws<ArgumentOutOfRangeException>(() =>
                _ = new Book(
                    "Книга",
                    pages,
                    bookType,
                    publisher,
                    2024,
                    new HashSet<Manuscript> { manuscript, },
                    "123"));
        }

        /// <summary>
        /// Проверяет, что конструктор <see cref="Book"/> выбрасывает исключение при неположительном годе издания.
        /// </summary>
        /// <param name="year"> Год издания. </param>
        [TestCase(0)]
        [TestCase(-1)]
        public void Ctor_NegativeOrZeroYear_ThrowsArgumentOutOfRangeException(int year)
        {
            // Arrange
            var publisher = TestData.ValidPublisher().Build();
            var bookType = TestData.ValidBookType().Build();
            var manuscript = TestData.ValidManuscript().Build();

            // Act & Assert
            Assert.Throws<ArgumentOutOfRangeException>(
                () =>
                _ = new Book(
                    "Книга",
                    300,
                    bookType,
                    publisher,
                    year,
                    new HashSet<Manuscript> { manuscript, },
                    "123"));
        }

        /// <summary>
        /// Проверяет, что конструктор <see cref="Book"/> выбрасывает исключение, если год издания больше текущего.
        /// </summary>
        [Test]
        public void Ctor_YearGreaterThanCurrent_ThrowsArgumentOutOfRangeException()
        {
            // Arrange
            var publisher = TestData.ValidPublisher().Build();
            var bookType = TestData.ValidBookType().Build();
            var manuscript = TestData.ValidManuscript().Build();
            var futureYear = DateTime.Now.Year + 1;

            // Act & Assert
            Assert.Throws<ArgumentOutOfRangeException>(() =>
                _ = new Book(
                    "Книга",
                    300,
                    bookType,
                    publisher,
                    futureYear,
                    new HashSet<Manuscript> { manuscript, }));
        }

        /// <summary>
        /// Проверяет, что конструктор <see cref="Book"/> выбрасывает исключение при отрицательном или нулевом томе.
        /// </summary>
        /// <param name="volume"> Номер тома. </param>
        [TestCase(0)]
        [TestCase(-1)]
        public void Ctor_NegativeVolume_ThrowsArgumentOutOfRangeException(int? volume)
        {
            // Arrange
            var publisher = TestData.ValidPublisher().Build();
            var bookType = TestData.ValidBookType().Build();
            var manuscript = TestData.ValidManuscript().Build();

            // Act & Assert
            Assert.Throws<ArgumentOutOfRangeException>(() =>
                _ = new Book(
                    "Книга",
                    300,
                    bookType,
                    publisher,
                    2024,
                    new HashSet<Manuscript> { manuscript, },
                    volume: volume));
        }

        /// <summary>
        /// Проверяет, что конструктор <see cref="Book"/> допускает передачу null в качестве тома.
        /// </summary>
        [Test]
        public void Ctor_NullVolume_Allowed()
        {
            // Arrange
            var publisher = TestData.ValidPublisher().Build();
            var bookType = TestData.ValidBookType().Build();
            var manuscript = TestData.ValidManuscript().Build();
            var manuscripts = new HashSet<Manuscript> { manuscript, };

            // Act
            var book = new Book(
                "Книга",
                300,
                bookType,
                publisher,
                2024,
                manuscripts,
                volume: null);

            // Assert
            Assert.That(book.Volume, Is.Null);
        }

        // ==========================================
        // Двусторонние связи (при создании)
        // ==========================================

        /// <summary>
        /// Проверяет, что добавление экземпляра книги на полку корректно обновляет коллекцию полки.
        /// </summary>
        [Test]
        public void AddBook_ToShelf_AddsItemToShelfCollection()
        {
            // Arrange
            var shelf = TestData.ValidShelf().WithName("A1").Build();
            var publisher = TestData.ValidPublisher().Build();
            var bookType = TestData.ValidBookType().Build();
            var manuscript = TestData.ValidManuscript().Build();
            var manuscripts = new HashSet<Manuscript> { manuscript, };
            var book = new Book("Книга", 300,  bookType, publisher, 2024, manuscripts);
            var item = new Item(book);

            // Act
            shelf.AddBook(item);

            // Assert
            Assert.That(shelf.Items, Contains.Item(item));
        }

        /// <summary>
        /// Проверяет, что передача редактора в конструктор <see cref="Book"/> устанавливает двустороннюю связь.
        /// </summary>
        [Test]
        public void Ctor_Editor_AddsBookToEditor()
        {
            // Arrange
            var person = TestData.ValidPerson().WithFirstName("Редактор").WithFamilyName("Тестовый").Build();
            var editor = TestData.ValidEditor().WithPerson(person).Build();
            var publisher = TestData.ValidPublisher().Build();
            var bookType = TestData.ValidBookType().Build();
            var manuscript = TestData.ValidManuscript().Build();
            var manuscripts = new HashSet<Manuscript> { manuscript, };

            // Act
            var book = new Book(
                "Книга",
                300,
                bookType,
                publisher,
                2024,
                manuscripts,
                editor: editor);

            // Assert
            Assert.That(editor.Books, Contains.Item(book));
        }

        /// <summary>
        /// Проверяет, что передача художника в конструктор <see cref="Book"/> устанавливает двустороннюю связь.
        /// </summary>
        [Test]
        public void Ctor_Illustrator_AddsBookToIllustrator()
        {
            // Arrange
            var person = TestData.ValidPerson().WithFirstName("Редактор").WithFamilyName("Тестовый").Build();
            var editor = TestData.ValidEditor().WithPerson(person).Build();
            var illustrator = TestData.ValidIllustrator().WithPerson(person).Build();
            var publisher = TestData.ValidPublisher().Build();
            var bookType = TestData.ValidBookType().Build();
            var manuscript = TestData.ValidManuscript().Build();
            var manuscripts = new HashSet<Manuscript> { manuscript, };

            // Act
            var book = new Book(
                "Книга",
                300,
                bookType,
                publisher,
                2024,
                manuscripts,
                editor: editor,
                illustrator: illustrator);

            // Assert
            Assert.That(illustrator.Books, Contains.Item(book));
        }

        /// <summary>
        /// Проверяет, что передача серии в конструктор <see cref="Book"/> устанавливает двустороннюю связь.
        /// </summary>
        [Test]
        public void Ctor_Seria_AddsBookToSeria()
        {
            // Arrange
            var seria = TestData.ValidSeria().WithName("Научная серия").Build();
            var publisher = TestData.ValidPublisher().Build();
            var bookType = TestData.ValidBookType().Build();
            var manuscript = TestData.ValidManuscript().Build();
            var manuscripts = new HashSet<Manuscript> { manuscript, };

            // Act
            var book = new Book(
                "Книга",
                300,
                bookType,
                publisher,
                2024,
                manuscripts,
                seria: seria);

            // Assert
            Assert.That(seria.Books, Contains.Item(book));
        }

        /// <summary>
        /// Проверяет, что передача издателя в конструктор <see cref="Book"/> устанавливает связь.
        /// </summary>
        [Test]
        public void Ctor_Publisher_AddsBookToPublisher()
        {
            // Arrange
            var publisher = TestData.ValidPublisher().Build();
            var bookType = TestData.ValidBookType().Build();
            var manuscript = TestData.ValidManuscript().Build();
            var manuscripts = new HashSet<Manuscript> { manuscript, };

            // Act
            var book = new Book("Книга", 300, bookType, publisher, 2024, manuscripts);

            // Assert
            Assert.That(publisher.Books, Contains.Item(book));
        }

        /// <summary>
        /// Проверяет, что передача рукописей в конструктор <see cref="Book"/> устанавливает двустороннюю связь.
        /// </summary>
        [Test]
        public void Ctor_Manuscripts_AddsBookToManuscripts()
        {
            // Arrange
            var publisher = TestData.ValidPublisher().Build();
            var bookType = TestData.ValidBookType().Build();
            var manuscript1 = TestData.ValidManuscript().WithName("Произведение 1").Build();
            var manuscript2 = TestData.ValidManuscript().WithName("Произведение 2").Build();
            var manuscripts = new HashSet<Manuscript> { manuscript1, manuscript2, };

            // Act
            var book = new Book("Книга", 300, bookType, publisher, 2024, manuscripts);

            // Assert
            using (Assert.EnterMultipleScope())
            {
                Assert.That(manuscript1.Books, Contains.Item(book));
                Assert.That(manuscript2.Books, Contains.Item(book));
            }
        }

        // ==========================================
        // Методы AddEditor / RemoveEditor
        // ==========================================

        /// <summary>
        /// Проверяет успешное добавление редактора к существующей книге.
        /// </summary>
        [Test]
        public void AddEditor_ValidEditor_AddsBookToEditor()
        {
            // Arrange
            var book = TestData.ValidBook().Build();
            var person = TestData.ValidPerson().WithFirstName("Редактор").WithFamilyName("Тестовый").Build();
            var editor = TestData.ValidEditor().WithPerson(person).Build();

            // Act
            var result = book.AddEditor(editor);

            // Assert
            using (Assert.EnterMultipleScope())
            {
                Assert.That(result, Is.True);
                Assert.That(book.Editor, Is.SameAs(editor));
                Assert.That(editor.Books, Contains.Item(book));
            }
        }

        /// <summary>
        /// Проверяет, что попытка добавить null в качестве редактора возвращает false.
        /// </summary>
        [Test]
        public void AddEditor_NullEditor_ReturnsFalse()
        {
            // Arrange
            var book = TestData.ValidBook().Build();

            // Act
            var result = book.AddEditor(null!);

            // Assert
            using (Assert.EnterMultipleScope())
            {
                Assert.That(result, Is.False);
                Assert.That(book.Editor, Is.Null);
            }
        }

        /// <summary>
        /// Проверяет успешное удаление редактора у книги.
        /// </summary>
        [Test]
        public void RemoveEditor_ExistingEditor_RemovesBookFromEditor()
        {
            // Arrange
            var person = TestData.ValidPerson().WithFirstName("Редактор").WithFamilyName("Тестовый").Build();
            var editor = TestData.ValidEditor().WithPerson(person).Build();
            var book = TestData.ValidBook().Build();
            book.AddEditor(editor);

            // Act
            var result = book.RemoveEditor(editor);

            // Assert
            using (Assert.EnterMultipleScope())
            {
                Assert.That(result, Is.True);
                Assert.That(book.Editor, Is.Null);
                Assert.That(editor.Books, Does.Not.Contain(book));
            }
        }

        /// <summary>
        /// Проверяет, что попытка удалить null в качестве редактора возвращает false.
        /// </summary>
        [Test]
        public void RemoveEditor_NullEditor_ReturnsFalse()
        {
            // Arrange
            var book = TestData.ValidBook().Build();

            // Act
            var result = book.RemoveEditor(null!);

            // Assert
            Assert.That(result, Is.False);
        }

        // ==========================================
        // Equals: базовые проверки
        // ==========================================

        /// <summary>
        /// Проверяет, что сравнение книги с самой собой возвращает true.
        /// </summary>
        [Test]
        public void Equals_SameReference_ReturnsTrue()
        {
            // Arrange
            var book = TestData.ValidBook().Build();

            // Act & Assert
            Assert.That(book.Equals(book), Is.True);
        }

        /// <summary>
        /// Проверяет, что сравнение книги с null возвращает false.
        /// </summary>
        [Test]
        public void Equals_Null_ReturnsFalse()
        {
            // Arrange
            var book = TestData.ValidBook().Build();

            // Act & Assert
            Assert.That(book.Equals(null), Is.False);
        }

        /// <summary>
        /// Проверяет, что сравнение книги с объектом другого типа возвращает false.
        /// </summary>
        [Test]
        public void Equals_DifferentType_ReturnsFalse()
        {
            // Arrange
            var book = TestData.ValidBook().Build();

            // Act & Assert
            Assert.That(book.Equals("not a book"), Is.False);
        }

        // ==========================================
        // Equals: логика сравнения с учётом ISBN
        // ==========================================

        /// <summary>
        /// Проверяет, что книги с одинаковым ISBN считаются равными, даже если остальные поля отличаются.
        /// </summary>
        [Test]
        public void Equals_SameIsbn_ReturnsTrue()
        {
            // Arrange
            var publisher1 = TestData.ValidPublisher().WithName("Издательство 1").Build();
            var publisher2 = TestData.ValidPublisher().WithName("Издательство 2").Build();
            var bookType = TestData.ValidBookType().Build();
            var manuscript = TestData.ValidManuscript().Build();
            var manuscripts = new HashSet<Manuscript> { manuscript, };

            var book1 = new Book("Книга 1", 300, bookType, publisher1, 2024, manuscripts, "978-123");
            var book2 = new Book("Книга 2", 400, bookType, publisher2, 2025, manuscripts, "978-123");

            // Act & Assert
            Assert.That(book1.Equals(book2), Is.True);
            Assert.That(book1.GetHashCode(), Is.EqualTo(book2.GetHashCode()));
        }

        /// <summary>
        /// Проверяет, что книги с разными ISBN считаются разными, даже если остальные поля совпадают.
        /// </summary>
        [Test]
        public void Equals_DifferentIsbn_ReturnsFalse()
        {
            // Arrange
            var publisher = TestData.ValidPublisher().Build();
            var bookType = TestData.ValidBookType().Build();
            var manuscript = TestData.ValidManuscript().Build();
            var manuscripts = new HashSet<Manuscript> { manuscript, };

            var book1 = new Book("Одинаковое название", 300, bookType, publisher, 2024, manuscripts, "978-111");
            var book2 = new Book("Одинаковое название", 300, bookType, publisher, 2024, manuscripts, "978-222");

            // Act & Assert
            Assert.That(book1.Equals(book2), Is.False);
        }

        /// <summary>
        /// Проверяет, что книги без ISBN с одинаковыми остальными полями считаются равными.
        /// </summary>
        [Test]
        public void Equals_BothWithoutIsbn_SameOtherFields_ReturnsTrue()
        {
            // Arrange
            var publisher = TestData.ValidPublisher().Build();
            var bookType = TestData.ValidBookType().Build();
            var manuscript = TestData.ValidManuscript().Build();
            var manuscripts = new HashSet<Manuscript> { manuscript, };

            var book1 = new Book("Одинаковое название", 300, bookType, publisher, 2024, manuscripts);
            var book2 = new Book("Одинаковое название", 300, bookType, publisher, 2024, manuscripts);

            using (Assert.EnterMultipleScope())
            {
                // Act & Assert
                Assert.That(book1.Equals(book2), Is.True);
                Assert.That(book1.GetHashCode(), Is.EqualTo(book2.GetHashCode()));
            }
        }

        /// <summary>
        /// Проверяет, что если ISBN задан только у одной книги, они считаются разными.
        /// </summary>
        [Test]
        public void Equals_OneWithIsbnOneWithout_SameOtherFields_ReturnsFalse()
        {
            // Arrange
            var publisher = TestData.ValidPublisher().Build();
            var bookType = TestData.ValidBookType().Build();
            var manuscript = TestData.ValidManuscript().Build();
            var manuscripts = new HashSet<Manuscript> { manuscript, };

            var book1 = new Book("Одинаковое название", 300, bookType, publisher, 2024, manuscripts, "978-123");
            var book2 = new Book("Одинаковое название", 300, bookType, publisher, 2024, manuscripts);

            // Act & Assert
            Assert.That(book1.Equals(book2), Is.False);
        }

        /// <summary>
        /// Проверяет, что книги без ISBN с разными названиями считаются разными.
        /// </summary>
        [Test]
        public void Equals_BothWithoutIsbn_DifferentTitle_ReturnsFalse()
        {
            // Arrange
            var publisher = TestData.ValidPublisher().Build();
            var bookType = TestData.ValidBookType().Build();
            var manuscript = TestData.ValidManuscript().Build();
            var manuscripts = new HashSet<Manuscript> { manuscript, };

            var book1 = new Book("Книга 1", 300, bookType, publisher, 2024, manuscripts);
            var book2 = new Book("Книга 2", 300, bookType, publisher, 2024, manuscripts);

            // Act & Assert
            Assert.That(book1.Equals(book2), Is.False);
        }

        // ==========================================
        // GetHashCode
        // ==========================================

        /// <summary>
        /// Проверяет, что книги с одинаковым ISBN имеют одинаковый хеш-код.
        /// </summary>
        [Test]
        public void GetHashCode_SameIsbn_SameHashCode()
        {
            // Arrange
            var publisher = TestData.ValidPublisher().Build();
            var bookType = TestData.ValidBookType().Build();
            var manuscript = TestData.ValidManuscript().Build();
            var manuscripts = new HashSet<Manuscript> { manuscript, };

            var book1 = new Book("Книга 1", 300, bookType, publisher, 2024, manuscripts, "978-123");
            var book2 = new Book("Книга 2", 400, bookType, publisher, 2025, manuscripts, "978-123");

            // Act & Assert
            Assert.That(book1.GetHashCode(), Is.EqualTo(book2.GetHashCode()));
        }

        /// <summary>
        /// Проверяет, что книги без ISBN с одинаковыми полями имеют одинаковый хеш-код.
        /// </summary>
        [Test]
        public void GetHashCode_BothWithoutIsbn_SameOtherFields_SameHashCode()
        {
            // Arrange
            var publisher = TestData.ValidPublisher().Build();
            var bookType = TestData.ValidBookType().Build();
            var manuscript = TestData.ValidManuscript().Build();
            var manuscripts = new HashSet<Manuscript> { manuscript, };

            var book1 = new Book("Одинаковое название", 300, bookType, publisher, 2024, manuscripts);
            var book2 = new Book("Одинаковое название", 400, bookType, publisher, 2024, manuscripts);

            // Act & Assert
            Assert.That(book1.GetHashCode(), Is.EqualTo(book2.GetHashCode()));
        }

        /// <summary>
        /// Проверяет, что книги с разными ISBN имеют разные хеш-коды.
        /// </summary>
        [Test]
        public void GetHashCode_DifferentIsbn_DifferentHashCode()
        {
            // Arrange
            var publisher = TestData.ValidPublisher().Build();
            var bookType = TestData.ValidBookType().Build();
            var manuscript = TestData.ValidManuscript().Build();
            var manuscripts = new HashSet<Manuscript> { manuscript, };

            var book1 = new Book("Одинаковое название", 300, bookType, publisher, 2024, manuscripts, "978-123");
            var book2 = new Book("Одинаковое название", 300, bookType, publisher, 2024, manuscripts, "978-111");

            // Act & Assert
            Assert.That(book1.GetHashCode(), Is.Not.EqualTo(book2.GetHashCode()));
        }

        // ==========================================
        // ToString
        // ==========================================

        /// <summary>
        /// Проверяет, что метод ToString возвращает отформатированную строку с названием книги и рукописями.
        /// </summary>
        [Test]
        public void ToString_WithTitleAndManuscripts_ReturnsFormattedString()
        {
            // Arrange
            var publisher = TestData.ValidPublisher().Build();
            var bookType = TestData.ValidBookType().Build();
            var person = TestData.ValidPerson()
                .WithFirstName("Лев")
                .WithFamilyName("Толстой")
                .Build();
            var author = TestData.ValidAuthor().WithPerson(person).Build();
            var authors = new HashSet<Author> { author, };
            var manuscript = TestData.ValidManuscript()
                .WithName("Война и мир")
                .WithAuthors(authors)
                .Build();

            var manuscripts = new HashSet<Manuscript> { manuscript, };

            var book = new Book("Классика", 1000, bookType, publisher, 1869, manuscripts);

            // Act
            var result = book.ToString();

            // Assert
            using (Assert.EnterMultipleScope())
            {
                Assert.That(result, Does.Contain("Классика"));
                Assert.That(result, Does.Contain("Война и мир"));
                Assert.That(result, Does.Contain("Толстой"));
            }
        }

        /// <summary>
        /// Проверяет, что метод ToString возвращает только рукописи, если название книги отсутствует.
        /// </summary>
        [Test]
        public void ToString_WithoutTitle_ReturnsManuscriptsOnly()
        {
            // Arrange
            var publisher = TestData.ValidPublisher().Build();
            var bookType = TestData.ValidBookType().Build();
            var manuscript = TestData.ValidManuscript().WithName("Без названия").Build();
            var manuscripts = new HashSet<Manuscript> { manuscript, };

            var book = new Book(null, 200, bookType, publisher, 2024, manuscripts);

            // Act
            var result = book.ToString();

            // Assert
            using (Assert.EnterMultipleScope())
            {
                Assert.That(result, Does.Not.StartWith(" "));
                Assert.That(result, Does.Contain("Без названия"));
            }
        }

        /// <summary>
        /// Проверяет, что метод ToString возвращает только название, если коллекция рукописей пуста.
        /// </summary>
        [Test]
        public void ToString_EmptyManuscripts_ReturnsTitleOnly()
        {
            // Arrange
            var publisher = TestData.ValidPublisher().Build();
            var bookType = TestData.ValidBookType().Build();
            var manuscripts = new HashSet<Manuscript>();

            var book = new Book("Только название", 100, bookType, publisher, 2024, manuscripts);

            // Act
            var result = book.ToString();

            // Assert
            Assert.That(result, Is.EqualTo("Только название "));
        }

        // ==========================================
        // Тесты свойства Quality (enum)
        // ==========================================

        /// <summary>
        /// Проверяет, что конструктор <see cref="Book"/> по умолчанию устанавливает качество "Типография".
        /// </summary>
        [Test]
        public void Ctor_DefaultQuality_SetsPrintingHouse()
        {
            // Arrange
            var publisher = TestData.ValidPublisher().Build();
            var bookType = TestData.ValidBookType().Build();
            var manuscript = TestData.ValidManuscript().Build();
            var manuscripts = new HashSet<Manuscript> { manuscript, };

            // Act
            var book = new Book("Книга", 300, bookType, publisher, 2024, manuscripts);

            // Assert
            Assert.That(book.Quality, Is.EqualTo(PrintQuality.PrintingHouse));
        }

        /// <summary>
        /// Проверяет, что конструктор <see cref="Book"/> корректно устанавливает указанное качество.
        /// </summary>
        [Test]
        public void Ctor_ExplicitQuality_SetsSpecifiedValue()
        {
            // Arrange
            var publisher = TestData.ValidPublisher().Build();
            var bookType = TestData.ValidBookType().Build();
            var manuscript = TestData.ValidManuscript().Build();
            var manuscripts = new HashSet<Manuscript> { manuscript, };

            // Act
            var book = new Book(
                "Книга",
                300,
                bookType,
                publisher,
                2024,
                manuscripts,
                quality: PrintQuality.SelfPublished);

            // Assert
            Assert.That(book.Quality, Is.EqualTo(PrintQuality.SelfPublished));
        }

        /// <summary>
        /// Проверяет, что качество книги можно изменить после создания.
        /// </summary>
        [Test]
        public void Quality_CanBeChangedAfterCreation()
        {
            // Arrange
            var book = TestData.ValidBook().Build();

            // Act
            book.Quality = PrintQuality.Manuscript;

            // Assert
            Assert.That(book.Quality, Is.EqualTo(PrintQuality.Manuscript));
        }

        /// <summary>
        /// Проверяет, что книги с одинаковым качеством имеют одинаковое значение свойства.
        /// </summary>
        [Test]
        public void Quality_SameValue_ReturnsEqual()
        {
            // Arrange
            var book1 = TestData.ValidBook().WithQuality(PrintQuality.PrintingHouse).Build();
            var book2 = TestData.ValidBook().WithQuality(PrintQuality.PrintingHouse).Build();

            // Act & Assert
            Assert.That(book1.Quality, Is.EqualTo(book2.Quality));
        }

        /// <summary>
        /// Проверяет, что книги с разным качеством имеют разные значения свойства.
        /// </summary>
        [Test]
        public void Quality_DifferentValue_ReturnsNotEqual()
        {
            // Arrange
            var book1 = TestData.ValidBook().WithQuality(PrintQuality.PrintingHouse).Build();
            var book2 = TestData.ValidBook().WithQuality(PrintQuality.SelfPublished).Build();

            // Act & Assert
            Assert.That(book1.Quality, Is.Not.EqualTo(book2.Quality));
        }
    }
}