// <copyright file="Manuscript.cs" company="Филипченко Марина Алексеевна">
// Copyright (c) Филипченко Марина Алексеевна 2026. Library.
// </copyright>

namespace Domain
{
    using System;
    using System.Collections.Generic;
    using Domain.Abstract;
    using Staff;

    /// <summary>
    /// Рукопись.
    /// </summary>
    public sealed class Manuscript : BilingualNamedEntity<Manuscript>, IEquatable<Manuscript>
    {
        /// <summary>
        /// Инициализирует новый экземпляр класса <see cref="Manuscript"/>.
        /// </summary>
        /// <param name="name"> Название произведения. </param>
        /// <param name="authors"> Авторы.</param>
        /// <param name="date"> Дата написания. </param>
        /// <param name="languages"> Язык.</param>
        /// <param name="origin"> Оригинальное название. </param>
        /// <exception cref="ArgumentNullException"> Если авторы или жанры <see langword="null"/>.</exception>
        /// <exception cref="ArgumentOutOfRangeException"> Если количество страниц равно ли меньше нуля.</exception>
        public Manuscript(string name, ISet<Language> languages, ISet<Author> authors, Range<DateOnly>? date = null, string? origin = null)
            : base(name, origin)
        {
            this.Dates = date;
            this.Languages = languages ?? throw new ArgumentNullException(nameof(languages));
            this.Authors = authors ?? throw new ArgumentNullException(nameof(authors));
            if (this.Authors.Count == 0)
            {
                throw new ArgumentOutOfRangeException(nameof(authors));
            }

            foreach (var author in authors)
            {
                _ = author.Manuscripts.Add(this);
            }
        }

        /// <summary>
        /// Инициализирует новый экземпляр класса <see cref="Manuscript"/>.
        /// </summary>
        /// <param name="name"> Название произведения.</param>
        /// <param name="from"> Дата начала написания. </param>
        /// <param name="to"> Дата конца написания. </param>
        /// <param name="languages"> Язык. </param
        /// <param name="origin"> Оригинальное наименование. </param>
        /// <param name="authors"> Список авторов. </param>
        public Manuscript(string name, ISet<Language> languages, DateOnly? from = null, DateOnly? to = null, string? origin = null, params Author[] authors)
            : this(name, new HashSet<Language>(languages), new HashSet<Author>(authors), new Range<DateOnly>(from, to), origin)
        {
        }

#pragma warning disable CS8618 // Необходимо для работы с обязательными полями, получаемыми не через конструктор.

        /// <summary>
        /// Инициализирует новый экземпляр класса <see cref="Manuscript"/>.
        /// </summary>
        [Obsolete("For ORM only", true)]
        private Manuscript()
            : base(name: "Не задано", origin: null)
        {
        }
#pragma warning restore CS8618

        /// <summary>
        /// Авторы.
        /// </summary>
        public ISet<Author> Authors { get; } = new HashSet<Author>(PersonComparer<Author>.Instance);

        /// <summary>
        /// Переводчики.
        /// </summary>
        public ISet<Translator> Translators { get; } = new HashSet<Translator>();

        /// <summary>
        /// Рецензенты.
        /// </summary>
        public ISet<Reviewer> Reviewers { get; } = new HashSet<Reviewer>();

        /// <summary>
        /// Дата создания произведения.
        /// </summary>
        public Range<DateOnly>? Dates { get; set; }

        /// <summary>
        /// Жанры.
        /// </summary>
        public ISet<Genre> Genres { get; } = new HashSet<Genre>();

        /// <summary>
        /// Язык.
        /// </summary>
        public ISet<Language> Languages { get; } = new HashSet<Language>();

        /// <summary>
        /// Книги, в которых напечатано данное произведение.
        /// </summary>
        public ISet<Book> Books { get; } = new HashSet<Book>();

        /// <inheritdoc cref="object.ToString()"/>
        public override string ToString()
        {
            return $"{base.ToString()}: [{this.Authors.Join()}]";
        }

        /// <inheritdoc/>
        public override bool Equals(Manuscript? other)
        {
            return base.Equals(other)
                && this.Authors.SetEquals(other.Authors);
        }

        /// <inheritdoc/>
        public override bool Equals(object? obj) => this.Equals(obj as Manuscript);

        /// <summary>
        /// Добавляет жанр произведению.
        /// </summary>
        /// <param name="genre"> Жанр. </param>
        /// <returns> Если добавили, то <see langword="true"/>, иначе - <see langword="false"/>. </returns>
        public bool AddGenre(Genre genre)
        {
            return genre is not null
                && this.Genres.Add(genre)
                && genre.Manuscripts.Add(this);
        }

        /// <summary>
        /// Удаляет жанр из произведения.
        /// </summary>
        /// <param name="genre"> Жанр. </param>
        /// <returns> <see langword="true"/>, если удалили, иначе - <see langword="false"/>.</returns>
        public bool RemoveGenre(Genre genre)
        {
            return genre is not null
                && this.Genres.Remove(genre)
                && genre.Manuscripts.Remove(this);
        }

        /// <inheritdoc/>
        public override int GetHashCode()
        {
            return HashCode.Combine(this.Name, this.Authors);
        }
    }
}