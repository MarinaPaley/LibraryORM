// <copyright file="CabinetBuilder.cs" company="Филипченко Марина Алексеевна">
// Copyright (c) Филипченко Марина Алексеевна 2026. Library.
// </copyright>

namespace TestDataProvider
{
    using Domain;

    /// <summary>
    /// Строитель для создания тестовых экземпляров <see cref="Cabinet"/>.
    /// </summary>
    public sealed class CabinetBuilder
    {
        /// <summary>
        /// Инициализирует новый экземпляр класса <see cref="CabinetBuilder"/> с дефолтными значениями.
        /// </summary>
        public CabinetBuilder()
        {
            this.Name = "Тестовый шкаф";
            this.Room = new RoomBuilder();
        }

        private string Name { get; set; }

        private Room Room { get; set; }

        /// <summary>
        /// Неявное преобразование строителя в сущность <see cref="Cabinet"/>.
        /// </summary>
        /// <param name="builder"> Экземпляр строителя. </param>
        public static implicit operator Cabinet(CabinetBuilder builder) => builder.Build();

        /// <summary>
        /// Устанавливает название шкафа.
        /// </summary>
        /// <param name="name"> Название шкафа. </param>
        /// <returns> Текущий экземпляр строителя. </returns>
        public CabinetBuilder WithName(string name)
        {
            this.Name = name;
            return this;
        }

        /// <summary>
        /// Устанавливает комнату, в которой находится шкаф.
        /// </summary>
        /// <param name="room"> Экземпляр комнаты. </param>
        /// <returns> Текущий экземпляр строителя. </returns>
        public CabinetBuilder WithRoom(Room room)
        {
            this.Room = room;
            return this;
        }

        /// <summary>
        /// Создает и возвращает экземпляр <see cref="Cabinet"/>.
        /// </summary>
        /// <returns> Настроенный шкаф. </returns>
        public Cabinet Build() => new Cabinet(this.Room, this.Name);
    }
}