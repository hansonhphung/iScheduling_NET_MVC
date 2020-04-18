using iScheduling.DTO.Enums;
using iScheduling.DTO.Models;
using iScheduling.Repositories.Interface;
using iScheduling.Services.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace iScheduling.Services.Implementation
{
    public class DayOffRequestServices : IDayOffRequestServices
    {
        private readonly IDayOffRequestRepositories dayOffRequestRepositories;

        public DayOffRequestServices(IDayOffRequestRepositories _dayOffRequestRepositories)
        {
            dayOffRequestRepositories = _dayOffRequestRepositories;
        }

        public IList<DayOffRequest> GetAllPendingRequest()
        {
            try
            {
                var lstRequests =  dayOffRequestRepositories.GetAllPendingRequest().ToList();

                return lstRequests;

            }catch(Exception ex)
            {
                throw ex;
            }
        }
        public bool CreateDayOff(DayOffRequest request)
        {
            try
            {
                return dayOffRequestRepositories.CreateDayOff(new Repositories.Context.Entities.DayOffRequest
                {
                    RequestId = Guid.NewGuid().ToString(),
                    RequestedShiftId = request.RequestedShiftId,
                    RequestEmployeeId = request.RequestEmployeeId,
                    RequestedAt = DateTime.Now,
                    Status = EnumHelpers.GetDescription(DayOffRequestStatus.PENDING),
                    Reason = request.Reason
                });
            }catch(Exception ex)
            {
                throw ex;
            }
        }

        public bool ApproveRequest(string shiftId, string approvedBy, string responseComment)
        {
            try
            {
                return dayOffRequestRepositories.ApproveRequest(shiftId, approvedBy, responseComment);
            }catch(Exception ex)
            {
                throw ex;
            }
        }

        public bool RejectRequest(string shiftId, string rejectedBy, string responseComment)
        {
            try
            {
                return dayOffRequestRepositories.RejectRequest(shiftId, rejectedBy, responseComment);
            }catch(Exception ex)
            {
                throw ex;
            }
        }
    }
}
