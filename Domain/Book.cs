// <copyright file="Book.cs" company="Васильева Марина Алексеевна">
// Copyright (c) Васильева Марина Алексеевна 2024. Library.
// </copyright>

namespace Domain
{
    using System;
    using System.Collections.Generic;
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
        /// <param name="ibsn"> Код ibsn. </param>
        /// <param name="authors"> Авторы. </param>
        /// <param name="shelf"> Полка. </param>
        /// <exception cref="ArgumentNullException">Если название книги или код <see langword="null"/>. </exception>
        /// <exception cref="ArgumentOutOfRangeException"> Если количество страниц меньше или равно нулю. </exception>
        /// <exception cref="ArgumentOutOfRangeException"> Если полка <see langword="null"/>. </exception>
        /// <exception cref="ArgumentOutOfRangeException"> Если авторы <see langword="null"/>. </exception>
        public Book(string title, int pages, string ibsn, ISet<Author> authors, Shelf? shelf = null)
        {

            this.Title = title.TrimOrNull() ?? throw new ArgumentNullException(nameof(title));

            ArgumentOutOfRangeException.ThrowIfNegativeOrZero(pages);
            this.Pages = pages;

            this.IBSN = ibsn.TrimOrNull() ?? throw new ArgumentNullException(nameof(ibsn));

            this.Authors = authors ?? throw new ArgumentNullException(nameof(authors));
            foreach (var author in authors)
            {
                author.AddBook(this);
            }

            this.Shelf = shelf;
            if (this.Shelf is not null)
            {
                _ = this.Shelf.AddBook(this);
            }
        }

        /// <summary>
        /// Инициализирует новый экземпляр класса <see cref="Book"/>.
        /// </summary>
        /// <param name="title"> Название.</param>
        /// <param name="pages"> Количество страниц. </param>
        /// <param name="ibsn"> Код IBSN. </param>
        /// <param name="authors">Авторы.</param>
        /// <param name="shelf">Полка. </param>
        /// <exception cref="ArgumentNullException">Если название книги или код <see langword="null"/>.</exception>
        /// <exception cref="ArgumentOutOfRangeException"> Если количество страниц меньше или равно нулю.</exception>
        /// <exception cref="ArgumentOutOfRangeException"> Если полка <see langword="null"/>.</exception>
        /// <exception cref="ArgumentOutOfRangeException"> Если авторы <see langword="null"/>.</exception>
        public Book(string title, int pages, string ibsn, Shelf? shelf = null, params Author[] authors)
            : this(title, pages, ibsn, new HashSet<Author>(authors), shelf)
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
        public string Title { get; }

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
        /// Авторы.
        /// </summary>
        public ISet<Author> Authors { get; } = new HashSet<Author>();

        /// <inheritdoc/>
        public override bool Equals(Book? other)
        {
            return ReferenceEquals(this, other) || ((other is not null) && (this.Title == other.Title));
        }

        /// <inheritdoc/>
        public override bool Equals(object? obj)
        {
            return this.Equals(obj as Book);
        }

        /// <inheritdoc/>
        public override int GetHashCode()
        {
            return this.Title.GetHashCode();
        }

        /// <inheritdoc/>
        public override string ToString()
        {
            return this.Authors.Count > 0
                ? $"{this.Title} {this.Authors.Join()}"
                : this.Title;
        }
    }
}