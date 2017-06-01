using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using NHibernate.Linq;

namespace DB2.Repositories.Warehouse
{
    using Models;
    using DB2.Middleware;
    using NHibernate;
    using Npgsql;

    public class WarehouseRepository : IWarehouseRepository
    {
        ISession session;
        public WarehouseRepository(ISession session)
        {
            this.session = session;
        }
        public List<Warehouse> GetAll()
        {
            return session.Query<Warehouse>().ToList();          
        }

        public Warehouse Get(int id)
        {
            return session.Query<Warehouse>().Where(x => x.Id == id).SingleOrDefault();
        }

        public Warehouse Insert(Warehouse model)
        {
            using (NHibernate.ITransaction transaction = session.BeginTransaction())
            {
                model.Id = 0;
                session.Save(model);
                transaction.Commit();
            }
            return model;
        }

        public Warehouse Update(int id, Warehouse model)
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
                Warehouse basket = session.Query<Warehouse>().Where(x => x.Id == id).SingleOrDefault();
                session.Delete(basket);
                transaction.Commit();
                return true;
            }
        }

        public List<Warehouse> GetByCity(string city)
        {
            return session.Query<Warehouse>().Where(x => x.City == city).ToList();
        }
    }
}