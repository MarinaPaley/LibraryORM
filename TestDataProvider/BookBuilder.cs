// <copyright file="BookBuilder.cs" company="Филипченко Марина Алексеевна">
// Copyright (c) Филипченко Марина Алексеевна 2026. Library.
// </copyright>

namespace TestDataProvider
{
    using System.Collections.Generic;
    using Domain;

    /// <summary>
    /// Строитель для создания тестовых экземпляров <see cref="Book"/>.
    /// </summary>
    public sealed class BookBuilder
    {
        /// <summary>
        /// Инициализирует новый экземпляр класса <see cref="BookBuilder"/>.
        /// </summary>
        public BookBuilder()
        {
        }

        private string? Title { get; set; } = "Тестовая книга";

        private int Pages { get; set; } = 100;

        private string ISBN { get; set; } = "978-3-16-148410-0";

        private BookType BookType { get; set; } = new BookType("Монография");

        private Publisher Publisher { get; set; } = new Publisher("Тестовое издательство");

        private int Year { get; set; } = 2024;

        private ISet<Manuscript> Manuscripts { get; set; } = new HashSet<Manuscript> { new ManuscriptBuilder() };

        private int? Volume { get; set; }

        private string? Annotation { get; set; }

        private string? Edition { get; set; }

        private Editor? Editor { get; set; }

        private Seria? Seria { get; set; }

        private string? Doi { get; set; }

        private string? Url { get; set; }

        /// <summary>
        /// Неявное преобразование строителя в сущность <see cref="Book"/>.
        /// </summary>
        /// <param name="builder"> Экземпляр строителя. </param>
        public static implicit operator Book(BookBuilder builder) => builder.Build();

        /// <summary>
        /// Устанавливает название книги.
        /// </summary>
        /// <param name="title"> Название. </param>
        /// <returns> Текущий экземпляр строителя. </returns>
        public BookBuilder WithTitle(string? title)
        {
            this.Title = title;
            return this;
        }

        /// <summary>
        /// Устанавливает количество страниц.
        /// </summary>
        /// <param name="pages"> Количество страниц. </param>
        /// <returns> Текущий экземпляр строителя. </returns>
        public BookBuilder WithPages(int pages)
        {
            this.Pages = pages;
            return this;
        }

        /// <summary>
        /// Устанавливает ISBN.
        /// </summary>
        /// <param name="isbn"> Код ISBN. </param>
        /// <returns> Текущий экземпляр строителя. </returns>
        public BookBuilder WithISBN(string isbn)
        {
            this.ISBN = isbn;
            return this;
        }

        /// <summary>
        /// Устанавливает тип книги.
        /// </summary>
        /// <param name="bookType"> Тип книги. </param>
        /// <returns> Текущий экземпляр строителя. </returns>
        public BookBuilder WithBookType(BookType bookType)
        {
            this.BookType = bookType;
            return this;
        }

        /// <summary>
        /// Устанавливает издательство.
        /// </summary>
        /// <param name="publisher"> Издательство. </param>
        /// <returns> Текущий экземпляр строителя. </returns>
        public BookBuilder WithPublisher(Publisher publisher)
        {
            this.Publisher = publisher;
            return this;
        }

        /// <summary>
        /// Устанавливает год издания.
        /// </summary>
        /// <param name="year"> Год издания. </param>
        /// <returns> Текущий экземпляр строителя. </returns>
        public BookBuilder WithYear(int year)
        {
            this.Year = year;
            return this;
        }

        /// <summary>
        /// Устанавливает набор рукописей.
        /// </summary>
        /// <param name="manuscripts"> Набор рукописей. </param>
        /// <returns> Текущий экземпляр строителя. </returns>
        public BookBuilder WithManuscripts(ISet<Manuscript> manuscripts)
        {
            this.Manuscripts = manuscripts;
            return this;
        }

        /// <summary>
        /// Устанавливает том.
        /// </summary>
        /// <param name="volume"> Номер тома. </param>
        /// <returns> Текущий экземпляр строителя. </returns>
        public BookBuilder WithVolume(int? volume)
        {
            this.Volume = volume;
            return this;
        }

        /// <summary>
        /// Устанавливает аннотацию.
        /// </summary>
        /// <param name="annotation"> Текст аннотации. </param>
        /// <returns> Текущий экземпляр строителя. </returns>
        public BookBuilder WithAnnotation(string? annotation)
        {
            this.Annotation = annotation;
            return this;
        }

        /// <summary>
        /// Устанавливает редакцию.
        /// </summary>
        /// <param name="edition"> Название редакции. </param>
        /// <returns> Текущий экземпляр строителя. </returns>
        public BookBuilder WithEdition(string? edition)
        {
            this.Edition = edition;
            return this;
        }

        /// <summary>
        /// Устанавливает редактора.
        /// </summary>
        /// <param name="editor"> Экземпляр редактора. </param>
        /// <returns> Текущий экземпляр строителя. </returns>
        public BookBuilder WithEditor(Editor? editor)
        {
            this.Editor = editor;
            return this;
        }

        /// <summary>
        /// Устанавливает серию.
        /// </summary>
        /// <param name="seria"> Экземпляр серии. </param>
        /// <returns> Текущий экземпляр строителя. </returns>
        public BookBuilder WithSeria(Seria? seria)
        {
            this.Seria = seria;
            return this;
        }

        /// <summary>
        /// Устанавливает DOI.
        /// </summary>
        /// <param name="doi"> Идентификатор DOI. </param>
        /// <returns> Текущий экземпляр строителя. </returns>
        public BookBuilder WithDoi(string? doi)
        {
            this.Doi = doi;
            return this;
        }

        /// <summary>
        /// Устанавливает URL.
        /// </summary>
        /// <param name="url"> Ссылка URL. </param>
        /// <returns> Текущий экземпляр строителя. </returns>
        public BookBuilder WithUrl(string? url)
        {
            this.Url = url;
            return this;
        }

        /// <summary>
        /// Создает и возвращает экземпляр <see cref="Book"/>.
        /// </summary>
        /// <returns> Настроенная книга. </returns>
        public Book Build() => new Book(
            this.Title,
            this.Pages,
            this.BookType,
            this.Publisher,
            this.Year,
            this.Manuscripts,
            this.ISBN,
            this.Volume,
            this.Annotation,
            this.Edition,
            this.Editor,
            this.Seria,
            this.Doi,
            this.Url);
    }
}