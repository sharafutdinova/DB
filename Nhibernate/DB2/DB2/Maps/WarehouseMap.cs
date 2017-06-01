using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DB2.Maps
{
    using FluentNHibernate.Mapping;
    using Models;

    public class WarehouseMap : BaseMap<Warehouse>
    {
        protected WarehouseMap() : base("warehouse", "id")
        {
            Map(x => x.Address, "address");
            Map(x => x.City, "city");
        }
    }
}