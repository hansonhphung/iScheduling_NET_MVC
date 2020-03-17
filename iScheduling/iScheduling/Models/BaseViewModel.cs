using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace iScheduling.Models
{
    public class BaseViewModel<T>
    {
        public bool IsSuccess { get; set; }
        public string Message { get; set; }
        public T Data { get; set; }
    }
}