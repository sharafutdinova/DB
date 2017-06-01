using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DB2.Maps
{
    using FluentNHibernate.Mapping;
    using Models;

    public class OrderMap : BaseMap<Order>
    {
        protected OrderMap() : base("order_", "id")
        {
            Map(x => x.Processing, "processing");
        }
    }    
}