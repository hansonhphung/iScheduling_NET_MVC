using iScheduling.Repositories.Context.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace iScheduling.Context.Entities
{
    public class Employee
    {
        [Key]
        public string EmployeeId { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string AccessToken { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Address { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public string Position { get; set; }
        public bool IsDeleted { get; set; }
        public DateTime CreatedAt { get; set; }
        [ForeignKey("EmployeeId")] 
        public virtual ICollection<Shift> Shifts { get; set; }
        [ForeignKey("RequestEmployeeId")]
        public virtual ICollection<VacationRequest> RequestedVacationRequests { get; set; }
        [ForeignKey("RequestEmployeeId")]
        public virtual ICollection<DayOffRequest> RequestedDayOffRequests { get; set; }
        [ForeignKey("ResponseManagerId")]
        public virtual ICollection<VacationRequest> ResponseVacationRequests { get; set; }
        [ForeignKey("ResponseManagerId")]
        public virtual ICollection<DayOffRequest> ResponseDayOffRequests { get; set; }

    }
}