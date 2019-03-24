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
    public partial class FormCustomer : Form
    {
        public FormCustomer()
        {
            InitializeComponent();
            
        }

        InvoiceContext db = DbSingleton.GetInstance();
        int customerID,countyID,cityID;
        


        private void btnAdd_Click(object sender, EventArgs e)
        {
            AddCustomer();
            FillDataGrid();
        }


        private void FillDataGrid()
        {
            dataGrid.DataSource = db.Customers
                .Select(c => new {
                    c.CustomerID,
                    c.CompanyName,
                    County = c.county.Description,
                    City = c.county.city.Description
                }).ToList();
        }

        private void AddCustomer()
        {
            Customer c = new Customer();
            c.CompanyName = txtCustomerName.Text;
            c.CountyID = (int)cmbCounty.SelectedValue;
            //c.county.CityID = (int)cmbCity.SelectedValue;
            c.Address = txtAddress.Text;
            db.Customers.Add(c);
            db.SaveChanges();
        }

        private void FillComboCity()
        {
            cmbCity.DisplayMember = "Description";
            cmbCity.ValueMember = "CityID";
            cmbCity.DataSource = db.Cities
                .Select(x=>new {
                    x.CityID,
                    x.Description
                }).ToList();
            
            
            //cmbCounty.DisplayMember = "";
            //cmbCounty.ValueMember = "";
            //cmbCounty.DataSource = db.Counties.ToList();
        }

        private void FormCustomer_Load(object sender, EventArgs e)
        {
            FillComboCity();
            FillDataGrid();
        }

        

        private void cmbCity_SelectedValueChanged(object sender, EventArgs e)
        {
           //if ((int)cmbCity.SelectedIndex != -1 && onUpdate == true)
           // {
           //     cmbCounty.SelectedValue = db.Customers.Find(customerID).CountyID;
           // }

        }



        private void FillCountyCombo()
        {
            cmbCounty.DisplayMember = "Description";
            cmbCounty.ValueMember = "CountyID";
            cmbCounty.DataSource = db.Counties.Where(x=>x.CityID==cityID).ToList();
        }

        

        private void cmbCity_SelectedIndexChanged(object sender, EventArgs e)
        {
            cmbCounty.DataSource = null;
            if ((int)cmbCity.SelectedIndex != -1)
            {
                cityID = (int)cmbCity.SelectedValue;
                FillCountyCombo();
            }
            

        }

        private void dataGrid_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            customerID = Convert.ToInt32(dataGrid.CurrentRow.Cells["CustomerID"].Value);
            countyID = db.Customers.Find(customerID).CountyID;

            txtCustomerName.Text = db.Customers.Find(customerID).CompanyName;
            txtAddress.Text = db.Customers.Find(customerID).Address;
            cmbCity.SelectedValue = db.Counties.Find(countyID).CityID;
            cmbCounty.SelectedValue = db.Customers.Find(customerID).CountyID;
            //MessageBox.Show(countyID.ToString() +  " "+ cmbCounty.SelectedValue.ToString());
            
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            UpdateCustomer();
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
                db.Customers.Remove(db.Customers.Find(customerID));
                db.SaveChanges();
                FillDataGrid();
            }
        }

        private void btnMultiDelete_Click(object sender, EventArgs e)
        {
            if (dataGrid.SelectedRows.Count > 1)
            {
                Customer c;
                foreach (DataGridViewRow item in dataGrid.SelectedRows)
                {
                    c = db.Customers.Find(item.Cells["CustomerID"].Value);
                    //MessageBox.Show("" + c.Description);
                    db.Customers.Remove(c);
                    db.SaveChanges();
                }
            }
            else
            {
                MessageBox.Show("Please select Multiple Items..");
            }
            FillDataGrid();
        }

        private void UpdateCustomer()
        {
            Customer c = db.Customers.Find(customerID);
            c.CompanyName = txtCustomerName.Text;
            c.Address = txtAddress.Text;
            c.CountyID = (int)cmbCounty.SelectedValue;
            //c.county.CityID = (int)cmbCity.SelectedValue;
            db.SaveChanges();
        }
    }
}
