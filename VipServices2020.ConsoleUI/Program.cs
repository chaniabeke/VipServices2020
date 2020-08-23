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

            //VipServicesReader.InitializeLocations(@"C:\Users\Chania\Desktop\PROJECT\VipServices2020\Resources\locaties.txt", vipServicesManger);
            //VipServicesReader.InitializeCustomers(@"C:\Users\Chania\Desktop\PROJECT\VipServices2020\Resources\klanten.txt", vipServicesManger);
            //VipServicesReader.InitializeLimousines(@"C:\Users\Chania\Desktop\PROJECT\VipServices2020\Resources\vehicles.txt", vipServicesManger);

            //CategoryType vip = CategoryType.vip;
            //StaffelDiscount staffel1 = new StaffelDiscount(2, 5, vip);
            //StaffelDiscount staffel2 = new StaffelDiscount(7, 7.5, vip);
            //StaffelDiscount staffel3 = new StaffelDiscount(15, 10, vip);
            //vipServicesManger.AddStaffel(staffel1);
            //vipServicesManger.AddStaffel(staffel2);
            //vipServicesManger.AddStaffel(staffel3);

            //CategoryType planner = CategoryType.huwelijksplanner;
            //StaffelDiscount staffel4 = new StaffelDiscount(5, 7.5, planner);
            //StaffelDiscount staffel5 = new StaffelDiscount(10, 10, planner);
            //StaffelDiscount staffel6 = new StaffelDiscount(15, 12.5, planner);
            //StaffelDiscount staffel7 = new StaffelDiscount(20, 15, planner);
            //StaffelDiscount staffel8 = new StaffelDiscount(25, 25, planner);
            //vipServicesManger.AddStaffel(staffel4);
            //vipServicesManger.AddStaffel(staffel5);
            //vipServicesManger.AddStaffel(staffel6);
            //vipServicesManger.AddStaffel(staffel7);
            //vipServicesManger.AddStaffel(staffel8);

            //LimousineRepository limousineRepo = new LimousineRepository(new VipServicesContext("Production"));

            //Address addressCustomer = new Address("Groenlaan", "17", "Herzele");
            //Address limousineExceptedAddress = new Address("Nieuwstraat", "5B", "Brussel");
            //Customer customer = new Customer("Jan", "", addressCustomer, CategoryType.particulier);
            //Location locationStart = new Location("Gent");
            //Location locationArrival = new Location("Brussel");
            //DateTime startTime = new DateTime(2020, 09, 22, 20, 0, 0);
            //DateTime endTime = new DateTime(2020, 09, 23, 3, 0, 0);

            //Limousine limousineChrysler = limousineRepo.Find(1);

            ////Eerste reservatie limousine beschikbaar
            //vipServicesManger.AddBusinessReservation(customer, limousineExceptedAddress, locationStart, locationArrival,
            //     new DateTime(2020, 12, 22, 8, 0, 0), new DateTime(2020, 12, 22, 10, 0, 0), limousineChrysler);

            //// 6 uur ervoor
            //vipServicesManger.AddBusinessReservation(customer, limousineExceptedAddress, locationStart, locationArrival,
            //     new DateTime(2020, 12, 22, 0, 0, 0), new DateTime(2020, 12, 22, 2, 0, 0), limousineChrysler);

            //// 6 uur erna
            //vipServicesManger.AddBusinessReservation(customer, limousineExceptedAddress, locationStart, locationArrival,
            //     new DateTime(2020, 12, 22, 16, 0, 0), new DateTime(2020, 12, 22, 18, 0, 0), limousineChrysler);

            //List<Limousine> limousines = vipServicesManger.GetAllAvailableLimousines(startTime, endTime, ArrangementType.NightLife);
            //Console.WriteLine(limousines.Count);

            Console.WriteLine("Einde Databank");
        }
    }
}
