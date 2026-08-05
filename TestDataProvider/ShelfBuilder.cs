// <copyright file="ShelfBuilder.cs" company="Филипченко Марина Алексеевна">
// Copyright (c) Филипченко Марина Алексеевна 2026. Library.
// </copyright>

namespace TestDataProvider
{
    using Domain;

    /// <summary>
    /// Строитель для создания тестовых экземпляров <see cref="Shelf"/>.
    /// </summary>
    public sealed class ShelfBuilder
    {
        /// <summary>
        /// Инициализирует новый экземпляр класса <see cref="ShelfBuilder"/>.
        /// </summary>
        public ShelfBuilder()
        {
        }

        private string Name { get; set; } = "Тестовая полка";

        private Cabinet? Cabinet { get; set; }

        /// <summary>
        /// Неявное преобразование строителя в сущность <see cref="Shelf"/>.
        /// </summary>
        /// <param name="builder"> Экземпляр строителя. </param>
        public static implicit operator Shelf(ShelfBuilder builder) => builder.Build();

        /// <summary>
        /// Устанавливает название полки.
        /// </summary>
        /// <param name="name"> Название. </param>
        /// <returns> Текущий экземпляр строителя. </returns>
        public ShelfBuilder WithName(string name)
        {
            this.Name = name;
            return this;
        }

        /// <summary>
        /// Устанавливает шкаф, в котором находится полка.
        /// </summary>
        /// <param name="cabinet"> Экземпляр шкафа. </param>
        /// <returns> Текущий экземпляр строителя. </returns>
        public ShelfBuilder WithCabinet(Cabinet? cabinet)
        {
            this.Cabinet = cabinet;
            return this;
        }

        /// <summary>
        /// Создает и возвращает экземпляр <see cref="Shelf"/>.
        /// </summary>
        /// <returns> Настроенная полка. </returns>
        public Shelf Build()
        {
            var shelf = new Shelf(this.Name);
            shelf.Cabinet = this.Cabinet;
            return shelf;
        }
    }
}