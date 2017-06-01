using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DB2.Repositories.ReportOnStorage
{
    using Models;

    public interface IReportOnStorageRepository : IRepository<ReportOnStorage>
    {
        List<ReportOnStorage> GetByCity(string city);
        List<ReportOnStorage> GetByProductId(int productId);
        List<ReportOnStorage> GetByWarehouseId(int warehouseId);
    }
}