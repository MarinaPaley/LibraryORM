// <copyright file="Role.cs" company="Филипченко Марина Алексеевна">
// Copyright (c) Филипченко Марина Алексеевна 2026. Library.
// </copyright>

namespace Domain
{
    /// <summary>
    /// Роли.
    /// </summary>
    public enum Role
    {
        /// <summary>
        /// По умолчанию.
        /// </summary>
        None = 0,

        /// <summary>
        /// Автор.
        /// </summary>
        Author = 1,

        /// <summary>
        /// Переводчик.
        /// </summary>
        Translator = 2,

        /// <summary>
        /// Редактор.
        /// </summary>
        Editor = 3,
    }
}
