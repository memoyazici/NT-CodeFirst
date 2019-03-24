using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodeFirst_Invoice.DAL
{
    public class Product
    {
        public int ProductID { get; set; }
        public string ProductName { get; set; }
        public string ProductCode { get; set; }
        public int UnitID { get; set; }
        public decimal UnitPrice { get; set; }

        public Unit unit { get; set; }
    }
}
