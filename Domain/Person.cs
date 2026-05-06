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

        /// <summary>
        /// Инициализирует новый экземпляр класса <see cref="Person"/>.
        /// </summary>
        /// <param name="familyName"> Фамилия. </param>
        /// <param name="firstName"> Имя. </param>
        /// <param name="patronymicName"> Отчество. </param>
        /// <param name="dateBirth"> Дата рождения. </param>
        /// <param name="dateDeath"> Дата смерти. </param>
        public Person(
            string familyName,
            string firstName,
            string? patronymicName = null,
            DateOnly? dateBirth = null,
            DateOnly? dateDeath = null)
            : this(new Name(familyName, firstName, patronymicName), dateBirth, dateDeath)
        {
        }

        /// <summary>
        /// Инициализирует новый экземпляр класса <see cref="Person"/>.
        /// </summary>
        /// <param name="familyName">  Фамилия. </param>
        /// <param name="firstName"> Имя.</param>
        /// <param name="patronymicName"> Отчество. </param>
        /// <param name="birthYear"> Дата рождения. </param>
        /// <param name="deathYear"> Дата смерти. </param>
        public Person(
            string familyName,
            string firstName,
            string? patronymicName = null,
            int? birthYear = null,
            int? deathYear = null)
            : this(
                  new Name(familyName, firstName, patronymicName),
                  birthYear.HasValue ? new DateOnly(birthYear.Value, 1, 1) : null,
                  deathYear.HasValue ? new DateOnly(deathYear.Value, 1, 1) : null)
        {
        }

#pragma warning disable CS8618 // Необходимо для работы с обязательными полями, получаемыми не через конструктор.
        /// <summary>
        /// Инициализирует новый экземпляр класса <see cref="Person"/>.
        /// </summary>
        [Obsolete("For ORM only")]
        private Person()
        {
        }
#pragma warning restore CS8618

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

        /// <summary>
        /// Персона является Автором.
        /// </summary>
        public Author? Author { get; set; }

        /// <summary>
        /// Персона является Редактором.
        /// </summary>
        public Editor? Editor { get; set; }

        /// <summary>
        /// Персона является переводчиком.
        /// </summary>
        public Translator? Translator { get; set; }

        /// <summary>
        /// Персона является Рецензентом.
        /// </summary>
        public Reviewer? Reviewer { get; set; }

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
