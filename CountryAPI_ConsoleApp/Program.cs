using CountryAPI.BL.Controllers;
using CountryAPI.BL.Database;
using CountryAPI.BL.Database.Repository;
using System;

namespace CountryAPI_ConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {
            var countryApiController = new CountryApiController(new CountryApiRepository(new CountryApiContext()));

            while (true)
            {
                Console.WriteLine("Select action: \nGet the country from API - G \nOutput all countries from database - O \nExit from the program - E");
                var action = Console.ReadKey();
                Console.WriteLine();

                switch (action.Key)
                {
                    case ConsoleKey.G:
                        Console.Write("Enter name of country: ");
                        var nameCountry = Console.ReadLine();

                        var country = countryApiController.GetCountry(nameCountry);

                        if (country == null)
                        {
                            Console.WriteLine("Country not found");
                        }
                        else
                        {
                            Console.WriteLine($"\nName: {country.Name}; Code: {country.Code};" +
                                           $" Capital: {country.Capital}; Area: {country.Area};" +
                                           $" Region: {country.Region}; People count: {country.PeopleCount}.");

                            Console.WriteLine("Save country info to the database? (Y/N)");
                            var isSaveAction = Console.ReadKey();

                            if (isSaveAction.Key == ConsoleKey.Y)
                            {
                                countryApiController.CreateCountry(nameCountry);
                                Console.WriteLine();
                            }
                            else if (isSaveAction.Key == ConsoleKey.N)
                            {
                                Console.WriteLine();
                                break;
                            }
                            else
                            {
                                Console.WriteLine("You are entered error key!");
                            }
                        }
                        Console.WriteLine();
                        break;

                    case ConsoleKey.O:
                        int i = 1;
                        Console.WriteLine("Countries:");
                        foreach (var c in countryApiController.GetAllCountries())
                        {
                            Console.WriteLine($"{i++}) Name: {c.Name}; Code: {c.Code}; Capital: {c.City?.Name}; " +
                                $"Area: {c.Area}; Population: {c.PeopleCount}; Region: {c.Region?.Name}");
                        }
                        Console.WriteLine();
                        break;

                    case ConsoleKey.E:
                        Environment.Exit(0);
                        break;

                    default:
                        Console.WriteLine("You are entered error key!\n");
                        break;
                }
            }
        }
    }
}
