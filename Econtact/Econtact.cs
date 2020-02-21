using Econtact.econtactClasses;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Econtact
{
    public partial class Econtact : Form
    {
        public Econtact()
        {
            InitializeComponent();
        }
        ContactClass c = new ContactClass();
        private void Econtact_Load(object sender, EventArgs e)
        {
            DataTable dt = c.Select();
            dgvContactList.DataSource = dt;
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            c.FirstName = txtBoxFirstName.Text;
            c.LastName = txtBoxLastName.Text;
            c.ContactNo = txtBoxContactNo.Text;
            c.Address = txtBoxAddress.Text;
            c.Gender = cmbGender.Text;

            bool success = c.Insert(c);
            if (success == true)
            {
                MessageBox.Show("New Contact Succesfully inserted");
                Clear();
            }
            else
            {
                MessageBox.Show("Failed to Add New Contact. Try Again later.");
            }
            DataTable dt = c.Select();
            dgvContactList.DataSource = dt;
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        public void Clear()
        {
            txtBoxContactID.Text = "";
            txtBoxFirstName.Text = "";
            txtBoxLastName.Text = "";
            txtBoxContactNo.Text = "";
            txtBoxAddress.Text = "";
            cmbGender.Text = "";
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            c.ContactID = int.Parse(txtBoxContactID.Text);
            c.FirstName = txtBoxFirstName.Text;
            c.LastName = txtBoxLastName.Text;
            c.ContactNo = txtBoxContactNo.Text;
            c.Address = txtBoxAddress.Text;
            c.Gender = cmbGender.Text;
            bool success = c.Update(c);
            if (success)
            {
                MessageBox.Show("Contact has been Updated Succesfully");
            }
            else
            {
                MessageBox.Show("Contact Failed to update. Try again Later");
            }
            DataTable dt = c.Select();
            dgvContactList.DataSource = dt;
        }

        private void dgvContactList_RowHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            int rowIndex = e.RowIndex;
            txtBoxContactID.Text = dgvContactList.Rows[rowIndex].Cells[0].Value.ToString();
            txtBoxFirstName.Text = dgvContactList.Rows[rowIndex].Cells[1].Value.ToString();
            txtBoxLastName.Text = dgvContactList.Rows[rowIndex].Cells[2].Value.ToString();
            txtBoxContactNo.Text = dgvContactList.Rows[rowIndex].Cells[3].Value.ToString();
            txtBoxAddress.Text = dgvContactList.Rows[rowIndex].Cells[4].Value.ToString();
            cmbGender.Text = dgvContactList.Rows[rowIndex].Cells[5].Value.ToString();
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            Clear();
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            bool success = c.Delete(new ContactClass
            {
                ContactID = Convert.ToInt32(txtBoxContactID.Text),
            });
            if (success)
                MessageBox.Show("Contact Deleted Succesfully");
            else
                MessageBox.Show("Contact not Deleted. try Again later");
            DataTable dt = c.Select();
            dgvContactList.DataSource = dt;
            Clear();
        }
        static string myconnstrng = ConfigurationManager.ConnectionStrings["ContactDb"].ConnectionString;
        private void txtBoxSearch_TextChanged(object sender, EventArgs e)
        {
            string keyword = txtBoxSearch.Text;
            SqlConnection conn = new SqlConnection(myconnstrng);
            SqlDataAdapter adapter = new SqlDataAdapter("select * from tbl_contact where firstname like '%" + keyword + "%' or lastname like '%"+keyword+"%' or contactno like '%"+keyword+"%' or address like '%"+keyword+"%' or gender like '%"+keyword+"%'", conn);
            DataTable dt = new DataTable();
            adapter.Fill(dt);
            dgvContactList.DataSource = dt;
        }

        private void pictureBox3_Click(object sender, EventArgs e)
        {
            FormBorderStyle = FormBorderStyle.None;
            WindowState = FormWindowState.Maximized;
            TopMost = true;
        }
    }
}
