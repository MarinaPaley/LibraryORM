// <copyright file="TagBuilder.cs" company="Филипченко Марина Алексеевна">
// Copyright (c) Филипченко Марина Алексеевна 2026. Library.
// </copyright>

namespace TestDataProvider
{
    using Domain;

    /// <summary>
    /// Строитель для создания тестовых экземпляров <see cref="Tag"/>.
    /// </summary>
    public sealed class TagBuilder
    {
        /// <summary>
        /// Инициализирует новый экземпляр класса <see cref="TagBuilder"/> с дефолтными значениями.
        /// </summary>
        public TagBuilder()
        {
            this.Name = "Тестовый тег";
            this.Category = new CategoryBuilder();
            this.Origin = null;
        }

        private string Name { get; set; }

        private Category Category { get; set; }

        private string? Origin { get; set; }

        /// <summary>
        /// Неявное преобразование строителя в сущность <see cref="Tag"/>.
        /// </summary>
        /// <param name="builder"> Экземпляр строителя. </param>
        public static implicit operator Tag(TagBuilder builder) => builder.Build();

        /// <summary>
        /// Устанавливает название тега.
        /// </summary>
        /// <param name="name"> Название тега. </param>
        /// <returns> Текущий экземпляр строителя. </returns>
        public TagBuilder WithName(string name)
        {
            this.Name = name;
            return this;
        }

        /// <summary>
        /// Устанавливает категорию тега.
        /// </summary>
        /// <param name="category"> Экземпляр категории. </param>
        /// <returns> Текущий экземпляр строителя. </returns>
        public TagBuilder WithCategory(Category category)
        {
            this.Category = category;
            return this;
        }

        /// <summary>
        /// Устанавливает оригинальное название тега.
        /// </summary>
        /// <param name="origin"> Оригинальное название. </param>
        /// <returns> Текущий экземпляр строителя. </returns>
        public TagBuilder WithOrigin(string? origin)
        {
            this.Origin = origin;
            return this;
        }

        /// <summary>
        /// Создает и возвращает экземпляр <see cref="Tag"/>.
        /// </summary>
        /// <returns> Настроенный тег. </returns>
        public Tag Build() => new Tag(this.Name, this.Category, this.Origin);
    }
}