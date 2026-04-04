using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using CarToGo.Data;
using CarToGo.Models;

public static class SeedData
{
    public static void Initialize(IServiceProvider serviceProvider)
    {
        using (var context = new CarToGoContext(serviceProvider.GetRequiredService<DbContextOptions<CarToGoContext>>()))
        {
            // Ако вече има коли – не правим нищо
            if (context.Cars.Any())
                return;

            context.Cars.AddRange(
            new Car
            {
                Brand = "BMW",
                Model = "320d",
                Year = 2018,
                Seats = 5,
                PricePerDay = 90,
                Description = "Reliable diesel sedan with excellent performance and comfort."
            },
            new Car
            {
                Brand = "Audi",
                Model = "A4",
                Year = 2019,
                Seats = 5,
                PricePerDay = 95,
                Description = "Luxury sedan with modern technology and efficient engine."
            },
            new Car
            {
                Brand = "Mercedes",
                Model = "C220",
                Year = 2020,
                Seats = 5,
                PricePerDay = 110,
                Description = "Premium vehicle offering high comfort and advanced safety features."
            },
            new Car
            {
                Brand = "Volkswagen",
                Model = "Golf",
                Year = 2017,
                Seats = 5,
                PricePerDay = 70,
                Description = "Compact and fuel‑efficient hatchback ideal for city driving."
            },
            new Car
            {
                Brand = "Toyota",
                Model = "Corolla",
                Year = 2016,
                Seats = 5,
                PricePerDay = 65,
                Description = "Extremely reliable car with low fuel consumption and great durability."
            }
        );

            context.SaveChanges();
        }
    }
}

