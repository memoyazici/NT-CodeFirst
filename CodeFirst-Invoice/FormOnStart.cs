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
    public partial class FormOnStart : Form
    {
        public FormOnStart()
        {
            InitializeComponent();
        }

        private void customerDefinitonsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FormCustomer f = new FormCustomer();
            f.Show();
        }

        private void productDefinitonsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FormProduct f = new FormProduct();
            f.Show();
        }

        private void unitDefinitionsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FormUnit f = new FormUnit();
            f.Show();
        }

        private void cityDefinitionsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FormCity f = new FormCity();
            f.Show();
        }

        private void countyDefinitonsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FormCounty f = new FormCounty();
            f.Show();
        }

        private void createNewInvoiceToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FormCreateNewInvoice f = new FormCreateNewInvoice();
            f.Show();
        }

        private void ınvoiceViewQueryEditToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FormEditInvoice f = new FormEditInvoice();
            f.Show();
        }
    }
}
