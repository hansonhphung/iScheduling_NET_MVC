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

        public BaseViewModel(bool isSuccess, string message, T data)
        {
            IsSuccess = isSuccess;
            Message = message;
            Data = data;
        }
    }
}