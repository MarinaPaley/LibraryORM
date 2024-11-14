// <copyright file="Person.cs" company="Васильева Марина Алексеевна">
// Copyright (c) Васильева Марина Алексеевна 2024. Library.
// </copyright>

namespace Domain
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics.CodeAnalysis;
    using System.Text;

    /// <summary>
    /// Класс Персона.
    /// </summary>
    public abstract class Person : IEqualityComparer<Person>
    {
        /// <summary>
        /// Инициализирует новый экземпляр класса <see cref="Person"/>.
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
            this.Id = Guid.Empty;
            this.FullName = fullName ?? throw new ArgumentNullException(nameof(fullName));
            this.DateBirth = dateBirth;
            this.DateDeath = dateDeath;
        }

        /// <summary>
        /// Идентификатор.
        /// </summary>
        protected Guid Id { get; }

        /// <summary>
        /// Полное имя.
        /// </summary>
        protected Name FullName { get; }

        /// <summary>
        /// Дата рождения.
        /// </summary>
        protected DateOnly? DateBirth { get; set; }

        /// <summary>
        /// Дата смерти.
        /// </summary>
        protected DateOnly? DateDeath { get; set; }

        /// <inheritdoc/>
        public bool Equals(Person? lha, Person? rha)
        {
            if (lha is null || rha is null)
            {
                return false;
            }

            return lha.FullName == rha.FullName && lha.DateBirth == rha.DateBirth && lha.DateDeath == rha.DateDeath;
        }

        /// <inheritdoc/>
        public int GetHashCode([DisallowNull] Person obj) => HashCode.Combine(obj.FullName, obj.DateBirth, obj.DateDeath);

        /// <inheritdoc/>
        public override string ToString()
        {
            var buffer = new StringBuilder();
            buffer.Append(this.FullName);
            if (this.DateBirth is not null)
            {
                _ = buffer.Append($" Год рождения: {this.DateBirth}");
            }

            if (this.DateDeath is not null)
            {
                _ = buffer.Append($" Год смерти: {this.DateDeath}");
            }

            return buffer.ToString();
        }
    }
}
