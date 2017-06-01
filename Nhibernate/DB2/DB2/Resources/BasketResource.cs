using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DB2.Models;
using DB2.Controllers;

namespace DB2.Resources
{
    public class BasketResource
    {
        public int Id { get; set; }
        public int IdCust { get; set; }
        public CustomerResource Customer { get; set; }
        public int IdReport { get; set; }
        public ReportOnStorageResource ReportOnStorage { get; set; }
        public int IdOrder { get; set; }
        public OrderResource Order { get; set; }
        public int Count { get; set; }
        
        public BasketResource() { }

        public BasketResource(Basket model)
        {
            if (model != null)
            {
                if (model.Cust != null)
                {
                    Customer = new CustomerResource(model.Cust);
                    IdCust = model.Cust.Id;
                }
                if (model.Order != null)
                {
                    Order = new OrderResource(model.Order);
                    IdOrder = model.Order.Id;
                }
                if (model.Report != null)
                {
                    ReportOnStorage = new ReportOnStorageResource(model.Report);
                    IdReport = model.Report.Id;
                }
                Id = model.Id;
                Count = model.Count;
            }
        }
        
        public Basket ToModel()
        {
            return new Basket
            {
                Id = Id,
                Count = Count,
                Cust = new Customer
                {
                    Id = IdCust
                },
                Order = new Order
                {
                    Id = IdOrder
                },
                Report = new ReportOnStorage
                {
                    Id = IdReport
                }
            };
        }        
    }
}