// <copyright file="Genre.cs" company="Филипченко Марина Алексеевна">
// Copyright (c) Филипченко Марина Алексеевна 2026. Library.
// </copyright>

namespace Domain
{
    using System;
    using System.Collections.Generic;
    using Domain.Abstract;

    /// <summary>
    /// Жанр.
    /// </summary>
    public sealed class Genre : Entity<Genre>, IEquatable<Genre>
    {
        /// <summary>
        /// Инициализирует новый экземпляр класса <see cref="Genre"/>.
        /// </summary>
        /// <param name="name"> Жанр. </param>
        public Genre(string name)
        {
            this.Name = new Title(name);
        }

        [Obsolete("For ORM only")]
        private Genre()
        {
        }

        /// <summary>
        /// Жанр.
        /// </summary>
        public Title Name { get; set; }

        /// <summary>
        /// Рукописи.
        /// </summary>
        public ISet<Manuscript> Manuscripts { get; } = new HashSet<Manuscript>();

        /// <inheritdoc/>
        public override bool Equals(Genre? other)
        {
            return ReferenceEquals(this, other) || ((other is not null) && (this.Name == other.Name));
        }

        /// <inheritdoc/>
        public override bool Equals(object? obj) => this.Equals(obj as Genre);

        /// <inheritdoc/>
        public override int GetHashCode() => this.Name?.GetHashCode() ?? 0;

        /// <inheritdoc/>
        public override string ToString() => this.Name.ToString();
    }
}
