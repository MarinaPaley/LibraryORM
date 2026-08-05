// <copyright file="LanguageBuilder.cs" company="Филипченко Марина Алексеевна">
// Copyright (c) Филипченко Марина Алексеевна 2026. Library.
// </copyright>

namespace TestDataProvider
{
    using Domain;

    /// <summary>
    /// Строитель для создания тестовых экземпляров <see cref="Language"/>.
    /// </summary>
    public sealed class LanguageBuilder
    {
        /// <summary>
        /// Инициализирует новый экземпляр класса <see cref="LanguageBuilder"/>.
        /// </summary>
        public LanguageBuilder()
        {
        }

        private string Name { get; set; } = "Русский";

        /// <summary>
        /// Неявное преобразование строителя в сущность <see cref="Language"/>.
        /// </summary>
        /// <param name="builder"> Экземпляр строителя. </param>
        public static implicit operator Language(LanguageBuilder builder) => builder.Build();

        /// <summary>
        /// Устанавливает название языка.
        /// </summary>
        /// <param name="name"> Название языка. </param>
        /// <returns> Текущий экземпляр строителя. </returns>
        public LanguageBuilder WithName(string name)
        {
            this.Name = name;
            return this;
        }

        /// <summary>
        /// Создает и возвращает экземпляр <see cref="Language"/>.
        /// </summary>
        /// <returns> Настроенный язык. </returns>
        public Language Build() => new Language(this.Name);
    }
}