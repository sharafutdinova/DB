using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DB2.Maps
{
    using FluentNHibernate.Mapping;
    using Models;

    public class CustomerMap : BaseMap<Customer>
    {
        protected CustomerMap() : base("customer", "id")
        {
            Map(x => x.Address, "address");
            Map(x => x.City, "city");
            Map(x => x.Name, "name");
        }
    }    
}