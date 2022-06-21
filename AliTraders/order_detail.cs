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

namespace AhmadSanitary
{
    public partial class order_detail : Form
    {
        Decimal o_id, p_tiles, p_qty, meters_in_box, availableQty, limit, sale_return_qty, breakage_qty, sale_qty, purchase_qty;
        String p_code, p_quality, tone, p_size, name;
        Int32 db_tiles;
        UF.Class1 test = new UF.Class1();

        public order_detail(Decimal o_no, String c_name)
        {
            InitializeComponent();
            o_id = o_no;
            name = c_name;
        }

        private void order_detail_Load(object sender, EventArgs e)
        {
            //string command = "select p.p_id P_Code, od.tone Tone, od.quality Quality, od.size Size, od.qty Boxes,od.tiles Tiles, CAST(ROUND((p.meters*((od.tiles / p.pieces) + od.qty)),2) as decimal(18,2)) Meters From order_detail od, product p Where p.p_id=od.p_id AND p.size=od.size AND od.o_id='" + o_id+"'";
            //test.bindingcode(command, dataGridView, Login.con);
            string command2 = "select p.p_id P_Code,p.p_name P_Name, od.qty Quantity From order_detail2 od, product2 p Where p.p_id=od.p_id AND od.o_id='" + o_id + "'";
            test.bindingcode(command2, dataGridView2, Login.con);
        }

        private decimal available_qty(String p_id)
        {
            Decimal purchase_qty;
            Decimal sale_qty;
            Decimal sale_return_qty;
            SqlCommand cmd = new SqlCommand("SELECT ISNULL(sum(qty),0) from purchase_stock_detail2 where p_id='" + p_id + "'", Login.con);
            purchase_qty = (Decimal)cmd.ExecuteScalar();
            SqlCommand cmd1 = new SqlCommand("SELECT ISNULL(sum(qty),0) from sale_stock_detail2 where p_id='" + p_id + "'", Login.con);
            sale_qty = (Decimal)cmd1.ExecuteScalar();
            SqlCommand cmd2 = new SqlCommand("SELECT ISNULL(sum(qty),0) from sale_return_detail2 where p_id='" + p_id + "'", Login.con);
            sale_return_qty = (Decimal)cmd2.ExecuteScalar();
            return ((purchase_qty + sale_return_qty) - sale_qty);
        }

        private decimal available_qty(String p_id, string tone, string quality, string size)
        {
            SqlCommand cmd0 = new SqlCommand("SELECT pieces from product where p_id='" + p_id + "' AND size='" + size + "'", Login.con);
            SqlDataReader dr0 = cmd0.ExecuteReader();
            if (dr0.HasRows)
            {
                while (dr0.Read())
                {
                    db_tiles = Convert.ToInt32(dr0[0]);
                }
            }
            dr0.Close();
            SqlCommand cmd = new SqlCommand("SELECT ISNULL(sum(qty),0),ISNULL(sum(tiles),0) from purchase_stock_detail where p_id='" + p_id + "' AND quality='" + quality + "' AND Tone='" + tone + "' AND Size='" + size + "'", Login.con);
            SqlDataReader dr = cmd.ExecuteReader();
            if (dr.HasRows)
            {
                while (dr.Read())
                {
                    purchase_qty = Convert.ToDecimal(dr[0]) + (Convert.ToDecimal(dr[1]) / Convert.ToDecimal(db_tiles));
                }
            }
            dr.Close();
            SqlCommand cmd2 = new SqlCommand("SELECT ISNULL(sum(qty),0),ISNULL(sum(tiles),0) from sale_stock_detail where p_id='" + p_id + "' AND quality='" + quality + "'AND Tone='" + tone + "' AND Size='" + size + "'", Login.con);
            SqlDataReader dr2 = cmd2.ExecuteReader();
            if (dr2.HasRows)
            {
                while (dr2.Read())
                {
                    sale_qty = Convert.ToDecimal(dr2[0]) + (Convert.ToDecimal(dr2[1]) / Convert.ToDecimal(db_tiles));
                }
            }
            dr2.Close();
            SqlCommand cmd3 = new SqlCommand("SELECT ISNULL(sum(qty),0),ISNULL(sum(tiles),0) from sale_return_detail where p_id='" + p_id + "' AND quality='" + quality + "'AND Tone='" + tone + "' AND Size='" + size + "'", Login.con);
            SqlDataReader dr3 = cmd3.ExecuteReader();
            if (dr3.HasRows)
            {
                while (dr3.Read())
                {
                    sale_return_qty = Convert.ToDecimal(dr3[0]) + (Convert.ToDecimal(dr3[1]) / Convert.ToDecimal(db_tiles));
                }
            }
            dr3.Close();
            SqlCommand cmd4 = new SqlCommand("SELECT ISNULL(sum(qty),0),ISNULL(sum(tiles),0) from breakage_detail where p_id='" + p_id + "' AND quality='" + quality + "'AND Tone='" + tone + "' AND Size='" + size + "'", Login.con);
            SqlDataReader dr4 = cmd4.ExecuteReader();
            if (dr4.HasRows)
            {
                while (dr4.Read())
                {
                    breakage_qty = Convert.ToDecimal(dr4[0]) + (Convert.ToDecimal(dr4[1]) / Convert.ToDecimal(db_tiles));
                }
            }
            dr4.Close();
            return ((purchase_qty + sale_return_qty) - (sale_qty + breakage_qty));
        }

        //private void dataGridView_Enter(object sender, EventArgs e)
        //{
        //    dataGridView.Columns["P_Code"].ReadOnly = dataGridView.Columns["Tone"].ReadOnly = dataGridView.Columns["Quality"].ReadOnly = dataGridView.Columns["Size"].ReadOnly = dataGridView.Columns["Meters"].ReadOnly = true;
        //}

        //private void dataGridView_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        //{
        //    if (e.RowIndex >= 0)
        //    {
        //        DataGridViewRow row = this.dataGridView.Rows[e.RowIndex];
        //        p_code = row.Cells[0].Value.ToString();
        //        p_quality = row.Cells[2].Value.ToString();
        //        tone = row.Cells[1].Value.ToString();
        //        p_size = row.Cells[3].Value.ToString();
        //        Login.con.Open();
        //        SqlCommand cmd121 = new SqlCommand("SELECT isnull(pieces,0) from product where p_id = '" + p_code + "' AND size='" + p_size + "'", Login.con);
        //        SqlDataReader dr121 = cmd121.ExecuteReader();
        //        while (dr121.Read())
        //        {
        //            db_tiles = Convert.ToInt16(dr121[0]);
        //        }
        //        dr121.Close();
        //        Login.con.Close();
        //        if (row.Cells[4].Value.ToString() == "0" && row.Cells[5].Value.ToString() == "0")
        //            p_qty = 1;
        //        else if (row.Cells[4].Value.ToString() != "")
        //            p_qty = Convert.ToDecimal(row.Cells[4].Value.ToString());
        //        else
        //            p_qty = 1;
        //        if (row.Cells[5].Value.ToString() != "")
        //        {
        //            p_tiles = Convert.ToDecimal(row.Cells[5].Value.ToString());
        //            p_qty = Convert.ToDecimal(Convert.ToInt32(p_qty) + (Convert.ToInt32(p_tiles) / db_tiles));
        //            p_tiles = Convert.ToDecimal(Convert.ToInt32(p_tiles) % db_tiles);
        //        }
        //        else
        //            p_tiles = 0;
        //    }
        //    Login.con.Open();
        //    availableQty = available_qty(p_code, tone, p_quality, p_size);
        //    SqlCommand cmd = new SqlCommand("SELECT isnull(meters,0) from product where p_id = '" + p_code + "' AND size='" + p_size + "' ", Login.con);
        //    meters_in_box = (Decimal)cmd.ExecuteScalar();
        //    if ((p_qty + (p_tiles / Convert.ToDecimal(db_tiles))) <= availableQty)
        //    {
        //        SqlCommand cmd2 = new SqlCommand("UPDATE order_detail set qty='" + p_qty + "',tiles='" + p_tiles + "' where p_id='" + p_code + "' AND quality='" + p_quality + "' AND tone='" + tone + "'AND size='" + p_size + "'", Login.con);
        //        cmd2.ExecuteNonQuery();
        //        String command = "select p.p_id P_Code, od.tone Tone, od.quality Quality, od.size Size, od.qty Boxes,od.tiles Tiles, CAST(ROUND((p.meters*((od.tiles / p.pieces) + od.qty)),2) as decimal(18,2)) Meters From order_detail od, product p Where p.p_id=od.p_id AND p.size=od.size AND od.o_id='" + o_id + "'";
        //        test.bindingcode(command, dataGridView, Login.con);
        //        SqlCommand cmd5 = new SqlCommand("SELECT * from limit where id='1'", Login.con);
        //        SqlDataReader dr4 = cmd5.ExecuteReader();
        //        while (dr4.Read())
        //        {
        //            limit = Convert.ToDecimal(dr4[1]);
        //        }
        //        dr4.Close();
        //        if ((availableQty - (p_qty + (p_tiles / Convert.ToDecimal(db_tiles)))) < limit)
        //            MessageBox.Show("Remain Product Quantity is '" + Convert.ToInt32(((availableQty - (p_qty + (p_tiles / Convert.ToInt32(db_tiles)))) * db_tiles) / db_tiles) + " - " + Convert.ToInt32(((availableQty - (p_qty + (p_tiles / Convert.ToInt32(db_tiles)))) * db_tiles) % db_tiles) + "'", "AR Tranders", MessageBoxButtons.OK, MessageBoxIcon.Information);
        //    }
        //    else
        //    {
        //        MessageBox.Show("You Have Not Enough Quantity Against This Product Code", "AR Tranders", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
        //        String command = "select p.p_id P_Code, od.tone Tone, od.quality Quality, od.size Size, od.qty Boxes,od.tiles Tiles, CAST(ROUND((p.meters*((od.tiles / p.pieces) + od.qty)),2) as decimal(18,2)) Meters From order_detail od, product p Where p.p_id=od.p_id AND p.size=od.size AND od.o_id='" + o_id + "'";
        //        test.bindingcode(command, dataGridView, Login.con);
        //    }
        //    Login.con.Close();
        //    dataGridView.Columns["P_Code"].ReadOnly = dataGridView.Columns["Tone"].ReadOnly = dataGridView.Columns["Quality"].ReadOnly = dataGridView.Columns["Size"].ReadOnly = dataGridView.Columns["Meters"].ReadOnly = true;
        //}

        //private void dataGridView_EditingControlShowing(object sender, DataGridViewEditingControlShowingEventArgs e)
        //{
        //    e.Control.KeyPress -= new KeyPressEventHandler(test.digit_correction);
        //    if (dataGridView.CurrentCell.ColumnIndex == 4)
        //    {
        //        TextBox tb = e.Control as TextBox;
        //        if (tb != null)
        //        {
        //            tb.KeyPress += new KeyPressEventHandler(test.digit_correction);
        //        }
        //    }
        //    else if (dataGridView.CurrentCell.ColumnIndex == 5)
        //    {
        //        TextBox tb = e.Control as TextBox;
        //        if (tb != null)
        //        {
        //            tb.KeyPress += new KeyPressEventHandler(test.digit_correction);
        //        }
        //    }
        //}

        private void dataGridView2_Enter(object sender, EventArgs e)
        {
            dataGridView2.Columns["P_Code"].ReadOnly = dataGridView2.Columns["P_Name"].ReadOnly = true;
        }

        private void dataGridView2_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = this.dataGridView2.Rows[e.RowIndex];
                if (row.Cells[2].Value.ToString() == "0")
                    p_qty = 1;
                else if (row.Cells[2].Value.ToString() != "")
                    p_qty = Convert.ToDecimal(row.Cells[2].Value.ToString());
                else
                    p_qty = 1;
                p_code = row.Cells[0].Value.ToString();
            }
            Login.con.Open();
            availableQty = available_qty(p_code);
            if (p_qty <= availableQty)
            {
                SqlCommand cmd2 = new SqlCommand("UPDATE order_detail2 set qty='" + p_qty + "'where p_id='" + p_code + "'", Login.con);
                cmd2.ExecuteNonQuery();
                String command = "select p.p_id P_Code,p.p_name P_Name, od.qty Quantity From order_detail2 od, product2 p Where p.p_id=od.p_id AND od.o_id='" + o_id + "'";
                test.bindingcode(command, dataGridView2, Login.con);
                SqlCommand cmd5 = new SqlCommand("SELECT * from limit where id='1'", Login.con);
                SqlDataReader dr4 = cmd5.ExecuteReader();
                while (dr4.Read())
                {
                    limit = Convert.ToDecimal(dr4[1]);
                }
                dr4.Close();
                if ((availableQty - p_qty) < limit)
                    MessageBox.Show("Remain Product Quantity is' " + (availableQty - (p_qty)) + "'", "AR Tranders", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                MessageBox.Show("You Have Not Enough Quantity Against This Product Code", "AR Tranders", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                String command = "select p.p_id P_Code,p.p_name P_Name, od.qty Quantity From order_detail2 od, product2 p Where p.p_id=od.p_id AND od.o_id='" + o_id + "'";
                test.bindingcode(command, dataGridView2, Login.con);
            }
            Login.con.Close();
            dataGridView2.Columns["P_Code"].ReadOnly = dataGridView2.Columns["P_Name"].ReadOnly = true;
        }

        private void dataGridView2_EditingControlShowing(object sender, DataGridViewEditingControlShowingEventArgs e)
        {
            e.Control.KeyPress -= new KeyPressEventHandler(test.digit_correction);
            if (dataGridView2.CurrentCell.ColumnIndex == 2)
            {
                TextBox tb = e.Control as TextBox;
                if (tb != null)
                {
                    tb.KeyPress += new KeyPressEventHandler(test.digit_correction);
                }
            }
        }

        private void button2_Click(object sender, EventArgs e)
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
            e.Graphics.DrawString("Temporary Gate Pass", new Font("Calibri", 12, FontStyle.Underline), new SolidBrush(Color.Black), startX, startY + Offset);
            Offset = Offset + 30;
            e.Graphics.DrawString("Gate Pass No:", new Font("Calibri", 12), new SolidBrush(Color.Black), startX, startY + Offset);
            e.Graphics.DrawString(o_id.ToString(), new Font("Calibri", 12, FontStyle.Bold), new SolidBrush(Color.Black), startX + 95, startY + Offset);
            Offset = Offset + 30;
            e.Graphics.DrawString("Customer Name:", new Font("Calibri", 10, FontStyle.Bold), new SolidBrush(Color.Black), startX, startY + Offset);
            e.Graphics.DrawString(name, new Font("Calibri", 10), new SolidBrush(Color.Black), startX + 105, startY + Offset);
            e.Graphics.DrawString("Date & Time:  ", new Font("Calibri", 10, FontStyle.Bold), new SolidBrush(Color.Black), startX + 500, startY + Offset);
            e.Graphics.DrawString(DateTime.Now.ToString(), new Font("Calibri", 10), new SolidBrush(Color.Black), startX + 580, startY + Offset);
            Offset = Offset + 20;
            string underLine = "=======================================================================================";
            //if (dataGridView.Rows.Count != 0)
            //{
            //    e.Graphics.DrawString(underLine, new Font("Courier New", 10, FontStyle.Bold), new SolidBrush(Color.Black), startX, startY + Offset);
            //    Offset = Offset + 20;
            //    e.Graphics.DrawString("Sr#", new Font("Calibri", 10, FontStyle.Bold), new SolidBrush(Color.Black), startX, startY + Offset);
            //    e.Graphics.DrawString("P_Code", new Font("Calibri", 10, FontStyle.Bold), new SolidBrush(Color.Black), startX + 45, startY + Offset);
            //    e.Graphics.DrawString("Tone", new Font("Calibri", 10, FontStyle.Bold), new SolidBrush(Color.Black), startX + 280, startY + Offset);
            //    e.Graphics.DrawString("Quality", new Font("Calibri", 10, FontStyle.Bold), new SolidBrush(Color.Black), startX + 375, startY + Offset);
            //    e.Graphics.DrawString("Size", new Font("Calibri", 10, FontStyle.Bold), new SolidBrush(Color.Black), startX + 480, startY + Offset);
            //    e.Graphics.DrawString("Box/T", new Font("Calibri", 10, FontStyle.Bold), new SolidBrush(Color.Black), startX + 580, startY + Offset);
            //    e.Graphics.DrawString("Meters", new Font("Calibri", 10, FontStyle.Bold), new SolidBrush(Color.Black), startX + 680, startY + Offset);
            //    Offset = Offset + 20;
            //    e.Graphics.DrawString(underLine, new Font("Courier New", 10, FontStyle.Bold), new SolidBrush(Color.Black), startX, startY + Offset);
            //    Offset = Offset + 20;
            //    int a = dataGridView.Rows.Count;
            //    for (; i < a; i++)
            //    {
            //        e.Graphics.DrawString(Convert.ToString(i + 1), new Font("Calibri", 9), new SolidBrush(Color.Black), startX, startY + Offset);
            //        e.Graphics.DrawString(Convert.ToString(dataGridView.Rows[i].Cells[0].Value), new Font("Calibri", 9), new SolidBrush(Color.Black), startX + 45, startY + Offset);
            //        e.Graphics.DrawString(Convert.ToString(dataGridView.Rows[i].Cells[1].Value), new Font("Calibri", 9), new SolidBrush(Color.Black), startX + 280, startY + Offset);
            //        e.Graphics.DrawString(Convert.ToString(dataGridView.Rows[i].Cells[2].Value), new Font("Calibri", 9), new SolidBrush(Color.Black), startX + 375, startY + Offset);
            //        e.Graphics.DrawString(Convert.ToString(dataGridView.Rows[i].Cells[3].Value), new Font("Calibri", 9), new SolidBrush(Color.Black), startX + 480, startY + Offset);
            //        e.Graphics.DrawString(Convert.ToString(dataGridView.Rows[i].Cells[4].Value + " - " + dataGridView.Rows[i].Cells[5].Value), new Font("Calibri", 9), new SolidBrush(Color.Black), startX + 580, startY + Offset);
            //        e.Graphics.DrawString(Convert.ToString(dataGridView.Rows[i].Cells[6].Value), new Font("Calibri", 9), new SolidBrush(Color.Black), startX + 680, startY + Offset);
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
                e.Graphics.DrawString("Qty", new Font("Calibri", 10, FontStyle.Bold), new SolidBrush(Color.Black), startX + 580, startY + Offset);
                Offset = Offset + 20;
                e.Graphics.DrawString(underLine, new Font("Courier New", 10, FontStyle.Bold), new SolidBrush(Color.Black), startX, startY + Offset);
                Offset = Offset + 20;
                int a = dataGridView2.Rows.Count;
                for (; j < a; j++)
                {
                    e.Graphics.DrawString(Convert.ToString(j + 1), new Font("Calibri", 9), new SolidBrush(Color.Black), startX, startY + Offset);
                    e.Graphics.DrawString(Convert.ToString(dataGridView2.Rows[j].Cells[1].Value), new Font("Calibri", 9), new SolidBrush(Color.Black), startX + 45, startY + Offset);
                    e.Graphics.DrawString(Convert.ToString(dataGridView2.Rows[j].Cells[2].Value), new Font("Calibri", 9), new SolidBrush(Color.Black), startX + 580, startY + Offset);
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
            e.Graphics.DrawString("نوٹ:(1)ڈرا‏‏ئیورنےمال‏چیک‏کرکےپورالوڈکیاہے۔(2)مال‏میں‏کسی‏قسم‏کی‏توڑپھوڑنہیں‏ہے۔(3)مال‏بلکل‏سہی‏لوڈکروایاگیاہے۔", new Font("Microsoft Uighur", 18), new SolidBrush(Color.Black), startX + 30, startY + Offset);
            Offset = Offset + 22;
            e.Graphics.DrawString("مال‏وصول‏کرنےوالامال‏چیک‏کرکے‏وصول‏کرےبعدمیں‏کمپنی‏اورڈرائیورکسی‏قسم‏کےکلیم‏کےذمہ‏دار‏نہیں‏ہونگے۔", new Font("Microsoft Uighur", 18), new SolidBrush(Color.Black), startX + 100, startY + Offset);
            Offset = Offset + 40;
            e.Graphics.DrawString("Customer Signature: ______________________", new Font("Calibri", 10, FontStyle.Bold), new SolidBrush(Color.Black), startX, startY + Offset);
            e.Graphics.DrawString("Driver Signature: ______________________", new Font("Calibri", 10, FontStyle.Bold), new SolidBrush(Color.Black), startX + 495, startY + Offset);
            Offset = Offset + 25;
            e.Graphics.DrawString("Developed By: Geeks IT Solutions 03126424443  ", new Font("Calibri", 8, FontStyle.Bold), new SolidBrush(Color.Black), startX + 280, startY + Offset + 5);
            e.Graphics.PageUnit = GraphicsUnit.Inch;
            i = j = 0;
        }
    }
}
