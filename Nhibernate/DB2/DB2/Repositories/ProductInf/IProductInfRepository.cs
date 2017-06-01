using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DB2.Repositories.ProductInf
{
    using Models;

    public interface IProductInfRepository : IRepository<ProductInf>
    {
        List<ProductInf> GetByType(string type);
        List<ProductInf> GetByGender(char gender);
        List<ProductInf> GetByTypeAndGender(string type, char gender);
    }
}