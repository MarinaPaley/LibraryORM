// <copyright file="Author.cs" company="Васильева М.А">
// Copyright (c) Васильева М.А. All rights reserved.
// </copyright>

namespace Domain
{
    using System;

    /// <summary>
    /// Класс Автор.
    /// </summary>
    public class Author : IEquatable<Author>
    {
        /// <summary>
        /// Инициализирует новый экземпляр класса <see cref="Author"/>.
        /// </summary>
        /// <param name="familyName"> Фамилия.</param>
        /// <param name="firstName"> Имя. </param>
        /// <param name="patronicName"> Отчество. </param>
        /// <param name="dateBirth"> Дата рождения. </param>
        /// <param name="dateDeath"> Дата смерти. </param>
        /// <exception cref="ArgumentNullException">
        /// Если имя или фамилия <see langword="null"/>.
        /// </exception>
        public Author(string familyName, string firstName, string? patronicName, DateOnly? dateBirth, DateOnly? dateDeath)
        {
            this.FamilyName = familyName ?? throw new ArgumentNullException(nameof(familyName));
            this.FirstName = firstName ?? throw new ArgumentNullException(nameof(firstName));
            this.PatronicName = patronicName;
            this.DateBirth = dateBirth;
            this.DateDeath = dateDeath;
        }

        /// <summary>
        /// Идентификатор.
        /// </summary>
        public Guid Id { get; }

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
        public string? PatronicName { get; }

        /// <summary>
        /// Дата рождения.
        /// </summary>
        public DateOnly? DateBirth { get; set; }

        /// <summary>
        /// Дата смерти.
        /// </summary>
        public DateOnly? DateDeath { get; set; }

        /// <inheritdoc/>
        public bool Equals(Author? other)
        {
            throw new NotImplementedException();
        }

        /// <inheritdoc/>
        public override bool Equals(object obj)
        {
            return Equals(obj as Author);
        }

        public override int GetHashCode()
        {
            throw new NotImplementedException();
        }
    }
}
