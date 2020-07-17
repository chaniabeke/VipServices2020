using System;
using VipServices2020.Domain;
using VipServices2020.EF;
using VipServices2020.EF.Repositories;
using VipServices2020.EF.Utilities;

namespace VipServices2020.ConsoleUI
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Initialiseer Databank");
            VipServicesManager vipServicesManger = new VipServicesManager(new UnitOfWork(new VipServicesContext("Production")));
            //CategoryRepository categoryRepository = new CategoryRepository(vipServicesContext);
            VipServicesReader.InitializeCategories(@"C:\Users\Chania\Desktop\PROJECT\VipServices2020\Resources\categories.txt", vipServicesManger);
            //VipServicesReader.InitializeCustomers(@"C: \Users\Chania\Desktop\PROJECT\VipServices2020\Resources\klanten.txt");
            //VipServicesReader.InitializeLimousine(@"C: \Users\Chania\Desktop\PROJECT\VipServices2020\Resources\vehicles.txt");
            Console.WriteLine("Einde Databank");
        }
    }
}
