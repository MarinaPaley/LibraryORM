// <copyright file="BookTypeBuilder.cs" company="Филипченко Марина Алексеевна">
// Copyright (c) Филипченко Марина Алексеевна 2026. Library.
// </copyright>

namespace TestDataProvider
{
    using Domain;

    /// <summary>
    /// Строитель для создания тестовых экземпляров <see cref="BookType"/>.
    /// </summary>
    public sealed class BookTypeBuilder
    {
        /// <summary>
        /// Инициализирует новый экземпляр класса <see cref="BookTypeBuilder"/> с дефолтными значениями.
        /// </summary>
        public BookTypeBuilder()
        {
            this.Name = "Монография";
        }

        private string Name { get; set; }

        /// <summary>
        /// Неявное преобразование строителя в сущность <see cref="BookType"/>.
        /// </summary>
        /// <param name="builder"> Экземпляр строителя. </param>
        public static implicit operator BookType(BookTypeBuilder builder) => builder.Build();

        /// <summary>
        /// Устанавливает название типа книги.
        /// </summary>
        /// <param name="name"> Название типа книги. </param>
        /// <returns> Текущий экземпляр строителя. </returns>
        public BookTypeBuilder WithName(string name)
        {
            this.Name = name;
            return this;
        }

        /// <summary>
        /// Создает и возвращает экземпляр <see cref="BookType"/>.
        /// </summary>
        /// <returns> Настроенный тип книги. </returns>
        public BookType Build() => new BookType(this.Name);
    }
}