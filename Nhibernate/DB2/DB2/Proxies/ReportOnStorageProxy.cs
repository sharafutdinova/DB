using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DB2.Proxies
{
    using Models;
    public class ReportOnStorageProxy : BaseProxy
    {
        public Product Prod { get; set; }
        public Warehouse Warehouse { get; set; }
        public int Count { get; set; }
    }
}