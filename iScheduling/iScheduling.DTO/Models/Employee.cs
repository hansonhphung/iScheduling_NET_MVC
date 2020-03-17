using iScheduling.DTO.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace iScheduling.DTO.Models
{
    public class Employee
    {
        public string EmployeeId { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string AccessToken { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string FullName {
            get {
                return string.Format("{0} {1}", FirstName, LastName);
            }
        }
        public string Address { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public Role Position { get; set; }
    }
}
