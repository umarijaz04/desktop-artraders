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
    public partial class Customer : Form
    {
        Form opener;
        String delid = "";
        String type;
        UF.Class1 test = new UF.Class1();

        public Customer(Form form)
        {
            InitializeComponent();
            opener = form;
        }

        private void Customer_Load(object sender, EventArgs e)
        {
            opener.Enabled = false;
            addToolStripMenuItem_Click(this, null);
        }

        private void Customer_FormClosing(object sender, FormClosingEventArgs e)
        {
            opener.Enabled = true;
        }

        //Add Customer
        private void addToolStripMenuItem_Click(object sender, EventArgs e)
        {
            panelupdatecustomer.Visible = panelviewcustomer.Visible = false;
            txtnameadd.Focus();
            txtnameadd.Text = txtcontactadd.Text = txtaddressadd.Text = "";
            cmbaddcustomer.SelectedIndex = 0;
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

        private void txtcontactadd_KeyPress(object sender, KeyPressEventArgs e)
        {
            test.digit_correction(sender, e);
        }

        private void txtaddressadd_KeyPress(object sender, KeyPressEventArgs e)
        {
            txtaddressadd.Text = txtaddressadd.Text.TrimStart();
        }

        private void txtaddressadd_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                btnadd_Click(this, e);
            }
        }

        private void btnadd_Click(object sender, EventArgs e)
        {
            if (txtnameadd.Text != "")
            {
                Login.con.Open();
                SqlCommand cmd = new SqlCommand("SELECT c_name from customer where c_name='" + txtnameadd.Text.ToUpper() + "' AND type='" + cmbaddcustomer.Text.ToUpper() + "'", Login.con);
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
                    SqlCommand cmd1 = new SqlCommand("INSERT into customer (c_name,address,contact,type) values('" + txtnameadd.Text.ToUpper() + "','" + txtaddressadd.Text.ToUpper() + "','" + txtcontactadd.Text + "','" + cmbaddcustomer.Text.ToUpper() + "')", Login.con);
                    cmd1.ExecuteNonQuery();
                    MessageBox.Show("Customer Inserted Successfully", "AR Tranders", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    Login.con.Close();
                    addToolStripMenuItem_Click(this, null);
                }
                Login.con.Close();
            }
        }

        //Update Customer
        private void updateToolStripMenuItem_Click(object sender, EventArgs e)
        {
            panelviewcustomer.Visible = false;
            panelupdatecustomer.Visible = true;
            txtcodeup.Focus();
            txtnameup.Enabled = txtcontactup.Enabled = txtaddressup.Enabled = false;
            txtcodeup.Text = txtnameup.Text = txtcontactup.Text = txtaddressup.Text = "";
        }

        private void txtcodeup_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                if (txtcodeup.Text != "")
                {
                    txtcodeup.SelectAll();
                    txtnameup.Text = txtcontactup.Text = txtaddressup.Text = "";
                    Login.con.Open();
                    SqlCommand cmd = new SqlCommand("SELECT * from customer where c_id='" + txtcodeup.Text + "'", Login.con);
                    SqlDataReader dr = cmd.ExecuteReader();
                    if (dr.HasRows)
                    {
                        while (dr.Read())
                        {
                            txtnameup.Enabled = txtcontactup.Enabled = txtaddressup.Enabled = true;
                            txtnameup.Text = Convert.ToString(dr[1]);
                            txtaddressup.Text = Convert.ToString(dr[2]);
                            txtcontactup.Text = Convert.ToString(dr[3]);
                            type = Convert.ToString(dr[4]);
                        }
                    }
                    else
                    {
                        dr.Close();
                        txtnameup.Enabled = txtcontactup.Enabled = txtaddressup.Enabled = false;
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

        private void txtcontactup_KeyPress(object sender, KeyPressEventArgs e)
        {
            test.digit_correction(sender, e);
        }

        private void txtaddressupr_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                btnup_Click(this, e);
            }
        }

        private void txtaddressup_KeyPress(object sender, KeyPressEventArgs e)
        {
            txtaddressup.Text = txtaddressup.Text.TrimStart();
        }

        private void btnup_Click(object sender, EventArgs e)
        {
            if (txtcodeup.Text != "" && txtnameup.Text != "")
            {
                Boolean flag = true;
                Login.con.Open();
                SqlCommand cmd = new SqlCommand("Select c_name from customer where c_id <> '" + Convert.ToDecimal(txtcodeup.Text) + "' AND type= '" + type + "'", Login.con);
                SqlDataReader dr = cmd.ExecuteReader();
                if (dr.HasRows)
                {
                    while (dr.Read())
                    {
                        if ((txtnameup.Text.ToUpper() == Convert.ToString(dr[0])))
                        {
                            flag = false;
                            dr.Close();
                            MessageBox.Show("This Customer is Already Exist", "AR Tranders", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                            break;
                        }
                    }
                }
                if (flag)
                {
                    dr.Close();
                    try
                    {
                        SqlCommand cmd2 = new SqlCommand("UPDATE customer set c_name='" + txtnameup.Text.ToUpper() + "',address='" + txtaddressup.Text.ToUpper() + "',contact='" + txtcontactup.Text + "' where c_id='" + txtcodeup.Text + "'", Login.con);
                        cmd2.ExecuteNonQuery();
                        MessageBox.Show("Customer Updated Successfully", "AR Tranders", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        updateToolStripMenuItem_Click(this, null);
                    }
                    catch (Exception)
                    {
                        MessageBox.Show("This Customer is Already Used in Transaction", "AR Tranders", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                    }
                }
                Login.con.Close();
            }
        }

        //View Customer
        private void viewToolStripMenuItem_Click(object sender, EventArgs e)
        {
            panelupdatecustomer.Visible = false;
            panelviewcustomer.Visible = true;
            cmbviewcustomer.SelectedIndex = 0;
            dataGridView.DataSource = null;
            string command = "SELECT c_id ID, c_name Name, address Address, contact Contact from customer where type='FIXED' Order By c_name";
            test.bindingcode(command, dataGridView, Login.con);
        }

        private void cmbview_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbviewcustomer.SelectedIndex == 0)
            {
                string command = "SELECT c_id ID, c_name Name, address Address, contact Contact from customer where type='FIXED' Order By c_name";
                test.bindingcode(command, dataGridView, Login.con);
            }
            else
            {
                string command = "SELECT c_id ID, c_name Name, address Address, contact Contact from customer where type='STAKEHOLDER' Order By c_name";
                test.bindingcode(command, dataGridView, Login.con);
            }
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
                DialogResult dresult = MessageBox.Show("Are you sure you want to delete this Customer", "AR Tranders", MessageBoxButtons.OKCancel, MessageBoxIcon.Question);
                if (dresult == DialogResult.OK)
                {
                    try
                    {
                        Login.con.Open();
                        SqlCommand cmd = new SqlCommand("DELETE from customer where c_id='" + delid + "'", Login.con);
                        cmd.ExecuteNonQuery();
                        viewToolStripMenuItem_Click(this, null);
                        delid = "";
                        MessageBox.Show("Customer Deleted Successfully", "AR Tranders", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    catch (Exception)
                    {
                        MessageBox.Show("This Customer is Already Used in Transaction", "AR Tranders", MessageBoxButtons.OK, MessageBoxIcon.Stop);
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
