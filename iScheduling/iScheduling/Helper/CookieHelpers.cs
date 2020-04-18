using iScheduling.Models.Auth;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace iScheduling.Helper
{
    public class CookieHelpers
    {
        public static AuthenticationPrincipal GetUserInfo()
        {
            return (AuthenticationPrincipal)System.Web.HttpContext.Current.User;
        }
    }
}