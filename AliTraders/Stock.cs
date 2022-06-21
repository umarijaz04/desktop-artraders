using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace AhmadSanitary
{
    public partial class Stock : Form
    {
        Form opener;
        Int32 db_tiles = 0;
        Decimal p_disc, p_tiles, pdebit, pcredit, m_id, p_qty, p_price, meters_in_box, sdebit, scredit, srdebit, prcredit, prdebit, srcredit, tdebit, tcredit;
        String p_code, p_quality, delid, tone, p_size, p_name, type;
        Boolean data, cmb, dc = false;
        UF.Class1 test = new UF.Class1();

        public Stock(Form form)
        {
            InitializeComponent();
            opener = form;
        }

        private void Stock_Load(object sender, EventArgs e)
        {
            opener.Hide();
            string command = "SELECT p_name from product2 order by p_name";
            test.cmbox_drop(command, cmbname, Login.con);
            string command2 = "SELECT CAST(p_id as varchar(50)) from product2 order by p_id";
            test.cmbox_drop(command2, txtcode, Login.con);
            //string command2 = "SELECT p_id from product order by p_id";
            //test.cmbox_drop(command2, cmbcode, Login.con);
            cart_clear();
        }

        private void Stock_FormClosing(object sender, FormClosingEventArgs e)
        {
            opener.Show();
        }

        private void Stock_EnabledChanged(object sender, EventArgs e)
        {
            string command = "SELECT p_name from product2";
            test.cmbox_drop(command, cmbname, Login.con);
            string command2 = "SELECT CAST(p_id as varchar(50)) from product2 order by p_id";
            test.cmbox_drop(command2, txtcode, Login.con);
            test.cmbox_drop("select name from manufacturer order by name", cmbmanu, Login.con);
            //string command2 = "SELECT p_id from product";
            //test.cmbox_drop(command2, cmbcode, Login.con);
        }

        //private void set_qty()
        //{
        //    if (cmbcode.Text != "" && cmbsize.Text != "")
        //    {
        //        Login.con.Open();
        //        SqlCommand cmd = new SqlCommand("SELECT pieces from product where p_id='" + cmbcode.Text + "' AND size='" + cmbsize.Text + "'", Login.con);
        //        SqlDataReader dr = cmd.ExecuteReader();
        //        if (dr.HasRows)
        //        {
        //            while (dr.Read())
        //            {
        //                db_tiles = Convert.ToInt32(dr[0]);
        //            }
        //        }
        //        dr.Close();
        //        Login.con.Close();
        //        txtqty.Text = (Convert.ToInt32(txtqty.Text) + (Convert.ToInt32(txttiles.Text) / db_tiles)).ToString();
        //        txttiles.Text = (Convert.ToInt32(txttiles.Text) % db_tiles).ToString();
        //    }
        //}

        void cart_clear()
        {
            cmbmanu.Text = txtdcno.Text = cmbdc.Text = "";
            cmbquality2.SelectedIndex = 1;
            txttotal.Text = txtpaid.Text = txtbalance.Text = txtprebalance.Text = "0";
            cmbquality2.Enabled = cmbmanu.Enabled = dateTimePicker.Enabled = txtdcno.Enabled = true;
            dateTimePicker.Value = DateTime.Now;
            delid = null;
            cmbdc_Enter(this, null);
            btnsubmit.Enabled = data = cmb = dc = false;
            rdbtn_CheckedChanged(this, null);
            rdbtn2_CheckedChanged(this, null);
        }

        private void entercartdata()
        {
            Login.con.Open();
            //if (ckbx.Checked == true)
            //{
            //    if (txttone.Text != "" && cmbsize.Text != "" && (txtqty.Text != "0" || txttiles.Text != "0"))
            //    {
            //        SqlCommand cmd6 = new SqlCommand("SELECT isnull(meters,0) from product where p_id = '" + cmbcode.Text + "' AND size= '" + cmbsize.Text + "'", Login.con);
            //        meters_in_box = (Decimal)cmd6.ExecuteScalar();
            //        SqlCommand cmd = new SqlCommand("SELECT Boxes,Tiles from purchase_cart where P_Code ='" + cmbcode.Text + "' AND Quality ='" + cmbquality.Text + "' AND Tone = '" + txttone.Text + "' AND Size= '" + cmbsize.Text + "'", Login.con);
            //        SqlDataReader dr = cmd.ExecuteReader();
            //        if (dr.HasRows)
            //        {
            //            MessageBox.Show("This Product Already Exist in Purchases Cart", "AR Tranders", MessageBoxButtons.OK, MessageBoxIcon.Stop);
            //        }
            //        else
            //        {
            //            dr.Close();
            //            SqlCommand cmd2 = new SqlCommand("INSERT into purchase_cart(P_Code,Tone,Quality,Size,Boxes,Tiles,Meters,Price,Amount) values('" + cmbcode.Text + "','" + txttone.Text.ToUpper() + "','" + cmbquality.Text + "','" + cmbsize.Text + "','" + Convert.ToDecimal(txtqty.Text) + "','" + Convert.ToDecimal(txttiles.Text) + "','" + (((Convert.ToDecimal(txttiles.Text) / db_tiles)+ Convert.ToDecimal(txtqty.Text)) * meters_in_box) + "', '" + Convert.ToDecimal(txtprice.Text) + "', '" + (((Convert.ToDecimal(txttiles.Text) / db_tiles) + Convert.ToDecimal(txtqty.Text)) * Convert.ToDecimal(txtprice.Text)) + "')", Login.con);
            //            cmd2.ExecuteNonQuery();
            //            SqlCommand cmd3 = new SqlCommand("SELECT isnull(sum(Amount),0) from purchase_cart", Login.con);
            //            SqlCommand cmd4 = new SqlCommand("SELECT isnull(sum(Amount),0) from purchase_cart_s", Login.con);
            //            txttotal.Text = Convert.ToString((Decimal)cmd4.ExecuteScalar() + (Decimal)cmd3.ExecuteScalar());
            //            txtbalance.Text = (Convert.ToDecimal(txtpaid.Text) - Convert.ToDecimal(txttotal.Text)).ToString();
            //            test.bindingcode("SELECT P_Code,Tone,Quality,Size,Boxes,Tiles,Meters,Price,Discount,Amount from purchase_cart", dataGridView, Login.con);
            //            txtqty.Text = txttiles.Text = "0";
            //            lbname.Text = cmbcode.Text = txtprice.Text = txttone.Text = "";
            //            cmbsize.Items.Clear();
            //            cmbcode.Focus();
            //            cmbquality.SelectedIndex = 0;
            //        }
            //    }
            //}
            //if (ckbx2.Checked == true)
            //{
            if (txtprice2.Text != "")
            {
                SqlCommand cmd1 = new SqlCommand("SELECT Quantity, Price from purchase_cart_s where P_Code ='" + txtcode.Text + "'", Login.con);
                SqlDataReader dr1 = cmd1.ExecuteReader();
                if (dr1.HasRows)
                {
                    MessageBox.Show("This Product Already Exist in Purchases Cart", "AR Tranders", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                    dr1.Close();
                }
                else
                {
                    dr1.Close();
                    SqlCommand cmd3 = new SqlCommand("INSERT into purchase_cart_s(Quantity,P_Code,P_Name,Price,Amount) values('" + Convert.ToDecimal(txtqty2.Text) + "','" + txtcode.Text + "','" + cmbname.Text + "', '" + Convert.ToDecimal(txtprice2.Text) + "','" + Convert.ToDecimal(txtqty2.Text) * Convert.ToDecimal(txtprice2.Text) + "')", Login.con);
                    cmd3.ExecuteNonQuery();
                    SqlCommand cmd4 = new SqlCommand("SELECT isnull(sum(Amount),0) from purchase_cart_s", Login.con);
                    //SqlCommand cmd5 = new SqlCommand("SELECT isnull(sum(Amount),0) from purchase_cart", Login.con);
                    txttotal.Text = Convert.ToString((Decimal)cmd4.ExecuteScalar());
                    txtbalance.Text = (Convert.ToDecimal(txtpaid.Text) - Convert.ToDecimal(txttotal.Text)).ToString();
                    test.bindingcode("SELECT * from purchase_cart_s", dataGridView2, Login.con);
                    txtqty2.Text = "1";
                    txtcode.Text = "";
                    cmbname.Text = "";
                    txtprice2.Clear();
                    cmbname.Focus();
                }
            }
            //}
            data = true;
            if (txtdcno.Text != "")
            {
                cmb = dc = true;
            }
            if (data && cmb && dc)
            {
                btnsubmit.Enabled = true;
            }
            Login.con.Close();
        }

        //Add Tiles Stock
        private void rdbtn_CheckedChanged(object sender, EventArgs e)
        {
            //if (ckbx2.Checked == false)
            //    ckbx.Checked = true;
            //if (ckbx.Checked == true)
            //{
            //    txtqty.Enabled = txttone.Enabled = label1.Enabled = txttiles.Enabled = cmbcode.Enabled = cmbsize.Enabled = cmbquality.Enabled = txtprice.Enabled = lbname.Enabled = dataGridView.Enabled = label16.Enabled = label2.Enabled = label18.Enabled = label15.Enabled = label4.Enabled = label6.Enabled = true;
            //    cmbcode.Focus();
            //}
            //else
            //    txtqty.Enabled = txttone.Enabled=label1.Enabled = txttiles.Enabled = cmbcode.Enabled = cmbsize.Enabled = cmbquality.Enabled = txtprice.Enabled = lbname.Enabled = dataGridView.Enabled = label16.Enabled = label2.Enabled = label18.Enabled = label15.Enabled = label4.Enabled = label6.Enabled = false;
            //txtqty.Text = txttiles.Text = "0";
            //txttone.Text = lbname.Text = txtprice.Text = cmbcode.Text = "";
            //cmbsize.Items.Clear();
            //cmbquality.SelectedIndex = 0;
            cmbquality2.SelectedIndex = 1;
            //Login.con.Open();
            //SqlCommand cmd = new SqlCommand("DELETE from purchase_cart", Login.con);
            //cmd.ExecuteNonQuery();
            //Login.con.Close();
            //test.bindingcode("SELECT P_Code,Tone,Quality,Size,Boxes,Tiles,Meters,Price,Discount,Amount from purchase_cart", dataGridView, Login.con);
        }

        private void cmbdc_Enter(object sender, EventArgs e)
        {
            test.cmbox_drop("SELECT CAST(ps_id as varchar(50)) from purchase_stock", cmbdc, Login.con);
        }

        private void cmbdc_SelectedIndexChanged(object sender, EventArgs e)
        {
            txttotal.Text = txtbalance.Text = txtpaid.Text = "0";
            Login.con.Open();
            //SqlCommand cmd112 = new SqlCommand("SELECT ps_id from purchase_stock_detail where ps_id = '" + cmbdc.Text + "'", Login.con);
            //SqlDataReader dr112 = cmd112.ExecuteReader();
            //if (dr112.HasRows)
            //{
            //    dr112.Close();
            //    Login.con.Close();
            //    //ckbx.Checked = true;
            //    Login.con.Open();
            //    SqlCommand cmd = new SqlCommand("Delete purchase_cart", Login.con);
            //    cmd.ExecuteNonQuery();
            //    SqlDataAdapter a = new SqlDataAdapter("SELECT qty,p_id,pprice,quality,size,tone,discount,tiles from purchase_stock_detail where ps_id='" + cmbdc.Text + "'", Login.con);
            //    DataTable dataTable1 = new DataTable();
            //    a.Fill(dataTable1);
            //    foreach (DataRow row in dataTable1.Rows)
            //    {
            //        SqlCommand cmd111 = new SqlCommand("SELECT pieces from product where p_id = '" + row["p_id"].ToString() + "' AND size= '" + row["size"].ToString() + "'", Login.con);
            //        SqlDataReader dr111 = cmd111.ExecuteReader();
            //        if (dr111.HasRows)
            //        {
            //            while (dr111.Read())
            //            {
            //                db_tiles = Convert.ToInt32(dr111[0]);
            //            }
            //        }
            //        dr111.Close();
            //        SqlCommand cmd3 = new SqlCommand("SELECT isnull(meters,0) from product where p_id = '" + row["p_id"].ToString() + "' AND size= '" + row["size"].ToString() + "'", Login.con);
            //        meters_in_box = (Decimal)cmd3.ExecuteScalar();
            //        SqlCommand cmd2 = new SqlCommand("INSERT into purchase_cart values('" + row["p_id"].ToString() + "','" + row["tone"].ToString() + "','" + row["quality"].ToString() + "','" + row["size"].ToString() + "', '" + Convert.ToDecimal(row["qty"]) + "', '" + (((Convert.ToDecimal(row["tiles"]) / db_tiles) + Convert.ToDecimal(row["qty"])) * meters_in_box) + "', '" + Convert.ToDecimal(row["pprice"]) + "', '" + Convert.ToDecimal(row["discount"]) + "', '0','" + Convert.ToDecimal(row["tiles"]) + "')", Login.con);
            //        cmd2.ExecuteNonQuery();
            //    }
            //}
            //else
            //{
            //    dr112.Close();
            //    Login.con.Close();
            //    //ckbx.Checked = false;
            //    Login.con.Open();
            //}
            SqlCommand cmd113 = new SqlCommand("SELECT ps_id from purchase_stock_detail2 where ps_id = '" + cmbdc.Text + "'", Login.con);
            SqlDataReader dr113 = cmd113.ExecuteReader();
            if (dr113.HasRows)
            {
                dr113.Close();
                Login.con.Close();
                //ckbx2.Checked = true;
                Login.con.Open();
                SqlCommand cmd22 = new SqlCommand("Delete purchase_cart_s", Login.con);
                cmd22.ExecuteNonQuery();
                SqlDataAdapter a2 = new SqlDataAdapter("SELECT qty,p_id,pprice from purchase_stock_detail2 where ps_id='" + cmbdc.Text + "'", Login.con);
                DataTable dataTable2 = new DataTable();
                a2.Fill(dataTable2);
                foreach (DataRow row in dataTable2.Rows)
                {
                    SqlCommand cmd3 = new SqlCommand("SELECT p_name from product2 where p_id = '" + row["p_id"].ToString() + "'", Login.con);
                    SqlDataReader dr3 = cmd3.ExecuteReader();
                    while (dr3.Read())
                    {
                        p_name = Convert.ToString(dr3[0]);
                    }
                    dr3.Close();
                    SqlCommand cmd2 = new SqlCommand("INSERT into purchase_cart_s values('" + row["p_id"].ToString() + "','" + p_name + "', '" + Convert.ToDecimal(row["qty"]) + "','" + Convert.ToDecimal(row["pprice"]) + "', '0', '" + Convert.ToDecimal(row["qty"]) * Convert.ToDecimal(row["pprice"]) + "')", Login.con);
                    cmd2.ExecuteNonQuery();
                }
            }
            else
            {
                dr113.Close();
                Login.con.Close();
                //ckbx2.Checked = false;
                Login.con.Open();
            }
            SqlCommand cmd5 = new SqlCommand("SELECT datetime,m_id,c_id from purchase_stock where ps_id='" + cmbdc.Text + "'", Login.con);
            SqlDataReader dr5 = cmd5.ExecuteReader();
            while (dr5.Read())
            {
                txtdcno.Enabled = false;
                dateTimePicker.Value = Convert.ToDateTime(dr5[0]);
                if (Convert.ToString(dr5[1]) != DBNull.Value.ToString())
                {
                    m_id = Convert.ToDecimal(dr5[1]);
                    dr5.Close();
                    SqlCommand cmd6 = new SqlCommand("SELECT name from manufacturer where m_id='" + m_id + "'", Login.con);
                    SqlDataReader dr6 = cmd6.ExecuteReader();
                    while (dr6.Read())
                    {
                        cmbmanu.Text = Convert.ToString(dr6[0]);
                        cmbquality2.SelectedItem = "Manufacturer";
                    }
                    dr6.Close();
                }
                else
                {
                    m_id = Convert.ToDecimal(dr5[2]);
                    dr5.Close();
                    SqlCommand cmd6 = new SqlCommand("SELECT c_name,type from customer where c_id='" + m_id + "'", Login.con);
                    SqlDataReader dr6 = cmd6.ExecuteReader();
                    while (dr6.Read())
                    {
                        cmbmanu.Text = Convert.ToString(dr6[0]);
                        if (Convert.ToString(dr6[1]) == "FIXED")
                            cmbquality2.SelectedItem = "Customer";
                        else
                            cmbquality2.SelectedItem = "Stakeholder";
                    }
                    dr6.Close();
                }
                break;
            }
            Login.con.Close();
            //test.bindingcode("SELECT P_Code,Tone,Quality,Size,Boxes,Tiles,Meters,Price,Discount,Amount from purchase_cart", dataGridView, Login.con);
            test.bindingcode("SELECT  * from purchase_cart_s", dataGridView2, Login.con);
            cmbmanu.Focus();
            cmbmanu_KeyDown(this, new KeyEventArgs(Keys.Enter));
            txtdcno.Text = cmbdc.Text;
            cmbmanu.Enabled = cmbquality2.Enabled = false;
            //= txtqty2.Enabled = txtcode.Enabled = cmbname.Enabled = txtprice2.Enabled
            btnsubmit.Enabled = dataGridView2.Columns["P_Code"].ReadOnly = dataGridView2.Columns["P_Name"].ReadOnly = dataGridView2.Columns["Amount"].ReadOnly = true;
            //dataGridView2.Columns["Quantity"].ReadOnly = true;
        }

        //private void txtqty_Leave(object sender, EventArgs e)
        //{
        //if (txtqty.Text == "" || Convert.ToDecimal(txtqty.Text) == 0)
        //    txtqty.Text = "0";
        //}

        //private void txtqty_KeyPress(object sender, KeyPressEventArgs e)
        //{
        //    test.digit_correction(sender, e);
        //}

        //private void txtqty_MouseClick(object sender, MouseEventArgs e)
        //{
        //    txtqty.SelectAll();
        //}

        //private void txttiles_Leave(object sender, EventArgs e)
        //{
        //if (cmbcode.Text != "" && cmbsize.Text != "")
        //{
        //    set_qty();
        //}
        //else if (txttiles.Text == "")
        //{
        //    txttiles.Text = "0";
        //}
        //}

        //private void txttiles_MouseClick(object sender, MouseEventArgs e)
        //{
        //txttiles.SelectAll();
        //}

        //private void txttiles_KeyPress(object sender, KeyPressEventArgs e)
        //{
        //    test.digit_correction(sender, e);
        //}

        //private void cmbcode_KeyDown(object sender, KeyEventArgs e)
        //{
        //if (e.KeyCode == Keys.Enter)
        //{
        //    if (cmbcode.Text != "")
        //    {
        //        cmbcode.Text = cmbcode.Text.ToUpper();
        //        cmbcode.SelectAll();
        //        cmbsize.Items.Clear();
        //        Login.con.Open();
        //        SqlCommand cmd = new SqlCommand("SELECT p_name from product where p_id='" + cmbcode.Text + "'", Login.con);
        //        SqlDataReader dr = cmd.ExecuteReader();
        //        if (!dr.HasRows)
        //        {
        //            dr.Close();
        //            MessageBox.Show("Record Not Found", "AR Tranders", MessageBoxButtons.OK, MessageBoxIcon.Stop);
        //            lbname.Text = "";
        //        }
        //        Login.con.Close();
        //    }
        //}
        //}

        //private void cmbcode_Leave(object sender, EventArgs e)
        //{
        //    if (cmbcode.Text == "")
        //    {
        //        lbname.Text = "";
        //        cmbsize.Items.Clear();
        //    }
        //    else
        //    {
        //        cmbcode.Text = cmbcode.Text.ToUpper();
        //        cmbcode.SelectAll();
        //        Login.con.Open();
        //        SqlCommand cmd = new SqlCommand("SELECT p_name from product where p_id='" + cmbcode.Text + "'", Login.con);
        //        SqlDataReader dr = cmd.ExecuteReader();
        //        if (!dr.HasRows)
        //        {
        //            dr.Close();
        //            MessageBox.Show("Record Not Found", "AR Tranders", MessageBoxButtons.OK, MessageBoxIcon.Stop);
        //            lbname.Text = "";
        //            cmbsize.Items.Clear();
        //            cmbcode.Focus();
        //        }
        //        Login.con.Close();
        //    }
        //}

        //private void cmbsize_Enter(object sender, EventArgs e)
        //{
        //    if (cmbcode.Text != "")
        //    {
        //        test.cmbox_drop("select DISTINCT size from product where p_id='" + cmbcode.Text + "'", cmbsize, Login.con);
        //    }
        //}

        //private void cmbsize_SelectedIndexChanged(object sender, EventArgs e)
        //{
        //    Login.con.Open();
        //    SqlCommand cmd = new SqlCommand("SELECT p_name from product where p_id='" + cmbcode.Text + "' AND size='" + cmbsize.Text + "'", Login.con);
        //    SqlDataReader dr = cmd.ExecuteReader();
        //    if (dr.HasRows)
        //    {
        //        while (dr.Read())
        //        {
        //            lbname.Text = Convert.ToString(dr[0]);
        //        }
        //        dr.Close();
        //    }
        //    else
        //    {
        //        dr.Close();
        //        lbname.Text = "";
        //    }
        //    Login.con.Close();
        //}

        //private void txtprice_KeyDown(object sender, KeyEventArgs e)
        //{
        //    if (e.KeyCode == Keys.Enter)
        //    {
        //        if (txtprice.Text == "" || Convert.ToDecimal(txtprice.Text) == 0)
        //        {
        //            txtprice.Text = "0";
        //            txtprice.SelectAll();
        //        }
        //        entercartdata();
        //    }
        //}

        //private void txtprice_KeyPress(object sender, KeyPressEventArgs e)
        //{
        //    test.decimaldigit_correction(sender, e);
        //}

        //private void dataGridView_CellClick(object sender, DataGridViewCellEventArgs e)
        //{
        //    if (e.RowIndex >= 0)
        //    {
        //        DataGridViewRow row = this.dataGridView.Rows[e.RowIndex];
        //        delid = row.Cells[0].Value.ToString();
        //        p_quality = row.Cells[2].Value.ToString();
        //        tone = row.Cells[1].Value.ToString();
        //        p_size = row.Cells[3].Value.ToString();
        //    }
        //}

        //private void dataGridView_KeyDown(object sender, KeyEventArgs e)
        //{
        //    if (e.KeyCode == Keys.Delete)
        //    {
        //        if (txtdcno.Enabled != false)
        //        {
        //            Login.con.Open();
        //            SqlCommand cmd = new SqlCommand("DELETE from purchase_cart where P_Code='" + delid + "' AND Quality='" + p_quality + "' AND Tone='" + tone + "'AND Size='" + p_size + "'", Login.con);
        //            cmd.ExecuteNonQuery();
        //            test.bindingcode("SELECT P_Code,Tone,Quality,Size,Boxes,Tiles,Meters,Price,Discount,Amount from purchase_cart", dataGridView, Login.con);
        //            SqlCommand cmd4 = new SqlCommand("SELECT isnull(sum(Amount),0) from purchase_cart_s", Login.con);
        //            SqlCommand cmd5 = new SqlCommand("SELECT isnull(sum(Amount),0) from purchase_cart", Login.con);
        //            txttotal.Text = Convert.ToString((Decimal)cmd4.ExecuteScalar() + (Decimal)cmd5.ExecuteScalar());
        //            txtbalance.Text = (Convert.ToDecimal(txtpaid.Text) - Convert.ToDecimal(txttotal.Text)).ToString();
        //            Login.con.Close();
        //            if (dataGridView.RowCount == 0)
        //                if(dataGridView2.RowCount == 0)
        //                {
        //                    data = false;
        //                    btnsubmit.Enabled = false;
        //                }
        //        }
        //    }
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
        //        SqlCommand cmd121 = new SqlCommand("SELECT isnull(pieces,0) from product where p_id = '" + p_code + "' AND size='"+p_size+"' ", Login.con);
        //        SqlDataReader dr121 = cmd121.ExecuteReader();
        //        while (dr121.Read())
        //        {
        //            db_tiles = Convert.ToInt16(dr121[0]);
        //        }
        //        dr121.Close();
        //        Login.con.Close();
        //        if (row.Cells[8].Value.ToString() != "")
        //            p_disc = Convert.ToDecimal(row.Cells[8].Value.ToString());
        //        else
        //            p_disc = 0;
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
        //        if (row.Cells[7].Value.ToString() != "")
        //            p_price = Convert.ToDecimal(row.Cells[7].Value.ToString());
        //        else
        //            p_price = 0;

        //    }
        //    Login.con.Open();
        //    SqlCommand cmd = new SqlCommand("SELECT isnull(meters,0) from product where p_id = '" + p_code + "' ", Login.con);
        //    meters_in_box = (Decimal)cmd.ExecuteScalar();
        //    SqlCommand cmd2 = new SqlCommand("UPDATE purchase_cart set Discount ='" + p_disc + "', Amount='" + ((((p_tiles / db_tiles) + p_qty) * (p_price)) - ((((p_tiles / db_tiles) + p_qty) * (p_price) * (p_disc)) / 100)) + "' ,Price='" + p_price + "',Boxes='" + p_qty + "',Tiles='" + p_tiles + "' ,Meters= '" + (((p_tiles/db_tiles) + p_qty) * meters_in_box) + "' where P_Code='" + p_code + "' AND Quality='" + p_quality + "' AND Tone='" + tone + "' AND Size='" + p_size + "'", Login.con);
        //    cmd2.ExecuteNonQuery();
        //    test.bindingcode("select P_Code,Tone,Quality,Size,Boxes,Tiles,Meters,Price,Discount,Amount from purchase_cart", dataGridView, Login.con);
        //    SqlCommand cmd4 = new SqlCommand("SELECT isnull(sum(Amount),0) from purchase_cart_s", Login.con);
        //    SqlCommand cmd5 = new SqlCommand("SELECT isnull(sum(Amount),0) from purchase_cart", Login.con);
        //    txttotal.Text = Convert.ToString((Decimal)cmd4.ExecuteScalar() + (Decimal)cmd5.ExecuteScalar());
        //    txtbalance.Text = (Convert.ToDecimal(txtpaid.Text) - Convert.ToDecimal(txttotal.Text)).ToString();
        //    Login.con.Close();
        //    if(txtdcno.Enabled != false)
        //        dataGridView.Columns["P_Code"].ReadOnly = dataGridView.Columns["Tone"].ReadOnly = dataGridView.Columns["Quality"].ReadOnly = dataGridView.Columns["Size"].ReadOnly = dataGridView.Columns["Meters"].ReadOnly = dataGridView.Columns["Amount"].ReadOnly = true;
        //    else
        //        dataGridView.Columns["Boxes"].ReadOnly = dataGridView.Columns["Tiles"].ReadOnly = btnsubmit.Enabled = dataGridView.Columns["P_Code"].ReadOnly = dataGridView.Columns["Tone"].ReadOnly = dataGridView.Columns["Quality"].ReadOnly = dataGridView.Columns["Size"].ReadOnly = dataGridView.Columns["Meters"].ReadOnly = dataGridView.Columns["Amount"].ReadOnly = true;
        //}

        //private void dataGridView_Enter(object sender, EventArgs e)
        //{
        //    dataGridView.Columns["P_Code"].ReadOnly = dataGridView.Columns["Tone"].ReadOnly = dataGridView.Columns["Quality"].ReadOnly = dataGridView.Columns["Size"].ReadOnly = dataGridView.Columns["Meters"].ReadOnly = dataGridView.Columns["Amount"].ReadOnly = true;
        //}

        //private void dataGridView_EditingControlShowing(object sender, DataGridViewEditingControlShowingEventArgs e)
        //{
        //    e.Control.KeyPress -= new KeyPressEventHandler(test.decimaldigit_correction);
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
        //    else if (dataGridView.CurrentCell.ColumnIndex == 6)
        //    {
        //        TextBox tb = e.Control as TextBox;
        //        if (tb != null)
        //        {
        //            tb.KeyPress += new KeyPressEventHandler(test.decimaldigit_correction);
        //        }
        //    }
        //    else if (dataGridView.CurrentCell.ColumnIndex == 7)
        //    {
        //        TextBox tb = e.Control as TextBox;
        //        if (tb != null)
        //        {
        //            tb.KeyPress += new KeyPressEventHandler(test.decimaldigit_correction);
        //        }
        //    }
        //}

        //Add Stock Sanitary
        private void rdbtn2_CheckedChanged(object sender, EventArgs e)
        {
            txtqty2.Enabled = txtcode.Enabled = cmbname.Enabled = txtprice2.Enabled = dataGridView2.Enabled = label21.Enabled = label20.Enabled = label22.Enabled = label23.Enabled = true;
            cmbname.Focus();
            txtqty2.Text = "1";
            txtcode.Text = txtprice2.Text = cmbname.Text = "";
            Login.con.Open();
            SqlCommand cmd2 = new SqlCommand("DELETE from purchase_cart_s", Login.con);
            cmd2.ExecuteNonQuery();
            Login.con.Close();
            test.bindingcode("SELECT * From purchase_cart_s", dataGridView2, Login.con);
        }

        private void txtqty2_Leave(object sender, EventArgs e)
        {
            if (txtqty2.Text == "" || Convert.ToDecimal(txtqty2.Text) == 0)
                txtqty2.Text = "1";
        }

        private void txtqty2_MouseClick(object sender, MouseEventArgs e)
        {
            txtqty2.SelectAll();
        }

        private void txtqty2_KeyPress(object sender, KeyPressEventArgs e)
        {
            test.digit_correction(sender, e);
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
                    cmbname.Text = "";
                    txtcode.Focus();
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



        private void cmbquality2_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (txtdcno.Enabled != false)
            {
                cmbmanu.Text = "";
                txtprebalance.Text = "0";
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
                    txtcode.Text = "";
                    cmbname.Focus();
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

        private void txtprice2_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                if (txtprice2.Text == "" || Convert.ToDecimal(txtprice2.Text) == 0)
                {
                    txtprice2.Text = "0";
                    txtprice2.SelectAll();
                }
                entercartdata();
            }
        }

        private void txtprice2_KeyPress(object sender, KeyPressEventArgs e)
        {
            test.decimaldigit_correction(sender, e);
        }

        private void dataGridView2_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Delete)
            {
                if (delid != null)
                {
                    Login.con.Open();
                    SqlCommand cmd = new SqlCommand("DELETE from purchase_cart_s where P_Code='" + delid + "' ", Login.con);
                    cmd.ExecuteNonQuery();
                    test.bindingcode("SELECT * from purchase_cart_s", dataGridView2, Login.con);
                    SqlCommand cmd4 = new SqlCommand("SELECT isnull(sum(Amount),0) from purchase_cart_s", Login.con);
                    //SqlCommand cmd5 = new SqlCommand("SELECT isnull(sum(Amount),0) from purchase_cart", Login.con);
                    txttotal.Text = Convert.ToString((Decimal)cmd4.ExecuteScalar());
                    txtbalance.Text = (Convert.ToDecimal(txtpaid.Text) - Convert.ToDecimal(txttotal.Text)).ToString();
                    Login.con.Close();
                    delid = null;
                    if (dataGridView2.RowCount == 0)
                    //if (dataGridView.RowCount == 0)
                    {
                        data = false;
                        btnsubmit.Enabled = false;
                    }
                }
            }
        }

        private void dataGridView2_Enter(object sender, EventArgs e)
        {
            dataGridView2.Columns["P_Code"].ReadOnly = dataGridView2.Columns["P_Name"].ReadOnly = dataGridView2.Columns["Amount"].ReadOnly = true;
        }

        private void dataGridView2_EditingControlShowing(object sender, DataGridViewEditingControlShowingEventArgs e)
        {
            e.Control.KeyPress -= new KeyPressEventHandler(test.decimaldigit_correction);
            e.Control.KeyPress -= new KeyPressEventHandler(test.digit_correction);
            if (dataGridView2.CurrentCell.ColumnIndex == 2)
            {
                TextBox tb = e.Control as TextBox;
                if (tb != null)
                {
                    tb.KeyPress += new KeyPressEventHandler(test.digit_correction);
                }
            }
            else if (dataGridView2.CurrentCell.ColumnIndex == 3)
            {
                TextBox tb = e.Control as TextBox;
                if (tb != null)
                {
                    tb.KeyPress += new KeyPressEventHandler(test.decimaldigit_correction);
                }
            }
            else if (dataGridView2.CurrentCell.ColumnIndex == 4)
            {
                TextBox tb = e.Control as TextBox;
                if (tb != null)
                {
                    tb.KeyPress += new KeyPressEventHandler(test.decimaldigit_correction);
                }
            }
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
                if (row.Cells[2].Value.ToString() == "0")
                    p_qty = 1;
                else if (row.Cells[2].Value.ToString() != "")
                    p_qty = Convert.ToDecimal(row.Cells[2].Value.ToString());
                else
                    p_qty = 1;
                if (row.Cells[3].Value.ToString() != "")
                    p_price = Convert.ToDecimal(row.Cells[3].Value.ToString());
                else
                    p_price = 0;
                p_code = row.Cells[0].Value.ToString();
            }
            Login.con.Open();
            SqlCommand cmd2 = new SqlCommand("UPDATE purchase_cart_s set Amount='" + (((p_qty) * (p_price)) - (p_disc)) + "' ,Price='" + p_price + "', Quantity='" + p_qty + "',Discount ='" + p_disc + "' where P_Code='" + p_code + "'", Login.con);
            cmd2.ExecuteNonQuery();
            test.bindingcode("select * from purchase_cart_s", dataGridView2, Login.con);
            SqlCommand cmd4 = new SqlCommand("SELECT isnull(sum(Amount),0) from purchase_cart_s", Login.con);
            //SqlCommand cmd5 = new SqlCommand("SELECT isnull(sum(Amount),0) from purchase_cart", Login.con);
            txttotal.Text = Convert.ToString((Decimal)cmd4.ExecuteScalar());
            txtbalance.Text = (Convert.ToDecimal(txtpaid.Text) - Convert.ToDecimal(txttotal.Text)).ToString();
            Login.con.Close();
            if (txtdcno.Enabled != false)
                dataGridView2.Columns["P_Code"].ReadOnly = dataGridView2.Columns["P_Name"].ReadOnly = dataGridView2.Columns["Amount"].ReadOnly = true;
            else
                dataGridView2.Columns["P_Code"].ReadOnly = dataGridView2.Columns["P_Name"].ReadOnly = dataGridView2.Columns["Amount"].ReadOnly = dataGridView2.Columns["Quantity"].ReadOnly = btnsubmit.Enabled = true;
        }

        private void dataGridView2_CellClick(object sender, DataGridViewCellEventArgs e)
        {

            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = this.dataGridView2.Rows[e.RowIndex];
                delid = row.Cells[0].Value.ToString();
            }

        }

        private void cmbmanu_Enter(object sender, EventArgs e)
        {
            if (cmbquality2.Text == "Customer" || cmbquality2.Text == "Stakeholder")
            {
                if (cmbquality2.Text == "Customer")
                    type = "FIXED";
                else if (cmbquality2.Text == "Stakeholder")
                    type = "STAKEHOLDER";
                test.cmbox_drop("select c_name from customer where type='" + type + "' order by c_name", cmbmanu, Login.con);
            }
            else if (cmbquality2.Text == "Manufacturer")
            {
                test.cmbox_drop("select name from manufacturer order by name", cmbmanu, Login.con);
            }
        }

        private void cmbmanu_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                if (cmbmanu.Text != "")
                {
                    cmbmanu.Text = cmbmanu.Text.ToUpper();
                    cmbmanu.SelectAll();
                    sdebit = scredit = srdebit = prdebit = prcredit = srcredit = tdebit = pcredit = pdebit = tcredit = 0;
                    Login.con.Open();
                    if (cmbquality2.Text == "Customer" || cmbquality2.Text == "Stakeholder")
                    {
                        SqlCommand cmd = new SqlCommand("select (ss.total-ss.discount) Debit ,ss.receive Credit from customer c, cus_transaction cus,sale_stock ss where c.c_id=ss.c_id AND c.type='" + type + "' AND cus.cus_id=ss.cus_id AND c.c_name='" + cmbmanu.Text.ToUpper() + "'AND cus.datetime <= '" + DateTime.Now + "'", Login.con);
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
                        SqlCommand cmd7 = new SqlCommand("select ps.paid Debit,ps.total Credit from customer c, cus_transaction cus,purchase_stock ps where c.c_id=ps.c_id AND cus.cus_id=ps.cus_id AND c.c_name = '" + cmbmanu.Text.ToUpper() + "' AND cus.datetime <= '" + DateTime.Now + "'", Login.con);
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
                        SqlCommand cmd2 = new SqlCommand("select sr.Paid Debit, (sr.total-sr.deduct) Credit from customer c, cus_transaction cus, sale_return sr where c.c_id = sr.c_id AND c.type = '" + type + "' AND cus.cus_id = sr.cus_id AND c.c_name = '" + cmbmanu.Text.ToUpper() + "'AND cus.datetime  <= '" + DateTime.Now + "'", Login.con);
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
                        SqlCommand cmd22 = new SqlCommand("select pr.total Debit, pr.receive Credit from customer c, cus_transaction cus, purchase_return pr where c.c_id = pr.c_id AND c.type = '" + type + "' AND cus.cus_id = pr.cus_id AND c.c_name = '" + cmbmanu.Text.ToUpper() + "'AND cus.datetime  <= '" + DateTime.Now + "'", Login.con);
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
                        SqlCommand cmd3 = new SqlCommand("select ct.amount Debit from customer c, cus_transaction cus, customer_transaction ct where c.c_id = ct.c_id AND c.type = '" + type + "' AND cus.cus_id = ct.cus_id AND c.c_name = '" + cmbmanu.Text.ToUpper() + "'AND cus.datetime <= '" + DateTime.Now + "' AND ct.status = 'PAID'", Login.con);
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
                        SqlCommand cmd4 = new SqlCommand("select ct.amount Credit from customer c, cus_transaction cus, customer_transaction ct where c.c_id = ct.c_id AND c.type = '" + type + "' AND cus.cus_id = ct.cus_id AND c.c_name = '" + cmbmanu.Text.ToUpper() + "'AND cus.datetime <= '" + DateTime.Now + "' AND ct.status = 'RECEIVED'", Login.con);
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
                    }
                    else if (cmbquality2.Text == "Manufacturer")
                    {
                        SqlCommand cmd = new SqlCommand("select ps.paid Debit,ps.total Credit from manufacturer m, man_transaction man,purchase_stock ps where m.m_id=ps.m_id AND man.man_id=ps.man_id AND m.name = '" + cmbmanu.Text.ToUpper() + "' AND man.datetime <= '" + DateTime.Now + "'", Login.con);
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
                        SqlCommand cmd7 = new SqlCommand("select (ss.total-ss.discount) Debit ,ss.receive Credit from manufacturer m, man_transaction man,sale_stock ss where m.m_id=ss.m_id AND man.man_id=ss.man_id AND m.name='" + cmbmanu.Text.ToUpper() + "'AND man.datetime <= '" + DateTime.Now + "'", Login.con);
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
                        SqlCommand cmd2 = new SqlCommand("select sr.Paid Debit, (sr.total-sr.deduct) Credit from manufacturer m, man_transaction man, sale_return sr where m.m_id = sr.m_id AND man.man_id = sr.man_id AND m.name = '" + cmbmanu.Text.ToUpper() + "'AND man.datetime  <= '" + DateTime.Now + "'", Login.con);
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
                        SqlCommand cmd22 = new SqlCommand("select pr.total Debit, pr.receive Credit from manufacturer m, man_transaction man, purchase_return pr where m.m_id = pr.m_id AND man.man_id = pr.man_id AND m.name = '" + cmbmanu.Text.ToUpper() + "'AND man.datetime  <= '" + DateTime.Now + "'", Login.con);
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
                        SqlCommand cmd3 = new SqlCommand("select mt.amount Debit from Manufacturer m, man_transaction man, manu_transaction mt where m.m_id = mt.m_id AND man.man_id = mt.man_id AND m.name = '" + cmbmanu.Text.ToUpper() + "'AND man.datetime <= '" + DateTime.Now + "' AND mt.status = 'PAID'", Login.con);
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
                        SqlCommand cmd4 = new SqlCommand("select mt.amount Credit from Manufacturer m, man_transaction man, manu_transaction mt where m.m_id = mt.m_id AND man.man_id = mt.man_id AND m.name = '" + cmbmanu.Text.ToUpper() + "'AND man.datetime <= '" + DateTime.Now + "' AND mt.status = 'RECEIVED'", Login.con);
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
                    }
                    Login.con.Close();
                    txtprebalance.Text = Convert.ToString(((sdebit + prdebit + tdebit + pdebit + srdebit) - (prcredit + scredit + tcredit + pcredit + srcredit)));
                }
            }
        }

        private void cmbmanu_Leave(object sender, EventArgs e)
        {
            if (cmbmanu.Text == "")
            {
                txtprebalance.Text = "0";
                cmb = false;
                btnsubmit.Enabled = false;
            }
            else
            {
                cmbmanu.Text = cmbmanu.Text.ToUpper();
                cmbmanu.SelectAll();
                Login.con.Open();
                if (cmbquality2.Text == "Customer" || cmbquality2.Text == "Stakeholder")
                {
                    SqlCommand cmd8 = new SqlCommand("SELECT c_id from customer where c_name='" + cmbmanu.Text.ToUpper() + "' AND type='" + type + "'", Login.con);
                    SqlDataReader dr8 = cmd8.ExecuteReader();
                    if (!dr8.HasRows)
                    {
                        dr8.Close();
                        MessageBox.Show("Record Not Found", "AR Tranders", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                        cmbmanu.Focus();
                        txtprebalance.Text = "0";
                        cmb = false;
                        btnsubmit.Enabled = false;
                    }
                    else
                    {
                        cmb = true;
                        if (data && cmb && dc)
                        {
                            btnsubmit.Enabled = true;
                        }
                    }
                }
                else if (cmbquality2.Text == "Manufacturer")
                {
                    SqlCommand cmd8 = new SqlCommand("SELECT m_id from manufacturer where name='" + cmbmanu.Text.ToUpper() + "'", Login.con);
                    SqlDataReader dr8 = cmd8.ExecuteReader();
                    if (!dr8.HasRows)
                    {
                        dr8.Close();
                        MessageBox.Show("Record Not Found", "AR Tranders", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                        cmbmanu.Focus();
                        txtprebalance.Text = "0";
                        cmb = false;
                        btnsubmit.Enabled = false;
                    }
                    else
                    {
                        cmb = true;
                        if (data && cmb && dc)
                        {
                            btnsubmit.Enabled = true;
                        }
                    }
                }
                Login.con.Close();
            }
        }

        private void txtpaid_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                if (txtdcno.Enabled != false)
                    txtdcno.Focus();
                else
                    btnsubmit.Focus();
            }
        }

        private void txtpaid_Leave(object sender, EventArgs e)
        {
            if (txtpaid.Text == "")
                txtpaid.Text = "0";
            else if (Convert.ToDecimal(txtpaid.Text) == 0)
                txtpaid.Text = "0";
            txtbalance.Text = (Convert.ToDecimal(txtpaid.Text) - Convert.ToDecimal(txttotal.Text)).ToString();
        }

        private void txtpaid_KeyPress(object sender, KeyPressEventArgs e)
        {
            test.decimaldigit_correction(sender, e);
        }

        private void txtpaid_MouseDown(object sender, MouseEventArgs e)
        {
            txtpaid.SelectAll();
        }

        private void dateTimePicker_Leave(object sender, EventArgs e)
        {
            if (DateTime.Now < dateTimePicker.Value)
                dateTimePicker.Value = DateTime.Now;
        }

        private void txtdcno_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                if (txtdcno.Text != "")
                {
                    if (Convert.ToDecimal(txtdcno.Text) != 0)
                    {
                        Login.con.Open();
                        SqlCommand cmd = new SqlCommand("SELECT ps_id from purchase_stock where ps_id='" + Convert.ToDecimal(txtdcno.Text) + "'", Login.con);
                        SqlDataReader dr = cmd.ExecuteReader();
                        if (dr.HasRows)
                        {
                            dr.Close();
                            MessageBox.Show("DC No Already Exist", "AR Tranders", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                            txtdcno.Clear();
                            txtdcno.Focus();
                            dc = false;
                            btnsubmit.Enabled = false;
                        }
                        else
                        {
                            dr.Close();
                            dc = true;
                            if (data && cmb && dc)
                            {
                                btnsubmit.Enabled = true;
                            }
                            btnsubmit.Focus();
                        }
                        Login.con.Close();
                    }
                }
            }
        }

        private void txtdcno_KeyPress(object sender, KeyPressEventArgs e)
        {
            test.digit_correction(sender, e);
        }

        private void btnsubmit_Click(object sender, EventArgs e)
        {
            Login.con.Open();
            if (cmbquality2.Text == "Customer" || cmbquality2.Text == "Stakeholder")
            {
                SqlCommand cmd8 = new SqlCommand("SELECT c_id from customer where c_name='" + cmbmanu.Text.ToUpper() + "' AND type='" + type + "'", Login.con);
                SqlDataReader dr8 = cmd8.ExecuteReader();
                if (dr8.HasRows)
                {
                    while (dr8.Read())
                    {
                        m_id = Convert.ToDecimal(dr8[0]);
                    }
                }
                dr8.Close();
            }
            else if (cmbquality2.Text == "Manufacturer")
            {
                SqlCommand cmd8 = new SqlCommand("SELECT m_id from manufacturer where name='" + cmbmanu.Text.ToUpper() + "'", Login.con);
                SqlDataReader dr8 = cmd8.ExecuteReader();
                if (dr8.HasRows)
                {
                    while (dr8.Read())
                    {
                        m_id = Convert.ToDecimal(dr8[0]);
                    }
                }
                dr8.Close();
            }
            int flag = 0;
            //SqlCommand cmd11 = new SqlCommand("select count(*) from purchase_cart", Login.con);
            //int count = (int)cmd11.ExecuteScalar();
            //SqlCommand cmd12 = new SqlCommand("select Amount from purchase_cart", Login.con);
            //SqlDataReader dr12 = cmd12.ExecuteReader();
            //if (dr12.HasRows)
            //{
            //    while (dr12.Read())
            //    {
            //        if (Convert.ToDecimal(dr12[0]) == 0)
            //            flag++;
            //    }
            //}
            //dr12.Close();
            int flag2 = 0;
            SqlCommand cmd112 = new SqlCommand("select count(*) from purchase_cart_s", Login.con);
            int count2 = (int)cmd112.ExecuteScalar();
            SqlCommand cmd122 = new SqlCommand("select Amount from purchase_cart_s", Login.con);
            SqlDataReader dr122 = cmd122.ExecuteReader();
            if (dr122.HasRows)
            {
                while (dr122.Read())
                {
                    if (Convert.ToDecimal(dr122[0]) == 0)
                        flag2++;
                }
            }
            dr122.Close();
            if (flag2 == count2)
            {
                if (txtdcno.Enabled != false)
                {
                    if (cmbquality2.Text == "Customer" || cmbquality2.Text == "Stakeholder")
                    {
                        SqlCommand cmd4 = new SqlCommand("INSERT into purchase_stock(ps_id,datetime,c_id,total,paid,pending) values('" + txtdcno.Text + "','" + dateTimePicker.Value + "','" + m_id + "','0','0','YES')", Login.con);
                        cmd4.ExecuteNonQuery();
                    }
                    else if (cmbquality2.Text == "Manufacturer")
                    {
                        SqlCommand cmd4 = new SqlCommand("INSERT into purchase_stock(ps_id,datetime,m_id,total,paid,pending) values('" + txtdcno.Text + "','" + dateTimePicker.Value + "','" + m_id + "','0','0','YES')", Login.con);
                        cmd4.ExecuteNonQuery();
                    }
                    //if (ckbx.Checked == true)
                    //{
                    //    if (dataGridView.RowCount != 0)
                    //    {
                    //while (true)
                    //{
                    //    SqlCommand cmd8 = new SqlCommand("SELECT Boxes,Tiles,P_Code,Quality,Size,Tone from purchase_cart", Login.con);
                    //    SqlDataReader dr8 = cmd8.ExecuteReader();
                    //    if (dr8.HasRows)
                    //    {
                    //        if (dr8.Read())
                    //        {
                    //            p_qty = Convert.ToDecimal(dr8[0]);
                    //            p_tiles = Convert.ToDecimal(dr8[1]);
                    //            p_code = Convert.ToString(dr8[2]);
                    //            p_quality = Convert.ToString(dr8[3]);
                    //            p_size = Convert.ToString(dr8[4]);
                    //            tone = Convert.ToString(dr8[5]);
                    //            dr8.Close();
                    //            SqlCommand cmd9 = new SqlCommand("INSERT into purchase_stock_detail values('" + txtdcno.Text + "','" + p_code + "','" + p_qty + "','0', '" + p_quality + "', '" + tone + "', '0', '" + p_size + "', '" + p_tiles + "')", Login.con);
                    //            cmd9.ExecuteNonQuery();
                    //            SqlCommand cmd10 = new SqlCommand("DELETE purchase_cart where P_Code='" + p_code + "' AND Quality='" + p_quality + "' AND Tone='" + tone + "' AND Size='" + p_size + "' ", Login.con);
                    //            cmd10.ExecuteNonQuery();
                    //        }
                    //    }
                    //    else
                    //    {
                    //        dr8.Close();
                    //        break;
                    //    }
                    //}
                    //    }
                    //}
                    //if (ckbx2.Checked == true)
                    //{
                    if (dataGridView2.RowCount != 0)
                    {
                        while (true)
                        {
                            SqlCommand cmd8 = new SqlCommand("SELECT P_Code,Quantity from purchase_cart_s", Login.con);
                            SqlDataReader dr8 = cmd8.ExecuteReader();
                            if (dr8.HasRows)
                            {
                                if (dr8.Read())
                                {
                                    p_qty = Convert.ToDecimal(dr8[1]);
                                    p_code = Convert.ToString(dr8[0]);
                                    dr8.Close();
                                    SqlCommand cmd9 = new SqlCommand("INSERT into purchase_stock_detail2 values('" + txtdcno.Text + "','" + p_code + "','" + p_qty + "','0', '0')", Login.con);
                                    cmd9.ExecuteNonQuery();
                                    SqlCommand cmd10 = new SqlCommand("DELETE purchase_cart_s where P_Code='" + p_code + "'", Login.con);
                                    cmd10.ExecuteNonQuery();
                                }
                            }
                            else
                            {
                                dr8.Close();
                                break;
                            }
                        }
                    }
                    //}
                    MessageBox.Show("Stock Inserted Successfully", "AR Tranders", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    Login.con.Close();
                    cart_clear();
                }
                else
                {
                    //MessageBox.Show("Please Set the Price in Cart First then Press 'Submit'", "AR Tranders", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    //Login.con.Close();
                    //if (cmbquality2.Text == "Customer" || cmbquality2.Text == "Stakeholder")
                    //{
                    //    SqlCommand cmd4 = new SqlCommand("INSERT into purchase_stock(ps_id,datetime,c_id,total,paid,pending) values('" + txtdcno.Text + "','" + dateTimePicker.Value + "','" + m_id + "','0','0','YES')", Login.con);
                    //    cmd4.ExecuteNonQuery();
                    //}
                    //else if (cmbquality2.Text == "Manufacturer")
                    //{
                    //    SqlCommand cmd4 = new SqlCommand("INSERT into purchase_stock(ps_id,datetime,m_id,total,paid,pending) values('" + txtdcno.Text + "','" + dateTimePicker.Value + "','" + m_id + "','0','0','YES')", Login.con);
                    //    cmd4.ExecuteNonQuery();
                    //}
                    //if (ckbx.Checked == true)
                    //{
                    //    if (dataGridView.RowCount != 0)
                    //    {
                    //while (true)
                    //{
                    //    SqlCommand cmd8 = new SqlCommand("SELECT Boxes,Tiles,P_Code,Quality,Size,Tone from purchase_cart", Login.con);
                    //    SqlDataReader dr8 = cmd8.ExecuteReader();
                    //    if (dr8.HasRows)
                    //    {
                    //        if (dr8.Read())
                    //        {
                    //            p_qty = Convert.ToDecimal(dr8[0]);
                    //            p_tiles = Convert.ToDecimal(dr8[1]);
                    //            p_code = Convert.ToString(dr8[2]);
                    //            p_quality = Convert.ToString(dr8[3]);
                    //            p_size = Convert.ToString(dr8[4]);
                    //            tone = Convert.ToString(dr8[5]);
                    //            dr8.Close();
                    //            SqlCommand cmd9 = new SqlCommand("INSERT into purchase_stock_detail values('" + txtdcno.Text + "','" + p_code + "','" + p_qty + "','0', '" + p_quality + "', '" + tone + "', '0', '" + p_size + "', '" + p_tiles + "')", Login.con);
                    //            cmd9.ExecuteNonQuery();
                    //            SqlCommand cmd10 = new SqlCommand("DELETE purchase_cart where P_Code='" + p_code + "' AND Quality='" + p_quality + "' AND Tone='" + tone + "' AND Size='" + p_size + "' ", Login.con);
                    //            cmd10.ExecuteNonQuery();
                    //        }
                    //    }
                    //    else
                    //    {
                    //        dr8.Close();
                    //        break;
                    //    }
                    //}
                    //    }
                    //}
                    //if (ckbx2.Checked == true)
                    //{
                    if (dataGridView2.RowCount != 0)
                    {
                        SqlCommand cmd100 = new SqlCommand("DELETE purchase_stock_detail2 where ps_id='" + txtdcno.Text + "'", Login.con);
                        cmd100.ExecuteNonQuery();
                        while (true)
                        {
                            SqlCommand cmd8 = new SqlCommand("SELECT P_Code,Quantity from purchase_cart_s", Login.con);
                            SqlDataReader dr8 = cmd8.ExecuteReader();
                            if (dr8.HasRows)
                            {
                                if (dr8.Read())
                                {
                                    p_qty = Convert.ToDecimal(dr8[1]);
                                    p_code = Convert.ToString(dr8[0]);
                                    dr8.Close();
                                    SqlCommand cmd9 = new SqlCommand("INSERT into purchase_stock_detail2 values('" + txtdcno.Text + "','" + p_code + "','" + p_qty + "','0', '0')", Login.con);
                                    cmd9.ExecuteNonQuery();
                                    SqlCommand cmd10 = new SqlCommand("DELETE purchase_cart_s where P_Code='" + p_code + "'", Login.con);
                                    cmd10.ExecuteNonQuery();
                                }
                            }
                            else
                            {
                                dr8.Close();
                                break;
                            }
                        }
                    }
                    //}
                    MessageBox.Show("Stock Inserted Successfully", "AR Tranders", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    Login.con.Close();
                    cart_clear();
                }
            }
            else if (flag2 == 0)
            {
                if (txtpaid.Text == "0")
                {
                    if (txtdcno.Enabled != false)
                    {
                        if (cmbquality2.Text == "Customer" || cmbquality2.Text == "Stakeholder")
                        {
                            SqlCommand cmd2 = new SqlCommand("INSERT into cus_transaction (datetime) values('" + dateTimePicker.Value + "')", Login.con);
                            cmd2.ExecuteNonQuery();
                            SqlCommand cmd3 = new SqlCommand("SELECT cus_id from cus_transaction where datetime='" + dateTimePicker.Value + "'", Login.con);
                            Decimal man_id = (Decimal)cmd3.ExecuteScalar();
                            SqlCommand cmd4 = new SqlCommand("INSERT into purchase_stock(ps_id,datetime,c_id,total,paid,cus_id,pending) values('" + Convert.ToDecimal(txtdcno.Text) + "','" + dateTimePicker.Value + "','" + m_id + "','" + Convert.ToDecimal(txttotal.Text) + "','" + Convert.ToDecimal(txtpaid.Text) + "','" + man_id + "','NO')", Login.con);
                            cmd4.ExecuteNonQuery();
                        }
                        else if (cmbquality2.Text == "Manufacturer")
                        {
                            SqlCommand cmd2 = new SqlCommand("INSERT into man_transaction (datetime) values('" + dateTimePicker.Value + "')", Login.con);
                            cmd2.ExecuteNonQuery();
                            SqlCommand cmd3 = new SqlCommand("SELECT man_id from man_transaction where datetime='" + dateTimePicker.Value + "'", Login.con);
                            Decimal man_id = (Decimal)cmd3.ExecuteScalar();
                            SqlCommand cmd4 = new SqlCommand("INSERT into purchase_stock(ps_id,datetime,m_id,total,paid,man_id,pending) values('" + Convert.ToDecimal(txtdcno.Text) + "','" + dateTimePicker.Value + "','" + m_id + "','" + Convert.ToDecimal(txttotal.Text) + "','" + Convert.ToDecimal(txtpaid.Text) + "','" + man_id + "','NO')", Login.con);
                            cmd4.ExecuteNonQuery();
                        }
                        //if (ckbx.Checked == true)
                        //{
                        //    if (dataGridView.RowCount != 0)
                        //    {
                        //while (true)
                        //{
                        //    SqlCommand cmd8 = new SqlCommand("SELECT Boxes,Tiles,P_Code,Price,Quality,Size,Tone,Discount from purchase_cart", Login.con);
                        //    SqlDataReader dr8 = cmd8.ExecuteReader();
                        //    if (dr8.HasRows)
                        //    {
                        //        if (dr8.Read())
                        //        {
                        //            p_qty = Convert.ToDecimal(dr8[0]);
                        //            p_tiles = Convert.ToDecimal(dr8[1]);
                        //            p_code = Convert.ToString(dr8[2]);
                        //            p_price = Convert.ToDecimal(dr8[3]);
                        //            p_quality = Convert.ToString(dr8[4]);
                        //            p_size = Convert.ToString(dr8[5]);
                        //            tone = Convert.ToString(dr8[6]);
                        //            p_disc = Convert.ToDecimal(dr8[7]);
                        //            dr8.Close();
                        //            SqlCommand cmd9 = new SqlCommand("INSERT into purchase_stock_detail values('" + txtdcno.Text + "','" + p_code + "','" + p_qty + "','" + p_price + "', '" + p_quality + "', '" + tone + "', '" + p_disc + "', '" + p_size + "', '" + p_tiles + "')", Login.con);
                        //            cmd9.ExecuteNonQuery();
                        //            SqlCommand cmd10 = new SqlCommand("DELETE purchase_cart where P_Code='" + p_code + "' AND Quality='" + p_quality + "' AND Tone='" + tone + "' AND Size='" + p_size + "' ", Login.con);
                        //            cmd10.ExecuteNonQuery();
                        //        }
                        //    }
                        //    else
                        //    {
                        //        dr8.Close();
                        //        break;
                        //    }
                        //}
                        //    }
                        //}
                        //if (ckbx2.Checked == true)
                        //{
                        if (dataGridView2.RowCount != 0)
                        {
                            while (true)
                            {
                                SqlCommand cmd8 = new SqlCommand("SELECT P_Code,Quantity,Price,Discount from purchase_cart_s", Login.con);
                                SqlDataReader dr8 = cmd8.ExecuteReader();
                                if (dr8.HasRows)
                                {
                                    if (dr8.Read())
                                    {
                                        p_qty = Convert.ToDecimal(dr8[1]);
                                        p_code = Convert.ToString(dr8[0]);
                                        p_price = Convert.ToDecimal(dr8[2]);
                                        p_disc = Convert.ToDecimal(dr8[3]);
                                        dr8.Close();
                                        SqlCommand cmd9 = new SqlCommand("INSERT into purchase_stock_detail2 values('" + txtdcno.Text + "','" + p_code + "','" + p_qty + "','" + p_price + "','" + p_disc + "')", Login.con);
                                        cmd9.ExecuteNonQuery();
                                        SqlCommand cmd10 = new SqlCommand("DELETE purchase_cart_s where P_Code='" + p_code + "'", Login.con);
                                        cmd10.ExecuteNonQuery();
                                    }
                                }
                                else
                                {
                                    dr8.Close();
                                    break;
                                }
                            }
                        }
                        //}
                        MessageBox.Show("Stock Inserted Successfully", "AR Tranders", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        Login.con.Close();
                        cart_clear();
                    }
                    else
                    {
                        if (cmbquality2.Text == "Customer" || cmbquality2.Text == "Stakeholder")
                        {
                            SqlCommand cmd2 = new SqlCommand("INSERT into cus_transaction (datetime) values('" + dateTimePicker.Value + "')", Login.con);
                            cmd2.ExecuteNonQuery();
                            SqlCommand cmd3 = new SqlCommand("SELECT cus_id from cus_transaction where datetime='" + dateTimePicker.Value + "'", Login.con);
                            Decimal man_id = (Decimal)cmd3.ExecuteScalar();
                            SqlCommand cmd4 = new SqlCommand("Update purchase_stock set total='" + Convert.ToDecimal(txttotal.Text) + "',paid='" + Convert.ToDecimal(txtpaid.Text) + "', cus_id='" + man_id + "',pending='NO' where ps_id='" + txtdcno.Text + "'", Login.con);
                            cmd4.ExecuteNonQuery();
                        }
                        else if (cmbquality2.Text == "Manufacturer")
                        {
                            SqlCommand cmd2 = new SqlCommand("INSERT into man_transaction (datetime) values('" + dateTimePicker.Value + "')", Login.con);
                            cmd2.ExecuteNonQuery();
                            SqlCommand cmd3 = new SqlCommand("SELECT man_id from man_transaction where datetime='" + dateTimePicker.Value + "'", Login.con);
                            Decimal man_id = (Decimal)cmd3.ExecuteScalar();
                            SqlCommand cmd4 = new SqlCommand("Update purchase_stock set total='" + Convert.ToDecimal(txttotal.Text) + "',paid='" + Convert.ToDecimal(txtpaid.Text) + "', man_id='" + man_id + "',pending='NO' where ps_id='" + txtdcno.Text + "'", Login.con);
                            cmd4.ExecuteNonQuery();
                        }
                        //if (ckbx.Checked == true)
                        //{
                        //    if (dataGridView.RowCount != 0)
                        //    {
                        //while (true)
                        //{
                        //    SqlCommand cmd8 = new SqlCommand("SELECT P_Code,Price,Quality,Size,Tone,Discount from purchase_cart", Login.con);
                        //    SqlDataReader dr8 = cmd8.ExecuteReader();
                        //    if (dr8.HasRows)
                        //    {
                        //        if (dr8.Read())
                        //        {
                        //            p_code = Convert.ToString(dr8[0]);
                        //            p_price = Convert.ToDecimal(dr8[1]);
                        //            p_quality = Convert.ToString(dr8[2]);
                        //            p_size = Convert.ToString(dr8[3]);
                        //            tone = Convert.ToString(dr8[4]);
                        //            p_disc = Convert.ToDecimal(dr8[5]);
                        //            dr8.Close();
                        //            SqlCommand cmd9 = new SqlCommand("Update purchase_stock_detail set pprice='" + p_price + "', discount='" + p_disc + "'where ps_id='" + txtdcno.Text + "'AND p_id='" + p_code + "' AND quality='" + p_quality + "' AND tone='" + tone + "' AND size='" + p_size + "'", Login.con);
                        //            cmd9.ExecuteNonQuery();
                        //            SqlCommand cmd10 = new SqlCommand("DELETE purchase_cart where P_Code='" + p_code + "' AND Quality='" + p_quality + "' AND Tone='" + tone + "' AND Size='" + p_size + "' ", Login.con);
                        //            cmd10.ExecuteNonQuery();
                        //        }
                        //    }
                        //    else
                        //    {
                        //        dr8.Close();
                        //        break;
                        //    }
                        //}
                        //    }
                        //}
                        //if (ckbx2.Checked == true)
                        //{
                        if (dataGridView2.RowCount != 0)
                        {
                            SqlCommand cmd100 = new SqlCommand("DELETE purchase_stock_detail2 where ps_id='" + txtdcno.Text + "'", Login.con);
                            cmd100.ExecuteNonQuery();
                            while (true)
                            {
                                SqlCommand cmd8 = new SqlCommand("SELECT P_Code,Price,Discount,Quantity from purchase_cart_s", Login.con);
                                SqlDataReader dr8 = cmd8.ExecuteReader();
                                if (dr8.HasRows)
                                {
                                    if (dr8.Read())
                                    {
                                        p_code = Convert.ToString(dr8[0]);
                                        p_price = Convert.ToDecimal(dr8[1]);
                                        p_disc = Convert.ToDecimal(dr8[2]);
                                        p_qty = Convert.ToDecimal(dr8[3]);
                                        dr8.Close();
                                        SqlCommand cmd9 = new SqlCommand("INSERT into purchase_stock_detail2 values('" + txtdcno.Text + "','" + p_code + "','" + p_qty + "','" + p_price + "','" + p_disc + "')", Login.con);
                                        cmd9.ExecuteNonQuery();
                                        SqlCommand cmd10 = new SqlCommand("DELETE purchase_cart_s where P_Code='" + p_code + "'", Login.con);
                                        cmd10.ExecuteNonQuery();
                                    }
                                }
                                else
                                {
                                    dr8.Close();
                                    break;
                                }
                            }
                        }
                        //}
                        MessageBox.Show("Stock Updated Successfully", "AR Tranders", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        Login.con.Close();
                        cart_clear();
                    }
                }
                else
                {
                    if (txtdcno.Enabled != false)
                    {
                        if (cmbquality2.Text == "Customer" || cmbquality2.Text == "Stakeholder")
                        {
                            SqlCommand cmd2 = new SqlCommand("insert into cash_transaction (datetime) values('" + dateTimePicker.Value + "')", Login.con);
                            cmd2.ExecuteNonQuery();
                            SqlCommand cmd3 = new SqlCommand("select cat_id from cash_transaction where datetime='" + dateTimePicker.Value + "'", Login.con);
                            Decimal cat_id = (Decimal)cmd3.ExecuteScalar();
                            SqlCommand cmd4 = new SqlCommand("INSERT into cus_transaction (datetime) values('" + dateTimePicker.Value + "')", Login.con);
                            cmd4.ExecuteNonQuery();
                            SqlCommand cmd5 = new SqlCommand("SELECT cus_id from cus_transaction where datetime='" + dateTimePicker.Value + "'", Login.con);
                            Decimal man_id = (Decimal)cmd5.ExecuteScalar();
                            SqlCommand cmd6 = new SqlCommand("INSERT into purchase_stock(ps_id,datetime,c_id,total,paid,cat_id,cus_id,pending) values('" + Convert.ToDecimal(txtdcno.Text) + "','" + dateTimePicker.Value + "','" + m_id + "','" + Convert.ToDecimal(txttotal.Text) + "','" + Convert.ToDecimal(txtpaid.Text) + "','" + cat_id + "' , '" + man_id + "','NO')", Login.con);
                            cmd6.ExecuteNonQuery();
                        }
                        else if (cmbquality2.Text == "Manufacturer")
                        {
                            SqlCommand cmd2 = new SqlCommand("insert into cash_transaction (datetime) values('" + dateTimePicker.Value + "')", Login.con);
                            cmd2.ExecuteNonQuery();
                            SqlCommand cmd3 = new SqlCommand("select cat_id from cash_transaction where datetime='" + dateTimePicker.Value + "'", Login.con);
                            Decimal cat_id = (Decimal)cmd3.ExecuteScalar();
                            SqlCommand cmd4 = new SqlCommand("INSERT into man_transaction (datetime) values('" + dateTimePicker.Value + "')", Login.con);
                            cmd4.ExecuteNonQuery();
                            SqlCommand cmd5 = new SqlCommand("SELECT man_id from man_transaction where datetime='" + dateTimePicker.Value + "'", Login.con);
                            Decimal man_id = (Decimal)cmd5.ExecuteScalar();
                            SqlCommand cmd6 = new SqlCommand("INSERT into purchase_stock(ps_id,datetime,m_id,total,paid,cat_id,man_id,pending) values('" + Convert.ToDecimal(txtdcno.Text) + "','" + dateTimePicker.Value + "','" + m_id + "','" + Convert.ToDecimal(txttotal.Text) + "','" + Convert.ToDecimal(txtpaid.Text) + "','" + cat_id + "' , '" + man_id + "','NO')", Login.con);
                            cmd6.ExecuteNonQuery();
                        }
                        //if (ckbx.Checked == true)
                        //{
                        //    if (dataGridView.RowCount != 0)
                        //    {
                        //while (true)
                        //{
                        //    SqlCommand cmd8 = new SqlCommand("SELECT Boxes,Tiles,P_Code,Price,Quality,Size,Tone,Discount from purchase_cart", Login.con);
                        //    SqlDataReader dr8 = cmd8.ExecuteReader();
                        //    if (dr8.HasRows)
                        //    {
                        //        if (dr8.Read())
                        //        {
                        //            p_qty = Convert.ToDecimal(dr8[0]);
                        //            p_tiles = Convert.ToDecimal(dr8[1]);
                        //            p_code = Convert.ToString(dr8[2]);
                        //            p_price = Convert.ToDecimal(dr8[3]);
                        //            p_quality = Convert.ToString(dr8[4]);
                        //            p_size = Convert.ToString(dr8[5]);
                        //            tone = Convert.ToString(dr8[6]);
                        //            p_disc = Convert.ToDecimal(dr8[7]);
                        //            dr8.Close();
                        //            SqlCommand cmd9 = new SqlCommand("INSERT into purchase_stock_detail values('" + txtdcno.Text + "','" + p_code + "','" + p_qty + "','" + p_price + "', '" + p_quality + "', '" + tone + "', '" + p_disc + "', '" + p_size + "', '" + p_tiles + "')", Login.con);
                        //            cmd9.ExecuteNonQuery();
                        //            SqlCommand cmd10 = new SqlCommand("DELETE purchase_cart where P_Code='" + p_code + "' AND Quality='" + p_quality + "' AND Tone='" + tone + "' AND Size='" + p_size + "' ", Login.con);
                        //            cmd10.ExecuteNonQuery();
                        //        }
                        //    }
                        //    else
                        //    {
                        //        dr8.Close();
                        //        break;
                        //    }
                        //}
                        //    }
                        //}
                        //if (ckbx2.Checked == true)
                        //{
                        if (dataGridView2.RowCount != 0)
                        {
                            while (true)
                            {
                                SqlCommand cmd8 = new SqlCommand("SELECT P_Code,Quantity,Price,Discount from purchase_cart_s", Login.con);
                                SqlDataReader dr8 = cmd8.ExecuteReader();
                                if (dr8.HasRows)
                                {
                                    if (dr8.Read())
                                    {
                                        p_qty = Convert.ToDecimal(dr8[1]);
                                        p_code = Convert.ToString(dr8[0]);
                                        p_price = Convert.ToDecimal(dr8[2]);
                                        p_disc = Convert.ToDecimal(dr8[3]);
                                        dr8.Close();
                                        SqlCommand cmd9 = new SqlCommand("INSERT into purchase_stock_detail2 values('" + txtdcno.Text + "','" + p_code + "','" + p_qty + "','" + p_price + "','" + p_disc + "')", Login.con);
                                        cmd9.ExecuteNonQuery();
                                        SqlCommand cmd10 = new SqlCommand("DELETE purchase_cart_s where P_Code='" + p_code + "'", Login.con);
                                        cmd10.ExecuteNonQuery();
                                    }
                                }
                                else
                                {
                                    dr8.Close();
                                    break;
                                }
                            }
                        }
                        //}
                        MessageBox.Show("Stock Inserted Successfully", "AR Tranders", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        Login.con.Close();
                        cart_clear();
                    }
                    else
                    {
                        if (cmbquality2.Text == "Customer" || cmbquality2.Text == "Stakeholder")
                        {
                            SqlCommand cmd2 = new SqlCommand("insert into cash_transaction (datetime) values('" + dateTimePicker.Value + "')", Login.con);
                            cmd2.ExecuteNonQuery();
                            SqlCommand cmd3 = new SqlCommand("select cat_id from cash_transaction where datetime='" + dateTimePicker.Value + "'", Login.con);
                            Decimal cat_id = (Decimal)cmd3.ExecuteScalar();
                            SqlCommand cmd4 = new SqlCommand("INSERT into cus_transaction (datetime) values('" + dateTimePicker.Value + "')", Login.con);
                            cmd4.ExecuteNonQuery();
                            SqlCommand cmd5 = new SqlCommand("SELECT cus_id from cus_transaction where datetime='" + dateTimePicker.Value + "'", Login.con);
                            Decimal man_id = (Decimal)cmd5.ExecuteScalar();
                            SqlCommand cmd6 = new SqlCommand("Update purchase_stock set total='" + Convert.ToDecimal(txttotal.Text) + "',paid='" + Convert.ToDecimal(txtpaid.Text) + "',cat_id='" + cat_id + "',cus_id='" + man_id + "',pending='NO' where ps_id='" + txtdcno.Text + "'", Login.con);
                            cmd6.ExecuteNonQuery();
                        }
                        else if (cmbquality2.Text == "Manufacturer")
                        {
                            SqlCommand cmd2 = new SqlCommand("insert into cash_transaction (datetime) values('" + dateTimePicker.Value + "')", Login.con);
                            cmd2.ExecuteNonQuery();
                            SqlCommand cmd3 = new SqlCommand("select cat_id from cash_transaction where datetime='" + dateTimePicker.Value + "'", Login.con);
                            Decimal cat_id = (Decimal)cmd3.ExecuteScalar();
                            SqlCommand cmd4 = new SqlCommand("INSERT into man_transaction (datetime) values('" + dateTimePicker.Value + "')", Login.con);
                            cmd4.ExecuteNonQuery();
                            SqlCommand cmd5 = new SqlCommand("SELECT man_id from man_transaction where datetime='" + dateTimePicker.Value + "'", Login.con);
                            Decimal man_id = (Decimal)cmd5.ExecuteScalar();
                            SqlCommand cmd6 = new SqlCommand("Update purchase_stock set total='" + Convert.ToDecimal(txttotal.Text) + "',paid='" + Convert.ToDecimal(txtpaid.Text) + "',cat_id='" + cat_id + "',man_id='" + man_id + "',pending='NO' where ps_id='" + txtdcno.Text + "'", Login.con);
                            cmd6.ExecuteNonQuery();
                        }
                        //if (ckbx.Checked == true)
                        //{
                        //    if (dataGridView.RowCount != 0)
                        //    {
                        //while (true)
                        //{
                        //    SqlCommand cmd8 = new SqlCommand("SELECT P_Code,Price,Quality,Size,Tone,Discount from purchase_cart", Login.con);
                        //    SqlDataReader dr8 = cmd8.ExecuteReader();
                        //    if (dr8.HasRows)
                        //    {
                        //        if (dr8.Read())
                        //        {
                        //            p_code = Convert.ToString(dr8[0]);
                        //            p_price = Convert.ToDecimal(dr8[1]);
                        //            p_quality = Convert.ToString(dr8[2]);
                        //            p_size = Convert.ToString(dr8[3]);
                        //            tone = Convert.ToString(dr8[4]);
                        //            p_disc = Convert.ToDecimal(dr8[5]);
                        //            dr8.Close();
                        //            SqlCommand cmd9 = new SqlCommand("Update purchase_stock_detail set pprice='" + p_price + "', discount='" + p_disc + "' where ps_id='" + txtdcno.Text + "' AND p_id='" + p_code + "' AND quality='" + p_quality + "' AND tone='" + tone + "' AND size='" + p_size + "'", Login.con);
                        //            cmd9.ExecuteNonQuery();
                        //            SqlCommand cmd10 = new SqlCommand("DELETE purchase_cart where P_Code='" + p_code + "' AND Quality='" + p_quality + "' AND Tone='" + tone + "' AND Size='" + p_size + "' ", Login.con);
                        //            cmd10.ExecuteNonQuery();
                        //        }
                        //    }
                        //    else
                        //    {
                        //        dr8.Close();
                        //        break;
                        //    }
                        //}
                        //    }
                        //}
                        //if (ckbx2.Checked == true)
                        //{
                        if (dataGridView2.RowCount != 0)
                        {
                            while (true)
                            {
                                SqlCommand cmd8 = new SqlCommand("SELECT P_Code,Price,Discount from purchase_cart_s", Login.con);
                                SqlDataReader dr8 = cmd8.ExecuteReader();
                                if (dr8.HasRows)
                                {
                                    if (dr8.Read())
                                    {
                                        p_code = Convert.ToString(dr8[0]);
                                        p_price = Convert.ToDecimal(dr8[1]);
                                        p_disc = Convert.ToDecimal(dr8[2]);
                                        dr8.Close();
                                        SqlCommand cmd9 = new SqlCommand("Update purchase_stock_detail2 set pprice='" + p_price + "', discount='" + p_disc + "' where ps_id='" + txtdcno.Text + "' AND p_id='" + p_code + "'", Login.con);
                                        cmd9.ExecuteNonQuery();
                                        SqlCommand cmd10 = new SqlCommand("DELETE purchase_cart_s where P_Code='" + p_code + "'", Login.con);
                                        cmd10.ExecuteNonQuery();
                                    }
                                }
                                else
                                {
                                    dr8.Close();
                                    break;
                                }
                            }
                        }
                        //}
                        MessageBox.Show("Stock Updated Successfully", "AR Tranders", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        Login.con.Close();
                        cart_clear();
                    }
                }
            }
            else
            {
                MessageBox.Show("Please Set the Price in Cart First then Press 'Submit'", "AR Tranders", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Login.con.Close();
            }
        }

        private void btnclear_Click(object sender, EventArgs e)
        {
            cart_clear();
        }

        //Menu Strip
        private void productToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Products f = new Products(this);
            f.Show();
        }

        private void customerInvoiceToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Sales_Invoices f = new Sales_Invoices(this);
            f.Show();
        }

        private void productWiseSalesToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            Product_Wise_Sales f = new Product_Wise_Sales(this);
            f.Show();
        }

        private void salesSummaryToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Sales_Summary f = new Sales_Summary(this);
            f.Show();
        }

        private void manufacturerAndDCToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Purchases_Report f = new Purchases_Report(this);
            f.Show();
        }

        private void manufacturerWisePurchasesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Product_Wise_Purchases f = new Product_Wise_Purchases(this);
            f.Show();
        }

        private void purchasesSummaryToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Purchases_Summary f = new Purchases_Summary(this);
            f.Show();
        }

        private void customerAndInvoiceToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Sales_Return_Report f = new Sales_Return_Report(this);
            f.Show();
        }

        private void productWiseSalesReturnToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Product_Wise_Sales_Return f = new Product_Wise_Sales_Return(this);
            f.Show();
        }

        private void salesReturnSummaryToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Sales_Return_Summary f = new Sales_Return_Summary(this);
            f.Show();
        }

        private void toolStripMenuItem1_Click(object sender, EventArgs e)
        {
            Purchases_Return_Report f = new Purchases_Return_Report(this);
            f.Show();
        }

        private void toolStripMenuItem2_Click(object sender, EventArgs e)
        {
            Product_Wise_Purchases_Return f = new Product_Wise_Purchases_Return(this);
            f.Show();
        }

        private void toolStripMenuItem3_Click(object sender, EventArgs e)
        {
            Purchases_Return_Summary f = new Purchases_Return_Summary(this);
            f.Show();
        }

        private void dCNoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DC_Wise_Breakage f = new DC_Wise_Breakage(this);
            f.Show();
        }

        private void productWiseToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Product_Wise_Breakage f = new Product_Wise_Breakage(this);
            f.Show();
        }

        private void toolStripMenuItem6_Click(object sender, EventArgs e)
        {
            Direct_Expenses_Detail f = new Direct_Expenses_Detail(this);
            f.Show();
        }

        private void expensesDetailToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Expenses_Report f = new Expenses_Report(this);
            f.Show();
        }

        private void profitLossToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Profit_Report f = new Profit_Report(this);
            f.Show();
        }

        private void toolStripMenuItem9_Click(object sender, EventArgs e)
        {
            Available_Stock f = new Available_Stock(this);
            f.Show();
        }

        private void avilableDetailToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Available_Detail f = new Available_Detail(this);
            f.Show();
        }

        private void toolStripMenuItem5_Click(object sender, EventArgs e)
        {
            Customer f = new Customer(this);
            f.Show();
        }

        private void toolStripMenuItem7_Click(object sender, EventArgs e)
        {
            Manufacturer f = new Manufacturer(this);
            f.Show();
        }

        private void othersToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Expenses f = new Expenses(this);
            f.Show();
        }

        private void manufacturerTransactionToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Customer_Transactions f = new Customer_Transactions(this);
            f.Show();
        }

        private void payableToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Payable f = new Payable(this);
            f.Show();
        }

        private void reciveableToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Receivable f = new Receivable(this);
            f.Show();
        }

        private void ledgerToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            Customer_Report f = new Customer_Report(this);
            f.Show();
        }

        private void dayBookToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Cash_Report f = new Cash_Report(this);
            f.Show();
        }
    }
}
