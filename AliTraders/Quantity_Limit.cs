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
    public partial class Quantity_Limit : Form
    {
        Form opener;
        UF.Class1 test = new UF.Class1();

        public Quantity_Limit(Form form)
        {
            InitializeComponent();
            opener = form;
        }

        private void Quantity_Limit_Load(object sender, EventArgs e)
        {
            opener.Enabled = false;
            Login.con.Open();
            SqlCommand cmd = new SqlCommand("SELECT * from limit where id='1'", Login.con);
            SqlDataReader dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                txtqty.Text = Convert.ToString(dr[1]);
            }
            Login.con.Close();
        }

        private void Quantity_Limit_FormClosing(object sender, FormClosingEventArgs e)
        {
            opener.Enabled = true;
        }

        private void txtqty_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                if (txtqty.Text != "")
                {
                    Login.con.Open();
                    SqlCommand cmd = new SqlCommand("UPDATE limit set qty='" + (Convert.ToDecimal(txtqty.Text)) + "'where id ='1'", Login.con);
                    cmd.ExecuteNonQuery();
                    Login.con.Close();
                    this.Close();
                }
            }
        }

        private void txtqty_KeyPress(object sender, KeyPressEventArgs e)
        {
            test.digit_correction(sender, e);
        }
    }
}
