// <copyright file="CabinetTests.cs" company="Филипченко Марина Алексеевна">
// Copyright (c) Филипченко Марина Алексеевна 2026. Library.
// </copyright>

namespace Domain.Tests
{
    using System;
    using System.Collections.Generic;
    using NUnit.Framework;

    /// <summary>
    /// Тесты для <see cref="Domain.Cabinet"/>.
    /// </summary>
    [TestFixture]
    internal sealed class CabinetTests
    {
        [Test]
        public void Ctor_NullRoom_ThrowsArgumentNullException()
        {
            // arrange & act & assert
            Assert.Throws<ArgumentNullException>(() =>
                _ = new Cabinet(null!, "Шкаф"));
        }

        [Test]
        public void Ctor_NullName_ThrowsArgumentNullException()
        {
            // arrange
            var room = CreateRoom();

            // act & assert
            Assert.Throws<ArgumentNullException>(() =>
                _ = new Cabinet(room, null!));
        }

        [Test]
        public void Ctor_EmptyName_AfterTrim_ThrowsArgumentNullException()
        {
            // arrange
            var room = CreateRoom();

            // act & assert
            Assert.Throws<ArgumentNullException>(() =>
                _ = new Cabinet(room, "   "));
        }

        [Test]
        public void Ctor_ValidData_Success()
        {
            // arrange
            var room = CreateRoom("Кабинет");

            // act
            var cabinet = new Cabinet(room, "Книжный шкаф");

            // assert
            using (Assert.EnterMultipleScope())
            {
                Assert.That(cabinet.Room, Is.SameAs(room));
                Assert.That(cabinet.Name.Value, Is.EqualTo("Книжный шкаф"));
                Assert.That(cabinet.Shelves, Is.Empty);
            }
        }

        [Test]
        public void AddShelf_ValidShelf_AddsToBothCollections()
        {
            // arrange
            var cabinet = new Cabinet(CreateRoom(), "Шкаф");
            var shelf = new Shelf("Полка");

            // act
            var result = cabinet.AddShelf(shelf);

            // assert
            using (Assert.EnterMultipleScope())
            {
                Assert.That(result, Is.True);
                Assert.That(cabinet.Shelves, Contains.Item(shelf));
                Assert.That(shelf.Cabinet, Is.SameAs(cabinet));
            }
        }

        [Test]
        public void AddShelf_NullShelf_ReturnsFalse()
        {
            // arrange
            var cabinet = new Cabinet(CreateRoom(), "Шкаф");

            // act
            var result = cabinet.AddShelf(null!);

            // assert
            using (Assert.EnterMultipleScope())
            {
                Assert.That(result, Is.False);
                Assert.That(cabinet.Shelves, Is.Empty);
            }
        }

        [Test]
        public void AddShelf_DuplicateShelf_ReturnsFalse()
        {
            // arrange
            var cabinet = new Cabinet(CreateRoom(), "Шкаф");
            var shelf = new Shelf("Полка");
            _ = cabinet.AddShelf(shelf);

            // act
            var result = cabinet.AddShelf(shelf);

            // assert
            using (Assert.EnterMultipleScope())
            {
                Assert.That(result, Is.False);
                Assert.That(cabinet.Shelves, Has.Count.EqualTo(1));
            }
        }

        [Test]
        public void RemoveShelf_ExistingShelf_RemovesFromBothCollections()
        {
            // arrange
            var cabinet = new Cabinet(CreateRoom(), "Шкаф");
            var shelf = new Shelf("Полка");
            _ = cabinet.AddShelf(shelf);

            // act
            var result = cabinet.RemoveShelf(shelf);

            // assert
            using (Assert.EnterMultipleScope())
            {
                Assert.That(result, Is.True);
                Assert.That(cabinet.Shelves, Does.Not.Contain(shelf));
                Assert.That(shelf.Cabinet, Is.Null);
            }
        }

        [Test]
        public void RemoveShelf_NonExistingShelf_ReturnsFalse()
        {
            // arrange
            var cabinet = new Cabinet(CreateRoom(), "Шкаф");
            var otherCabinet = new Cabinet(CreateRoom(), "Другой шкаф");
            var shelf = new Shelf("Чужая полка");
            _ = otherCabinet.AddShelf(shelf);

            // act
            var result = cabinet.RemoveShelf(shelf);

            // assert
            using (Assert.EnterMultipleScope())
            {
                Assert.That(result, Is.False);
                Assert.That(shelf.Cabinet, Is.SameAs(otherCabinet));
            }
        }

        [Test]
        public void RemoveShelf_NullShelf_ReturnsFalse()
        {
            // arrange
            var cabinet = new Cabinet(CreateRoom(), "Шкаф");

            // act
            var result = cabinet.RemoveShelf(null!);

            // assert
            Assert.That(result, Is.False);
        }

        [Test]
        public void Equals_SameReference_ReturnsTrue()
        {
            // arrange
            var cabinet = new Cabinet(CreateRoom(), "Шкаф");

            // act & assert
            Assert.That(cabinet.Equals(cabinet), Is.True);
        }

        [Test]
        public void Equals_Null_ReturnsFalse()
        {
            // arrange
            var cabinet = new Cabinet(CreateRoom(), "Шкаф");

            // act & assert
            Assert.That(cabinet, Is.Not.Null);
        }

        [Test]
        public void Equals_DifferentType_ReturnsFalse()
        {
            // arrange
            var cabinet = new Cabinet(CreateRoom(), "Шкаф");

            // act & assert
            Assert.That(cabinet.Equals("not a cabinet"), Is.False);
        }

        [Test]
        public void Equals_SameRoomAndName_ReturnsTrue()
        {
            // arrange
            var room = CreateRoom();
            var cabinet1 = new Cabinet(room, "Книжный шкаф");
            var cabinet2 = new Cabinet(room, "Книжный шкаф");

            // act & assert
            Assert.That(cabinet1, Is.EqualTo(cabinet2));
        }

        [Test]
        public void Equals_DifferentRoom_ReturnsFalse()
        {
            // arrange
            var room1 = CreateRoom("Комната 1");
            var room2 = CreateRoom("Комната 2");
            var cabinet1 = new Cabinet(room1, "Шкаф");
            var cabinet2 = new Cabinet(room2, "Шкаф");

            // act & assert
            Assert.That(cabinet1, Is.Not.EqualTo(cabinet2));
        }

        [Test]
        public void Equals_DifferentName_ReturnsFalse()
        {
            // arrange
            var room = CreateRoom();
            var cabinet1 = new Cabinet(room, "Книжный шкаф");
            var cabinet2 = new Cabinet(room, "Платяной шкаф");

            // act & assert
            Assert.That(cabinet1, Is.Not.EqualTo(cabinet2));
        }

        [Test]
        public void GetHashCode_SameRoomAndName_SameHashCode()
        {
            // arrange
            var room = CreateRoom();
            var cabinet1 = new Cabinet(room, "Шкаф");
            var cabinet2 = new Cabinet(room, "Шкаф");

            // act & assert
            Assert.That(cabinet1.GetHashCode(), Is.EqualTo(cabinet2.GetHashCode()));
        }

        [Test]
        public void ToString_WithoutShelves_ReturnsCabinetNameAndRoom()
        {
            // arrange
            var cabinet = new Cabinet(CreateRoom("Кабинет"), "Книжный шкаф");

            // act
            var result = cabinet.ToString();

            // assert
            Assert.That(result, Does.Contain("Книжный шкаф"));
            Assert.That(result, Does.Contain("Кабинет"));
        }

        [Test]
        public void ToString_WithShelves_IncludesShelvesList()
        {
            // arrange
            var cabinet = new Cabinet(CreateRoom(), "Шкаф");
            _ = cabinet.AddShelf(new Shelf("Полка 1"));
            _ = cabinet.AddShelf(new Shelf("Полка 2"));

            // act
            var result = cabinet.ToString();

            // assert
            Assert.That(result, Does.Contain("Шкаф"));
            Assert.That(result, Does.Contain("Полка 1"));
            Assert.That(result, Does.Contain("Полка 2"));
        }

        [Test]
        public void Shelves_Collection_StartsEmpty()
        {
            // arrange & act
            var cabinet = new Cabinet(CreateRoom(), "Шкаф");

            // assert
            Assert.That(cabinet.Shelves, Is.Empty);
        }

        [Test]
        public void Shelves_Collection_IsReadOnlyExternally()
        {
            // arrange
            var cabinet = new Cabinet(CreateRoom(), "Шкаф");
            var shelvesReference = cabinet.Shelves;

            // act & assert
            Assert.That(shelvesReference, Is.Not.Null);
            Assert.That(shelvesReference, Is.InstanceOf<ISet<Shelf>>());
        }

        [Test]
        public void FullHierarchy_Creation_Success()
        {
            // arrange
            var address = CreateAddress("Москва", "Ленина", 10);
            var room = new Room(address, "Кабинет");
            var cabinet = new Cabinet(room, "Книжный шкаф");
            var shelf = new Shelf("Верхняя полка");

            // act
            _ = room.AddCabinet(cabinet);
            _ = cabinet.AddShelf(shelf);

            // assert
            using (Assert.EnterMultipleScope())
            {
                Assert.That(cabinet.Room, Is.SameAs(room));
                Assert.That(shelf.Cabinet, Is.SameAs(cabinet));
                Assert.That(room.Cabinets, Contains.Item(cabinet));
                Assert.That(cabinet.Shelves, Contains.Item(shelf));
                Assert.That(room.Address, Is.SameAs(address));
            }
        }

        [Test]
        public void RemoveRoom_ClearsCabinetsRoomReference()
        {
            // arrange
            var room = CreateRoom();
            var cabinet = new Cabinet(room, "Шкаф");
            room.AddCabinet(cabinet);

            // act
            _ = room.RemoveCabinet(cabinet);

            // assert
            using (Assert.EnterMultipleScope())
            {
                Assert.That(cabinet.Room, Is.Null);
                Assert.That(room.Cabinets, Does.Not.Contain(cabinet));
            }
        }

        [Test]
        public void RemoveCabinet_ClearsShelvesCabinetReference()
        {
            // arrange
            var cabinet = new Cabinet(CreateRoom(), "Шкаф");
            var shelf = new Shelf("Полка");
            _ = cabinet.AddShelf(shelf);

            // act
            _ = cabinet.RemoveShelf(shelf);

            // assert
            using (Assert.EnterMultipleScope())
            {
                Assert.That(shelf.Cabinet, Is.Null);
                Assert.That(cabinet.Shelves, Does.Not.Contain(shelf));
            }
        }

        private static Address CreateAddress(string city = "Город", string street = "Улица", int house = 1) =>
            new (new Street(street, new City(city)), house);

        private static Room CreateRoom(string name = "Комната") =>
            new (CreateAddress(), name);

        private static Shelf CreateShelf(Cabinet cabinet, string name = "Полка") => new (name);
    }
}