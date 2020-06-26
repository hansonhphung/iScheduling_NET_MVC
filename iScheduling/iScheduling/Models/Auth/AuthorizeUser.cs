using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace iScheduling.Models.Auth
{
    public class AuthorizeUser
    {
        public string Username { get; set; }
        public string Password { get; set; }

        public AuthorizeUser() { }

        public AuthorizeUser(string username, string password)
        {
            Username = username;
            Password = password;
        }
    }
}