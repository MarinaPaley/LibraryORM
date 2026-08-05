// <copyright file="ReviewerBuilder.cs" company="Филипченко Марина Алексеевна">
// Copyright (c) Филипченко Марина Алексеевна 2026. Library.
// </copyright>

namespace TestDataProvider
{
    using Domain;

    /// <summary>
    /// Строитель для создания тестовых экземпляров <see cref="Reviewer"/>.
    /// </summary>
    public sealed class ReviewerBuilder
    {
        /// <summary>
        /// Инициализирует новый экземпляр класса <see cref="ReviewerBuilder"/> с дефолтными значениями.
        /// </summary>
        public ReviewerBuilder()
        {
            this.Person = new PersonBuilder();
        }

        private Person Person { get; set; }

        /// <summary>
        /// Неявное преобразование строителя в сущность <see cref="Reviewer"/>.
        /// </summary>
        /// <param name="builder"> Экземпляр строителя. </param>
        public static implicit operator Reviewer(ReviewerBuilder builder) => builder.Build();

        /// <summary>
        /// Устанавливает персону для роли рецензента.
        /// </summary>
        /// <param name="person"> Экземпляр персоны. </param>
        /// <returns> Текущий экземпляр строителя. </returns>
        public ReviewerBuilder WithPerson(Person person)
        {
            this.Person = person;
            return this;
        }

        /// <summary>
        /// Создает и возвращает экземпляр <see cref="Reviewer"/>.
        /// </summary>
        /// <returns> Настроенный рецензент. </returns>
        public Reviewer Build() => new Reviewer(this.Person);
    }
}