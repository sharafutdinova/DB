using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DB2.Models
{
    public class Warehouse
    {
        public int Id { get; set; }
        public string City { get; set; }
        public string Address { get; set; }
    }

    public class Customer
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string City { get; set; }
        public string Address { get; set; }
    }

    public class Order
    {
        public int Id { get; set; }
        public bool Processing { get; set; }
    }

    public class Product
    {
        public int Id { get; set; }
        public int Price { get; set; }
        public int IdInf { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
    }

    public class Basket
    {
        public int Id { get; set; }
        public int IdCust { get; set; }
        public int IdReport { get; set; }
        public int IdOrder { get; set; }
        public int Count { get; set; }
    }

    public class Courier
    {
        public int Id { get; set; }
        public int NumCourier { get; set; }
        public int IdOrder { get; set; }
    }

    public class ProductInf
    {        
        public int Id { get; set; }
        public string TypeProd { get; set; }
        public char Gender { get; set; }
    }

    public class ReportOnStorage
    {
        public int Id { get; set; }
        public int IdProd { get; set; }
        public int IdWarehouse { get; set; }
        public int Count { get; set; }
    }
}