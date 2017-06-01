using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DB2.Maps
{
    using FluentNHibernate.Mapping;
    using Models;
    public abstract class BaseMap<TEntity> : ClassMap<TEntity> where TEntity : Base
    {
        protected BaseMap(string tableName, string idColumnName)
        {
            Table(tableName);
            Id(x => x.Id, idColumnName);
        }
    }
}