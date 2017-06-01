using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DB2.Proxies
{
    using Models;
    public class ProductProxy : BaseProxy
    {
        public int Price { get; set; }
        public ProductInf ProductInf { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
    }
}