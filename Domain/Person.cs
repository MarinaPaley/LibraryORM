// <copyright file="Person.cs" company="Филипченко Марина Алексеевна">
// Copyright (c) Филипченко Марина Алексеевна 2026. Library.
// </copyright>

namespace Domain
{
    using System;
    using System.Globalization;
    using System.Text;
    using Domain.Abstract;
    using Staff;

    /// <summary>
    /// Персона.
    /// </summary>
    public sealed class Person : Entity<Person>, IEquatable<Person>
    {
        private readonly CultureInfo culture = new ("ru-RU");

        /// <summary>
        /// Инициализирует новый экземпляр класса <see cref="Person"/>.
        /// </summary>
        /// <param name="fullName"> Полное имя.</param>
        /// <param name="dateBirth"> Дата рождения. </param>
        /// <param name="dateDeath"> Дата смерти. </param>
        /// <exception cref="ArgumentNullException">
        /// Если Полное имя <see langword="null"/>.
        /// </exception>
        public Person(
            Name fullName,
            DateOnly? dateBirth = null,
            DateOnly? dateDeath = null)
        {
            this.FullName = fullName ?? throw new ArgumentNullException(nameof(fullName));
            this.DateBirth = dateBirth;
            this.DateDeath = dateDeath;
        }

        [Obsolete("For ORM only")]
        private Person()
        {
        }

        /// <summary>
        /// Полное имя.
        /// </summary>
        public Name FullName { get; private set; }

        /// <summary>
        /// Дата рождения.
        /// </summary>
        public DateOnly? DateBirth { get; set; }

        /// <summary>
        /// Дата смерти.
        /// </summary>
        public DateOnly? DateDeath { get; set; }

        /// <inheritdoc/>
        public override bool Equals(object? obj)
        {
            return obj is Person person && this.Equals(person);
        }

        /// <inheritdoc/>
        public override bool Equals(Person? other)
        {
            return base.Equals(other)
                && this.FullName == other.FullName
                && this.DateBirth == other.DateBirth
                && this.DateDeath == other.DateDeath;
        }

        /// <inheritdoc cref="object.ToString()"/>
        public override int GetHashCode() => HashCode.Combine(this.FullName, this.DateBirth, this.DateDeath);

        /// <inheritdoc/>
        public override string ToString()
        {
            var dateBirth = this.DateBirth.HasValue
                ? $" Год рождения: {this.DateBirth.Value.ToString("dd.MM.yyyy", this.culture)}"
                : string.Empty;

            var dateDeath = this.DateDeath.HasValue
                ? $" Год смерти: {this.DateDeath.Value.ToString("dd.MM.yyyy", this.culture)}"
                : string.Empty;

            return new StringBuilder()
                .Append(this.FullName)
                .AppendIf(this.DateBirth is not null, dateBirth)
                .AppendIf(this.DateDeath is not null, dateDeath)
                .ToString();
        }
    }
}
