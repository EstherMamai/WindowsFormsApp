using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApp2
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void LoadData()
        {
            try
            {
                DatabaseHelper db = new DatabaseHelper();
                dgvParts.DataSource = db.GetSpareParts();
            }
            catch (Exception ex)
            {
                MessageBox.Show("An error occurred while loading data: " + ex.Message);
            }
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void label1_Click_1(object sender, EventArgs e)
        {

        }

        private void Form1_Load(object sender, EventArgs e)
        {
            try
            {
                this.sparePartsTableAdapter.Fill(this.tutorialDBDataSet.SpareParts);
                LoadData();
            }
            catch (Exception ex)
            {
                MessageBox.Show("An error occurred while loading the form: " + ex.Message);
            }
        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void bindingNavigatorMoveNextItem_Click(object sender, EventArgs e)
        {

        }

        private void Add_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                string partName = txtPartName.Text;
                string partNumber = txtPartNo.Text;
                decimal price = Convert.ToDecimal(txtPrice.Text);

                DatabaseHelper db = new DatabaseHelper();
                db.AddSparePart(partName, partNumber, price);

                MessageBox.Show("Spare part added successfully!");
                LoadData();
            }
            catch (Exception ex)
            {
                MessageBox.Show("An error occurred while adding the spare part: " + ex.Message);
            }
        }

        private void Update_Click(object sender, EventArgs e)
        {
            if (dgvParts.SelectedRows.Count > 0)
            {
                DataGridViewRow selectedRow = dgvParts.SelectedRows[0];
                int partId = Convert.ToInt32(selectedRow.Cells["PartIDDataGridViewTextBoxColumn"].Value);
                string partName = txtPartName.Text;
                string partNumber = txtPartNo.Text;
                decimal price = Convert.ToDecimal(txtPrice.Text);

                DatabaseHelper db = new DatabaseHelper();
                db.UpdateSparePart(partId, partName, partNumber, price);

                MessageBox.Show("Spare part updated successfully!");
                LoadData();
            }
            else
            {
                MessageBox.Show("Please select a spare part to update.");
            }
        }

        private void delete_Click(object sender, EventArgs e)
        {
            if (dgvParts.SelectedRows.Count > 0)
            {
                DataGridViewRow selectedRow = dgvParts.SelectedRows[0];
                int partId = Convert.ToInt32(selectedRow.Cells["PartIDDataGridViewTextBoxColumn"].Value); // Use correct column name

                DatabaseHelper db = new DatabaseHelper();
                db.DeleteSparePart(partId);

                MessageBox.Show("Spare part deleted successfully!");
                LoadData();
            }
            else
            {
                MessageBox.Show("Please select a spare part to delete.");
            }
        }
    }
}
