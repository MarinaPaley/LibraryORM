// <copyright file="IllustratorBuilder.cs" company="Филипченко Марина Алексеевна">
// Copyright (c) Филипченко Марина Алексеевна 2026. Library.
// </copyright>

namespace TestDataProvider
{
    using Domain;

    /// <summary>
    /// Строитель для создания тестовых экземпляров <see cref="Illustrator"/>.
    /// </summary>
    public sealed class IllustratorBuilder
    {
        /// <summary>
        /// Инициализирует новый экземпляр класса <see cref="IllustratorBuilder"/> с дефолтными значениями.
        /// </summary>
        public IllustratorBuilder()
        {
            this.Person = new PersonBuilder();
        }

        private Person Person { get; set; }

        /// <summary>
        /// Неявное преобразование строителя в сущность <see cref="Illustrator"/>.
        /// </summary>
        /// <param name="builder"> Экземпляр строителя. </param>
        public static implicit operator Illustrator(IllustratorBuilder builder) => builder.Build();

        /// <summary>
        /// Устанавливает персону для роли художник.
        /// </summary>
        /// <param name="person"> Экземпляр персоны. </param>
        /// <returns> Текущий экземпляр строителя. </returns>
        public IllustratorBuilder WithPerson(Person person)
        {
            this.Person = person;
            return this;
        }

        /// <summary>
        /// Создает и возвращает экземпляр <see cref="Editor"/>.
        /// </summary>
        /// <returns> Настроенный художник. </returns>
        public Illustrator Build() => new Illustrator(this.Person);
    }
}