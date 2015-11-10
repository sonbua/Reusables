using Microsoft.Owin;
using Owin;

[assembly: OwinStartup(typeof(CqrsEventSourcingDemo.Web.Startup))]
namespace CqrsEventSourcingDemo.Web
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
