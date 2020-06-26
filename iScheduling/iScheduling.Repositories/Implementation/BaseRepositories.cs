using iScheduling.Repositories.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace iScheduling.Repositories.Implementation
{
    public abstract class BaseRepositories
    {
        protected iSchedulingContext Entities { get; set; }
        public BaseRepositories(iSchedulingContext _context)
        {
            Entities = _context;
        }
    }
}