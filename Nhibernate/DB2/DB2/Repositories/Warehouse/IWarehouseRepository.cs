using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DB2.Repositories.Warehouse
{
    using Models;

    public interface IWarehouseRepository : IRepository<Warehouse>
    {
        List<Warehouse> GetByCity(string city);
    }
}