using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodeFirst_Invoice.DAL
{
    public class City
    {
        public City()
        {
            this.counties = new HashSet<County>();
        }

        public int CityID { get; set; }
        public string Description { get; set; }

        public ICollection<County> counties { get; set; }

    }
}
