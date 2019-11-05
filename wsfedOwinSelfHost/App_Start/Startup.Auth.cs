using System;
using System.Threading.Tasks;
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
        public void ConfigureAuth(IAppBuilder app)
        {
            app.UseCookieAuthentication(
                new CookieAuthenticationOptions
                {
                    AuthenticationType = CookieAuthenticationDefaults.AuthenticationType
                });
            
            app.UseWsFederationAuthentication(
            new WsFederationAuthenticationOptions
            {
                MetadataAddress = "https://namapp.example.com/nidp/saml2MetaData.xml",
                Wtrealm = "https://namapp.example.com",
                TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters
                {
                    ValidIssuer = "https://namapp.example.com/nidp/wsfed/"
                    //ValidateIssuer = false
                },
                /*Notifications = new WsFederationAuthenticationNotifications()
                {
                    RedirectToIdentityProvider = (ctx) =>
                    {
                        //To avoid a redirect loop to the federation server send 403 when user is authenticated but does not have access
                     
                        if (ctx.OwinContext.Response.StatusCode == 401 && ctx.OwinContext.Authentication.User.Identity.IsAuthenticated)
                        {
                            ctx.OwinContext.Response.StatusCode = 403;
                            ctx.HandleResponse();
                        }
                        //XHR requests cannot handle redirects to a login screen, return 401
                        if (ctx.OwinContext.Response.StatusCode == 401 && 1==2 ) //IsXhrRequest(ctx.OwinContext.Request))
                        {
                            ctx.HandleResponse();
                        }
                        return Task.FromResult(0);
                    },
                    SecurityTokenValidated = (ctx) =>
                    {
                        //Ignore scheme/host name in redirect Uri to make sure a redirect to HTTPS does not redirect back to HTTP
                        var redirectUri = new Uri(ctx.AuthenticationTicket.Properties.RedirectUri, UriKind.RelativeOrAbsolute);
                        if (redirectUri.IsAbsoluteUri)
                        {
                            ctx.AuthenticationTicket.Properties.RedirectUri = redirectUri.PathAndQuery;
                        }
                        //Sync user and the roles to EPiServer in the background
                        //ServiceLocator.Current.GetInstance<ISynchronizingUserService>().SynchronizeAsync(ctx.AuthenticationTicket.Identity);
                        return Task.FromResult(0);
                    }
                }*/

            }) ;

            //var audienceRestriction = new AudienceRestriction(AudienceUriMode.Always);
            //audienceRestriction.AllowedAudienceUris.Add(new Uri("urn:realm"));

            //var issuerRegistry = new ConfigurationBasedIssuerNameRegistry();
            //issuerRegistry.AddTrustedIssuer("xxxxxxxxxxxxxxxxxxxxxxxxx", "http://sts/");

            app.SetDefaultSignInAsAuthenticationType(CookieAuthenticationDefaults.AuthenticationType);
            Console.WriteLine("Configure Auth");
        }
    }
}
