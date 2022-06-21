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
    public partial class Manufacturer : Form
    {
        Form opener;
        String delid = "";
        UF.Class1 test = new UF.Class1();

        public Manufacturer(Form form)
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

        //Add Manufacturer
        private void addToolStripMenuItem_Click(object sender, EventArgs e)
        {
            panelupdatemanu.Visible = panelview.Visible = false;
            txtnameadd.Text = txtcontactadd.Text = "";
            txtnameadd.Focus();
            Login.con.Open();
            SqlCommand cmd = new SqlCommand("SELECT isnull(max(m_id),0) from manufacturer", Login.con);
            Decimal id = (Decimal)cmd.ExecuteScalar() + 1;
            txtcodeadd.Text = id.ToString();
            Login.con.Close();
        }

        private void txtnameadd_KeyPress(object sender, KeyPressEventArgs e)
        {
            test.name_correction(txtnameadd, e);
        }

        private void txtcontactadd_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                btnadd_Click(this, e);
            }
        }

        private void txtcontactadd_KeyPress(object sender, KeyPressEventArgs e)
        {
            test.digit_correction(sender, e);
        }

        private void btnadd_Click(object sender, EventArgs e)
        {
            if (txtcodeadd.Text != "" && txtnameadd.Text != "")
            {
                Login.con.Open();
                SqlCommand cmd = new SqlCommand("SELECT name from manufacturer where name='" + txtnameadd.Text + "'", Login.con);
                SqlDataReader dr = cmd.ExecuteReader();
                if (dr.HasRows)
                {
                    MessageBox.Show("This Manufacturer Name Already Exist", "AR Tranders", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                    txtnameadd.Clear();
                    txtnameadd.Focus();
                    dr.Close();
                    Login.con.Close();
                }
                else
                {
                    dr.Close();
                    SqlCommand cmd2 = new SqlCommand("INSERT into manufacturer (name,contact) values('" + txtnameadd.Text.ToUpper() + "','" + txtcontactadd.Text + "')", Login.con);
                    cmd2.ExecuteNonQuery();
                    MessageBox.Show("Manufacturer Inserted Successfully", "AR Tranders", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    Login.con.Close();
                    addToolStripMenuItem_Click(this, null);
                }
            }
        }

        //Update Manufacturer
        private void updateToolStripMenuItem_Click(object sender, EventArgs e)
        {
            panelview.Visible = txtnameup.Enabled = txtcontactup.Enabled = false;
            panelupdatemanu.Visible = true;
            txtcodeup.Text = txtnameup.Text = txtcontactup.Text = "";
            txtcodeup.Focus();
        }

        private void txtcodeup_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                if (txtcodeup.Text != "")
                {
                    txtcodeup.SelectAll();
                    txtnameup.Text = txtcontactup.Text = "";
                    Login.con.Open();
                    SqlCommand cmd = new SqlCommand("SELECT * from manufacturer where m_id='" + txtcodeup.Text + "'", Login.con);
                    SqlDataReader dr = cmd.ExecuteReader();
                    if (dr.HasRows)
                    {
                        while (dr.Read())
                        {
                            txtnameup.Enabled = txtcontactup.Enabled = true;
                            txtnameup.Text = Convert.ToString(dr[1]);
                            txtcontactup.Text = Convert.ToString(dr[2]);
                        }
                    }
                    else
                    {
                        dr.Close();
                        txtnameup.Enabled = txtcontactup.Enabled = false;
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
            if (txtcodeup.Text != "" && txtnameup.Text != "")
            {
                Boolean flag = true;
                Login.con.Open();
                SqlCommand cmd = new SqlCommand("Select name from manufacturer where m_id <> '" + Convert.ToDecimal(txtcodeup.Text) + "'", Login.con);
                SqlDataReader dr = cmd.ExecuteReader();
                if (dr.HasRows)
                {
                    while (dr.Read())
                    {
                        if ((txtnameup.Text.ToUpper() == Convert.ToString(dr[0])))
                        {
                            flag = false;
                            dr.Close();
                            MessageBox.Show("This Manufacturer is Already Exist", "AR Tranders", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                            break;
                        }
                    }
                }
                if (flag)
                {
                    dr.Close();
                    try
                    {
                        SqlCommand cmd2 = new SqlCommand("UPDATE manufacturer set  name= '" + txtnameup.Text.ToUpper() + "',contact='" + Convert.ToDecimal(txtcontactup.Text) + "' where m_id='" + txtcodeup.Text + "'", Login.con);
                        cmd2.ExecuteNonQuery();
                        MessageBox.Show("Manufacturer Updated Successfully", "AR Tranders", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        updateToolStripMenuItem_Click(this, null);
                    }
                    catch (Exception)
                    {
                        MessageBox.Show("This Manufacturer is Already Used in Transaction", "AR Tranders", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                    }
                }
                Login.con.Close();
            }
        }

        //View Manufacturer
        private void viewToolStripMenuItem_Click(object sender, EventArgs e)
        {
            panelupdatemanu.Visible = false;
            panelview.Visible = true;
            dataGridviewmanu.DataSource = null;
            string command = "SELECT m_id ID, name Name, contact Contact from manufacturer";
            test.bindingcode(command, dataGridviewmanu, Login.con);
        }

        private void dataGridView_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = this.dataGridviewmanu.Rows[e.RowIndex];
                delid = row.Cells[0].Value.ToString();
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
