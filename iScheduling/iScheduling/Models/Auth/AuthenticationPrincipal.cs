using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Web;

namespace iScheduling.Models.Auth
{
    public class AuthenticationPrincipal : IPrincipal
    {
        public string EmployeeId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string UserName { get; set; }
        public string FullName
        {
            get
            {
                return string.Format("{0} {1}", FirstName, LastName);
            }
        }

        public string Position { get; set; }

        public IIdentity Identity { get; private set; }

        public bool IsInRole(string role)
        {
            return role.Contains(Position);
        }

        public AuthenticationPrincipal(string employeeId)
        {
            Identity = new GenericIdentity(employeeId);
        }
    }
}