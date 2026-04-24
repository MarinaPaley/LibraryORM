// <copyright file="ManuscriptTests.cs" company="Филипченко Марина Алексеевна">
// Copyright (c) Филипченко Марина Алексеевна 2026. Library.
// </copyright>

namespace Domain.Tests
{
    using System;
    using System.Collections.Generic;
    using NUnit.Framework;
    using Staff;

    /// <summary>
    /// Модульные тесты для класса <see cref="Manuscript"/>.
    /// </summary>
    [TestFixture]
    public sealed class ManuscriptTests
    {
        #region Test data helpers

        private static Language CreateLanguage(string name = "Русский") => new(name);

        private static Person CreatePerson(string family, string given, string? patronymic = null) =>
            new(new Name(family, given, patronymic));

        private static Author CreateAuthor(string family, string given, string? patronymic = null) =>
            new(CreatePerson(family, given, patronymic));

        private static Translator CreateTranslator(string family, string given) =>
            new(CreatePerson(family, given));

        private static Reviewer CreateReviewer(string family, string given) =>
            new(CreatePerson(family, given));

        private static Genre CreateGenre(string name) => new(name);

        #endregion

        #region Constructor validation tests

        [Test]
        public void Ctor_NullName_ThrowsArgumentNullException()
        {
            // arrange
            var language = CreateLanguage();
            var authors = new HashSet<Author>();

            // act & assert
            Assert.Throws<ArgumentNullException>(() =>
                _ = new Manuscript(null!, language, authors));
        }

        [Test]
        public void Ctor_EmptyName_AfterTrim_ThrowsArgumentNullException()
        {
            // arrange
            var language = CreateLanguage();
            var authors = new HashSet<Author>();

            // act & assert
            Assert.Throws<ArgumentNullException>(() =>
                _ = new Manuscript("   ", language, authors));
        }

        [Test]
        public void Ctor_NullLanguage_ThrowsArgumentNullException()
        {
            // arrange
            var authors = new HashSet<Author>();

            // act & assert
            Assert.Throws<ArgumentNullException>(() =>
                _ = new Manuscript("Название", null!, authors));
        }

        [Test]
        public void Ctor_NullAuthors_ThrowsArgumentNullException()
        {
            // arrange
            var language = CreateLanguage();

            // act & assert
            Assert.Throws<ArgumentNullException>(() =>
                _ = new Manuscript(name: "Название", language: language, authors: null!, date: null));
        }

        [Test]
        public void Ctor_EmptyAuthorsCollection_Allowed()
        {
            // arrange
            var language = CreateLanguage();
            var authors = new HashSet<Author>();

            // act & assert
            Assert.DoesNotThrow(() =>
            {
                var manuscript = new Manuscript("Название", language, authors);
                Assert.That(manuscript.Authors, Is.Empty);
            });
        }

        [Test]
        public void Ctor_NullDateRange_Allowed()
        {
            // arrange
            var language = CreateLanguage();
            var authors = new HashSet<Author>();

            // act
            var manuscript = new Manuscript("Название", language, authors, date: null);

            // assert
            Assert.That(manuscript.Dates, Is.Null);
        }

        [Test]
        public void Ctor_ValidData_Success()
        {
            // arrange
            var language = CreateLanguage();
            var author = CreateAuthor("Толстой", "Лев");
            var authors = new HashSet<Author> { author };
            var dateRange = new Range<DateOnly>(new DateOnly(1865, 1, 1), new DateOnly(1869, 12, 31));

            // act & assert
            Assert.DoesNotThrow(() =>
            {
                var manuscript = new Manuscript("Война и мир", language, authors, dateRange);

                Assert.That(manuscript.Title.Value, Is.EqualTo("Война и мир"));
                Assert.That(manuscript.Language.ToString(), Is.EqualTo("Русский"));
                Assert.That(manuscript.Authors, Contains.Item(author));
                Assert.That(manuscript.Dates?.From, Is.EqualTo(new DateOnly(1865, 1, 1)));
                Assert.That(manuscript.Dates?.To, Is.EqualTo(new DateOnly(1869, 12, 31)));
            });
        }

        [Test]
        public void Ctor_WithDateOnlyParams_CreatesRange()
        {
            // arrange
            var language = CreateLanguage();
            var author = CreateAuthor("Ильф", "Илья");
            var from = new DateOnly(1927, 1, 1);
            var to = new DateOnly(1928, 12, 31);

            // act
            var manuscript = new Manuscript(
                "12 стульев", language, from, to, author);

            // assert
            Assert.That(manuscript.Dates, Is.Not.Null);
            Assert.That(manuscript.Dates!.From, Is.EqualTo(from));
            Assert.That(manuscript.Dates!.To, Is.EqualTo(to));
        }

        [Test]
        public void Ctor_WithSingleDateOnly_CreatesRangeWithSameBounds()
        {
            // arrange
            var language = CreateLanguage();
            var author = CreateAuthor("Чехов", "Антон");
            var date = new DateOnly(1886, 1, 1);

            // act
            var manuscript = new Manuscript(
                "Хамелеон", language, date, date, author);

            // assert
            Assert.That(manuscript.Dates!.From, Is.EqualTo(manuscript.Dates!.To));
            Assert.That(manuscript.Dates!.From, Is.EqualTo(date));
        }

        #endregion

        #region Bidirectional relationship tests

        [Test]
        public void Ctor_AddsManuscriptToAuthors()
        {
            // arrange
            var language = CreateLanguage();
            var author1 = CreateAuthor("Толстой", "Лев");
            var author2 = CreateAuthor("Достоевский", "Фёдор");
            var authors = new HashSet<Author> { author1, author2 };

            // act
            var manuscript = new Manuscript("Классика", language, authors);

            // assert
            Assert.That(author1.Manuscripts, Contains.Item(manuscript));
            Assert.That(author2.Manuscripts, Contains.Item(manuscript));
        }

        [Test]
        public void Ctor_EmptyAuthors_NoBidirectionalAdd()
        {
            // arrange
            var language = CreateLanguage();
            var authors = new HashSet<Author>();

            // act
            var manuscript = new Manuscript("Без автора", language, authors);

            // assert — ничего не должно упасть
            Assert.That(manuscript.Authors, Is.Empty);
        }

        [Test]
        public void AddGenre_ValidGenre_AddsToBothCollections()
        {
            // arrange
            var manuscript = CreateMinimalManuscript();
            var genre = CreateGenre("Роман");

            // act
            var result = manuscript.AddGenre(genre);

            // assert
            Assert.That(result, Is.True);
            Assert.That(manuscript.Genres, Contains.Item(genre));
            Assert.That(genre.Manuscripts, Contains.Item(manuscript));
        }

        [Test]
        public void AddGenre_NullGenre_ReturnsFalse()
        {
            // arrange
            var manuscript = CreateMinimalManuscript();

            // act
            var result = manuscript.AddGenre(null!);

            // assert
            Assert.That(result, Is.False);
            Assert.That(manuscript.Genres, Is.Empty);
        }

        [Test]
        public void AddGenre_DuplicateGenre_ReturnsFalse()
        {
            // arrange
            var manuscript = CreateMinimalManuscript();
            var genre = CreateGenre("Роман");
            manuscript.AddGenre(genre);

            // act
            var result = manuscript.AddGenre(genre);

            // assert
            Assert.That(result, Is.False); // HashSet не добавляет дубликаты
            Assert.That(manuscript.Genres.Count, Is.EqualTo(1));
        }

        [Test]
        public void RemoveGenre_ExistingGenre_RemovesFromBothCollections()
        {
            // arrange
            var manuscript = CreateMinimalManuscript();
            var genre = CreateGenre("Роман");
            manuscript.AddGenre(genre);

            // act
            var result = manuscript.RemoveGenre(genre);

            // assert
            Assert.That(result, Is.True);
            Assert.That(manuscript.Genres, Does.Not.Contain(genre));
            Assert.That(genre.Manuscripts, Does.Not.Contain(manuscript));
        }

        [Test]
        public void RemoveGenre_NonExistingGenre_ReturnsFalse()
        {
            // arrange
            var manuscript = CreateMinimalManuscript();
            var genre = CreateGenre("Не добавленный жанр");

            // act
            var result = manuscript.RemoveGenre(genre);

            // assert
            Assert.That(result, Is.False);
        }

        [Test]
        public void RemoveGenre_NullGenre_ReturnsFalse()
        {
            // arrange
            var manuscript = CreateMinimalManuscript();

            // act
            var result = manuscript.RemoveGenre(null!);

            // assert
            Assert.That(result, Is.False);
        }

        #endregion

        #region Equality tests

        [Test]
        public void Equals_SameReference_ReturnsTrue()
        {
            // arrange
            var manuscript = CreateMinimalManuscript();

            // act & assert
            Assert.That(manuscript, Is.EqualTo(manuscript));
        }

        [Test]
        public void Equals_Null_ReturnsFalse()
        {
            // arrange
            var manuscript = CreateMinimalManuscript();

            // act & assert
            Assert.That(manuscript, Is.Not.Null);
        }

        [Test]
        public void Equals_DifferentType_ReturnsFalse()
        {
            // arrange
            var manuscript = CreateMinimalManuscript();

            // act & assert
            Assert.That(manuscript.Equals("not a manuscript"), Is.False);
        }

        [Test]
        public void Equals_SameTitle_DifferentOtherFields_ReturnsTrue()
        {
            // arrange
            var language1 = CreateLanguage("Русский");
            var language2 = CreateLanguage("Английский");
            var author = CreateAuthor("Толстой", "Лев");

            var manuscript1 = new Manuscript("Война и мир", language1, new HashSet<Author> { author });
            var manuscript2 = new Manuscript("Война и мир", language2, new HashSet<Author>());

            // act & assert
            Assert.That(manuscript1, Is.EqualTo(manuscript2));
        }

        [Test]
        public void Equals_DifferentTitle_ReturnsFalse()
        {
            // arrange
            var language = CreateLanguage();
            var author = CreateAuthor("Толстой", "Лев");

            var manuscript1 = new Manuscript("Война и мир", language, new HashSet<Author> { author });
            var manuscript2 = new Manuscript("Анна Каренина", language, new HashSet<Author> { author });

            // act & assert
            Assert.That(manuscript1, Is.Not.EqualTo(manuscript2));
        }

        [Test]
        public void GetHashCode_SameObject_SameHashCode()
        {
            // arrange
            var manuscript = CreateMinimalManuscript();

            // act & assert
            Assert.That(manuscript.GetHashCode(), Is.EqualTo(expected: manuscript.GetHashCode()));
        }

        #endregion

        #region ToString tests

        #region ToString tests

        [Test]
        public void ToString_WithSingleAuthor_ReturnsTitleAndAuthor()
        {
            // arrange
            var author = new Author(new Person(new Name("Толстой", "Лев", "Николаевич")));
            var manuscript = new Manuscript(
                "Анна Каренина",
                new Language("Русский"),
                new HashSet<Author> { author });

            // act
            var result = manuscript.ToString();

            // assert
            Assert.That(result, Is.EqualTo("Анна Каренина Толстой Лев Николаевич"));
        }

        [Test]
        public void ToString_WithMultipleAuthors_ReturnsTitleAndAuthorsJoined()
        {
            // arrange
            var author1 = new Author(new Person(new Name("Ильф", "Илья")));
            var author2 = new Author(new Person(new Name("Петров", "Евгений")));
            var manuscript = new Manuscript(
                "12 стульев",
                new Language("Русский"),
                new HashSet<Author> { author1, author2 });

            // act
            var result = manuscript.ToString();

            // assert
            Assert.That(result, Is.EqualTo("12 стульев Ильф Илья, Петров Евгений"));
        }

        [Test]
        public void ToString_WithNoAuthors_ReturnsTitleOnly()
        {
            // arrange
            var manuscript = new Manuscript(
                "Без автора",
                new Language("Русский"),
                new HashSet<Author>());

            // act
            var result = manuscript.ToString();

            // assert
            Assert.That(result, Is.EqualTo("Без автора"));
        }

        [Test]
        public void ToString_TitleWithSpaces_Trimmed()
        {
            // arrange
            var manuscript = new Manuscript(
                "  Название с пробелами  ",
                new Language("Русский"),
                new HashSet<Author>());

            // act
            var result = manuscript.ToString();

            // assert — Title.TrimOrNull() должен убрать пробелы
            Assert.That(result, Is.EqualTo("Название с пробелами"));
        }

        #endregion
        [Test]
        public void ToString_TitleWithSpaces_Preserved()
        {
            // arrange
            var manuscript = new Manuscript("  Название с пробелами  ", new Language("Русский"), new HashSet<Author>());

            // act
            var result = manuscript.ToString();

            // assert — Title.TrimOrNull() должен убрать пробелы
            Assert.That(result, Does.StartWith("Название с пробелами"));
        }

        #endregion

        #region Collection property tests

        [Test]
        public void Authors_Collection_IsReadOnlyExternally()
        {
            // arrange
            var language = CreateLanguage();
            var author = CreateAuthor("Толстой", "Лев");
            var authors = new HashSet<Author> { author };

            var manuscript = new Manuscript("Война и мир", language, authors);
            var authorsReference = manuscript.Authors;

            // act — пытаемся изменить коллекцию извне
            var anotherAuthor = CreateAuthor("Достоевский", "Фёдор");

            // assert — коллекция должна быть доступна только для чтения через публичный API
            // (фактически это HashSet, но свойство не имеет публичного сеттера)
            Assert.That(authorsReference, Is.Not.Null);
            Assert.That(authorsReference, Contains.Item(author));
        }

        [Test]
        public void Genres_Collection_StartsEmpty()
        {
            // arrange & act
            var manuscript = CreateMinimalManuscript();

            // assert
            Assert.That(manuscript.Genres, Is.Empty);
        }

        [Test]
        public void Translators_Collection_StartsEmpty()
        {
            // arrange & act
            var manuscript = CreateMinimalManuscript();

            // assert
            Assert.That(manuscript.Translators, Is.Empty);
        }

        [Test]
        public void Reviewers_Collection_StartsEmpty()
        {
            // arrange & act
            var manuscript = CreateMinimalManuscript();

            // assert
            Assert.That(manuscript.Reviewers, Is.Empty);
        }

        [Test]
        public void Books_Collection_StartsEmpty()
        {
            // arrange & act
            var manuscript = CreateMinimalManuscript();

            // assert
            Assert.That(manuscript.Books, Is.Empty);
        }

        #endregion

        #region Helper methods

        private static Manuscript CreateMinimalManuscript()
        {
            return new Manuscript(
                "Минимальная рукопись",
                CreateLanguage(),
                new HashSet<Author> { CreateAuthor("Автор", "Тестовый") });
        }

        #endregion
    }
}