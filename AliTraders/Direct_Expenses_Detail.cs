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
    public partial class Direct_Expenses_Detail : Form
    {
        Form opener;
        Decimal tdebit, tcredit, pre_balance, amt;
        UF.Class1 test = new UF.Class1();
        private int i = 0;

        public Direct_Expenses_Detail(Form form)
        {
            InitializeComponent();
            opener = form;
        }

        private void Direct_Expenses_Detail_Load(object sender, EventArgs e)
        {
            opener.Enabled = false;
        }

        private void Direct_Expenses_Detail_FormClosing(object sender, FormClosingEventArgs e)
        {
            opener.Enabled = true;
        }

        private void btnsearch_Click(object sender, EventArgs e)
        {
            prebalance();
        }

        void prebalance()
        {
            amt = 0;
            Login.con.Open();
            SqlCommand cm2 = new SqlCommand("Delete prebalance", Login.con);
            cm2.ExecuteNonQuery();
            SqlCommand cm = new SqlCommand("SELECT c_id,c_name from customer where type='EXPENSE'", Login.con);
            SqlDataAdapter da = new SqlDataAdapter(cm);
            DataTable dt = new DataTable();
            da.Fill(dt);
            foreach (DataRow row in dt.Rows)
            {
                tdebit = tcredit = 0;
                SqlCommand cmd3 = new SqlCommand("select ct.amount Debit from customer c, cus_transaction cus, customer_transaction ct where c.c_id = ct.c_id AND c.type = 'EXPENSE' AND cus.cus_id = ct.cus_id AND c.c_id = '" + row[0] + "' AND cus.datetime Between '" + Convert.ToDateTime(dateTimePicker1.Text) + "' AND '" + Convert.ToDateTime(dateTimePicker2.Text).AddDays(1) + "' AND ct.status = 'PAID'", Login.con);
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
                SqlCommand cmd4 = new SqlCommand("select ct.amount Credit from customer c, cus_transaction cus, customer_transaction ct where c.c_id = ct.c_id AND c.type = 'EXPENSE' AND cus.cus_id = ct.cus_id AND c.c_id = '" + row[0] + "'AND cus.datetime Between '" + Convert.ToDateTime(dateTimePicker1.Text) + "' AND '" + Convert.ToDateTime(dateTimePicker2.Text).AddDays(1) + "' AND ct.status = 'RECEIVED'", Login.con);
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
                pre_balance = ((tdebit) - (tcredit));
                if (pre_balance > 0)
                {
                    SqlCommand cm3 = new SqlCommand("Insert into prebalance(name,balance) values('" + row[1].ToString() + "','" + pre_balance + "')", Login.con);
                    cm3.ExecuteNonQuery();
                }
            }
            SqlCommand cmd8 = new SqlCommand("Select isnull(sum(balance),0) From prebalance", Login.con);
            lbpaycus.Text = (amt = (Decimal)cmd8.ExecuteScalar()).ToString();
            Login.con.Close();
            test.bindingcode("SELECT name AC_Name, balance Amount from prebalance", dGViewCus, Login.con);
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
            e.Graphics.DrawString("Direct Expenses Detail", new Font("Calibri", 12, FontStyle.Underline), new SolidBrush(Color.Black), startX, startY + Offset);
            Offset = Offset + 30;
            string underLine = "--------------------------------------------------------------------------------";
            e.Graphics.DrawString(underLine, new Font("Courier New", 10, FontStyle.Bold), new SolidBrush(Color.Black), startX, startY + Offset);
            Offset = Offset + 20;
            e.Graphics.DrawString("Sr#", new Font("Calibri", 10, FontStyle.Bold), new SolidBrush(Color.Black), startX, startY + Offset);
            e.Graphics.DrawString("A/c Name", new Font("Calibri", 10, FontStyle.Bold), new SolidBrush(Color.Black), startX + 50, startY + Offset);
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
