using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace DeserializeADictionaryObject
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Example1_SerializeObject();
            Example2_DeserializeObject();
            Example3_SomeLinqFunctions();
        }

        public static void Example1_SerializeObject()
        {

            Dictionary<string, string> carManufacturers = new Dictionary<string, string>
            {
                {"Toyota", "Japan"},                {"Ford", "USA"},                {"Volkswagen", "Germany"},
                {"Honda", "Japan"},                 {"BMW", "Germany"},             {"Nissan", "Japan"},
                {"Mercedes-Benz", "Germany"},       {"Hyundai", "South Korea"},     {"Kia", "South Korea"},
                {"Fiat", "Italy"},                  {"Renault", "France"},          {"Peugeot", "France"},
                {"Chevrolet", "USA"},               {"Mazda", "Japan"},             {"Jaguar Land Rover", "UK"},
                {"Citroen", "France"},              {"Audi", "Germany"},            {"Porsche", "Germany"},
                {"Suzuki", "Japan"},                {"Daimler", "Germany"},         {"Opel", "Germany"},
                {"Mini", "UK"},                     {"Bentley", "UK"},              {"Rolls-Royce", "UK"},
                {"Lamborghini", "Italy"},           {"Telsa","USA"}
            };

            // serializeObject takes dictionary converts to string
            string jsonString = JsonConvert.SerializeObject(carManufacturers, Formatting.Indented);

            string fileName = @"C:\YOUTUBE\C#\DeserializeADictionaryObject\video\CarManufacturers.json";

            // test to see if filename is already in the folder, if so, delete it
            if (System.IO.File.Exists(fileName))
            {
                System.IO.File.Delete(fileName);
            }

            if (string.IsNullOrEmpty(jsonString) == false)
            {
                File.WriteAllText(fileName, jsonString);
            }

        }

        public static void Example2_DeserializeObject()
        {
            string fileName = @"C:\YOUTUBE\C#\DeserializeADictionaryObject\video\CarManufacturers.json";

            if (System.IO.File.Exists(fileName) == false)
            {
                Console.WriteLine($"File Not found: {fileName}");
                return;
            }

            string jsonString = File.ReadAllText(fileName);

            if (string.IsNullOrEmpty(jsonString) == true)
            {
                Console.WriteLine("No data is available");
                return;
            }

            // Deserialize the JSON string into a dictionary object
            Dictionary<string, string> carManufacturers =
                                        JsonConvert.DeserializeObject<Dictionary<string, string>>(jsonString);


            // Loop through the dictionary and print out the key-value pairs
            foreach (KeyValuePair<string, string> kvp in carManufacturers)
            {
                Console.WriteLine($"Key: {kvp.Key}, Value: {kvp.Value}");
            }

        }

        public static void Example3_SomeLinqFunctions()
        {

            string fileName = @"C:\YOUTUBE\C#\DeserializeADictionaryObject\video\CarManufacturers.json";

            if (System.IO.File.Exists(fileName) == false)
            {
                Console.WriteLine($"File Not found: {fileName}");
                return;
            }

            string jsonString = File.ReadAllText(fileName);

            if (string.IsNullOrEmpty(jsonString) == true)
            {
                Console.WriteLine("No data is available");
                return;
            }


            // Deserialize the JSON string into a dictionary object
            Dictionary<string, string> carManufacturers =
                                        JsonConvert.DeserializeObject<Dictionary<string, string>>(jsonString);


            var groupedManufacturers = carManufacturers
                                            .GroupBy(x => x.Value)
                                            .Select(g => new { Country = g.Key, Count = g.Count() });

            if (groupedManufacturers != null)
            {
                {
                    Console.WriteLine();
                    Console.WriteLine("Example 3-1--------------------------------------------------------");

                    Console.WriteLine();


                    foreach (var group in groupedManufacturers)
                    {
                        Console.WriteLine($"Country: {group.Country}, Count: {group.Count}");

                    }

                }


                // get a list of Manufacturers from Germany
                var germanManufacturers = carManufacturers
                                                .Where(x => x.Value == "Germany")
                                                .Select(x => x.Key)
                                                .ToList();

                if (germanManufacturers != null)
                {
                    Console.WriteLine();
                    Console.WriteLine("Example 3-2--------------------------------------------------------");
                    Console.WriteLine($"The data type is: {germanManufacturers.GetType().ToString()}");
                    Console.WriteLine();


                    foreach (var manufacturer in germanManufacturers)
                    {
                        Console.WriteLine(manufacturer);
                    }
                }



                // find the first car that has "FORD" in its model name
                KeyValuePair<string, string> carModelContains_OR = carManufacturers
                                                        .Select(kvp => kvp)
                                                        .Where(kvp => kvp.Key.Contains("Ford"))
                                                        .FirstOrDefault();



                if (!carModelContains_OR.Equals(default(KeyValuePair<string, string>)))
                {

                    Console.WriteLine();
                    Console.WriteLine("Example 3-3--------------------------------------------------------");
                    Console.WriteLine();


                    Console.WriteLine($"{carModelContains_OR.Key} {carModelContains_OR.Value}");
                }

            }
        }
    }
}
