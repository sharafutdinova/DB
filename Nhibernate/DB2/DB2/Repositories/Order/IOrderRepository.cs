using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DB2.Repositories.Order
{
    using Models;

    public interface IOrderRepository : IRepository<Order>
    {
        List<Order> GetByProcessing(bool processing);
    }
}