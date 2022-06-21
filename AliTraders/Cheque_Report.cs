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
    public partial class Cheque_Report : Form
    {
        Form opener;
        Decimal amt;
        UF.Class1 test = new UF.Class1();

        public Cheque_Report(Form from)
        {
            InitializeComponent();
            opener = from;
        }

        private void Sales_Invoices_Load(object sender, EventArgs e)
        {
            opener.Enabled = false;
            string command = "select c_name from customer";
            test.cmbox_drop(command, cmbname, Login.con);
        }

        private void Sales_Invoices_FormClosing(object sender, FormClosingEventArgs e)
        {
            opener.Enabled = true;
        }

        private void btnsearch_Click(object sender, EventArgs e)
        {
            amt = 0;
            if (cmbname.Text != "")
            {
                cmbname.Text = cmbname.Text.ToUpper();
                cmbname.SelectAll();
                string command = "select ch.ch_no Cheque_No, ss.datetime DateTime, c.c_name Customer_Name, ch.bank_name Bank_Name, ch.amount Amount, ch.status Status, ch.ss_id Invoice_No From cheque ch,sale_stock ss,Customer c Where ch.ss_id=ss.ss_id AND ch.c_id=c.c_id AND (ss.datetime Between '" + Convert.ToDateTime(dateTimePicker1.Text) + "' AND '" + Convert.ToDateTime(dateTimePicker2.Text).AddDays(1) + "') AND c.c_name='" + cmbname.Text.ToUpper() + "'";
                test.bindingcode(command, dataGridView, Login.con);
                Login.con.Open();
                SqlCommand cmd = new SqlCommand("Select isnull(sum(ch.amount),0) from cheque ch, customer c,sale_stock ss where ch.ss_id=ss.ss_id AND ch.c_id=c.c_id AND (ss.datetime Between '" + Convert.ToDateTime(dateTimePicker1.Text) + "' AND '" + Convert.ToDateTime(dateTimePicker2.Text).AddDays(1) + "') AND c.c_name='" + cmbname.Text.ToUpper() + "'", Login.con);
                lbcheque.Text = (amt = (Decimal)cmd.ExecuteScalar()).ToString();
                Login.con.Close();
            }
            else
            {
                MessageBox.Show("Please Enter Customer Name First", "AR Tranders", MessageBoxButtons.OK, MessageBoxIcon.Warning);
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
            e.Graphics.DrawString("Cheques Report", new Font("Calibri", 12, FontStyle.Underline), new SolidBrush(Color.Black), startX, startY + Offset);
            Offset = Offset + 30;
            e.Graphics.DrawString("Customer Name:", new Font("Calibri", 10, FontStyle.Bold), new SolidBrush(Color.Black), startX, startY + Offset);
            e.Graphics.DrawString(Convert.ToString(dataGridView.Rows[0].Cells[2].Value), new Font("Calibri", 10), new SolidBrush(Color.Black), startX + 105, startY + Offset);
            Offset = Offset + 20;
            e.Graphics.DrawString("From:", new Font("Calibri", 10, FontStyle.Bold), new SolidBrush(Color.Black), startX, startY + Offset);
            e.Graphics.DrawString(dateTimePicker1.Text, new Font("Calibri", 10), new SolidBrush(Color.Black), startX + 40, startY + Offset);
            e.Graphics.DrawString("To:", new Font("Calibri", 10, FontStyle.Bold), new SolidBrush(Color.Black), startX + 380, startY + Offset);
            e.Graphics.DrawString(dateTimePicker2.Text, new Font("Calibri", 10), new SolidBrush(Color.Black), startX + 405, startY + Offset);
            Offset = Offset + 20;
            string underLine = "--------------------------------------------------------------------------------";
            e.Graphics.DrawString(underLine, new Font("Courier New", 10, FontStyle.Bold), new SolidBrush(Color.Black), startX, startY + Offset);
            Offset = Offset + 20;
            e.Graphics.DrawString("Cheque_No", new Font("Calibri", 10, FontStyle.Bold), new SolidBrush(Color.Black), startX, startY + Offset);
            e.Graphics.DrawString("Date & Time", new Font("Calibri", 11, FontStyle.Bold), new SolidBrush(Color.Black), startX + 130, startY + Offset);
            e.Graphics.DrawString("Bank Name", new Font("Calibri", 10, FontStyle.Bold), new SolidBrush(Color.Black), startX + 300, startY + Offset);
            e.Graphics.DrawString("Amount", new Font("Calibri", 10, FontStyle.Bold), new SolidBrush(Color.Black), startX + 450, startY + Offset);
            e.Graphics.DrawString("Invoice#", new Font("Calibri", 10, FontStyle.Bold), new SolidBrush(Color.Black), startX + 550, startY + Offset);
            e.Graphics.DrawString("Status", new Font("Calibri", 10, FontStyle.Bold), new SolidBrush(Color.Black), startX + 615, startY + Offset);
            Offset = Offset + 20;
            e.Graphics.DrawString(underLine, new Font("Courier New", 10, FontStyle.Bold), new SolidBrush(Color.Black), startX, startY + Offset);
            Offset = Offset + 20;
            int a = dataGridView.Rows.Count;
            for (; i < a; i++)
            {
                e.Graphics.DrawString(Convert.ToString(dataGridView.Rows[i].Cells[0].Value), new Font("Calibri", 9), new SolidBrush(Color.Black), startX, startY + Offset);
                e.Graphics.DrawString(Convert.ToString(dataGridView.Rows[i].Cells[1].Value), new Font("Calibri", 9), new SolidBrush(Color.Black), startX + 130, startY + Offset);
                e.Graphics.DrawString(Convert.ToString(dataGridView.Rows[i].Cells[3].Value), new Font("Calibri", 9), new SolidBrush(Color.Black), startX + 300, startY + Offset);
                e.Graphics.DrawString(Convert.ToString(dataGridView.Rows[i].Cells[4].Value), new Font("Calibri", 9), new SolidBrush(Color.Black), startX + 450, startY + Offset);
                e.Graphics.DrawString(Convert.ToString(dataGridView.Rows[i].Cells[6].Value), new Font("Calibri", 9), new SolidBrush(Color.Black), startX + 550, startY + Offset);
                e.Graphics.DrawString(Convert.ToString(dataGridView.Rows[i].Cells[5].Value), new Font("Calibri", 9), new SolidBrush(Color.Black), startX + 615, startY + Offset);
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
            e.Graphics.DrawString(amt.ToString(), new Font("Calibri", 10), new SolidBrush(Color.Black), startX + 450, startY + Offset);
            Offset = Offset + 20;
            e.Graphics.DrawString(underLine, new Font("Courier New", 10, FontStyle.Bold), new SolidBrush(Color.Black), startX, startY + Offset);
            Offset = Offset + 20;
            e.Graphics.DrawString("Signature:  __________________", new Font("Calibri", 10, FontStyle.Bold), new SolidBrush(Color.Black), startX, startY + Offset);
            e.Graphics.DrawString("Date & Time:  ", new Font("Calibri", 10, FontStyle.Bold), new SolidBrush(Color.Black), startX + 320, startY + Offset);
            e.Graphics.DrawString(DateTime.Now.ToString(), new Font("Calibri", 10), new SolidBrush(Color.Black), startX + 400, startY + Offset);
            Offset = Offset + 25;
            e.Graphics.DrawString("Developed By: Geeks IT Solutions 03126424443  ", new Font("Calibri", 8, FontStyle.Bold), new SolidBrush(Color.Black), startX + 280, startY + Offset + 5);
            e.Graphics.PageUnit = GraphicsUnit.Inch;
        }
    }
}
