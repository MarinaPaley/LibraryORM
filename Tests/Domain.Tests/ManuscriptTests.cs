// <copyright file="ManuscriptTests.cs" company="Филипченко Марина Алексеевна">
// Copyright (c) Филипченко Марина Алексеевна 2026. Library.
// </copyright>

namespace Domain.Tests
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using NUnit.Framework;
    using Staff;

    /// <summary>
    /// Модульные тесты для класса <see cref="Manuscript"/>.
    /// </summary>
    [TestFixture]
    public sealed class ManuscriptTests
    {
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
                _ = new Manuscript(name: "Название", languages: language, authors: null!, date: null));
        }

        [Test]
        public void Ctor_EmptyAuthorsCollection_Throws()
        {
            // arrange
            var language = CreateLanguage();
            var authors = new HashSet<Author>();

            // act & assert
            Assert.Throws<ArgumentOutOfRangeException>(() =>
            {
                _ = new Manuscript("Название", language, authors);
            });
        }

        [Test]
        public void Ctor_NullDateRange_Allowed()
        {
            // arrange
            var language = CreateLanguage();
            var authors = new HashSet<Author>() { CreateAuthor("a", "b") };

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

                using (Assert.EnterMultipleScope())
                {
                    Assert.That(manuscript.Name.Value, Is.EqualTo("Война и мир"));
                    Assert.That(manuscript.Authors, Contains.Item(author));
                    Assert.That(manuscript.Dates?.From, Is.EqualTo(new DateOnly(1865, 1, 1)));
                    Assert.That(manuscript.Dates?.To, Is.EqualTo(new DateOnly(1869, 12, 31)));
                }
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
                "12 стульев", language, from, to, null, author);

            // assert
            Assert.That(manuscript.Dates, Is.Not.Null);
            using (Assert.EnterMultipleScope())
            {
                Assert.That(manuscript.Dates!.From, Is.EqualTo(from));
                Assert.That(manuscript.Dates!.To, Is.EqualTo(to));
            }
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
                "Хамелеон", language, date, date, null, author);

            // assert
            Assert.That(manuscript.Dates!.From, Is.EqualTo(manuscript.Dates!.To));
            Assert.That(manuscript.Dates!.From, Is.EqualTo(date));
        }

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
            using (Assert.EnterMultipleScope())
            {
                Assert.That(author1.Manuscripts, Contains.Item(manuscript));
                Assert.That(author2.Manuscripts, Contains.Item(manuscript));
            }
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
            using (Assert.EnterMultipleScope())
            {
                Assert.That(result, Is.True);
                Assert.That(manuscript.Genres, Contains.Item(genre));
                Assert.That(genre.Manuscripts, Contains.Item(manuscript));
            }
        }

        [Test]
        public void AddGenre_NullGenre_ReturnsFalse()
        {
            // arrange
            var manuscript = CreateMinimalManuscript();

            // act
            var result = manuscript.AddGenre(null!);

            // assert
            using (Assert.EnterMultipleScope())
            {
                Assert.That(result, Is.False);
                Assert.That(manuscript.Genres, Is.Empty);
            }
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
            Assert.That(result, Is.False);
            Assert.That(manuscript.Genres, Has.Count.EqualTo(1));
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
            var title = "Война и мир";
            var authors = new HashSet<Author>() { author };

            var manuscript1 = new Manuscript(title, language1, authors);
            var manuscript2 = new Manuscript(title, language2, authors);

            // act
            var result = manuscript1.Equals(manuscript2);

            // assert
            Assert.That(result, Is.True);
        }

        [Test]
        public void Equals_DifferentTitle_ReturnsFalse()
        {
            // arrange
            var language = CreateLanguage();
            var author = CreateAuthor("Толстой", "Лев");

            var manuscript1 = new Manuscript("Война и мир", language, new HashSet<Author> { author });
            var manuscript2 = new Manuscript("Анна Каренина", language, new HashSet<Author> { author });
            var result = manuscript1.Equals(manuscript2);

            // act & assert
            Assert.That(result, Is.False);
        }

        [Test]
        public void GetHashCode_SameObject_SameHashCode()
        {
            // arrange
            var manuscript = CreateMinimalManuscript();

            // act & assert
            Assert.That(manuscript.GetHashCode(), Is.EqualTo(expected: manuscript.GetHashCode()));
        }

        [Test]
        public void ToString_WithSingleAuthor_ReturnsTitleAndAuthor()
        {
            // arrange
            var author = new Author(new Person(new Name("Толстой", "Лев", "Николаевич")));
            var manuscript = new Manuscript(
                "Анна Каренина",
                new HashSet<Language>() { new ("Русский") },
                new HashSet<Author> { author });

            // act
            var result = manuscript.ToString();

            // assert
            Assert.That(result, Is.EqualTo("Анна Каренина: [Толстой Лев Николаевич]"));
        }

        [Test]
        public void ToString_WithMultipleAuthors_ReturnsTitleAndAuthorsJoined()
        {
            // arrange
            var author1 = new Author(new Person(new Name("Ильф", "Илья")));
            var author2 = new Author(new Person(new Name("Петров", "Евгений")));
            var manuscript = new Manuscript(
                "12 стульев",
                new HashSet<Language>() { new ("Русский") },
                new HashSet<Author> { author1, author2 });

            // act
            var result = manuscript.ToString();

            // assert
            Assert.That(result, Is.EqualTo("12 стульев: [Ильф Илья, Петров Евгений]"));
        }

        [Test]
        public void ToString_TitleWithSpaces_Trimmed()
        {
            // arrange
            var manuscript = new Manuscript(
                "  Название с пробелами  ",
                new HashSet<Language>() { new ("Русский") },
                new HashSet<Author>() { CreateMinimalAuthor() });

            // act
            var result = manuscript.ToString();

            // assert
            Assert.That(result, Is.EqualTo("Название с пробелами: [Фамилия Имя]"));
        }

        [Test]
        public void ToString_TitleWithSpaces_Preserved()
        {
            // arrange
            var manuscript = new Manuscript(
                "  Название с пробелами  ",
                new HashSet<Language>() { new ("Русский") },
                new HashSet<Author>() { CreateMinimalAuthor() });

            // act
            var result = manuscript.ToString();

            // assert
            Assert.That(result, Does.StartWith("Название с пробелами"));
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
        public void AddReviwer_ValidData_Success()
        {
            // act
            var manuscript = CreateMinimalManuscript();
            var reviwer = CreateReviewer("Палей", "Алексей");

            // act
            reviwer.AddManuscript(manuscript);

            // assert
            Assert.That(manuscript.Reviewers, Has.Count.EqualTo(1));
            Assert.That(manuscript.Reviewers.Any(r => r.Id == reviwer.Id), Is.True);
        }

        [Test]
        public void AddTranslator_ValidData_Success()
        {
            // act
            var manuscript = CreateMinimalManuscript();
            var translator = CreateTranslator("Палей", "Алексей");

            // act
            translator.AddManuscript(manuscript);

            // assert
            Assert.That(manuscript.Translators, Has.Count.EqualTo(1));
            Assert.That(manuscript.Translators.Any(r => r.Id == translator.Id), Is.True);
        }

        [Test]
        public void Books_Collection_StartsEmpty()
        {
            // arrange & act
            var manuscript = CreateMinimalManuscript();

            // assert
            Assert.That(manuscript.Books, Is.Empty);
        }

        private static Manuscript CreateMinimalManuscript()
        {
            return new Manuscript(
                "Минимальная рукопись",
                CreateLanguage(),
                new HashSet<Author> { CreateAuthor("Автор", "Тестовый") });
        }

        private static HashSet<Language> CreateLanguage(string name = "Русский") => new HashSet<Language>() { new(name) };

        private static Person CreatePerson(string family, string given, string? patronymic = null) =>
            new (new Name(family, given, patronymic));

        private static Author CreateAuthor(string family, string given, string? patronymic = null) =>
            new (CreatePerson(family, given, patronymic));

        private static Author CreateMinimalAuthor() =>
            new (new Person(new Name("Фамилия", "Имя")));

        private static Translator CreateTranslator(string family, string given) =>
            new (CreatePerson(family, given));

        private static Reviewer CreateReviewer(string family, string given) =>
            new (CreatePerson(family, given));

        private static Genre CreateGenre(string name) => new (name);
    }
}
