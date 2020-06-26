using iScheduling.DTO.Enums;
using iScheduling.Repositories.Context;
using iScheduling.Repositories.Context.Entities;
using iScheduling.Repositories.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace iScheduling.Repositories.Implementation
{
    public class VacationRequestRepositories : BaseRepositories, IVacationRequestRepositories
    {
        public VacationRequestRepositories(iSchedulingContext context) : base(context) { }

        public IList<DTO.Models.VacationRequest> SearchRequest(string empId, string keyword)
        {
            try
            {
                IQueryable<DTO.Models.VacationRequest> query = Entities.VacationRequests
                    .Join(Entities.Employees, vr => vr.RequestEmployeeId, e => e.EmployeeId,
                         (vr, e) => new { vr, e })
                    .Where(x => x.e.FirstName.Contains(keyword) || x.e.LastName.Contains(keyword)).Select(x => new DTO.Models.VacationRequest
                    {
                        RequestId = x.vr.RequestId,
                        RequestEmployeeId = x.vr.RequestEmployeeId,
                        RequestEmployeeName = x.e.FirstName + " " + x.e.LastName,
                        RequestedAt = x.vr.RequestedAt,
                        StartDate = x.vr.StartDate,
                        EndDate = x.vr.EndDate,
                        Status = x.vr.Status,
                        ResponseComment = x.vr.Comment
                    });

                if (!empId.Equals(string.Empty))
                    query = query.Where(x => x.RequestEmployeeId.Equals(empId));

                return query.OrderByDescending(x => x.RequestedAt).ToList();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public DTO.Models.VacationRequest GetRequestById(string requestId)
        {
            try
            {
                return Entities.VacationRequests
                    .Join(Entities.Employees, vr => vr.RequestEmployeeId, e => e.EmployeeId,
                         (vr, e) => new { vr, e })
                    .Where(x => x.vr.RequestId.Equals(requestId))
                    .Select(x => new DTO.Models.VacationRequest
                    {
                        RequestId = x.vr.RequestId,
                        RequestEmployeeId = x.vr.RequestEmployeeId,
                        RequestEmployeeName = x.e.FirstName + " " + x.e.LastName,
                        RequestedAt = x.vr.RequestedAt,
                        StartDate = x.vr.StartDate,
                        EndDate = x.vr.EndDate,
                        Status = x.vr.Status,
                        ResponseComment = x.vr.Comment
                    }).FirstOrDefault();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public bool CreateVacationRequest(VacationRequest request)
        {
            try
            {
                Entities.VacationRequests.Add(request);

                Entities.SaveChanges();

                return true;
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }

        public bool ApproveRequest(string requestId, string approvedBy, string responseComment)
        {
            try
            {
                var pending = EnumHelpers.GetDescription(VacationRequestStatus.PENDING);
                var approved = EnumHelpers.GetDescription(VacationRequestStatus.APPROVED);

                var request = Entities.VacationRequests.Where(x => x.RequestId.Equals(requestId) && x.Status.Equals(pending)).FirstOrDefault();

                if (request == null)
                    return false;

                request.ResponseManagerId = approvedBy;
                request.Status = approved;
                request.Comment = responseComment;

                Entities.SaveChanges();

                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public bool RejectRequest(string requestId, string rejectedBy, string responseComment)
        {
            try
            {
                var pending = EnumHelpers.GetDescription(VacationRequestStatus.PENDING);
                var rejected = EnumHelpers.GetDescription(VacationRequestStatus.REJECTED);

                var request = Entities.VacationRequests.Where(x => x.RequestId.Equals(requestId) && x.Status.Equals(pending)).FirstOrDefault();

                if (request == null)
                    return false;

                request.ResponseManagerId = rejectedBy;
                request.Status = rejected;
                request.Comment = responseComment;

                Entities.SaveChanges();

                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
