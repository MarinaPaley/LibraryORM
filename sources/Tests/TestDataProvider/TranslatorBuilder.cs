// <copyright file="TranslatorBuilder.cs" company="Филипченко Марина Алексеевна">
// Copyright (c) Филипченко Марина Алексеевна 2026. Library.
// </copyright>

namespace TestDataProvider
{
    using Domain;

    /// <summary>
    /// Строитель для создания тестовых экземпляров <see cref="Translator"/>.
    /// </summary>
    public sealed class TranslatorBuilder
    {
        /// <summary>
        /// Инициализирует новый экземпляр класса <see cref="TranslatorBuilder"/> с дефолтными значениями.
        /// </summary>
        public TranslatorBuilder()
        {
            this.Person = new PersonBuilder();
        }

        private Person Person { get; set; }

        /// <summary>
        /// Неявное преобразование строителя в сущность <see cref="Translator"/>.
        /// </summary>
        /// <param name="builder"> Экземпляр строителя. </param>
        public static implicit operator Translator(TranslatorBuilder builder) => builder.Build();

        /// <summary>
        /// Устанавливает персону для роли переводчика.
        /// </summary>
        /// <param name="person"> Экземпляр персоны. </param>
        /// <returns> Текущий экземпляр строителя. </returns>
        public TranslatorBuilder WithPerson(Person person)
        {
            this.Person = person;
            return this;
        }

        /// <summary>
        /// Создает и возвращает экземпляр <see cref="Translator"/>.
        /// </summary>
        /// <returns> Настроенный переводчик. </returns>
        public Translator Build() => new Translator(this.Person);
    }
}