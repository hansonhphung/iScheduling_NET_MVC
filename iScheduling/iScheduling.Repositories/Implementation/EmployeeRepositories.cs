using iScheduling.Context.Entities;
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