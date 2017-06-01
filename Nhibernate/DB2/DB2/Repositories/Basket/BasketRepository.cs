using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using NHibernate.Linq;
using NHibernate.Transaction;

namespace DB2.Repositories.Basket
{
    using Models;
    using DB2.Middleware;
    using Npgsql;
    using NHibernate;
    using System.Reflection;

    public class BasketRepository : IBasketRepository
    {
        ISession session;
        public BasketRepository(ISession session)
        {
            this.session = session;
        }

        public List<Basket> GetAll()
        {
            return session.Query<Basket>().ToList();
        }

        public Basket Get(int id)
        {
           return session.Query<Basket>().Where(x => x.Id == id).SingleOrDefault();
        }

        public Basket Insert(Basket model)
        {
            using (NHibernate.ITransaction transaction = session.BeginTransaction())
            {
                model.Id = 0;
                session.Save(model);
                transaction.Commit();
            }
            return model;
        }

        public Basket Update(int id, Basket model)
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
                Basket basket = session.Query<Basket>().Where(x => x.Id == id).SingleOrDefault();
                session.Delete(basket);
                transaction.Commit();
                return true;
            }
        }

        public List<Basket> GetByProcessing(bool processing)
        {
            var basket = new List<Basket>();
                var orderIds = session.Query<Order>().Where(x => x.Processing == processing).Select(x => x.Id);
            foreach (var orderId in orderIds)
            {
                var baskets = session.Query<Basket>().Where(x => Convert.ToInt32(x.Order) == orderId).ToList();
                basket = basket.Concat(baskets).ToList();
            }
            return basket;
        }

        public List<Basket> GetByCustomerId(int custId)
        {
            return session.Query<Basket>().Where(x => Convert.ToInt32(x.Cust) == custId).ToList();
        }
    }
}