using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DB2.Models;

namespace DB2.Resources
{
    public class ProductResource
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public int IdProdInf { get; set; }
        public int Price { get; set; }
        public ProductInfResource ProductInf { get; set; }

        public ProductResource() { }

        public ProductResource(Product model)
        {
            if (model != null)
            {
                if (model.ProductInf != null)
                {
                    ProductInf = new ProductInfResource(model.ProductInf);
                    IdProdInf = model.ProductInf.Id;
                }
                Id = model.Id;
                Title = model.Title;
                Description = model.Description;
                Price = model.Price;
            }
        }
        
        public Product ToModel()
        {
            return new Product
            {
                Id = Id,
                Title = Title,
                Description = Description,
                Price = Price,
                ProductInf = new ProductInf
                {
                    Id = IdProdInf
                }
            };
        }
    }
}