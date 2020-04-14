﻿using iScheduling.DTO.Models;
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
    public class ShiftRepositories : BaseRepositories, IShiftRepositories 
    {
        public ShiftRepositories(iSchedulingContext context) : base(context) { }

        public EmployeeShift GetShiftById(string shiftId)
        {
            try
            {
                return Entities.Shifts
                    .Join(Entities.Employees, s => s.EmployeeId, e => e.EmployeeId,
                                   (s, e) => new {
                                       s.ShiftId,
                                       e.EmployeeId,
                                       FullName = e.FirstName + " " + e.LastName,
                                       s.StartTime,
                                       s.EndTime,
                                       s.IsDeleted,
                                       s.IsCancelled
                                    })
                    .Where(x => x.ShiftId == shiftId && x.IsCancelled == false && x.IsDeleted == false)
                    .Select(x => new EmployeeShift()
                    {
                        ShiftId = x.ShiftId,
                        EmployeeId = x.EmployeeId,
                        FullName = x.FullName,
                        StartTime = x.StartTime,
                        EndTime = x.EndTime
                    }).FirstOrDefault();
            }catch(Exception ex)
            {
                throw ex;
            }
        }

        public IList<EmployeeShift> GetAllShiftsByDate(DateTime date)
        {
            try
            {
                DateTime nextDate = date.AddDays(1);
                return Entities.Shifts
                               .Join(Entities.Employees, s => s.EmployeeId, e => e.EmployeeId,
                                   (s, e) => new  {
                                       s.ShiftId,
                                       e.EmployeeId,
                                       FullName = e.FirstName + " " + e.LastName,
                                       s.StartTime,
                                       s.EndTime,
                                       s.IsDeleted,
                                       s.IsCancelled
                                   })
                               .Where(x => x.StartTime >= date && x.EndTime <= nextDate
                                          && x.IsDeleted == false
                                          && x.IsCancelled == false)
                               .Select(x => new EmployeeShift() {
                                   ShiftId = x.ShiftId,
                                   EmployeeId = x.EmployeeId,
                                   FullName = x.FullName,
                                   StartTime = x.StartTime,
                                   EndTime = x.EndTime
                               })
                               .OrderBy(x => x.StartTime)
                               .ToList();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public IList<Context.Entities.Shift> GetAllShiftsByEmployeesWithinTime(string employeeId, DateTime startDate, DateTime endDate)
        {
            try
            {
                DateTime nextEndDate = endDate.AddDays(1);
                return Entities.Shifts.Where(x => x.EmployeeId == employeeId 
                                          && x.StartTime >= startDate 
                                          && x.EndTime <= nextEndDate
                                          && x.IsDeleted == false
                                          && x.IsCancelled == false)
                                      .OrderBy(s => s.StartTime).ToList();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public bool AddShift(Context.Entities.Shift shift)
        {
            try
            {
                Entities.Shifts.Add(shift);
                Entities.SaveChanges();

                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public bool EditShift(Context.Entities.Shift shift)
        {
            try
            {
                var shiftEntity = Entities.Shifts.Where(x => x.ShiftId == shift.ShiftId 
                                                     && x.IsDeleted == false 
                                                     && x.IsCancelled == false).FirstOrDefault();

                if (shiftEntity == null)
                {
                    return false;
                }

                shiftEntity.EmployeeId = shift.EmployeeId;
                shiftEntity.StartTime = shift.StartTime;
                shiftEntity.EndTime = shift.EndTime;

                Entities.SaveChanges();

                return true;
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
                var shiftEntity = Entities.Shifts.Where(x => x.ShiftId == shiftId 
                                                     && x.IsDeleted == false
                                                     && x.IsCancelled == false).FirstOrDefault();

                if (shiftEntity == null)
                {
                    return false;
                }

                shiftEntity.IsDeleted = true;

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
