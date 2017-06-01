using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DB2.Models;

namespace DB2.Resources
{
    public class CustomerResource
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string City { get; set; }
        public string Address { get; set; }  
        
        public CustomerResource() { }

        public CustomerResource(Customer model)
        {
            if (model != null)
            {
                Id = model.Id;
                Name = model.Name;
                City = model.City;
                Address = model.Address;
            }
        }

        public Customer ToModel()
        {
            return new Customer
            {
                Id = Id,
                Name = Name,
                City = City,
                Address = Address
            };
        }
    }
}