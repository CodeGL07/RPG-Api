using Microsoft.EntityFrameworkCore;
using Rpg_Api.Data;
using System;

public class Program
{
    public static void Main(string[] args)
    {
        // Comentário commit Aula 03
        var builder = WebApplication.CreateBuilder(args);

        builder.Services.AddDbContext<DataContext>(options =>
        {
            options.UseSqlServer(builder.Configuration.GetConnectionString("ConexaoLocal"));
        });

        // Add services to the container.
        builder.Services.AddOpenApi();
        builder.Services.AddControllers();

        var app = builder.Build();

        // Configure the HTTP request pipeline.
        if (app.Environment.IsDevelopment())
        {
            app.MapOpenApi();
        }

        app.UseHttpsRedirection();

        var summaries = new[]
        {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };

        app.MapGet("/weatherforecast", () =>
        {
            var forecast = Enumerable.Range(1, 5).Select(index =>
                new WeatherForecast
                (
                    DateOnly.FromDateTime(DateTime.Now.AddDays(index)),
                    Random.Shared.Next(-20, 55),
                    summaries[Random.Shared.Next(summaries.Length)]
                ))
                .ToArray();
            return forecast;
        })
        .WithName("GetWeatherForecast");

        app.MapControllers();

        // Se quiser testar os métodos, chame-os aqui antes do app.Run():
        // CalcularMedia();
        // VerificarAulaEtec();

        app.Run();
    }

    public static void CalcularMedia()
    {
        Console.WriteLine("Digite a primeira nota");
        decimal nota1 = decimal.Parse(Console.ReadLine()!);

        Console.WriteLine("Digite a segunda nota");
        decimal nota2 = decimal.Parse(Console.ReadLine()!);

        decimal media = (nota1 + nota2) / 2;
        Console.WriteLine($"A média é {media}");

        if (media >= 7)
            Console.WriteLine("Aprovado");
        else if (media < 7 && media >= 4)
            Console.WriteLine("Recuperado");
        else
            Console.WriteLine("Reprovado");
    }

    public static void VerificarAulaEtec()
    {
        Console.WriteLine("Digite a data (ex: 2026-06-06)");
        DateTime data = DateTime.Parse(Console.ReadLine()!);

        if (data.DayOfWeek == DayOfWeek.Saturday || data.DayOfWeek == DayOfWeek.Sunday)
        {
            Console.WriteLine("Final de semana! Hoje não tem aula! Revisarei exercícios.");
        }
        else
        {
            Console.WriteLine("Dia da semana! Bora pra Etec!");
        }
    }
}

record WeatherForecast(DateOnly Date, int TemperatureC, string? Summary)
{
    public int TemperatureF => 32 + (int)(TemperatureC / 0.5556);
}