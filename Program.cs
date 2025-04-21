using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using SalesAnalyticsConsole.Data;
using SalesAnalyticsConsole.Services;

namespace Assessment;
class Program
{
    private const string Path = @"C:\Users\vishw_ged6020\source\repos\Assessment\Assessment\CsvFile.csv";

    public static async Task Main(string[] args)
    {
        var serviceProvider = new ServiceCollection()
                .AddDbContext<AppDbContext>(options =>
                    options.UseSqlite("Data Source=sales.db"))
                .AddScoped<CsvLoader>()
                .AddScoped<RevenueCalculator>()
                .BuildServiceProvider();

        using (var scope = serviceProvider.CreateScope())
        {
            var db = scope.ServiceProvider.GetRequiredService<AppDbContext>();
            db.Database.Migrate();
        }

        while (true)
        {
            Console.WriteLine("\n== Sales Analytics CLI ==");
            Console.WriteLine("1. Load CSV");
            Console.WriteLine("2. Calculate Total Revenue");
            Console.WriteLine("3. Exit");
            Console.Write("Choice: ");
            var input = Console.ReadLine();

            using var scope = serviceProvider.CreateScope();
            var loader = scope.ServiceProvider.GetRequiredService<CsvLoader>();
            var calculator = scope.ServiceProvider.GetRequiredService<RevenueCalculator>();

            switch (input)
            {
                case "1":
                    await loader.LoadFromCsvAsync(Path);
                    Console.WriteLine("✅ CSV Loaded.");
                    break;
                case "2":
                    Console.Write("Start Date (yyyy-MM-dd): ");
                    var start = DateTime.Parse(Console.ReadLine());
                    Console.Write("End Date (yyyy-MM-dd): ");
                    var end = DateTime.Parse(Console.ReadLine());
                    var revenue = await calculator.GetTotalRevenueAsync(start, end);
                    Console.WriteLine($"💰 Total Revenue: {revenue:C}");
                    break;
                case "3":
                    return;
            }
        }

    }
}