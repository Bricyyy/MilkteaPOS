﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PointOfSalesSystem
{
    public class Category
    {
        public int CategoryId { get; set; }
        public string CategoryName { get; set; }

        public override string ToString()
        {
            return CategoryName;
        }
    }
}
