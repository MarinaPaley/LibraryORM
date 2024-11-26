// <copyright file="BookConfigurationTests.cs" company="Васильева Марина Алексеевна">
// Copyright (c) Васильева Марина Алексеевна 2024. Library.
// </copyright>

namespace DataAccessLibrary.Tests
{
    using Domain;
    using NUnit.Framework;

    /// <summary>
    /// Тесты для <see cref="DataAccessLibrary.Configurations.BookConfiguration"/>.
    /// </summary>
    [TestFixture]
    internal sealed class BookConfigurationTests : BaseConfigurationTests
    {
        [TearDown]
        public void TearDown()
        {
            this.DataContext.ChangeTracker.Clear();
        }

        [Test]
        public void AddEntityToDatabase_Success()
        {
            // arrange
            var book = new Book("Тестовая", 100, "12345");

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
