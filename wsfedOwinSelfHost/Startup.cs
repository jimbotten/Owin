using System;
using System.Threading.Tasks;
using Microsoft.IdentityModel.Logging;
using Microsoft.Owin;
using Microsoft.Owin.Security;
using Microsoft.Owin.Security.Cookies;
using Microsoft.Owin.Security.WsFederation;
using Owin;

[assembly: OwinStartup(typeof(wsfedOwinSelfHost.Startup))]

namespace wsfedOwinSelfHost
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            IdentityModelEventSource.ShowPII = true; 
            ConfigureAuth(app);
            Console.WriteLine("Configuration");
        }

    }
}