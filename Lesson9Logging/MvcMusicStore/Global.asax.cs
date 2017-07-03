using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using Autofac;
using Autofac.Integration;
using Autofac.Integration.Mvc;
using MvcMusicStore.Controllers;
using MvcMusicStore.Infrastructure;
using NLog;
using PerformanceCounterHelper;

namespace MvcMusicStore
{
    public class MvcApplication : System.Web.HttpApplication
    {
		//Create logger
		private readonly ILogger logger;

		public MvcApplication()
		{
			//Initialize logger
			logger = LogManager.GetCurrentClassLogger();
		}
        protected void Application_Start()
        {
			logger.Info("Application started");
			var builder = new ContainerBuilder();

			builder.RegisterControllers(typeof(HomeController).Assembly);
			builder.Register(f => LogManager.GetLogger("ForControllers")).As<ILogger>();
			DependencyResolver.SetResolver(new AutofacDependencyResolver(builder.Build()));

            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);

			logger.Info("Application started");
			
		}

		protected void Application_Error()
		{
			var ex = Server.GetLastError();

			//Save error to log
			logger.Error(ex.ToString());
		}

		protected new void Init()
		{
			base.Init();
			logger.Info("Application Init");
		}

		protected new void Dispose()
		{
			base.Dispose();
			logger.Info("Application Dispose");
		}
    }
}
