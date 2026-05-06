// <copyright file="Program.cs" company="Филипченко Марина Алексеевна">
// Copyright (c) Филипченко Марина Алексеевна 2026. Library.
// </copyright>
namespace WebAPI
{
    using Microsoft.EntityFrameworkCore;
    using Repository;

    /// <summary>
    /// Программа.
    /// </summary>
    public static class Program
    {
        /// <summary>
        /// Точка вход в программу.
        /// </summary>
        /// <param name="args"> Список параметров. </param>
        /// <exception cref="InvalidOperationException"> При невозможности создать программу. </exception>
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            var connectionString =
                builder.Configuration.GetConnectionString("Default")
                ?? throw new InvalidOperationException("Connection string"
                + "'DefaultConnection' not found.");

            builder.Services.AddDbContext<DataAccessLayer.DataContext>(
                opt => opt.UseNpgsql(connectionString)
                    .EnableDetailedErrors()
                    .EnableSensitiveDataLogging()
                    .LogTo(Console.WriteLine, LogLevel.Error));

            builder.Services.AddScoped<ShelfRepository>();

            builder.Services.AddControllers();

            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger(x => x.OpenApiVersion = Microsoft.OpenApi.OpenApiSpecVersion.OpenApi3_1);
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();

            app.MapControllers();

            app.Run();
        }
    }
}
