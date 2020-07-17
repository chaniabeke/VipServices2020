using System;
using System.Collections.Generic;
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
    }
}
