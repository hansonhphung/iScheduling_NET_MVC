using iScheduling.DTO.Models;
using iScheduling.Helper;
using iScheduling.Models;
using iScheduling.Models.Shift;
using iScheduling.Services.Interface;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace iScheduling.Controllers
{
    public class ShiftController : Controller
    {
        private readonly IEmployeeServices employeeServices;
        private readonly IShiftServices shiftServices;

        private readonly string ADD_SHIFT_SUCCESS = "ADD_SHIFT_SUCCESS";
        private readonly string EDIT_SHIFT_SUCCESS = "EDIT_SHIFT_SUCCESS";
        private readonly string DELETE_SHIFT_SUCCESS = "DELETE_SHIFT_SUCCESS";
        public ShiftController(IEmployeeServices _employeeServices, IShiftServices _shiftServices)
        {
            employeeServices = _employeeServices;
            shiftServices = _shiftServices;
        }

        // GET: Shift
        public ActionResult Index(DateTime? date)
        {
            
            return View("CalendarView", new BaseViewModel<DateTime?>(true, "", date));
        }

        [HttpPost]
        public ActionResult List(DateTime date)
        {
            //DateTime datee = DateTime.Parse(date);

            var lstShifts = shiftServices.GetAllShiftsByDate(date).ToList();
            lstShifts.ForEach(x => x.ColorFrom = ColorHelpers.RandomColor());
            lstShifts.ForEach(x => x.ColorTo = ColorHelpers.RandomColor());

            var lstEmployeeShifts = new ShiftCalendarViewVM(true, string.Empty, lstShifts);

            return PartialView("_ListShift", lstEmployeeShifts);
        }

        [HttpGet]
        public ActionResult EmployeeView(string empId)
        {
            try
            {
                var employee = employeeServices.GetEmployeeById(empId);

                return View(employee);
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }
        
        [HttpGet]
        public ActionResult ListShiftEmployeeView(string empId, DateTime startDate, DateTime endDate)
        {
            try
            {
                var lstShifts = shiftServices.GetAllShiftsByEmployeesWithinTime(empId, startDate, endDate);

                
                var employeeShifts = new ShiftEmployeeViewVM
                {
                    EmployeeId = empId,
                    ListShifts = lstShifts
                };

                return PartialView("_List_Shift_Employee_View", employeeShifts);
            }
            catch (Exception ex)
            {
                return RedirectToAction("List", "Employee");
            }
        }

        [HttpGet]
        public ActionResult AddCalendarView(DateTime selectedDate)
        {
            IList<Employee> lstEmployee = employeeServices.GetAllEmployeeOrderByPosition();
            
            var shift = new AddShiftVM()
            {
                DateOfShift = selectedDate,
                ListEmployees = lstEmployee,
                IsCalendarView = true
            };

            return PartialView("_Add_Edit_Shift", shift);
        }

        [HttpGet]
        public ActionResult AddShiftEmployeeView(string employeeId, DateTime startDate, DateTime endDate)
        {
            string employeeFullName = employeeFullName = employeeServices.GetEmployeeById(employeeId).FullName;

            var shift = new AddShiftVM()
            {
                AssignedShiftEmployeeId = employeeId,
                AssignedShiftEmployeeFullName = employeeFullName,
                StartDate = startDate,
                EndDate = endDate,
                IsCalendarView = false
            };

            return PartialView("_Add_Edit_Shift", shift);
        }

        [HttpPost]
        public ActionResult Add(AddShiftVM shift)
        {
            try
            {
                var startAt = DateTime.Parse(shift.ShiftStartAt);
                var endAt = DateTime.Parse(shift.ShiftEndAt);

                bool isAdded = shiftServices.AddShift(new Shift() {
                    EmployeeId = shift.AssignedShiftEmployeeId,
                    // Work around to fix bug UI cannot parse datetime.
                    // Then simple get the DateOfShift plus the start/end hour of the shift
                    StartTime = ((DateTime)shift.DateOfShift).AddHours(startAt.Hour),
                    EndTime = ((DateTime)shift.DateOfShift).AddHours(endAt.Hour)
                });

                if (!shift.IsCalendarView)
                    return Json(new BaseViewModel<bool>(true, string.Empty , isAdded), JsonRequestBehavior.AllowGet);

                return RedirectToAction("Index", shift.DateOfShift);

                //return Json(new BaseViewModel<bool>(true, APIResponseMessages.ADD_SHIFT_SUCCESS, true));
            }
            catch (Exception ex)
            {
                return Json(new BaseViewModel<bool>(false, ex.Message, false), JsonRequestBehavior.AllowGet);
            }
        }

        [HttpGet]
        public ActionResult Edit(string shiftId, bool isCalendarView)
        {
            try
            {
                var lstEmployee = employeeServices.GetAllEmployeeOrderByPosition();

                var shift = shiftServices.GetShiftById(shiftId);

                var shiftVM = new AddShiftVM()
                {
                    IsCalendarView = isCalendarView,
                    ShiftId = shift.ShiftId,
                    DateOfShift = shift.StartTime.Date,
                    ListEmployees = lstEmployee,
                    AssignedShiftEmployeeId = shift.EmployeeId,
                    AssignedShiftEmployeeFullName = shift.FullName,
                    ShiftStartAt = shift.StartTime.ToShortTimeString(),
                    ShiftEndAt = shift.EndTime.ToShortTimeString()
                };

                return PartialView("_Add_Edit_Shift", shiftVM);
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        public ActionResult Edit(AddShiftVM shift)
        {
            try
            {
                var startAt = DateTime.Parse(shift.ShiftStartAt);
                var endAt = DateTime.Parse(shift.ShiftEndAt);

                bool isEdited = shiftServices.EditShift(new Shift()
                {
                    ShiftId = shift.ShiftId,
                    EmployeeId = shift.AssignedShiftEmployeeId,
                    // Work around to fix bug UI cannot parse datetime.
                    // Then simple get the DateOfShift plus the start/end hour of the shift
                    StartTime = ((DateTime)shift.DateOfShift).AddHours(startAt.Hour),
                    EndTime = ((DateTime)shift.DateOfShift).AddHours(endAt.Hour)
                });

                if (!shift.IsCalendarView)
                    return Json(new BaseViewModel<bool>(true, string.Empty, isEdited), JsonRequestBehavior.AllowGet);

                return RedirectToAction("Index", shift.DateOfShift);
            }
            catch (Exception ex)
            {
                return Json(new BaseViewModel<bool>(true, ex.Message, false), JsonRequestBehavior.AllowGet);
            }
        }

        [HttpGet]
        public JsonResult Delete(string shiftId)
        {
            try {
                bool isDeleted = shiftServices.DeleteShift(shiftId);

                return Json(new BaseViewModel<bool>(true, APIResponseMessages.DELETE_SHIFT_SUCCESS, true), JsonRequestBehavior.AllowGet);
            }
            catch(Exception ex)
            {
                return Json(new BaseViewModel<bool>(false, ex.Message, false), JsonRequestBehavior.AllowGet);
            }
        }

        
    }
}