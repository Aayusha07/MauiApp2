using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace MauiApp2.Data
{
    public class AddInsService
    {
        public static List<AddIns> GetAll()
        {
            string appAddInsFilePath = Utils.GetAppAddInsFilePath();
            if (!File.Exists(appAddInsFilePath))
            {
                return new List<AddIns>();
            }

            var json = File.ReadAllText(appAddInsFilePath);

            return JsonSerializer.Deserialize<List<AddIns>>(json);
        }



        public static List<AddIns> Create(string AddInsName, float price)
        {
            List<AddIns> addins = GetAll();
            bool AddInsnameExists = addins.Any(x => x.AddInsName == AddInsName);

            if (AddInsnameExists)
            {
                throw new Exception("AddInsname already exists.");
            }

            addins.Add(
                new AddIns
                {
                    AddInsName = AddInsName,
                    Price = price


                }
            );
            SaveAll(addins);
            return addins;
        }

        private static void SaveAll(List<AddIns> addins)
        {
            string appDataDirectoryPath = Utils.GetAppDirectoryPath();
            string appAddInsFilePath = Utils.GetAppAddInsFilePath();

            if (!Directory.Exists(appDataDirectoryPath))
            {
                Directory.CreateDirectory(appDataDirectoryPath);
            }

            var json = JsonSerializer.Serialize(addins);
            File.WriteAllText(appAddInsFilePath, json);
        }



        public static List<AddIns> Delete(Guid id)
        {
            List<AddIns> addIns = GetAll();
            AddIns addIn = addIns.FirstOrDefault(x => x.Id == id);

            if (addIns == null)
            {
                throw new Exception("AddIns not found.");
            }


            addIns.Remove(addIn);
            SaveAll(addIns);

            return addIns;
        }

        public static List<AddIns> Update(string addinname, float price)
        {
            List<AddIns> addIns = GetAll();
            AddIns addIn = addIns.FirstOrDefault(x => x.AddInsName == addinname);

            if (addIn == null)
            {
                throw new Exception("addin not found.");
            }


            addIn.AddInsName = addinname;
            addIn.Price = price;


            SaveAll(addIns);
            return addIns;
        }
    }
}

