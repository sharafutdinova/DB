using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DB2.Models;

namespace DB2.Resources
{
    public class ReportOnStorageResource
    {
        public int Id { get; set; }
        public int IdProd { get; set; }
        public ProductResource Product { get; set; }
        public int IdWarehouse { get; set; }
        public WarehouseResource Warehouse { get; set; }
        public int Count { get; set; }

        public ReportOnStorageResource() { }

        public ReportOnStorageResource(ReportOnStorage model)
        {
            if (model != null)
            {
                if (model.Prod != null)
                {
                    Product = new ProductResource(model.Prod);
                    IdProd = model.Prod.Id;
                }
                if (model.Warehouse != null)
                {
                    Warehouse = new WarehouseResource(model.Warehouse);
                    IdWarehouse = model.Warehouse.Id;
                }
                this.Count = model.Count;
                Id = model.Id;
            }
        }
        //expand

        public ReportOnStorage ToModel()
        {
            return new ReportOnStorage
            {
                Id = Id,
                Count = Count,
                Prod = new Product
                {
                    Id = IdProd
                },
                Warehouse = new Warehouse
                {
                    Id = IdWarehouse
                }
            };
        }
    }
}