// <copyright file="Program.cs" company="Васильева Марина Алексеевна">
// Copyright (c) Васильева Марина Алексеевна 2024. Library.
// </copyright>

namespace Demo
{
    using System;
    using System.Linq;
    using DataAccessLayer;
    using Domain;
    using Repository;

    /// <summary>
    /// Точка входа в программу.
    /// </summary>
    internal static class Program
    {
        private static void Main()
        {
            var dataContext = new DataContext();

            try
            {
                var shelfRepository = new ShelfRepository(dataContext);
                _ = shelfRepository.Create(new Shelf("Полка"));
                _ = shelfRepository.Save();

                foreach (var shelf in shelfRepository.GetAll())
                {
                    Console.WriteLine($"{shelf.Id} --> {shelf.Name}");
                }
            }
            catch (Exception exception)
            {
                Console.Error.WriteLine(exception.Message);
            }
        }
    }
}
