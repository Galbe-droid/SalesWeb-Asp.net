using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SalesWebMVC.Models.Enum
{
    public enum SalesStatus : int
    {
        PENDING = 0,
        BILLED = 1,
        CANCELED = 2
    }
}
