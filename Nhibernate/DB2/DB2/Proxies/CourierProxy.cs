using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DB2.Proxies
{
    using Models;
    public class CourierProxy : BaseProxy
    {
        public int NumCourier { get; set; }
        public Order Order { get; set; }
    }
}