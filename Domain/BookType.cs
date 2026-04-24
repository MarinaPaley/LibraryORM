// <copyright file="BookType.cs" company="Филипченко Марина Алексеевна">
// Copyright (c) Филипченко Марина Алексеевна 2026. Library.
// </copyright>

namespace Domain
{
    using System;
    using System.Collections.Generic;
    using Domain.Abstract;

    /// <summary>
    /// Тип напечатанного произведения (книга, методичка, статья).
    /// </summary>
    public sealed class BookType : Entity<BookType>, IEquatable<BookType>
    {
        /// <summary>
        /// Инициализирует новый экземпляр класса <see cref="BookType"/>.
        /// </summary>
        /// <param name="name"> Жанр. </param>
        public BookType(string name)
        {
            this.BookTypeName = new Title(name);
        }

        [Obsolete("For ORM only")]
        private BookType()
        {
        }

        /// <summary>
        /// Тип книги (книга, методичка, журнал).
        /// </summary>
        public Title BookTypeName { get; set; }

        /// <summary>
        /// Книги.
        /// </summary>
        public ISet<Book> Books { get; } = new HashSet<Book>();

        /// <inheritdoc/>
        public override bool Equals(BookType? other)
        {
            return ReferenceEquals(this, other) || ((other is not null) && (this.BookTypeName == other.BookTypeName));
        }

        /// <inheritdoc/>
        public override bool Equals(object? obj) => this.Equals(obj as BookType);

        /// <inheritdoc/>
        public override int GetHashCode() => this.BookTypeName?.GetHashCode() ?? 0;

        /// <inheritdoc/>
        public override string ToString() => this.BookTypeName.ToString();
    }
}
