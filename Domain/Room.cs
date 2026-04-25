// <copyright file="Room.cs" company="Филипченко Марина Алексеевна">
// Copyright (c) Филипченко Марина Алексеевна 2026. Library.
// </copyright>

namespace Domain
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Domain.Abstract;
    using Staff;

    /// <summary>
    /// Комната.
    /// </summary>
    public sealed class Room : Entity<Room>, IEquatable<Room>
    {
        /// <summary>
        /// Инициализирует новый экземпляр класса <see cref="Room"/>.
        /// </summary>
        /// <param name="address"> Адрес. </param>
        /// <param name="name"> Название комнаты. </param>
        /// <exception cref="ArgumentNullException">
        /// Если адрес или название <see langword="null"/>.
        /// </exception>
        public Room(Address address, string name)
        {
            this.Address = address ?? throw new ArgumentNullException(nameof(address));
            this.Name = new Title(name ?? throw new ArgumentNullException(nameof(name)));
        }

#pragma warning disable CS8618 // Необходимо для работы с обязательными полями, получаемыми не через конструктор.
        /// <summary>
        /// Инициализирует новый экземпляр класса <see cref="Room"/>.
        /// </summary>
        [Obsolete("For ORM only", true)]
        private Room()
        {
        }
#pragma warning restore CS8618

        /// <summary>
        /// Адрес.
        /// </summary>
        public Address Address { get; set; }

        /// <summary>
        /// Название комнаты.
        /// </summary>
        public Title Name { get; set; }

        /// <summary>
        /// Шкафы в комнате.
        /// </summary>
        public ISet<Cabinet> Cabinets { get; } = new HashSet<Cabinet>();

        /// <summary>
        /// Добавляет шкаф в комнату.
        /// </summary>
        /// <param name="cabinet"> Шкаф. </param>
        /// <returns> <see langword="true"/>, если добавили, иначе - <see langword="false"/>. </returns>
        public bool AddCabinet(Cabinet cabinet)
        {
            var result = cabinet is not null && this.Cabinets.Add(cabinet);
            if (result)
            {
                cabinet!.Room = this;
            }

            return result;
        }

        /// <summary>
        /// Удаляет шкаф из комнаты.
        /// </summary>
        /// <param name="cabinet"> Шкаф. </param>
        /// <returns> <see langword="true"/>, если удалили, иначе - <see langword="false"/>. </returns>
        public bool RemoveCabinet(Cabinet cabinet)
        {
            var result = cabinet is not null && this.Cabinets.Remove(cabinet);
            if (result)
            {
                cabinet!.Room = null;
            }

            return result;
        }

        /// <inheritdoc/>
        public override bool Equals(Room? other)
        {
            return ReferenceEquals(this, other)
                || (other is not null
                    && this.Name == other.Name
                    && this.Address.Equals(other.Address));
        }

        /// <inheritdoc/>
        public override bool Equals(object? obj) => this.Equals(obj as Room);

        /// <inheritdoc/>
        public override int GetHashCode() => this.Name?.GetHashCode() ?? 0;

        /// <inheritdoc cref="object.ToString()"/>
        public override string ToString()
        {
            return this.Cabinets.Count == 0
                ? $"Комната: {this.Name} ({this.Address})"
                : $"Комната: {this.Name} ({this.Address}) | Шкафы: {this.Cabinets.Join()}";
        }
    }
}