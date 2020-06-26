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
    public class DayOffRequestRepositories : BaseRepositories, IDayOffRequestRepositories
    {
        public DayOffRequestRepositories(iSchedulingContext context ) : base(context) { }

        public DTO.Models.DayOffRequest GetRequestById(string requestId)
        {
            try
            {
                var request = Entities.DayOffRequests
                    .Join(Entities.Employees, dor => dor.RequestEmployeeId, e => e.EmployeeId,
                         (dor, e) => new { dor, e })
                    .Where(x => x.dor.RequestId == requestId)
                    .FirstOrDefault();

                return new DTO.Models.DayOffRequest
                {
                    RequestId = request.dor.RequestId,
                    RequestEmployeeId = request.dor.RequestEmployeeId,
                    RequestEmployeeName = request.e.FirstName + " " + request.e.LastName,
                    RequestedAt = request.dor.RequestedAt,
                    Reason = request.dor.Reason,
                    RequestedShift = new DTO.Models.Shift
                    {
                        ShiftId = request.dor.RequestedShift.ShiftId,
                        EmployeeId = request.dor.RequestedShift.EmployeeId,
                        StartTime = request.dor.RequestedShift.StartTime,
                        EndTime = request.dor.RequestedShift.EndTime
                    },
                };
            }catch(Exception ex)
            {
                throw ex;
            }
        }

        public IList<DTO.Models.DayOffRequest> GetAllPendingRequest()
        {
            try
            {
                var pending = EnumHelpers.GetDescription(DayOffRequestStatus.PENDING);
                return Entities.DayOffRequests
                    .Join(Entities.Employees, dor => dor.RequestEmployeeId, e => e.EmployeeId,
                         (dor, e ) => new { dor, e})
                    .Join(Entities.Shifts, dor_e => dor_e.dor.RequestedShiftId, s => s.ShiftId,
                         (dor_e, s) => new {
                             dor_e.dor.RequestId,
                             dor_e.dor.RequestEmployeeId,
                             RequestEmployeeName = dor_e.e.FirstName + " " + dor_e.e.LastName,
                             dor_e.dor.RequestedShiftId,
                             s.StartTime,
                             s.EndTime,
                             dor_e.dor.RequestedAt,
                             dor_e.dor.Reason,
                             dor_e.dor.Status
                         })
                    .Where(x => x.Status.Equals(pending))
                    .Select(r => new DTO.Models.DayOffRequest {
                        RequestId = r.RequestId,
                        RequestEmployeeId = r.RequestEmployeeId,
                        RequestEmployeeName = r.RequestEmployeeName,
                        RequestedShift = new DTO.Models.Shift {
                            ShiftId = r.RequestedShiftId,
                            StartTime = r.StartTime,
                            EndTime = r.EndTime
                            },
                        RequestedAt = r.RequestedAt,
                        Reason = r.Reason,
                        Status = r.Status
                    })
                    .ToList();
            }catch(Exception ex)
            {
                throw ex;
            }
        }

        public bool CreateDayOff(DayOffRequest request)
        {
            try
            {
                Entities.DayOffRequests.Add(request);
                Entities.SaveChanges();

                return true;
            }catch(Exception ex)
            {
                throw ex;
            }
        }


        public bool ApproveRequest(string requestId, string shiftId, string approvedBy ,string responseComment)
        {
            try
            {
                var pending = EnumHelpers.GetDescription(DayOffRequestStatus.PENDING);
                var approved = EnumHelpers.GetDescription(DayOffRequestStatus.APPROVED);

                var request = Entities.DayOffRequests.Where(x => x.RequestId.Equals(requestId) && x.Status.Equals(pending)).FirstOrDefault();

                if (request == null)
                    return false;

                request.ResponseManagerId = approvedBy;
                request.Status = approved;
                request.ResponseComment = responseComment;

                Entities.SaveChanges();

                return true;
            }catch(Exception ex)
            {
                throw ex;
            }
        }
        

        public bool RejectRequest(string requestId, string shiftId, string rejectedBy ,string responseComment)
        {
            try
            {
                var pending = EnumHelpers.GetDescription(DayOffRequestStatus.PENDING);
                var rejected = EnumHelpers.GetDescription(DayOffRequestStatus.REJECTED);

                var request = Entities.DayOffRequests.Where(x => x.RequestId.Equals(requestId) && x.Status.Equals(pending)).FirstOrDefault();

                if (request == null)
                    return false;

                request.ResponseManagerId = rejectedBy;
                request.Status = rejected;
                request.ResponseComment = responseComment;

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
