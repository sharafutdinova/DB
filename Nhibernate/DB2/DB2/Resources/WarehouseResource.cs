using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DB2.Models;

namespace DB2.Resources
{
    public class WarehouseResource
    {
        public int Id { get; set; }
        public string City { get; set; }
        public string Address { get; set; }      
        
        public WarehouseResource() { }
        
        public WarehouseResource(Warehouse model)
        {
            if (model != null)
            {
                Id = model.Id;
                City = model.City;
                Address = model.Address;
            }
        } 

        public Warehouse ToModel()
        {
            return new Warehouse
            {
                Id = Id,
                City = City,
                Address = Address
            };
        }
    }
}