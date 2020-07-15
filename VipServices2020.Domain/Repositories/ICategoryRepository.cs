using System;
using System.Collections.Generic;
using System.Text;
using VipServices2020.Domain.Interfaces;

namespace VipServices2020.Domain.Repositories {
    public interface ICategoryRepository {
        void AddCategory(ICategory category);
    }
}
