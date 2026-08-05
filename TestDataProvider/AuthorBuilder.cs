// <copyright file="AuthorBuilder.cs" company="Филипченко Марина Алексеевна">
// Copyright (c) Филипченко Марина Алексеевна 2026. Library.
// </copyright>

namespace TestDataProvider
{
    using Domain;

    /// <summary>
    /// Строитель для создания тестовых экземпляров <see cref="Author"/>.
    /// </summary>
    public sealed class AuthorBuilder
    {
        /// <summary>
        /// Инициализирует новый экземпляр класса <see cref="AuthorBuilder"/>.
        /// </summary>
        public AuthorBuilder()
        {
        }

        private Person Person { get; set; } = new PersonBuilder();

        /// <summary>
        /// Неявное преобразование строителя в сущность <see cref="Author"/>.
        /// </summary>
        /// <param name="builder"> Экземпляр строителя. </param>
        public static implicit operator Author(AuthorBuilder builder) => builder.Build();

        /// <summary>
        /// Устанавливает персону для роли автора.
        /// </summary>
        /// <param name="person"> Экземпляр персоны. </param>
        /// <returns> Текущий экземпляр строителя. </returns>
        public AuthorBuilder WithPerson(Person person)
        {
            this.Person = person;
            return this;
        }

        /// <summary>
        /// Создает и возвращает экземпляр <see cref="Author"/>.
        /// </summary>
        /// <returns> Настроенный автор. </returns>
        public Author Build() => new Author(this.Person);
    }
}
