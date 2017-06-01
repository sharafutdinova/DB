using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DB2.Middleware
{
    using NHibernate.Cfg;
    using FluentNHibernate.Cfg;
    using NHibernate;
    using Maps;
    using Models;

    public static class NHibernateHelper
    {
        private static readonly ISessionFactory sessionFactory;
        private static readonly Configuration configuration = new Configuration().Configure();

        static NHibernateHelper()
        {
            sessionFactory = Fluently.Configure(configuration).Mappings(x => x.FluentMappings.AddFromAssemblyOf<Base>()).BuildSessionFactory();
        }
        public static ISession OpenSession()
        {
            return sessionFactory.OpenSession();
        }
    }
}
