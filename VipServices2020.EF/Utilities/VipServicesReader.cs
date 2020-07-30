using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using VipServices2020.Domain;
using VipServices2020.Domain.Model;
using VipServices2020.EF.Repositories;

namespace VipServices2020.EF.Utilities
{
    public static class VipServicesReader
    {
        public static void InitializeCategories(string path, VipServicesManager manager)
        {
            using (StreamReader r = new StreamReader(path))
            {
                string line;
                string categoryName;
                while ((line = r.ReadLine()) != null)
                {
                    string[] ss = line.Split(',').Select(x => x.Trim()).ToArray();
                    categoryName = ss[0];
                    manager.AddCategory(categoryName);
                }
            }
        }
        public static void InitializeLocations(string path, VipServicesManager manager)
        {
            using (StreamReader r = new StreamReader(path))
            {
                string line;
                string locationName;
                while ((line = r.ReadLine()) != null)
                {
                    string[] ss = line.Split(',').Select(x => x.Trim()).ToArray();
                    locationName = ss[0];
                    manager.AddLocation(locationName);
                }
            }
        }
        public static void InitializeCustomers(string path, VipServicesManager manager)
        {
            using (StreamReader r = new StreamReader(path))
            {
                string line;
                int customerNumber; string name; string categoryName;
                string BtwNumber; string addressString;
                string streetName; string streetNumber; string town;
                while ((line = r.ReadLine()) != null)
                {
                    string[] ss = line.Split(',').Select(x => x.Trim()).ToArray();
                    customerNumber = int.Parse(ss[0]);
                    name = ss[1];

                    categoryName = ss[2];
                    //Category category = manager.FindCategory(categoryName);
                    //duplicate categories select id based on name
                    BtwNumber = ss[3];
                    addressString = ss[4];
                    string[] addresItems = addressString.Split(' ').Select(x => x.Trim()).ToArray();
                    streetName = addresItems[0];
                    streetNumber = addresItems[1];
                    town = addresItems[3];
                    Address address = new Address(streetName, streetNumber, town);

                    manager.AddCustomers(customerNumber, name, new Category(categoryName), BtwNumber, address);
                }
            }
        }
        public static void InitializeLimousine(string path, VipServicesManager manager)
        {
            //HashSet<Limousine> limousineSet = new HashSet<Limousine>();
            using (StreamReader r = new StreamReader(path))
            {
                string line;
                while ((line = r.ReadLine()) != null)
                {
                    string brand; string model; string color;
                    int firstHourPrice; int nightLifePrice = 0; int weddingPrice = 0; int welnessPrice = 0;
                    string[] ss = line.Split(';').Select(x => x.Trim()).ToArray();
                    brand = ss[0];
                    model = ss[1];
                    color = ss[2];
                    firstHourPrice = int.Parse(ss[3]);
                    if(ss[4].Length != 0) {
                        nightLifePrice = int.Parse(ss[4]);
                    }
                    if (ss[5].Length != 0)
                    {
                        weddingPrice = int.Parse(ss[5]);
                    }
                    if (ss[6].Length != 0)
                    {
                        welnessPrice = int.Parse(ss[6]);
                    }

                    manager.AddLimousines(brand, model, color, firstHourPrice, nightLifePrice, weddingPrice, welnessPrice);
                }
            }
        }
    }
}