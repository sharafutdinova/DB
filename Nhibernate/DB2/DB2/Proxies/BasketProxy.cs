using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DB2.Proxies
{
    using DB2.Models;
    public class BasketProxy : BaseProxy
    {
        public Customer Cust { get; set; }
        public ReportOnStorage Report { get; set; }
        public Order Order { get; set; }
        public int Count { get; set; }
    }
}