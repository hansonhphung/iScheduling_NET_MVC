using iScheduling.Services.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace iScheduling.Controllers
{
    public class HomeController : Controller
    {
        protected readonly IEmployeeServices employeeServices;

        public HomeController(IEmployeeServices _employeeServices) {
            employeeServices = _employeeServices;
        }

        public ActionResult Index()
        {
            var lstEmployees = employeeServices.GetAllEmployees();

            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}