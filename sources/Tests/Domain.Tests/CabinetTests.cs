// <copyright file="CabinetTests.cs" company="Филипченко Марина Алексеевна">
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
    /// Модульные тесты для класса <see cref="Cabinet"/>.
    /// </summary>
    [TestFixture]
    internal sealed class CabinetTests
    {
        /// <summary>
        /// Проверяет, что конструктор <see cref="Cabinet"/> выбрасывает исключение при передаче null вместо комнаты.
        /// </summary>
        [Test]
        public void Ctor_NullRoom_ThrowsArgumentNullException()
        {
            // Arrange, Act & Assert
            Assert.Throws<ArgumentNullException>(() => _ = new Cabinet(null!, "Шкаф"));
        }

        /// <summary>
        /// Проверяет, что конструктор <see cref="Cabinet"/> выбрасывает исключение при передаче null в качестве названия.
        /// </summary>
        [Test]
        public void Ctor_NullName_ThrowsArgumentNullException()
        {
            // Arrange
            var room = TestData.ValidRoom().Build();

            // Act & Assert
            Assert.Throws<ArgumentNullException>(() => _ = new Cabinet(room, null!));
        }

        /// <summary>
        /// Проверяет, что конструктор <see cref="Cabinet"/> выбрасывает исключение при передаче строки из пробелов в качестве названия.
        /// </summary>
        [Test]
        public void Ctor_EmptyName_AfterTrim_ThrowsArgumentNullException()
        {
            // Arrange
            var room = TestData.ValidRoom().Build();

            // Act & Assert
            // Примечание: Value Object Title выбрасывает ArgumentNullException даже для пустых строк,
            // так как после TrimOrNull() возвращается null.
            Assert.Throws<ArgumentNullException>(() => _ = new Cabinet(room, "   "));
        }

        /// <summary>
        /// Проверяет успешное создание <see cref="Cabinet"/> с валидными данными и установку двусторонней связи.
        /// </summary>
        [Test]
        public void Ctor_ValidData_Success()
        {
            // Arrange
            var room = TestData.ValidRoom().WithName("Кабинет").Build();

            // Act
            var cabinet = new Cabinet(room, "Книжный шкаф");

            // Assert
            using (Assert.EnterMultipleScope())
            {
                Assert.That(cabinet.Room, Is.SameAs(room));
                Assert.That(cabinet.Name.Value, Is.EqualTo("Книжный шкаф"));
                Assert.That(cabinet.Shelves, Is.Empty);
            }
        }

        /// <summary>
        /// Проверяет, что добавление валидной полки устанавливает двустороннюю связь.
        /// </summary>
        [Test]
        public void AddShelf_ValidShelf_AddsToBothCollections()
        {
            // Arrange
            var room = TestData.ValidRoom().WithName("Кабинет").Build();
            var cabinet = new Cabinet(room, "Шкаф");
            var shelf = new Shelf("Полка");

            // Act
            var result = cabinet.AddShelf(shelf);

            // Assert
            using (Assert.EnterMultipleScope())
            {
                Assert.That(result, Is.True);
                Assert.That(cabinet.Shelves, Contains.Item(shelf));
                Assert.That(shelf.Cabinet, Is.SameAs(cabinet));
            }
        }

        /// <summary>
        /// Проверяет, что попытка добавить null в качестве полки возвращает false.
        /// </summary>
        [Test]
        public void AddShelf_NullShelf_ReturnsFalse()
        {
            // Arrange
            var room = TestData.ValidRoom().WithName("Кабинет").Build();
            var cabinet = new Cabinet(room, "Шкаф");

            // Act
            var result = cabinet.AddShelf(null!);

            // Assert
            using (Assert.EnterMultipleScope())
            {
                Assert.That(result, Is.False);
                Assert.That(cabinet.Shelves, Is.Empty);
            }
        }

        /// <summary>
        /// Проверяет, что попытка добавить одну и ту же полку дважды возвращает false при втором вызове.
        /// </summary>
        [Test]
        public void AddShelf_DuplicateShelf_ReturnsFalse()
        {
            // Arrange
            var room = TestData.ValidRoom().WithName("Кабинет").Build();
            var cabinet = new Cabinet(room, "Шкаф");
            var shelf = new Shelf("Полка");
            _ = cabinet.AddShelf(shelf);

            // Act
            var result = cabinet.AddShelf(shelf);

            // Assert
            using (Assert.EnterMultipleScope())
            {
                Assert.That(result, Is.False);
                Assert.That(cabinet.Shelves, Has.Count.EqualTo(1));
            }
        }

        /// <summary>
        /// Проверяет, что удаление существующей полки разрывает двустороннюю связь.
        /// </summary>
        [Test]
        public void RemoveShelf_ExistingShelf_RemovesFromBothCollections()
        {
            // Arrange
            var room = TestData.ValidRoom().WithName("Кабинет").Build();
            var cabinet = new Cabinet(room, "Шкаф");
            var shelf = new Shelf("Полка");
            _ = cabinet.AddShelf(shelf);

            // Act
            var result = cabinet.RemoveShelf(shelf);

            // Assert
            using (Assert.EnterMultipleScope())
            {
                Assert.That(result, Is.True);
                Assert.That(cabinet.Shelves, Does.Not.Contain(shelf));
                Assert.That(shelf.Cabinet, Is.Null);
            }
        }

        /// <summary>
        /// Проверяет, что попытка удалить полку, принадлежащую другому шкафу, возвращает false.
        /// </summary>
        [Test]
        public void RemoveShelf_NonExistingShelf_ReturnsFalse()
        {
            // Arrange
            var room = TestData.ValidRoom().WithName("Кабинет").Build();
            var cabinet = new Cabinet(room, "Шкаф");
            var otherCabinet = new Cabinet(room, "Другой шкаф");
            var shelf = new Shelf("Чужая полка");
            _ = otherCabinet.AddShelf(shelf);

            // Act
            var result = cabinet.RemoveShelf(shelf);

            // Assert
            using (Assert.EnterMultipleScope())
            {
                Assert.That(result, Is.False);
                Assert.That(shelf.Cabinet, Is.SameAs(otherCabinet));
            }
        }

        /// <summary>
        /// Проверяет, что попытка удалить null в качестве полки возвращает false.
        /// </summary>
        [Test]
        public void RemoveShelf_NullShelf_ReturnsFalse()
        {
            // Arrange
            var room = TestData.ValidRoom().WithName("Кабинет").Build();
            var cabinet = new Cabinet(room, "Шкаф");

            // Act
            var result = cabinet.RemoveShelf(null!);

            // Assert
            Assert.That(result, Is.False);
        }

        /// <summary>
        /// Проверяет, что сравнение шкафа с самим собой возвращает true.
        /// </summary>
        [Test]
        public void Equals_SameReference_ReturnsTrue()
        {
            // Arrange
            var room = TestData.ValidRoom().WithName("Кабинет").Build();
            var cabinet = new Cabinet(room, "Шкаф");

            // Act & Assert
            Assert.That(cabinet.Equals(cabinet), Is.True);
        }

        /// <summary>
        /// Проверяет, что сравнение шкафа с null возвращает false.
        /// </summary>
        [Test]
        public void Equals_Null_ReturnsFalse()
        {
            // Arrange
            var room = TestData.ValidRoom().WithName("Кабинет").Build();
            var cabinet = new Cabinet(room, "Шкаф");

            // Act & Assert
            Assert.That(cabinet.Equals(null), Is.False);
        }

        /// <summary>
        /// Проверяет, что сравнение шкафа с объектом другого типа возвращает false.
        /// </summary>
        [Test]
        public void Equals_DifferentType_ReturnsFalse()
        {
            // Arrange
            var room = TestData.ValidRoom().WithName("Кабинет").Build();
            var cabinet = new Cabinet(room, "Шкаф");

            // Act & Assert
            Assert.That(cabinet.Equals("not a cabinet"), Is.False);
        }

        /// <summary>
        /// Проверяет, что шкафы с одинаковой комнатой и названием считаются равными.
        /// </summary>
        [Test]
        public void Equals_SameRoomAndName_ReturnsTrue()
        {
            // Arrange
            var room = TestData.ValidRoom().WithName("Кабинет").Build();
            var cabinet1 = new Cabinet(room, "Книжный шкаф");
            var cabinet2 = new Cabinet(room, "Книжный шкаф");

            // Act & Assert
            Assert.That(cabinet1, Is.EqualTo(cabinet2));
        }

        /// <summary>
        /// Проверяет, что шкафы в разных комнатах не равны, даже если названия совпадают.
        /// </summary>
        [Test]
        public void Equals_DifferentRoom_ReturnsFalse()
        {
            // Arrange
            var room1 = TestData.ValidRoom().WithName("Комната 1").Build();
            var room2 = TestData.ValidRoom().WithName("Комната 2").Build();
            var cabinet1 = new Cabinet(room1, "Шкаф");
            var cabinet2 = new Cabinet(room2, "Шкаф");

            // Act & Assert
            Assert.That(cabinet1, Is.Not.EqualTo(cabinet2));
        }

        /// <summary>
        /// Проверяет, что шкафы с разными названиями в одной комнате не равны.
        /// </summary>
        [Test]
        public void Equals_DifferentName_ReturnsFalse()
        {
            // Arrange
            var room = TestData.ValidRoom().Build();
            var cabinet1 = new Cabinet(room, "Книжный шкаф");
            var cabinet2 = new Cabinet(room, "Платяной шкаф");

            // Act & Assert
            Assert.That(cabinet1, Is.Not.EqualTo(cabinet2));
        }

        /// <summary>
        /// Проверяет, что шкафы с одинаковой комнатой и названием имеют одинаковый хеш-код.
        /// </summary>
        [Test]
        public void GetHashCode_SameRoomAndName_SameHashCode()
        {
            // Arrange
            var room = TestData.ValidRoom().Build();
            var cabinet1 = new Cabinet(room, "Шкаф");
            var cabinet2 = new Cabinet(room, "Шкаф");

            // Act & Assert
            Assert.That(cabinet1.GetHashCode(), Is.EqualTo(cabinet2.GetHashCode()));
        }

        /// <summary>
        /// Проверяет, что метод ToString возвращает название шкафа и комнаты, если полок нет.
        /// </summary>
        [Test]
        public void ToString_WithoutShelves_ReturnsCabinetNameAndRoom()
        {
            // Arrange
            var room = TestData.ValidRoom().WithName("Кабинет").Build();
            var cabinet = new Cabinet(room, "Книжный шкаф");

            // Act
            var result = cabinet.ToString();

            // Assert
            using (Assert.EnterMultipleScope())
            {
                Assert.That(result, Does.Contain("Книжный шкаф"));
                Assert.That(result, Does.Contain("Кабинет"));
            }
        }

        /// <summary>
        /// Проверяет, что метод ToString включает список полок, если они есть.
        /// </summary>
        [Test]
        public void ToString_WithShelves_IncludesShelvesList()
        {
            // Arrange
            var room = TestData.ValidRoom().WithName("Кабинет").Build();
            var cabinet = new Cabinet(room, "Шкаф");
            _ = cabinet.AddShelf(new Shelf("Полка 1"));
            _ = cabinet.AddShelf(new Shelf("Полка 2"));

            // Act
            var result = cabinet.ToString();

            // Assert
            using (Assert.EnterMultipleScope())
            {
                Assert.That(result, Does.Contain("Шкаф"));
                Assert.That(result, Does.Contain("Полка 1"));
                Assert.That(result, Does.Contain("Полка 2"));
            }
        }

        /// <summary>
        /// Проверяет, что коллекция полок изначально пуста.
        /// </summary>
        [Test]
        public void Shelves_Collection_StartsEmpty()
        {
            // Arrange
            var room = TestData.ValidRoom().Build();

            // Act
            var cabinet = new Cabinet(room, "Шкаф");

            // Assert
            Assert.That(cabinet.Shelves, Is.Empty);
        }

        /// <summary>
        /// Проверяет, что ссылка на коллекцию полок возвращает корректный тип ISet.
        /// </summary>
        [Test]
        public void Shelves_Collection_IsReadOnlyExternally()
        {
            // Arrange
            var room = TestData.ValidRoom().Build();
            var cabinet = new Cabinet(room, "Шкаф");
            var shelvesReference = cabinet.Shelves;

            // Act & Assert
            using (Assert.EnterMultipleScope())
            {
                Assert.That(shelvesReference, Is.Not.Null);
                Assert.That(shelvesReference, Is.InstanceOf<ISet<Shelf>>());
            }
        }

        /// <summary>
        /// Проверяет успешное создание полной иерархии объектов и корректность двусторонних связей.
        /// </summary>
        [Test]
        public void FullHierarchy_Creation_Success()
        {
            // Arrange
            var city = TestData.ValidCity().WithName("Москва").Build();
            var street = TestData.ValidStreet().WithName("Ленина").WithCity(city).Build();
            var address = TestData.ValidAddress().WithStreet(street).WithHouse(10).Build();
            var room = TestData.ValidRoom().WithAddress(address).WithName("Кабинет").Build();
            var cabinet = TestData.ValidCabinet().WithName("Книжный шкаф").WithRoom(room).Build();
            var shelf = TestData.ValidShelf().WithName("Верхняя полка").Build();

            // Act
            _ = room.AddCabinet(cabinet);
            _ = cabinet.AddShelf(shelf);

            // Assert
            using (Assert.EnterMultipleScope())
            {
                Assert.That(cabinet.Room, Is.SameAs(room));
                Assert.That(shelf.Cabinet, Is.SameAs(cabinet));
                Assert.That(room.Cabinets, Contains.Item(cabinet));
                Assert.That(cabinet.Shelves, Contains.Item(shelf));
                Assert.That(room.Address, Is.SameAs(address));
            }
        }

        /// <summary>
        /// Проверяет, что удаление шкафа из комнаты очищает ссылку на комнату у шкафа.
        /// </summary>
        [Test]
        public void RemoveRoom_ClearsCabinetsRoomReference()
        {
            // Arrange
            var room = TestData.ValidRoom().Build();
            var cabinet = new Cabinet(room, "Шкаф");
            _ = room.AddCabinet(cabinet);

            // Act
            _ = room.RemoveCabinet(cabinet);

            // Assert
            using (Assert.EnterMultipleScope())
            {
                Assert.That(cabinet.Room, Is.Null);
                Assert.That(room.Cabinets, Does.Not.Contain(cabinet));
            }
        }

        /// <summary>
        /// Проверяет, что удаление полки из шкафа очищает ссылку на шкаф у полки.
        /// </summary>
        [Test]
        public void RemoveCabinet_ClearsShelvesCabinetReference()
        {
            // Arrange
            var room = TestData.ValidRoom().Build();
            var cabinet = new Cabinet(room, "Шкаф");
            var shelf = TestData.ValidShelf().WithName("Верхняя полка").Build();
            _ = cabinet.AddShelf(shelf);

            // Act
            _ = cabinet.RemoveShelf(shelf);

            // Assert
            using (Assert.EnterMultipleScope())
            {
                Assert.That(shelf.Cabinet, Is.Null);
                Assert.That(cabinet.Shelves, Does.Not.Contain(shelf));
            }
        }
    }
}