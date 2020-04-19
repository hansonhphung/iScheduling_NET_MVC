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

                var lstRequest = vacationRequestServices.SearchRequest(keyword);

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
    }
}