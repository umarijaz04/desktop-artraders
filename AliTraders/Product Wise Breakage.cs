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
    public partial class Product_Wise_Breakage : Form
    {
        Form opener;
        Int32 db_tiles;
        Decimal qty, tile;
        UF.Class1 test = new UF.Class1();

        public Product_Wise_Breakage(Form form)
        {
            InitializeComponent();
            opener = form;
        }

        private void Product_Wise_Breakage_Load(object sender, EventArgs e)
        {
            opener.Enabled = false;
            rdbtnavil_CheckedChanged(this, null);
            string command = "select Cast(p_id as varchar(50)) from product2 order by p_id";
            test.cmbox_drop(command, txtcode, Login.con);
            string command2 = "select p.p_name from product2 p order by p.p_name";
            test.cmbox_drop(command2, cmbname, Login.con);
        }

        private void Product_Wise_Breakage_FormClosing(object sender, FormClosingEventArgs e)
        {
            opener.Enabled = true;
        }

        private void rdbtnavil_CheckedChanged(object sender, EventArgs e)
        {

        }

        //private void cmbsize_Enter(object sender, EventArgs e)
        //{
        //    string command2 = "SELECT DISTINCT size from product order by size";
        //    test.cmbox_drop(command2, cmbsize, Login.con);
        //}

        //private void cmbtone_Enter(object sender, EventArgs e)
        //{
        //    string command = "select DISTINCT tone from purchase_stock_detail";
        //    test.cmbox_drop(command, cmbtone, Login.con);
        //}

        //private void btnsearch_Click(object sender, EventArgs e)
        //{
        //    string command1 = "select s.b_id Breakage_No, ss.ps_id DC_No, ss.datetime DateTime,s.p_id P_Code,s.tone Tone, s.quality Quality, s.size Size, s.qty Boxes, s.tiles Tiles, CAST(ROUND((p.meters*(s.qty + (s.tiles / p.pieces))),2) as decimal(18,2)) Meters from breakage_detail s, breakage_stock ss, product p where p.p_id=s.p_id AND p.size=s.size AND s.b_id=ss.b_id AND (ss.datetime between '" + Convert.ToDateTime(dateTimePicker1.Text) + "' AND '" + Convert.ToDateTime(dateTimePicker2.Text).AddDays(1) + "') AND s.p_id like'" + cmbcode.Text + "%' AND s.size like'" + cmbsize.Text + "%' AND s.quality like'" + cmbquality.Text + "%' AND s.tone like'" + cmbtone.Text + "%'";
        //    test.bindingcode(command1, dataGridView, Login.con);
        //}

        //private void btnprint_Click(object sender, EventArgs e)
        //{
        //    try
        //    {
        //        printDocument1.Print();
        //    }
        //    catch (Exception ex)
        //    {
        //        MessageBox.Show(ex.ToString());
        //    }
        //}

        private int i = 0;
        private void printDocument1_PrintPage(object sender, PrintPageEventArgs e)
        {
            //db_tiles = 0;
            //qty = tile = 0;
            ////Image imgPerson = Image.FromFile(@"C:\wonder.png");
            ////e.Graphics.DrawImage(imgPerson, 20, 28);
            //e.Graphics.DrawString("AR Tranders", new Font("Arial", 24, FontStyle.Bold), Brushes.Black, 210, 50);
            //e.Graphics.DrawString("IMPORTERS & GENERAL ORDER SUPPLIER", new Font("Arial", 10), Brushes.Black, 220, 90);
            //e.Graphics.DrawString("Mian Sanse Road, Gujranwala, Pakistan", new Font("Arial", 9, FontStyle.Italic), Brushes.Black, 220, 105);
            //e.Graphics.DrawString("Zeeshan 0323-8644433, +8615795799454", new Font("Arial", 9, FontStyle.Regular), Brushes.Black, 220, 125);
            ////e.Graphics.DrawString("Proprietor: ", new Font("Arial", 9, FontStyle.Bold), Brushes.Black, 265, 140);
            //e.Graphics.DrawString("email: zshani434@gmail.com", new Font("Arial", 9, FontStyle.Italic), Brushes.Black, 220, 140);
            //int startX = 50;
            //int startY = 150;
            //int Offset = 40;
            //e.Graphics.DrawString("Product Wise Breakage", new Font("Calibri", 12, FontStyle.Underline), new SolidBrush(Color.Black), startX, startY + Offset);
            //Offset = Offset + 30;
            //e.Graphics.DrawString("Product Code:", new Font("Calibri", 10, FontStyle.Bold), new SolidBrush(Color.Black), startX, startY + Offset);
            //e.Graphics.DrawString(cmbcode.Text, new Font("Calibri", 10), new SolidBrush(Color.Black), startX + 90, startY + Offset);
            //e.Graphics.DrawString("Size:", new Font("Calibri", 10, FontStyle.Bold), new SolidBrush(Color.Black), startX + 300, startY + Offset);
            //e.Graphics.DrawString(cmbsize.Text, new Font("Calibri", 10), new SolidBrush(Color.Black), startX + 330, startY + Offset);
            //e.Graphics.DrawString("Quality:", new Font("Calibri", 10, FontStyle.Bold), new SolidBrush(Color.Black), startX + 400, startY + Offset);
            //e.Graphics.DrawString(cmbquality.Text, new Font("Calibri", 10), new SolidBrush(Color.Black), startX + 450, startY + Offset);
            //e.Graphics.DrawString("Tone:", new Font("Calibri", 10, FontStyle.Bold), new SolidBrush(Color.Black), startX + 550, startY + Offset);
            //e.Graphics.DrawString(cmbtone.Text, new Font("Calibri", 10), new SolidBrush(Color.Black), startX + 585, startY + Offset);
            //Offset = Offset + 20;
            //string underLine = "=======================================================================================";
            //e.Graphics.DrawString(underLine, new Font("Courier New", 10, FontStyle.Bold), new SolidBrush(Color.Black), startX, startY + Offset);
            //Offset = Offset + 20;
            //e.Graphics.DrawString("Invoice#", new Font("Calibri", 10, FontStyle.Bold), new SolidBrush(Color.Black), startX, startY + Offset);
            //e.Graphics.DrawString("DC#", new Font("Calibri", 10, FontStyle.Bold), new SolidBrush(Color.Black), startX + 150, startY + Offset);
            //e.Graphics.DrawString("Date & Time", new Font("Calibri", 10, FontStyle.Bold), new SolidBrush(Color.Black), startX + 250, startY + Offset);
            //e.Graphics.DrawString("Box/T", new Font("Calibri", 10, FontStyle.Bold), new SolidBrush(Color.Black), startX + 430, startY + Offset);
            //e.Graphics.DrawString("Meters", new Font("Calibri", 10, FontStyle.Bold), new SolidBrush(Color.Black), startX + 510, startY + Offset);
            //Offset = Offset + 20;
            //e.Graphics.DrawString(underLine, new Font("Courier New", 10, FontStyle.Bold), new SolidBrush(Color.Black), startX, startY + Offset);
            //Offset = Offset + 20;
            //int a = dataGridView.Rows.Count;
            //for (; i < a; i++)
            //{
            //    e.Graphics.DrawString(Convert.ToString(dataGridView.Rows[i].Cells[0].Value), new Font("Calibri", 9), new SolidBrush(Color.Black), startX, startY + Offset);
            //    e.Graphics.DrawString(Convert.ToString(dataGridView.Rows[i].Cells[1].Value), new Font("Calibri", 9), new SolidBrush(Color.Black), startX + 150, startY + Offset);
            //    e.Graphics.DrawString(Convert.ToString(dataGridView.Rows[i].Cells[2].Value), new Font("Calibri", 9), new SolidBrush(Color.Black), startX + 250, startY + Offset);
            //    e.Graphics.DrawString(Convert.ToString(dataGridView.Rows[i].Cells[3].Value + " - " + dataGridView.Rows[i].Cells[4].Value), new Font("Calibri", 9), new SolidBrush(Color.Black), startX + 430, startY + Offset);
            //    e.Graphics.DrawString(Convert.ToString(dataGridView.Rows[i].Cells[5].Value), new Font("Calibri", 9), new SolidBrush(Color.Black), startX + 510, startY + Offset);
            //    qty += Convert.ToDecimal(dataGridView.Rows[i].Cells[3].Value.ToString());
            //    tile += Convert.ToDecimal(dataGridView.Rows[i].Cells[4].Value.ToString());
            //    Offset = Offset + 20;
            //    if (Offset >= e.MarginBounds.Height)
            //    {
            //        i++;
            //        e.HasMorePages = true;
            //        Offset = 0;
            //        return;
            //    }
            //    else
            //    {
            //        e.HasMorePages = false;
            //    }
            //}
            //e.Graphics.DrawString(underLine, new Font("Courier New", 10, FontStyle.Bold), new SolidBrush(Color.Black), startX, startY + Offset);
            //Offset = Offset + 20;
            //Login.con.Open();
            //SqlCommand cmd = new SqlCommand("SELECT pieces from product where p_id='" + cmbcode.Text + "' AND size='" + cmbsize.Text + "'", Login.con);
            //SqlDataReader dr = cmd.ExecuteReader();
            //if (dr.HasRows)
            //{
            //    while (dr.Read())
            //    {
            //        db_tiles = Convert.ToInt32(dr[0]);
            //    }
            //}
            //dr.Close();
            //Login.con.Close();
            //e.Graphics.DrawString((Convert.ToInt32(qty) + (Convert.ToInt32(tile) / db_tiles)).ToString() + " - " + (Convert.ToInt32(tile) % db_tiles).ToString(), new Font("Calibri", 10), new SolidBrush(Color.Black), startX + 430, startY + Offset);
            //Offset = Offset + 20;
            //e.Graphics.DrawString(underLine, new Font("Courier New", 10, FontStyle.Bold), new SolidBrush(Color.Black), startX, startY + Offset);
            //Offset = Offset + 20;
            //e.Graphics.DrawString("Signature:  __________________", new Font("Calibri", 10, FontStyle.Bold), new SolidBrush(Color.Black), startX, startY + Offset);
            //e.Graphics.DrawString("Date & Time:  ", new Font("Calibri", 10, FontStyle.Bold), new SolidBrush(Color.Black), startX + 320, startY + Offset);
            //e.Graphics.DrawString(DateTime.Now.ToString(), new Font("Calibri", 10), new SolidBrush(Color.Black), startX + 400, startY + Offset);
            //Offset = Offset + 25;
            //e.Graphics.DrawString("Developed By: Geeks IT Solutions 03126424443  ", new Font("Calibri", 8, FontStyle.Bold), new SolidBrush(Color.Black), startX + 280, startY + Offset + 5);
            //e.Graphics.PageUnit = GraphicsUnit.Inch;
            //i = 0;
        }

        private void rdbtnavil2_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void txtcode_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                txtcode.Text = txtcode.Text.ToUpper();
                txtcode.SelectAll();
                Login.con.Open();
                SqlCommand cmd = new SqlCommand("SELECT p_name from product2 where p_id='" + txtcode.Text.ToUpper() + "'", Login.con);
                SqlDataReader dr = cmd.ExecuteReader();
                if (dr.HasRows)
                {
                    while (dr.Read())
                    {
                        cmbname.Text = Convert.ToString(dr[0]);
                    }
                    dr.Close();
                }
                else
                {
                    dr.Close();
                    MessageBox.Show("Record Not Found", "AR Tranders", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                }
                Login.con.Close();
            }
        }

        private void txtcode_Leave(object sender, EventArgs e)
        {
            if (txtcode.Text == "")
            {
                cmbname.Text = "";
            }
            else
            {
                txtcode.Text = txtcode.Text.ToUpper();
                txtcode.SelectAll();
                Login.con.Open();
                SqlCommand cmd = new SqlCommand("SELECT p_name from product2 where p_id='" + txtcode.Text.ToUpper() + "'", Login.con);
                SqlDataReader dr = cmd.ExecuteReader();
                if (dr.HasRows)
                {
                    while (dr.Read())
                    {
                        cmbname.Text = Convert.ToString(dr[0]);
                    }
                    dr.Close();
                }
                else
                {
                    dr.Close();
                    MessageBox.Show("Record Not Found", "AR Tranders", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                    cmbname.Text = "";
                    txtcode.Focus();
                }
                Login.con.Close();
            }
        }

        private void cmbname_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                cmbname.Text = cmbname.Text.ToUpper();
                cmbname.SelectAll();
                Login.con.Open();
                SqlCommand cmd = new SqlCommand("SELECT p_id from product2 where p_name='" + cmbname.Text.ToUpper() + "'", Login.con);
                SqlDataReader dr = cmd.ExecuteReader();
                if (dr.HasRows)
                {
                    while (dr.Read())
                    {
                        txtcode.Text = Convert.ToString(dr[0]);
                    }
                    dr.Close();
                }
                else
                {
                    dr.Close();
                    MessageBox.Show("Record Not Found", "AR Tranders", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                }
                Login.con.Close();
            }
        }

        private void cmbname_Leave(object sender, EventArgs e)
        {
            if (cmbname.Text == "")
            {
                txtcode.Text = "";
            }
            else
            {
                cmbname.Text = cmbname.Text.ToUpper();
                cmbname.SelectAll();
                Login.con.Open();
                SqlCommand cmd = new SqlCommand("SELECT p_id from product2 where p_name='" + cmbname.Text.ToUpper() + "'", Login.con);
                SqlDataReader dr = cmd.ExecuteReader();
                if (dr.HasRows)
                {
                    while (dr.Read())
                    {
                        txtcode.Text = Convert.ToString(dr[0]);
                    }
                    dr.Close();
                }
                else
                {
                    dr.Close();
                    MessageBox.Show("Record Not Found", "AR Tranders", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                    txtcode.Text = "";
                    cmbname.Focus();
                }
                Login.con.Close();
            }
        }

        private void btnserach2_Click(object sender, EventArgs e)
        {
            string command1 = "select s.b_id Breakage_No, ss.ps_id DC_No,ss.datetime DateTime, s.qty Quantity from breakage_detail2 s, breakage_stock ss, product2 p where p.p_id=s.p_id AND s.b_id=ss.b_id AND (ss.datetime between '" + Convert.ToDateTime(dateTimePicker4.Text) + "' AND '" + Convert.ToDateTime(dateTimePicker3.Text).AddDays(1) + "') AND p.p_id='" + txtcode.Text + "'";
            test.bindingcode(command1, dataGridView2, Login.con);
        }

        private void btnprint2_Click(object sender, EventArgs e)
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
            qty = 0;
            //Image imgPerson = Image.FromFile(@"C:\wonder.png");
            //e.Graphics.DrawImage(imgPerson, 20, 28);
            e.Graphics.DrawString("AR Tranders", new Font("Arial", 24, FontStyle.Bold), Brushes.Black, 210, 50);
            e.Graphics.DrawString("IMPORTERS & GENERAL ORDER SUPPLIER", new Font("Arial", 10), Brushes.Black, 220, 90);
            e.Graphics.DrawString("Mian Sanse Road, Gujranwala, Pakistan", new Font("Arial", 9, FontStyle.Italic), Brushes.Black, 220, 105);
            e.Graphics.DrawString("Zeeshan 0323-8644433, +8615795799454", new Font("Arial", 9, FontStyle.Regular), Brushes.Black, 220, 125);
            //e.Graphics.DrawString("Proprietor: ", new Font("Arial", 9, FontStyle.Bold), Brushes.Black, 265, 140);
            e.Graphics.DrawString("email: zshani434@gmail.com", new Font("Arial", 9, FontStyle.Italic), Brushes.Black, 220, 140);
            int startX = 50;
            int startY = 150;
            int Offset = 40;
            e.Graphics.DrawString("Product Wise Breakage", new Font("Calibri", 12, FontStyle.Underline), new SolidBrush(Color.Black), startX, startY + Offset);
            Offset = Offset + 30;
            e.Graphics.DrawString("P_Name:", new Font("Calibri", 10, FontStyle.Bold), new SolidBrush(Color.Black), startX, startY + Offset);
            e.Graphics.DrawString(cmbname.Text, new Font("Calibri", 10), new SolidBrush(Color.Black), startX + 90, startY + Offset);
            Offset = Offset + 20;
            string underLine = "=======================================================================================";
            e.Graphics.DrawString(underLine, new Font("Courier New", 10, FontStyle.Bold), new SolidBrush(Color.Black), startX, startY + Offset);
            Offset = Offset + 20;
            e.Graphics.DrawString("Invoice#", new Font("Calibri", 10, FontStyle.Bold), new SolidBrush(Color.Black), startX, startY + Offset);
            e.Graphics.DrawString("DC#", new Font("Calibri", 10, FontStyle.Bold), new SolidBrush(Color.Black), startX + 150, startY + Offset);
            e.Graphics.DrawString("Date & Time", new Font("Calibri", 10, FontStyle.Bold), new SolidBrush(Color.Black), startX + 250, startY + Offset);
            e.Graphics.DrawString("Qty", new Font("Calibri", 10, FontStyle.Bold), new SolidBrush(Color.Black), startX + 470, startY + Offset);
            Offset = Offset + 20;
            e.Graphics.DrawString(underLine, new Font("Courier New", 10, FontStyle.Bold), new SolidBrush(Color.Black), startX, startY + Offset);
            Offset = Offset + 20;
            int a = dataGridView2.Rows.Count;
            for (; i < a; i++)
            {
                e.Graphics.DrawString(Convert.ToString(dataGridView2.Rows[i].Cells[0].Value), new Font("Calibri", 9), new SolidBrush(Color.Black), startX, startY + Offset);
                e.Graphics.DrawString(Convert.ToString(dataGridView2.Rows[i].Cells[1].Value), new Font("Calibri", 9), new SolidBrush(Color.Black), startX + 150, startY + Offset);
                e.Graphics.DrawString(Convert.ToString(dataGridView2.Rows[i].Cells[2].Value), new Font("Calibri", 9), new SolidBrush(Color.Black), startX + 250, startY + Offset);
                e.Graphics.DrawString(Convert.ToString(dataGridView2.Rows[i].Cells[3].Value), new Font("Calibri", 9), new SolidBrush(Color.Black), startX + 470, startY + Offset);
                qty += Convert.ToDecimal(dataGridView2.Rows[i].Cells[3].Value.ToString());
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
            e.Graphics.DrawString(qty.ToString(), new Font("Calibri", 10), new SolidBrush(Color.Black), startX + 470, startY + Offset);
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
