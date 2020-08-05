using System;
using System.Collections.Generic;
using System.Text;
using VipServices2020.Domain.Model;

namespace VipServices2020.Domain.Repositories
{
    public interface ILimousineRepository
    {
        void AddLimousine(Limousine limousine);
        IEnumerable<Limousine> FindAll();
    }
}
