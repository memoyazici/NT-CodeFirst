using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using CodeFirst_Invoice.DAL;

namespace CodeFirst_Invoice
{
    public partial class FormCreateNewInvoice : Form
    {
        public FormCreateNewInvoice()
        {
            InitializeComponent();
        }

        InvoiceContext db = DbSingleton.GetInstance();
        Customer c;
        Product p;


        private void FormCreateNewInvoice_Load(object sender, EventArgs e)
        {
            FillCombos();
            dtpInvoiceDate.MinDate = DateTime.Now;
            dtpPaymentDate.MinDate = DateTime.Now;
        }

        private void FillCombos()
        {
            cmbCustomer.DisplayMember = "CompanyName";
            cmbCustomer.ValueMember = "CustomerID";
            cmbCustomer.DataSource =db.Customers
                .Select(x=> new {
                    x.CustomerID,
                    x.CompanyName,
                }).ToList();

            cmbProduct.DisplayMember = "ProductName";
            cmbProduct.ValueMember = "ProductID";
            cmbProduct.DataSource = db.Products
                .Select(x=> new {
                    x.ProductID,
                    x.ProductName
                }).ToList();
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            InvoiceHeader i = new InvoiceHeader();
            i.CustomerID = (int)cmbCustomer.SelectedValue;
            i.DeliveryNote = Convert.ToInt32(txtDeliveryNote.Text);
            i.InvoiceDate = dtpInvoiceDate.Value;
            i.PaymentDate = dtpPaymentDate.Value;
            i.TotalAmount = 0;
            db.InvoiceHeaders.Add(i);
            db.SaveChanges();
            txtInvoiceID.Text = i.InvoiceID.ToString();
        }

        private void btnAddDetail_Click(object sender, EventArgs e)
        {
            InvoiceDetail i = new InvoiceDetail();
            i.InvoiceID = Convert.ToInt32(txtInvoiceID.Text);
            i.ProductID = (int)cmbProduct.SelectedValue;
            i.Quantity = Convert.ToInt32(txtQuantity.Text);
            i.VATAmount = Convert.ToInt32(txtVAT.Text);
            
            i.Description = txtDetailDescription.Text;
            i.UnitPrice = db.Products.Find((int)cmbProduct.SelectedValue).UnitPrice;
            db.InvoiceDetails.Add(i);
            db.SaveChanges();
        }

        private void txtInvoiceID_TextChanged(object sender, EventArgs e)
        {
            if (String.IsNullOrEmpty(txtInvoiceID.Text))
            {
                btnAddDetail.Enabled = false;
            }
            else
            {
                btnAddDetail.Enabled = true;
            }
        }
    }
}
