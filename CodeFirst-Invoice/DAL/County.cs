﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodeFirst_Invoice.DAL
{
    public class County
    {
        public int CountyID { get; set; }
        public string Description { get; set; }
        public int CityID { get; set; }

        public City city { get; set; }
    }
}
