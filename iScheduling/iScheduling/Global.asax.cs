using iScheduling.App_Start;
using iScheduling.DTO.Models;
using iScheduling.Models.Auth;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using System.Web.Security;

namespace iScheduling
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
            IoCConfig.Register();
        }

        protected void Application_PostAuthenticateRequest(Object sender, EventArgs e)
        {
            HttpCookie authCookie = Request.Cookies["userCookie"];

            if(authCookie != null)
            {
                FormsAuthenticationTicket authTicket = FormsAuthentication.Decrypt(authCookie.Value);

                var serializeEmployeeModel = JsonConvert.DeserializeObject<Employee>(authTicket.UserData);

                AuthenticationPrincipal principal = new AuthenticationPrincipal(serializeEmployeeModel.EmployeeId);

                principal.EmployeeId = serializeEmployeeModel.EmployeeId;
                principal.FirstName = serializeEmployeeModel.FirstName;
                principal.LastName = serializeEmployeeModel.LastName;
                principal.Position = serializeEmployeeModel.Position;

                HttpContext.Current.User = principal;
            }
        }
    }
}
