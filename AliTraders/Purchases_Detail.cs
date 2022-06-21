﻿using System;
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
    public partial class Purchases_Detail : Form
    {
        Decimal invoice_no, c_id;
        UF.Class1 test = new UF.Class1();

        public Purchases_Detail(decimal Invoice_No)
        {
            InitializeComponent();
            invoice_no = Invoice_No;
            labelinvoiveno.Text = Convert.ToString(Invoice_No);
        }

        private void Purchases_Detail_Load(object sender, EventArgs e)
        {
            Login.con.Open();
            SqlCommand cmd = new SqlCommand("select * from purchase_stock where ps_id='" + invoice_no + "'", Login.con);
            SqlDataReader dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                labeldatetime.Text = Convert.ToString(dr[0]);
                labeltotal.Text = Convert.ToString(dr[2]);
                labelpaid.Text = Convert.ToString(dr[3]);
                labelbalance.Text = Convert.ToString(Convert.ToDecimal(dr[3]) - Convert.ToDecimal(dr[2]));
                if (Convert.ToString(dr[8]) != DBNull.Value.ToString())
                {
                    c_id = Convert.ToDecimal(dr[8]);
                    dr.Close();
                    SqlCommand cmd2 = new SqlCommand("select c_name from customer where c_id='" + c_id + "'", Login.con);
                    SqlDataReader dr2 = cmd2.ExecuteReader();
                    while (dr2.Read())
                    {
                        labelmanuname.Text = Convert.ToString(dr2[0]);
                    }
                    dr2.Close();
                }
                else if (Convert.ToString(dr[1]) != DBNull.Value.ToString())
                {
                    c_id = Convert.ToDecimal(dr[1]);
                    dr.Close();
                    SqlCommand cmd2 = new SqlCommand("select name from manufacturer where m_id='" + c_id + "'", Login.con);
                    SqlDataReader dr2 = cmd2.ExecuteReader();
                    while (dr2.Read())
                    {
                        labelmanuname.Text = Convert.ToString(dr2[0]);
                    }
                    dr2.Close();
                }
                break;
            }
            dr.Close();
            //string command = "select s.p_id P_Code,s.tone Tone,s.quality Quality,s.size Size,s.qty Boxes,s.tiles Tiles, CAST(ROUND((p.meters*(s.qty + (s.tiles / p.pieces))),2)as decimal(18,2)) Meters, s.pprice Price, s.discount Discount, CAST(ROUND((((s.qty + (s.tiles / p.pieces)) * (s.pprice)) - (((s.qty + (s.tiles / p.pieces)) * (s.pprice) * (s.discount))/100)),2) as decimal(18,2)) Amount From purchase_stock_detail s, product p Where s.p_id=p.p_id AND s.size=p.size AND s.ps_id ='" + invoice_no + "'";
            //test.bindingcode(command, dataGridView, Login.con);
            string command2 = "select s.p_id P_Code,p.p_name P_Name,s.qty Quantity, s.pprice Price, s.discount Discount,CAST(ROUND((((s.qty) * (s.pprice)) - (s.discount)),2) as decimal(18,2)) Amount From purchase_stock_detail2 s, product2 p Where s.p_id=p.p_id AND s.ps_id ='" + invoice_no + "'";
            test.bindingcode(command2, dataGridView2, Login.con);
            Login.con.Close();
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
            e.Graphics.DrawString("DC No:", new Font("Calibri", 12), new SolidBrush(Color.Black), startX, startY + Offset);
            e.Graphics.DrawString(labelinvoiveno.Text, new Font("Calibri", 12, FontStyle.Bold), new SolidBrush(Color.Black), startX + 55, startY + Offset);
            Offset = Offset + 30;
            e.Graphics.DrawString("A/c Name:", new Font("Calibri", 10, FontStyle.Bold), new SolidBrush(Color.Black), startX, startY + Offset);
            e.Graphics.DrawString(labelmanuname.Text, new Font("Calibri", 10), new SolidBrush(Color.Black), startX + 65, startY + Offset);
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
            //    e.Graphics.DrawString("Price", new Font("Calibri", 10, FontStyle.Bold), new SolidBrush(Color.Black), startX + 530, startY + Offset);
            //    e.Graphics.DrawString("Disc%", new Font("Calibri", 10, FontStyle.Bold), new SolidBrush(Color.Black), startX + 605, startY + Offset);
            //    e.Graphics.DrawString("Amount", new Font("Calibri", 10, FontStyle.Bold), new SolidBrush(Color.Black), startX + 665, startY + Offset);
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
            //        e.Graphics.DrawString(Convert.ToString(dataGridView.Rows[i].Cells[7].Value), new Font("Calibri", 9), new SolidBrush(Color.Black), startX + 530, startY + Offset);
            //        e.Graphics.DrawString(Convert.ToString(dataGridView.Rows[i].Cells[8].Value), new Font("Calibri", 9), new SolidBrush(Color.Black), startX + 605, startY + Offset);
            //        e.Graphics.DrawString(Convert.ToString(dataGridView.Rows[i].Cells[9].Value), new Font("Calibri", 9), new SolidBrush(Color.Black), startX + 665, startY + Offset);
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
                e.Graphics.DrawString("P_Code", new Font("Calibri", 10, FontStyle.Bold), new SolidBrush(Color.Black), startX + 45, startY + Offset);
                e.Graphics.DrawString("P_Name", new Font("Calibri", 10, FontStyle.Bold), new SolidBrush(Color.Black), startX + 160, startY + Offset);
                e.Graphics.DrawString("Qty", new Font("Calibri", 10, FontStyle.Bold), new SolidBrush(Color.Black), startX + 410, startY + Offset);
                e.Graphics.DrawString("Price", new Font("Calibri", 10, FontStyle.Bold), new SolidBrush(Color.Black), startX + 530, startY + Offset);
                e.Graphics.DrawString("Disc%", new Font("Calibri", 10, FontStyle.Bold), new SolidBrush(Color.Black), startX + 605, startY + Offset);
                e.Graphics.DrawString("Amount", new Font("Calibri", 10, FontStyle.Bold), new SolidBrush(Color.Black), startX + 665, startY + Offset);
                Offset = Offset + 20;
                e.Graphics.DrawString(underLine, new Font("Courier New", 10, FontStyle.Bold), new SolidBrush(Color.Black), startX, startY + Offset);
                Offset = Offset + 20;
                int a = dataGridView2.Rows.Count;
                for (; j < a; j++)
                {
                    e.Graphics.DrawString(Convert.ToString(j + 1), new Font("Calibri", 9), new SolidBrush(Color.Black), startX, startY + Offset);
                    e.Graphics.DrawString(Convert.ToString(dataGridView2.Rows[j].Cells[0].Value), new Font("Calibri", 9), new SolidBrush(Color.Black), startX + 45, startY + Offset);
                    e.Graphics.DrawString(Convert.ToString(dataGridView2.Rows[j].Cells[1].Value), new Font("Calibri", 9), new SolidBrush(Color.Black), startX + 160, startY + Offset);
                    e.Graphics.DrawString(Convert.ToString(dataGridView2.Rows[j].Cells[2].Value), new Font("Calibri", 9), new SolidBrush(Color.Black), startX + 410, startY + Offset);
                    e.Graphics.DrawString(Convert.ToString(dataGridView2.Rows[j].Cells[3].Value), new Font("Calibri", 9), new SolidBrush(Color.Black), startX + 530, startY + Offset);
                    e.Graphics.DrawString(Convert.ToString(dataGridView2.Rows[j].Cells[4].Value), new Font("Calibri", 9), new SolidBrush(Color.Black), startX + 605, startY + Offset);
                    e.Graphics.DrawString(Convert.ToString(dataGridView2.Rows[j].Cells[5].Value), new Font("Calibri", 9), new SolidBrush(Color.Black), startX + 665, startY + Offset);
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
            Offset = Offset - 50;
            e.Graphics.DrawString(underLine, new Font("Courier New", 10, FontStyle.Bold), new SolidBrush(Color.Black), startX, startY + Offset);
            Offset = Offset + 20;
            e.Graphics.DrawString("Total:", new Font("Calibri", 10, FontStyle.Bold), new SolidBrush(Color.Black), startX + 540, startY + Offset);
            e.Graphics.DrawString(labeltotal.Text, new Font("Calibri", 10), new SolidBrush(Color.Black), startX + 665, startY + Offset);
            Offset = Offset + 20;
            e.Graphics.DrawString(underLine, new Font("Courier New", 10, FontStyle.Bold), new SolidBrush(Color.Black), startX, startY + Offset);
            Offset = Offset + 20;
            e.Graphics.DrawString("Debit:", new Font("Calibri", 10, FontStyle.Bold), new SolidBrush(Color.Black), startX + 540, startY + Offset);
            e.Graphics.DrawString(labelpaid.Text, new Font("Calibri", 10), new SolidBrush(Color.Black), startX + 665, startY + Offset);
            Offset = Offset + 20;
            e.Graphics.DrawString(underLine, new Font("Courier New", 10, FontStyle.Bold), new SolidBrush(Color.Black), startX, startY + Offset);
            Offset = Offset + 20;
            e.Graphics.DrawString("Balance:", new Font("Calibri", 10, FontStyle.Bold), new SolidBrush(Color.Black), startX + 540, startY + Offset);
            e.Graphics.DrawString(((Convert.ToDecimal(labelpaid.Text)) - Convert.ToDecimal(labeltotal.Text)).ToString(), new Font("Calibri", 10), new SolidBrush(Color.Black), startX + 665, startY + Offset);
            Offset = Offset + 20;
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
