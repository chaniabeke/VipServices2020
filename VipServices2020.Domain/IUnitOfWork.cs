using System;
using System.Collections.Generic;
using System.Text;

namespace VipServices2020.Domain {
    public interface IUnitOfWork : IDisposable {
        //ICyclingRepository CyclingTrainings { get; }
        //IRunningRepository RunningTrainings { get; }
        int Complete();
    }
}
