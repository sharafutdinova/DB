using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DB2.Repositories.Courier
{
    using Models;

    public interface ICourierRepository : IRepository<Courier>
    {
        List<Courier> GetByProcessing(bool processing);
        List<Courier> GetByNumCourier(int num);
    }
}