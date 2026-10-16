// <copyright file="RoomTests.cs" company="Филипченко Марина Алексеевна">
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
    /// Модульные тесты для класса <see cref="Room"/>.
    /// </summary>
    [TestFixture]
    internal sealed class RoomTests
    {
        /// <summary>
        /// Проверяет, что конструктор <see cref="Room"/> выбрасывает исключение при передаче null вместо адреса.
        /// </summary>
        [Test]
        public void Ctor_NullAddress_ThrowsArgumentNullException()
        {
            // Arrange, Act & Assert
            Assert.Throws<ArgumentNullException>(() => _ = new Room(null!, "Комната"));
        }

        /// <summary>
        /// Проверяет, что конструктор <see cref="Room"/> выбрасывает исключение при передаче null в качестве названия.
        /// </summary>
        [Test]
        public void Ctor_NullName_ThrowsArgumentNullException()
        {
            // Arrange
            var address = TestData.ValidAddress().Build();

            // Act & Assert
            Assert.Throws<ArgumentNullException>(() => _ = new Room(address, null!));
        }

        /// <summary>
        /// Проверяет, что конструктор <see cref="Room"/> выбрасывает исключение при передаче строки из пробелов в качестве названия.
        /// </summary>
        [Test]
        public void Ctor_EmptyName_AfterTrim_ThrowsArgumentNullException()
        {
            // Arrange
            var address = TestData.ValidAddress().Build();

            // Act & Assert
            Assert.Throws<ArgumentNullException>(() => _ = new Room(address, "   "));
        }

        /// <summary>
        /// Проверяет успешное создание <see cref="Room"/> с валидными данными.
        /// </summary>
        [Test]
        public void Ctor_ValidData_Success()
        {
            // Arrange
            var city = TestData.ValidCity().WithName("Москва").Build();
            var street = TestData.ValidStreet().WithName("Ленина").WithCity(city).Build();
            var address = TestData.ValidAddress().WithStreet(street).WithApartment(10).Build();
            var name = "Кабинет директора";

            // Act
            var room = new Room(address, name);

            // Assert
            using (Assert.EnterMultipleScope())
            {
                Assert.That(room.Address, Is.SameAs(address));
                Assert.That(room.Name.Value, Is.EqualTo(name));
                Assert.That(room.Cabinets, Is.Empty);
            }
        }

        /// <summary>
        /// Проверяет, что создание шкафа автоматически добавляет его в коллекцию комнаты (двусторонняя связь).
        /// </summary>
        [Test]
        public void AddCabinet_ValidCabinet_AddsToBothCollections()
        {
            // Arrange
            var room = TestData.ValidRoom().Build();

            // Act
            var cabinet = new Cabinet(room, "Шкаф 1");

            // Assert
            using (Assert.EnterMultipleScope())
            {
                Assert.That(room.Cabinets, Contains.Item(cabinet));
                Assert.That(cabinet.Room, Is.SameAs(room));
            }
        }

        /// <summary>
        /// Проверяет, что попытка добавить null в качестве шкафа возвращает false.
        /// </summary>
        [Test]
        public void AddCabinet_NullCabinet_ReturnsFalse()
        {
            // Arrange
            var room = TestData.ValidRoom().Build();

            // Act
            var result = room.AddCabinet(null!);

            // Assert
            using (Assert.EnterMultipleScope())
            {
                Assert.That(result, Is.False);
                Assert.That(room.Cabinets, Is.Empty);
            }
        }

        /// <summary>
        /// Проверяет, что попытка добавить уже существующий шкаф возвращает false.
        /// </summary>
        [Test]
        public void AddCabinet_DuplicateCabinet_ReturnsFalse()
        {
            // Arrange
            var room = TestData.ValidRoom().Build();
            var cabinet = new Cabinet(room, "Шкаф 1"); // Автоматически добавляется в комнату

            // Act
            var result = room.AddCabinet(cabinet);

            // Assert
            using (Assert.EnterMultipleScope())
            {
                Assert.That(result, Is.False);
                Assert.That(room.Cabinets, Has.Count.EqualTo(1));
            }
        }

        /// <summary>
        /// Проверяет, что удаление существующего шкафа разрывает двустороннюю связь.
        /// </summary>
        [Test]
        public void RemoveCabinet_ExistingCabinet_RemovesFromBothCollections()
        {
            // Arrange
            var room = TestData.ValidRoom().Build();
            var cabinet = new Cabinet(room, "Шкаф 1");

            // Act
            var result = room.RemoveCabinet(cabinet);

            // Assert
            using (Assert.EnterMultipleScope())
            {
                Assert.That(result, Is.True);
                Assert.That(room.Cabinets, Does.Not.Contain(cabinet));
                Assert.That(cabinet.Room, Is.Null);
            }
        }

        /// <summary>
        /// Проверяет, что попытка удалить шкаф, принадлежащий другой комнате, возвращает false.
        /// </summary>
        [Test]
        public void RemoveCabinet_NonExistingCabinet_ReturnsFalse()
        {
            // Arrange
            var room = TestData.ValidRoom().WithName("Комната").Build();
            var otherRoom = TestData.ValidRoom().WithName("Другая Комната").Build();
            var cabinet = TestData.ValidCabinet().WithName("Чужой шкаф").WithRoom(otherRoom).Build();

            // Act
            var result = room.RemoveCabinet(cabinet);

            // Assert
            using (Assert.EnterMultipleScope())
            {
                Assert.That(result, Is.False);
                Assert.That(cabinet.Room, Is.SameAs(otherRoom));
            }
        }

        /// <summary>
        /// Проверяет, что попытка удалить null в качестве шкафа возвращает false.
        /// </summary>
        [Test]
        public void RemoveCabinet_NullCabinet_ReturnsFalse()
        {
            // Arrange
            var room = TestData.ValidRoom().WithName("Комната").Build();

            // Act
            var result = room.RemoveCabinet(null!);

            // Assert
            Assert.That(result, Is.False);
        }

        /// <summary>
        /// Проверяет, что сравнение комнаты с самой собой возвращает true.
        /// </summary>
        [Test]
        public void Equals_SameReference_ReturnsTrue()
        {
            // Arrange
            var room = TestData.ValidRoom().WithName("Комната").Build();

            // Act & Assert
            Assert.That(room.Equals(room), Is.True);
        }

        /// <summary>
        /// Проверяет, что сравнение комнаты с null возвращает false.
        /// </summary>
        [Test]
        public void Equals_Null_ReturnsFalse()
        {
            // Arrange
            var room = TestData.ValidRoom().WithName("Комната").Build();

            // Act & Assert
            Assert.That(room.Equals(null), Is.False);
        }

        /// <summary>
        /// Проверяет, что сравнение комнаты с объектом другого типа возвращает false.
        /// </summary>
        [Test]
        public void Equals_DifferentType_ReturnsFalse()
        {
            // Arrange
            var room = TestData.ValidRoom().WithName("Комната").Build();

            // Act & Assert
            Assert.That(room.Equals("not a room"), Is.False);
        }

        /// <summary>
        /// Проверяет, что комнаты с одинаковым адресом и названием считаются равными.
        /// </summary>
        [Test]
        public void Equals_SameAddressAndName_ReturnsTrue()
        {
            // Arrange
            var room1 = TestData.ValidRoom().WithName("Кабинет").Build();
            var room2 = TestData.ValidRoom().WithName("Кабинет").Build();

            // Act & Assert
            Assert.That(room1, Is.EqualTo(room2));
        }

        /// <summary>
        /// Проверяет, что комнаты с разными адресами не равны, даже если названия совпадают.
        /// </summary>
        [Test]
        public void Equals_DifferentAddress_ReturnsFalse()
        {
            // Arrange
            var city1 = TestData.ValidCity().WithName("Москва").Build();
            var city2 = TestData.ValidCity().WithName("Санкт-Петербург").Build();
            var street1 = TestData.ValidStreet().WithName("Ленина").WithCity(city1).Build();
            var street2 = TestData.ValidStreet().WithName("Ленина").WithCity(city2).Build();
            var address1 = TestData.ValidAddress().WithStreet(street1).WithApartment(10).Build();
            var address2 = TestData.ValidAddress().WithStreet(street2).WithApartment(10).Build();
            var room1 = TestData.ValidRoom().WithName("Кабинет").WithAddress(address1).Build();
            var room2 = TestData.ValidRoom().WithName("Кабинет").WithAddress(address2).Build();

            // Act
            var actual = room1.Equals(room2);

            // Assert
            Assert.That(actual, Is.False);
        }

        /// <summary>
        /// Проверяет, что комнаты с разными названиями не равны.
        /// </summary>
        [Test]
        public void Equals_DifferentName_ReturnsFalse()
        {
            // Arrange
            var room1 = TestData.ValidRoom().WithName("Кабинет").Build();
            var room2 = TestData.ValidRoom().WithName("Спальня").Build();

            // Act & Assert
            Assert.That(room1, Is.Not.EqualTo(room2));
        }

        /// <summary>
        /// Проверяет, что комнаты с одинаковым адресом и названием имеют одинаковый хеш-код.
        /// </summary>
        [Test]
        public void GetHashCode_SameAddressAndName_SameHashCode()
        {
            // Arrange
            var room1 = TestData.ValidRoom().WithName("Кабинет").Build();
            var room2 = TestData.ValidRoom().WithName("Кабинет").Build();

            // Act & Assert
            Assert.That(room1.GetHashCode(), Is.EqualTo(room2.GetHashCode()));
        }

        /// <summary>
        /// Проверяет, что комнаты с разными адресами имеют разные хеш-коды и не равны.
        /// </summary>
        [Test]
        public void GetHashCode_DifferentAddress_DifferentHashCode()
        {
            // Arrange
            var city1 = TestData.ValidCity().WithName("Москва").Build();
            var city2 = TestData.ValidCity().WithName("Санкт-Петербург").Build();
            var street1 = TestData.ValidStreet().WithName("Ленина").WithCity(city1).Build();
            var street2 = TestData.ValidStreet().WithName("Ленина").WithCity(city2).Build();
            var address1 = TestData.ValidAddress().WithStreet(street1).WithApartment(10).Build();
            var address2 = TestData.ValidAddress().WithStreet(street2).WithApartment(10).Build();
            var room1 = TestData.ValidRoom().WithName("Кабинет").WithAddress(address1).Build();
            var room2 = TestData.ValidRoom().WithName("Кабинет").WithAddress(address2).Build();

            // Act & Assert
            Assert.That(room1, Is.Not.EqualTo(room2));
            Assert.That(room1.GetHashCode(), Is.Not.EqualTo(room2.GetHashCode()));
        }

        /// <summary>
        /// Проверяет, что метод ToString возвращает название комнаты и адрес, если шкафов нет.
        /// </summary>
        [Test]
        public void ToString_WithoutCabinets_ReturnsRoomNameAndAddress()
        {
            // Arrange
            var city = TestData.ValidCity().WithName("Москва").Build();
            var street = TestData.ValidStreet().WithName("Ленина").WithCity(city).Build();
            var address = TestData.ValidAddress().WithStreet(street).WithApartment(10).Build();
            var room = TestData.ValidRoom().WithName("Кабинет").WithAddress(address).Build();

            // Act
            var result = room.ToString();

            // Assert
            using (Assert.EnterMultipleScope())
            {
                Assert.That(result, Does.Contain("Кабинет"));
                Assert.That(result, Does.Contain("Москва"));
                Assert.That(result, Does.Contain("Ленина"));
            }
        }

        /// <summary>
        /// Проверяет, что метод ToString включает список шкафов, если они есть.
        /// </summary>
        [Test]
        public void ToString_WithCabinets_IncludesCabinetsList()
        {
            // Arrange
            var room = TestData.ValidRoom().WithName("Комната").Build();
            var cabinet1 = new Cabinet(room, "Шкаф 1");
            var cabinet2 = new Cabinet(room, "Шкаф 2");

            // Act
            var result = room.ToString();

            // Assert
            using (Assert.EnterMultipleScope())
            {
                Assert.That(room.Cabinets, Does.Contain(cabinet1), "Шкаф 1 должен быть в коллекции комнаты");
                Assert.That(room.Cabinets, Does.Contain(cabinet2), "Шкаф 2 должен быть в коллекции комнаты");

                Assert.That(result, Does.Contain("Комната"));
                Assert.That(result, Does.Contain("Шкаф 1"));
                Assert.That(result, Does.Contain("Шкаф 2"));
            }
        }

        /// <summary>
        /// Проверяет, что коллекция шкафов изначально пуста.
        /// </summary>
        [Test]
        public void Cabinets_Collection_StartsEmpty()
        {
            // Arrange & Act
            var room = TestData.ValidRoom().WithName("Комната").Build();

            // Assert
            Assert.That(room.Cabinets, Is.Empty);
        }

        /// <summary>
        /// Проверяет, что ссылка на коллекцию шкафов возвращает корректный тип ISet.
        /// </summary>
        [Test]
        public void Cabinets_Collection_IsReadOnlyExternally()
        {
            // Arrange
            var room = TestData.ValidRoom().WithName("Комната").Build();
            var cabinetsReference = room.Cabinets;

            // Act & Assert
            using (Assert.EnterMultipleScope())
            {
                Assert.That(cabinetsReference, Is.Not.Null);
                Assert.That(cabinetsReference, Is.InstanceOf<ISet<Cabinet>>());
            }
        }
    }
}