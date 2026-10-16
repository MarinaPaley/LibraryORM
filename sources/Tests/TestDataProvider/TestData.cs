// <copyright file="TestData.cs" company="Филипченко Марина Алексеевна">
// Copyright (c) Филипченко Марина Алексеевна 2026. Library.
// </copyright>

namespace TestDataProvider
{
    using Domain;

    /// <summary>
    /// Фабрика тестовых данных (Object Mother) для быстрого создания сложных агрегатов.
    /// </summary>
    public static class TestData
    {
        // ==========================================
        // Персоны и Роли
        // ==========================================

        /// <summary>
        /// Создает валидную персону с дефолтными настройками.
        /// </summary>
        /// <returns> Экземпляр строителя для персоны. </returns>
        public static PersonBuilder ValidPerson() => new PersonBuilder();

        /// <summary>
        /// Создает валидного автора с дефолтными настройками.
        /// </summary>
        /// <returns> Экземпляр строителя для автора. </returns>
        public static AuthorBuilder ValidAuthor() => new AuthorBuilder();

        /// <summary>
        /// Создает валидного редактора с дефолтными настройками.
        /// </summary>
        /// <returns> Экземпляр строителя для редактора. </returns>
        public static EditorBuilder ValidEditor() => new EditorBuilder();

        /// <summary>
        /// Создает валидного художника с дефолтными настройками.
        /// </summary>
        /// <returns> Экземпляр строителя для художника. </returns>
        public static IllustratorBuilder ValidIllustrator() => new IllustratorBuilder();

        /// <summary>
        /// Создает валидного рецензента с дефолтными настройками.
        /// </summary>
        /// <returns> Экземпляр строителя для рецензента. </returns>
        public static ReviewerBuilder ValidReviewer() => new ReviewerBuilder();

        /// <summary>
        /// Создает валидного переводчика с дефолтными настройками.
        /// </summary>
        /// <returns> Экземпляр строителя для переводчика. </returns>
        public static TranslatorBuilder ValidTranslator() => new TranslatorBuilder();

        // ==========================================
        // Произведения и Книги
        // ==========================================

        /// <summary>
        /// Создает валидную рукопись с дефолтными настройками.
        /// </summary>
        /// <returns> Экземпляр строителя для рукописи. </returns>
        public static ManuscriptBuilder ValidManuscript() => new ManuscriptBuilder();

        /// <summary>
        /// Создает валидную книгу с дефолтными настройками.
        /// </summary>
        /// <returns> Экземпляр строителя для книги. </returns>
        public static BookBuilder ValidBook() => new BookBuilder();

        /// <summary>
        /// Создает валидный экземпляр книги (Item) с дефолтными настройками.
        /// </summary>
        /// <returns> Экземпляр строителя для экземпляра книги. </returns>
        public static ItemBuilder ValidItem() => new ItemBuilder();

        /// <summary>
        /// Создает валидный тип книги (BookType) с дефолтными настройками.
        /// </summary>
        /// <returns> Экземпляр строителя для типа книги. </returns>
        public static BookTypeBuilder ValidBookType() => new BookTypeBuilder();

        /// <summary>
        /// Создает валидный жанр с дефолтными настройками.
        /// </summary>
        /// <returns> Экземпляр строителя для жанра. </returns>
        public static GenreBuilder ValidGenre() => new GenreBuilder();

        /// <summary>
        /// Создает валидный язык с дефолтными настройками.
        /// </summary>
        /// <returns> Экземпляр строителя для языка. </returns>
        public static LanguageBuilder ValidLanguage() => new LanguageBuilder();

        /// <summary>
        /// Создает валиную серию с дефолтными настройкми.
        /// </summary>
        /// <returns> Экземпляр строителя для серии. </returns>
        public static SeriaBuilder ValidSeria() => new SeriaBuilder();

        // ==========================================
        // Классификация и Теги
        // ==========================================

        /// <summary>
        /// Создает валидную категорию с дефолтными настройками.
        /// </summary>
        /// <returns> Экземпляр строителя для категории. </returns>
        public static CategoryBuilder ValidCategory() => new CategoryBuilder();

        /// <summary>
        /// Создает валидный тег с дефолтными настройками.
        /// </summary>
        /// <returns> Экземпляр строителя для тега. </returns>
        public static TagBuilder ValidTag() => new TagBuilder();

        /// <summary>
        /// Создает валидный цвет с дефолтными настройками.
        /// </summary>
        /// <returns> Экземпляр строителя для цвета. </returns>
        public static ColorBuilder ValidColor() => new ColorBuilder();

        // ==========================================
        // Локация и Хранение
        // ==========================================

        /// <summary>
        /// Создает валидный город с дефолтными настройками.
        /// </summary>
        /// <returns> Экземпляр строителя для города. </returns>
        public static CityBuilder ValidCity() => new CityBuilder();

        /// <summary>
        /// Создает валидную улицу с дефолтными настройками.
        /// </summary>
        /// <returns> Экземпляр строителя для улицы. </returns>
        public static StreetBuilder ValidStreet() => new StreetBuilder();

        /// <summary>
        /// Создает валидный адрес с дефолтными настройками.
        /// </summary>
        /// <returns> Экземпляр строителя для адреса. </returns>
        public static AddressBuilder ValidAddress() => new AddressBuilder();

        /// <summary>
        /// Создает валидную комнату с дефолтными настройками.
        /// </summary>
        /// <returns> Экземпляр строителя для комнаты. </returns>
        public static RoomBuilder ValidRoom() => new RoomBuilder();

        /// <summary>
        /// Создает валидный шкаф с дефолтными настройками.
        /// </summary>
        /// <returns> Экземпляр строителя для шкафа. </returns>
        public static CabinetBuilder ValidCabinet() => new CabinetBuilder();

        /// <summary>
        /// Создает валидную полку с дефолтными настройками.
        /// </summary>
        /// <returns> Экземпляр строителя для полки. </returns>
        public static ShelfBuilder ValidShelf() => new ShelfBuilder();

        /// <summary>
        /// Создает валидное идательство с дефолтными настройками.
        /// </summary>
        /// <returns> Экземпляр издательства. </returns>
        public static PublisherBuilder ValidPublisher() => new PublisherBuilder();
    }
}