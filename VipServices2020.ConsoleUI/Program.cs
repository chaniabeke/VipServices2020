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

            //Address address = new Address("Bldfostreet", "5g", "fdv");
            //Category category = new Category("category");
            //Customer customer = new Customer(5, "Han", "", address, category);
            //Location location = new Location("gent");
            //Limousine limousine = new Limousine("bluu", "df", "sddd", 555, 555, 45, 0);
            //vipServicesManger.AddWeddingReservation(customer, address, location, location, new DateTime(2019, 05, 22), new TimeSpan(05, 00, 00), new TimeSpan(06, 00, 00), limousine);
            Console.WriteLine("Einde Databank");
        }
    }
}
