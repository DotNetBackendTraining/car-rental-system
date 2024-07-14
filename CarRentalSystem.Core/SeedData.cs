using CarRentalSystem.Core.Entities;

namespace CarRentalSystem.Core;

public static class SeedData
{
    public static IEnumerable<Car> GetCarsList()
    {
        yield return new Car
        {
            Id = 1,
            Make = "Toyota",
            Model = "Camry",
            Year = 2020,
            Location = "New York",
            ImageName = "2k55x9_1280x960-min.png"
        };
        yield return new Car
        {
            Id = 2,
            Make = "Honda",
            Model = "Civic",
            Year = 2019,
            Location = "Los Angeles",
            ImageName = "2k5559_1280x960-min.png"
        };
        yield return new Car
        {
            Id = 3,
            Make = "Ford",
            Model = "Mustang",
            Year = 2021,
            Location = "Chicago",
            ImageName = "3keee6_1280x960-min.png"
        };
        yield return new Car
        {
            Id = 4,
            Make = "Chevrolet",
            Model = "Malibu",
            Year = 2020,
            Location = "Houston",
            ImageName = "5gw135_1280x960-min.png"
        };
        yield return new Car
        {
            Id = 5,
            Make = "Nissan",
            Model = "Altima",
            Year = 2018,
            Location = "Phoenix",
            ImageName = "d5qqjo_1280x960-min.png"
        };
        yield return new Car
        {
            Id = 6,
            Make = "BMW",
            Model = "3 Series",
            Year = 2021,
            Location = "Philadelphia",
            ImageName = "d5qqz3_1280x960-min.png"
        };
        yield return new Car
        {
            Id = 7,
            Make = "Mercedes-Benz",
            Model = "C-Class",
            Year = 2019,
            Location = "San Antonio",
            ImageName = "gjllll_1280x960-min.png"
        };
        yield return new Car
        {
            Id = 8,
            Make = "Audi",
            Model = "A4",
            Year = 2020,
            Location = "San Diego",
            ImageName = "w8rrj6_1280x960-min.png"
        };
        yield return new Car
        {
            Id = 9,
            Make = "Lexus",
            Model = "ES",
            Year = 2021,
            Location = "Dallas",
            ImageName = "x122yv_1280x960-min.png"
        };
    }
}