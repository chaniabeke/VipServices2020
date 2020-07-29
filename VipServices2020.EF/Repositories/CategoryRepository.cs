using System;
using System.Collections.Generic;
using System.Text;
using VipServices2020.Domain.Model;
using VipServices2020.Domain.Repositories;

namespace VipServices2020.EF.Repositories {
    public class CategoryRepository : ICategoryRepository {

        private VipServicesContext context;

        public CategoryRepository(VipServicesContext context) {
            this.context = context;
        }

        public void AddCategory(Category category) {
            context.Categories.Add(category);
        }
        public Category SelectCategory(string categoryName)
        {
            return context.Categories.Find(categoryName);
        }
    }
}
//var justOneBook = unitOfWork.BookRepository.Entities  .First(n => n.ID == 1);