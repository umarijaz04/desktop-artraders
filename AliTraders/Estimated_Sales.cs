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
    public partial class Estimated_Sales : Form
    {
        Cart opener;
        Int32 db_tiles;
        String finvoice, p_code, p_quality, tone, p_size, p_name;
        Decimal customer_id, p_tiles, p_disc, p_qty, p_price, meters_in_box;
        DateTime date;
        Boolean amt = false;
        UF.Class1 test = new UF.Class1();
        private int i, j = 0;

        public Estimated_Sales(Cart form, String total, String sinvoice, Decimal c_id, DateTime dt)
        {
            InitializeComponent();
            opener = form;
            txttotal.Text = total;
            finvoice = sinvoice;
            customer_id = c_id;
            date = dt;
        }

        private void Estimated_Sales_Load(object sender, EventArgs e)
        {
            opener.Enabled = false;
            //String command = "SELECT P_Code,Tone,Quality,Size,Boxes,Tiles,Meter,Price,Discount,Amount from fake_cart";
            //test.bindingcode(command, dataGridView, Login.con);
            String command2 = "SELECT * from fake_cart_s";
            test.bindingcode(command2, dataGridView2, Login.con);
            txtgtotal.Text = Convert.ToString(Convert.ToDecimal(txttotal.Text) - Convert.ToDecimal(txtdiscount.Text));
            txtbalance.Text = Convert.ToString(Convert.ToDecimal(txtgtotal.Text) - Convert.ToDecimal(txtamount.Text));
        }

        private void Estimated_Sales_FormClosing(object sender, FormClosingEventArgs e)
        {
            opener.Enabled = true;
        }

        //Estimated Sanitary
        //private void dataGridView_Enter(object sender, EventArgs e)
        //{
        //    dataGridView.Columns["P_Code"].ReadOnly= dataGridView.Columns["Boxes"].ReadOnly = dataGridView.Columns["Tiles"].ReadOnly = dataGridView.Columns["Tone"].ReadOnly = dataGridView.Columns["Quality"].ReadOnly = dataGridView.Columns["Size"].ReadOnly = dataGridView.Columns["Meter"].ReadOnly = dataGridView.Columns["Amount"].ReadOnly = true;
        //}

        //private void dataGridView_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        //{
        //    if (e.RowIndex >= 0)
        //    {
        //        DataGridViewRow row = this.dataGridView.Rows[e.RowIndex];
        //        if (row.Cells[8].Value.ToString() != "")
        //            p_disc = Convert.ToDecimal(row.Cells[8].Value.ToString());
        //        else
        //            p_disc = 0;
        //        p_code = row.Cells[0].Value.ToString();
        //        p_quality = row.Cells[2].Value.ToString();
        //        tone = row.Cells[1].Value.ToString();
        //        p_size = row.Cells[3].Value.ToString();
        //        p_qty = Convert.ToDecimal(row.Cells[4].Value.ToString());
        //        p_tiles = Convert.ToDecimal(row.Cells[5].Value.ToString());
        //        if (row.Cells[7].Value.ToString() != "")
        //            p_price = Convert.ToDecimal(row.Cells[7].Value.ToString());
        //        else
        //            p_price = 0;
        //        Login.con.Open();
        //        SqlCommand cmd121 = new SqlCommand("SELECT isnull(pieces,0) from product where p_id = '" + p_code + "' ", Login.con);
        //        SqlDataReader dr121 = cmd121.ExecuteReader();
        //        while (dr121.Read())
        //        {
        //            db_tiles = Convert.ToInt16(dr121[0]);
        //        }
        //        dr121.Close();
        //        SqlCommand cmd = new SqlCommand("SELECT isnull(meters,0) from product where p_id = '" + p_code + "' AND size='" + p_size + "' ", Login.con);
        //        meters_in_box = (Decimal)cmd.ExecuteScalar();
        //        SqlCommand cmd2 = new SqlCommand("UPDATE fake_cart set Discount ='" + p_disc + "', Amount='" + ((((meters_in_box) * (p_qty + (p_tiles / Convert.ToDecimal(db_tiles)))) * (p_price)) - ((((meters_in_box) * (p_qty + (p_tiles / Convert.ToDecimal(db_tiles)))) * (p_price) * (p_disc)) / 100)) + "', Price='"+p_price+"' where P_Code='" + p_code + "' AND Quality='" + p_quality + "' AND Tone='" + tone + "'AND Size='" + p_size + "'", Login.con);
        //        cmd2.ExecuteNonQuery();
        //        String command = "select P_Code,Tone,Quality,Size,Boxes,Tiles,Meter,Price,Discount,Amount from fake_cart";
        //        test.bindingcode(command, dataGridView, Login.con);
        //        SqlCommand cmd3 = new SqlCommand("SELECT isnull(sum(Amount),0) from fake_cart", Login.con);
        //        SqlCommand cmd4 = new SqlCommand("SELECT isnull(sum(Amount),0) from fake_cart_s", Login.con);
        //        txttotal.Text = Convert.ToString((Decimal)cmd3.ExecuteScalar() + (Decimal)cmd4.ExecuteScalar());
        //        txtgtotal.Text = Convert.ToString(Convert.ToDecimal(txttotal.Text) - Convert.ToDecimal(txtdiscount.Text));
        //        txtbalance.Text = Convert.ToString(Convert.ToDecimal(txtgtotal.Text) - Convert.ToDecimal(txtamount.Text));
        //        Login.con.Close();
        //        dataGridView.Columns["P_Code"].ReadOnly = dataGridView.Columns["Boxes"].ReadOnly = dataGridView.Columns["Tiles"].ReadOnly = dataGridView.Columns["Tone"].ReadOnly = dataGridView.Columns["Quality"].ReadOnly = dataGridView.Columns["Size"].ReadOnly = dataGridView.Columns["Meter"].ReadOnly = dataGridView.Columns["Amount"].ReadOnly = true;
        //    }
        //}

        private void dataGridView2_Enter(object sender, EventArgs e)
        {
            dataGridView2.Columns["P_Code"].ReadOnly = dataGridView2.Columns["Quantity"].ReadOnly = dataGridView2.Columns["P_Name"].ReadOnly = dataGridView2.Columns["Amount"].ReadOnly = true;
        }

        private void dataGridView2_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = this.dataGridView2.Rows[e.RowIndex];
                if (row.Cells[4].Value.ToString() != "")
                    p_disc = Convert.ToDecimal(row.Cells[4].Value.ToString());
                else
                    p_disc = 0;
                p_code = row.Cells[0].Value.ToString();
                p_qty = Convert.ToDecimal(row.Cells[2].Value.ToString());
                if (row.Cells[3].Value.ToString() != "")
                    p_price = Convert.ToDecimal(row.Cells[3].Value.ToString());
                else
                    p_price = 0; Login.con.Open();
                SqlCommand cmd2 = new SqlCommand("UPDATE fake_cart_s set Discount ='" + p_disc + "', Amount='" + ((((p_qty)) * (p_price)) - ((((p_qty)) * (p_price) * (p_disc)) / 100)) + "', Price='" + p_price + "' where P_Code='" + p_code + "'", Login.con);
                cmd2.ExecuteNonQuery();
                String command = "select * from fake_cart_s";
                test.bindingcode(command, dataGridView2, Login.con);
                SqlCommand cmd3 = new SqlCommand("SELECT isnull(sum(Amount),0) from fake_cart", Login.con);
                SqlCommand cmd4 = new SqlCommand("SELECT isnull(sum(Amount),0) from fake_cart_s", Login.con);
                txttotal.Text = Convert.ToString((Decimal)cmd3.ExecuteScalar() + (Decimal)cmd4.ExecuteScalar());
                txtgtotal.Text = Convert.ToString(Convert.ToDecimal(txttotal.Text) - Convert.ToDecimal(txtdiscount.Text));
                txtbalance.Text = Convert.ToString(Convert.ToDecimal(txtgtotal.Text) - Convert.ToDecimal(txtamount.Text));
                Login.con.Close();
                dataGridView2.Columns["P_Code"].ReadOnly = dataGridView2.Columns["Quantity"].ReadOnly = dataGridView2.Columns["P_Name"].ReadOnly = dataGridView2.Columns["Amount"].ReadOnly = true;
            }
        }

        private void txtdiscount_KeyPress(object sender, KeyPressEventArgs e)
        {
            test.decimaldigit_correction(sender, e);
        }

        private void txtdiscount_Leave(object sender, EventArgs e)
        {
            if (txtdiscount.Text == "")
                txtdiscount.Text = "0";
            else if (Convert.ToDecimal(txtdiscount.Text) == 0)
                txtdiscount.Text = "0";
            txtgtotal.Text = Convert.ToString(Convert.ToDecimal(txttotal.Text) - Convert.ToDecimal(txtdiscount.Text));
            txtbalance.Text = Convert.ToString(Convert.ToDecimal(txtgtotal.Text) - Convert.ToDecimal(txtamount.Text));
        }

        private void txtdiscount_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                txtamount.Focus();
            }
        }

        private void txtdiscount_MouseDown(object sender, MouseEventArgs e)
        {
            txtdiscount.SelectAll();
        }

        private void txtamount_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                amt = true;
                if (amt)
                {
                    btninvoice.Enabled = true;
                }
                btninvoice.Focus();
            }
        }

        private void txtamount_KeyPress(object sender, KeyPressEventArgs e)
        {
            test.decimaldigit_correction(sender, e);
        }

        private void txtamount_Leave(object sender, EventArgs e)
        {
            if (txtamount.Text == "")
                txtamount.Text = "0";
            else if (Convert.ToDecimal(txtamount.Text) == 0)
                txtamount.Text = "0";
            txtbalance.Text = Convert.ToString(Convert.ToDecimal(txtgtotal.Text) - Convert.ToDecimal(txtamount.Text));
        }

        private void txtamount_MouseDown(object sender, MouseEventArgs e)
        {
            txtamount.SelectAll();
        }

        private void btninvoice_Click(object sender, EventArgs e)
        {
            Login.con.Open();
            SqlCommand cmd5 = new SqlCommand("Select m_id,c_id from sale_stock where ss_id='" + finvoice + "'", Login.con);
            SqlDataReader dr = cmd5.ExecuteReader();
            while (dr.Read())
            {
                if (Convert.ToString(dr[0]) != DBNull.Value.ToString())
                {
                    dr.Close();
                    SqlCommand cmd6 = new SqlCommand("INSERT into fake_stock(fk_id,m_id,datetime,total,discount,receive) values('" + Convert.ToDecimal(finvoice) + "','" + customer_id + "', '" + date + "', '" + Convert.ToDecimal(txttotal.Text) + "', '" + Convert.ToDecimal(txtdiscount.Text) + "', '" + Convert.ToDecimal(txtamount.Text) + "')", Login.con);
                    cmd6.ExecuteNonQuery();
                }
                else
                {
                    dr.Close();
                    SqlCommand cmd6 = new SqlCommand("INSERT into fake_stock(fk_id,c_id,datetime,total,discount,receive) values('" + Convert.ToDecimal(finvoice) + "','" + customer_id + "', '" + date + "', '" + Convert.ToDecimal(txttotal.Text) + "', '" + Convert.ToDecimal(txtdiscount.Text) + "', '" + Convert.ToDecimal(txtamount.Text) + "')", Login.con);
                    cmd6.ExecuteNonQuery();
                }
                break;
            }
            dr.Close();
            //if (dataGridView.Rows.Count != 0)
            //{
            while (true)
            {
                SqlCommand cmd8 = new SqlCommand("SELECT Boxes,P_Code,Price,Quality,Tone,Discount,Size,Tiles from fake_cart", Login.con);
                SqlDataReader dr8 = cmd8.ExecuteReader();
                if (dr8.HasRows)
                {
                    if (dr8.Read())
                    {
                        p_qty = Convert.ToDecimal(dr8[0]);
                        p_code = Convert.ToString(dr8[1]);
                        p_price = Convert.ToDecimal(dr8[2]);
                        p_disc = Convert.ToDecimal(dr8[5]);
                        p_quality = Convert.ToString(dr8[3]);
                        tone = Convert.ToString(dr8[4]);
                        p_size = Convert.ToString(dr8[6]);
                        p_tiles = Convert.ToDecimal(dr8[7]);
                        dr8.Close();
                        SqlCommand cmd9 = new SqlCommand("INSERT into fake_stock_detail values('" + Convert.ToDecimal(finvoice) + "','" + p_code + "','" + p_price + "','" + p_qty + "','" + p_disc + "','" + p_quality + "','" + tone + "','" + p_size + "','" + p_tiles + "')", Login.con);
                        cmd9.ExecuteNonQuery();
                        SqlCommand cmd10 = new SqlCommand("DELETE fake_cart where P_Code='" + p_code + "' AND Quality='" + p_quality + "'AND Tone='" + tone + "'AND Size='" + p_size + "' ", Login.con);
                        cmd10.ExecuteNonQuery();
                    }
                }
                else
                {
                    dr8.Close();
                    break;
                }
            }
            //}
            if (dataGridView2.Rows.Count != 0)
            {
                while (true)
                {
                    SqlCommand cmd4 = new SqlCommand("SELECT * from fake_cart_s", Login.con);
                    SqlDataReader dr4 = cmd4.ExecuteReader();
                    if (dr4.HasRows)
                    {
                        if (dr4.Read())
                        {
                            p_code = Convert.ToString(dr4[0]);
                            p_name = Convert.ToString(dr4[1]);
                            p_qty = Convert.ToDecimal(dr4[2]);
                            p_price = Convert.ToDecimal(dr4[3]);
                            p_disc = Convert.ToDecimal(dr4[4]);
                            dr4.Close();
                            SqlCommand cmd7 = new SqlCommand("INSERT into fake_stock_detail2 values('" + Convert.ToDecimal(finvoice) + "','" + p_code + "','" + p_price + "','" + p_qty + "','" + p_disc + "')", Login.con);
                            cmd7.ExecuteNonQuery();
                            SqlCommand cmd8 = new SqlCommand("DELETE fake_cart_s where P_Code='" + p_code + "'", Login.con);
                            cmd8.ExecuteNonQuery();
                        }
                    }
                    else
                    {
                        dr4.Close();
                        break;
                    }
                }
            }
            Login.con.Close();
            DialogResult dresult = MessageBox.Show("Press 'OK' to Print Estimated Invoice", "AR Tranders", MessageBoxButtons.OKCancel, MessageBoxIcon.Question);
            if (dresult == DialogResult.OK)
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
            this.Close();
        }

        private void btncancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void printDocument1_PrintPage_1(object sender, PrintPageEventArgs e)
        {
            int startX = 50;
            int startY = 150;
            int Offset = 40;
            e.Graphics.DrawString("Estimated Report", new Font("Arial", 28, FontStyle.Bold), Brushes.Black, 250, 50);
            e.Graphics.DrawString("Invoice No:", new Font("Calibri", 12), new SolidBrush(Color.Black), startX, startY + Offset);
            e.Graphics.DrawString(finvoice, new Font("Calibri", 12, FontStyle.Bold), new SolidBrush(Color.Black), startX + 85, startY + Offset);
            Offset = Offset + 30;
            Login.con.Open();
            SqlCommand cmd3 = new SqlCommand("SELECT c_name from customer where c_id='" + customer_id + "'", Login.con);
            e.Graphics.DrawString("Customer Name:", new Font("Calibri", 10, FontStyle.Bold), new SolidBrush(Color.Black), startX, startY + Offset);
            e.Graphics.DrawString(Convert.ToString((String)cmd3.ExecuteScalar()), new Font("Calibri", 10), new SolidBrush(Color.Black), startX + 105, startY + Offset);
            Offset = Offset + 20;
            Login.con.Close();
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
                e.Graphics.DrawString("P_Name", new Font("Calibri", 10, FontStyle.Bold), new SolidBrush(Color.Black), startX + 45, startY + Offset);
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
                    e.Graphics.DrawString(Convert.ToString(dataGridView2.Rows[j].Cells[1].Value), new Font("Calibri", 9), new SolidBrush(Color.Black), startX + 45, startY + Offset);
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
            e.Graphics.DrawString(underLine, new Font("Courier New", 10, FontStyle.Bold), new SolidBrush(Color.Black), startX, startY + Offset);
            Offset = Offset + 20;
            e.Graphics.DrawString("Total:", new Font("Calibri", 10, FontStyle.Bold), new SolidBrush(Color.Black), startX + 540, startY + Offset);
            e.Graphics.DrawString(txttotal.Text, new Font("Calibri", 10), new SolidBrush(Color.Black), startX + 665, startY + Offset);
            Offset = Offset + 20;
            e.Graphics.DrawString(underLine, new Font("Courier New", 10, FontStyle.Bold), new SolidBrush(Color.Black), startX, startY + Offset);
            Offset = Offset + 20;
            e.Graphics.DrawString("Discount:", new Font("Calibri", 10, FontStyle.Bold), new SolidBrush(Color.Black), startX + 540, startY + Offset);
            e.Graphics.DrawString(txtdiscount.Text, new Font("Calibri", 10), new SolidBrush(Color.Black), startX + 665, startY + Offset);
            Offset = Offset + 20;
            e.Graphics.DrawString("Grand Total:", new Font("Calibri", 10, FontStyle.Bold), new SolidBrush(Color.Black), startX + 540, startY + Offset);
            e.Graphics.DrawString(txtgtotal.Text, new Font("Calibri", 10), new SolidBrush(Color.Black), startX + 665, startY + Offset);
            Offset = Offset + 20;
            e.Graphics.DrawString("Credit:", new Font("Calibri", 10, FontStyle.Bold), new SolidBrush(Color.Black), startX + 540, startY + Offset);
            e.Graphics.DrawString(txtamount.Text, new Font("Calibri", 10), new SolidBrush(Color.Black), startX + 665, startY + Offset);
            Offset = Offset + 20;
            e.Graphics.DrawString(underLine, new Font("Courier New", 10, FontStyle.Bold), new SolidBrush(Color.Black), startX, startY + Offset);
            Offset = Offset + 20;
            e.Graphics.DrawString("Balance:", new Font("Calibri", 10, FontStyle.Bold), new SolidBrush(Color.Black), startX + 540, startY + Offset);
            e.Graphics.DrawString(((Convert.ToDecimal(txtgtotal.Text)) - Convert.ToDecimal(txtamount.Text)).ToString(), new Font("Calibri", 10), new SolidBrush(Color.Black), startX + 665, startY + Offset);
            Offset = Offset + 20;
            e.Graphics.DrawString(underLine, new Font("Courier New", 10, FontStyle.Bold), new SolidBrush(Color.Black), startX, startY + Offset);
            Offset = Offset + 20;
            e.Graphics.DrawString("Signature:  __________________", new Font("Calibri", 10, FontStyle.Bold), new SolidBrush(Color.Black), startX, startY + Offset);
            e.Graphics.DrawString("Date & Time:  ", new Font("Calibri", 10, FontStyle.Bold), new SolidBrush(Color.Black), startX + 320, startY + Offset);
            e.Graphics.DrawString(DateTime.Now.ToString(), new Font("Calibri", 10), new SolidBrush(Color.Black), startX + 400, startY + Offset);
            Offset = Offset + 25;
            e.Graphics.DrawString("Developed By: Geeks IT Solutions 03126424443  ", new Font("Calibri", 8, FontStyle.Bold), new SolidBrush(Color.Black), startX + 280, startY + Offset + 5);
            e.Graphics.PageUnit = GraphicsUnit.Inch;
            i = j = 0;
        }
    }
}
