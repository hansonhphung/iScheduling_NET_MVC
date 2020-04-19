using iScheduling.Context;
using iScheduling.DTO;
using iScheduling.DTO.Enums;
using iScheduling.DTO.Models;
using iScheduling.Repositories.Interface;
using iScheduling.Services.Implementation;
using iScheduling.Services.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace iScheduling.Implementation.Services
{
    public class EmployeeServices : BaseServices, IEmployeeServices
    {
        private readonly IEmployeeRepositories employeeRepositories;
        public EmployeeServices(IEmployeeRepositories _employeeRepository){
            employeeRepositories = _employeeRepository;
        }

        public Employee Login(string username, string password)
        {
            try
            {
                password = EncryptHelper.Encrypt(password);

                var empEntity = employeeRepositories.Login(username, password);

                if (empEntity == null)
                    return null;

                return new Employee {
                    EmployeeId = empEntity.EmployeeId,
                    FirstName = empEntity.FirstName,
                    LastName = empEntity.LastName,
                    Email = empEntity.Email,
                    Address = empEntity.Address,
                    Phone = empEntity.Phone,
                    Position = empEntity.Position
                };
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
                return employeeRepositories.GetAllEmployees().Select(x => new Employee
                {
                    EmployeeId = x.EmployeeId,
                    FirstName = x.FirstName,
                    LastName = x.LastName,
                    Address = x.Address,
                    Phone = x.Phone,
                    Email = x.Email,
                    Position = x.Position
                }).ToList();
            }catch(Exception ex)
            {
                throw ex;
            }   
        }

        public IList<Employee> GetAllEmployeeOrderByPosition()
        {
            try
            {
                return GetAllEmployees().OrderBy(x => x.Position).ToList();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public IList<Employee> GetAllEmployeeToAssignShift(DateTime dateOfShift)
        {
            try
            {
                return employeeRepositories.GetAllEmployeeToAssignShift(dateOfShift)
                    .Select(x => new Employee
                    {
                        EmployeeId = x.EmployeeId,
                        FirstName = x.FirstName,
                        LastName = x.LastName,
                        Address = x.Address,
                        Phone = x.Phone,
                        Email = x.Email,
                        Position = x.Position
                    }).OrderBy(e => e.Position).ToList();

            }catch(Exception ex)
            {
                throw ex;
            }
        }

        public Employee GetEmployeeById(string empId)
        {
            try
            {
                var empEntity = employeeRepositories.GetEmployeeById(empId);

                return new Employee
                {
                    EmployeeId = empEntity.EmployeeId,
                    Username = empEntity.Username,
                    FirstName = empEntity.FirstName,
                    LastName = empEntity.LastName,
                    Address = empEntity.Address,
                    Phone = empEntity.Phone,
                    Email = empEntity.Email,
                    Position = empEntity.Position
                };
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public bool AddEmployee(Employee emp)
        {
            try
            {
                return employeeRepositories.AddEmployee(new Context.Entities.Employee
                {
                    EmployeeId = Guid.NewGuid().ToString(),
                    FirstName = emp.FirstName,
                    LastName = emp.LastName,
                    Username = emp.Username,
                    // Password will be automatically generated and hashsed.
                    // Will refractor later
                    Password = EncryptHelper.Encrypt("Password"),
                    Position = emp.Position,
                    CreatedAt = DateTime.Now,
                    Email = emp.Email,
                    Phone = emp.Phone,
                    Address = emp.Address,
                    IsDeleted = false
                });
            }catch(Exception ex)
            {
                throw ex;
            }
            
        }

        public bool UpdateEmployee(Employee emp) {
            try
            {
                return employeeRepositories.UpdateEmployee(new Context.Entities.Employee
                {
                    EmployeeId = emp.EmployeeId,
                    FirstName = emp.FirstName,
                    LastName = emp.LastName,
                    Position = emp.Position.ToString(),
                    Email = emp.Email,
                    Phone = emp.Phone,
                    Address = emp.Address
                });
            }
            catch (Exception ex) {
                throw ex;
            }
        }

        public bool DeleteEmployee(string empId)
        {
            try
            {
                return employeeRepositories.DeleteEmployee(empId);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }



    }
}