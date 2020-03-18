using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace iScheduling.DTO.Enums
{
    public enum Role
    {
        [Description("Store Manager")]
        STORE_MANAGER,
        [Description("Front Manager")]
        FRONT_TEAM_MANAGER,
        [Description("Production Manager")]
        PRODUCTION_TEAM_MANAGER,
        [Description("Front Team Member")]
        FRONT_TEAM_MEMBER,
        [Description("Production Team Member")]
        PRODUCTION_TEAM_MEMBER
    }
}
