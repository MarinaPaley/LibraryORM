// <copyright file="ShelfTests.cs" company="Филипченко Марина Алексеевна">
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
    /// Модульные тесты для класса <see cref="Shelf"/>.
    /// </summary>
    [TestFixture]
    public sealed class ShelfTests
    {
        /// <summary>
        /// Проверяет, что конструктор <see cref="Shelf"/> успешно создает объект с валидными данными.
        /// </summary>
        [Test]
        public void Ctor_ValidData_Success()
        {
            // Act & Assert
            Assert.DoesNotThrow(() => TestData.ValidShelf().WithName("Полка 1").Build());
        }

        /// <summary>
        /// Проверяет, что конструктор <see cref="Shelf"/> выбрасывает исключение при передаче null.
        /// </summary>
        [Test]
        public void Ctor_NullData_ExpectedException()
        {
            // Act & Assert
            Assert.Throws<ArgumentNullException>(() => new Shelf(null!));
        }

        /// <summary>
        /// Проверяет логику равенства двух экземпляров <see cref="Shelf"/>.
        /// </summary>
        /// <param name="name1"> Название первой полки. </param>
        /// <param name="name2"> Название второй полки. </param>
        /// <param name="expected"> Ожидаемый результат сравнения. </param>
        [TestCase("1", "1", true)]
        [TestCase("1", "2", false)]
        public void Equals_ValidData_Success(string name1, string name2, bool expected)
        {
            // Arrange
            var shelf1 = TestData.ValidShelf().WithName(name1).Build();
            var shelf2 = TestData.ValidShelf().WithName(name2).Build();

            // Act
            var actual = shelf1.Equals(shelf2);

            // Assert
            Assert.That(actual, Is.EqualTo(expected));
        }

        /// <summary>
        /// Проверяет, что метод ToString возвращает корректную строку для полки без книг.
        /// </summary>
        [Test]
        public void ToString_NoBook_Success()
        {
            // Arrange
            const string expected = "Полка: Полка 1";
            var shelf = TestData.ValidShelf().WithName("Полка 1").Build();

            // Act
            var actual = shelf.ToString();

            // Assert
            Assert.That(actual, Is.EqualTo(expected));
        }

        /// <summary>
        /// Проверяет, что метод ToString возвращает корректную строку для полки с книгами.
        /// </summary>
        [Test]
        public void ToString_WithBooks_Success()
        {
            // Arrange
            const string expected = "Полка: Полка 1 | Книги: Анна Каренина: [Толстой Лев Николаевич], " +
                "12 стульев: [Ильф Илья, Петров Евгений]";

            var shelf = TestData.ValidShelf().WithName("Полка 1").Build();

            // Создаем авторов через TestDataProvider
            Person tolstoy = TestData.ValidPerson()
                .WithFamilyName("Толстой")
                .WithFirstName("Лев")
                .WithPatronymicName("Николаевич");
            Author author1 = TestData.ValidAuthor().WithPerson(tolstoy);

            Person ilf = TestData.ValidPerson().WithFamilyName("Ильф").WithFirstName("Илья");
            Person petrov = TestData.ValidPerson().WithFamilyName("Петров").WithFirstName("Евгений");
            Author author2 = TestData.ValidAuthor().WithPerson(ilf);
            Author author3 = TestData.ValidAuthor().WithPerson(petrov);

            // Создаем зависимые сущности
            Language language = TestData.ValidLanguage().WithName("Русский");
            Publisher publisher = TestData.ValidPublisher().WithName("Издательство");
            BookType bookType = TestData.ValidBookType().WithName("Книга");

            // Создаем рукописи
            Manuscript manuscript1 = TestData.ValidManuscript()
                .WithName("Анна Каренина")
                .WithLanguages(new HashSet<Language> { language })
                .WithAuthors(new HashSet<Author> { author1 });

            Manuscript manuscript2 = TestData.ValidManuscript()
                .WithName("12 стульев")
                .WithLanguages(new HashSet<Language> { language })
                .WithAuthors(new HashSet<Author> { author2, author3 });

            // Создаем книги
            Book book1 = TestData.ValidBook()
                .WithTitle(null)
                .WithPages(250)
                .WithISBN("123")
                .WithBookType(bookType)
                .WithPublisher(publisher)
                .WithYear(1925)
                .WithManuscripts(new HashSet<Manuscript> { manuscript1 });

            Book book2 = TestData.ValidBook()
                .WithTitle(null)
                .WithPages(250)
                .WithISBN("12345")
                .WithBookType(bookType)
                .WithPublisher(publisher)
                .WithYear(1925)
                .WithManuscripts(new HashSet<Manuscript> { manuscript2 });

            // Создаем экземпляры книг и добавляем на полку
            var item1 = TestData.ValidItem().WithBook(book1);
            var item2 = TestData.ValidItem().WithBook(book2);

            shelf.AddBook(item1);
            shelf.AddBook(item2);

            // Act
            var actual = shelf.ToString();

            // Assert
            Assert.That(actual, Is.EqualTo(expected));
        }

        /// <summary>
        /// Проверяет успешное добавление книги на полку.
        /// </summary>
        [Test]
        public void AddBook_Book_Success()
        {
            // Arrange
            var book = TestData.ValidBook().Build();
            var shelf = TestData.ValidShelf().WithName("Полка 1").Build();
            var item = TestData.ValidItem().WithBook(book).Build();

            // Act
            shelf.AddBook(item);

            // Assert
            Assert.That(shelf.Items.Contains(item), Is.True);
        }

        /// <summary>
        /// Проверяет корректное удаление книги с полки, включая граничные случаи.
        /// </summary>
        [Test]
        public void RemoveBook_ValidData_Success()
        {
            // Arrange
            var shelf = TestData.ValidShelf().WithName("Полка 1").Build();

            Language language = TestData.ValidLanguage().WithName("Русский");
            Publisher publisher = TestData.ValidPublisher().WithName("Издательство");
            BookType bookType = TestData.ValidBookType().WithName("Книга");

            Person authorPerson = TestData.ValidPerson().WithFamilyName("Толстой").WithFirstName("Лев");
            Author author = TestData.ValidAuthor().WithPerson(authorPerson);

            Manuscript manuscript1 = TestData.ValidManuscript()
                .WithName("Анна Каренина")
                .WithLanguages(new HashSet<Language> { language })
                .WithAuthors(new HashSet<Author> { author });

            Manuscript manuscript2 = TestData.ValidManuscript()
                .WithName("12 стульев")
                .WithLanguages(new HashSet<Language> { language })
                .WithAuthors(new HashSet<Author> { author });

            Book book = TestData.ValidBook()
                .WithTitle(null)
                .WithPages(1234)
                .WithISBN("12345")
                .WithBookType(bookType)
                .WithPublisher(publisher)
                .WithYear(2026)
                .WithManuscripts(new HashSet<Manuscript> { manuscript1 });

            Book otherBook = TestData.ValidBook()
                .WithTitle(null)
                .WithPages(1234)
                .WithISBN("12345")
                .WithBookType(bookType)
                .WithPublisher(publisher)
                .WithYear(2026)
                .WithManuscripts(new HashSet<Manuscript> { manuscript2 });

            var item1 = TestData.ValidItem().WithBook(book);
            var item2 = TestData.ValidItem().WithBook(otherBook);

            shelf.AddBook(item1);

            // Act & Assert
            using (Assert.EnterMultipleScope())
            {
                Assert.That(shelf.RemoveBook(item1), Is.True, "Удаление существующей книги должно вернуть true");
                Assert.That(shelf.RemoveBook(null!), Is.False, "Удаление null должно вернуть false");
                Assert.That(shelf.RemoveBook(item2), Is.False, "Удаление книги, которой нет на полке, должно вернуть false");
            }
        }
    }
}