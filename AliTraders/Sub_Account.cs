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
    public partial class Sub_Account : Form
    {
        Form opener;
        String delid = "";
        Decimal customer_id = 0;
        UF.Class1 test = new UF.Class1();

        public Sub_Account(Form form)
        {
            InitializeComponent();
            opener = form;
        }

        private void Manufacturer_Load(object sender, EventArgs e)
        {
            opener.Enabled = false;
            addToolStripMenuItem_Click(this, null);
        }

        private void Manufacturer_FormClosing(object sender, FormClosingEventArgs e)
        {
            opener.Enabled = true;
        }

        //Add Sub Account
        private void addToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //panelupdatemanu.Visible = panelview.Visible = false;
            string command = "SELECT sa.sub_id ID, c.c_name Account, sa.[name] SubAccount FROM sub_account sa INNER JOIN customer c ON c.c_id = sa.c_id";
            test.bindingcode(command, dataGridviewmanu, Login.con);
            txtnameadd.Text = cmbcustomername.Text = "";
            customer_id = 0;
            delid = "";
            cmbcustomername.Focus();
            //Login.con.Open();
            //SqlCommand cmd = new SqlCommand("SELECT isnull(max(sub_id),0) from sub_account", Login.con);
            //Decimal id = (Decimal)cmd.ExecuteScalar() + 1;
            //txtcodeadd.Text = id.ToString();
            //Login.con.Close();
        }

        private void txtnameadd_KeyPress(object sender, KeyPressEventArgs e)
        {
            test.name_correction(txtnameadd, e);
        }

        //private void txtcontactadd_KeyDown(object sender, KeyEventArgs e)
        //{
        //    if (e.KeyCode == Keys.Enter)
        //    {
        //        btnadd_Click(this, e);
        //    }
        //}

        //private void txtcontactadd_KeyPress(object sender, KeyPressEventArgs e)
        //{
        //    test.digit_correction(sender,e);
        //}

        private void txtnameadd_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                btnadd_Click(this, e);
            }
        }

        private void cmbcustomername_Enter(object sender, EventArgs e)
        {
            string command = "select c_name from customer order by c_name";
            test.cmbox_drop(command, cmbcustomername, Login.con);
        }

        private void cmbcustomername_Leave(object sender, EventArgs e)
        {
            if (cmbcustomername.Text != "")
            {
                cmbcustomername.Text = cmbcustomername.Text.ToUpper();
                cmbcustomername.SelectAll();
                Login.con.Open();

                SqlCommand cmd8 = new SqlCommand("SELECT c_id from customer where c_name='" + cmbcustomername.Text.ToUpper() + "'", Login.con);
                SqlDataReader dr8 = cmd8.ExecuteReader();
                if (dr8.HasRows)
                {
                    while (dr8.Read())
                    {
                        customer_id = Convert.ToDecimal(dr8[0]);
                    }
                    dr8.Close();
                }
                else
                {
                    dr8.Close();
                    MessageBox.Show("Record Not Found", "AR Tranders", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                    cmbcustomername.Focus();
                }
                Login.con.Close();
            }
        }

        private void btnadd_Click(object sender, EventArgs e)
        {
            if (cmbcustomername.Text != "" && txtnameadd.Text != "" && customer_id != 0)
            {
                if (delid == "")
                {
                    Login.con.Open();
                    SqlCommand cmd = new SqlCommand("SELECT c.c_name Account, sa.[name] SubAccount FROM sub_account sa INNER JOIN customer c ON c.c_id = sa.c_id where name='" + txtnameadd.Text.ToUpper() + "' AND c.c_id='" + customer_id + "'", Login.con);
                    SqlDataReader dr = cmd.ExecuteReader();
                    if (dr.HasRows)
                    {
                        MessageBox.Show("This Sub Account Name Already Exist", "AR Tranders", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                        txtnameadd.Clear();
                        txtnameadd.Focus();
                        dr.Close();
                        Login.con.Close();
                    }
                    else
                    {
                        dr.Close();
                        SqlCommand cmd2 = new SqlCommand("INSERT into sub_account (name,c_id) values('" + txtnameadd.Text.ToUpper() + "','" + customer_id + "')", Login.con);
                        cmd2.ExecuteNonQuery();
                        MessageBox.Show("Sub Account Inserted Successfully", "AR Tranders", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        Login.con.Close();
                        addToolStripMenuItem_Click(this, null);
                    }
                }
                else
                {
                    Boolean flag = true;
                    Login.con.Open();
                    SqlCommand cmd = new SqlCommand("SELECT c.c_name Account, sa.[name] SubAccount FROM sub_account sa INNER JOIN customer c ON c.c_id = sa.c_id where sub_id <> '" + delid + "'", Login.con);
                    SqlDataReader dr = cmd.ExecuteReader();
                    if (dr.HasRows)
                    {
                        while (dr.Read())
                        {
                            if (txtnameadd.Text.ToUpper() == Convert.ToString(dr[1]) && cmbcustomername.Text.ToUpper() == Convert.ToString(dr[0]))
                            {
                                flag = false;
                                dr.Close();
                                MessageBox.Show("This Sub Account is Already Exist", "AR Tranders", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                                txtnameadd.Clear();
                                txtnameadd.Focus();
                                break;
                            }
                        }
                    }
                    if (flag)
                    {
                        dr.Close();
                        try
                        {
                            SqlCommand cmd2 = new SqlCommand("UPDATE sub_account set  name= '" + txtnameadd.Text.ToUpper() + "', c_id='" + Convert.ToDecimal(customer_id) + "' where sub_id='" + delid + "'", Login.con);
                            cmd2.ExecuteNonQuery();
                            MessageBox.Show("Sub Account Updated Successfully", "AR Tranders", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                        catch (Exception)
                        {
                            MessageBox.Show("This Sub Account is Already Used in Transaction", "AR Tranders", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                        }
                        Login.con.Close();
                        addToolStripMenuItem_Click(this, null);
                    }
                    else
                    {
                        Login.con.Close();
                    }
                }
            }
        }

        //Update Sub Account
        private void updateToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //panelview.Visible = txtnameup.Enabled = txtcontactup.Enabled = false;
            //panelupdatemanu.Visible = true;
            //txtcodeup.Text = txtnameup.Text = txtcontactup.Text = "";
            //txtcodeup.Focus();
        }

        private void txtcodeup_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                //if (txtcodeup.Text != "")
                //{
                //    txtcodeup.SelectAll();
                //    txtnameup.Text = txtcontactup.Text = "";
                //    Login.con.Open();
                //    SqlCommand cmd = new SqlCommand("SELECT * from manufacturer where m_id='" + txtcodeup.Text + "'", Login.con);
                //    SqlDataReader dr = cmd.ExecuteReader();
                //    if (dr.HasRows)
                //    {
                //        while (dr.Read())
                //        {
                //            txtnameup.Enabled = txtcontactup.Enabled = true;
                //            txtnameup.Text = Convert.ToString(dr[1]);
                //            txtcontactup.Text = Convert.ToString(dr[2]);
                //        }
                //    }
                //    else
                //    {
                //        dr.Close();
                //        txtnameup.Enabled = txtcontactup.Enabled = false;
                //    }
                //    dr.Close();
                //    Login.con.Close();
                //}
            }
        }

        private void txtcodeup_KeyPress(object sender, KeyPressEventArgs e)
        {
            test.digit_correction(sender, e);
        }

        private void txtnameup_KeyPress(object sender, KeyPressEventArgs e)
        {
            //test.name_correction(txtnameup, e);
        }

        private void txtcontactup_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                btnup_Click(this, e);
            }
        }

        private void txtcontactup_KeyPress(object sender, KeyPressEventArgs e)
        {
            test.digit_correction(sender, e);
        }

        private void btnup_Click(object sender, EventArgs e)
        {
            //if (txtcodeup.Text != "" && txtnameup.Text != "")
            //{
            //    Boolean flag = true;
            //    Login.con.Open();
            //    SqlCommand cmd = new SqlCommand("Select name from manufacturer where m_id <> '" + Convert.ToDecimal(txtcodeup.Text) + "'", Login.con);
            //    SqlDataReader dr = cmd.ExecuteReader();
            //    if (dr.HasRows)
            //    {
            //        while (dr.Read())
            //        {
            //            if ((txtnameup.Text.ToUpper() == Convert.ToString(dr[0])))
            //            {
            //                flag = false;
            //                dr.Close();
            //                MessageBox.Show("This Manufacturer is Already Exist", "AR Tranders", MessageBoxButtons.OK, MessageBoxIcon.Stop);
            //                break;
            //            }
            //        }
            //    }
            //    if (flag)
            //    {
            //        dr.Close();
            //        try
            //        {
            //            SqlCommand cmd2 = new SqlCommand("UPDATE manufacturer set  name= '" + txtnameup.Text.ToUpper() + "',contact='" + Convert.ToDecimal(txtcontactup.Text) + "' where m_id='" + txtcodeup.Text + "'", Login.con);
            //            cmd2.ExecuteNonQuery();
            //            MessageBox.Show("Manufacturer Updated Successfully", "AR Tranders", MessageBoxButtons.OK, MessageBoxIcon.Information);
            //            updateToolStripMenuItem_Click(this, null);
            //        }
            //        catch (Exception)
            //        {
            //            MessageBox.Show("This Manufacturer is Already Used in Transaction", "AR Tranders", MessageBoxButtons.OK, MessageBoxIcon.Stop);
            //        }
            //    }
            //    Login.con.Close();
            //}
        }

        //View Sub Account
        private void viewToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //panelupdatemanu.Visible = false;
            //panelview.Visible = true;
            dataGridviewmanu.DataSource = null;
            string command = "SELECT sa.sub_id ID, c.c_name Account, sa.[name] SubAccount FROM sub_account sa INNER JOIN customer c ON c.c_id = sa.c_id";
            test.bindingcode(command, dataGridviewmanu, Login.con);
        }

        private void dataGridView_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = this.dataGridviewmanu.Rows[e.RowIndex];
                delid = row.Cells[0].Value.ToString();
                txtnameadd.Text = cmbcustomername.Text = "";
                Login.con.Open();
                SqlCommand cmd = new SqlCommand("SELECT c.c_id, c.c_name Account, sa.[name] SubAccount FROM sub_account sa INNER JOIN customer c ON c.c_id = sa.c_id where sa.sub_id='" + delid + "'", Login.con);
                SqlDataReader dr = cmd.ExecuteReader();
                if (dr.HasRows)
                {
                    while (dr.Read())
                    {
                        //txtnameup.Enabled = txtcontactup.Enabled = true;
                        customer_id = Convert.ToDecimal(dr[0]);
                        cmbcustomername.Text = Convert.ToString(dr[1]);
                        txtnameadd.Text = Convert.ToString(dr[2]);
                    }
                }
                else
                {
                    dr.Close();
                    //txtnameup.Enabled = txtcontactup.Enabled = false;
                }
                dr.Close();
                Login.con.Close();
            }
        }

        private void btndel_Click(object sender, EventArgs e)
        {
            if (delid != "")
            {
                DialogResult dresult = MessageBox.Show("Are you sure you want to delete this Manufacturer", "AR Tranders", MessageBoxButtons.OKCancel, MessageBoxIcon.Question);
                if (dresult == DialogResult.OK)
                {
                    try
                    {
                        Login.con.Open();
                        SqlCommand cmd = new SqlCommand("DELETE from manufacturer where m_id='" + delid + "'", Login.con);
                        cmd.ExecuteNonQuery();
                        viewToolStripMenuItem_Click(this, null);
                        delid = "";
                        MessageBox.Show("Manufacturer Deleted Successfully", "AR Tranders", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    catch (Exception)
                    {
                        MessageBox.Show("This Manufacturer is Already Used in Transaction", "AR Tranders", MessageBoxButtons.OK, MessageBoxIcon.Stop);
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
