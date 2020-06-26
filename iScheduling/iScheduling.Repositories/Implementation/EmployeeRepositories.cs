using iScheduling.Context.Entities;
using iScheduling.DTO.Enums;
using iScheduling.Repositories.Context;
using iScheduling.Repositories.Implementation;
using iScheduling.Repositories.Interface;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace iScheduling.Repositories.Implementation
{
    public class EmployeeRepositories : BaseRepositories, IEmployeeRepositories
    {
        public EmployeeRepositories(iSchedulingContext context) : base(context)  { } 

        public Employee Login(string username, string password)
        {
            try
            {
                var employee = Entities.Employees.Where(x => x.Username.Equals(username) && x.Password.Equals(password)).FirstOrDefault();

                return employee;
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }

        public IList<Employee> GetAllEmployees()
        {
            try
            {
                var lstEmployees = Entities.Employees.Where(x => x.IsDeleted == false).ToList();
                return lstEmployees;
            }
            catch(Exception ex)
            {
                throw ex;
            }   
        }

        public bool IsUsernameExists(string username)
        {
            try
            {
                var user = Entities.Employees.Where(x => x.Username.Equals(username)).FirstOrDefault();

                return (user != null);
            }catch(Exception ex)
            {
                throw ex;
            }
        }

        public IList<Employee> GetAllEmployeeToAssignShift(DateTime dateOfShift)
        {
            try
            {
                var approved = EnumHelpers.GetDescription(VacationRequestStatus.APPROVED);
                var onVacationEmployee = Entities.Employees.Join(Entities.VacationRequests,
                                    e => e.EmployeeId, vr => vr.RequestEmployeeId,
                                    (e, vr) => new
                                    {
                                        e.EmployeeId,
                                        vr.StartDate,
                                        vr.EndDate,
                                        vr.Status
                                    })
                                    .Where(x => x.Status.Equals(approved))
                                    .Select(x => x.EmployeeId)
                                    .Distinct()
                                    .ToList();

                return Entities.Employees.Where(x => !onVacationEmployee.Contains(x.EmployeeId)
                                                             && x.IsDeleted == false).ToList();
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }

        public bool AddEmployee(Employee emp)
        {
            try
            {
                Entities.Employees.Add(emp);
                Entities.SaveChanges();
                return true;
            }catch(Exception ex)
            {
                throw ex;
            }
        }

        public Employee GetEmployeeById(string empId)
        {
            try
            {
                return Entities.Employees.Where(x => x.EmployeeId == empId && x.IsDeleted == false).FirstOrDefault();
            }catch(Exception ex)
            {
                throw ex;
            }
        }

        public bool UpdateEmployee(Employee emp)
        {
            try
            {
                var empEntity = Entities.Employees.Where(x => x.EmployeeId == emp.EmployeeId && x.IsDeleted == false).FirstOrDefault();

                if (empEntity == null) {
                    return false;
                }

                empEntity.FirstName = emp.FirstName;
                empEntity.LastName = emp.LastName;
                empEntity.Username = emp.Username;
                empEntity.Email = emp.Email;
                empEntity.Address = emp.Address;
                empEntity.Phone = emp.Phone;
                empEntity.Position = emp.Position;

                Entities.SaveChanges();

                return true;
            }catch(Exception ex)
            {
                throw ex;
            }
        }

        public bool DeleteEmployee(string empId) {
            try
            {
                var empEntity = Entities.Employees.Where(x => x.EmployeeId == empId && x.IsDeleted == false).FirstOrDefault();

                if (empEntity == null)
                {
                    return false;
                }

                empEntity.IsDeleted = true;

                Entities.SaveChanges();

                return true;
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }
    }
}