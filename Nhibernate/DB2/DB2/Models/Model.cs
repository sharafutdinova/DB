using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DB2.Models
{
    public abstract class Base
    {
        public virtual int Id { get; set; }
    }

    public class Warehouse : Base
    {
        public virtual string City { get; set; }
        public virtual string Address { get; set; }
    }

    public class Customer : Base 
    {
        public virtual string Name { get; set; }
        public virtual string City { get; set; }
        public virtual string Address { get; set; }
    }

    public class Order : Base
    {
        public virtual bool Processing { get; set; }
    }

    public class Product : Base
    {
        public virtual int Price { get; set; }
        public virtual ProductInf ProductInf { get; set; }
        public virtual string Title { get; set; }
        public virtual string Description { get; set; }
    }

    public class Basket : Base
    {
        public virtual Customer Cust { get; set; }
        public virtual ReportOnStorage Report { get; set; }
        public virtual Order Order { get; set; }
        public virtual int Count { get; set; }
    }

    public class Courier : Base
    {
        public virtual int NumCourier { get; set; }
        public virtual Order Order { get; set; }
    }

    public class ProductInf : Base
    {
        public virtual string TypeProd { get; set; }
        public virtual char Gender { get; set; }
    }

    public class ReportOnStorage :Base
    {
        public virtual Product Prod { get; set; }
        public virtual Warehouse Warehouse { get; set; }
        public virtual int Count { get; set; }
    }
}