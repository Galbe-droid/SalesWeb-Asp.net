﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SalesWebMVC.Models
{
    public class SellerFromViewModel
    {
        public Sellers Seller { get; set; }
        public ICollection<Department> Departments { get; set; }
    }
}
