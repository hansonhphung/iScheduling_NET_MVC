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
    public class VacationRequestServices : IVacationRequestServices
    {
        private readonly IVacationRequestRepositories vacationRequestRepositories;

        public VacationRequestServices(IVacationRequestRepositories _vacationRequestRepositories)
        {
            vacationRequestRepositories = _vacationRequestRepositories;
        }

        public IList<VacationRequest> SearchRequest(string empId, string keyword)
        {
            try
            {
                return vacationRequestRepositories.SearchRequest(empId, keyword);
            }catch(Exception ex)
            {
                throw ex;
            }
        }

        public VacationRequest GetRequestById(string requestId)
        {
            try
            {
                return vacationRequestRepositories.GetRequestById(requestId);
            }catch(Exception ex)
            {
                throw ex;
            }
        }

        public bool ApproveRequest(string requestId, string approvedBy, string responseComment)
        {
            try
            {
                return vacationRequestRepositories.ApproveRequest(requestId, approvedBy, responseComment);
            }catch(Exception ex)
            {
                throw ex;
            }
        }
        
        public bool RejectRequest(string requestId, string rejectedBy, string responseComment)
        {
            try
            {
                return vacationRequestRepositories.RejectRequest(requestId, rejectedBy, responseComment);
            }catch(Exception ex)
            {
                throw ex;
            }
        }
        public bool CreateVacationRequest(VacationRequest request)
        {
            try
            {
                return vacationRequestRepositories.CreateVacationRequest(new Repositories.Context.Entities.VacationRequest
                {
                    RequestId = Guid.NewGuid().ToString(),
                    RequestEmployeeId = request.RequestEmployeeId,
                    RequestedAt = DateTime.Now,
                    Status = EnumHelpers.GetDescription(VacationRequestStatus.PENDING),
                    StartDate = request.StartDate,
                    EndDate = request.EndDate
                });
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }

    }
}
