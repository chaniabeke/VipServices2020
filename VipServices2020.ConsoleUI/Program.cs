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

            Console.WriteLine("Einde Databank");
        }
    }
}
