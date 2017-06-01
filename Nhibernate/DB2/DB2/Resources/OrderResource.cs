using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DB2.Models;

namespace DB2.Resources
{
    public class OrderResource
    {
        public int Id { get; set; }
        public bool Processing { get; set; }  
        
        public OrderResource() { }

        public OrderResource(Order model)
        {
            if (model != null)
            {
                Id = model.Id;
                Processing = model.Processing;
            }
        }

        public Order ToModel()
        {
            return new Order
            {
                Id = Id,
                Processing = Processing
            };
        }
    }
}