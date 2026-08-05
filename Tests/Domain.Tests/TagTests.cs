// <copyright file="TagTests.cs" company="Филипченко Марина Алексеевна">
// Copyright (c) Филипченко Марина Алексеевна 2026. Library.
// </copyright>

namespace Domain.Tests
{
    using System;
    using Domain;
    using NUnit.Framework;
    using TestDataProvider;

    /// <summary>
    /// Модульные тесты для класса <see cref="Tag"/>.
    /// </summary>
    [TestFixture]
    public sealed class TagTests
    {
        /// <summary>
        /// Проверяет, что конструктор <see cref="Tag"/> успешно создает объект с валидными данными.
        /// </summary>
        [Test]
        public void Ctor_ValidData_Success()
        {
            // Arrange
            var category = TestData.ValidCategory().Build();

            // Act & Assert
            Assert.DoesNotThrow(() => _ = new Tag("Tag", category));
        }

        /// <summary>
        /// Проверяет, что конструктор <see cref="Tag"/> выбрасывает исключение при передаче null в качестве названия.
        /// </summary>
        /// <param name="name"> Некорректное имя тега (null или пустая строка). </param>
        [TestCase(null)]
        [TestCase("")]
        public void Ctor_NullName_ThrowsArgumentNullException(string? name)
        {
            // Arrange
            var category = TestData.ValidCategory().Build();

            // Act & Assert
            Assert.Throws<ArgumentNullException>(() => _ = new Tag(name!, category));
        }

        /// <summary>
        /// Проверяет, что конструктор <see cref="Tag"/> выбрасывает исключение при передаче null в качестве категории.
        /// </summary>
        [Test]
        public void Ctor_NullCategory_ThrowsArgumentNullException()
        {
            // Arrange, Act & Assert
            Assert.Throws<ArgumentNullException>(() => _ = new Tag("Tag", null!));
        }

        /// <summary>
        /// Проверяет, что конструктор <see cref="Tag"/> выбрасывает исключение при попытке добавить тег, который уже существует в категории.
        /// </summary>
        [Test]
        public void Ctor_DuplicateTagInCategory_ThrowsArgumentException()
        {
            // Arrange
            var category = TestData.ValidCategory().WithName("Category").Build();
            _ = new Tag("Tag", category);

            // Act & Assert
            Assert.Throws<ArgumentException>(() => _ = new Tag("Tag", category));
        }

        /// <summary>
        /// Проверяет, что при создании тега он автоматически добавляется в коллекцию тегов категории.
        /// </summary>
        [Test]
        public void Ctor_AddsTagToCategory()
        {
            // Arrange
            var category = TestData.ValidCategory().WithName("Category").Build();

            // Act
            var tag = new Tag("Tag", category);

            // Assert
            Assert.That(category.Tags, Contains.Item(tag));
        }

        /// <summary>
        /// Проверяет логику равенства двух экземпляров <see cref="Tag"/> с одинаковыми названиями.
        /// </summary>
        /// <param name="first"> Название первого тега. </param>
        /// <param name="second"> Название второго тега. </param>
        /// <param name="expected"> Ожидаемый результат сравнения. </param>
        [TestCase("Tag", "Tag", true)]
        [TestCase("Tag", "New", false)]
        public void Equals_Success(string first, string second, bool expected)
        {
            // Arrange
            // Используем две разные категории, чтобы избежать конфликта дубликатов в одной категории
            var category1 = TestData.ValidCategory().WithName("Category1").Build();
            var category2 = TestData.ValidCategory().WithName("Category2").Build();

            var left = new Tag(first, category1);
            var right = new Tag(second, category2);

            // Act
            var actual = left.Equals(right);

            // Assert
            Assert.That(actual, Is.EqualTo(expected));
        }

        /// <summary>
        /// Проверяет, что сравнение тега с null возвращает false.
        /// </summary>
        [Test]
        public void Equals_Null_ReturnsFalse()
        {
            // Arrange
            var category = TestData.ValidCategory().Build();
            var tag = new Tag("Tag", category);

            // Act & Assert
            Assert.That(tag.Equals(null), Is.False);
        }

        /// <summary>
        /// Проверяет, что сравнение тега с объектом другого типа возвращает false.
        /// </summary>
        [Test]
        public void Equals_DifferentType_ReturnsFalse()
        {
            // Arrange
            var category = TestData.ValidCategory().Build();
            var tag = new Tag("Tag", category);

            // Act & Assert
            Assert.That(tag.Equals("not a tag"), Is.False);
        }

        /// <summary>
        /// Проверяет, что метод ToString возвращает название тега.
        /// </summary>
        [Test]
        public void ToString_ReturnsName()
        {
            // Arrange
            var category = TestData.ValidCategory().Build();
            var tag = new Tag("TestTag", category);

            // Act
            var result = tag.ToString();

            // Assert
            Assert.That(result, Is.EqualTo("TestTag"));
        }

        // ==========================================
        // Тесты двусторонней связи с книгами (Books)
        // ==========================================

        /// <summary>
        /// Проверяет, что коллекция книг у нового тега изначально пуста.
        /// </summary>
        [Test]
        public void Books_Collection_StartsEmpty()
        {
            // Arrange & Act
            var tag = TestData.ValidTag().Build();

            // Assert
            Assert.That(tag.Books, Is.Empty);
        }

        /// <summary>
        /// Проверяет, что добавление валидного тега к книге устанавливает двустороннюю связь.
        /// </summary>
        [Test]
        public void Book_AddTag_ValidTag_AddsToBothCollections()
        {
            // Arrange
            var book = TestData.ValidBook().Build();
            var tag = TestData.ValidTag().Build();

            // Act
            var result = book.AddTag(tag);

            // Assert
            using (Assert.EnterMultipleScope())
            {
                Assert.That(result, Is.True);
                Assert.That(book.Tags, Contains.Item(tag));
                Assert.That(tag.Books, Contains.Item(book));
            }
        }

        /// <summary>
        /// Проверяет, что попытка добавить null в качестве тега к книге возвращает false.
        /// </summary>
        [Test]
        public void Book_AddTag_NullTag_ReturnsFalse()
        {
            // Arrange
            var book = TestData.ValidBook().Build();

            // Act
            var result = book.AddTag(null!);

            // Assert
            using (Assert.EnterMultipleScope())
            {
                Assert.That(result, Is.False);
                Assert.That(book.Tags, Is.Empty);
            }
        }

        /// <summary>
        /// Проверяет, что попытка добавить один и тот же тег к книге дважды возвращает false при втором вызове.
        /// </summary>
        [Test]
        public void Book_AddTag_DuplicateTag_ReturnsFalse()
        {
            // Arrange
            var book = TestData.ValidBook().Build();
            var tag = TestData.ValidTag().Build();
            _ = book.AddTag(tag);

            // Act
            var result = book.AddTag(tag);

            // Assert
            Assert.That(result, Is.False);
            Assert.That(book.Tags, Has.Count.EqualTo(1));
        }

        /// <summary>
        /// Проверяет, что удаление существующего тега у книги разрывает двустороннюю связь.
        /// </summary>
        [Test]
        public void Book_RemoveTag_ExistingTag_RemovesFromBothCollections()
        {
            // Arrange
            var book = TestData.ValidBook().Build();
            var tag = TestData.ValidTag().Build();
            _ = book.AddTag(tag);

            // Act
            var result = book.RemoveTag(tag);

            // Assert
            using (Assert.EnterMultipleScope())
            {
                Assert.That(result, Is.True);
                Assert.That(book.Tags, Does.Not.Contain(tag));
                Assert.That(tag.Books, Does.Not.Contain(book));
            }
        }

        /// <summary>
        /// Проверяет, что попытка удалить тег, не связанный с книгой, возвращает false.
        /// </summary>
        [Test]
        public void Book_RemoveTag_NonExistingTag_ReturnsFalse()
        {
            // Arrange
            var book = TestData.ValidBook().Build();
            var tag = TestData.ValidTag().Build();

            // Act
            var result = book.RemoveTag(tag);

            // Assert
            Assert.That(result, Is.False);
        }

        /// <summary>
        /// Проверяет, что попытка удалить null в качестве тега у книги возвращает false.
        /// </summary>
        [Test]
        public void Book_RemoveTag_NullTag_ReturnsFalse()
        {
            // Arrange
            var book = TestData.ValidBook().Build();

            // Act
            var result = book.RemoveTag(null!);

            // Assert
            Assert.That(result, Is.False);
        }
    }
}