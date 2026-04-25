// <copyright file="Language.cs" company="Филипченко Марина Алексеевна">
// Copyright (c) Филипченко Марина Алексеевна 2026. Library.
// </copyright>

namespace Domain
{
    using System;
    using System.Collections.Generic;
    using Domain.Abstract;

    /// <summary>
    /// Язык.
    /// </summary>
    public sealed class Language : Entity<Language>, IEquatable<Language>
    {
        /// <summary>
        /// Инициализирует новый экземпляр класса <see cref="Language"/>.
        /// </summary>
        /// <param name="name"> Язык. </param>
        public Language(string name)
        {
            this.Name = new Title(name);
        }

        [Obsolete("For ORM only")]
        private Language()
        {
        }

        /// <summary>
        /// Язык.
        /// </summary>
        public Title Name { get; set; }

        /// <summary>
        /// Рукописи.
        /// </summary>
        public ISet<Manuscript> Manuscripts { get; } = new HashSet<Manuscript>();

        /// <inheritdoc/>
        public override bool Equals(Language? other)
        {
            return ReferenceEquals(this, other) || ((other is not null) && (this.Name == other.Name));
        }

        /// <inheritdoc/>
        public override bool Equals(object? obj) => this.Equals(obj as Language);

        /// <inheritdoc/>
        public override int GetHashCode() => this.Name?.GetHashCode() ?? 0;

        /// <inheritdoc cref="object.ToString()"/>
        public override string ToString() => this.Name.ToString();
    }
}
