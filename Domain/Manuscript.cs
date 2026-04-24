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
    public sealed class Manuscript : Entity<Manuscript>, IEquatable<Manuscript>
    {
        /// <summary>
        /// Инициализирует новый экземпляр класса <see cref="Manuscript"/>.
        /// </summary>
        /// <param name="name"> Название произведения. </param>
        /// <param name="authors"> Авторы.</param>
        /// <param name="date"> Дата написания. </param>
        /// <param name="language"> Язык.</param>
        /// <exception cref="ArgumentNullException"> Если авторы или жанры <see langword="null"/>.</exception>
        /// <exception cref="ArgumentOutOfRangeException"> Если количество страниц равно ли меньше нуля.</exception>
        public Manuscript(string name, Language language, ISet<Author> authors, Range<DateOnly>? date = null)
        {
            this.Title = new Title(name);
            this.Dates = date;
            this.Language = language ?? throw new ArgumentNullException(nameof(language));
            this.Authors = authors ?? throw new ArgumentNullException(nameof(authors));
            foreach (var author in authors)
            {
                author.AddManuscript(this);
            }
        }

        /// <summary>
        /// Инициализирует новый экземпляр класса <see cref="Manuscript"/>.
        /// </summary>
        /// <param name="name"> Название произведения.</param>
        /// <param name="from"> Дата начала написания. </param>
        /// <param name="to"> Дата конца написания. </param>
        /// <param name="language"> Язык. </param>
        /// <param name="authors"> Список авторов. </param>
        public Manuscript(string name, Language language, DateOnly? from = null, DateOnly? to = null, params Author[] authors)
            : this(name, language, new HashSet<Author>(authors), new Range<DateOnly>(from, to))
        {
        }

#pragma warning disable CS8618 // Необходимо для работы с обязательными полями, получаемыми не через конструктор.
        /// <summary>
        /// Инициализирует новый экземпляр класса <see cref="Manuscript"/>.
        /// </summary>
        [Obsolete("For ORM only", true)]
        private Manuscript()
        {
        }
#pragma warning restore CS8618

        /// <summary>
        /// Название произведения.
        /// </summary>
        public Title Title { get; set; }

        /// <summary>
        /// Авторы.
        /// </summary>
        public ISet<Author> Authors { get; } = new HashSet<Author>();

        /// <summary>
        /// Переводчики.
        /// </summary>
        public ISet<Translator> Translators { get; } = new HashSet<Translator>();

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
        public Language Language { get; set; }

        /// <summary>
        /// Книги, в которых напечатано данное произведение.
        /// </summary>
        public ISet<Book> Books { get; } = new HashSet<Book>();

        /// <inheritdoc/>
        public override bool Equals(Manuscript? other)
        {
            return ReferenceEquals(this, other) || ((other is not null) && (this.Title == other.Title));
        }

        /// <inheritdoc/>
        public override bool Equals(object? obj) => this.Equals(obj as Manuscript);

        /// <inheritdoc/>
        public override int GetHashCode() => this.Id.GetHashCode();

        /// <inheritdoc/>
        public override string ToString()
        {
            return this.Authors.Count > 0
                ? $"{this.Title} {this.Authors.Join()}"
                : this.Title.ToString();
        }

        /// <summary>
        /// Добавляет жанр произведению.
        /// </summary>
        /// <param name="genre"> Жанр. </param>
        /// <returns> Если добавили, то <see langword="true"/>, иначе - <see langword="false"/>. </returns>
        public bool AddGenre(Genre genre)
        {
            ArgumentNullException.ThrowIfNull(genre);
            var result = this.Genres.Add(genre);
            result &= genre.Manuscripts.Add(this);

            return result;
        }
    }
}