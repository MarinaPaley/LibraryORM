// <copyright file="RoomTests.cs" company="Филипченко Марина Алексеевна">
// Copyright (c) Филипченко Марина Алексеевна 2026. Library.
// </copyright>

namespace Domain.Tests
{
    using System;
    using System.Collections.Generic;
    using NUnit.Framework;

    /// <summary>
    /// Тесты для <see cref="Domain.Room"/>.
    /// </summary>
    [TestFixture]
    internal sealed class RoomTests
    {
        #region Test data helpers

        private static Address CreateAddress(string city = "Город", string street = "Улица", int house = 1) =>
            new(new City(city), new Street(street, new City(city)), house);

        private static Cabinet CreateCabinet(Room room, string name = "Шкаф") => new(room, name);

        #endregion

        #region Constructor validation tests

        [Test]
        public void Ctor_NullAddress_ThrowsArgumentNullException()
        {
            // arrange & act & assert
            Assert.Throws<ArgumentNullException>(() =>
                _ = new Room(null!, "Комната"));
        }

        [Test]
        public void Ctor_NullName_ThrowsArgumentNullException()
        {
            // arrange
            var address = CreateAddress();

            // act & assert
            Assert.Throws<ArgumentNullException>(() =>
                _ = new Room(address, null!));
        }

        [Test]
        public void Ctor_EmptyName_AfterTrim_ThrowsArgumentNullException()
        {
            // arrange
            var address = CreateAddress();

            // act & assert
            Assert.Throws<ArgumentNullException>(() =>
                _ = new Room(address, "   "));
        }

        [Test]
        public void Ctor_ValidData_Success()
        {
            // arrange
            var address = CreateAddress("Москва", "Ленина", 10);

            // act
            var room = new Room(address, "Кабинет директора");

            // assert
            Assert.That(room.Address, Is.SameAs(address));
            Assert.That(room.Name.Value, Is.EqualTo("Кабинет директора"));
            Assert.That(room.Cabinets, Is.Empty);
        }

        #endregion

        #region Bidirectional relationship tests

        [Test]
        public void AddCabinet_ValidCabinet_AddsToBothCollections()
        {
            // arrange
            var room = new Room(CreateAddress(), "Комната");
            var cabinet = new Cabinet(room, "Шкаф");

            // act
            var result = room.AddCabinet(cabinet);

            // assert
            Assert.That(result, Is.True);
            Assert.That(room.Cabinets, Contains.Item(cabinet));
            Assert.That(cabinet.Room, Is.SameAs(room));
        }

        [Test]
        public void AddCabinet_NullCabinet_ReturnsFalse()
        {
            // arrange
            var room = new Room(CreateAddress(), "Комната");

            // act
            var result = room.AddCabinet(null!);

            // assert
            Assert.That(result, Is.False);
            Assert.That(room.Cabinets, Is.Empty);
        }

        [Test]
        public void AddCabinet_DuplicateCabinet_ReturnsFalse()
        {
            // arrange
            var room = new Room(CreateAddress(), "Комната");
            var cabinet = new Cabinet(room, "Шкаф");
            room.AddCabinet(cabinet);

            // act
            var result = room.AddCabinet(cabinet);

            // assert
            Assert.That(result, Is.False);
            Assert.That(room.Cabinets.Count, Is.EqualTo(1));
        }

        [Test]
        public void RemoveCabinet_ExistingCabinet_RemovesFromBothCollections()
        {
            // arrange
            var room = new Room(CreateAddress(), "Комната");
            var cabinet = new Cabinet(room, "Шкаф");
            room.AddCabinet(cabinet);

            // act
            var result = room.RemoveCabinet(cabinet);

            // assert
            Assert.That(result, Is.True);
            Assert.That(room.Cabinets, Does.Not.Contain(cabinet));
            Assert.That(cabinet.Room, Is.Null);
        }

        [Test]
        public void RemoveCabinet_NonExistingCabinet_ReturnsFalse()
        {
            // arrange
            var room = new Room(CreateAddress(), "Комната");
            var otherRoom = new Room(CreateAddress(), "Другая комната");
            var cabinet = new Cabinet(otherRoom, "Чужой шкаф");

            // act
            var result = room.RemoveCabinet(cabinet);

            // assert
            Assert.That(result, Is.False);
            Assert.That(cabinet.Room, Is.SameAs(otherRoom));
        }

        [Test]
        public void RemoveCabinet_NullCabinet_ReturnsFalse()
        {
            // arrange
            var room = new Room(CreateAddress(), "Комната");

            // act
            var result = room.RemoveCabinet(null!);

            // assert
            Assert.That(result, Is.False);
        }

        #endregion

        #region Equality tests

        [Test]
        public void Equals_SameReference_ReturnsTrue()
        {
            // arrange
            var room = new Room(CreateAddress(), "Комната");

            // act & assert
            Assert.That(room.Equals(room), Is.True);
        }

        [Test]
        public void Equals_Null_ReturnsFalse()
        {
            // arrange
            var room = new Room(CreateAddress(), "Комната");

            // act & assert
            Assert.That(room.Equals(null), Is.False);
        }

        [Test]
        public void Equals_DifferentType_ReturnsFalse()
        {
            // arrange
            var room = new Room(CreateAddress(), "Комната");

            // act & assert
            Assert.That(room.Equals("not a room"), Is.False);
        }

        [Test]
        public void Equals_SameAddressAndName_ReturnsTrue()
        {
            // arrange
            var address = CreateAddress();
            var room1 = new Room(address, "Кабинет");
            var room2 = new Room(address, "Кабинет");

            // act & assert
            Assert.That(room1, Is.EqualTo(room2));
        }

        [Test]
        public void Equals_DifferentAddress_ReturnsFalse()
        {
            // arrange
            var address1 = CreateAddress("Москва");
            var address2 = CreateAddress("Санкт-Петербург");
            var room1 = new Room(address1, "Кабинет");
            var room2 = new Room(address2, "Кабинет");

            // act & assert
            Assert.That(room1, Is.Not.EqualTo(room2));
        }

        [Test]
        public void Equals_DifferentName_ReturnsFalse()
        {
            // arrange
            var address = CreateAddress();
            var room1 = new Room(address, "Кабинет");
            var room2 = new Room(address, "Спальня");

            // act & assert
            Assert.That(room1, Is.Not.EqualTo(room2));
        }

        [Test]
        public void GetHashCode_SameAddressAndName_SameHashCode()
        {
            // arrange
            var address = CreateAddress();
            var room1 = new Room(address, "Кабинет");
            var room2 = new Room(address, "Кабинет");

            // act & assert
            Assert.That(room1.GetHashCode(), Is.EqualTo(room2.GetHashCode()));
        }

        [Test]
        public void GetHashCode_DifferentAddress_DifferenObjects()
        {
            // arrange
            var address1 = CreateAddress("Москва");
            var address2 = CreateAddress("Санкт-Петербург");
            var room1 = new Room(address1, "Кабинет");
            var room2 = new Room(address2, "Кабинет");

            // act & assert
            Assert.That(room1, Is.Not.EqualTo(room2));
        }

        #endregion

        #region ToString tests

        [Test]
        public void ToString_WithoutCabinets_ReturnsRoomNameAndAddress()
        {
            // arrange
            var room = new Room(CreateAddress("Москва", "Ленина", 10), "Кабинет");

            // act
            var result = room.ToString();

            // assert
            Assert.That(result, Does.Contain("Кабинет"));
            Assert.That(result, Does.Contain("Москва"));
            Assert.That(result, Does.Contain("Ленина"));
        }

        [Test]
        public void ToString_WithCabinets_IncludesCabinetsList()
        {
            // arrange
            var room = new Room(CreateAddress(), "Комната");
            _ = room.AddCabinet(new Cabinet(room, "Шкаф 1"));
            _ = room.AddCabinet(new Cabinet(room, "Шкаф 2"));

            // act
            var result = room.ToString();

            // assert
            Assert.That(result, Does.Contain("Комната"));
            Assert.That(result, Does.Contain("Шкаф 1"));
            Assert.That(result, Does.Contain("Шкаф 2"));
        }

        #endregion

        #region Collection property tests

        [Test]
        public void Cabinets_Collection_StartsEmpty()
        {
            // arrange & act
            var room = new Room(CreateAddress(), "Комната");

            // assert
            Assert.That(room.Cabinets, Is.Empty);
        }

        [Test]
        public void Cabinets_Collection_IsReadOnlyExternally()
        {
            // arrange
            var room = new Room(CreateAddress(), "Комната");
            var cabinetsReference = room.Cabinets;

            // act & assert — коллекция доступна, но изменение должно идти через AddCabinet
            Assert.That(cabinetsReference, Is.Not.Null);
            Assert.That(cabinetsReference, Is.InstanceOf<ISet<Cabinet>>());
        }

        #endregion
    }
}