// <copyright file="Person.cs" company="Васильева Марина Алексеевна">
// Copyright (c) Васильева Марина Алексеевна 2024. Library.
// </copyright>

namespace Domain
{
    using System;
    using System.Text;
    using Staff;

    /// <summary>
    /// Персона.
    /// </summary>
    /// <typeparam name="TPerson"> Конкретный тип персоны. </typeparam>
    public abstract class Person<TPerson> : Entity<TPerson>
        where TPerson : Person<TPerson>
    {
        /// <summary>
        /// Инициализирует новый экземпляр класса <see cref="Person{TPerson}"/>.
        /// </summary>
        /// <param name="fullName"> Полное имя.</param>
        /// <param name="dateBirth"> Дата рождения. </param>
        /// <param name="dateDeath"> Дата смерти. </param>
        /// <exception cref="ArgumentNullException">
        /// Если Полное имя <see langword="null"/>.
        /// </exception>
        protected Person(
            Name fullName,
            DateOnly? dateBirth = null,
            DateOnly? dateDeath = null)
        {
            this.FullName = fullName ?? throw new ArgumentNullException(nameof(fullName));
            this.DateBirth = dateBirth;
            this.DateDeath = dateDeath;
        }

        /// <summary>
        /// Полное имя.
        /// </summary>
        public Name FullName { get; }

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
            return obj is TPerson person && this.Equals(person);
        }

        /// <inheritdoc/>
        public override bool Equals(TPerson? other)
        {
            return base.Equals(other)
                && this.FullName == other.FullName
                && this.DateBirth == other.DateBirth
                && this.DateDeath == other.DateDeath;
        }

        /// <inheritdoc/>
        public override int GetHashCode() => HashCode.Combine(this.FullName, this.DateBirth, this.DateDeath);

        /// <inheritdoc/>
        public override string ToString()
        {
            return new StringBuilder()
                .Append(this.FullName)
                .AppendIf(this.DateBirth is not null, $" Год рождения: {this.DateBirth}")
                .AppendIf(this.DateDeath is not null, $" Год смерти: {this.DateDeath}")
                .ToString();
        }
    }
}
