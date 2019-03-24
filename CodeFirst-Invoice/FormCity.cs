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
    public partial class FormCity : Form
    {
        public FormCity()
        {
            InitializeComponent();
        }

        InvoiceContext db = DbSingleton.GetInstance();
        int cityID;

        private void btnAdd_Click(object sender, EventArgs e)
        {
            AddCity();
            FillDataGrid();
        }

        private void AddCity()
        {
            City c = new City();
            c.Description = txtCity.Text;
            db.Cities.Add(c);
            db.SaveChanges();
        }

        private void FillDataGrid()
        {
            dataGrid.DataSource = db.Cities
                .Select(c=> new {
                    c.CityID,
                    c.Description
                }).ToList();
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            UpdateCity();
            FillDataGrid();
            dataGrid.ClearSelection();
        }


        private void UpdateCity()
        {
            City c = db.Cities.Find(cityID);
            c.Description = txtCity.Text;
            db.SaveChanges();
        }

        private void dataGrid_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            cityID = Convert.ToInt32(dataGrid.CurrentRow.Cells["CityID"].Value);
            txtCity.Text = db.Cities.Find(cityID).Description;
        }

        private void FormCity_Load(object sender, EventArgs e)
        {
            FillDataGrid();
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (dataGrid.SelectedRows.Count > 1)
            {
                MessageBox.Show("Please use MULTI DELETE");
            }
            else
            {
                db.Cities.Remove(db.Cities.Find(cityID));
                db.SaveChanges();
                FillDataGrid();
            }
        }

        private void btnMultiDelete_Click(object sender, EventArgs e)
        {
            if (dataGrid.SelectedRows.Count > 1)
            {
                City c;
                foreach (DataGridViewRow item in dataGrid.SelectedRows)
                {
                    c = db.Cities.Find(item.Cells["CityID"].Value);
                    //MessageBox.Show("" + c.Description);
                    db.Cities.Remove(c);
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
