using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SalesWebMVC.Services.Exception
{
    public class DbConcurrenceException : ApplicationException
    {
        public DbConcurrenceException(string message) : base(message)
        {

        }
    }
}
