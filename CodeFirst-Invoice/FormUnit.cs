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
    public partial class FormUnit : Form
    {
        public FormUnit()
        {
            InitializeComponent();
        }

        InvoiceContext db = DbSingleton.GetInstance();
        int unitID;

        private void btnAdd_Click(object sender, EventArgs e)
        {
            AddUnit();
            FillDataGrid();
        }

        private void AddUnit()
        {
            Unit u = new Unit();
            u.UnitName = txtUnit.Text;
            db.Units.Add(u);
            db.SaveChanges();
        }

        private void FillDataGrid()
        {
            dataGrid.DataSource = db.Units
                .Select(u=> new {
                    u.UnitID,
                    u.UnitName
                }).ToList();
        }

        private void FormUnit_Load(object sender, EventArgs e)
        {
            FillDataGrid();
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            UpdateUnit();
            FillDataGrid();
            dataGrid.ClearSelection();
        }

        private void UpdateUnit()
        {
            Unit u = db.Units.Find(unitID);
            u.UnitName = txtUnit.Text;
            db.SaveChanges();
        }

        private void dataGrid_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            unitID = Convert.ToInt32(dataGrid.CurrentRow.Cells["UnitID"].Value);
            txtUnit.Text = db.Units.Find(unitID).UnitName;
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (dataGrid.SelectedRows.Count > 1)
            {
                MessageBox.Show("Please use MULTI DELETE");
            }
            else
            {
                db.Units.Remove(db.Units.Find(unitID));
                db.SaveChanges();
                FillDataGrid();
            }
        }

        private void btnMultiDelete_Click(object sender, EventArgs e)
        {
            if (dataGrid.SelectedRows.Count > 1)
            {
                Unit u;
                foreach (DataGridViewRow item in dataGrid.SelectedRows)
                {
                    u = db.Units.Find(item.Cells["UnitID"].Value);
                    //MessageBox.Show("" + c.Description);
                    db.Units.Remove(u);
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
