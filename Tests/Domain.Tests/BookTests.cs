// <copyright file="BookTests.cs" company="Васильева Марина Алексеевна">
// Copyright (c) Васильева Марина Алексеевна 2024. Library.
// </copyright>

namespace Domain.Tests
{
    using System;
    using System.Collections.Generic;
    using Domain;
    using NUnit.Framework;

    /// <summary>
    /// Модульные тесты для класса <see cref="Book"/>.
    /// </summary>
    [TestFixture]
    public sealed class BookTests
    {
        private static readonly Name Tolstoy = new ("Толстой", "Лев", "Николаевич");
        private static readonly Name Ilf = new ("Ильф", "Илья");
        private static readonly Name Petrov = new ("Петров", "Евгений");
        private static readonly Author Lev = new (Tolstoy);
        private static readonly Author Ilia = new (Ilf);
        private static readonly Author Evgen = new (Petrov);

        [Test]
        public void Ctor_NullTitle_ExpectedArgumentNullException()
        {
            Assert.Throws<ArgumentNullException>(() => _ = new Book(null!, 100, "1"));
        }

        [Test]
        public void Ctor_NullISBN_ExpectedArgumentNullException()
        {
            Assert.Throws<ArgumentNullException>(() => _ = new Book("Тестовое название", 100, null!));
        }

        [TestCase(0)]
        [TestCase(-1)]
        public void Ctor_NegativePages_ExpectedArgumentOutOfRangeException(int pages)
        {
            Assert.Throws<ArgumentOutOfRangeException>(() => _ = new Book("Тестовое название", pages, "1"));
        }

        [TestCaseSource(nameof(BookWithAuthors))]
        public string ToString_ValidData_Success(Book book) => book.ToString();

        private static IEnumerable<TestCaseData> BookWithAuthors()
        {
            yield return new TestCaseData(new Book("Анна Каренина", 250, "123", null, Lev))
                .Returns("Анна Каренина Толстой Лев Николаевич");

            yield return new TestCaseData(new Book("12 стульев", 250, "1456", null, Ilia, Evgen))
                .Returns("12 стульев Ильф Илья, Петров Евгений");
        }
    }
}
