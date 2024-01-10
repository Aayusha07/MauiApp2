using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;


namespace MauiApp2.Data
{
    public class OrderServices
    {
        public static List<Order> GetAll()
        {
            string appOrderFilePath = Utils.GetAppOrderFilePath();
            if (!File.Exists(appOrderFilePath))
            {
                return new List<Order>();
            }

            var json = File.ReadAllText(appOrderFilePath);

            return JsonSerializer.Deserialize<List<Order>>(json);
        }

        public static List<Coffee> GetCoffee(Order order)
        {
            return order.coffee;
           
        }



        public static List<Order> Create(string OrderDescription, List<Coffee> coffees, List<AddIns> addIns) 
        {
            List<Order> orders = GetAll();
            

           
            orders.Add(
                new Order
                {
                    Description = OrderDescription,
                    coffee = coffees,
                    addIns = addIns

                }
            );
            SaveAll(orders);
            return orders;
        }

        private static void SaveAll(List<Order> orders)
        {
            string appDataDirectoryPath = Utils.GetAppDirectoryPath();
            string appOrderFilePath = Utils.GetAppOrderFilePath();

            if (!Directory.Exists(appDataDirectoryPath))
            {
                Directory.CreateDirectory(appDataDirectoryPath);
            }

            var json = JsonSerializer.Serialize(orders);
            File.WriteAllText(appOrderFilePath, json);
        }



        public static List<Order> Delete(Guid id)
        {
            List<Order> orders = GetAll();
            Order order = orders.FirstOrDefault(x => x.Id == id);

            if (order == null)
            {
                throw new Exception("Order not found.");
            }


            orders.Remove(order);
            SaveAll(orders);

            return orders;
        }

        public static List<Order> Update(string orderdescription, float price)
        {
            List<Order> orders = GetAll();
            Order order = orders.FirstOrDefault(x => x.Description == orderdescription);

            if (orders == null)
            {
                throw new Exception("Order not found.");
            }


            order.Description = orderdescription;
            order.Price = price;


            SaveAll(orders);
            return orders;
        }

        public static Order IncreaseQuantity(Order order, Coffee coffee)
        {
            List<Order> orders = GetAll();
            int orderIndex = orders.FindIndex(x => x.Id == order.Id);

            int coffeeIndex = orders[orderIndex].coffee.FindIndex(x => x.Id == coffee.Id);
            orders[orderIndex].coffee[coffeeIndex].Quantity += 1;

            
            SaveAll(orders);

            return orders[orderIndex];
        }

        public static List<Coffee> DecreaseQuantity(Order order, Coffee coffee)
        {
            List<Order> orders = GetAll();
            int orderIndex = orders.FindIndex(x => x.Id == order.Id);

            int coffeeIndex = orders[orderIndex].coffee.FindIndex(x => x.Id == coffee.Id);
            if (orders[orderIndex].coffee[coffeeIndex].Quantity > 0)
            {
                orders[orderIndex].coffee[coffeeIndex].Quantity -= 1;

            }
            SaveAll(orders);

            return orders[orderIndex].coffee;
        }

        public static List<AddIns> IncreaseAddInQuantity(Order order, AddIns addIns)
        {
            List<Order> orders = GetAll();
            int orderIndex = orders.FindIndex(x => x.Id == order.Id);

            int addinIndex = orders[orderIndex].addIns.FindIndex(x => x.Id == addIns.Id);
            orders[orderIndex].addIns[addinIndex].Quantity += 1;


            SaveAll(orders);

            return orders[orderIndex].addIns;
        }

        public static List<AddIns> DecreaseAddInQuantity(Order order, AddIns addIns )
        {
            List<Order> orders = GetAll();
            int orderIndex = orders.FindIndex(x => x.Id == order.Id);

            int addinIndex = orders[orderIndex].addIns.FindIndex(x => x.Id == addIns.Id);
            if (orders[orderIndex].addIns[addinIndex].Quantity > 0)
            {
                orders[orderIndex].addIns[addinIndex].Quantity -= 1;

            }
            SaveAll(orders);

            return orders[orderIndex].addIns;
        }
    }
}

