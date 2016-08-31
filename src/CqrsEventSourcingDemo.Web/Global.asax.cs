using System;
using System.Diagnostics;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using Reusables.Diagnostics.Logging;

namespace CqrsEventSourcingDemo.Web
{
    public class MvcApplication : HttpApplication
    {
        private const string _KEY = "tq8OuyxHpDgkRg54klfpqg==";
        private static ILogger _logger;

        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            GlobalConfiguration.Configure(WebApiConfig.Register);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);

            DependencyResolverConfig.RegisterDependencies();

            _logger = ((ILoggerFactory) DependencyResolverConfig.ServiceProvider.GetService(typeof(ILoggerFactory))).GetCurrentClassLogger();
        }

        protected void Application_BeginRequest()
        {
            var context = HttpContext.Current;

            if (context.Items.Contains(_KEY))
                return;

            var mutex = string.Intern($"{_KEY}:{context.GetHashCode()}");

            lock (mutex)
                if (!context.Items.Contains(_KEY))
                    context.Items[_KEY] = Guid.NewGuid();

            var stopwatch = new Stopwatch();

            HttpContext.Current.Items[_KEY + "_Stopwatch"] = stopwatch;

            stopwatch.Start();
        }

        protected void Application_EndRequest()
        {
            var requestId = HttpContext.Current.Items[_KEY];
            var stopwatch = (Stopwatch) HttpContext.Current.Items[_KEY + "_Stopwatch"];

            stopwatch.Stop();

            _logger.Info($"Request {requestId} elapsed: {(stopwatch.ElapsedTicks/10000).ToString("F")} ms");
        }
    }
}
