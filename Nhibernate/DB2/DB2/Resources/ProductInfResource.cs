using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DB2.Models;

namespace DB2.Resources
{
    public class ProductInfResource
    {
        public int Id { get; set; }
        public char Gender { get; set; }
        public string TypeProd { get; set; }

        public ProductInfResource() { }

        public ProductInfResource(ProductInf model)
        {
            if (model != null)
            {
                Id = model.Id;
                Gender = model.Gender;
                TypeProd = model.TypeProd;
            }
        }

        public ProductInf ToModel()
        {
            return new ProductInf
            {
                Id = Id,
                Gender = Gender,
                TypeProd = TypeProd
            };
        }
    }
}