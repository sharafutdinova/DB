using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using NHibernate.Linq;

namespace DB2.Repositories.Customer
{
    using Models;
    using DB2.Middleware;
    using NHibernate;
    using Npgsql;

    public class CustomerRepository : ICustomerRepository
    {
        ISession session;
        public CustomerRepository(ISession session)
        {
            this.session = session;
        }
        public List<Customer> GetAll()
        {
            return session.Query<Customer>().ToList();          
        }

        public Customer Get(int id)
        {
            return session.Query<Customer>().Where(x => x.Id == id).SingleOrDefault();
        }

        public Customer Insert(Customer model)
        {
            using (NHibernate.ITransaction transaction = session.BeginTransaction())
            {
                model.Id = 0;
                session.Save(model);
                transaction.Commit();
            }
            return model;
        }

        public Customer Update(int id, Customer model)
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
                Customer basket = session.Query<Customer>().Where(x => x.Id == id).SingleOrDefault();
                session.Delete(basket);
                transaction.Commit();
                return true;
            }
        }

        public List<Customer> GetByCity(string city)
        {
            return session.Query<Customer>().Where(x => x.City == city).ToList();
        }
    }
}
