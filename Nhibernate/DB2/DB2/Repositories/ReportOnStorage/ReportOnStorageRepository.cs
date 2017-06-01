using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using NHibernate.Linq;

namespace DB2.Repositories.ReportOnStorage
{
    using Models;
    using DB2.Middleware;
    using NHibernate;
    using Npgsql;

    public class ReportOnStorageRepository : IReportOnStorageRepository
    {
        ISession session;
        public ReportOnStorageRepository(ISession session)
        {
            this.session = session;
        }
        public List<ReportOnStorage> GetAll()
        {
            return session.Query<ReportOnStorage>().ToList();         
        }

        public ReportOnStorage Get(int id)
        {
            return session.Query<ReportOnStorage>().Where(x => x.Id == id).SingleOrDefault();
        }

        public ReportOnStorage Insert(ReportOnStorage model)
        {
            using (NHibernate.ITransaction transaction = session.BeginTransaction())
            {
                model.Id = 0;
                session.Save(model);
                transaction.Commit();
            }
            return model;
        }

        public ReportOnStorage Update(int id, ReportOnStorage model)
        {
            using (NHibernate.ITransaction transaction = session.BeginTransaction())
            {
                model.Id = id;
                session.Update(model);
                transaction.Commit();
            }
            return model;
        }

        public bool Delete(int id)
        {
            using (NHibernate.ITransaction transaction = session.BeginTransaction())
            {
                ReportOnStorage basket = session.Query<ReportOnStorage>().Where(x => x.Id == id).SingleOrDefault();
                session.Delete(basket);
                transaction.Commit();
                return true;
            }
        }

        public List<ReportOnStorage> GetByCity(string city)
        {
            var reportOnStorage = new List<ReportOnStorage>();
            var warehouseIds = session.Query<Warehouse>().Where(x => x.City == city).Select(x => x.Id);
            foreach (var warehouseId in warehouseIds)
            {
                var reports = session.Query<ReportOnStorage>().Where(x => Convert.ToInt32(x.Warehouse) == warehouseId).ToList();
                reportOnStorage = reportOnStorage.Concat(reports).ToList();
            }
            return reportOnStorage;
        }

        public List<ReportOnStorage> GetByProductId(int productId)
        {
            return session.Query<ReportOnStorage>().Where(x => Convert.ToInt32(x.Prod) == productId).ToList();
        }

        public List<ReportOnStorage> GetByWarehouseId(int warehouseId)
        {
            return session.Query<ReportOnStorage>().Where(x => Convert.ToInt32(x.Warehouse) == warehouseId).ToList();
        }
    }
}