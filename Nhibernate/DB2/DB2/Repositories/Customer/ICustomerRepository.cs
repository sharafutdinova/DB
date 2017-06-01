using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DB2.Repositories.Customer
{
    using Models;

    public interface ICustomerRepository : IRepository<Customer>
    {
        List<Customer> GetByCity(string city);
    }
}