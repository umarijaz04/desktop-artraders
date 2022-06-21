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
    public partial class Settings : Form
    {
        Form opener;
        String pass, uname;
        UF.Class1 test = new UF.Class1();

        public Settings(Form form, String name)
        {
            InitializeComponent();
            opener = form;
            uname = name;
        }

        private void Settings_Load(object sender, EventArgs e)
        {
            opener.Enabled = false;
        }

        private void Settings_FormClosing(object sender, FormClosingEventArgs e)
        {
            opener.Enabled = true;
        }

        private void txtcpass_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                btnok_Click(this, null);
            }
        }

        private void btnok_Click(object sender, EventArgs e)
        {
            Login.con.Open();
            SqlCommand cmd = new SqlCommand("SELECT pass from office_account where uname='" + uname + "' ", Login.con);
            SqlDataReader dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                pass = Convert.ToString(dr[0]);
            }
            dr.Close();
            if (test.MD5Hash(txtppass.Text) == pass)
            {
                ppwrg.Visible = false;
                if (txtnpass.Text != "")
                {
                    npwrg.Visible = false;
                    if (txtcpass.Text != "")
                    {
                        cpwrg.Visible = false;
                        if (txtnpass.Text == txtcpass.Text)
                        {
                            SqlCommand cmd2 = new SqlCommand("UPDATE office_account set pass= '" + test.MD5Hash(txtnpass.Text) + "' where uname='" + uname + "'", Login.con);
                            cmd2.ExecuteNonQuery();
                            MessageBox.Show("Your Password Has Been Changed Successfully", "AR Tranders", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            txtppass.Clear();
                            txtnpass.Clear();
                            txtcpass.Clear();
                            txtppass.Focus();
                        }
                        else
                        {
                            npwrg.Visible = true;
                            txtnpass.Clear();
                            txtcpass.Clear();
                            txtnpass.Focus();
                        }
                    }
                    else
                    {
                        cpwrg.Visible = true;
                        txtcpass.Clear();
                        txtcpass.Focus();
                    }
                }
                else
                {
                    npwrg.Visible = true;
                    cpwrg.Visible = false;
                    txtnpass.Clear();
                    txtcpass.Clear();
                    txtnpass.Focus();
                }
            }
            else
            {
                ppwrg.Visible = true;
                npwrg.Visible = false;
                cpwrg.Visible = false;
                txtppass.Clear();
                txtnpass.Clear();
                txtcpass.Clear();
                txtppass.Focus();
            }
            Login.con.Close();
        }

        private void btncancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
