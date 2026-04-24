// <copyright file="ManuscriptTests.cs" company="Филипченко Марина Алексеевна">
// Copyright (c) Филипченко Марина Алексеевна 2026. Library.
// </copyright>

namespace Domain.Tests
{
    using System;
    using System.Collections.Generic;
    using Domain;
    using NUnit.Framework;
    using Staff;

    /// <summary>
    /// Модульные тесты для класса <see cref="Book"/>.
    /// </summary>
    [TestFixture]
    public sealed class ManuscriptTests
    {
        private static readonly Name Tolstoy = new ("Толстой", "Лев", "Николаевич");
        private static readonly Name Ilf = new ("Ильф", "Илья");
        private static readonly Name Petrov = new ("Петров", "Евгений");
        private static readonly Author Lev = new (new Person(Tolstoy));
        private static readonly Author Ilia = new (new Person(Ilf));
        private static readonly Author Evgen = new (new Person(Petrov));

        [Test]
        public void Ctor_NullTitle_ExpectedArgumentNullException()
        {
            var language = new Language("Русский");
            Assert.Throws<ArgumentNullException>(() => _ = new Manuscript(null!, language, new HashSet<Author>()));
        }

        [TestCaseSource(nameof(BookWithAuthors))]
        public string ToString_ValidData_Success(Manuscript book) => book.ToString();

        private static IEnumerable<TestCaseData> BookWithAuthors()
        {
            yield return new TestCaseData(new Manuscript("Анна Каренина", new Language("Русский"), new HashSet<Author>() { Lev }))
                .Returns("Анна Каренина Толстой Лев Николаевич");

            yield return new TestCaseData(new Manuscript("12 стульев", new Language("Русский"), new HashSet<Author>() { Ilia, Evgen }))
                .Returns("12 стульев Ильф Илья, Петров Евгений");
        }
    }
}
