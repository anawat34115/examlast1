using System;
using System.Collections.Generic;

class City
{
    public int CityId { get; set; }
    public string CityName { get; set; }
    public int InfectionLevel { get; set; }
    public List<int> ConnectedCityIds { get; set; }

    public City()
    {
        ConnectedCityIds = new List<int>();
    }
}

class Program
{
    static List<City> cities = new List<City>();

    static void Main(string[] args)
    {
        Console.Write("Enter the number of cities in the model: ");
        int numberOfCities = int.Parse(Console.ReadLine());

        // Input city details
        for (int i = 0; i < numberOfCities; i++)
        {
            Console.WriteLine($"City {i + 1}:");
            AddCity();
        }

        // Display initial city details
        Console.WriteLine("Initial City Details:");
        DisplayCityDetails();

        // Simulate events during the COVID-19 outbreak
        SimulateOutbreakEvents();

        Console.WriteLine("Program ended.");
    }

    static void AddCity()
    {
        Console.Write("City Name: ");
        string name = Console.ReadLine();

        Console.Write("Number of cities connected: ");
        int numberOfConnections = int.Parse(Console.ReadLine());

        List<int> connectedCityIds = new List<int>();

        for (int i = 0; i < numberOfConnections; i++)
        {
            Console.Write($"Connected City {i + 1} ID: ");
            int connectedCityId = int.Parse(Console.ReadLine());

            if (connectedCityId == cities.Count || connectedCityIds.Contains(connectedCityId))
            {
                Console.WriteLine("Invalid ID");
                i--;
                continue;
            }

            connectedCityIds.Add(connectedCityId);
        }

        cities.Add(new City { CityId = cities.Count, CityName = name, InfectionLevel = 0, ConnectedCityIds = connectedCityIds });
    }

    static void DisplayCityDetails()
    {
        foreach (City city in cities)
        {
            Console.WriteLine($"{city.CityId} {city.CityName} {city.InfectionLevel}");
        }
        Console.WriteLine();
    }

    static void SimulateOutbreakEvents()
    {
        while (true)
        {
            Console.Write("Enter an event (Outbreak, Vaccinate, Lock down, Spread, or Exit): ");
            string input = Console.ReadLine();

            if (input == "Outbreak" || input == "Vaccinate" || input == "Lock down")
            {
                Console.Write("Enter the City ID where the event occurred: ");
                int cityId = int.Parse(Console.ReadLine());

                if (cityId < 0 || cityId >= cities.Count)
                {
                    Console.WriteLine("Invalid City ID");
                    continue;
                }

                PerformEvent(input, cityId);
            }
            else if (input == "Spread")
            {
                SpreadInfection();
            }
            else if (input == "Exit")
            {
                break;
            }
            else
            {
                Console.WriteLine("Invalid");
            }
        }
    }

    static void PerformEvent(string eventType, int cityId)
    {
        switch (eventType)
        {
            case "Outbreak":
                cities[cityId].InfectionLevel += 2;
                if (cities[cityId].InfectionLevel > 3)
                {
                cities[cityId].InfectionLevel = 3;
                }
                SpreadInfectionFromCity(cityId);
                break;
            case "Vaccinate":
                cities[cityId].InfectionLevel = 0;
                SpreadInfectionFromCity(cityId);
                break;
            case "Lockdown":
                cities[cityId].InfectionLevel = 0;
                break;
            default:
                Console.WriteLine("Invalid event type.");
                return;
        }
        Console.WriteLine("Updated City Details:");
        DisplayCityDetails();
    }

    static void SpreadInfection()
    {
        bool infectionSpread = false;

        foreach (City city in cities)
        {
            foreach (int connectedCityId in city.ConnectedCityIds)
            {
                if (city.InfectionLevel > cities[connectedCityId].InfectionLevel)
                {
                    cities[connectedCityId].InfectionLevel++;
                    infectionSpread = true;
                }
            }
        }

        if (infectionSpread)
        {
            Console.WriteLine("Infection spread to connected cities.");
            Console.WriteLine("Updated City Details:");
            DisplayCityDetails();
        }
        else
        {
            Console.WriteLine("No infection spread to connected cities.");
        }
    }

    static void SpreadInfectionFromCity(int cityId)
    {
        bool infectionSpread = false;

        City city = cities[cityId];

        foreach (int connectedCityId in city.ConnectedCityIds)
        {
            if (city.InfectionLevel > cities[connectedCityId].InfectionLevel)
            {
                cities[connectedCityId].InfectionLevel++;
                infectionSpread = true;
            }
        }

        if (infectionSpread)
        {
            Console.WriteLine("Infection spread to connected cities.");
            Console.WriteLine("Updated City Details:");
            DisplayCityDetails();
        }
        else
        {
            Console.WriteLine("No infection spread to connected cities.");
        }
    }
}
