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
    public partial class Breakage_Detail : Form
    {
        Decimal invoice_no, amt;
        UF.Class1 test = new UF.Class1();

        public Breakage_Detail(decimal Invoice_No)
        {
            InitializeComponent();
            invoice_no = Invoice_No;
            labelinvoiveno.Text = Convert.ToString(Invoice_No);
        }

        private void Breakage_Detail_Load(object sender, EventArgs e)
        {
            amt = 0;
            Login.con.Open();
            SqlCommand cmd = new SqlCommand("select * from breakage_stock where b_id='" + invoice_no + "'", Login.con);
            SqlDataReader dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                labeldc.Text = Convert.ToString(dr[1]);
                labeldatetime.Text = Convert.ToString(dr[2]);
            }
            dr.Close();
            Login.con.Close();
            //string command = "select s.p_id P_Code,s.tone Tone,s.quality Quality,s.size Size,s.qty Boxes,s.tiles Tiles, CAST(ROUND((p.meters*((s.tiles / p.pieces) + s.qty)),2)as decimal(18,2)) Meters, ps.pprice Price, ps.discount Discount, CAST(ROUND((((s.qty + (s.tiles / p.pieces)) * (ps.pprice)) - (((s.qty + (s.tiles / p.pieces)) * (ps.pprice) * (ps.discount))/100)),2) as decimal(18,2)) Amount From breakage_detail s, breakage_stock bd , product p, purchase_stock_detail ps Where s.p_id=p.p_id AND ps.p_id=s.p_id AND ps.p_id=p.p_id AND s.size=p.size AND s.quality=ps.quality AND ps.ps_id=bd.ps_id AND s.b_id=bd.b_id AND s.b_id ='" + invoice_no + "' AND bd.ps_id='" + Convert.ToDecimal(labeldc.Text) + "'";
            //test.bindingcode(command, dataGridView, Login.con);
            string command2 = "select s.p_id P_Code,p.p_name P_Name,s.qty Quantity, ps.pprice Price, ps.discount Discount,CAST(ROUND((((s.qty) * (ps.pprice)) - (((s.qty) * (ps.pprice) * (ps.discount))/100)),2) as decimal(18,2)) Amount From breakage_detail2 s, breakage_stock bd, product2 p, purchase_stock_detail2 ps Where s.p_id=p.p_id AND ps.p_id=s.p_id AND ps.p_id=p.p_id AND ps.ps_id=bd.ps_id AND s.b_id=bd.b_id AND s.b_id ='" + invoice_no + "' AND bd.ps_id='" + Convert.ToDecimal(labeldc.Text) + "'";
            test.bindingcode(command2, dataGridView2, Login.con);
            //for (int i = 0; i < dataGridView.RowCount; i++)
            //{
            //    amt += Convert.ToDecimal(dataGridView.Rows[i].Cells[9].Value);
            //}
            for (int i = 0; i < dataGridView2.RowCount; i++)
            {
                amt += Convert.ToDecimal(dataGridView2.Rows[i].Cells[5].Value);
            }
            labeltotal.Text = amt.ToString();
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

        private int i, j = 0;
        private void printDocument1_PrintPage(object sender, System.Drawing.Printing.PrintPageEventArgs e)
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
            e.Graphics.DrawString("DC No:", new Font("Calibri", 12), new SolidBrush(Color.Black), startX, startY + Offset);
            e.Graphics.DrawString(labeldc.Text, new Font("Calibri", 12, FontStyle.Bold), new SolidBrush(Color.Black), startX + 55, startY + Offset);
            Offset = Offset + 30;
            e.Graphics.DrawString("Breakage No:", new Font("Calibri", 10, FontStyle.Bold), new SolidBrush(Color.Black), startX, startY + Offset);
            e.Graphics.DrawString(labelinvoiveno.Text, new Font("Calibri", 10), new SolidBrush(Color.Black), startX + 65, startY + Offset);
            Offset = Offset + 20;
            string underLine = "=======================================================================================";
            //if (dataGridView.Rows.Count != 0)
            //{
            //    e.Graphics.DrawString(underLine, new Font("Courier New", 10, FontStyle.Bold), new SolidBrush(Color.Black), startX, startY + Offset);
            //    Offset = Offset + 20;
            //    e.Graphics.DrawString("Sr#", new Font("Calibri", 10, FontStyle.Bold), new SolidBrush(Color.Black), startX, startY + Offset);
            //    e.Graphics.DrawString("P_Code", new Font("Calibri", 10, FontStyle.Bold), new SolidBrush(Color.Black), startX + 45, startY + Offset);
            //    e.Graphics.DrawString("Tone", new Font("Calibri", 10, FontStyle.Bold), new SolidBrush(Color.Black), startX + 215, startY + Offset);
            //    e.Graphics.DrawString("Quality", new Font("Calibri", 10, FontStyle.Bold), new SolidBrush(Color.Black), startX + 280, startY + Offset);
            //    e.Graphics.DrawString("Size", new Font("Calibri", 10, FontStyle.Bold), new SolidBrush(Color.Black), startX + 360, startY + Offset);
            //    e.Graphics.DrawString("Box/T", new Font("Calibri", 10, FontStyle.Bold), new SolidBrush(Color.Black), startX + 410, startY + Offset);
            //    e.Graphics.DrawString("Meters", new Font("Calibri", 10, FontStyle.Bold), new SolidBrush(Color.Black), startX + 465, startY + Offset);
            //    Offset = Offset + 20;
            //    e.Graphics.DrawString(underLine, new Font("Courier New", 10, FontStyle.Bold), new SolidBrush(Color.Black), startX, startY + Offset);
            //    Offset = Offset + 20;
            //    int a = dataGridView.Rows.Count;
            //    for (; i < a; i++)
            //    {
            //        e.Graphics.DrawString(Convert.ToString(i + 1), new Font("Calibri", 9), new SolidBrush(Color.Black), startX, startY + Offset);
            //        e.Graphics.DrawString(Convert.ToString(dataGridView.Rows[i].Cells[0].Value), new Font("Calibri", 9), new SolidBrush(Color.Black), startX + 45, startY + Offset);
            //        e.Graphics.DrawString(Convert.ToString(dataGridView.Rows[i].Cells[1].Value), new Font("Calibri", 9), new SolidBrush(Color.Black), startX + 215, startY + Offset);
            //        e.Graphics.DrawString(Convert.ToString(dataGridView.Rows[i].Cells[2].Value), new Font("Calibri", 9), new SolidBrush(Color.Black), startX + 280, startY + Offset);
            //        e.Graphics.DrawString(Convert.ToString(dataGridView.Rows[i].Cells[3].Value), new Font("Calibri", 9), new SolidBrush(Color.Black), startX + 360, startY + Offset);
            //        e.Graphics.DrawString(Convert.ToString(dataGridView.Rows[i].Cells[4].Value + " - " + dataGridView.Rows[i].Cells[5].Value), new Font("Calibri", 9), new SolidBrush(Color.Black), startX + 410, startY + Offset);
            //        e.Graphics.DrawString(Convert.ToString(dataGridView.Rows[i].Cells[6].Value), new Font("Calibri", 9), new SolidBrush(Color.Black), startX + 465, startY + Offset);
            //        Offset = Offset + 20;
            //        if (Offset >= e.MarginBounds.Height)
            //        {
            //            i++;
            //            e.HasMorePages = true;
            //            Offset = 0;
            //            return;
            //        }
            //        else
            //        {
            //            e.HasMorePages = false;
            //        }
            //    }
            //    Offset = Offset + 50;
            //}
            if (dataGridView2.Rows.Count != 0)
            {
                //if (dataGridView.Rows.Count != 0)
                //    Offset = Offset - 50;
                e.Graphics.DrawString(underLine, new Font("Courier New", 10, FontStyle.Bold), new SolidBrush(Color.Black), startX, startY + Offset);
                Offset = Offset + 20;
                e.Graphics.DrawString("Sr#", new Font("Calibri", 10, FontStyle.Bold), new SolidBrush(Color.Black), startX, startY + Offset);
                e.Graphics.DrawString("P_Name", new Font("Calibri", 10, FontStyle.Bold), new SolidBrush(Color.Black), startX + 45, startY + Offset);
                e.Graphics.DrawString("Qty", new Font("Calibri", 10, FontStyle.Bold), new SolidBrush(Color.Black), startX + 410, startY + Offset);
                Offset = Offset + 20;
                e.Graphics.DrawString(underLine, new Font("Courier New", 10, FontStyle.Bold), new SolidBrush(Color.Black), startX, startY + Offset);
                Offset = Offset + 20;
                int a = dataGridView2.Rows.Count;
                for (; j < a; j++)
                {
                    e.Graphics.DrawString(Convert.ToString(j + 1), new Font("Calibri", 9), new SolidBrush(Color.Black), startX, startY + Offset);
                    e.Graphics.DrawString(Convert.ToString(dataGridView2.Rows[j].Cells[1].Value), new Font("Calibri", 9), new SolidBrush(Color.Black), startX + 45, startY + Offset);
                    e.Graphics.DrawString(Convert.ToString(dataGridView2.Rows[j].Cells[2].Value), new Font("Calibri", 9), new SolidBrush(Color.Black), startX + 410, startY + Offset);
                    Offset = Offset + 20;
                    if (Offset >= e.MarginBounds.Height)
                    {
                        j++;
                        e.HasMorePages = true;
                        Offset = 0;
                        return;
                    }
                    else
                    {
                        e.HasMorePages = false;
                    }
                }
                Offset = Offset + 50;
            }
            e.Graphics.DrawString(underLine, new Font("Courier New", 10, FontStyle.Bold), new SolidBrush(Color.Black), startX, startY + Offset);
            Offset = Offset + 20;
            e.Graphics.DrawString("Signature:  __________________", new Font("Calibri", 10, FontStyle.Bold), new SolidBrush(Color.Black), startX, startY + Offset);
            e.Graphics.DrawString("Date & Time:  ", new Font("Calibri", 10, FontStyle.Bold), new SolidBrush(Color.Black), startX + 320, startY + Offset);
            e.Graphics.DrawString(labeldatetime.Text, new Font("Calibri", 10), new SolidBrush(Color.Black), startX + 400, startY + Offset);
            Offset = Offset + 25;
            e.Graphics.DrawString("Developed By: Geeks IT Solutions 03126424443  ", new Font("Calibri", 8, FontStyle.Bold), new SolidBrush(Color.Black), startX + 280, startY + Offset + 5);
            e.Graphics.PageUnit = GraphicsUnit.Inch;
            i = j = 0;
        }
    }
}
