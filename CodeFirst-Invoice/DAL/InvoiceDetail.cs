using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodeFirst_Invoice.DAL
{
    public class InvoiceDetail
    {
        [Key]
        [Column(Order=0)]
        public int InvoiceID { get; set; }
        [Key]
        [Column(Order = 1)]
        public int ProductID { get; set; }
        public int Quantity { get => quantity; set => quantity = value; }
        public int VATAmount { get => vATAmount; set => vATAmount = value; }
        public decimal UnitPrice { get => unitPrice; set => unitPrice = value; }
        [NotMapped]
        public decimal AmountWithVAT
        { get => amountWithVAT = this.quantity * this.unitPrice * (1.0m + ((decimal)this.vATAmount/100));
            
        }
        
        public string Description { get; set; }

        private decimal amountWithVAT;
        private int quantity;
        private int vATAmount;
        private decimal unitPrice;

        public InvoiceHeader invoiceHeader { get; set; }
        public Product product { get; set; }

    }
}
