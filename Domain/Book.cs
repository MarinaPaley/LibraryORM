// <copyright file="Book.cs" company="Филипченко Марина Алексеевна">
// Copyright (c) Филипченко Марина Алексеевна 2026. Library.
// </copyright>

namespace Domain
{
    using System;
    using System.Collections.Generic;
    using Domain.Abstract;
    using Staff;

    /// <summary>
    /// Книга.
    /// </summary>
    public sealed class Book : Entity<Book>
    {
        /// <summary>
        /// Инициализирует новый экземпляр класса <see cref="Book"/>.
        /// </summary>
        /// <param name="title"> Название. </param>
        /// <param name="pages"> Количество страниц. </param>
        /// <param name="bookType"> Тип книги. </param>
        /// <param name="publisher"> Издательство.</param>
        /// <param name="year"> Год издания. </param>
        /// <param name="manuscripts"> Рукописи. </param>
        /// <param name="ibsn"> Код <c>ISBN</c>. </param>
        /// <param name="volume"> Том.</param>
        /// <param name="annotation"> Аннотация. </param>
        /// <param name="edition"> Редакция. </param>
        /// <param name="editor"> Редактор. </param>
        /// <param name="illustrator"> Художник. </param>
        /// <param name="seria"> Серия. </param>
        /// <param name="doi"> DOI. </param>
        /// <param name="url"> URL. </param>
        /// <param name="quality"> Качество. </param>
        /// <param name="isConvolutus"> Сшита из нескольких журналов. </param>
        /// <exception cref="ArgumentNullException">
        /// Если название книги или код <see langword="null"/>
        /// или Издательство <see langword="null"/>
        /// или Тип издания <see langword="null"/>.
        /// </exception>
        /// <exception cref="ArgumentOutOfRangeException">
        /// Если количество страниц меньше или равно нулю или год издания не валиден.
        /// </exception>
        public Book(
            string? title,
            int pages,
            BookType bookType,
            Publisher publisher,
            int year,
            ISet<Manuscript> manuscripts,
            string? ibsn = null,
            int? volume = null,
            string? annotation = null,
            string? edition = null,
            Editor? editor = null,
            Illustrator? illustrator = null,
            Seria? seria = null,
            string? doi = null,
            string? url = null,
            PrintQuality quality = PrintQuality.PrintingHouse,
            bool isConvolutus = false)
        {
            ArgumentOutOfRangeException.ThrowIfNegativeOrZero(pages);
            ArgumentOutOfRangeException.ThrowIfNegativeOrZero(year);
            ArgumentNullException.ThrowIfNull(publisher);
            ArgumentNullException.ThrowIfNull(bookType);
            ArgumentOutOfRangeException.ThrowIfGreaterThan(year, DateTime.Now.Year);
            if (volume.HasValue && volume <= 0)
            {
                throw new ArgumentOutOfRangeException(nameof(volume));
            }

            this.ISBN = ibsn.TrimOrNull();
            this.Manuscripts = manuscripts ?? throw new ArgumentNullException(nameof(manuscripts));

            this.Title = title;
            this.Pages = pages;
            this.Year = year;

            foreach (var manuscript in manuscripts)
            {
                manuscript.Books.Add(this);
            }

            this.Publisher = publisher;
            publisher.Books.Add(this);

            this.Annotation = annotation.TrimOrNull();
            this.Volume = volume;
            this.Edition = edition.TrimOrNull();
            this.Editor = editor;
            this.Illustrator = illustrator;

            _ = editor?.AddBook(this);
            _ = seria?.AddBook(this);
            _ = illustrator?.AddBook(this);

            this.Doi = doi;
            this.Url = url;
            this.BookType = bookType;
            bookType.Books.Add(this);

            this.Quality = quality;
            this.IsConvolutus = isConvolutus;
        }

        /// <summary>
        /// Инициализирует новый экземпляр класса <see cref="Book"/>.
        /// </summary>
        /// <param name="title"> Название.</param>
        /// <param name="pages"> Количество страниц. </param>
        /// <param name="ibsn"> Код <c>ISBN</c>. </param>
        /// <param name="bookType"> Тип издания. </param>
        /// <param name="publisher"> Издательство. </param>
        /// <param name="year"> Год издания. </param>
        /// <param name="manuscripts"> Рукописи. </param>
        /// <param name="volume"> Том.</param>
        /// <param name="annotation"> Аннотация. </param>
        /// <param name="edition"> Редакция. </param>
        /// <param name="editor"> Редактор. </param>
        /// <param name="illustrator"> Художник.</param>
        /// <param name="seria"> Серия. </param>
        /// <param name="doi"> DOI. </param>
        /// <param name="url"> URL. </param>
        /// <param name="quality"> Качество. </param>
        /// <param name="isConvolutus"> Сшита из нескольких журналов. </param>
        /// <exception cref="ArgumentNullException"> Если название книги или код <see langword="null"/>.</exception>
        /// <exception cref="ArgumentOutOfRangeException"> Если количество страниц меньше или равно нулю.</exception>
        public Book(
            string title,
            int pages,
            string? ibsn,
            BookType bookType,
            Publisher publisher,
            int year,
            int? volume = null,
            string? annotation = null,
            string? edition = null,
            Editor? editor = null,
            Illustrator? illustrator = null,
            Seria? seria = null,
            string? doi = null,
            string? url = null,
            PrintQuality quality = PrintQuality.PrintingHouse,
            bool isConvolutus = false,
            params Manuscript[] manuscripts)
            : this(
                   title,
                   pages,
                   bookType,
                   publisher,
                   year,
                   new HashSet<Manuscript>(manuscripts),
                   ibsn,
                   volume,
                   annotation,
                   edition,
                   editor,
                   illustrator,
                   seria,
                   doi,
                   url,
                   quality,
                   isConvolutus)
        {
        }

#pragma warning disable CS8618 // Необходимо для работы с обязательными полями, получаемыми не через конструктор.

        /// <summary>
        /// Инициализирует новый экземпляр класса <see cref="Book"/>.
        /// </summary>
        [Obsolete("For ORM only", true)]
        private Book()
        {
        }
#pragma warning restore CS8618

        /// <summary>
        /// Название.
        /// </summary>
        public string? Title { get; }

        /// <summary>
        /// Количество страниц.
        /// </summary>
        public int Pages { get; }

        /// <summary>
        /// Качество.
        /// </summary>
        public PrintQuality Quality { get; set; }

        /// <summary>
        /// Код isbn.
        /// </summary>
        public string? ISBN { get; }

        /// <summary>
        /// Издательсто.
        /// </summary>
        public Publisher Publisher { get; set; }

        /// <summary>
        /// Тип книги.
        /// </summary>
        public BookType BookType { get; set; }

        /// <summary>
        /// Год издания.
        /// </summary>
        public int Year { get; }

        /// <summary>
        /// Сшита из нескольких журналов.
        /// </summary>
        public bool IsConvolutus { get; }

        /// <summary>
        /// Рукописи в книге.
        /// </summary>
        public ISet<Manuscript> Manuscripts { get; } = new HashSet<Manuscript>(BilingualNamedEntityComparer<Manuscript>.Instance);

        /// <summary>
        /// Экземпляры книги.
        /// </summary>
        public HashSet<Item> Items { get; set; } = new HashSet<Item>(EntityComparer<Item>.Instance);

        /// <summary>
        /// Редактор.
        /// </summary>
        public Editor? Editor { get; set; }

        /// <summary>
        /// Художник.
        /// </summary>
        public Illustrator? Illustrator { get; set; }

        /// <summary>
        /// Аннотация.
        /// </summary>
        public string? Annotation { get; set; }

        /// <summary>
        /// Издание.
        /// </summary>
        public string? Edition { get; set; }

        /// <summary>
        /// Том.
        /// </summary>
        public int? Volume { get; set; }

        /// <summary>
        /// Серия.
        /// </summary>
        public Seria? Seria { get; set; }

        /// <summary>
        /// DOI.
        /// </summary>
        public string? Doi { get; set; }

        /// <summary>
        /// URL.
        /// </summary>
        public string? Url { get; set; }

        /// <summary>
        /// Теги.
        /// </summary>
        public ISet<Tag> Tags { get; set; } = new HashSet<Tag>(BilingualNamedEntityComparer<Tag>.Instance);

        /// <inheritdoc/>
        public override bool Equals(Book? other)
        {
            if (ReferenceEquals(this, other))
            {
                return true;
            }

            if (other is null)
            {
                return false;
            }

            // Если ISBN задан у обеих книг — сравниваем по ISBN (главный идентификатор).
            if (this.ISBN is not null && other.ISBN is not null)
            {
                return string.Equals(this.ISBN, other.ISBN, StringComparison.OrdinalIgnoreCase);
            }

            // Если ISBN задан только у одной книги — они разные.
            if (this.ISBN is not null || other.ISBN is not null)
            {
                return false;
            }

            // Если ISBN отсутствует у обеих книг — сравниваем по остальным полям.
            return this.Title == other.Title
                && this.Publisher.Equals(other.Publisher)
                && this.Year == other.Year
                && StringComparer.OrdinalIgnoreCase.Equals(this.Edition, other.Edition);
        }

        /// <inheritdoc/>
        public override bool Equals(object? obj) => this.Equals(obj as Book);

        /// <inheritdoc/>
        public override int GetHashCode()
        {
            // Если ISBN задан, он является главным идентификатором.
            if (this.ISBN is not null)
            {
                return HashCode.Combine(this.ISBN.ToUpperInvariant());
            }

            // Если ISBN отсутствует, используем комбинацию остальных полей.
            return HashCode.Combine(
                this.Title,
                this.Publisher,
                this.Year,
                this.Edition);
        }

        /// <inheritdoc cref="object.ToString()"/>
        public override string ToString()
        {
            var parts = this.Manuscripts.Join(", ");
            return this.Title is not null
                ? $"{this.Title} {parts}"
                : parts;
        }

        /// <summary>
        /// Добавить редактора.
        /// </summary>
        /// <param name="editor"> Редактор. </param>
        /// <returns> Если добавили, то <see langword="true"/>, иначе - <see langword="false"/>. </returns>
        public bool AddEditor(Editor editor)
        {
            this.Editor = editor;

            return editor is not null
                && editor.Books.Add(this);
        }

        /// <summary>
        /// Удалили редактора.
        /// </summary>
        /// <param name="editor"> Редактор. </param>
        /// <returns>  Если удалили, то <see langword="true"/>, иначе - <see langword="false"/>. </returns>
        public bool RemoveEditor(Editor editor)
        {
            this.Editor = null;

            return editor is not null
                && editor.Books.Remove(this);
        }

        /// <summary>
        /// Добавляет иллюстратора к книге.
        /// </summary>
        /// <param name="illustrator"> Иллюстратор. </param>
        /// <returns> Если добавили, то <see langword="true"/>, иначе - <see langword="false"/>. </returns>
        public bool AddIllustrator(Illustrator illustrator)
        {
            this.Illustrator = illustrator;

            return illustrator is not null
                && illustrator.Books.Add(this);
        }

        /// <summary>
        /// Удаляет иллюстратора у книги.
        /// </summary>
        /// <param name="illustrator"> Иллюстратор. </param>
        /// <returns> Если удалили, то <see langword="true"/>, иначе - <see langword="false"/>. </returns>
        public bool RemoveIllustrator(Illustrator illustrator)
        {
            this.Illustrator = null;

            return illustrator is not null
                && illustrator.Books.Remove(this);
        }

        /// <summary>
        /// Меняет тип издания.
        /// </summary>
        /// <param name="bookType"> Тип издания. </param>
        /// <returns>
        /// Если изменили, то <see langword="true"/>, иначе - <see langword="false"/>.
        /// </returns>
        public bool ChangeBookType(BookType bookType)
        {
            if (bookType is null)
            {
                return false;
            }

            var result = this.BookType.Books.Remove(this);
            this.BookType = bookType;
            return result && bookType.Books.Add(this);
        }

        /// <summary>
        /// Меняет издательства.
        /// </summary>
        /// <param name="publisher"> Издательство. </param>
        /// <returns>
        /// Если изменили, то <see langword="true"/>, иначе - <see langword="false"/>.
        /// </returns>
        public bool ChangePublisher(Publisher publisher)
        {
            if (publisher is null)
            {
                return false;
            }

            var result = this.Publisher.Books.Remove(this);
            this.Publisher = publisher;
            return result && publisher.Books.Add(this);
        }

        /// <summary>
        /// Добавляет тег.
        /// </summary>
        /// <param name="tag"> Тег. </param>
        /// <returns>
        /// Если добавили, то <see langword="true"/>, иначе - <see langword="false"/>.
        /// </returns>
        public bool AddTag(Tag tag)
        {
            return tag is not null
                && this.Tags.Add(tag)
                && tag.Books.Add(this);
        }

        /// <summary>
        /// Удаляет тег.
        /// </summary>
        /// <param name="tag"> Тег. </param>
        /// <returns>
        /// Если удалили, то <see langword="true"/>, иначе - <see langword="false"/>.
        /// </returns>
        public bool RemoveTag(Tag tag)
        {
            return tag is not null
                && this.Tags.Remove(tag)
                && tag.Books.Remove(this);
        }
    }
}
