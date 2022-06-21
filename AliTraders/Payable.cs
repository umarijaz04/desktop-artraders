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
using System.Drawing.Printing;

namespace AhmadSanitary
{
    public partial class Payable : Form
    {
        Form opener;
        Decimal balance, sdebit, scredit, prcredit, prdebit, srdebit, srcredit, tdebit, tcredit, pre_balance, pcredit, pdebit, amt;
        UF.Class1 test = new UF.Class1();
        private int i = 0;

        public Payable(Form form)
        {
            InitializeComponent();
            opener = form;
        }

        private void Payable_Load(object sender, EventArgs e)
        {
            opener.Enabled = false;
            cmbaddcustomer.SelectedIndex = 0;
        }

        private void Payable_FormClosing(object sender, FormClosingEventArgs e)
        {
            opener.Enabled = true;
        }

        private void toolStripMenuItem2_Click(object sender, EventArgs e)
        {
            panelmanu.Visible = false;
            cmbaddcustomer.SelectedIndex = 0;
        }

        private void toolStripMenuItem1_Click(object sender, EventArgs e)
        {
            panelmanu.Visible = true;
        }

        private void cmbaddcustomer_SelectedIndexChanged(object sender, EventArgs e)
        {
            panelmanu.Visible = false;
        }

        private void btnsearch_Click(object sender, EventArgs e)
        {
            prebalance();
        }

        private void btnsearch2_Click(object sender, EventArgs e)
        {
            prebalance();
        }

        void prebalance()
        {
            amt = 0;
            if (panelmanu.Visible == false)
            {
                Login.con.Open();
                SqlCommand cm2 = new SqlCommand("Delete prebalance", Login.con);
                cm2.ExecuteNonQuery();
                SqlCommand cm = new SqlCommand("SELECT c_id,c_name,contact from customer where type='" + cmbaddcustomer.Text.ToUpper() + "'", Login.con);
                SqlDataAdapter da = new SqlDataAdapter(cm);
                DataTable dt = new DataTable();
                da.Fill(dt);
                foreach (DataRow row in dt.Rows)
                {
                    sdebit = scredit = srdebit = srcredit = prcredit = prdebit = tdebit = pcredit = pdebit = tcredit = 0;
                    SqlCommand cmd = new SqlCommand("select (ss.total-ss.discount) Debit ,ss.receive Credit from customer c, cus_transaction cus,sale_stock ss where c.c_id=ss.c_id AND c.type='" + cmbaddcustomer.Text.ToUpper() + "' AND cus.cus_id=ss.cus_id AND c.c_id='" + row[0] + "'AND cus.datetime Between '" + Convert.ToDateTime(dateTimePicker1.Text) + "' AND '" + Convert.ToDateTime(dateTimePicker2.Text).AddDays(1) + "'", Login.con);
                    SqlDataReader dr = cmd.ExecuteReader();
                    if (dr.HasRows)
                    {
                        while (dr.Read())
                        {
                            sdebit += Convert.ToDecimal(dr[0]);
                            scredit += Convert.ToDecimal(dr[1]);
                        }
                    }
                    else
                    {
                        dr.Close();
                        sdebit = scredit = 0;
                    }
                    dr.Close();
                    SqlCommand cmd7 = new SqlCommand("select ps.paid Debit,ps.total Credit from customer c, cus_transaction cus,purchase_stock ps where c.c_id=ps.c_id AND cus.cus_id=ps.cus_id AND c.c_id='" + row[0] + "' AND cus.datetime Between '" + Convert.ToDateTime(dateTimePicker1.Text) + "' AND '" + Convert.ToDateTime(dateTimePicker2.Text).AddDays(1) + "'", Login.con);
                    SqlDataReader dr7 = cmd7.ExecuteReader();
                    if (dr7.HasRows)
                    {
                        while (dr7.Read())
                        {
                            pdebit += Convert.ToDecimal(dr7[0]);
                            pcredit += Convert.ToDecimal(dr7[1]);
                        }
                    }
                    else
                    {
                        dr7.Close();
                        pdebit = pcredit = 0;
                    }
                    dr7.Close();
                    SqlCommand cmd2 = new SqlCommand("select sr.Paid Debit, (sr.total-sr.deduct) Credit from customer c, cus_transaction cus, sale_return sr where c.c_id = sr.c_id AND c.type = '" + cmbaddcustomer.Text.ToUpper() + "' AND cus.cus_id = sr.cus_id AND c.c_id='" + row[0] + "'AND cus.datetime  Between '" + Convert.ToDateTime(dateTimePicker1.Text) + "' AND '" + Convert.ToDateTime(dateTimePicker2.Text).AddDays(1) + "'", Login.con);
                    SqlDataReader dr2 = cmd2.ExecuteReader();
                    if (dr2.HasRows)
                    {
                        while (dr2.Read())
                        {
                            srdebit += Convert.ToDecimal(dr2[0]);
                            srcredit += Convert.ToDecimal(dr2[1]);
                        }
                    }
                    else
                    {
                        dr2.Close();
                        srdebit = srcredit = 0;
                    }
                    dr2.Close();
                    SqlCommand cmd22 = new SqlCommand("select pr.total Debit, pr.receive Credit from customer c, cus_transaction cus, purchase_return pr where c.c_id = pr.c_id AND c.type = '" + cmbaddcustomer.Text.ToUpper() + "' AND cus.cus_id = pr.cus_id AND c.c_id='" + row[0] + "'AND cus.datetime  Between '" + Convert.ToDateTime(dateTimePicker1.Text) + "' AND '" + Convert.ToDateTime(dateTimePicker2.Text).AddDays(1) + "'", Login.con);
                    SqlDataReader dr22 = cmd22.ExecuteReader();
                    if (dr22.HasRows)
                    {
                        while (dr22.Read())
                        {
                            prdebit += Convert.ToDecimal(dr22[0]);
                            prcredit += Convert.ToDecimal(dr22[1]);
                        }
                    }
                    else
                    {
                        dr22.Close();
                        prdebit = prcredit = 0;
                    }
                    dr22.Close();
                    SqlCommand cmd3 = new SqlCommand("select ct.amount Debit from customer c, cus_transaction cus, customer_transaction ct where c.c_id = ct.c_id AND c.type = '" + cmbaddcustomer.Text.ToUpper() + "' AND cus.cus_id = ct.cus_id AND c.c_id='" + row[0] + "'AND cus.datetime Between '" + Convert.ToDateTime(dateTimePicker1.Text) + "' AND '" + Convert.ToDateTime(dateTimePicker2.Text).AddDays(1) + "' AND ct.status = 'PAID'", Login.con);
                    SqlDataReader dr3 = cmd3.ExecuteReader();
                    if (dr3.HasRows)
                    {
                        while (dr3.Read())
                        {
                            tdebit += Convert.ToDecimal(dr3[0]);
                        }
                    }
                    else
                    {
                        dr3.Close();
                        tdebit = 0;
                    }
                    dr3.Close();
                    SqlCommand cmd4 = new SqlCommand("select ct.amount Credit from customer c, cus_transaction cus, customer_transaction ct where c.c_id = ct.c_id AND c.type = '" + cmbaddcustomer.Text.ToUpper() + "' AND cus.cus_id = ct.cus_id AND c.c_id='" + row[0] + "'AND cus.datetime Between '" + Convert.ToDateTime(dateTimePicker1.Text) + "' AND '" + Convert.ToDateTime(dateTimePicker2.Text).AddDays(1) + "' AND ct.status = 'RECEIVED'", Login.con);
                    SqlDataReader dr4 = cmd4.ExecuteReader();
                    if (dr4.HasRows)
                    {
                        while (dr4.Read())
                        {
                            tcredit += Convert.ToDecimal(dr4[0]);
                        }
                    }
                    else
                    {
                        dr4.Close();
                        tcredit = 0;
                    }
                    dr4.Close();
                    pre_balance = ((sdebit + prdebit + tdebit + pdebit + srdebit) - (scredit + prcredit + tcredit + pcredit + srcredit));
                    if (pre_balance < 0)
                    {
                        pre_balance = Convert.ToDecimal(pre_balance.ToString().Substring(1));
                        SqlCommand cm3 = new SqlCommand("Insert into prebalance(name,balance,contact) values('" + row[1].ToString() + "','" + pre_balance + "','" + row[2].ToString() + "')", Login.con);
                        cm3.ExecuteNonQuery();
                    }
                }
                SqlCommand cmd8 = new SqlCommand("Select isnull(sum(balance),0) From prebalance", Login.con);
                lbpaycus.Text = (amt = (Decimal)cmd8.ExecuteScalar()).ToString();
                Login.con.Close();
                test.bindingcode("SELECT name Customer_Name, balance Payable_Balance, contact Contact from prebalance", dGViewCus, Login.con);
            }
            else if (panelmanu.Visible == true)
            {
                Login.con.Open();
                SqlCommand cm2 = new SqlCommand("Delete prebalance", Login.con);
                cm2.ExecuteNonQuery();
                SqlCommand cm = new SqlCommand("SELECT m_id, name, contact from manufacturer", Login.con);
                SqlDataAdapter da = new SqlDataAdapter(cm);
                DataTable dt = new DataTable();
                da.Fill(dt);
                foreach (DataRow row in dt.Rows)
                {
                    balance = sdebit = prdebit = prcredit = scredit = srdebit = srcredit = tdebit = tcredit = pdebit = pcredit = 0;
                    SqlCommand cmd = new SqlCommand("select ps.paid Debit,ps.total Credit from manufacturer m, man_transaction man,purchase_stock ps where m.m_id=ps.m_id AND man.man_id=ps.man_id AND m.m_id='" + row[0] + "' AND man.datetime Between '" + Convert.ToDateTime(dateTimePicker3.Text) + "' AND '" + Convert.ToDateTime(dateTimePicker4.Text).AddDays(1) + "'", Login.con);
                    SqlDataReader dr = cmd.ExecuteReader();
                    if (dr.HasRows)
                    {
                        while (dr.Read())
                        {
                            pdebit += Convert.ToDecimal(dr[0]);
                            pcredit += Convert.ToDecimal(dr[1]);
                        }
                    }
                    else
                    {
                        dr.Close();
                        pdebit = pcredit = 0;
                    }
                    dr.Close();
                    SqlCommand cmd7 = new SqlCommand("select (ss.total-ss.discount) Debit ,ss.receive Credit from manufacturer m, man_transaction man,sale_stock ss where m.m_id=ss.m_id AND man.man_id=ss.man_id AND m.name='" + row[1] + "'AND man.datetime Between '" + Convert.ToDateTime(dateTimePicker3.Text) + "' AND '" + Convert.ToDateTime(dateTimePicker4.Text).AddDays(1) + "'", Login.con);
                    SqlDataReader dr7 = cmd7.ExecuteReader();
                    if (dr7.HasRows)
                    {
                        while (dr7.Read())
                        {
                            sdebit += Convert.ToDecimal(dr7[0]);
                            scredit += Convert.ToDecimal(dr7[1]);
                        }
                    }
                    else
                    {
                        dr7.Close();
                        sdebit = scredit = 0;
                    }
                    dr7.Close();
                    SqlCommand cmd2 = new SqlCommand("select sr.Paid Debit, (sr.total-sr.deduct) Credit from manufacturer m, man_transaction man, sale_return sr where m.m_id = sr.m_id AND man.man_id = sr.man_id AND m.m_id='" + row[0] + "'AND man.datetime  Between '" + Convert.ToDateTime(dateTimePicker3.Text) + "' AND '" + Convert.ToDateTime(dateTimePicker4.Text).AddDays(1) + "'", Login.con);
                    SqlDataReader dr2 = cmd2.ExecuteReader();
                    if (dr2.HasRows)
                    {
                        while (dr2.Read())
                        {
                            srdebit += Convert.ToDecimal(dr2[0]);
                            srcredit += Convert.ToDecimal(dr2[1]);
                        }
                    }
                    else
                    {
                        dr2.Close();
                        srdebit = srcredit = 0;
                    }
                    dr2.Close();
                    SqlCommand cmd22 = new SqlCommand("select pr.total Debit, pr.receive Credit from manufacturer m, man_transaction man, purchase_return pr where m.m_id = pr.m_id AND man.man_id = pr.man_id AND m.m_id='" + row[0] + "'AND man.datetime  Between '" + Convert.ToDateTime(dateTimePicker3.Text) + "' AND '" + Convert.ToDateTime(dateTimePicker4.Text).AddDays(1) + "'", Login.con);
                    SqlDataReader dr22 = cmd22.ExecuteReader();
                    if (dr22.HasRows)
                    {
                        while (dr22.Read())
                        {
                            prdebit += Convert.ToDecimal(dr22[0]);
                            prcredit += Convert.ToDecimal(dr22[1]);
                        }
                    }
                    else
                    {
                        dr22.Close();
                        prdebit = prcredit = 0;
                    }
                    dr22.Close();
                    SqlCommand cmd3 = new SqlCommand("select mt.amount Debit from Manufacturer m, man_transaction man, manu_transaction mt where m.m_id = mt.m_id AND man.man_id = mt.man_id AND m.m_id='" + row[0] + "'AND man.datetime Between '" + Convert.ToDateTime(dateTimePicker3.Text) + "' AND '" + Convert.ToDateTime(dateTimePicker4.Text).AddDays(1) + "' AND mt.status = 'PAID'", Login.con);
                    SqlDataReader dr3 = cmd3.ExecuteReader();
                    if (dr3.HasRows)
                    {
                        while (dr3.Read())
                        {
                            tdebit += Convert.ToDecimal(dr3[0]);
                        }
                    }
                    else
                    {
                        dr3.Close();
                        tdebit = 0;
                    }
                    dr3.Close();
                    SqlCommand cmd4 = new SqlCommand("select mt.amount Credit from Manufacturer m, man_transaction man, manu_transaction mt where m.m_id = mt.m_id AND man.man_id = mt.man_id AND m.m_id='" + row[0] + "'AND man.datetime Between '" + Convert.ToDateTime(dateTimePicker3.Text) + "' AND '" + Convert.ToDateTime(dateTimePicker4.Text).AddDays(1) + "' AND mt.status = 'RECEIVED'", Login.con);
                    SqlDataReader dr4 = cmd4.ExecuteReader();
                    if (dr4.HasRows)
                    {
                        while (dr4.Read())
                        {
                            tcredit += Convert.ToDecimal(dr4[0]);
                        }
                    }
                    else
                    {
                        dr4.Close();
                        tcredit = 0;
                    }
                    dr4.Close();
                    pre_balance = ((sdebit + tdebit + pdebit + srdebit + prdebit) - (prcredit + scredit + tcredit + pcredit + srcredit));
                    if (pre_balance < 0)
                    {
                        pre_balance = Convert.ToDecimal(pre_balance.ToString().Substring(1));
                        SqlCommand cm3 = new SqlCommand("Insert into prebalance(name,balance,contact) values('" + row[1].ToString() + "','" + pre_balance + "','" + row[2].ToString() + "')", Login.con);
                        cm3.ExecuteNonQuery();
                    }
                }
                SqlCommand cmd8 = new SqlCommand("Select isnull(sum(balance),0) From prebalance", Login.con);
                lbpaymanu.Text = (amt = (Decimal)cmd8.ExecuteScalar()).ToString();
                Login.con.Close();
                test.bindingcode("SELECT name Manufacturer_Name, balance Payable_Balance, contact Contact from prebalance", dGViewManu, Login.con);
            }
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

        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                printDocument2.Print();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void printDocument2_PrintPage(object sender, PrintPageEventArgs e)
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
            e.Graphics.DrawString("Manufacturer Payable Balance", new Font("Calibri", 12, FontStyle.Underline), new SolidBrush(Color.Black), startX, startY + Offset);
            Offset = Offset + 30;
            string underLine = "--------------------------------------------------------------------------------";
            e.Graphics.DrawString(underLine, new Font("Courier New", 10, FontStyle.Bold), new SolidBrush(Color.Black), startX, startY + Offset);
            Offset = Offset + 20;
            e.Graphics.DrawString("Sr#", new Font("Calibri", 10, FontStyle.Bold), new SolidBrush(Color.Black), startX, startY + Offset);
            e.Graphics.DrawString("Manufacturer Name", new Font("Calibri", 10, FontStyle.Bold), new SolidBrush(Color.Black), startX + 50, startY + Offset);
            e.Graphics.DrawString("Balance", new Font("Calibri", 10, FontStyle.Bold), new SolidBrush(Color.Black), startX + 390, startY + Offset);
            e.Graphics.DrawString("Contact", new Font("Calibri", 10, FontStyle.Bold), new SolidBrush(Color.Black), startX + 490, startY + Offset);
            Offset = Offset + 20;
            e.Graphics.DrawString(underLine, new Font("Courier New", 10, FontStyle.Bold), new SolidBrush(Color.Black), startX, startY + Offset);
            Offset = Offset + 20;
            int a = dGViewManu.Rows.Count;
            for (; i < a; i++)
            {
                e.Graphics.DrawString(Convert.ToString(i + 1), new Font("Calibri", 9), new SolidBrush(Color.Black), startX, startY + Offset);
                e.Graphics.DrawString(Convert.ToString(dGViewManu.Rows[i].Cells[0].Value), new Font("Calibri", 9), new SolidBrush(Color.Black), startX + 50, startY + Offset);
                e.Graphics.DrawString(Convert.ToString(dGViewManu.Rows[i].Cells[1].Value), new Font("Calibri", 9), new SolidBrush(Color.Black), startX + 390, startY + Offset);
                e.Graphics.DrawString(Convert.ToString(dGViewManu.Rows[i].Cells[2].Value), new Font("Calibri", 9), new SolidBrush(Color.Black), startX + 490, startY + Offset);
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
            Login.con.Open();
            e.Graphics.DrawString(amt.ToString(), new Font("Calibri", 10), new SolidBrush(Color.Black), startX + 390, startY + Offset);
            Login.con.Close();
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
            e.Graphics.DrawString("" + cmbaddcustomer.Text + " Customer Payable Balance", new Font("Calibri", 12, FontStyle.Underline), new SolidBrush(Color.Black), startX, startY + Offset);
            Offset = Offset + 30;
            string underLine = "--------------------------------------------------------------------------------";
            e.Graphics.DrawString(underLine, new Font("Courier New", 10, FontStyle.Bold), new SolidBrush(Color.Black), startX, startY + Offset);
            Offset = Offset + 20;
            e.Graphics.DrawString("Sr#", new Font("Calibri", 10, FontStyle.Bold), new SolidBrush(Color.Black), startX, startY + Offset);
            e.Graphics.DrawString("Customer Name", new Font("Calibri", 10, FontStyle.Bold), new SolidBrush(Color.Black), startX + 50, startY + Offset);
            e.Graphics.DrawString("Balance", new Font("Calibri", 10, FontStyle.Bold), new SolidBrush(Color.Black), startX + 390, startY + Offset);
            e.Graphics.DrawString("Contact", new Font("Calibri", 10, FontStyle.Bold), new SolidBrush(Color.Black), startX + 490, startY + Offset);
            Offset = Offset + 20;
            e.Graphics.DrawString(underLine, new Font("Courier New", 10, FontStyle.Bold), new SolidBrush(Color.Black), startX, startY + Offset);
            Offset = Offset + 20;
            int a = dGViewCus.Rows.Count;
            for (; i < a; i++)
            {
                e.Graphics.DrawString(Convert.ToString(i + 1), new Font("Calibri", 9), new SolidBrush(Color.Black), startX, startY + Offset);
                e.Graphics.DrawString(Convert.ToString(dGViewCus.Rows[i].Cells[0].Value), new Font("Calibri", 9), new SolidBrush(Color.Black), startX + 50, startY + Offset);
                e.Graphics.DrawString(Convert.ToString(dGViewCus.Rows[i].Cells[1].Value), new Font("Calibri", 9), new SolidBrush(Color.Black), startX + 390, startY + Offset);
                e.Graphics.DrawString(Convert.ToString(dGViewCus.Rows[i].Cells[2].Value), new Font("Calibri", 9), new SolidBrush(Color.Black), startX + 490, startY + Offset);
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
            Login.con.Open();
            e.Graphics.DrawString(amt.ToString(), new Font("Calibri", 10), new SolidBrush(Color.Black), startX + 390, startY + Offset);
            Login.con.Close();
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
