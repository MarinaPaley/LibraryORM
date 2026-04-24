// <copyright file="Editor.cs" company="Филипченко Марина Алексеевна">
// Copyright (c) Филипченко Марина Алексеевна 2026. Library.
// </copyright>

namespace Domain
{
    using System;
    using System.Collections.Generic;
    using Domain.Abstract;

    /// <summary>
    /// Редактор.
    /// </summary>
    public sealed class Editor : Contributor, IEquatable<Editor>
    {
        /// <summary>
        /// Инициализирует новый экземпляр класса <see cref="Editor"/>.
        /// </summary>
        /// <param name="person"> Персона. </param>
        /// <exception cref="ArgumentNullException">
        /// Если Полное имя <see langword="null"/>.
        /// </exception>
        public Editor(Person person)
            : base(person)
        {
        }

        /// <summary>
        /// Инициализирует новый экземпляр класса <see cref="Editor"/>.
        /// </summary>
        [Obsolete("For ORM only", true)]
        private Editor()
        {
        }

        /// <summary>
        /// Книги.
        /// </summary>
        public ISet<Book> Books { get; } = new HashSet<Book>();

        /// <inheritdoc/>
        public bool Equals(Editor? other)
        {
            return ReferenceEquals(this, other) || ((other is not null) && this.Person.Equals(other.Person));
        }

        /// <inheritdoc/>
        public override bool Equals(object? obj) => this.Equals(obj as Editor);

        /// <inheritdoc/>
        public override int GetHashCode() => this.Person?.GetHashCode() ?? 0;

        /// <inheritdoc/>
        public override string ToString() => this.Person.ToString();
    }
}
