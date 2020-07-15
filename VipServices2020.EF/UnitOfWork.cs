using System;
using System.Collections.Generic;
using System.Text;
using VipServices2020.Domain;

namespace VipServices2020.EF {
    public class UnitOfWork : IUnitOfWork {
        VipServicesContext _context;

        public UnitOfWork(VipServicesContext context) {
            this._context = context;
            //CyclingTrainings = new CyclingRepository(context);
            //RunningTrainings = new RunningRepository(context);
        }

        //public ICyclingRepository CyclingTrainings { get; private set; }

        //public IRunningRepository RunningTrainings { get; private set; }

        public int Complete() {
            try {
                return _context.SaveChanges();
            } catch (Exception ex)
              //TODO : SqlExceptions
              {
                throw;
            }
        }

        public void Dispose() {
            _context.Dispose(); ;
        }
    }
}


