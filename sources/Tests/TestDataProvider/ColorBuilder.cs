// <copyright file="ColorBuilder.cs" company="Филипченко Марина Алексеевна">
// Copyright (c) Филипченко Марина Алексеевна 2026. Library.
// </copyright>

namespace TestDataProvider
{
    using Domain;

    /// <summary>
    /// Строитель для создания тестовых экземпляров <see cref="Color"/>.
    /// </summary>
    public sealed class ColorBuilder
    {
        /// <summary>
        /// Инициализирует новый экземпляр класса <see cref="ColorBuilder"/>.
        /// </summary>
        public ColorBuilder()
        {
        }

        private string Name { get; set; } = "Красный";

        private ColorCode Code { get; set; } = new ColorCode("#FF0000");

        /// <summary>
        /// Неявное преобразование строителя в сущность <see cref="Color"/>.
        /// </summary>
        /// <param name="builder"> Экземпляр строителя. </param>
        public static implicit operator Color(ColorBuilder builder) => builder.Build();

        /// <summary>
        /// Устанавливает название цвета.
        /// </summary>
        /// <param name="name"> Название цвета. </param>
        /// <returns> Текущий экземпляр строителя. </returns>
        public ColorBuilder WithName(string name)
        {
            this.Name = name;
            return this;
        }

        /// <summary>
        /// Устанавливает HEX-код цвета.
        /// </summary>
        /// <param name="code"> Экземпляр кода цвета. </param>
        /// <returns> Текущий экземпляр строителя. </returns>
        public ColorBuilder WithCode(ColorCode code)
        {
            this.Code = code;
            return this;
        }

        /// <summary>
        /// Создает и возвращает экземпляр <see cref="Color"/>.
        /// </summary>
        /// <returns> Настроенный цвет. </returns>
        public Color Build() => new Color(this.Name, this.Code);
    }
}