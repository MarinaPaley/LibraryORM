// <copyright file="Name.cs" company="Филипченко Марина Алексеевна">
// Copyright (c) Филипченко Марина Алексеевна 2026. Library.
// </copyright>

namespace Domain
{
    using System;
    using Staff;

    /// <summary>
    /// Полное имя.
    /// </summary>
    public sealed class Name : IEquatable<Name>
    {
        /// <summary>
        /// Имя-заглушка.
        /// </summary>
        public static readonly Name Unknown = new ("Неизвестно", "Неизвестно");

        private string familyName;

        private string firstName;

        private string? patronymicName;

        /// <summary>
        /// Инициализирует новый экземпляр класса <see cref="Name"/>.
        /// </summary>
        /// <param name="familyName">Фамилия.</param>
        /// <param name="firstName">Имя.</param>
        /// <param name="patronymicName">Отчество.</param>
        /// <exception cref="ArgumentNullException">Если имя или фамилия <see langword="null"/>.</exception>
        public Name(string familyName, string firstName, string? patronymicName = null)
        {
            this.FamilyName = familyName;
            this.FirstName = firstName;
            this.PatronymicName = patronymicName;
        }

        /// <summary>
        /// Инициализирует новый экземпляр класса <see cref="Name"/>.
        /// </summary>
        [Obsolete("For ORN only", true)]
        private Name()
        {
        }

        /// <summary>
        /// Фамилия.
        /// </summary>
        public string FamilyName
        {
            get => this.familyName;
            private set => this.familyName = value.TrimOrNull()
                ?? throw new ArgumentNullException(nameof(value));
        }

        /// <summary>
        /// Имя.
        /// </summary>
        public string FirstName
        {
            get => this.firstName;
            private set => this.firstName = value.TrimOrNull()
                ?? throw new ArgumentNullException(nameof(value));
        }

        /// <summary>
        /// Отчество.
        /// </summary>
        public string? PatronymicName
        {
            get => this.patronymicName;
            private set => this.patronymicName = value?.TrimOrNull();
        }

        public static bool operator ==(Name? lha, Name? rha)
        {
            if (lha is null || rha is null)
            {
                return false;
            }

            return lha.Equals(rha);
        }

        public static bool operator !=(Name? lha, Name? rha) => !(lha == rha);

        /// <inheritdoc/>
        public bool Equals(Name? other)
        {
            return other is not null
                 && this.FamilyName == other.FamilyName
                 && this.FirstName == other.FirstName
                 && this.PatronymicName == other.PatronymicName;
        }

        /// <inheritdoc/>
        public override bool Equals(object? obj) => this.Equals(obj as Name);

        /// <inheritdoc/>
        public override int GetHashCode() =>
            HashCode.Combine(this.FamilyName, this.FirstName, this.PatronymicName);

        /// <inheritdoc/>
        public override string ToString()
        {
            return this.PatronymicName is null
               ? $"{this.FamilyName} {this.FirstName}"
               : $"{this.FamilyName} {this.FirstName} {this.PatronymicName}";
        }
    }
}
