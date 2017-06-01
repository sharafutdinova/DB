using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DB2.Repositories
{
    using Models;
    public interface IRepository<TModel>
    {
        List<TModel> GetAll();
        TModel Get(int id);
        TModel Insert(TModel model);
        TModel Update(int id, TModel model);
        bool Delete(int id);
    }
}