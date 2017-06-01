using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DB2.Models;

namespace DB2.Resources
{
    public class CourierResource
    {
        public int Id { get; set; }
        public int IdOrder { get; set; }
        public OrderResource Order { get; set; }
        public int NumCourier { get; set; }

        public CourierResource() { }

        public CourierResource(Courier model)
        {
            if (model != null)
            {
                if (model.Order != null)
                {
                    Order = new OrderResource(model.Order);
                    IdOrder = model.Order.Id;
                }
                Id = model.Id;
                NumCourier = model.NumCourier;
            }
        }

        public Courier ToModel()
        {
            return new Courier
            {
                Id = Id,
                NumCourier = NumCourier,
                Order = new Order
                {
                    Id = IdOrder
                }
            };
        }
    }
}