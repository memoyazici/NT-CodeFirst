using CodeFirst_Invoice.DAL;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CodeFirst_Invoice
{
    public partial class FormEditInvoice : Form
    {
        public FormEditInvoice()
        {
            InitializeComponent();
        }

        InvoiceContext db = DbSingleton.GetInstance();
        InvoiceDetail details;
        int invoiceID,productID;

        private void FormEditInvoice_Load(object sender, EventArgs e)
        {
            dataGrid.DataSource = db.InvoiceDetails
                .Select(x => new
                {
                    x.InvoiceID,
                    x.ProductID,
                    x.Quantity,
                    x.UnitPrice,
                    x.VATAmount,
                    x.Description
                }).ToList();

            FillCombos();

        }

        private void dataGrid_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            invoiceID = Convert.ToInt32(dataGrid.CurrentRow.Cells["InvoiceID"].Value);
            productID = Convert.ToInt32(dataGrid.CurrentRow.Cells["ProductID"].Value);

            details = db.InvoiceDetails.Find(invoiceID,productID);

            txtQuantity.Text = details.Quantity.ToString();
            txtVAT.Text = details.VATAmount.ToString();
            txtDeliveryNote.Text = db.InvoiceHeaders.Find(invoiceID).DeliveryNote.ToString();
            txtDetailDescription.Text = details.Description;
            cmbCustomer.SelectedValue = db.InvoiceHeaders.Find(invoiceID).CustomerID;
            cmbProduct.SelectedValue = details.ProductID;
        }

        private void btnViewInvoice_Click(object sender, EventArgs e)
        {
            invoiceID = Convert.ToInt32(txtInvoiceID.Text);
            productID = Convert.ToInt32(cmbProduct.SelectedValue);
            dataGrid.DataSource = db.InvoiceDetails
                .Where(x=>x.InvoiceID == invoiceID)
                .Select(x=> new
                {
                    x.InvoiceID,
                    x.ProductID,
                    x.Quantity,
                    x.UnitPrice,
                    x.VATAmount,
                    
                    x.Description
                }).ToList();
            
            
        }


        private void FillCombos()
        {
            cmbCustomer.DisplayMember = "CompanyName";
            cmbCustomer.ValueMember = "CustomerID";
            cmbCustomer.DataSource = db.Customers
                .Select(x => new {
                    x.CustomerID,
                    x.CompanyName,
                }).ToList();

            cmbProduct.DisplayMember = "ProductName";
            cmbProduct.ValueMember = "ProductID";
            cmbProduct.DataSource = db.Products
                .Select(x => new {
                    x.ProductID,
                    x.ProductName
                }).ToList();
        }


    }
}
