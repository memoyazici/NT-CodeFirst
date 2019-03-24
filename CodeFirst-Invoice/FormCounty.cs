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
    public partial class FormCounty : Form
    {
        public FormCounty()
        {
            InitializeComponent();
            
        }

        InvoiceContext db = DbSingleton.GetInstance();
        int countyID,cityID;

        private void btnAdd_Click(object sender, EventArgs e)
        {
            AddCounty();
            FilteredDataGrid();
        }

        private void AddCounty()
        {
            County c = new County();
            c.Description = txtCounty.Text;
            c.CityID = (int)cmbCity.SelectedValue;
            db.Counties.Add(c);
            db.SaveChanges();
        }

        private void FillDataGrid()
        {
            dataGrid.DataSource = db.Counties
                .Select(c => new {
                    c.CountyID,
                    County = c.Description,
                    City = c.city.Description
                }).ToList();
        }

        private void FillCombo()
        {
            cmbCity.DisplayMember = "Description";
            cmbCity.ValueMember = "CityID";
            cmbCity.DataSource = db.Cities.ToList();
        }

        private void FormCounty_Load(object sender, EventArgs e)
        {
            cmbCity.SelectedIndex = -1;
            FillCombo();
            FilteredDataGrid();
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            UpdateCounty();
            FilteredDataGrid();
            dataGrid.ClearSelection();
        }

        private void UpdateCounty()
        {
            County c = db.Counties.Find(countyID);
            c.Description = txtCounty.Text;
            c.CityID = (int)cmbCity.SelectedValue;
            db.SaveChanges();
        }

        private void dataGrid_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            
            countyID = Convert.ToInt32(dataGrid.CurrentRow.Cells["CountyID"].Value);
            txtCounty.Text = db.Counties.Find(countyID).Description;
            cmbCity.SelectedValue = db.Counties.Find(countyID).city.CityID;
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (dataGrid.SelectedRows.Count > 1)
            {
                MessageBox.Show("Please use MULTI DELETE");
            }
            else
            {
                db.Counties.Remove(db.Counties.Find(countyID));
                db.SaveChanges();
                FilteredDataGrid();
            }
           
        }

        private void btnMultiDelete_Click(object sender, EventArgs e)
        {
            if (dataGrid.SelectedRows.Count > 1)
            {
                County c;
                foreach (DataGridViewRow item in dataGrid.SelectedRows)
                {
                    c = db.Counties.Find(item.Cells["CountyID"].Value);
                    //MessageBox.Show("" + c.Description);
                    db.Counties.Remove(c);
                    db.SaveChanges();
                }

            }
            else
            {
                MessageBox.Show("Please select Multiple Items..");
            }
            FillDataGrid();
        }

        private void cmbCity_SelectedIndexChanged(object sender, EventArgs e)
        {
            cityID = (int)cmbCity.SelectedValue;
            dataGrid.DataSource = db.Counties
                .Where(c=>c.CityID == cityID)
                .Select(c => new {
                    c.CountyID,
                    County = c.Description,
                    City = c.city.Description
                }).ToList();

            FilteredDataGrid();
        }

        private void FilteredDataGrid()
        {
            dataGrid.DataSource = db.Counties
                .Where(c=>c.CityID == (int)cmbCity.SelectedValue)
                .Select(c=> new
                {
                    c.CountyID,
                    County = c.Description,
                    City = c.city.Description
                }).ToList();

        }

    }
}
