using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodeFirst_Invoice.DAL
{
    public class Unit
    {
        public Unit()
        {
            this.products = new HashSet<Product>();
        }

        public int UnitID { get; set; }
        public string UnitName { get; set; }

        public ICollection<Product> products { get; set; }
    }
}
