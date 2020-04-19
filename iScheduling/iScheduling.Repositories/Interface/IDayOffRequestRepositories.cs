using iScheduling.Repositories.Context.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace iScheduling.Repositories.Interface
{
    public interface IDayOffRequestRepositories
    {
        bool CreateDayOff(DayOffRequest request);
        bool ApproveRequest(string requestId, string shiftId, string approvedBy ,string responseComment);
        bool RejectRequest(string requestId, string shiftId, string rejectedBy ,string responseComment);
        IList<DTO.Models.DayOffRequest> GetAllPendingRequest();
        DTO.Models.DayOffRequest GetRequestById(string requestId);
    }
}
