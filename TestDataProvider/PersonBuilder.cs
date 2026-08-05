// <copyright file="PersonBuilder.cs" company="Филипченко Марина Алексеевна">
// Copyright (c) Филипченко Марина Алексеевна 2026. Library.
// </copyright>

namespace TestDataProvider
{
    using System;
    using Domain;

    /// <summary>
    /// Строитель для создания тестовых экземпляров <see cref="Person"/>.
    /// </summary>
    public sealed class PersonBuilder
    {
        private bool hasCustomFamilyName;
        private bool hasCustomFirstName;
        private bool hasCustomPatronymicName;
        private bool hasCustomDateBirth;
        private bool hasCustomDateDeath;

        /// <summary>
        /// Инициализирует новый экземпляр класса <see cref="PersonBuilder"/>.
        /// </summary>
        public PersonBuilder()
        {
        }

        private string? FamilyName { get; set; }

        private string? FirstName { get; set; }

        private string? PatronymicName { get; set; }

        private DateOnly? DateBirth { get; set; }

        private DateOnly? DateDeath { get; set; }

        /// <summary>
        /// Неявное преобразование строителя в сущность <see cref="Person"/>.
        /// </summary>
        /// <param name="builder"> Экземпляр строителя. </param>
        public static implicit operator Person(PersonBuilder builder) => builder.Build();

        /// <summary>
        /// Устанавливает фамилию персоны.
        /// </summary>
        /// <param name="familyName"> Фамилия. </param>
        /// <returns> Текущий экземпляр строителя. </returns>
        public PersonBuilder WithFamilyName(string familyName)
        {
            this.FamilyName = familyName;
            this.hasCustomFamilyName = true;
            return this;
        }

        /// <summary>
        /// Устанавливает имя персоны.
        /// </summary>
        /// <param name="firstName"> Имя. </param>
        /// <returns> Текущий экземпляр строителя. </returns>
        public PersonBuilder WithFirstName(string firstName)
        {
            this.FirstName = firstName;
            this.hasCustomFirstName = true;
            return this;
        }

        /// <summary>
        /// Устанавливает отчество персоны.
        /// </summary>
        /// <param name="patronymicName"> Отчество. </param>
        /// <returns> Текущий экземпляр строителя. </returns>
        public PersonBuilder WithPatronymicName(string? patronymicName)
        {
            this.PatronymicName = patronymicName;
            this.hasCustomPatronymicName = true;
            return this;
        }

        /// <summary>
        /// Устанавливает дату рождения персоны.
        /// </summary>
        /// <param name="dateBirth"> Дата рождения. </param>
        /// <returns> Текущий экземпляр строителя. </returns>
        public PersonBuilder WithDateBirth(DateOnly? dateBirth)
        {
            this.DateBirth = dateBirth;
            this.hasCustomDateBirth = true;
            return this;
        }

        /// <summary>
        /// Устанавливает дату смерти персоны.
        /// </summary>
        /// <param name="dateDeath"> Дата смерти. </param>
        /// <returns> Текущий экземпляр строителя. </returns>
        public PersonBuilder WithDateDeath(DateOnly? dateDeath)
        {
            this.DateDeath = dateDeath;
            this.hasCustomDateDeath = true;
            return this;
        }

        /// <summary>
        /// Создает и возвращает экземпляр <see cref="Person"/>.
        /// </summary>
        /// <returns> Настроенная персона. </returns>
        public Person Build()
        {
            if (this.hasCustomFamilyName || this.hasCustomFirstName || this.hasCustomPatronymicName ||
                this.hasCustomDateBirth || this.hasCustomDateDeath)
            {
                if (!this.hasCustomFamilyName)
                {
                    this.FamilyName = null;
                }

                if (!this.hasCustomFirstName)
                {
                    this.FirstName = null;
                }

                if (!this.hasCustomPatronymicName)
                {
                    this.PatronymicName = null;
                }

                if (!this.hasCustomDateBirth)
                {
                    this.DateBirth = null;
                }

                if (!this.hasCustomDateDeath)
                {
                    this.DateDeath = null;
                }
            }
            else
            {
                this.FamilyName ??= "Иванов";
                this.FirstName ??= "Иван";
                this.PatronymicName ??= "Иванович";
                this.DateBirth ??= new DateOnly(1990, 1, 1);
            }

            return new Person(this.FamilyName!, this.FirstName!, this.PatronymicName, this.DateBirth, this.DateDeath);
        }
    }
}