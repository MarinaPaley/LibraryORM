// <copyright file="CategoryBuilder.cs" company="Филипченко Марина Алексеевна">
// Copyright (c) Филипченко Марина Алексеевна 2026. Library.
// </copyright>

namespace TestDataProvider
{
    using Domain;

    /// <summary>
    /// Строитель для создания тестовых экземпляров <see cref="Category"/>.
    /// </summary>
    public sealed class CategoryBuilder
    {
        /// <summary>
        /// Инициализирует новый экземпляр класса <see cref="CategoryBuilder"/> с дефолтными значениями.
        /// </summary>
        public CategoryBuilder()
        {
            this.Name = "Тестовая категория";
            this.Color = new ColorBuilder();
            this.Origin = null;
        }

        private string Name { get; set; }

        private Color Color { get; set; }

        private string? Origin { get; set; }

        /// <summary>
        /// Неявное преобразование строителя в сущность <see cref="Category"/>.
        /// </summary>
        /// <param name="builder"> Экземпляр строителя. </param>
        public static implicit operator Category(CategoryBuilder builder) => builder.Build();

        /// <summary>
        /// Устанавливает название категории.
        /// </summary>
        /// <param name="name"> Название категории. </param>
        /// <returns> Текущий экземпляр строителя. </returns>
        public CategoryBuilder WithName(string name)
        {
            this.Name = name;
            return this;
        }

        /// <summary>
        /// Устанавливает цвет категории.
        /// </summary>
        /// <param name="color"> Экземпляр цвета. </param>
        /// <returns> Текущий экземпляр строителя. </returns>
        public CategoryBuilder WithColor(Color color)
        {
            this.Color = color;
            return this;
        }

        /// <summary>
        /// Устанавливает оригинальное название категории.
        /// </summary>
        /// <param name="origin"> Оригинальное название. </param>
        /// <returns> Текущий экземпляр строителя. </returns>
        public CategoryBuilder WithOrigin(string? origin)
        {
            this.Origin = origin;
            return this;
        }

        /// <summary>
        /// Создает и возвращает экземпляр <see cref="Category"/>.
        /// </summary>
        /// <returns> Настроенная категория. </returns>
        public Category Build() => new Category(this.Name, this.Color, this.Origin);
    }
}