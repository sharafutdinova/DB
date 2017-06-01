using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DB2.Repositories.Basket
{
    using Models;

    public interface IBasketRepository : IRepository<Basket>
    {
        List<Basket> GetByProcessing(bool processing);
        List<Basket> GetByCustomerId(int custId);
    }
}