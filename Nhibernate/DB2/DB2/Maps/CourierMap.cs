using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DB2.Maps
{
    using FluentNHibernate.Mapping;
    using Models;

    public class CourierMap : BaseMap<Courier>
    {
        protected CourierMap() : base("courier", "id")
        {
            References(x => x.Order, "id_ord");
            Map(x => x.NumCourier, "num_courier");
        }
    }
}