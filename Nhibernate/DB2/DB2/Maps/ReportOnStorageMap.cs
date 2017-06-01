using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DB2.Maps
{
    using FluentNHibernate.Mapping;
    using Models;

    public class ReportOnStorageMap : BaseMap<ReportOnStorage>
    {
        protected ReportOnStorageMap() : base("report_on_storage", "id")
        {
            References(x => x.Prod, "prod_id");
            References(x => x.Warehouse, "id_warehouse");
            Map(x => x.Count, "count");
        }
    }
}