using iScheduling.DTO.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace iScheduling.Services.Interface
{
    public interface IVacationRequestServices
    {
        IList<VacationRequest> SearchRequest(string keyword);
        VacationRequest GetRequestById(string requestId);
        bool CreateVacationRequest(VacationRequest request);
        bool ApproveRequest(string requestId, string approvedBy, string responseComment);
        bool RejectRequest(string requestId, string rejectedBy, string responseComment);
    }
}
