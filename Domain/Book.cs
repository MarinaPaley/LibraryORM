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
    public sealed class Book : Entity<Book>, IEquatable<Book>
    {
        /// <summary>
        /// Инициализирует новый экземпляр класса <see cref="Book"/>.
        /// </summary>
        /// <param name="title"> Название. </param>
        /// <param name="pages"> Количество страниц. </param>
        /// <param name="ibsn"> Код <c>IBSN</c>. </param>
        /// <param name="bookType"> Тип книги. </param>
        /// <param name="publisher"> Издательство.</param>
        /// <param name="year"> Год издания. </param>
        /// <param name="manuscripts"> Рукописи. </param>
        /// <param name="shelf"> Полка. </param>
        /// <exception cref="ArgumentNullException">Если название книги или код <see langword="null"/> или
        /// Издательство <see langword="null"/> или
        /// Тип издания <see langword="null"/>. </exception>
        /// <exception cref="ArgumentOutOfRangeException"> Если количество страниц меньше или равно нулю или год издания не валиден. </exception>
        public Book(string? title, int pages, string ibsn, BookType bookType, Publisher publisher, int year, ISet<Manuscript> manuscripts, Shelf? shelf = null)
        {
            this.BookType = bookType ?? throw new ArgumentNullException(nameof(bookType));
            this.Publisher = publisher ?? throw new ArgumentNullException(nameof(publisher));
            this.Title = title;

            ArgumentOutOfRangeException.ThrowIfNegativeOrZero(pages);
            this.Pages = pages;
            ArgumentOutOfRangeException.ThrowIfNegativeOrZero(year);
            ArgumentOutOfRangeException.ThrowIfGreaterThan(year, DateTime.Now.Year);
            this.Year = year;

            this.IBSN = ibsn.TrimOrNull() ?? throw new ArgumentNullException(nameof(ibsn));

            this.Shelf = shelf;
            if (this.Shelf is not null)
            {
                _ = this.Shelf.AddBook(this);
            }

            this.Manuscripts = manuscripts ?? throw new ArgumentNullException(nameof(manuscripts));
            foreach (var manuscript in manuscripts)
            {
                manuscript.Books.Add(this);
            }
        }

        /// <summary>
        /// Инициализирует новый экземпляр класса <see cref="Book"/>.
        /// </summary>
        /// <param name="title"> Название.</param>
        /// <param name="pages"> Количество страниц. </param>
        /// <param name="ibsn"> Код <c>IBSN</c>. </param>
        /// <param name="bookType"> Тип издания. </param>
        /// <param name="publisher"> Издательство. </param>
        /// <param name="year"> Год издания. </param>
        /// <param name="manuscripts"> Рукописи. </param>
        /// <param name="shelf" > Полка. </param>
        /// <exception cref="ArgumentNullException"> Если название книги или код <see langword="null"/>.</exception>
        /// <exception cref="ArgumentOutOfRangeException"> Если количество страниц меньше или равно нулю.</exception>
        public Book(
            string title,
            int pages,
            string ibsn,
            BookType bookType,
            Publisher publisher,
            int year,
            Shelf? shelf = null,
            params Manuscript[] manuscripts)
            : this(title, pages, ibsn, bookType, publisher, year, new HashSet<Manuscript>(manuscripts), shelf)
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
        /// Код ibsn.
        /// </summary>
        public string IBSN { get; }

        /// <summary>
        /// Полка.
        /// </summary>
        public Shelf? Shelf { get; set; }

        /// <summary>
        /// Издательство.
        /// </summary>
        public Publisher Publisher { get; set; }

        /// <summary>
        /// Тип книги.
        /// </summary>
        public BookType BookType { get; set; }

        /// <summary>
        /// Год издания.
        /// </summary>
        public int Year { get; set; }

        /// <summary>
        /// Рукописи в книге.
        /// </summary>
        public ISet<Manuscript> Manuscripts { get; } = new HashSet<Manuscript>();

        /// <summary>
        /// Редактор.
        /// </summary>
        public Editor? Editor { get; set; }

        /// <inheritdoc/>
        public override bool Equals(Book? other)
        {
            return ReferenceEquals(this, other) || ((other is not null) && (this.Title == other.Title));
        }

        /// <inheritdoc/>
        public override bool Equals(object? obj) => this.Equals(obj as Book);

        /// <inheritdoc/>
        public override int GetHashCode() => HashCode.Combine(this.Manuscripts, this.IBSN, this.BookType);

        /// <inheritdoc/>
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
            this.Editor = editor;

            return editor is not null
                && editor.Books.Remove(this);
        }
    }
}