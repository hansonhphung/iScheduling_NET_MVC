using iScheduling.DTO.Enums;
using iScheduling.Helper;
using iScheduling.Models;
using iScheduling.Services.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace iScheduling.Controllers
{
    [AllowAnonymous]
    public class AuthController : Controller
    {
        private readonly IEmployeeServices _employeeServices;

        public AuthController(IEmployeeServices employeeServices)
        {
            _employeeServices = employeeServices;
        }


        // GET: Auth
        [HttpGet]
        public ActionResult Login(string ReturnUrl)
        {
            ViewBag.ReturnUrl = ReturnUrl;
            return View();
        }

        [HttpPost]
        public ActionResult Login(AuthorizeUser user, string ReturnUrl)
        {
            try
            {
                var emp = _employeeServices.Login(user.Username, user.Password);

                if(emp == null)
                {
                    ViewBag.Message = "Invalid username/password";
                    return View("Login");
                }

                string role = emp.Position;

                if (ReturnUrl.IsNullOrEmpty())
                {
                    if (role.Equals(EnumHelpers.GetDescription(Role.FRONT_TEAM_MEMBER)) || role.Equals(EnumHelpers.GetDescription(Role.PRODUCTION_TEAM_MEMBER)))
                        ReturnUrl = string.Format("/Shift/EmployeeView?empId={0}", emp.EmployeeId); 
                    else
                        ReturnUrl = "/Shift";
                }

                FormsAuthentication.SetAuthCookie(emp.Username, false);

                return Redirect(ReturnUrl);

            }catch(Exception ex)
            {
                ViewBag.Message = "An internal error happend. Please try again later.";

                return View("Login");
            }
        }

        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();
            return View("Login");
        }
    }
}