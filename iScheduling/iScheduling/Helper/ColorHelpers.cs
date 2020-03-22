using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace iScheduling.Helper
{
    public class ColorHelpers
    {
        public static string RandomColor()
        {
            return string.Format("#{0}", Guid.NewGuid().ToString().Substring(0, 6));
        }
    }
}