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
        private static ILogger _logger;

        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            GlobalConfiguration.Configure(WebApiConfig.Register);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);

            DependencyResolverConfig.RegisterDependencies();

            _logger = (ILogger) DependencyResolverConfig.ServiceProvider.GetService(typeof (ILogger));
        }

        protected void Application_BeginRequest()
        {
            var context = HttpContext.Current;
            const string key = "tq8OuyxHpDgkRg54klfpqg==";

            if (context.Items.Contains(key))
            {
                return;
            }

            var mutex = string.Intern($"{key}:{context.GetHashCode()}");

            lock (mutex)
            {
                if (!context.Items.Contains(key))
                {
                    context.Items[key] = Guid.NewGuid();
                }
            }

            var stopwatch = new Stopwatch();

            HttpContext.Current.Items["tq8OuyxHpDgkRg54klfpqg==_Stopwatch"] = stopwatch;

            stopwatch.Start();
        }

        protected void Application_EndRequest()
        {
            var requestId = HttpContext.Current.Items["tq8OuyxHpDgkRg54klfpqg=="];
            var stopwatch = (Stopwatch) HttpContext.Current.Items["tq8OuyxHpDgkRg54klfpqg==_Stopwatch"];

            stopwatch.Stop();

            _logger.Info($"Request {requestId} elapsed: {(stopwatch.ElapsedTicks/10000).ToString("F")} ms");
        }
    }
}
