using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Text;
using VipServices2020.Domain.Model;

namespace VipServices2020.EF
{
    public class VipServicesContext : DbContext
    {

        private string connectionString;

        public VipServicesContext()
        {
        }

        public VipServicesContext(string db = "Production") : base()
        {
            SetConnectionString(db);
        }

        private void SetConnectionString(string db = "Production")
        {
            var builder = new ConfigurationBuilder();
            builder.AddJsonFile("appsettings.json", optional: false);

            var configuration = builder.Build();
            switch (db)
            {
                case "Production":
                    connectionString = configuration.GetConnectionString("ProdSQLconnection").ToString();
                    break;
                case "Test":
                    connectionString = configuration.GetConnectionString("TestSQLconnection").ToString();
                    break;
            }
        }

        public DbSet<Category> Categories { get; set; }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Address> Addresses { get; set; }
        public DbSet<Limousine> Limousines { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Customer>().HasOne(c => c.Category);
            modelBuilder.Entity<Customer>().HasOne(c => c.Address);
            modelBuilder.Entity<Customer>().HasKey(c => c.CustomerNumber);
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (connectionString == null)
            {
                SetConnectionString();
            }
            optionsBuilder.UseSqlServer(connectionString);
        }
    }
}

