using System;
using System.Collections.Generic;
using System.Text;
using VipServices2020.EF;

namespace VipServices2020.Tests.EFLayer
{
    public class VipServicesContextTest : VipServicesContext
    {
        public VipServicesContextTest(bool keepExistingDB = false) : base("Test")
        {
            if (keepExistingDB)
            {
                Database.EnsureCreated();
            }
            else
            {
                Database.EnsureDeleted();
                Database.EnsureCreated();
            }
        }
    }
}

