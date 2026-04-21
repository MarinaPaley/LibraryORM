// <copyright file="Publisher.cs" company="Филипченко Марина Алексеевна">
// Copyright (c) Филипченко Марина Алексеевна 2026. Library.
// </copyright>

namespace Domain
{
    using System;
    using Domain.Abstract;

    /// <summary>
    /// Издательство.
    /// </summary>
    public sealed class Publisher : Entity<Publisher>, IEquatable<Publisher>
    {
        /// <summary>
        /// Инициализирует новый экземпляр класса <see cref="Publisher"/>.
        /// </summary>
        /// <param name="name"> Название. </param>
        /// <param name="address"> Адрес. </param>
        /// <exception cref="ArgumentNullException"> если name или address <see langword="null"/>.</exception>
        public Publisher(string name, Address address)
        {
            this.Name = new Title(name);
            this.Address = address ?? throw new ArgumentNullException(nameof(address));
        }

        [Obsolete("For ORM only")]
        private Publisher()
        {
        }

        /// <summary>
        /// Название.
        /// </summary>
        public Title Name { get; set; }

        /// <summary>
        /// Адрес.
        /// </summary>
        public Address Address { get; set; }

        /// <inheritdoc/>
        public override bool Equals(Publisher? other)
        {
            return ReferenceEquals(this, other) || ((other is not null) && (this.Id == other.Id));
        }

        /// <inheritdoc/>
        public override bool Equals(object? obj) => this.Equals(obj as Publisher);

        /// <inheritdoc/>
        public override int GetHashCode() => this.Id.GetHashCode();

        /// <inheritdoc/>
        public override string ToString() => $"{this.Name}";
    }
}
