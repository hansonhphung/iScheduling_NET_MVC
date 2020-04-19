using iScheduling.Repositories.Context.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace iScheduling.Repositories.Interface
{
    public interface IVacationRequestRepositories
    {
        bool CreateVacationRequest(VacationRequest request);
        bool ApproveRequest(string requestId, string approvedBy, string responseComment);
        bool RejectRequest(string requestId, string rejectedBy, string responseComment);
        IList<DTO.Models.VacationRequest> SearchRequest(string empId, string keyword);
        DTO.Models.VacationRequest GetRequestById(string requestId);
    }
}
