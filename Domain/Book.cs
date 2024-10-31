// <copyright file="Book.cs" company="Васильева Марина Алексеевна">
// Copyright (c) Васильева Марина Алексеевна 2024. Library.
// </copyright>

namespace Domain
{
    using System;
    using Staff;

    /// <summary>
    /// Класс Книга.
    /// </summary>
    public sealed class Book : IEquatable<Book>
    {
        /// <summary>
        /// Инициализирует новый экземпляр класса <see cref="Book"/>.
        /// </summary>
        /// <param name="title"> Название.</param>
        /// <param name="pages"> Количество страниц. </param>
        /// <param name="ibsn"> Код IBSN. </param>
        /// <exception cref="ArgumentNullException">Если название книги или код <see langword="null"/>.</exception>
        /// <exception cref="ArgumentOutOfRangeException"> Если количество страниц меньше или равно нулю.</exception>
        /// <exception cref="ArgumentOutOfRangeException"> Если полка <see langword="null"/>.</exception>
        /// <exception cref="ArgumentOutOfRangeException"> Если авторы <see langword="null"/>.</exception>
        public Book(string title, int pages, string ibsn)
        {
            this.Title = title.TrimOrNull() ?? throw new ArgumentNullException(nameof(title));
            ArgumentOutOfRangeException.ThrowIfNegativeOrZero(pages);

            this.Pages = pages;
            this.IBSN = ibsn.TrimOrNull() ?? throw new ArgumentNullException(nameof(ibsn));
            this.Id = Guid.Empty;
        }

        /// <summary>
        /// Идентификатор.
        /// </summary>
        public Guid Id { get; }

        /// <summary>
        /// Название.
        /// </summary>
        public string Title { get; }

        /// <summary>
        /// Количество страниц.
        /// </summary>
        public int Pages { get; }

        /// <summary>
        /// Код IBSN.
        /// </summary>
        public string IBSN { get; }

        /// <inheritdoc/>
        public bool Equals(Book? other)
        {
            if (other is null)
            {
                return false;
            }

            if (ReferenceEquals(this, other))
            {
                return true;
            }

            if (this.Title == other.Title)
            {
                return true;
            }

            return false;
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
            return this.Title;
        }
    }
}