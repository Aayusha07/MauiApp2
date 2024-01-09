using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace MauiApp2.Data
{
    public class CoffeeService
    {
        
        public static List<Coffee> GetAll()
        {
            string appCoffeeFilePath = Utils.GetAppCoffeeFilePath();
            if (!File.Exists(appCoffeeFilePath))
            {
                return new List<Coffee>();
            }

            var json = File.ReadAllText(appCoffeeFilePath);

            return JsonSerializer.Deserialize<List<Coffee>>(json);
        }

        

        public static List<Coffee>Create( string CoffeeName, float price)
        {
            List<Coffee> coffees = GetAll();
            bool coffeenameExists = coffees.Any(x => x.CoffeeName == CoffeeName);

            if (coffeenameExists)
            {
                throw new Exception("Coffeename already exists.");
            }

            coffees.Add(
                new Coffee
                {
                    CoffeeName = CoffeeName,
                    Price = price
                    

                }
            );
            SaveAll(coffees);
            return coffees;
        }

        private static void SaveAll(List<Coffee> coffees)
        {
            string appDataDirectoryPath = Utils.GetAppDirectoryPath();
            string appCoffeeFilePath = Utils.GetAppCoffeeFilePath();

            if (!Directory.Exists(appDataDirectoryPath))
            {
                Directory.CreateDirectory(appDataDirectoryPath);
            }

            var json = JsonSerializer.Serialize(coffees);
            File.WriteAllText(appCoffeeFilePath, json);
        }

        

        public static List<Coffee> Delete(Guid id)
        {
            List<Coffee> coffees = GetAll();
            Coffee coffee = coffees.FirstOrDefault(x => x.Id == id);

            if (coffee == null)
            {
                throw new Exception("Coffee not found.");
            }


            coffees.Remove(coffee);
            SaveAll(coffees);

            return coffees;
        }

        public static List<Coffee> Update(string coffeename, float price )
        {
            List<Coffee> coffees = GetAll();
            Coffee coffee = coffees.FirstOrDefault(x => x.CoffeeName == coffeename);

            if (coffee == null)
            {
                throw new Exception("Coffee not found.");
            }


            coffee.CoffeeName = coffeename;
            coffee.Price = price;


            SaveAll(coffees);
            return coffees;
        }
    }
}

