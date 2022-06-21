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
    public partial class Cheque : Form
    {
        Form opener;
        Decimal cheque_no, amount, invoice_no;
        String status;
        UF.Class1 test = new UF.Class1();

        public Cheque(Form form)
        {
            InitializeComponent();
            opener = form;
        }

        private void Cheque_Load(object sender, EventArgs e)
        {
            opener.Enabled = false;
            dataGridView.DataSource = null;
            Login.con.Open();
            SqlCommand cmd = new SqlCommand("Delete from cheque where ss_id=null", Login.con);
            cmd.ExecuteNonQuery();
            Login.con.Close();
            string command = "SELECT ch.ss_id Invoice_No, c.c_name Customer_Name, ch.ch_no Cheque_No,ch.bank_name Bank_Name,ch.amount Amount From cheque ch,customer c Where ch.c_id=c.c_id AND ch.status='Wait' ";
            test.bindingcode(command, dataGridView, Login.con);
        }

        private void Cheque_FormClosing(object sender, FormClosingEventArgs e)
        {
            opener.Enabled = true;
        }

        private void dataGridviewcheque_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = this.dataGridView.Rows[e.RowIndex];
                cheque_no = Convert.ToDecimal(row.Cells["Cheque_No"].Value.ToString());
                amount = Convert.ToDecimal(row.Cells["Amount"].Value.ToString());
                btnverified.Enabled = true;
                btnunverified.Enabled = true;
            }
        }

        private void btnverified_Click(object sender, EventArgs e)
        {
            Login.con.Open();
            SqlCommand cmd = new SqlCommand("select isnull(ss_id,0) from cheque where ch_no='" + cheque_no + "' ", Login.con);
            SqlDataReader dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                invoice_no = Convert.ToDecimal(dr[0]);
            }
            dr.Close();
            if (invoice_no != 0)
            {
                /*SqlCommand cmd2 = new SqlCommand("insert into cash_transaction (datetime) values('" + dateTimePicker.Value + "')", Login.con);
                cmd2.ExecuteNonQuery();
                SqlCommand cmd3 = new SqlCommand("select cat_id from cash_transaction where datetime='" + dateTimePicker.Value + "'", Login.con);
                Decimal cat_id = (Decimal)cmd3.ExecuteScalar();
                SqlCommand cmd4 = new SqlCommand("update cheque set status = 'Verified',checked='yes' where ch_no = '" + cheque_no + "'", Login.con);
                cmd4.ExecuteNonQuery();
                SqlCommand cmd5 = new SqlCommand("update sale_stock set cheque_mode = 'Verified',cat_id='" + cat_id + "' where ss_id = '" + invoice_no + "'", Login.con);
                cmd5.ExecuteNonQuery();
                MessageBox.Show("Cheque Verified Successfully", "AR Tranders", MessageBoxButtons.OK, MessageBoxIcon.Information);
                string command = "SELECT ch.ss_id Invoice_No, c.c_name Customer_Name, ch.ch_no Cheque_No,ch.bank_name Bank_Name,ch.amount Amount From cheque ch,customer c Where ch.c_id=c.c_id AND ch.status='Wait' ";
                test.bindingcode(command, dataGridView, Login.con);*/
            }
            Login.con.Close();
        }

        private void btnunverified_Click(object sender, EventArgs e)
        {
            Login.con.Open();
            SqlCommand cmd = new SqlCommand("update cheque set status='Unverified',checked='yes'  where ch_no = '" + cheque_no + "'", Login.con);
            cmd.ExecuteNonQuery();
            SqlCommand cmd2 = new SqlCommand("SELECT isnull(ss_id,0) from cheque where ch_no='" + cheque_no + "' ", Login.con);
            SqlDataReader dr2 = cmd2.ExecuteReader();
            while (dr2.Read())
            {
                invoice_no = Convert.ToDecimal(dr2[0]);
            }
            dr2.Close();
            if (invoice_no != 0)
            {
                SqlCommand cmd3 = new SqlCommand("SELECT status from sale_stock where ss_id='" + invoice_no + "' ", Login.con);
                SqlDataReader dr3 = cmd3.ExecuteReader();
                while (dr3.Read())
                {
                    status = Convert.ToString(dr3[0]);
                }
                dr3.Close();
                if (status == "Read")
                {
                    SqlCommand cmd4 = new SqlCommand("update sale_stock set cheque_mode='Unverified',status='DRead' where ss_id = '" + invoice_no + "'", Login.con);
                    cmd4.ExecuteNonQuery();
                }
                else
                {
                    SqlCommand cmd4 = new SqlCommand("update sale_stock set cheque_mode='Unverified' where ss_id = '" + invoice_no + "'", Login.con);
                    cmd4.ExecuteNonQuery();
                }
                Login.con.Close();
                MessageBox.Show("Cheque UnVerified Successfully", "AR Tranders", MessageBoxButtons.OK, MessageBoxIcon.Information);
                string command = "SELECT ch.ss_id Invoice_No, c.c_name Customer_Name, ch.ch_no Cheque_No,ch.bank_name Bank_Name,ch.amount Amount From cheque ch,customer c  Where ch.c_id=c.c_id AND ch.status='Wait' ";
                test.bindingcode(command, dataGridView, Login.con);
            }
            Login.con.Close();
        }
    }
}
