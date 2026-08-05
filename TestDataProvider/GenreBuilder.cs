// <copyright file="GenreBuilder.cs" company="Филипченко Марина Алексеевна">
// Copyright (c) Филипченко Марина Алексеевна 2026. Library.
// </copyright>

namespace TestDataProvider
{
    using Domain;

    /// <summary>
    /// Строитель для создания тестовых экземпляров <see cref="Genre"/>.
    /// </summary>
    public sealed class GenreBuilder
    {
        /// <summary>
        /// Инициализирует новый экземпляр класса <see cref="GenreBuilder"/>.
        /// </summary>
        public GenreBuilder()
        {
        }

        private string Name { get; set; } = "Роман";

        /// <summary>
        /// Неявное преобразование строителя в сущность <see cref="Genre"/>.
        /// </summary>
        /// <param name="builder"> Экземпляр строителя. </param>
        public static implicit operator Genre(GenreBuilder builder) => builder.Build();

        /// <summary>
        /// Устанавливает название жанра.
        /// </summary>
        /// <param name="name"> Название жанра. </param>
        /// <returns> Текущий экземпляр строителя. </returns>
        public GenreBuilder WithName(string name)
        {
            this.Name = name;
            return this;
        }

        /// <summary>
        /// Создает и возвращает экземпляр <see cref="Genre"/>.
        /// </summary>
        /// <returns> Настроенный жанр. </returns>
        public Genre Build() => new Genre(this.Name);
    }
}