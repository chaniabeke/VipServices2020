using System;
using System.Collections.Generic;
using VipServices2020.Domain;
using VipServices2020.Domain.Model;
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

            //VipServicesReader.InitializeCategories(@"C:\Users\Chania\Desktop\PROJECT\VipServices2020\Resources\categories.txt", vipServicesManger);
            //VipServicesReader.InitializeLocations(@"C:\Users\Chania\Desktop\PROJECT\VipServices2020\Resources\locaties.txt", vipServicesManger);
            //VipServicesReader.InitializeCustomers(@"C:\Users\Chania\Desktop\PROJECT\VipServices2020\Resources\klanten.txt", vipServicesManger);
            //VipServicesReader.InitializeLimousine(@"C:\Users\Chania\Desktop\PROJECT\VipServices2020\Resources\vehicles.txt", vipServicesManger);

            Address address = new Address("Bldfostreet", "5g", "fdv");
            Category category = new Category("category");
            Customer customer = new Customer("Dan", "", address, category);
            Location location = new Location("gent");
            Limousine limousine = new Limousine("bluu", "df", "sddd", 300, 1500, 1400, 1350);
            vipServicesManger.AddWeddingReservation(customer, address, location, location, new DateTime(2020, 09, 22, 10, 0, 0), new DateTime(2020, 09, 22, 20, 0, 0), limousine);
            Console.WriteLine("Einde Databank");
        }
    }
}
