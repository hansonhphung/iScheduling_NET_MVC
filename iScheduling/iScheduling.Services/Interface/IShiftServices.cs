using iScheduling.DTO.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace iScheduling.Services.Interface
{
    public interface IShiftServices
    {
        EmployeeShift GetShiftById(string shiftId);
        IList<EmployeeShift> GetAllShiftsByDate(DateTime date);

        IList<Shift> GetAllShiftsByEmployeesWithinTime(string employeeId, DateTime startDate, DateTime endDate);

        bool AddShift(Shift shift);

        bool EditShift(Shift shift);

        bool DeleteShift(string shiftId);

        bool CancelShift(string shiftId);
    }
}
