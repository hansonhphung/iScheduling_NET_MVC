using iScheduling.DTO.Enums;
using iScheduling.Helper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace iScheduling.Models.Auth
{
    public class CustomAuthorizeAttribute : AuthorizeAttribute
    {
        private string MANAGEMENT_ROLE = string.Format("{0}, {1}, {2}",
            EnumHelpers.GetDescription(Role.STORE_MANAGER),
            EnumHelpers.GetDescription(Role.PRODUCTION_TEAM_MANAGER),
            EnumHelpers.GetDescription(Role.FRONT_TEAM_MANAGER));

        private string MEMBER_ROLE = string.Format("{0}, {1}", EnumHelpers.GetDescription(Role.FRONT_TEAM_MEMBER)
                                                        , EnumHelpers.GetDescription(Role.PRODUCTION_TEAM_MEMBER));
        protected virtual AuthenticationPrincipal CurrentUser
        {
            get { return HttpContext.Current.User as AuthenticationPrincipal; }
        }

        protected override bool AuthorizeCore(HttpContextBase httpContext)
        {
            Console.WriteLine("aaa");
            return ((CurrentUser != null) && !CurrentUser.IsInRole(Roles) || CurrentUser == null) ? false : true;
        }

        protected override void HandleUnauthorizedRequest(AuthorizationContext filterContext)
        {
            RedirectToRouteResult routeData = null;

            if(CurrentUser == null)
            {
                routeData = new RedirectToRouteResult(new System.Web.Routing.RouteValueDictionary(
                    new { controller = "Auth", action = "Login" }
                    ));
            }
            else if (CurrentUser.IsInRole(MANAGEMENT_ROLE))
            {
                routeData = new RedirectToRouteResult(new System.Web.Routing.RouteValueDictionary(
                    new { controller = "Shift", action = "ListShiftCalendarView" }
                    ));
            }else if (CurrentUser.IsInRole(MEMBER_ROLE))
            {
                routeData = new RedirectToRouteResult(new System.Web.Routing.RouteValueDictionary(
                    new { controller = "Shift", action = "EmployeeView", empId = CurrentUser.EmployeeId }
                    ));
            }

            filterContext.Result = routeData;
        }
    }
}