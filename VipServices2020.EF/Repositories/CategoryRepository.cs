using System;
using System.Collections.Generic;
using System.Text;
using VipServices2020.Domain.Model;
using VipServices2020.Domain.Repositories;

namespace VipServices2020.EF.Repositories {
    public class CategoryRepository : ICategoryRepository {

        private VipServicesContext _context;

        public CategoryRepository(VipServicesContext context) {
            this._context = context;
        }

        public void AddCategory(Category category) {
            _context.Categories.Add(category);
        }
    }
}
