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
    public partial class Add_Cheque : Form
    {
        Cart opener;
        Decimal c_id;
        String amount;
        CheckBox checkbx;
        UF.Class1 test = new UF.Class1();

        public Add_Cheque(Cart form, Decimal cid, String amt, CheckBox ckbx)
        {
            InitializeComponent();
            opener = form;
            c_id = cid;
            amount = amt;
            checkbx = ckbx;
        }

        private void Add_Cheque_Load(object sender, EventArgs e)
        {
            if (opener != null)
                opener.Enabled = false;
            Login.con.Open();
            SqlCommand cmd = new SqlCommand("select c_name from customer where c_id='" + c_id + "' ", Login.con);
            SqlDataReader dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                txtcnameaddcheque.Text = Convert.ToString(dr[0]);
            }
            dr.Close();
            Login.con.Close();
            txtamtaddcheque.Text = amount;
        }

        private void Add_Cheque_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (opener != null)
            {
                opener.Enabled = true;
                if (txtchnoaddcheque.Text != "" && txtbnknameaddcheque.Text != "")
                {
                    //opener.rcv_chno(Convert.ToDecimal(txtchnoaddcheque.Text));
                }
                else
                    checkbx.Checked = false;
            }
            else
            {
                //opener2.Enabled = true;
                //if (txtchnoaddcheque.Text != "" && txtbnknameaddcheque.Text != "")
                //{
                //  opener2.rcv_chno(Convert.ToDecimal(txtchnoaddcheque.Text));
                //}
                //else
                //  checkbx.Checked = false;
            }
        }

        private void btnaddaddcheque_Click(object sender, EventArgs e)
        {
            if (txtchnoaddcheque.Text != "" && txtcnameaddcheque.Text != "" && txtbnknameaddcheque.Text != "" && txtamtaddcheque.Text != "")
            {
                Login.con.Open();
                SqlCommand cmd = new SqlCommand("SELECT ch_no from cheque where ch_no='" + txtchnoaddcheque.Text + "'", Login.con);
                SqlDataReader dr = cmd.ExecuteReader();
                if (dr.HasRows)
                {
                    MessageBox.Show("This Cheque Number Already Exist");
                    txtchnoaddcheque.Clear();
                    txtchnoaddcheque.Focus();
                    dr.Close();
                }
                else
                {
                    dr.Close();
                    SqlCommand cmd2 = new SqlCommand("insert into cheque(ch_no,c_id,bank_name,amount,status) values('" + txtchnoaddcheque.Text + "', '" + c_id + "', '" + txtbnknameaddcheque.Text + "', '" + txtamtaddcheque.Text + "', 'Wait')", Login.con);
                    cmd2.ExecuteNonQuery();
                    Login.con.Close();
                    this.Close();
                }
                Login.con.Close();
            }
        }

        private void txtchnoaddcheque_KeyPress(object sender, KeyPressEventArgs e)
        {
            test.digit_correction(sender, e);
        }

        private void txtbnknameaddcheque_KeyPress(object sender, KeyPressEventArgs e)
        {
            test.name_correction(txtbnknameaddcheque, e);
        }
    }
}
