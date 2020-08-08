using System;
using System.Collections.Generic;
using VipServices2020.Domain;
using VipServices2020.Domain.Models;
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
            //namespace modelsss

            //VipServicesReader.InitializeLocations(@"C:\Users\Chania\Desktop\PROJECT\VipServices2020\Resources\locaties.txt", vipServicesManger);
            //VipServicesReader.InitializeCustomers(@"C:\Users\Chania\Desktop\PROJECT\VipServices2020\Resources\klanten.txt", vipServicesManger);
            //VipServicesReader.InitializeLimousine(@"C:\Users\Chania\Desktop\PROJECT\VipServices2020\Resources\vehicles.txt", vipServicesManger);

            Address address = new Address("Bldfostreet", "5g", "fdv");
            Customer customer = new Customer("Dan", "", address, CategoryType.concertpromotor);
            Location location = new Location("gent");
            Limousine limousine = new Limousine("bluu", "df", "sddd", 198, 547, 1477, 1351);
            Staffel staffel = new Staffel(2, 5);
            vipServicesManger.AddNightLifeReservation(customer, address, location, location, new DateTime(2020, 09, 22, 0, 0, 0), new DateTime(2020, 09, 22, 8, 0, 0), limousine, staffel);
            Console.WriteLine("Einde Databank");

            //foreach (Reservation reservation in limousine.Reservations)
            //{
            //    Console.WriteLine(reservation.Id);
            //}
        }
    }
}
