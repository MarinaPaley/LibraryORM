// <copyright file="PublisherBuilder.cs" company="Филипченко Марина Алексеевна">
// Copyright (c) Филипченко Марина Алексеевна 2026. Library.
// </copyright>

namespace TestDataProvider
{
    using Domain;

    /// <summary>
    /// Строитель для создания тестовых экземпляров <see cref="Publisher"/>.
    /// </summary>
    public sealed class PublisherBuilder
    {
        /// <summary>
        /// Инициализирует новый экземпляр класса <see cref="PublisherBuilder"/>.
        /// </summary>
        public PublisherBuilder()
        {
        }

        private string Name { get; set; } = "Просвещение";

        /// <summary>
        /// Неявное преобразование строителя в сущность <see cref="Publisher"/>.
        /// </summary>
        /// <param name="builder"> Экземпляр строителя. </param>
        public static implicit operator Publisher(PublisherBuilder builder) => builder.Build();

        /// <summary>
        /// Устанавливает название издательства.
        /// </summary>
        /// <param name="name"> Название мздательства. </param>
        /// <returns> Текущий экземпляр строителя. </returns>
        public PublisherBuilder WithName(string name)
        {
            this.Name = name;
            return this;
        }

        /// <summary>
        /// Создает и возвращает экземпляр <see cref="Publisher"/>.
        /// </summary>
        /// <returns> Настроенное издательство. </returns>
        public Publisher Build() => new Publisher(this.Name);
    }
}