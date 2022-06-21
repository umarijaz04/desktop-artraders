using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace AhmadSanitary
{
    public partial class Office_Expenses : Form
    {
        Form opener;
        String delid;
        UF.Class1 test = new UF.Class1();
        public Office_Expenses(Form form)
        {
            InitializeComponent();
            opener = form;
        }

        private void Office_Expenses_Load(object sender, EventArgs e)
        {
            opener.Enabled = false;
        }

        private void Office_Expenses_FormClosing(object sender, FormClosingEventArgs e)
        {
            opener.Enabled = true;
        }

        private void manufacturerToolStripMenuItem_Click(object sender, EventArgs e)
        {
            paneldel.Visible = false;
        }

        private void txtamt_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                btnadd_Click(this, null);
        }

        private void txtamt_KeyPress(object sender, KeyPressEventArgs e)
        {
            test.digit_correction(sender, e);
        }

        private void txtdes_KeyPress(object sender, KeyPressEventArgs e)
        {
            txtdes.Text = txtdes.Text.TrimStart();
        }

        private void btnadd_Click(object sender, EventArgs e)
        {
            if (txtdes.Text != "" && txtamt.Text != "")
            {
                Login.con.Open();
                SqlCommand cmd = new SqlCommand("insert into cash_transaction (datetime) values('" + DateTime.Now + "')", Login.con);
                cmd.ExecuteNonQuery();
                SqlCommand cmd2 = new SqlCommand("select cat_id from cash_transaction where datetime='" + DateTime.Now + "'", Login.con);
                Decimal cat_id = (Decimal)cmd2.ExecuteScalar();
                SqlCommand cmd3 = new SqlCommand("INSERT into expense (description,amount,datetime,cat_id,type) values('" + txtdes.Text + "','" + Convert.ToDecimal(txtamt.Text) + "','" + DateTime.Now + "','" + cat_id + "','OFFICE')", Login.con);
                cmd3.ExecuteNonQuery();
                Login.con.Close();
                MessageBox.Show("Expenses Insert Successfully", "AR Tranders", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtdes.Text = txtamt.Text = "";
                txtdes.Focus();
            }
        }

        private void avilableStockToolStripMenuItem_Click(object sender, EventArgs e)
        {
            paneldel.Visible = true;
            dataGridView.DataSource = null;
            string command = "SELECT  e_id E_ID, datetime DateTime, description Description, amount Amount from expense";
            test.bindingcode(command, dataGridView, Login.con);
        }

        private void dataGridView_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = this.dataGridView.Rows[e.RowIndex];
                delid = row.Cells[0].Value.ToString();
            }
        }

        private void btndel_Click(object sender, EventArgs e)
        {
            if (delid != "")
            {
                DialogResult dresult = MessageBox.Show("Are you sure you want to delete this Expense", "AR Tranders", MessageBoxButtons.OKCancel, MessageBoxIcon.Question);
                if (dresult == DialogResult.OK)
                {
                    Login.con.Open();
                    SqlCommand cmd = new SqlCommand("DELETE from expense where e_id='" + delid + "'", Login.con);
                    cmd.ExecuteNonQuery();
                    avilableStockToolStripMenuItem_Click(this, null);
                    delid = "";
                    MessageBox.Show("Expense Deleted Successfully", "AR Tranders", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    Login.con.Close();
                }
            }
        }
    }
}