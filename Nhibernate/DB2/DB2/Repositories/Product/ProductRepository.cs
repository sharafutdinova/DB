using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using NHibernate.Linq;

namespace DB2.Repositories.Product
{
    using Models;
    using DB2.Middleware;
    using NHibernate;
    using Npgsql;

    public class ProductRepository : IProductRepository
    {
        ISession session;
        public ProductRepository(ISession session)
        {
            this.session = session;
        }
        public List<Product> GetAll()
        {
            return session.Query<Product>().ToList();        
        }

        public Product Get(int id)
        {
            return session.Query<Product>().Where(x => x.Id == id).SingleOrDefault();
        }

        public Product Insert(Product model)
        {
            using (NHibernate.ITransaction transaction = session.BeginTransaction())
            {
                model.Id = 0;
                session.Save(model);
                transaction.Commit();
            }
            return model;
        }

        public Product Update(int id, Product model)
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
                Product basket = session.Query<Product>().Where(x => x.Id == id).SingleOrDefault();
                session.Delete(basket);
                transaction.Commit();
                return true;
            }
        }

        public List<Product> GetByType(string type)
        {
            var product = new List<Product>();
            var prodInfIds = session.Query<ProductInf>().Where(x => x.TypeProd == type).Select(x => x.Id);
            foreach (var prodInfId in prodInfIds)
            {
                var products = session.Query<Product>().Where(x => Convert.ToInt32(x.ProductInf) == prodInfId).ToList();
                product = product.Concat(products).ToList();
            }
            return product;
        }

        public List<Product> GetByGender(string gender)
        {
            var product = new List<Product>();
            var prodInfIds = session.Query<ProductInf>().Where(x => x.Gender == gender[0]).Select(x => x.Id).ToList();
            foreach (var prodInfId in prodInfIds)
            {
                var products = session.Query<Product>().Where(x => Convert.ToInt32(x.ProductInf) == prodInfId).ToList();
                product = product.Concat(products).ToList();
            }
            return product;
        }

        public List<Product> GetByPrice(int max, int min = 0)
        {
            return session.Query<Product>().Where(x => x.Price < max).Where(x => x.Price > min).ToList();
        }

        public List<Product> GetByTypeAndGender(string gender, string type)
        {
            var product = new List<Product>();
            var prodInfIds = session.Query<ProductInf>().Where(x => x.TypeProd == type).Where(x => x.Gender == gender[0]).Select(x => x.Id).ToList();
            foreach (var prodInfId in prodInfIds)
            {
                var products = session.Query<Product>().Where(x => Convert.ToInt32(x.ProductInf) == prodInfId).ToList();
                product = product.Concat(products).ToList();
            }
            return product;
        }
    }
}