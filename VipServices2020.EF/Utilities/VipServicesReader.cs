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
                string categoryName;
                string line;
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
        public static void InitializeCustomers(string path)
        {

        }
        public static void InitializeLimousine(string path)
        {

        }
    }
}