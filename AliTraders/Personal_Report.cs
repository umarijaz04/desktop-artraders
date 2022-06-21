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
    public partial class Personal_Report : Form
    {
        Form opener;
        Decimal debit, credit;
        UF.Class1 test = new UF.Class1();

        public Personal_Report(Form from)
        {
            InitializeComponent();
            opener = from;
        }

        private void Personal_Report_Load(object sender, EventArgs e)
        {
            opener.Enabled = false;
            string command = "select c_name from customer where type='PERSONAL'";
            test.cmbox_drop(command, cmbname, Login.con);
        }

        private void Personal_Report_FormClosing(object sender, FormClosingEventArgs e)
        {
            opener.Enabled = true;
        }

        private void cmbname_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                cmbname.Text = cmbname.Text.ToUpper();
                cmbname.SelectAll();
            }
        }

        private void btnsearch_Click(object sender, EventArgs e)
        {
            if (cmbname.Text != "")
            {
                string command = "select cus.cus_id CT_ID,c.c_name Name,cus.datetime DateTime,'Sales' Account,Convert(varchar(50),ss.ss_id) Description, ss.amount Debit ,ss.receive Credit,CASE WHEN ss.pre_balance < 0 THEN 'Credit' ELSE 'Debit' END as Status, ss.pre_balance Balance from customer c, cus_transaction cus,sale_stock ss where c.c_id=ss.c_id AND c.type='personal' AND cus.cus_id=ss.cus_id AND c.c_name='" + cmbname.Text.ToUpper() + "'AND(cus.datetime Between '" + Convert.ToDateTime(dateTimePicker1.Text) + "' AND '" + Convert.ToDateTime(dateTimePicker2.Text).AddDays(1) + "') UNION select cus.cus_id CT_ID, c.c_name Name, cus.datetime DateTime,'Sales Return' Account,Convert(varchar(50), sr.sr_id) Description, sr.Paid Debit, sr.amount Credit, CASE WHEN sr.pre_balance < 0 THEN 'Credit' ELSE 'Debit' END as Status, sr.pre_balance Balance from customer c, cus_transaction cus, sale_return sr where c.c_id = sr.c_id AND c.type = 'personal' AND cus.cus_id = sr.cus_id AND c.c_name = '" + cmbname.Text.ToUpper() + "'AND(cus.datetime Between '" + Convert.ToDateTime(dateTimePicker1.Text) + "' AND '" + Convert.ToDateTime(dateTimePicker2.Text).AddDays(1) + "') UNION (select cus.cus_id CT_ID, c.c_name Name, cus.datetime DateTime, 'Transaction' Account, ct.description Description, '0' Credit, ct.amount Debit, CASE WHEN ct.balance < 0 THEN 'Credit' ELSE 'Debit' END as Status, ct.balance Balance from customer c, cus_transaction cus, customer_transaction ct where c.c_id = ct.c_id AND c.type = 'personal' AND cus.cus_id = ct.cus_id AND c.c_name = '" + cmbname.Text.ToUpper() + "'AND(cus.datetime Between '" + Convert.ToDateTime(dateTimePicker1.Text) + "' AND '" + Convert.ToDateTime(dateTimePicker2.Text).AddDays(1) + "') AND ct.status = 'RECEIVED' UNION ALL select cus.cus_id CT_ID, c.c_name Name, cus.datetime DateTime, 'Transaction' Account, ct.description Description, ct.amount Credit, '0' Debit, CASE WHEN ct.balance < 0 THEN 'Credit' ELSE 'Debit' END as Status, ct.balance Balance from customer c, cus_transaction cus, customer_transaction ct where c.c_id = ct.c_id AND c.type = 'personal' AND cus.cus_id = ct.cus_id AND c.c_name = '" + cmbname.Text.ToUpper() + "'AND(cus.datetime Between '" + Convert.ToDateTime(dateTimePicker1.Text) + "' AND '" + Convert.ToDateTime(dateTimePicker2.Text).AddDays(1) + "') AND ct.status = 'PAID')";
                test.bindingcode(command, dataGridView, Login.con);
            }
            else
            {
                MessageBox.Show("Please Enter Personal Name First", "AR Tranders", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                debit = credit = 0;
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
            e.Graphics.DrawString("Personal Report", new Font("Calibri", 12, FontStyle.Underline), new SolidBrush(Color.Black), startX, startY + Offset);
            Offset = Offset + 30;
            e.Graphics.DrawString("Personal Name:", new Font("Calibri", 10, FontStyle.Bold), new SolidBrush(Color.Black), startX, startY + Offset);
            e.Graphics.DrawString(cmbname.Text, new Font("Calibri", 10), new SolidBrush(Color.Black), startX + 105, startY + Offset);
            Offset = Offset + 30;
            e.Graphics.DrawString("From:", new Font("Calibri", 10, FontStyle.Bold), new SolidBrush(Color.Black), startX, startY + Offset);
            e.Graphics.DrawString(dateTimePicker1.Text, new Font("Calibri", 10), new SolidBrush(Color.Black), startX + 40, startY + Offset);
            e.Graphics.DrawString("To:", new Font("Calibri", 10, FontStyle.Bold), new SolidBrush(Color.Black), startX + 380, startY + Offset);
            e.Graphics.DrawString(dateTimePicker2.Text, new Font("Calibri", 10), new SolidBrush(Color.Black), startX + 405, startY + Offset);
            Offset = Offset + 20;
            string underLine = "=======================================================================================";
            e.Graphics.DrawString(underLine, new Font("Courier New", 10, FontStyle.Bold), new SolidBrush(Color.Black), startX, startY + Offset);
            Offset = Offset + 20;
            e.Graphics.DrawString("CT_ID", new Font("Calibri", 10, FontStyle.Bold), new SolidBrush(Color.Black), startX, startY + Offset);
            e.Graphics.DrawString("Date & Time", new Font("Calibri", 10, FontStyle.Bold), new SolidBrush(Color.Black), startX + 60, startY + Offset);
            e.Graphics.DrawString("Account", new Font("Calibri", 10, FontStyle.Bold), new SolidBrush(Color.Black), startX + 330 - 115, startY + Offset);
            e.Graphics.DrawString("Invoice/Des", new Font("Calibri", 10, FontStyle.Bold), new SolidBrush(Color.Black), startX + 300, startY + Offset);
            e.Graphics.DrawString("Debit", new Font("Calibri", 10, FontStyle.Bold), new SolidBrush(Color.Black), startX + 425, startY + Offset);
            e.Graphics.DrawString("Credit", new Font("Calibri", 10, FontStyle.Bold), new SolidBrush(Color.Black), startX + 515, startY + Offset);
            e.Graphics.DrawString("Status", new Font("Calibri", 10, FontStyle.Bold), new SolidBrush(Color.Black), startX + 605, startY + Offset);
            e.Graphics.DrawString("Balance", new Font("Calibri", 10, FontStyle.Bold), new SolidBrush(Color.Black), startX + 670, startY + Offset);
            Offset = Offset + 20;
            e.Graphics.DrawString(underLine, new Font("Courier New", 10, FontStyle.Bold), new SolidBrush(Color.Black), startX, startY + Offset);
            Offset = Offset + 20;
            int a = dataGridView.Rows.Count;
            for (; i < a; i++)
            {
                e.Graphics.DrawString(Convert.ToString(dataGridView.Rows[i].Cells[0].Value), new Font("Calibri", 8), new SolidBrush(Color.Black), startX, startY + Offset);
                e.Graphics.DrawString(Convert.ToString(dataGridView.Rows[i].Cells[2].Value), new Font("Calibri", 8), new SolidBrush(Color.Black), startX + 60, startY + Offset);
                e.Graphics.DrawString(Convert.ToString(dataGridView.Rows[i].Cells[3].Value), new Font("Calibri", 8), new SolidBrush(Color.Black), startX + 330 - 115, startY + Offset);
                e.Graphics.DrawString(Convert.ToString(dataGridView.Rows[i].Cells[4].Value), new Font("Calibri", 8), new SolidBrush(Color.Black), startX + 300, startY + Offset);
                if (Convert.ToString(dataGridView.Rows[i].Cells[5].Value) != "0.00")
                {
                    e.Graphics.DrawString(Convert.ToString(dataGridView.Rows[i].Cells[5].Value), new Font("Calibri", 8), new SolidBrush(Color.Black), startX + 425, startY + Offset);
                    debit += Convert.ToDecimal(dataGridView.Rows[i].Cells[5].Value);
                }
                else
                    e.Graphics.DrawString("", new Font("Calibri", 8), new SolidBrush(Color.Black), startX + 425, startY + Offset);
                if (Convert.ToString(dataGridView.Rows[i].Cells[6].Value) != "0.00")
                {
                    e.Graphics.DrawString(Convert.ToString(dataGridView.Rows[i].Cells[6].Value), new Font("Calibri", 8), new SolidBrush(Color.Black), startX + 515, startY + Offset);
                    credit += Convert.ToDecimal(dataGridView.Rows[i].Cells[6].Value);
                }
                else
                    e.Graphics.DrawString("", new Font("Calibri", 8), new SolidBrush(Color.Black), startX + 515, startY + Offset);
                e.Graphics.DrawString(Convert.ToString(dataGridView.Rows[i].Cells[7].Value), new Font("Calibri", 8), new SolidBrush(Color.Black), startX + 605, startY + Offset);
                if (Convert.ToString(dataGridView.Rows[i].Cells[8].Value).Contains("-"))
                    e.Graphics.DrawString(Convert.ToString(dataGridView.Rows[i].Cells[8].Value).Substring(1), new Font("Calibri", 8), new SolidBrush(Color.Black), startX + 670, startY + Offset);
                else
                    e.Graphics.DrawString(Convert.ToString(dataGridView.Rows[i].Cells[8].Value), new Font("Calibri", 8), new SolidBrush(Color.Black), startX + 670, startY + Offset);
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
            e.Graphics.DrawString(Convert.ToString(debit), new Font("Calibri", 8), new SolidBrush(Color.Black), startX + 425, startY + Offset);
            e.Graphics.DrawString(Convert.ToString(credit), new Font("Calibri", 8), new SolidBrush(Color.Black), startX + 515, startY + Offset);
            if (Convert.ToString(debit - credit).Contains("-"))
                e.Graphics.DrawString("Cr. " + Convert.ToString(debit - credit).Substring(1), new Font("Calibri", 8), new SolidBrush(Color.Black), startX + 670, startY + Offset);
            else
                e.Graphics.DrawString("Dr. " + Convert.ToString(debit - credit), new Font("Calibri", 8), new SolidBrush(Color.Black), startX + 670, startY + Offset);
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
