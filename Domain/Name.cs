// <copyright file="Name.cs" company="Васильева Марина Алексеевна">
// Copyright (c) Васильева Марина Алексеевна 2024. Library.
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
        /// Инициализирует новый экземпляр класса <see cref="Name"/>.
        /// </summary>
        /// <param name="familyName">Фамилия.</param>
        /// <param name="firstName">Имя.</param>
        /// <param name="patronicName">Отчество.</param>
        /// <exception cref="ArgumentNullException">Если имя или фамилия <see langword="null"/>.</exception>
        public Name(string familyName, string firstName, string? patronicName = null)
        {
            this.FamilyName = familyName.TrimOrNull() ?? throw new ArgumentNullException(nameof(familyName));
            this.FirstName = firstName.TrimOrNull() ?? throw new ArgumentNullException(nameof(firstName));
            this.PatronicName = patronicName;
        }

        /// <summary>
        /// Фамилия.
        /// </summary>
        public string FamilyName { get; }

        /// <summary>
        /// Имя.
        /// </summary>
        public string FirstName { get; }

        /// <summary>
        /// Отчество.
        /// </summary>
        public string? PatronicName { get; } = null;

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
                 && this.PatronicName == other.PatronicName;
        }

        /// <inheritdoc/>
        public override bool Equals(object? obj) => this.Equals(obj as Name);

        /// <inheritdoc/>
        public override int GetHashCode() =>
            HashCode.Combine(this.FamilyName, this.FirstName, this.PatronicName);

        /// <inheritdoc/>
        public override string ToString()
        {
            return this.PatronicName is null
               ? $"{this.FamilyName} {this.FirstName}"
               : $"{this.FamilyName} {this.FirstName} {this.PatronicName}";
        }
    }
}
