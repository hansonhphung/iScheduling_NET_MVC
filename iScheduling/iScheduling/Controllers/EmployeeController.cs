using iScheduling.DTO.Models;
using iScheduling.Models.Auth;
using iScheduling.Models.Employee;
using iScheduling.Services.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace iScheduling.Controllers
{
    [CustomAuthorize(Roles = "Store Manager, Front Manager, Production Manager")]
    public class EmployeeController : Controller
    {
        private readonly IEmployeeServices _employeeServices;

        public EmployeeController(IEmployeeServices employeeServices)
        {
            _employeeServices = employeeServices;
        }
        // GET: Employee
        public ActionResult List()
        {
            try
            {
                var lstEmployees = _employeeServices.GetAllEmployees();

                return View(new ListEmployeesVM(true, string.Empty, lstEmployees));
            }
            catch (Exception ex) {
                return View(new ListEmployeesVM(false, ex.Message, new List<Employee>()));
            }
        }

        [HttpPost]
        public ActionResult Add(Employee emp)
        {
            try
            {
                var isAdded = _employeeServices.AddEmployee(emp);
                return RedirectToAction("List");
            }
            catch(Exception ex)
            {
                return RedirectToAction("List");
            }
            
        }

        [HttpGet]
        public ActionResult Edit(string employeeId) {
            try
            {
                var emp = _employeeServices.GetEmployeeById(employeeId);

                return PartialView("_Edit_Employee", emp);
            }
            catch(Exception ex)
            {
                return PartialView("_Edit_Employee", new Employee());
            }
        }

        [HttpPost]
        public ActionResult Edit(Employee emp)
        {
            try
            {
                var isEditted = _employeeServices.UpdateEmployee(emp);

                return RedirectToAction("List");
            }catch(Exception ex)
            {
                return RedirectToAction("List");
            }
        }

        [HttpGet]
        public ActionResult _Delete(string employeeId) {
            try
            {
                var emp = _employeeServices.GetEmployeeById(employeeId);

                return PartialView("_Delete_Employee", emp);
            }
            catch (Exception ex)
            {
                return PartialView("_Delete_Employee", new Employee());
            }
        }

        [HttpPost]
        public ActionResult Delete(string employeeId) {
            try
            {
                var isDeleted = _employeeServices.DeleteEmployee(employeeId);

                return RedirectToAction("List");
            }catch(Exception ex)
            {
                return RedirectToAction("List");
            }
        }
    }
}