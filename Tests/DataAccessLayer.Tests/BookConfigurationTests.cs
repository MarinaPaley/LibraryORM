// <copyright file="BookConfigurationTests.cs" company="Филипченко Марина Алексеевна">
// Copyright (c) Филипченко Марина Алексеевна 2026. Library.
// </copyright>

namespace DataAccessLayer.Tests
{
    using DataAccessLayer.Tests.Abstract;
    using Domain;
    using NUnit.Framework;

    /// <summary>
    /// Тесты для <see cref="DataAccessLayer.Configurations.BookConfiguration"/>.
    /// </summary>
    [TestFixture]
    internal sealed class BookConfigurationTests : BaseConfigurationTests
    {
        [Test]
        public void AddEntityToDatabase_Success()
        {
            // arrange
            var bookType = new BookType("Книга");
            var publisher = new Publisher("Издательство");
            var book = new Book("Тестовая", 100, "12345", bookType, publisher, 2026);

            // act
            _ = this.DataContext.Add(book);
            _ = this.DataContext.SaveChanges(); // <-- если что-то плохо, то тут БУМ!
            this.DataContext.ChangeTracker.Clear();

            // assert
            var result = this.DataContext.Find<Book>(book.Id);

            Assert.That(result, Is.Not.Null);
            Assert.That(result!.Title, Is.EqualTo(book.Title));
        }
    }
}
