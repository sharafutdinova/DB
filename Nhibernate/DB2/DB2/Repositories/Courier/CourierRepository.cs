using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using NHibernate.Linq;

namespace DB2.Repositories.Courier
{
    using Models;
    using DB2.Middleware;
    using NHibernate;
    using Npgsql;

    public class CourierRepository : ICourierRepository
    {
        ISession session;
        public CourierRepository(ISession session)
        {
            this.session = session;
        }

        public List<Courier> GetAll()
        {
            return session.Query<Courier>().ToList();
        }

        public Courier Get(int id)
        {
            return session.Query<Courier>().Where(x => x.Id == id).SingleOrDefault();
        }

        public Courier Insert(Courier model)
        {
            using (NHibernate.ITransaction transaction = session.BeginTransaction())
            {
                model.Id = 0;
                session.Save(model);
                transaction.Commit();
            }
            return model;
        }

        public Courier Update(int id, Courier model)
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
                Courier basket = session.Query<Courier>().Where(x => x.Id == id).SingleOrDefault();
                session.Delete(basket);
                transaction.Commit();
                return true;
            }
        }

        public List<Courier> GetByProcessing(bool processing)
        {
            var courier = new List<Courier>();
                var orderIds = session.Query<Order>().Where(x => x.Processing == processing).Select(x => x.Id);
            foreach (var orderId in orderIds)
            {
                var couriers = session.Query<Courier>().Where(x => Convert.ToInt32(x.Order) == orderId).ToList();
                courier = courier.Concat(couriers).ToList();
            }                
            return courier;
        }

        public List<Courier> GetByNumCourier(int num)
        {
            return session.Query<Courier>().Where(x => x.NumCourier == num).ToList();
            
        }
    }
}