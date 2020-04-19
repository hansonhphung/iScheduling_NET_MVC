using iScheduling.DTO.Enums;
using iScheduling.DTO.Models;
using iScheduling.Helper;
using iScheduling.Models;
using iScheduling.Models.Auth;
using iScheduling.Models.VacationRequest;
using iScheduling.Services.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace iScheduling.Controllers
{
    [CustomAuthorize(Roles = "Store Manager, Front Manager, Production Manager, Front Team Member, Production Team Member")]
    public class VacationRequestController : Controller
    {
        private readonly IVacationRequestServices vacationRequestServices;

        public VacationRequestController(IVacationRequestServices _vacationRequestServices)
        {
            vacationRequestServices = _vacationRequestServices;
        }

        [HttpGet]
        public ActionResult List(string keyword)
        {
            try
            {
                keyword = (keyword == null) ? string.Empty : keyword;

                var empId = "";

                var userInfo = CookieHelpers.GetUserInfo();
                if (userInfo.IsInMemberRole())
                    empId = userInfo.EmployeeId; ;

                var lstRequest = vacationRequestServices.SearchRequest(empId, keyword);

                var vm = new ListVacationRequestVM
                {
                    KeyWord = keyword,
                    ListRequest = lstRequest
                };

                return View(vm);
            }
            catch(Exception ex)
            {
                return View(new ListVacationRequestVM());
            }
        }

        [HttpGet]
        public ActionResult Create()
        {
            return PartialView("_Create_Vacation_Request");
        }

        [HttpPost]
        public ActionResult Create(AddVacationRequestVM request)
        {
            try
            {
                var userInfo = CookieHelpers.GetUserInfo();
                var vacationRequest = new VacationRequest
                {
                    RequestEmployeeId = userInfo.EmployeeId,
                    RequestedAt = DateTime.Now,
                    StartDate = request.StartDate,
                    EndDate = request.EndDate,
                    Status = EnumHelpers.GetDescription(VacationRequestStatus.PENDING)
                };

                var isAdded = vacationRequestServices.CreateVacationRequest(vacationRequest);

                return Json(new BaseViewModel<bool>(true, string.Empty, isAdded), JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new BaseViewModel<bool>(true, ex.Message, false), JsonRequestBehavior.AllowGet);
            }
        }

        [HttpGet]
        public ActionResult Approve(string requestId)
        {
            try
            {
                var request = vacationRequestServices.GetRequestById(requestId);

                var requestVM = new ApproveRejectVacationRequestVM
                {
                    RequestId = request.RequestId,
                    RequestedBy = request.RequestEmployeeName,
                    RequestedAt = request.RequestedAt,
                    StartDate = request.StartDate,
                    EndDate = request.EndDate,
                };

                return PartialView("_Approve_Vacation_Request", requestVM);
            }
            catch (Exception ex)
            {
                return Redirect("/VacationRequest/List");
            }
        }

        [HttpPost]
        public ActionResult Approve(ApproveRejectVacationRequestVM request)
        {
            try
            {
                var userInfo = CookieHelpers.GetUserInfo();

                var isApproved = vacationRequestServices.ApproveRequest(request.RequestId, userInfo.EmployeeId, request.ResponseComment);

                return Json(new BaseViewModel<bool>(true, string.Empty, isApproved), JsonRequestBehavior.AllowGet);

            }
            catch(Exception ex)
            {
                return Json(new BaseViewModel<bool>(true, ex.Message, false), JsonRequestBehavior.AllowGet);
            }
        }

        [HttpGet]
        public ActionResult Reject(string requestId)
        {
            try
            {
                var request = vacationRequestServices.GetRequestById(requestId);

                var requestVM = new ApproveRejectVacationRequestVM
                {
                    RequestId = request.RequestId,
                    RequestedBy = request.RequestEmployeeName,
                    RequestedAt = request.RequestedAt,
                    StartDate = request.StartDate,
                    EndDate = request.EndDate,
                };

                return PartialView("_Reject_Vacation_Request", requestVM);
            }
            catch (Exception ex)
            {
                return Redirect("/VacationRequest/List");
            }
        }

        [HttpPost]
        public ActionResult Reject(ApproveRejectVacationRequestVM request)
        {
            try
            {
                var userInfo = CookieHelpers.GetUserInfo();

                var isApproved = vacationRequestServices.RejectRequest(request.RequestId, userInfo.EmployeeId, request.ResponseComment);

                return Json(new BaseViewModel<bool>(true, string.Empty, isApproved), JsonRequestBehavior.AllowGet);

            }
            catch (Exception ex)
            {
                return Json(new BaseViewModel<bool>(true, ex.Message, false), JsonRequestBehavior.AllowGet);
            }
        }
    }
}