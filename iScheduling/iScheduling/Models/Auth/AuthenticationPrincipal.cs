using iScheduling.DTO.Enums;
using iScheduling.Helper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Web;

namespace iScheduling.Models.Auth
{
    public class AuthenticationPrincipal : IPrincipal
    {
        private string MANAGEMENT_ROLE = string.Format("{0}, {1}, {2}",
                            EnumHelpers.GetDescription(Role.STORE_MANAGER),
                            EnumHelpers.GetDescription(Role.PRODUCTION_TEAM_MANAGER),
                            EnumHelpers.GetDescription(Role.FRONT_TEAM_MANAGER));

        private string MEMBER_ROLE = string.Format("{0}, {1}",
                            EnumHelpers.GetDescription(Role.FRONT_TEAM_MEMBER),
                            EnumHelpers.GetDescription(Role.PRODUCTION_TEAM_MEMBER));
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

        public bool IsInManagementRole()
        {
            return MANAGEMENT_ROLE.Contains(Position);
        }

        public bool IsInMemberRole()
        {
            return MEMBER_ROLE.Contains(Position);
        }

        public AuthenticationPrincipal(string employeeId)
        {
            Identity = new GenericIdentity(employeeId);
        }
    }
}