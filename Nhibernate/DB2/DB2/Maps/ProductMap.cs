using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DB2.Maps
{
    using FluentNHibernate.Mapping;
    using Models;

    public class ProductMap : BaseMap<Product>
    {
        protected ProductMap() : base("product", "id")
        {
            References(x => x.ProductInf, "id_inf");
            Map(x => x.Price, "price");
            Map(x => x.Title, "title");
            Map(x => x.Description, "description");
        }
    }    
}