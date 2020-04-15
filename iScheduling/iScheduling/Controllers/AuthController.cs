using iScheduling.DTO.Enums;
using iScheduling.Helper;
using iScheduling.Models;
using iScheduling.Models.Auth;
using iScheduling.Services.Interface;
using Newtonsoft.Json;
using System;
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

                string userData = JsonConvert.SerializeObject(emp);

                FormsAuthenticationTicket authTicket = new FormsAuthenticationTicket(
                                                            1, emp.EmployeeId, 
                                                            DateTime.Now, DateTime.Now.AddMinutes(30), 
                                                            false, userData
                                                        );

                string encryptTicket = FormsAuthentication.Encrypt(authTicket);

                HttpCookie userCookie = new HttpCookie("userCookie", encryptTicket);
                Response.Cookies.Add(userCookie);

                //FormsAuthentication.SetAuthCookie(emp.EmployeeId, false);

                return Redirect(ReturnUrl);

            }catch(Exception ex)
            {
                ViewBag.Message = "An internal error happend. Please try again later.";

                return View("Login");
            }
        }

        public ActionResult Logout()
        {
            HttpCookie userCookie = new HttpCookie("userCookie", "");
            userCookie.Expires = DateTime.Now.AddYears(-1);
            Response.Cookies.Add(userCookie);

            FormsAuthentication.SignOut();
            return View("Login");
        }
    }
}