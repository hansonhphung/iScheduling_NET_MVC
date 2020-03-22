using iScheduling.DTO.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace iScheduling.Models.Shift
{
    public class ShiftCalendarViewVM : BaseViewModel<IEnumerable<EmployeeShift>>
    {
        public ShiftCalendarViewVM() :base(false, string.Empty, null) { }


        public ShiftCalendarViewVM(bool isSuccess, string message, IList<EmployeeShift> lstEmployeeShift) : base(isSuccess, message, lstEmployeeShift)
        {
        }
    }
}