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
    public partial class FormProduct : Form
    {
        public FormProduct()
        {
            InitializeComponent();
        }

        InvoiceContext db = DbSingleton.GetInstance();
        Product pToEdit;

        private void btnAdd_Click(object sender, EventArgs e)
        {
            AddProduct();
            FillDataGrid();
        }

        private void AddProduct()
        {
            Product p = new Product();
            p.ProductName = txtProductName.Text;
            p.ProductCode = txtProductCode.Text;
            p.UnitPrice = Convert.ToDecimal(txtUnitPrice.Text);
            p.UnitID = (int)cmbUnit.SelectedValue;
            db.Products.Add(p);
            db.SaveChanges();

        }

        private void UpdateProduct()
        {
            pToEdit.ProductName = txtProductName.Text;
            pToEdit.ProductCode = txtProductCode.Text;
            pToEdit.UnitPrice = Convert.ToDecimal(txtUnitPrice.Text);
            pToEdit.UnitID = (int)cmbUnit.SelectedValue;
            db.SaveChanges();
        }

        private void FillDataGrid()
        {
            dataGrid.DataSource = db.Products
                .Select(p=> new {
                    p.ProductID,
                    p.ProductCode,
                    p.ProductName,
                    p.UnitPrice,
                    p.unit.UnitName
                }).ToList();
        }

        private void FormProduct_Load(object sender, EventArgs e)
        {
            FillDataGrid();
            FillComboUnit();
        }

        private void FillComboUnit()
        {
            cmbUnit.DisplayMember = "UnitName";
            cmbUnit.ValueMember = "UnitID";
            cmbUnit.DataSource = db.Units
                .Select(s=> new {
                    s.UnitID,
                    s.UnitName
                }).ToList();
        }

        int productID;
        private void dataGrid_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            productID = Convert.ToInt32(dataGrid.CurrentRow.Cells["ProductID"].Value);
            pToEdit = db.Products.Find(productID);
            txtProductName.Text = pToEdit.ProductName;
            txtProductCode.Text = pToEdit.ProductCode;
            txtUnitPrice.Text = pToEdit.UnitPrice.ToString();
            cmbUnit.SelectedValue = pToEdit.UnitID;
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            UpdateProduct();
            FillDataGrid();
            dataGrid.ClearSelection();
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (dataGrid.SelectedRows.Count > 1)
            {
                MessageBox.Show("Please use MULTI DELETE");
            }
            else
            {
                db.Products.Remove(pToEdit);
                db.SaveChanges();
                FillDataGrid();
            }
        }

        private void btnMultiDelete_Click(object sender, EventArgs e)
        {
            if (dataGrid.SelectedRows.Count > 1)
            {
                Product p;
                foreach (DataGridViewRow item in dataGrid.SelectedRows)
                {
                    p = db.Products.Find(item.Cells["ProductID"].Value);
                    //MessageBox.Show("" + c.Description);
                    db.Products.Remove(p);
                    db.SaveChanges();
                }

            }
            else
            {
                MessageBox.Show("Please select Multiple Items..");
            }
            FillDataGrid();
        }
    }
}
