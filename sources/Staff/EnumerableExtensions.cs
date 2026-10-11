// <copyright file="EnumerableExtensions.cs" company="Филипченко Марина Алексеевна">
// Copyright (c) Филипченко Марина Алексеевна 2026. Library.
// </copyright>

namespace Staff
{
    using System.Collections.Generic;

    /// <summary>
    /// Коллекция методов расширений для типа <see cref="IEnumerable{T}"/>.
    /// </summary>
    public static class EnumerableExtensions
    {
        /// <inheritdoc cref="string.Join{T}(string?,IEnumerable{T})"/>
        public static string Join<T>(this IEnumerable<T> values, string separator = ", ")
            => string.Join(separator, values);
    }
}
