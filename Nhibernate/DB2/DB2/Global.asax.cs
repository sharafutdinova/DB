using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using System.Web.Security;
using System.Web.SessionState;
using System.Web.Http;
using Castle.Windsor;
using DB2.Utils;
using System.Web.Http.Dispatcher;

namespace DB2
{
    public class Global : HttpApplication
    {
        void Application_Start(object sender, EventArgs e)
        {
            // Code that runs on application startup

            var container = new WindsorContainer();
            container.Install(new ApplicationCastleInstaller());
            //var castleControllerFactory = new CastleControllerFactory(container);
            //ControllerBuilder.Current.SetControllerFactory(castleControllerFactory);

            var controllerActivator = new CastleControllerActivator(container);
            GlobalConfiguration.Configuration.Services.Replace(typeof(IHttpControllerActivator), controllerActivator);

            AreaRegistration.RegisterAllAreas();
            GlobalConfiguration.Configure(WebApiConfig.Register);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            log4net.Config.XmlConfigurator.Configure();
        }
    }
}