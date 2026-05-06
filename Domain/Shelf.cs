// <copyright file="Shelf.cs" company="Филипченко Марина Алексеевна">
// Copyright (c) Филипченко Марина Алексеевна 2026. Library.
// </copyright>

namespace Domain
{
    using System;
    using System.Collections.Generic;
    using Domain.Abstract;
    using Staff;

    /// <summary>
    /// Полка.
    /// </summary>
    public sealed class Shelf : NamedEntity<Shelf>
    {
        /// <summary>
        /// Инициализирует новый экземпляр класса <see cref="Shelf"/>.
        /// </summary>
        /// <param name="name"> Название полки. </param>
        public Shelf(string name)
            : base(name)
        {
        }

#pragma warning disable CS8618 // Необходимо для работы с обязательными полями, получаемыми не через конструктор.

        [Obsolete("For ORM only")]
        private Shelf()
            : base("Не задано")
        {
        }

#pragma warning restore CS8618

        /// <summary>
        /// Шкаф.
        /// </summary>
        public Cabinet? Cabinet { get; set; }

        /// <summary>
        ///  Книги.
        /// </summary>
        public IList<Item> Items { get; } = [];

        /// <summary>
        /// Добавляет книгу на полку.
        /// </summary>
        /// <param name="item">Книга. </param>
        public void AddBook(Item item)
        {
            this.Items.Add(item);
            item!.Shelf = this;
        }

        /// <summary>
        /// Снимаем книгу с полки.
        /// </summary>
        /// <param name="item">Книга.</param>
        /// <returns><see langword="true"/> если убрали, иначе <see langword="false"/>.</returns>
        public bool RemoveBook(Item item)
        {
            var result = item is not null
                && this.Items.Remove(item);

            if (result)
            {
                item!.Shelf = null;
            }

            return result;
        }

        /// <inheritdoc />
        public override bool Equals(Shelf? other)
        {
            return ReferenceEquals(this, other)
                || (NamedEntityComparer<Shelf>.Instance.Equals(this, other)
                && NamedEntityComparer<Cabinet>.Instance.Equals(this.Cabinet, other.Cabinet));
        }

        /// <inheritdoc/>
        public override bool Equals(object? obj) => this.Equals(obj as Shelf);

        /// <inheritdoc/>
        public override int GetHashCode() => HashCode.Combine(this.Name, this.Cabinet);

        /// <inheritdoc cref="object.ToString()"/>
        public override string ToString()
        {
            var location = this.Cabinet is not null
                ? $" ({this.Cabinet.Name} → {this.Cabinet.Room?.Name})"
                : string.Empty;

            return this.Items.Count == 0
                ? $"Полка: {this.Name}{location}"
                : $"Полка: {this.Name}{location} | Книги: {this.Items.Join()}";
        }
    }
}
