using Logga;
using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(LoggaSample.Mvc.Startup))]
namespace LoggaSample.Mvc
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);

            LoggaConfiguration.UseSqlServerData("ErrorLogContext");
        }
    }
}
