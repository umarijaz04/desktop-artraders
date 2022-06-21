using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace AhmadSanitary
{
    public partial class Expenses : Form
    {
        Form opener;
        String delid = "";
        UF.Class1 test = new UF.Class1();

        public Expenses(Form form)
        {
            InitializeComponent();
            opener = form;
        }

        private void Expenses_Load(object sender, EventArgs e)
        {
            opener.Enabled = false;
            addToolStripMenuItem_Click(this, null);
        }

        private void Expenses_FormClosing(object sender, FormClosingEventArgs e)
        {
            opener.Enabled = true;
        }

        //Add Expenses
        private void addToolStripMenuItem_Click(object sender, EventArgs e)
        {
            panelupdateExpenses.Visible = panelviewExpenses.Visible = false;
            txtnameadd.Focus();
            txtnameadd.Text = "";
            Login.con.Open();
            SqlCommand cmd = new SqlCommand("SELECT isnull(max(c_id),0) from customer", Login.con);
            Decimal id = (Decimal)cmd.ExecuteScalar() + 1;
            txtcodeadd.Text = id.ToString();
            Login.con.Close();
        }

        private void txtnameadd_KeyPress(object sender, KeyPressEventArgs e)
        {
            test.name_correction(txtnameadd, e);
        }

        private void txtnameadd_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                btnadd_Click(this, null);
            }
        }

        private void btnadd_Click(object sender, EventArgs e)
        {
            if (txtnameadd.Text != "")
            {
                Login.con.Open();
                SqlCommand cmd = new SqlCommand("SELECT c_name from customer where c_name='" + txtnameadd.Text.ToUpper() + "' AND type='EXPENSE'", Login.con);
                SqlDataReader dr = cmd.ExecuteReader();
                if (dr.HasRows)
                {
                    MessageBox.Show("This P_Name Already Exist", "AR Tranders", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                    txtnameadd.Clear();
                    txtnameadd.Focus();
                    dr.Close();
                    Login.con.Close();
                }
                else
                {
                    dr.Close();
                    SqlCommand cmd1 = new SqlCommand("INSERT into customer (c_name,type) values('" + txtnameadd.Text.ToUpper() + "','EXPENSE')", Login.con);
                    cmd1.ExecuteNonQuery();
                    MessageBox.Show("Expenses Inserted Successfully", "AR Tranders", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    Login.con.Close();
                    addToolStripMenuItem_Click(this, null);
                }
                Login.con.Close();
            }
        }

        //Update Expenses
        private void updateToolStripMenuItem_Click(object sender, EventArgs e)
        {
            panelviewExpenses.Visible = false;
            panelupdateExpenses.Visible = true;
            txtcodeup.Focus();
            txtnameup.Enabled = false;
            txtcodeup.Text = txtnameup.Text = "";
        }

        private void txtcodeup_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                if (txtcodeup.Text != "")
                {
                    txtcodeup.SelectAll();
                    txtnameup.Text = "";
                    Login.con.Open();
                    SqlCommand cmd = new SqlCommand("SELECT * from customer where c_id='" + txtcodeup.Text + "' AND type='EXPENSE'", Login.con);
                    SqlDataReader dr = cmd.ExecuteReader();
                    if (dr.HasRows)
                    {
                        while (dr.Read())
                        {
                            txtnameup.Enabled = true;
                            txtnameup.Text = Convert.ToString(dr[1]);
                        }
                    }
                    else
                    {
                        dr.Close();
                        txtnameup.Enabled = false;
                    }
                    dr.Close();
                    Login.con.Close();
                }
            }
        }

        private void txtcodeup_KeyPress(object sender, KeyPressEventArgs e)
        {
            test.digit_correction(sender, e);
        }

        private void txtnameup_KeyPress(object sender, KeyPressEventArgs e)
        {
            test.name_correction(txtnameup, e);
        }

        private void txtnameup_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                btnup_Click(this, null);
            }
        }

        private void btnup_Click(object sender, EventArgs e)
        {
            if (txtcodeup.Text != "" && txtnameup.Text != "")
            {
                Boolean flag = true;
                Login.con.Open();
                SqlCommand cmd = new SqlCommand("Select c_name from customer where c_id <> '" + Convert.ToDecimal(txtcodeup.Text) + "' AND type='EXPENSE'", Login.con);
                SqlDataReader dr = cmd.ExecuteReader();
                if (dr.HasRows)
                {
                    while (dr.Read())
                    {
                        if ((txtnameup.Text.ToUpper() == Convert.ToString(dr[0])))
                        {
                            flag = false;
                            dr.Close();
                            MessageBox.Show("This Expenses is Already Exist", "AR Tranders", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                            break;
                        }
                    }
                }
                if (flag)
                {
                    dr.Close();
                    try
                    {
                        SqlCommand cmd2 = new SqlCommand("UPDATE customer set c_name='" + txtnameup.Text.ToUpper() + "' where c_id='" + txtcodeup.Text + "' AND type='EXPENSE'", Login.con);
                        cmd2.ExecuteNonQuery();
                        MessageBox.Show("Expenses Updated Successfully", "AR Tranders", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        updateToolStripMenuItem_Click(this, null);
                    }
                    catch (Exception)
                    {
                        MessageBox.Show("This Expenses is Already Used in Transaction", "AR Tranders", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                    }
                }
                Login.con.Close();
            }
        }

        //View Expenses
        private void viewToolStripMenuItem_Click(object sender, EventArgs e)
        {
            panelupdateExpenses.Visible = false;
            panelviewExpenses.Visible = true;
            dataGridView.DataSource = null;
            string command = "SELECT c_id ID, c_name Name from customer where type='EXPENSE' Order By c_name";
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
                DialogResult dresult = MessageBox.Show("Are you sure you want to delete this Expenses", "AR Tranders", MessageBoxButtons.OKCancel, MessageBoxIcon.Question);
                if (dresult == DialogResult.OK)
                {
                    try
                    {
                        Login.con.Open();
                        SqlCommand cmd = new SqlCommand("DELETE from customer where c_id='" + delid + "' AND type='EXPENSE'", Login.con);
                        cmd.ExecuteNonQuery();
                        viewToolStripMenuItem_Click(this, null);
                        delid = "";
                        MessageBox.Show("Expenses Deleted Successfully", "AR Tranders", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    catch (Exception)
                    {
                        MessageBox.Show("This Expenses is Already Used in Transaction", "AR Tranders", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                        delid = "";
                    }
                    finally
                    {
                        Login.con.Close();
                    }
                }
            }
        }
    }
}
