using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DB2.Repositories.Product
{
    using Models;

    public interface IProductRepository : IRepository<Product>
    {
        List<Product> GetByType(string type);
        List<Product> GetByGender(string gender);
        List<Product> GetByPrice(int max, int min = 0);
        List<Product> GetByTypeAndGender(string gender, string type);
    }
}