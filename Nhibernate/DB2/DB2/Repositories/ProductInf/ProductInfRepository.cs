using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using NHibernate.Linq;

namespace DB2.Repositories.ProductInf
{
    using Models;
    using DB2.Middleware;
    using NHibernate;
    using Npgsql;

    public class ProductInfRepository : IProductInfRepository
    {
        ISession session;
        public ProductInfRepository(ISession session)
        {
            this.session = session;
        }
        public List<ProductInf> GetAll()
        {
            return session.Query<ProductInf>().ToList();
        }

        public ProductInf Get(int id)
        {
            return session.Query<ProductInf>().Where(x => x.Id == id).SingleOrDefault();
        }

        public ProductInf Insert(ProductInf model)
        {
            using (NHibernate.ITransaction transaction = session.BeginTransaction())
            {
                model.Id = 0;
                session.Save(model);
                transaction.Commit();
            }
            return model;
        }

        public ProductInf Update(int id, ProductInf model)
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
                ProductInf basket = session.Query<ProductInf>().Where(x => x.Id == id).SingleOrDefault();
                session.Delete(basket);
                transaction.Commit();
                return true;
            }
        }

        public List<ProductInf> GetByType(string type)
        {
            return session.Query<ProductInf>().Where(x => x.TypeProd == type).ToList();
        }

        public List<ProductInf> GetByGender(char gender)
        {
            return session.Query<ProductInf>().Where(x => x.Gender == gender).ToList();
        }

        public List<ProductInf> GetByTypeAndGender(string type, char gender)
        {
            return session.Query<ProductInf>().Where(x => x.TypeProd == type).Where(x => x.Gender == gender).ToList();
        }
    }
}