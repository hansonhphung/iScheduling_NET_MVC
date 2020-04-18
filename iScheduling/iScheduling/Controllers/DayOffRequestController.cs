using iScheduling.DTO.Enums;
using iScheduling.DTO.Models;
using iScheduling.Helper;
using iScheduling.Models;
using iScheduling.Models.Auth;
using iScheduling.Models.DayOffRequest;
using iScheduling.Services.Interface;
using System;
using System.Collections.Generic;
using System.Web.Mvc;

namespace iScheduling.Controllers
{
    [CustomAuthorize(Roles = "Store Manager, Front Manager, Production Manager")]
    public class DayOffRequestController : Controller
    {
        private readonly IDayOffRequestServices dayOffRequestServices;
        private readonly IShiftServices shiftServices;

        public DayOffRequestController(IDayOffRequestServices _dayOffRequestServices, IShiftServices _shiftServices)
        {
            dayOffRequestServices = _dayOffRequestServices;
            shiftServices = _shiftServices;
        }

        [HttpGet]
        public ActionResult List()
        {
            try {
                var lstRequests = dayOffRequestServices.GetAllPendingRequest();

                return View(lstRequests);
            }
            catch(Exception ex)
            {
                return View(new List<DayOffRequest>());
            }
        }

        [OverrideAuthorization]
        [CustomAuthorize(Roles = "Store Manager, Front Manager, Production Manager, Front Team Member, Production Team Member")]
        [HttpGet]
        public ActionResult Create(string shiftId)
        {
            try
            {
                var shift = shiftServices.GetShiftById(shiftId);

                var requestDayOffVM = new AddDayOffRequestVM
                {
                    ShiftId = shiftId,
                    DateOfShift = shift.StartTime.Date,
                    ShiftStartAt = shift.StartTime.ToShortTimeString(),
                    ShiftEndAt = shift.EndTime.ToShortTimeString()
                };

                return PartialView("_Create_Day_Off_Request", requestDayOffVM);
            }
            catch (Exception ex)
            {
                var userInfo = CookieHelpers.GetUserInfo();
                return Redirect(string.Format("/Shift/EmployeeView?empId={0}", userInfo.EmployeeId));
            }
        }

        [OverrideAuthorization]
        [CustomAuthorize(Roles = "Store Manager, Front Manager, Production Manager, Front Team Member, Production Team Member")]
        [HttpPost]
        public ActionResult Create(AddDayOffRequestVM request)
        {
            try
            {
                var userInfo = CookieHelpers.GetUserInfo();
                var dayOffRequest = new DayOffRequest
                {
                    RequestedShiftId = request.ShiftId,
                    RequestEmployeeId = userInfo.EmployeeId,
                    RequestedAt = DateTime.Now,
                    Reason = request.Reason,
                    Status = EnumHelpers.GetDescription(DayOffRequestStatus.PENDING)

                };

                var isAdded = dayOffRequestServices.CreateDayOff(dayOffRequest);

                return Json(new BaseViewModel<bool>(true, string.Empty, isAdded), JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new BaseViewModel<bool>(true, ex.Message, false), JsonRequestBehavior.AllowGet);
            }
        }

        [HttpGet]
        public ActionResult Approve(string shiftId)
        {
            try
            {
                var shift = shiftServices.GetShiftById(shiftId);

                var requestVM = new ApproveRejectDayOffRequestVM
                {
                    ShiftId = shiftId,
                    AssignedEmployee = shift.FullName,
                    DateOfShift = shift.StartTime.Date,
                    ShiftStartAt = shift.StartTime.ToShortTimeString(),
                    ShiftEndAt = shift.EndTime.ToShortTimeString()
                };

                return PartialView("_Approve_Day_Off_Request", requestVM);
            }
            catch (Exception ex)
            {
                var userInfo = CookieHelpers.GetUserInfo();
                return Redirect(string.Format("/Shift/EmployeeView?empId={0}", userInfo.EmployeeId));
            }
        }

        [HttpPost]
        public ActionResult Approve(ApproveRejectDayOffRequestVM request)
        {
            try
            {
                var userInfo = CookieHelpers.GetUserInfo();

                var isApproved = dayOffRequestServices.ApproveRequest(request.ShiftId, userInfo.EmployeeId, request.ResponseComment);

                return Json(new BaseViewModel<bool>(true, string.Empty, isApproved), JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new BaseViewModel<bool>(true, ex.Message, false), JsonRequestBehavior.AllowGet);
            }
        }

        [HttpGet]
        public ActionResult Reject(string shiftId)
        {
            try
            {
                var shift = shiftServices.GetShiftById(shiftId);

                var requestVM = new ApproveRejectDayOffRequestVM
                {
                    ShiftId = shiftId,
                    AssignedEmployee = shift.FullName,
                    DateOfShift = shift.StartTime.Date,
                    ShiftStartAt = shift.StartTime.ToShortTimeString(),
                    ShiftEndAt = shift.EndTime.ToShortTimeString()
                };

                return PartialView("_Reject_Day_Off_Request", requestVM);
            }
            catch (Exception ex)
            {
                var userInfo = CookieHelpers.GetUserInfo();
                return Redirect(string.Format("/Shift/EmployeeView?empId={0}", userInfo.EmployeeId));
            }
        }

        [HttpPost]
        public ActionResult Reject(ApproveRejectDayOffRequestVM request)
        {
            try
            {
                var userInfo = CookieHelpers.GetUserInfo();

                var isRejected = dayOffRequestServices.RejectRequest(request.ShiftId, userInfo.EmployeeId, request.ResponseComment);

                return Json(new BaseViewModel<bool>(true, string.Empty, isRejected), JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new BaseViewModel<bool>(true, ex.Message, false), JsonRequestBehavior.AllowGet);
            }
        }
    }
}