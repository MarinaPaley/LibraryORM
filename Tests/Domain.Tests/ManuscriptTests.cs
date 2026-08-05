// <copyright file="ManuscriptTests.cs" company="Филипченко Марина Алексеевна">
// Copyright (c) Филипченко Марина Алексеевна 2026. Library.
// </copyright>

namespace Domain.Tests
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Domain;
    using NUnit.Framework;
    using Staff;
    using TestDataProvider;

    /// <summary>
    /// Модульные тесты для класса <see cref="Manuscript"/>.
    /// </summary>
    [TestFixture]
    public sealed class ManuscriptTests
    {
        /// <summary>
        /// Проверяет, что конструктор <see cref="Manuscript"/> выбрасывает исключение при передаче null в качестве названия.
        /// </summary>
        [Test]
        public void Ctor_NullName_ThrowsArgumentNullException()
        {
            // Arrange
            var languages = new HashSet<Language> { TestData.ValidLanguage().Build(), };
            var authors = new HashSet<Author>();

            // Act & Assert
            Assert.Throws<ArgumentNullException>(() => _ = new Manuscript(null!, languages, authors));
        }

        /// <summary>
        /// Проверяет, что конструктор <see cref="Manuscript"/> выбрасывает исключение при передаче строки из пробелов в качестве названия.
        /// </summary>
        [Test]
        public void Ctor_EmptyName_AfterTrim_ThrowsArgumentNullException()
        {
            // Arrange
            var languages = new HashSet<Language> { TestData.ValidLanguage().Build(), };
            var authors = new HashSet<Author>();

            // Act & Assert
            Assert.Throws<ArgumentNullException>(() => _ = new Manuscript("   ", languages, authors));
        }

        /// <summary>
        /// Проверяет, что конструктор <see cref="Manuscript"/> выбрасывает исключение при передаче null в качестве коллекции языков.
        /// </summary>
        [Test]
        public void Ctor_NullLanguage_ThrowsArgumentNullException()
        {
            // Arrange
            var authors = new HashSet<Author>();

            // Act & Assert
            Assert.Throws<ArgumentNullException>(() => _ = new Manuscript("Название", null!, authors));
        }

        /// <summary>
        /// Проверяет, что конструктор <see cref="Manuscript"/> выбрасывает исключение при передаче null в качестве коллекции авторов.
        /// </summary>
        [Test]
        public void Ctor_NullAuthors_ThrowsArgumentNullException()
        {
            // Arrange
            var languages = new HashSet<Language> { TestData.ValidLanguage().Build(), };
            ISet<Author>? nullAuthors = null;

            // Act & Assert
            Assert.Throws<ArgumentNullException>(() =>
                _ = new Manuscript("Название", languages, nullAuthors!));
        }

        /// <summary>
        /// Проверяет, что конструктор <see cref="Manuscript"/> выбрасывает исключение при передаче пустой коллекции авторов.
        /// </summary>
        [Test]
        public void Ctor_EmptyAuthorsCollection_ThrowsArgumentOutOfRangeException()
        {
            // Arrange
            var languages = new HashSet<Language> { TestData.ValidLanguage().Build(), };
            var authors = new HashSet<Author>();

            // Act & Assert
            Assert.Throws<ArgumentOutOfRangeException>(() => _ = new Manuscript("Название", languages, authors));
        }

        /// <summary>
        /// Проверяет, что конструктор <see cref="Manuscript"/> допускает передачу null в качестве диапазона дат.
        /// </summary>
        [Test]
        public void Ctor_NullDateRange_Allowed()
        {
            // Arrange
            var languages = new HashSet<Language> { TestData.ValidLanguage().Build(), };
            var author = TestData.ValidAuthor().Build();
            var authors = new HashSet<Author> { author, };

            // Act
            var manuscript = new Manuscript("Название", languages, authors, date: null);

            // Assert
            Assert.That(manuscript.Dates, Is.Null);
        }

        /// <summary>
        /// Проверяет успешное создание <see cref="Manuscript"/> с валидными данными.
        /// </summary>
        [Test]
        public void Ctor_ValidData_Success()
        {
            // Arrange
            var languages = new HashSet<Language> { TestData.ValidLanguage().Build(), };
            var person = TestData.ValidPerson().WithFirstName("Лев").WithFamilyName("Толстой").Build();
            var author = TestData.ValidAuthor().WithPerson(person).Build();
            var authors = new HashSet<Author> { author, };
            var dateRange = new Range<DateOnly>(new DateOnly(1865, 1, 1), new DateOnly(1869, 12, 31));

            // Act
            var manuscript = new Manuscript("Война и мир", languages, authors, dateRange);

            // Assert
            using (Assert.EnterMultipleScope())
            {
                Assert.That(manuscript.Name.Value, Is.EqualTo("Война и мир"));
                Assert.That(manuscript.Authors, Contains.Item(author));
                Assert.That(manuscript.Dates?.From, Is.EqualTo(new DateOnly(1865, 1, 1)));
                Assert.That(manuscript.Dates?.To, Is.EqualTo(new DateOnly(1869, 12, 31)));
            }
        }

        /// <summary>
        /// Проверяет, что конструктор с параметрами DateOnly корректно создает диапазон дат.
        /// </summary>
        [Test]
        public void Ctor_WithDateOnlyParams_CreatesRange()
        {
            // Arrange
            var languages = new HashSet<Language> { TestData.ValidLanguage().Build(), };
            var person = TestData.ValidPerson().WithFirstName("Илья").WithFamilyName("Ильф").Build();
            var author = TestData.ValidAuthor().WithPerson(person).Build();
            var from = new DateOnly(1927, 1, 1);
            var to = new DateOnly(1928, 12, 31);

            // Act
            var manuscript = new Manuscript("12 стульев", languages, from, to, null, author);

            // Assert
            Assert.That(manuscript.Dates, Is.Not.Null);
            using (Assert.EnterMultipleScope())
            {
                Assert.That(manuscript.Dates!.From, Is.EqualTo(from));
                Assert.That(manuscript.Dates!.To, Is.EqualTo(to));
            }
        }

        /// <summary>
        /// Проверяет, что конструктор с одной датой DateOnly создает диапазон с одинаковыми границами.
        /// </summary>
        [Test]
        public void Ctor_WithSingleDateOnly_CreatesRangeWithSameBounds()
        {
            // Arrange
            var languages = new HashSet<Language> { TestData.ValidLanguage().Build(), };
            var person = TestData.ValidPerson().WithFirstName("Антон").WithFamilyName("Чехов").Build();
            var author = TestData.ValidAuthor().WithPerson(person).Build();
            var date = new DateOnly(1886, 1, 1);

            // Act
            var manuscript = new Manuscript("Хамелеон", languages, date, date, null, author);

            // Assert
            Assert.That(manuscript.Dates!.From, Is.EqualTo(manuscript.Dates!.To));
            Assert.That(manuscript.Dates!.From, Is.EqualTo(date));
        }

        /// <summary>
        /// Проверяет, что при создании рукописи она автоматически добавляется в коллекции всех переданных авторов.
        /// </summary>
        [Test]
        public void Ctor_AddsManuscriptToAuthors()
        {
            // Arrange
            var languages = new HashSet<Language> { TestData.ValidLanguage().Build(), };
            var person1 = TestData.ValidPerson().WithFirstName("Лев").WithFamilyName("Толстой").Build();
            var author1 = TestData.ValidAuthor().WithPerson(person1).Build();
            var person2 = TestData.ValidPerson().WithFirstName("Фёдор").WithFamilyName("Достоевский").Build();
            var author2 = TestData.ValidAuthor().WithPerson(person2).Build();
            var authors = new HashSet<Author> { author1, author2, };

            // Act
            var manuscript = new Manuscript("Классика", languages, authors);

            // Assert
            using (Assert.EnterMultipleScope())
            {
                Assert.That(author1.Manuscripts, Contains.Item(manuscript));
                Assert.That(author2.Manuscripts, Contains.Item(manuscript));
            }
        }

        /// <summary>
        /// Проверяет, что добавление валидного жанра устанавливает двустороннюю связь.
        /// </summary>
        [Test]
        public void AddGenre_ValidGenre_AddsToBothCollections()
        {
            // Arrange
            var manuscript = TestData.ValidManuscript().Build();
            var genre = TestData.ValidGenre().WithName("Роман").Build();

            // Act
            var result = manuscript.AddGenre(genre);

            // Assert
            using (Assert.EnterMultipleScope())
            {
                Assert.That(result, Is.True);
                Assert.That(manuscript.Genres, Contains.Item(genre));
                Assert.That(genre.Manuscripts, Contains.Item(manuscript));
            }
        }

        /// <summary>
        /// Проверяет, что попытка добавить null в качестве жанра возвращает false.
        /// </summary>
        [Test]
        public void AddGenre_NullGenre_ReturnsFalse()
        {
            // Arrange
            var manuscript = TestData.ValidManuscript().Build();

            // Act
            var result = manuscript.AddGenre(null!);

            // Assert
            using (Assert.EnterMultipleScope())
            {
                Assert.That(result, Is.False);
                Assert.That(manuscript.Genres, Is.Empty);
            }
        }

        /// <summary>
        /// Проверяет, что попытка добавить один и тот же жанр дважды возвращает false при втором вызове.
        /// </summary>
        [Test]
        public void AddGenre_DuplicateGenre_ReturnsFalse()
        {
            // Arrange
            var manuscript = TestData.ValidManuscript().Build();
            var genre = TestData.ValidGenre().WithName("Роман").Build();
            _ = manuscript.AddGenre(genre);

            // Act
            var result = manuscript.AddGenre(genre);

            // Assert
            Assert.That(result, Is.False);
            Assert.That(manuscript.Genres, Has.Count.EqualTo(1));
        }

        /// <summary>
        /// Проверяет, что удаление существующего жанра разрывает двустороннюю связь.
        /// </summary>
        [Test]
        public void RemoveGenre_ExistingGenre_RemovesFromBothCollections()
        {
            // Arrange
            var manuscript = TestData.ValidManuscript().Build();
            var genre = TestData.ValidGenre().WithName("Роман").Build();
            _ = manuscript.AddGenre(genre);

            // Act
            var result = manuscript.RemoveGenre(genre);

            // Assert
            Assert.That(result, Is.True);
            Assert.That(manuscript.Genres, Does.Not.Contain(genre));
            Assert.That(genre.Manuscripts, Does.Not.Contain(manuscript));
        }

        /// <summary>
        /// Проверяет, что попытка удалить не добавленный жанр возвращает false.
        /// </summary>
        [Test]
        public void RemoveGenre_NonExistingGenre_ReturnsFalse()
        {
            // Arrange
            var manuscript = TestData.ValidManuscript().Build();
            var genre = TestData.ValidGenre().WithName("Не добавленный жанр").Build();

            // Act
            var result = manuscript.RemoveGenre(genre);

            // Assert
            Assert.That(result, Is.False);
        }

        /// <summary>
        /// Проверяет, что попытка удалить null в качестве жанра возвращает false.
        /// </summary>
        [Test]
        public void RemoveGenre_NullGenre_ReturnsFalse()
        {
            // Arrange
            var manuscript = TestData.ValidManuscript().Build();

            // Act
            var result = manuscript.RemoveGenre(null!);

            // Assert
            Assert.That(result, Is.False);
        }

        /// <summary>
        /// Проверяет, что сравнение рукописи с null возвращает false.
        /// </summary>
        [Test]
        public void Equals_Null_ReturnsFalse()
        {
            // Arrange
            var manuscript = TestData.ValidManuscript().Build();

            // Act & Assert
            // Исправлено: теперь реально проверяется метод Equals, а не просто Is.Not.Null
            Assert.That(manuscript.Equals(null), Is.False);
        }

        /// <summary>
        /// Проверяет, что сравнение рукописи с объектом другого типа возвращает false.
        /// </summary>
        [Test]
        public void Equals_DifferentType_ReturnsFalse()
        {
            // Arrange
            var manuscript = TestData.ValidManuscript().Build();

            // Act & Assert
            Assert.That(manuscript.Equals("not a manuscript"), Is.False);
        }

        /// <summary>
        /// Проверяет, что рукописи с одинаковым названием считаются равными, даже если языки отличаются.
        /// </summary>
        [Test]
        public void Equals_SameTitle_DifferentOtherFields_ReturnsTrue()
        {
            // Arrange
            var language1 = TestData.ValidLanguage().WithName("Русский").Build();
            var language2 = TestData.ValidLanguage().WithName("Английский").Build();
            var languages1 = new HashSet<Language> { language1, };
            var languages2 = new HashSet<Language> { language2, };
            var person = TestData.ValidPerson().WithFirstName("Лев").WithFamilyName("Толстой").Build();
            var author = TestData.ValidAuthor().WithPerson(person).Build();
            var title = "Война и мир";
            var authors = new HashSet<Author> { author, };

            var manuscript1 = new Manuscript(title, languages1, authors);
            var manuscript2 = new Manuscript(title, languages2, authors);

            // Act
            var result = manuscript1.Equals(manuscript2);

            // Assert
            Assert.That(result, Is.True);
        }

        /// <summary>
        /// Проверяет, что рукописи с разными названиями не равны.
        /// </summary>
        [Test]
        public void Equals_DifferentTitle_ReturnsFalse()
        {
            // Arrange
            var languages = new HashSet<Language> { TestData.ValidLanguage().Build(), };
            var person = TestData.ValidPerson().WithFirstName("Лев").WithFamilyName("Толстой").Build();
            var author = TestData.ValidAuthor().WithPerson(person).Build();

            var manuscript1 = new Manuscript("Война и мир", languages, new HashSet<Author> { author, });
            var manuscript2 = new Manuscript("Анна Каренина", languages, new HashSet<Author> { author, });

            // Act
            var result = manuscript1.Equals(manuscript2);

            // Assert
            Assert.That(result, Is.False);
        }

        /// <summary>
        /// Проверяет, что метод ToString возвращает корректную строку с названием и одним автором.
        /// </summary>
        [Test]
        public void ToString_WithSingleAuthor_ReturnsTitleAndAuthor()
        {
            // Arrange
            var person = TestData.ValidPerson()
                .WithFirstName("Лев")
                .WithFamilyName("Толстой")
                .WithPatronymicName("Николаевич")
                .Build();
            var author = TestData.ValidAuthor().WithPerson(person).Build();
            var languages = new HashSet<Language> { TestData.ValidLanguage().Build(), };
            var manuscript = new Manuscript("Анна Каренина", languages, new HashSet<Author> { author, });

            // Act
            var result = manuscript.ToString();

            // Assert
            Assert.That(result, Is.EqualTo("Анна Каренина: [Толстой Лев Николаевич]"));
        }

        /// <summary>
        /// Проверяет, что метод ToString возвращает корректную строку с названием и несколькими авторами.
        /// </summary>
        [Test]
        public void ToString_WithMultipleAuthors_ReturnsTitleAndAuthorsJoined()
        {
            // Arrange
            var person1 = TestData.ValidPerson().WithFirstName("Илья").WithFamilyName("Ильф").Build();
            var author1 = TestData.ValidAuthor().WithPerson(person1).Build();
            var person2 = TestData.ValidPerson().WithFirstName("Евгений").WithFamilyName("Петров").Build();
            var author2 = TestData.ValidAuthor().WithPerson(person2).Build();
            var languages = new HashSet<Language> { TestData.ValidLanguage().Build(), };
            var manuscript = new Manuscript("12 стульев", languages, new HashSet<Author> { author1, author2, });

            // Act
            var result = manuscript.ToString();

            // Assert
            Assert.That(result, Is.EqualTo("12 стульев: [Ильф Илья, Петров Евгений]"));
        }

        /// <summary>
        /// Проверяет, что коллекция жанров изначально пуста.
        /// </summary>
        [Test]
        public void Genres_Collection_StartsEmpty()
        {
            // Arrange & Act
            var manuscript = TestData.ValidManuscript().Build();

            // Assert
            Assert.That(manuscript.Genres, Is.Empty);
        }

        /// <summary>
        /// Проверяет, что коллекция переводчиков изначально пуста.
        /// </summary>
        [Test]
        public void Translators_Collection_StartsEmpty()
        {
            // Arrange & Act
            var manuscript = TestData.ValidManuscript().Build();

            // Assert
            Assert.That(manuscript.Translators, Is.Empty);
        }

        /// <summary>
        /// Проверяет, что коллекция рецензентов изначально пуста.
        /// </summary>
        [Test]
        public void Reviewers_Collection_StartsEmpty()
        {
            // Arrange & Act
            var manuscript = TestData.ValidManuscript().Build();

            // Assert
            Assert.That(manuscript.Reviewers, Is.Empty);
        }

        /// <summary>
        /// Проверяет, что добавление рукописи рецензенту устанавливает двустороннюю связь.
        /// </summary>
        [Test]
        public void AddReviewer_ValidData_EstablishesBidirectionalLink()
        {
            // Arrange
            var manuscript = TestData.ValidManuscript().Build();
            var person = TestData.ValidPerson().WithFirstName("Алексей").WithFamilyName("Палей").Build();
            var reviewer = TestData.ValidReviewer().WithPerson(person).Build();

            // Act
            reviewer.AddManuscript(manuscript);

            // Assert
            Assert.That(manuscript.Reviewers, Has.Count.EqualTo(1));
            Assert.That(manuscript.Reviewers.Any(r => r.Id == reviewer.Id), Is.True);
        }

        /// <summary>
        /// Проверяет, что добавление рукописи переводчику устанавливает двустороннюю связь.
        /// </summary>
        [Test]
        public void AddTranslator_ValidData_EstablishesBidirectionalLink()
        {
            // Arrange
            var manuscript = TestData.ValidManuscript().Build();
            var person = TestData.ValidPerson().WithFirstName("Алексей").WithFamilyName("Палей").Build();
            var translator = TestData.ValidTranslator().WithPerson(person).Build();

            // Act
            translator.AddManuscript(manuscript);

            // Assert
            Assert.That(manuscript.Translators, Has.Count.EqualTo(1));
            Assert.That(manuscript.Translators.Any(r => r.Id == translator.Id), Is.True);
        }

        /// <summary>
        /// Проверяет, что коллекция книг изначально пуста.
        /// </summary>
        [Test]
        public void Books_Collection_StartsEmpty()
        {
            // Arrange & Act
            var manuscript = TestData.ValidManuscript().Build();

            // Assert
            Assert.That(manuscript.Books, Is.Empty);
        }

        /// <summary>
        /// Проверяет, что рукопись, созданная через провайдер по умолчанию, содержит ровно одного автора.
        /// </summary>
        [Test]
        public void Authors_CreateManuscriptViaProvider_HasOneAuthor()
        {
            // Arrange
            var manuscript = TestData.ValidManuscript().Build();

            // Act
            var authors = manuscript.Authors;

            // Assert
            Assert.That(authors, Has.Count.EqualTo(1));
        }
    }
}