using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodeFirst_Invoice.DAL
{
    public class InvoiceHeader
    {
        public InvoiceHeader()
        {
            this.invoiceDetails = new HashSet<InvoiceDetail>();
        }
        [Key]
        public int InvoiceID { get; set; }
        public int CustomerID { get; set; }
        public DateTime InvoiceDate { get; set; }
        public int DeliveryNote { get; set; }
        public DateTime PaymentDate { get; set; }
        public int TotalAmount { get; set; }

        public Customer customer { get; set; }
        public ICollection<InvoiceDetail> invoiceDetails { get; set; }
    }
}
