using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DB2.Maps
{
    using FluentNHibernate.Mapping;
    using Models;

    public class ProductInfMap : BaseMap<ProductInf>
    {
        protected ProductInfMap() : base("product_inf", "id")
        {
            Map(x => x.Gender, "gender");
            Map(x => x.TypeProd, "type_prod");
        }
    }
}