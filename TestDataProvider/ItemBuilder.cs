// <copyright file="ItemBuilder.cs" company="Филипченко Марина Алексеевна">
// Copyright (c) Филипченко Марина Алексеевна 2026. Library.
// </copyright>

namespace TestDataProvider
{
    using Domain;

    /// <summary>
    /// Строитель для создания тестовых экземпляров <see cref="Item"/>.
    /// </summary>
    public sealed class ItemBuilder
    {
        /// <summary>
        /// Инициализирует новый экземпляр класса <see cref="ItemBuilder"/>.
        /// </summary>
        public ItemBuilder()
        {
        }

        private Book Book { get; set; } = new BookBuilder();

        private Shelf? Shelf { get; set; }

        /// <summary>
        /// Неявное преобразование строителя в сущность <see cref="Item"/>.
        /// </summary>
        /// <param name="builder"> Экземпляр строителя. </param>
        public static implicit operator Item(ItemBuilder builder) => builder.Build();

        /// <summary>
        /// Устанавливает книгу для данного экземпляра.
        /// </summary>
        /// <param name="book"> Экземпляр книги. </param>
        /// <returns> Текущий экземпляр строителя. </returns>
        public ItemBuilder WithBook(Book book)
        {
            this.Book = book;
            return this;
        }

        /// <summary>
        /// Устанавливает полку для данного экземпляра.
        /// </summary>
        /// <param name="shelf"> Экземпляр полки. </param>
        /// <returns> Текущий экземпляр строителя. </returns>
        public ItemBuilder WithShelf(Shelf? shelf)
        {
            this.Shelf = shelf;
            return this;
        }

        /// <summary>
        /// Создает и возвращает экземпляр <see cref="Item"/>.
        /// </summary>
        /// <returns> Настроенный экземпляр книги. </returns>
        public Item Build()
        {
            var item = new Item(this.Book);
            item.Shelf = this.Shelf;
            return item;
        }
    }
}