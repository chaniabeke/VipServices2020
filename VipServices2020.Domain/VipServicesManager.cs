using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using VipServices2020.Domain.Model;

namespace VipServices2020.Domain
{
    public class VipServicesManager
    {
        private IUnitOfWork uow;

        public VipServicesManager(IUnitOfWork uow)
        {
            this.uow = uow;
        }


        public void AddCategory(string categoryName)
        {
            uow.Categories.AddCategory(new Category(categoryName));
            uow.Complete();
        }

        public void AddLocation(string locationName)
        {
            uow.Locations.AddLocation(new Location(locationName));
            uow.Complete();
        }
        public List<Location> GetAllLocations()
        {
            return uow.Locations.FindAll().ToList();
        }
        public void AddCustomer(string name, Category category, string BtwNumber, Address address)
        {
            uow.Customers.AddCustomer(new Customer(name, BtwNumber, address, category));
            uow.Complete();
        }
        public List<Customer> GetAllCustomers()
        {
            return uow.Customers.FindAll().ToList();
        }
        //public void ShowCustomersDetails()
        //{
        //    uow.Customers.ShowCustomerDetails();
        //}
         public void AddAddress(string streetName, string streetNumber, string town)
        {
            uow.Addresses.AddAddress(new Address(streetName, streetNumber, town));
            uow.Complete();
        }

        public void AddLimousine(string brand, string model, string color, int firstHourPrice, int nightLifePrice, int weddingPrice, int welnessPrice)
        {
            uow.Limousines.AddLimousine(new Limousine(brand, model, color, firstHourPrice, nightLifePrice, weddingPrice, welnessPrice));
            uow.Complete();
        }
        public List<Limousine> GetAllLimousines()
        {
            return uow.Limousines.FindAll().ToList();
        }
        public void AddWelnessReservation(Customer customer, Address limousineExpectedAddress, Location startLocation, Location arrivalLocation,
             DateTime startTime, DateTime endTime, Limousine limousine)
        {
            //Domeinregels
            //Een reservering begint altijd exact op het uur en eindigt altijd exact op het uur
            //Tussen twee reserveringen van eenzelfde limousine moet minstens 6 uur verschil zijn
            //Wellness is altijd 10 uur
            //Wellness is voor 10 uur met start vanaf 7u00 tot 12u00
            if (startTime < DateTime.Now) throw new DomainException("Een reservatie mag niet in het verleden zijn.");
            if (startTime.Hour < 7) if (startTime.Hour > 12) throw new DomainException("Een Welness reservatie moet starten tussen 07u00 en 12u00.");
            TimeSpan totalHours = endTime - startTime;
            if (totalHours.Hours != 10) throw new DomainException("Een Welness reservatie moet altijd 10 uur zijn.");

            //Pricecalculator
            Price price = PriceCalculator.WelnessCalculator(limousine, totalHours, startTime, endTime);

            uow.Reservations.AddReservation(new Reservation(customer, DateTime.Now, limousineExpectedAddress, startLocation, arrivalLocation,
                ArrangementType.Wellness, startTime, endTime, totalHours, limousine, price));
            uow.Complete();
        }
        public void AddNightLifeReservation(Customer customer, Address limousineExpectedAddress, Location startLocation, Location arrivalLocation,
             DateTime startTime, DateTime endTime, Limousine limousine)
        {
            //Domeinregels
            //Geen enkel arrangement mag een duur van 11 uur overschrijden
            //Een reservering begint altijd exact op het uur en eindigt altijd exact op het uur
            //Tussen twee reserveringen van eenzelfde limousine moet minstens 6 uur verschil zijn
            //Wedding en NightLife arrangementen zijn goed voor een huurperiode van 7 uur
            //voor NightLife is dat met een start vanaf 20u00 tot en met middernacht(24u00 ofte 00u00).
            //NightLife arrangementen kunnen ook geboekt worden met overuren met een maximum van 4 overuren
            if (startTime < DateTime.Now) throw new DomainException("Een reservatie mag niet in het verleden zijn.");
            if (startTime.Hour < 20) throw new DomainException("Een NightLife reservatie moet starten tussen 20u00 en 24u00.");
            TimeSpan totalHours = endTime - startTime;
            if (totalHours.Hours > 11) throw new DomainException("Een reservatie mag niet langer zijn dan 11uur.");
            if (totalHours.Hours < 7) throw new DomainException("Een NigtLife reservatie moet minstens 7uur zijn.");

            //Pricecalculator
            Price price = PriceCalculator.NightLifeCalculator(limousine, totalHours, startTime, endTime);

            uow.Reservations.AddReservation(new Reservation(customer, DateTime.Now, limousineExpectedAddress, startLocation, arrivalLocation,
                ArrangementType.NightLife, startTime, endTime, totalHours, limousine, price));
            uow.Complete();
        }
        public void AddWeddingReservation(Customer customer, Address limousineExpectedAddress, Location startLocation, Location arrivalLocation,
             DateTime startTime, DateTime endTime, Limousine limousine)
        {
            //Domeinregels
            //Geen enkel arrangement mag een duur van 11 uur overschrijden
            //Een reservering begint altijd exact op het uur en eindigt altijd exact op het uur
            //Tussen twee reserveringen van eenzelfde limousine moet minstens 6 uur verschil zijn
            //Wedding en NightLife arrangementen zijn goed voor een huurperiode van 7 uur. Voor Wedding isdat met een start vanaf 7u00 tot en met 15u00
            //Wedding en  arrangementen kunnen ook geboekt worden met overuren met een maximum van 4 overure

            if (startTime < DateTime.Now) throw new DomainException("Een reservatie mag niet in het verleden zijn.");
            if (startTime.Hour < 7) if (startTime.Hour > 15) throw new DomainException("Een Wedding reservatie moet starten tussen 07u00 en 15u00.");
            TimeSpan totalHours = endTime - startTime;
            if (totalHours.Hours > 11) throw new DomainException("Een reservatie mag niet langer zijn dan 11uur.");
            if (totalHours.Hours < 7) throw new DomainException("Een Wedding reservatie moet minstens 7uur zijn.");

            //Pricecalculator
            Price price = PriceCalculator.WeddingPriceCalculator(limousine, totalHours, startTime, endTime);

            uow.Reservations.AddReservation(new Reservation(customer, DateTime.Now, limousineExpectedAddress, startLocation, arrivalLocation,
                ArrangementType.Wedding, startTime, endTime, totalHours, limousine, price));
            uow.Complete();

        }
        public void AddAirportReservation(Customer customer, Address limousineExpectedAddress, Location startLocation, Location arrivalLocation,
             DateTime startTime, DateTime endTime, Limousine limousine)
        {
            //Domeinregels
            //Geen enkel arrangement mag een duur van 11 uur overschrijden
            //Een reservering begint altijd exact op het uur en eindigt altijd exact op het uur
            //Tussen twee reserveringen van eenzelfde limousine moet minstens 6 uur verschil zijn

            if (startTime < DateTime.Now) throw new DomainException("Een reservatie mag niet in het verleden zijn.");
            TimeSpan totalHours = endTime - startTime;
            if (totalHours.Hours > 11) throw new DomainException("Een reservatie mag niet langer zijn dan 11uur.");

            //Pricecalculator
            Price price = PriceCalculator.PerHourPriceCalculator(limousine, totalHours, startTime, endTime);

            uow.Reservations.AddReservation(new Reservation(customer, DateTime.Now, limousineExpectedAddress, startLocation, arrivalLocation,
                ArrangementType.Airport, startTime, endTime, totalHours, limousine, price));
            uow.Complete();
        }
        public void AddBusinessReservation(Customer customer, Address limousineExpectedAddress, Location startLocation, Location arrivalLocation,
            DateTime startTime, DateTime endTime, Limousine limousine)
        {
            //Domeinregels
            //Geen enkel arrangement mag een duur van 11 uur overschrijden
            //Een reservering begint altijd exact op het uur en eindigt altijd exact op het uur
            //Tussen twee reserveringen van eenzelfde limousine moet minstens 6 uur verschil zijn

            if (startTime < DateTime.Now) throw new DomainException("Een reservatie mag niet in het verleden zijn.");
            TimeSpan totalHours = endTime - startTime;
            if (totalHours.Hours > 11) throw new DomainException("Een reservatie mag niet langer zijn dan 11uur.");
            //if (totalHours.Minutes > 0) if (totalHours.Seconds > 0) if (totalHours.Milliseconds > 0) throw new DomainException("Een reservatie moet altijd exact op het uur beginnen.");

            //Pricecalculator
            Price price = PriceCalculator.PerHourPriceCalculator(limousine, totalHours, startTime, endTime);

            uow.Reservations.AddReservation(new Reservation(customer, DateTime.Now, limousineExpectedAddress, startLocation, arrivalLocation,
                ArrangementType.Business, startTime, endTime, totalHours, limousine, price));
            uow.Complete();
        }
        public List<Reservation> GetAllReservations()
        {
            return uow.Reservations.FindAll().ToList();
        }
        public List<Reservation> GetAllReservations(int customerId)
        {
            return uow.Reservations.FindAll(new Customer { CustomerNumber = customerId }).ToList();
        }
        public List<Reservation> GetAllReservations(DateTime date)
        {
            return uow.Reservations.FindAll(date).ToList();
        }
        public List<Reservation> GetAllReservations(int customerId, DateTime date)
        {
            return uow.Reservations.FindAll(new Customer { CustomerNumber = customerId }, date).ToList();
        }
    }
}