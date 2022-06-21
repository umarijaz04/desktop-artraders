using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.Drawing.Printing;

namespace AhmadSanitary
{
    public partial class Sales_Invoices : Form
    {
        Form opener;
        Decimal invoice_no, amt, customer_id;
        String type, delid;
        UF.Class1 test = new UF.Class1();

        public Sales_Invoices(Form from)
        {
            InitializeComponent();
            opener = from;
        }

        private void Sales_Invoices_Load(object sender, EventArgs e)
        {
            opener.Enabled = false;
        }

        private void Sales_Invoices_FormClosing(object sender, FormClosingEventArgs e)
        {
            opener.Enabled = true;
        }

        private void txtinvoiceno_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                if (txtinvoiceno.Text != "")
                {
                    amt = 0;
                    cmbcustomername.Text = "";
                    string command = "select s.ss_id Invoice_No,s.datetime DateTime,case when c.type='FIXED' then 'Customer' else 'Stakeholder' end as AC_Type,c.c_name AC_Name,(s.total-s.discount) Amount From sale_stock s,Customer c Where s.c_id=c.c_id AND s.ss_id='" + Convert.ToDecimal(txtinvoiceno.Text) + "' UNION ALL select s.ss_id Invoice_No,s.datetime DateTime,'Manufacturer' AC_Type,m.name AC_Name,(s.total-s.discount) Amount From sale_stock s,manufacturer m Where s.m_id=m.m_id AND s.ss_id='" + Convert.ToDecimal(txtinvoiceno.Text) + "'";
                    test.bindingcode(command, dataGridView, Login.con);
                    for (int i = 0; i < dataGridView.RowCount; i++)
                    {
                        amt += Convert.ToDecimal(dataGridView.Rows[i].Cells[4].Value);
                    }
                    lbsales.Text = amt.ToString();
                }
            }
        }

        private void btnsearch_Click(object sender, EventArgs e)
        {
            amt = 0;
            txtinvoiceno.Clear();
            if (cmbcustomername.Text != "")
            {
                cmbcustomername.Text = cmbcustomername.Text.ToUpper();
                cmbcustomername.SelectAll();
                Login.con.Open();
                if (cmbquality2.Text == "Customer" || cmbquality2.Text == "Stakeholder")
                {
                    SqlCommand cmd = new SqlCommand("SELECT c_id from customer where c_name='" + cmbcustomername.Text.ToUpper() + "' AND type='" + type + "'", Login.con);
                    SqlDataReader dr = cmd.ExecuteReader();
                    if (dr.HasRows)
                    {
                        while (dr.Read())
                        {
                            customer_id = Convert.ToDecimal(dr[0]);
                        }
                        dr.Close();
                    }
                    dr.Close();
                    Login.con.Close();
                    string command = "select s.ss_id Invoice_No,s.datetime DateTime,'" + cmbquality2.Text + "' AC_Type ,c.c_name AC_Name,(s.total-s.discount) Amount From sale_stock s,Customer c Where s.c_id=c.c_id AND s.c_id='" + customer_id + "' AND (s.datetime Between '" + Convert.ToDateTime(dateTimePicker1.Text) + "' AND '" + Convert.ToDateTime(dateTimePicker2.Text).AddDays(1) + "')";
                    test.bindingcode(command, dataGridView, Login.con);
                }
                else if (cmbquality2.Text == "Manufacturer")
                {
                    SqlCommand cmd = new SqlCommand("SELECT m_id from manufacturer where name='" + cmbcustomername.Text.ToUpper() + "'", Login.con);
                    SqlDataReader dr = cmd.ExecuteReader();
                    if (dr.HasRows)
                    {
                        while (dr.Read())
                        {
                            customer_id = Convert.ToDecimal(dr[0]);
                        }
                        dr.Close();
                    }
                    dr.Close();
                    Login.con.Close();
                    string command = "select s.ss_id Invoice_No,s.datetime DateTime,'Manufacturer' AC_Type ,m.name AC_Name,(s.total-s.discount) Amount From sale_stock s,manufacturer m Where s.m_id=m.m_id AND s.m_id='" + customer_id + "' AND (s.datetime Between '" + Convert.ToDateTime(dateTimePicker1.Text) + "' AND '" + Convert.ToDateTime(dateTimePicker2.Text).AddDays(1) + "')";
                    test.bindingcode(command, dataGridView, Login.con);
                }
            }
            else
            {
                string command = "select s.ss_id Invoice_No,s.datetime DateTime, case when c.type='FIXED' then 'Customer' else 'Stakeholder' end as AC_Type,c.c_name AC_Name,(s.total-s.discount) Amount From sale_stock s,Customer c Where s.c_id=c.c_id AND (s.datetime Between '" + Convert.ToDateTime(dateTimePicker1.Text) + "' AND '" + Convert.ToDateTime(dateTimePicker2.Text).AddDays(1) + "') UNION ALL select s.ss_id Invoice_No,s.datetime DateTime,'Manufacturer' AC_Type ,m.name AC_Name,(s.total-s.discount) Amount From sale_stock s,manufacturer m Where s.m_id=m.m_id AND (s.datetime Between '" + Convert.ToDateTime(dateTimePicker1.Text) + "' AND '" + Convert.ToDateTime(dateTimePicker2.Text).AddDays(1) + "')";
                test.bindingcode(command, dataGridView, Login.con);
            }
            for (int i = 0; i < dataGridView.RowCount; i++)
            {
                amt += Convert.ToDecimal(dataGridView.Rows[i].Cells[4].Value);
            }
            lbsales.Text = amt.ToString();
        }

        private void dataGridView_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = this.dataGridView.Rows[e.RowIndex];
                invoice_no = Convert.ToDecimal(row.Cells["Invoice_No"].Value.ToString());
                delid = row.Cells["Invoice_No"].Value.ToString();
                btndetail.Enabled = true;
                btndel.Enabled = true;
            }
        }

        private void btndel_Click(object sender, EventArgs e)
        {
            if (delid != "")
            {
                DialogResult dresult = MessageBox.Show("Are you sure you want to delete this Invoice No", "AR Tranders", MessageBoxButtons.OKCancel, MessageBoxIcon.Question);
                if (dresult == DialogResult.OK)
                {
                    try
                    {
                        Login.con.Open();
                        //SqlCommand cmd = new SqlCommand("select ss_id from sale_stock_detail where ss_id='" + delid + "'", Login.con);
                        //SqlDataReader dr = cmd.ExecuteReader();
                        //if (dr.HasRows)
                        //{
                        //    dr.Close();
                        //    SqlCommand cmd2 = new SqlCommand("DELETE from sale_stock_detail where ss_id='" + delid + "'", Login.con);
                        //    cmd2.ExecuteNonQuery();
                        //}
                        //dr.Close();
                        SqlCommand cmd1 = new SqlCommand("select ss_id from sale_stock_detail2 where ss_id='" + delid + "'", Login.con);
                        SqlDataReader dr1 = cmd1.ExecuteReader();
                        if (dr1.HasRows)
                        {
                            dr1.Close();
                            SqlCommand cmd2 = new SqlCommand("DELETE from sale_stock_detail2 where ss_id='" + delid + "'", Login.con);
                            cmd2.ExecuteNonQuery();
                        }
                        dr1.Close();
                        SqlCommand cmd3 = new SqlCommand("DELETE from sale_stock where ss_id='" + delid + "'", Login.con);
                        cmd3.ExecuteNonQuery();
                        delid = "";
                        Login.con.Close();
                        MessageBox.Show("Invoice Deleted Successfully", "AR Tranders", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        btnsearch_Click(this, null);
                    }
                    catch (Exception)
                    {
                        MessageBox.Show("First Delete Sales Return Against This Sales Invoice", "AR Tranders", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }

        private void btndetail_Click(object sender, EventArgs e)
        {
            Invoice_Detail f = new Invoice_Detail(invoice_no);
            f.Show();
        }

        private void txtinvoiceno_KeyPress(object sender, KeyPressEventArgs e)
        {
            test.digit_correction(sender, e);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                printDocument1.Print();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void cmbcustomername_Enter(object sender, EventArgs e)
        {
            if (cmbquality2.Text == "Customer" || cmbquality2.Text == "Stakeholder")
            {
                if (cmbquality2.Text == "Customer")
                    type = "FIXED";
                else if (cmbquality2.Text == "Stakeholder")
                    type = "STAKEHOLDER";
                test.cmbox_drop("select c_name from customer where type='" + type + "' order by c_name", cmbcustomername, Login.con);
            }
            else if (cmbquality2.Text == "Manufacturer")
            {
                test.cmbox_drop("select name from manufacturer order by name", cmbcustomername, Login.con);
            }
        }

        private void cmbquality2_SelectedIndexChanged(object sender, EventArgs e)
        {
            cmbcustomername.Text = "";
        }

        private int i = 0;
        private void printDocument1_PrintPage(object sender, PrintPageEventArgs e)
        {
            //Image imgPerson = Image.FromFile(@"C:\wonder.png");
            //e.Graphics.DrawImage(imgPerson, 20, 28);
            e.Graphics.DrawString("AR Tranders", new Font("Arial", 24, FontStyle.Bold), Brushes.Black, 210, 50);
            e.Graphics.DrawString("IMPORTERS & GENERAL ORDER SUPPLIER", new Font("Arial", 10), Brushes.Black, 220, 90);
            e.Graphics.DrawString("Deals in All Kinds of Imported Sanitary Items", new Font("Arial", 9, FontStyle.Italic), Brushes.Black, 220, 105);
            e.Graphics.DrawString("Zeeshan 0323-8644433, +8615795799454", new Font("Arial", 9, FontStyle.Regular), Brushes.Black, 220, 125);
            //e.Graphics.DrawString("Proprietor: ", new Font("Arial", 9, FontStyle.Bold), Brushes.Black, 265, 140);
            e.Graphics.DrawString("email: zshani434@gmail.com", new Font("Arial", 9, FontStyle.Italic), Brushes.Black, 220, 140);
            int startX = 50;
            int startY = 150;
            int Offset = 40;
            e.Graphics.DrawString("Sales Report", new Font("Calibri", 12, FontStyle.Underline), new SolidBrush(Color.Black), startX, startY + Offset);
            Offset = Offset + 30;
            e.Graphics.DrawString("A/c Type:", new Font("Calibri", 10, FontStyle.Bold), new SolidBrush(Color.Black), startX, startY + Offset);
            e.Graphics.DrawString(Convert.ToString(dataGridView.Rows[0].Cells[2].Value), new Font("Calibri", 10, FontStyle.Bold), new SolidBrush(Color.Black), startX + 60, startY + Offset);
            Offset = Offset + 20;
            e.Graphics.DrawString("A/c Name:", new Font("Calibri", 10, FontStyle.Bold), new SolidBrush(Color.Black), startX, startY + Offset);
            e.Graphics.DrawString(Convert.ToString(dataGridView.Rows[0].Cells[3].Value), new Font("Calibri", 10), new SolidBrush(Color.Black), startX + 65, startY + Offset);
            Offset = Offset + 20;
            e.Graphics.DrawString("From:", new Font("Calibri", 10, FontStyle.Bold), new SolidBrush(Color.Black), startX, startY + Offset);
            e.Graphics.DrawString(dateTimePicker1.Text, new Font("Calibri", 10), new SolidBrush(Color.Black), startX + 40, startY + Offset);
            e.Graphics.DrawString("To:", new Font("Calibri", 10, FontStyle.Bold), new SolidBrush(Color.Black), startX + 380, startY + Offset);
            e.Graphics.DrawString(dateTimePicker2.Text, new Font("Calibri", 10), new SolidBrush(Color.Black), startX + 405, startY + Offset);
            Offset = Offset + 20;
            string underLine = "--------------------------------------------------------------------------------";
            e.Graphics.DrawString(underLine, new Font("Courier New", 10, FontStyle.Bold), new SolidBrush(Color.Black), startX, startY + Offset);
            Offset = Offset + 20;
            e.Graphics.DrawString("S_ID", new Font("Calibri", 10, FontStyle.Bold), new SolidBrush(Color.Black), startX, startY + Offset);
            e.Graphics.DrawString("Date & Time", new Font("Calibri", 10, FontStyle.Bold), new SolidBrush(Color.Black), startX + 120, startY + Offset);
            e.Graphics.DrawString("Amount", new Font("Calibri", 10, FontStyle.Bold), new SolidBrush(Color.Black), startX + 560, startY + Offset);
            Offset = Offset + 20;
            e.Graphics.DrawString(underLine, new Font("Courier New", 10, FontStyle.Bold), new SolidBrush(Color.Black), startX, startY + Offset);
            Offset = Offset + 20;
            int a = dataGridView.Rows.Count;
            for (; i < a; i++)
            {
                e.Graphics.DrawString(Convert.ToString(dataGridView.Rows[i].Cells[0].Value), new Font("Calibri", 9), new SolidBrush(Color.Black), startX, startY + Offset);
                e.Graphics.DrawString(Convert.ToString(dataGridView.Rows[i].Cells[1].Value), new Font("Calibri", 9), new SolidBrush(Color.Black), startX + 120, startY + Offset);
                e.Graphics.DrawString(Convert.ToString(dataGridView.Rows[i].Cells[4].Value), new Font("Calibri", 9), new SolidBrush(Color.Black), startX + 560, startY + Offset);
                Offset = Offset + 20;
                if (Offset >= e.MarginBounds.Height)
                {
                    i++;
                    e.HasMorePages = true;
                    Offset = 0;
                    return;
                }
                else
                {
                    e.HasMorePages = false;
                }
            }
            e.Graphics.DrawString(underLine, new Font("Courier New", 10, FontStyle.Bold), new SolidBrush(Color.Black), startX, startY + Offset);
            Offset = Offset + 20;
            e.Graphics.DrawString(amt.ToString(), new Font("Calibri", 10), new SolidBrush(Color.Black), startX + 560, startY + Offset);
            Offset = Offset + 20;
            e.Graphics.DrawString(underLine, new Font("Courier New", 10, FontStyle.Bold), new SolidBrush(Color.Black), startX, startY + Offset);
            Offset = Offset + 20;
            e.Graphics.DrawString("Signature:  __________________", new Font("Calibri", 10, FontStyle.Bold), new SolidBrush(Color.Black), startX, startY + Offset);
            e.Graphics.DrawString("Date & Time:  ", new Font("Calibri", 10, FontStyle.Bold), new SolidBrush(Color.Black), startX + 320, startY + Offset);
            e.Graphics.DrawString(DateTime.Now.ToString(), new Font("Calibri", 10), new SolidBrush(Color.Black), startX + 400, startY + Offset);
            Offset = Offset + 25;
            e.Graphics.DrawString("Developed By: Geeks IT Solutions 03126424443  ", new Font("Calibri", 8, FontStyle.Bold), new SolidBrush(Color.Black), startX + 280, startY + Offset + 5);
            e.Graphics.PageUnit = GraphicsUnit.Inch;
            i = 0;
        }
    }
}
