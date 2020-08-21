﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using VipServices2020.Domain.Models;

namespace VipServices2020.Domain
{
    public class VipServicesManager
    {
        private IUnitOfWork uow;

        public VipServicesManager(IUnitOfWork uow)
        {
            this.uow = uow;
        }

        /// <summary>
        /// Deze method voegt een locatie toe aan de DB
        /// </summary>
        public void AddLocation(string locationName)
        {
            uow.Locations.AddLocation(new Location(locationName));
            uow.Complete();
        }
        /// <summary>
        /// Deze method haalt alle locaties uit de DB
        /// </summary>
        public List<Location> GetAllLocations()
        {
            return uow.Locations.FindAll().ToList();
        }

        /// <summary>
        /// Deze method voegt een klant toe aan de DB
        /// </summary>
        public void AddCustomer(string name, string BtwNumber, Address address, CategoryType category)
        {
            uow.Customers.AddCustomer(new Customer(name, BtwNumber, address, category));
            uow.Complete();
        }
        /// <summary>
        /// Deze method haalt alle klanten uit de DB
        /// </summary>
        public List<Customer> GetAllCustomers()
        {
            return uow.Customers.FindAll().ToList();
        }

        /// <summary>
        /// Deze method voegt een adres toe aan de DB
        /// </summary>
        public void AddAddress(string streetName, string streetNumber, string town)
        {
            uow.Addresses.AddAddress(new Address(streetName, streetNumber, town));
            uow.Complete();
        }

        /// <summary>
        /// Deze method voegt een limousine toe aan de DB
        /// </summary>
        public void AddLimousine(string brand, string model, string color, int firstHourPrice, int nightLifePrice, int weddingPrice, int welnessPrice)
        {
            uow.Limousines.AddLimousine(new Limousine(brand, model, color, firstHourPrice, nightLifePrice, weddingPrice, welnessPrice));
            uow.Complete();
        }
        /// <summary>
        /// Deze method haalt alle limousines uit de DB
        /// </summary>
        public List<Limousine> GetAllLimousines()
        {
            return uow.Limousines.FindAll().ToList();
        }

        /// <summary>
        /// Deze method haalt alle beschikbare limousines uit de DB, op basis van het start uur, eind uur en arrangement type
        /// </summary>
        public List<Limousine> GetAllAvailableLimousines(DateTime startTime, DateTime endTime, ArrangementType arrangement)
        {
            List<Limousine> notAvailableLimousines = new List<Limousine>();
            //Haal alle beschikbare limousines uit de DB op basis van het arrangement (waar prijs geen 0 is)
            List<Limousine> availableLimousines = uow.Limousines.FindAllAvailable(arrangement);

            //Kijk voor iedere reservatie of de limo beschikbaar is
            foreach (Reservation r in uow.Reservations.FindAll())
            {
                /*Indien het einduur van de reservatie plus 6 uur groter is dan het gekozen startuur 
                of indien het startuur van de reservatie groter is dan het gekozen einduur */
                if (r.EndTime.AddHours(6) > startTime || r.StartTime > endTime)
                {
                    //Voeg limo toe aan de lijst van niet beschikbare limousines
                    notAvailableLimousines.Add(r.Limousine);
                }
            }
            //Haal de niet beschikbare limousines uit de lijst van beschikbare limousines
            availableLimousines = availableLimousines.Except(notAvailableLimousines).ToList();
            return availableLimousines;
        }

        /// <summary>
        /// Deze method voegt een Discount toe aan de DB
        /// </summary>
        public void AddDiscount(Discount discount)
        {
            uow.Discounts.AddDiscount(discount);
            uow.Complete();
        }
        /// <summary>
        /// Deze method haalt alle Discounts uit de DB
        /// </summary>
        public List<Discount> GetAllDiscounts()
        {
            return uow.Discounts.FindAll().ToList();
        }

        /// <summary>
        /// Deze method voegt een Staffel toe aan de DB
        /// </summary>
        public void AddStaffel(Staffel staffel)
        {
            uow.Staffels.AddStaffel(staffel);
            uow.Complete();
        }
        public double CalculateStaffel(Customer customer, DateTime startTime, CategoryType discountCategory)
        {
            Discount discount = uow.Discounts.Find(discountCategory);
            int reservationCount = uow.Customers.FindReservationCount(customer, startTime);
            double staffelDiscount = 0.0;
            if (discountCategory != CategoryType.geen)
            {
                int smallestStaffelCount = uow.Staffels.FindSmallestReservationCount(discount).NumberOfBookedReservations;
                if (reservationCount == smallestStaffelCount || reservationCount > smallestStaffelCount) 
                {
                    if(reservationCount > smallestStaffelCount)
                    {
                        staffelDiscount = uow.Staffels.FindAll(discount)
                        .Where(s => reservationCount > s.NumberOfBookedReservations)
                        .FirstOrDefault().DiscountPercentage;
                        return staffelDiscount;
                    }
                    staffelDiscount = uow.Staffels.FindAll(discount)
                        .Where(s => s.NumberOfBookedReservations == reservationCount)
                        .FirstOrDefault().DiscountPercentage;
                    return staffelDiscount;
                }
            }
            return staffelDiscount;
        }

        /// <summary>
        /// Deze method voegt een welness arrangement reservatie toe aan de DB
        /// </summary>
        public void AddWelnessReservation(Customer customer, Address limousineExpectedAddress, Location startLocation, Location arrivalLocation,
             DateTime startTime, DateTime endTime, Limousine limousine, CategoryType discountCategory)
        {
            if (endTime < startTime) throw new DomainException("Een reservatie mag niet eindigen voor het begint.");
            if (startTime.Hour < 7 || startTime.Hour > 12) throw new DomainException("Een Welness reservatie moet starten tussen 07u00 en 12u00.");
            TimeSpan totalHours = endTime - startTime;
            if (totalHours.Hours != 10) throw new DomainException("Een Welness reservatie moet altijd 10 uur zijn.");
            List<Limousine> limousines = GetAllAvailableLimousines(startTime, endTime, ArrangementType.Wellness);
            if (!limousines.Contains(limousine)) throw new DomainException("Limousine is niet beschikbaar.");

            double discountPercentage = CalculateStaffel(customer, startTime, discountCategory);
            Price price = PriceCalculator.WelnessCalculator(limousine, totalHours, startTime, endTime, discountPercentage);

            Reservation reservation = new Reservation(customer, DateTime.Now, limousineExpectedAddress, startLocation, arrivalLocation,
                ArrangementType.Wellness, startTime, endTime, totalHours, limousine, price);
            uow.Reservations.AddReservation(reservation);

            uow.Complete();
        }

        /// <summary>
        /// Deze method voegt een nightlife arrangement reservatie toe aan de DB
        /// </summary>
        public void AddNightLifeReservation(Customer customer, Address limousineExpectedAddress, Location startLocation, Location arrivalLocation,
             DateTime startTime, DateTime endTime, Limousine limousine, CategoryType discountCategory)
        {
            if (startTime.Hour < 20 && startTime.Hour != 0) throw new DomainException("Een NightLife reservatie moet starten tussen 20u00 en 24u00.");
            TimeSpan totalHours = endTime - startTime;
            if (totalHours.Hours > 11) throw new DomainException("Een reservatie mag niet langer zijn dan 11uur.");
            if (totalHours.Hours < 7) throw new DomainException("Een NightLife reservatie moet minstens 7uur zijn.");
            List<Limousine> limousines = GetAllAvailableLimousines(startTime, endTime, ArrangementType.NightLife);
            if (!limousines.Contains(limousine)) throw new DomainException("Limousine is niet beschikbaar.");

            double discountPercentage = CalculateStaffel(customer, startTime, discountCategory);
            Price price = PriceCalculator.NightLifeCalculator(limousine, totalHours, startTime, endTime, discountPercentage);

            Reservation reservation = new Reservation(customer, DateTime.Now, limousineExpectedAddress, startLocation, arrivalLocation,
                ArrangementType.NightLife, startTime, endTime, totalHours, limousine, price);
            uow.Reservations.AddReservation(reservation);
            uow.Complete();
        }

        /// <summary>
        /// Deze method voegt een wedding arrangement reservatie toe aan de DB
        /// </summary>
        public void AddWeddingReservation(Customer customer, Address limousineExpectedAddress, Location startLocation, Location arrivalLocation,
             DateTime startTime, DateTime endTime, Limousine limousine, CategoryType discountCategory)
        {
            if (startTime.Hour < 7 || startTime.Hour > 15) throw new DomainException("Een Wedding reservatie moet starten tussen 07u00 en 15u00.");
            TimeSpan totalHours = endTime - startTime;
            if (totalHours.Hours > 11) throw new DomainException("Een reservatie mag niet langer zijn dan 11uur.");
            if (totalHours.Hours < 7) throw new DomainException("Een Wedding reservatie moet minstens 7uur zijn.");
            List<Limousine> limousines = GetAllAvailableLimousines(startTime, endTime, ArrangementType.Wedding);
            if (!limousines.Contains(limousine)) throw new DomainException("Limousine is niet beschikbaar.");

            double discountPercentage = CalculateStaffel(customer, startTime, discountCategory);
            Price price = PriceCalculator.WeddingPriceCalculator(limousine, totalHours, startTime, endTime, discountPercentage);

            Reservation reservation = new Reservation(customer, DateTime.Now, limousineExpectedAddress, startLocation, arrivalLocation,
                ArrangementType.Wedding, startTime, endTime, totalHours, limousine, price);
            uow.Reservations.AddReservation(reservation);
            uow.Complete();
        }

        /// <summary>
        /// Deze method voegt een airport arrangement reservatie toe aan de DB
        /// </summary>
        public void AddAirportReservation(Customer customer, Address limousineExpectedAddress, Location startLocation, Location arrivalLocation,
             DateTime startTime, DateTime endTime, Limousine limousine, CategoryType discountCategory)
        {
            TimeSpan totalHours = endTime - startTime;
            if (totalHours.Hours > 11) throw new DomainException("Een Airport reservatie mag niet langer zijn dan 11uur.");
            List<Limousine> limousines = GetAllAvailableLimousines(startTime, endTime, ArrangementType.Airport);
            if (!limousines.Contains(limousine)) throw new DomainException("Limousine is niet beschikbaar.");

            double discountPercentage = CalculateStaffel(customer, startTime, discountCategory);
            Price price = PriceCalculator.PerHourPriceCalculator(limousine, totalHours, startTime, endTime, discountPercentage);

            Reservation reservation = new Reservation(customer, DateTime.Now, limousineExpectedAddress, startLocation, arrivalLocation,
                ArrangementType.Airport, startTime, endTime, totalHours, limousine, price);
            uow.Reservations.AddReservation(reservation);
            uow.Complete();
        }

        /// <summary>
        /// Deze method voegt een business arrangement reservatie toe aan de DB
        /// </summary>
        public void AddBusinessReservation(Customer customer, Address limousineExpectedAddress, Location startLocation, Location arrivalLocation,
            DateTime startTime, DateTime endTime, Limousine limousine, CategoryType discountCategory)
        {
            TimeSpan totalHours = endTime - startTime;
            if (totalHours.Hours > 11) throw new DomainException("Een Business reservatie mag niet langer zijn dan 11uur.");
            List<Limousine> limousines = GetAllAvailableLimousines(startTime, endTime, ArrangementType.Business);
            if (!limousines.Contains(limousine)) throw new DomainException("Limousine is niet beschikbaar.");

            double discountPercentage = CalculateStaffel(customer, startTime, discountCategory);
            Price price = PriceCalculator.PerHourPriceCalculator(limousine, totalHours, startTime, endTime, discountPercentage);

            Reservation reservation = new Reservation(customer, DateTime.Now, limousineExpectedAddress, startLocation, arrivalLocation,
                ArrangementType.Business, startTime, endTime, totalHours, limousine, price);
            uow.Reservations.AddReservation(reservation);
            uow.Complete();
        }

        /// <summary>
        /// Deze method haalt 1 reservatie uit de DB op basis van reservatie Id
        /// </summary>
        public Reservation GetReservation(int reservationId)
        {
            return uow.Reservations.Find(reservationId).FirstOrDefault();
        }
        /// <summary>
        /// Deze method haalt alle reservaties uit de DB
        /// </summary>
        public List<Reservation> GetAllReservations()
        {
            return uow.Reservations.FindAll().ToList();
        }

        /// <summary>
        /// Deze method haalt alle reservaties uit de DB op basis van gekozen klantnummer
        /// </summary>
        public List<Reservation> GetAllReservations(int customerId)
        {
            return uow.Reservations.FindAll(new Customer { CustomerNumber = customerId }).ToList();
        }
        /// <summary>
        /// Deze method haalt alle reservaties uit de DB op basis van gekozen datum
        /// </summary>
        public List<Reservation> GetAllReservations(DateTime date)
        {
            return uow.Reservations.FindAll(date).ToList();
        }
        /// <summary>
        /// Deze method haalt alle reservaties uit de DB op basis van gekozen klantnummer en datum
        /// </summary>
        public List<Reservation> GetAllReservations(int customerId, DateTime date)
        {
            return uow.Reservations.FindAll(new Customer { CustomerNumber = customerId }, date).ToList();
        }
    }
}