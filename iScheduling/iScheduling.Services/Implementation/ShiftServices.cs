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
    public class ShiftServices : IShiftServices
    {
        private readonly IShiftRepositories shiftRepositories;
        public ShiftServices(IShiftRepositories _shiftRepositories)
        {
            shiftRepositories = _shiftRepositories;
        }

        public EmployeeShift GetShiftById(string shiftId)
        {
            try
            {
                return shiftRepositories.GetShiftById(shiftId);
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }

        public IList<EmployeeShift> GetAllShiftsByDate(DateTime date)
        {
            try
            {
                return shiftRepositories.GetAllShiftsByDate(date);
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }

        public IList<Shift> GetAllShiftsByEmployeesWithinTime(string employeeId, DateTime startDate, DateTime endDate)
        {
            try
            {
                return shiftRepositories.GetAllShiftsByEmployeesWithinTime(employeeId, startDate, endDate)
                    .Select(x => new Shift()
                    {
                        ShiftId = x.ShiftId,
                        EmployeeId = x.EmployeeId,
                        StartTime = x.StartTime,
                        EndTime = x.EndTime
                    }).ToList();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public bool AddShift(Shift shift)
        {
            try
            {
                return shiftRepositories.AddShift(new Repositories.Context.Entities.Shift()
                {
                    ShiftId = Guid.NewGuid().ToString(),
                    EmployeeId = shift.EmployeeId,
                    StartTime = shift.StartTime,
                    EndTime = shift.EndTime
                });
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public bool DeleteShift(string shiftId)
        {
            try
            {
                return shiftRepositories.DeleteShift(shiftId);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public bool EditShift(Shift shift)
        {
            try
            {
                return shiftRepositories.EditShift(new Repositories.Context.Entities.Shift()
                {
                    ShiftId = shift.ShiftId,
                    EmployeeId = shift.EmployeeId,
                    StartTime = shift.StartTime,
                    EndTime = shift.EndTime
                });
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }
    }
}
