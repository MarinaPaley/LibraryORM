// <copyright file="EditorBuilder.cs" company="Филипченко Марина Алексеевна">
// Copyright (c) Филипченко Марина Алексеевна 2026. Library.
// </copyright>

namespace TestDataProvider
{
    using Domain;

    /// <summary>
    /// Строитель для создания тестовых экземпляров <see cref="Editor"/>.
    /// </summary>
    public sealed class EditorBuilder
    {
        /// <summary>
        /// Инициализирует новый экземпляр класса <see cref="EditorBuilder"/> с дефолтными значениями.
        /// </summary>
        public EditorBuilder()
        {
            this.Person = new PersonBuilder();
        }

        private Person Person { get; set; }

        /// <summary>
        /// Неявное преобразование строителя в сущность <see cref="Editor"/>.
        /// </summary>
        /// <param name="builder"> Экземпляр строителя. </param>
        public static implicit operator Editor(EditorBuilder builder) => builder.Build();

        /// <summary>
        /// Устанавливает персону для роли редактора.
        /// </summary>
        /// <param name="person"> Экземпляр персоны. </param>
        /// <returns> Текущий экземпляр строителя. </returns>
        public EditorBuilder WithPerson(Person person)
        {
            this.Person = person;
            return this;
        }

        /// <summary>
        /// Создает и возвращает экземпляр <see cref="Editor"/>.
        /// </summary>
        /// <returns> Настроенный редактор. </returns>
        public Editor Build() => new Editor(this.Person);
    }
}