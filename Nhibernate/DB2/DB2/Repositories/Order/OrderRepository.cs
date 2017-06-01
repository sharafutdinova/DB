using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using NHibernate.Linq;

namespace DB2.Repositories.Order
{
    using Models;
    using DB2.Middleware;
    using NHibernate;
    using Npgsql;

    public class OrderRepository : IOrderRepository
    {
        ISession session;
        public OrderRepository(ISession session)
        {
            this.session = session;
        }
        public List<Order> GetAll()
        {
            return session.Query<Order>().ToList();
        }

        public Order Get(int id)
        {
            return session.Query<Order>().Where(x => x.Id == id).SingleOrDefault();
        }

        public Order Insert(Order model)
        {
            using (NHibernate.ITransaction transaction = session.BeginTransaction())
            {
                model.Id = 0;
                session.Save(model);
                transaction.Commit();
            }
            return model;
        }

        public Order Update(int id, Order model)
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
                Order basket = session.Query<Order>().Where(x => x.Id == id).SingleOrDefault();
                session.Delete(basket);
                transaction.Commit();
                return true;
            }
        }

        public List<Order> GetByProcessing(bool processing)
        {
            return session.Query<Order>().Where(x => x.Processing == processing).ToList();
        }
    }
}
