using Castle.MicroKernel.Registration;
using Castle.MicroKernel.SubSystems.Configuration;
using Castle.Windsor;
using NHibernate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;

namespace DB2.Utils
{
    using Middleware;
    using Repositories.Basket;
    using Repositories.Courier;
    using Repositories.Customer;
    using Repositories.Order;
    using Repositories.Product;
    using Repositories.ProductInf;
    using Repositories.ReportOnStorage;
    using Repositories.Warehouse;
    using Controllers;

    public class ApplicationCastleInstaller : IWindsorInstaller
    {
        public void Install(IWindsorContainer container, IConfigurationStore store)
        {
            container.Register(Component.For<ISession>().UsingFactoryMethod(NHibernateHelper.OpenSession).LifestylePerWebRequest());

            //container.Register(Component.For<ISession>().UsingFactoryMethod(x => x.Resolve<ISessionFactory>().OpenSession()).LifeStyle.PerWebRequest);
            container.Register(Component.For<IBasketRepository>().ImplementedBy<BasketRepository>().LifestyleTransient());
            container.Register(Component.For<ICourierRepository>().ImplementedBy<CourierRepository>().LifestyleTransient());
            container.Register(Component.For<ICustomerRepository>().ImplementedBy<CustomerRepository>().LifestyleTransient());
            container.Register(Component.For<IOrderRepository>().ImplementedBy<OrderRepository>().LifestyleTransient());
            container.Register(Component.For<IProductRepository>().ImplementedBy<ProductRepository>().LifestyleTransient());
            container.Register(Component.For<IProductInfRepository>().ImplementedBy<ProductInfRepository>().LifestyleTransient());
            container.Register(Component.For<IReportOnStorageRepository>().ImplementedBy<ReportOnStorageRepository>().LifestyleTransient());
            container.Register(Component.For<IWarehouseRepository>().ImplementedBy<WarehouseRepository>().LifestyleTransient());

            
            //container.Register(Component.For<FilmController>().DynamicParameters((a, b)=>{b["filmRepository"] = new FilmRepository();}));
            //container.Register(Component.For<PersonController>().DynamicParameters((a, b) => { b["personRepository"] = new FilmRepository(); b["personToFilmRepository"] = new PersonToFilmRepository(); }));
            //container.Register(Component.For<PersonToFilmController>().DynamicParameters((a, b) => { b["personToFilmRepository"] = new FilmRepository(); }));

            var controllers = Assembly.GetExecutingAssembly()
                .GetTypes().Where(x => x.BaseType == typeof(ApiController)).ToList();
            foreach (var controller in controllers)
            {
                container.Register(Component.For(controller).LifestylePerWebRequest());
            }
            //var filmController = container.Resolve<FilmController>();
            //filmController.
        }
    }
}