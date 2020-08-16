using System;
using System.Collections.Generic;
using System.Text;
using VipServices2020.Domain.Models;

namespace VipServices2020.Domain.Repositories
{
    public interface ILimousineRepository
    {
        void AddLimousine(Limousine limousine);
        Limousine Find(int id);
        IEnumerable<Limousine> FindAll();
        List<Limousine> FindAllAvailable(ArrangementType arrangement);
    }
}
