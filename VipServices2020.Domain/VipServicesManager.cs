using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using VipServices2020.Domain.Model;

namespace VipServices2020.Domain {
    public class VipServicesManager {
        private IUnitOfWork uow;

        public VipServicesManager(IUnitOfWork uow) {
            this.uow = uow;
        }

        public void AddCategory(string categoryName) {
            uow.Categories.AddCategory(new Category(categoryName));
            uow.Complete();
        }
        //public Category FindCategory(string categoryName)
        //{
        //    return uow.Categories.SelectCategory(categoryName);
        //}
        public void AddLocation(string locationName)
        {
            uow.Locations.AddLocation(new Location(locationName));
            uow.Complete();
        }
        public void AddCustomer(int customerNumber, string name, Category category, string BtwNumber, Address address)
        {
            uow.Customers.AddCustomer(new Customer(customerNumber, name, BtwNumber, address, category));
            uow.Complete();
        }
        public List<Customer> GetAllCustomers()
        {
            return uow.Customers.FindAllCustomers().ToList();
        }
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
        public void AddWeddingReservation(Customer customer, Address limousineExpectedAddress, Location startLocation, Location arrivalLocation,
            DateTime reservationDate, TimeSpan startTime, TimeSpan endTime, Limousine limousine)
        {
            Price price = new Price();
            uow.Reservations.AddReservation(new Reservation(customer, DateTime.Now, limousineExpectedAddress, startLocation, arrivalLocation,
                ArrangementType.Wedding, reservationDate, startTime, endTime, limousine, price));
            uow.Complete();
        }
    }
}
