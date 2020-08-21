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

            VipServicesReader.InitializeLocations(@"C:\Users\Chania\Desktop\PROJECT\VipServices2020\Resources\locaties.txt", vipServicesManger);
            VipServicesReader.InitializeCustomers(@"C:\Users\Chania\Desktop\PROJECT\VipServices2020\Resources\klanten.txt", vipServicesManger);
            VipServicesReader.InitializeLimousine(@"C:\Users\Chania\Desktop\PROJECT\VipServices2020\Resources\vehicles.txt", vipServicesManger);

            Discount discountVip = new Discount(CategoryType.vip);
            Staffel staffel1 = new Staffel(2, 5, discountVip);
            Staffel staffel2 = new Staffel(7, 7.5, discountVip);
            Staffel staffel3 = new Staffel(15, 10, discountVip);
            vipServicesManger.AddStaffel(staffel1);
            vipServicesManger.AddStaffel(staffel2);
            vipServicesManger.AddStaffel(staffel3);

            Discount discountPlanner = new Discount(CategoryType.huwelijksplanner);
            Staffel staffel4 = new Staffel(5, 7.5, discountPlanner);
            Staffel staffel5 = new Staffel(10, 10, discountPlanner);
            Staffel staffel6 = new Staffel(15, 12.5, discountPlanner);
            Staffel staffel7 = new Staffel(20, 15, discountPlanner);
            Staffel staffel8 = new Staffel(25, 25, discountPlanner);
            vipServicesManger.AddStaffel(staffel4);
            vipServicesManger.AddStaffel(staffel5);
            vipServicesManger.AddStaffel(staffel6);
            vipServicesManger.AddStaffel(staffel7);
            vipServicesManger.AddStaffel(staffel8);

            Discount discountGeen = new Discount(CategoryType.geen);
            vipServicesManger.AddDiscount(discountGeen);

            Console.WriteLine("Einde Databank");
        }
    }
}
