// <copyright file="SeriaBuilder.cs" company="Филипченко Марина Алексеевна">
// Copyright (c) Филипченко Марина Алексеевна 2026. Library.
// </copyright>

namespace TestDataProvider
{
    using Domain;

    /// <summary>
    /// Строитель для создания тестовых экземпляров <see cref="Seria"/>.
    /// </summary>
    public sealed class SeriaBuilder
    {
        /// <summary>
        /// Инициализирует новый экземпляр класса <see cref="SeriaBuilder"/> с дефолтными значениями.
        /// </summary>
        public SeriaBuilder()
        {
            this.Name = "Тестовая серия";
        }

        private string Name { get; set; }

        /// <summary>
        /// Неявное преобразование строителя в сущность <see cref="Seria"/>.
        /// </summary>
        /// <param name="builder"> Экземпляр строителя. </param>
        public static implicit operator Seria(SeriaBuilder builder) => builder.Build();

        /// <summary>
        /// Устанавливает название серии.
        /// </summary>
        /// <param name="name"> Название серии. </param>
        /// <returns> Текущий экземпляр строителя. </returns>
        public SeriaBuilder WithName(string name)
        {
            this.Name = name;
            return this;
        }

        /// <summary>
        /// Создает и возвращает экземпляр <see cref="Seria"/>.
        /// </summary>
        /// <returns> Настроенная серия. </returns>
        public Seria Build() => new Seria(this.Name);
    }
}