using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace iScheduling.Helper
{
    public static class StringHelpers
    {
        public static bool IsNullOrEmpty(this string s)
        {
            return s == null || s.Equals(string.Empty);
        }
    }
}