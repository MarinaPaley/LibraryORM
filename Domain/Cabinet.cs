// <copyright file="Cabinet.cs" company="Филипченко Марина Алексеевна">
// Copyright (c) Филипченко Марина Алексеевна 2026. Library.
// </copyright>

namespace Domain
{
    using Domain.Abstract;
    using Staff;
    using System;
    using System.Collections.Generic;
    using System.Linq;

    /// <summary>
    /// Шкаф.
    /// </summary>
    public sealed class Cabinet : Entity<Cabinet>, IEquatable<Cabinet>
    {
        /// <summary>
        /// Инициализирует новый экземпляр класса <see cref="Cabinet"/>.
        /// </summary>
        /// <param name="room"> Комната. </param>
        /// <param name="name"> Название шкафа. </param>
        /// <exception cref="ArgumentNullException">
        /// Если комната или название <see langword="null"/>.
        /// </exception>
        public Cabinet(Room room, string name)
        {
            this.Room = room ?? throw new ArgumentNullException(nameof(room));
            this.Name = new Title(name ?? throw new ArgumentNullException(nameof(name)));
        }

#pragma warning disable CS8618 // Необходимо для работы с обязательными полями, получаемыми не через конструктор.
        /// <summary>
        /// Инициализирует новый экземпляр класса <see cref="Cabinet"/>.
        /// </summary>
        [Obsolete("For ORM only", true)]
        private Cabinet()
        {
        }
#pragma warning restore CS8618

        /// <summary>
        /// Комната.
        /// </summary>
        public Room? Room { get; set; }

        /// <summary>
        /// Название шкафа.
        /// </summary>
        public Title Name { get; set; }

        /// <summary>
        /// Полки в шкафу.
        /// </summary>
        public ISet<Shelf> Shelves { get; } = new HashSet<Shelf>();

        /// <summary>
        /// Добавляет полку в шкаф.
        /// </summary>
        /// <param name="shelf"> Полка. </param>
        /// <returns> <see langword="true"/>, если добавили, иначе - <see langword="false"/>. </returns>
        public bool AddShelf(Shelf shelf)
        {
            var result = shelf is not null && this.Shelves.Add(shelf);
            if (result)
            {
                shelf!.Cabinet = this;
            }

            return result;
        }

        /// <summary>
        /// Удаляет полку из шкафа.
        /// </summary>
        /// <param name="shelf"> Полка. </param>
        /// <returns> <see langword="true"/>, если удалили, иначе - <see langword="false"/>. </returns>
        public bool RemoveShelf(Shelf shelf)
        {
            var result = shelf is not null && this.Shelves.Remove(shelf);
            if (result)
            {
                shelf!.Cabinet = null;
            }

            return result;
        }

        /// <inheritdoc/>
        public override bool Equals(Cabinet? other)
        {
            return ReferenceEquals(this, other)
                || (other is not null
                && this.Name == other.Name
                && this.Room is not null
                && this.Room.Equals(other.Room));
        }

        /// <inheritdoc/>
        public override bool Equals(object? obj) => this.Equals(obj as Cabinet);

        /// <inheritdoc/>
        public override int GetHashCode() => this.Name?.GetHashCode() ?? 0;

        /// <inheritdoc cref="object.ToString()"/>
        public override string ToString()
        {
            return this.Shelves.Count == 0
               ? $"Шкаф: {this.Name} ({this.Room?.Name})"
               : $"Шкаф: {this.Name} ({this.Room?.Name}) | Полки: {this.Shelves.Join()}";
        }
    }
}