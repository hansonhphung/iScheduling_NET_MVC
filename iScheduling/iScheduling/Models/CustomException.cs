using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace iScheduling.Models
{
    public class CustomException : Exception
    {
        public CustomException(string errorMessage) : base(errorMessage) { }

        public CustomException(Exception ex) : base("Error happened", ex) { }

        public CustomException(string errorMessage, Exception innerException) : base(errorMessage, innerException) { }
    }
}