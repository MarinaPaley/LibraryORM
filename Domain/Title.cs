// <copyright file="Title.cs" company="Филипченко Марина Алексеевна">
// Copyright (c) Филипченко Марина Алексеевна 2026. Library.
// </copyright>

namespace Domain
{
    using System;
    using Staff;

    /// <summary>
    /// Value Object Название.
    /// </summary>
    public sealed class Title : IEquatable<Title>
    {
        private string value;

        /// <summary>
        ///  Инициализирует новый экземпляр класса <see cref="Title"/>.
        /// </summary>
        /// <param name="value"> Значение. </param>
        public Title(string value)
        {
            this.Value = value;
        }

        /// <summary>
        /// Значение.
        /// </summary>
        public string Value
        {
            get => this.value;
            private set => this.value = value.TrimOrNull() ?? throw new ArgumentNullException(nameof(value));
        }

        public static bool operator ==(Title? lha, Title? rha) => Equals(lha, rha);

        public static bool operator !=(Title? lha, Title? rha) => !Equals(lha, rha);

        /// <inheritdoc/>
        public bool Equals(Title? other)
        {
            return ReferenceEquals(this, other) || ((other is not null) && (this.Value == other.Value));
        }

        /// <inheritdoc/>
        public override bool Equals(object? obj) => this.Equals(obj as Title);

        /// <inheritdoc/>
        public override int GetHashCode() => this.Value.GetHashCode();

        /// <inheritdoc cref="object.ToString()"/>
        public override string ToString() => this.Value;
    }
}
