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
            HashSet<Category> categorySet = new HashSet<Category>();
            using (StreamReader r = new StreamReader(path))
            {
                string line;
                string categoryName;
                while ((line = r.ReadLine()) != null)
                {
                    string[] ss = line.Split(',').Select(x => x.Trim()).ToArray();
                    categoryName = ss[0];
                    Category category = new Category(categoryName);
                    categorySet.Add(category);
                    manager.AddCategory(categoryName);
                }
            }
        }
        public static void InitializeCustomers(string path, VipServicesManager manager)
        {
            HashSet<Customer> customerSet = new HashSet<Customer>();
            using (StreamReader r = new StreamReader(path))
            {
                string line;
                int customerNumber; string name; string categoryName;
                string BtwNumber; string address;
                string streetName; string streetNumber; string town;
                while ((line = r.ReadLine()) != null)
                {
                    string[] ss = line.Split(',').Select(x => x.Trim()).ToArray();
                    customerNumber = int.Parse(ss[0]);
                    name = ss[1];

                    categoryName = ss[2];
                    //category zoeken en die toevoegen
                    Category category = ;

                    BtwNumber = ss[3];
                    address = ss[4];

                    string[] aa = address.Split(' ').Select(x => x.Trim()).ToArray();
                    streetName = aa[0];
                    streetNumber = aa[1];
                    town = aa[3];

                    //Category category = new Category(categoryName);
                    //categorySet.Add(category);
                    //manager.AddCategory(categoryName);
                }
            }
        }
        public static void InitializeLimousine(string path)
        {

        }
    }
}