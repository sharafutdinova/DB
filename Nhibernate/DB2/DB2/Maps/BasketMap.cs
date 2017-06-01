using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DB2.Maps
{
    using FluentNHibernate.Mapping;
    using Models;

    public class BasketMap : BaseMap<Basket>
    {
        protected BasketMap() : base("basket", "id")
        {   
            References(x => x.Cust, "id_cust").Fetch.Join();
            References(x => x.Report, "id_report").Fetch.Join();
            References(x => x.Order, "id_ord").Fetch.Join();
            Map(x => x.Count, "count");
        }
    }
}