using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Web;
using System.Web.Mvc;
using Microsoft.Owin.Security;
using Microsoft.Owin.Security.WsFederation;

namespace wsfedOwinSelfHost.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            ViewBag.ClaimsIdentity = System.Threading.Thread.CurrentPrincipal.Identity;
            var claimsIdentity = System.Threading.Thread.CurrentPrincipal.Identity as ClaimsIdentity;
            ViewBag.DisplayName = claimsIdentity.Claims.First(c => c.Type == ClaimTypes.NameIdentifier).Value;
            
            Console.WriteLine("index"); 
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult LogOff()
        {
            if (User.Identity.IsAuthenticated)
            {
                var owinContext = this.Request.GetOwinContext();
                var authProperties = new AuthenticationProperties();
                authProperties.RedirectUri = new Uri(this.HttpContext.Request.Url, new UrlHelper(this.ControllerContext.RequestContext).Action("PostLogOff")).AbsoluteUri;
                owinContext.Authentication.SignOut(authProperties);
                return View();
            }
            else
            {
                throw new InvalidOperationException("User is not authenticated");
            }
        }

        [AllowAnonymous]
        public ActionResult PostLogOff()
        {
            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}