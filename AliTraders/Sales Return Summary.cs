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
    public partial class Sales_Return_Summary : Form
    {
        Form opener;
        Decimal amt;
        UF.Class1 test = new UF.Class1();

        public Sales_Return_Summary(Form from)
        {
            InitializeComponent();
            opener = from;
        }

        private void Sales_Return_Summary_Load(object sender, EventArgs e)
        {
            opener.Enabled = false;
        }

        private void Sales_Return_Summary_FormClosing(object sender, FormClosingEventArgs e)
        {
            opener.Enabled = true;
        }

        private void btnsearch_Click(object sender, EventArgs e)
        {
            amt = 0;
            test.bindingcode("select case when c.type='FIXED' then 'Customer' else 'Stakeholder' end as AC_Type, c.c_name AC_Name, isnull(sum(s.total-s.deduct),0) Amount from customer c, sale_return s where s.c_id=c.c_id AND (datetime Between '" + Convert.ToDateTime(dateTimePicker1.Text) + "' AND '" + Convert.ToDateTime(dateTimePicker2.Text).AddDays(1) + "') group by c.c_name,c.type UNION ALL select 'Manufacturer' AC_Type, m.name AC_Name, isnull(sum(s.total-deduct),0) Amount from manufacturer m, sale_return s where s.m_id=m.m_id AND (datetime Between '" + Convert.ToDateTime(dateTimePicker1.Text) + "' AND '" + Convert.ToDateTime(dateTimePicker2.Text).AddDays(1) + "') group by m.name", dataGridView, Login.con);
            for (int i = 0; i < dataGridView.RowCount; i++)
            {
                amt += Convert.ToDecimal(dataGridView.Rows[i].Cells[2].Value);
            }
            lbsales.Text = amt.ToString();
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
            e.Graphics.DrawString("Sales Return Summary", new Font("Calibri", 12, FontStyle.Underline), new SolidBrush(Color.Black), startX, startY + Offset);
            Offset = Offset + 30;
            string underLine = "--------------------------------------------------------------------------------";
            e.Graphics.DrawString(underLine, new Font("Courier New", 10, FontStyle.Bold), new SolidBrush(Color.Black), startX, startY + Offset);
            Offset = Offset + 20;
            e.Graphics.DrawString("Sr#", new Font("Calibri", 10, FontStyle.Bold), new SolidBrush(Color.Black), startX, startY + Offset);
            e.Graphics.DrawString("A/c Type", new Font("Calibri", 10, FontStyle.Bold), new SolidBrush(Color.Black), startX + 50, startY + Offset);
            e.Graphics.DrawString("A/c Name", new Font("Calibri", 10, FontStyle.Bold), new SolidBrush(Color.Black), startX + 190, startY + Offset);
            e.Graphics.DrawString("Amount", new Font("Calibri", 10, FontStyle.Bold), new SolidBrush(Color.Black), startX + 390, startY + Offset);
            Offset = Offset + 20;
            e.Graphics.DrawString(underLine, new Font("Courier New", 10, FontStyle.Bold), new SolidBrush(Color.Black), startX, startY + Offset);
            Offset = Offset + 20;
            int a = dataGridView.Rows.Count;
            for (; i < a; i++)
            {
                e.Graphics.DrawString(Convert.ToString(i + 1), new Font("Calibri", 9), new SolidBrush(Color.Black), startX, startY + Offset);
                e.Graphics.DrawString(Convert.ToString(dataGridView.Rows[i].Cells[0].Value), new Font("Calibri", 9), new SolidBrush(Color.Black), startX + 50, startY + Offset);
                e.Graphics.DrawString(Convert.ToString(dataGridView.Rows[i].Cells[1].Value), new Font("Calibri", 9), new SolidBrush(Color.Black), startX + 190, startY + Offset);
                e.Graphics.DrawString(Convert.ToString(dataGridView.Rows[i].Cells[2].Value), new Font("Calibri", 9), new SolidBrush(Color.Black), startX + 390, startY + Offset);
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
