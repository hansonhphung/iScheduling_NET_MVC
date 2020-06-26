using iScheduling.Repositories.Context.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace iScheduling.Repositories.Interface
{
    public interface IShiftRepositories
    {
        DTO.Models.EmployeeShift GetShiftById(string shiftId);
        IList<DTO.Models.EmployeeShift> GetAllShiftsByDate(DateTime date);

        IList<DTO.Models.Shift> GetAllShiftsByEmployeesWithinTime(string employeeId, DateTime startDate, DateTime endDate);

        bool AddShift(Shift shift);

        bool EditShift(Shift shift);

        bool DeleteShift(string shiftId);

        bool CancelShift(string shiftId);
    }
}
