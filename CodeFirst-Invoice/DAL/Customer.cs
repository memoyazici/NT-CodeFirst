using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodeFirst_Invoice.DAL
{
    public class Customer
    {
        public Customer()
        {
            this.invoiceHeaders = new HashSet<InvoiceHeader>();
        }

        public int CustomerID { get; set; }
        public string CompanyName { get; set; }
        public int CountyID { get; set; }
        public string Address { get; set; }

        public County county { get; set; }
        public ICollection<InvoiceHeader> invoiceHeaders { get; set; }
    }
}
