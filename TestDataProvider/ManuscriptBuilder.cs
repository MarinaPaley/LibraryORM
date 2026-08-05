// <copyright file="ManuscriptBuilder.cs" company="Филипченко Марина Алексеевна">
// Copyright (c) Филипченко Марина Алексеевна 2026. Library.
// </copyright>

namespace TestDataProvider
{
    using System;
    using System.Collections.Generic;
    using Domain;
    using Staff;

    /// <summary>
    /// Строитель для создания тестовых экземпляров <see cref="Manuscript"/>.
    /// </summary>
    public sealed class ManuscriptBuilder
    {
        /// <summary>
        /// Инициализирует новый экземпляр класса <see cref="ManuscriptBuilder"/>.
        /// </summary>
        public ManuscriptBuilder()
        {
        }

        private string Name { get; set; } = "Тестовая рукопись";

        private ISet<Language> Languages { get; set; } = new HashSet<Language> { new Language("Русский") };

        private ISet<Author> Authors { get; set; } = new HashSet<Author> { new AuthorBuilder() };

        private Range<DateOnly>? Date { get; set; }

        private string? Origin { get; set; }

        /// <summary>
        /// Неявное преобразование строителя в сущность <see cref="Manuscript"/>.
        /// </summary>
        /// <param name="builder"> Экземпляр строителя. </param>
        public static implicit operator Manuscript(ManuscriptBuilder builder) => builder.Build();

        /// <summary>
        /// Устанавливает название рукописи.
        /// </summary>
        /// <param name="name"> Название. </param>
        /// <returns> Текущий экземпляр строителя. </returns>
        public ManuscriptBuilder WithName(string name)
        {
            this.Name = name;
            return this;
        }

        /// <summary>
        /// Устанавливает языки рукописи.
        /// </summary>
        /// <param name="languages"> Набор языков. </param>
        /// <returns> Текущий экземпляр строителя. </returns>
        public ManuscriptBuilder WithLanguages(ISet<Language> languages)
        {
            this.Languages = languages;
            return this;
        }

        /// <summary>
        /// Устанавливает авторов рукописи.
        /// </summary>
        /// <param name="authors"> Набор авторов. </param>
        /// <returns> Текущий экземпляр строителя. </returns>
        public ManuscriptBuilder WithAuthors(ISet<Author> authors)
        {
            this.Authors = authors;
            return this;
        }

        /// <summary>
        /// Устанавливает диапазон дат написания.
        /// </summary>
        /// <param name="date"> Диапазон дат. </param>
        /// <returns> Текущий экземпляр строителя. </returns>
        public ManuscriptBuilder WithDate(Range<DateOnly>? date)
        {
            this.Date = date;
            return this;
        }

        /// <summary>
        /// Устанавливает оригинальное название.
        /// </summary>
        /// <param name="origin"> Оригинальное название. </param>
        /// <returns> Текущий экземпляр строителя. </returns>
        public ManuscriptBuilder WithOrigin(string? origin)
        {
            this.Origin = origin;
            return this;
        }

        /// <summary>
        /// Создает и возвращает экземпляр <see cref="Manuscript"/>.
        /// </summary>
        /// <returns> Настроенная рукопись. </returns>
        public Manuscript Build() => new Manuscript(this.Name, this.Languages, this.Authors, this.Date, this.Origin);
    }
}