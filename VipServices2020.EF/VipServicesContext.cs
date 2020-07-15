using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Text;

namespace VipServices2020.EF {
    public class VipServicesContext : DbContext {

        string _connectionString;

        public VipServicesContext() {
        }

        public VipServicesContext(string db = "Production") : base() {
            SetConnectionString(db);
        }

        private void SetConnectionString(string db = "Production") {
            var builder = new ConfigurationBuilder();
            builder.AddJsonFile("appsettings.json", optional: false);

            var configuration = builder.Build();
            switch (db) {
                case "Production":
                    _connectionString = configuration.GetConnectionString("ProdSQLconnection").ToString();
                    break;
                case "Test":
                    _connectionString = configuration.GetConnectionString("TestSQLconnection").ToString();
                    break;
            }
        }

        //public DbSet<CyclingSession> CyclingSessions { get; set; }
        //public DbSet<RunningSession> RunningSessions { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) {
            if (_connectionString == null) {
                SetConnectionString();
            }
            optionsBuilder.UseSqlServer(_connectionString);
        }
    }
}

