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
    public partial class Cart : Form
    {
        Form opener;
        Boolean data, cmb, sinvoice = false;
        Int32 db_tiles = 0;
        Decimal availableQty, p_tiles, user_qty, p_price, prcredit, prdebit, customer_id, p_disc, limit, p_qty, meters_in_box, purchase_qty, sale_qty, sale_return_qty, purchase_return_qty, breakage_qty, sdebit, scredit, srdebit, srcredit, tdebit, tcredit, pdebit, pcredit;
        String p_code, delid, p_quality, tone, p_size, p_name, type;
        UF.Class1 test = new UF.Class1();

        public Cart(Form form)
        {
            InitializeComponent();
            opener = form;
        }

        private void Cart_Load(object sender, EventArgs e)
        {
            opener.Hide();
            //test.cmbox_drop("select DISTINCT p_id from purchase_stock_detail order by p_id", cmbcode, Login.con);
            test.cmbox_drop("select p.p_name from purchase_stock_detail2 ps, product2 p where ps.p_id=p.p_id order by p.p_name", cmbname, Login.con);
            test.cmbox_drop("select CAST(p.p_id as varchar(50)) from purchase_stock_detail2 ps, product2 p where ps.p_id=p.p_id order by p.p_id", txtcode, Login.con);
            test.cmbox_drop("select c_name from customer order by c_name", cmbcustomername, Login.con);
            test.cmbox_drop("SELECT CAST(ss_id as varchar(50)) from sale_stock order by ss_id", cmbdc, Login.con);
            reset_cart();
        }

        private void Cart_FormClosing(object sender, FormClosingEventArgs e)
        {
            opener.Show();
        }

        private void reset_cart()
        {
            cmbcustomername.Text = txtsinvoiceno.Text = cmbdc.Text = "";
            cmbquality2.SelectedIndex = 1;
            txttotal.Text = txtgtotal.Text = txtamount.Text = txtdiscount.Text = txtbalance.Text = txtprebalance.Text = "0";
            cmbquality2.Enabled = cmbcustomername.Enabled = dateTimePicker.Enabled = txtsinvoiceno.Enabled = true;
            dateTimePicker.Value = DateTime.Now;
            cmbdc_Enter(this, null);
            btninvoice.Enabled = data = cmb = sinvoice = false;
            ckbx_CheckedChanged(this, null);
            ckbx2_CheckedChanged(this, null);
        }

        private decimal available_qty(String p_id)
        {
            Decimal purchase_qty, purchase_return_qty, breakage_qty, sale_qty, sale_return_qty;
            SqlCommand cmd = new SqlCommand("SELECT ISNULL(sum(qty),0) from purchase_stock_detail2 where p_id='" + p_id + "'", Login.con);
            purchase_qty = (Decimal)cmd.ExecuteScalar();
            SqlCommand cmd1 = new SqlCommand("SELECT ISNULL(sum(qty),0) from sale_stock_detail2 where p_id='" + p_id + "'", Login.con);
            sale_qty = (Decimal)cmd1.ExecuteScalar();
            SqlCommand cmd2 = new SqlCommand("SELECT ISNULL(sum(qty),0) from sale_return_detail2 where p_id='" + p_id + "'", Login.con);
            sale_return_qty = (Decimal)cmd2.ExecuteScalar();
            SqlCommand cmd3 = new SqlCommand("SELECT ISNULL(sum(qty),0) from purchase_return_detail2 where p_id='" + p_id + "'", Login.con);
            purchase_return_qty = (Decimal)cmd3.ExecuteScalar();
            SqlCommand cmd4 = new SqlCommand("SELECT ISNULL(sum(qty),0) from breakage_detail2 where p_id='" + p_id + "'", Login.con);
            breakage_qty = (Decimal)cmd4.ExecuteScalar();
            return ((purchase_qty + sale_return_qty) - (sale_qty + purchase_return_qty + breakage_qty));
        }

        private decimal available_qty(String p_id, String tone, String quality, String size)
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
            SqlCommand cmd5 = new SqlCommand("SELECT ISNULL(sum(qty),0),ISNULL(sum(tiles),0) from purchase_return_detail where p_id='" + p_id + "' AND quality='" + quality + "'AND Tone='" + tone + "' AND Size='" + size + "'", Login.con);
            SqlDataReader dr5 = cmd5.ExecuteReader();
            if (dr5.HasRows)
            {
                while (dr5.Read())
                {
                    purchase_return_qty = Convert.ToDecimal(dr5[0]) + (Convert.ToDecimal(dr5[1]) / Convert.ToDecimal(db_tiles));
                }
            }
            dr5.Close();
            return ((purchase_qty + sale_return_qty) - (sale_qty + breakage_qty + purchase_return_qty));
        }

        private void entercartdata()
        {
            //if (ckbx.Checked == true)
            //{
            //    if (lbname.Text != "" && txtprice.Text != "" && txtprice.Text != "0" && cmbtone.Text != ""  && cmbsize.Text != "" && cmbquality.Text != "" && (txtqty.Text != "0" || txttiles.Text != "0"))
            //    {
            //        Login.con.Open();
            //        user_qty = limit = 0;
            //        availableQty = available_qty(cmbcode.Text, cmbtone.Text, cmbquality.Text, cmbsize.Text);
            //        SqlCommand cmd6 = new SqlCommand("SELECT isnull(meters,0) from product where p_id = '" + cmbcode.Text + "' AND size= '" + cmbsize.Text + "'", Login.con);
            //        meters_in_box = (Decimal)cmd6.ExecuteScalar();
            //        SqlCommand cmd = new SqlCommand("SELECT * from product where p_id='" + cmbcode.Text + "' AND size= '" + cmbsize.Text + "'", Login.con);
            //        SqlDataReader dr = cmd.ExecuteReader();
            //        if (dr.HasRows)
            //        {
            //            while (dr.Read())
            //            {
            //                user_qty = (Convert.ToDecimal(txtqty.Text) + (Convert.ToDecimal(txttiles.Text) / Convert.ToDecimal(db_tiles)));
            //            }
            //            dr.Close();
            //            SqlCommand cmd2 = new SqlCommand("SELECT Boxes,Tiles from cart where P_Code ='" + cmbcode.Text + "' AND Quality='" + cmbquality.Text + "' AND Tone='" + cmbtone.Text + "'AND Size= '" + cmbsize.Text + "' ", Login.con);
            //            SqlDataReader dr2 = cmd2.ExecuteReader();
            //            if (dr2.HasRows)
            //            {
            //                MessageBox.Show("This Product Already Exist in Sales Cart", "AR Tranders", MessageBoxButtons.OK, MessageBoxIcon.Stop);
            //            }
            //            else
            //            {
            //                dr2.Close();
            //                if (Math.Round(user_qty,2) <= Math.Round(availableQty,2))
            //                {
            //                    SqlCommand cmd3 = new SqlCommand("INSERT into cart(P_Code,Tone,Quality,Size,Boxes,Meter,Price,Amount,Tiles) values('" + cmbcode.Text + "','" + cmbtone.Text + "','" + cmbquality.Text + "','" + cmbsize.Text + "','" + Convert.ToDecimal(txtqty.Text) + "','" + (((Convert.ToDecimal(txttiles.Text) / db_tiles) + Convert.ToDecimal(txtqty.Text)) * meters_in_box) + "', '" + Convert.ToDecimal(txtprice.Text) + "', '" + ((((Convert.ToDecimal(txttiles.Text) / db_tiles) + Convert.ToDecimal(txtqty.Text)) * meters_in_box) * Convert.ToDecimal(txtprice.Text)) + "','" + Convert.ToDecimal(txttiles.Text) + "')", Login.con);
            //                    cmd3.ExecuteNonQuery();
            //                    SqlCommand cmd39 = new SqlCommand("SELECT isnull(sum(Amount),0) from cart_s", Login.con);
            //                    SqlCommand cmd4 = new SqlCommand("SELECT isnull(sum(Amount),0) from cart", Login.con);
            //                    txttotal.Text = Convert.ToString((Decimal)cmd4.ExecuteScalar() + (Decimal)cmd39.ExecuteScalar());
            //                    txtgtotal.Text = Convert.ToString(Convert.ToDecimal(txttotal.Text) - Convert.ToDecimal(txtdiscount.Text));
            //                    txtbalance.Text = Convert.ToString(Convert.ToDecimal(txtgtotal.Text) - Convert.ToDecimal(txtamount.Text));
            //                    SqlCommand cmd5 = new SqlCommand("SELECT * from limit where id='1'", Login.con);
            //                    SqlDataReader dr5 = cmd5.ExecuteReader();
            //                    while (dr5.Read())
            //                    {
            //                        limit = Convert.ToDecimal(dr5[1]);
            //                    }
            //                    dr5.Close();
            //                    if ((availableQty - (user_qty)) < limit)
            //                        MessageBox.Show("Remain Product Quantity is '" + Convert.ToInt32((((availableQty - (user_qty)) * db_tiles) / db_tiles)) + " - " + Convert.ToInt32((((availableQty - (user_qty)) * db_tiles) % db_tiles)) + "'", "AR Tranders", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            //                    test.bindingcode("SELECT P_Code,Tone,Quality,Size,Boxes,Tiles,Meter,Price,Discount,Amount from cart", dataGridView, Login.con);
            //                    txtqty.Text = txttiles.Text = "0";
            //                    lbname.Text = cmbcode.Text = txtprice.Text = txtmeter.Text = "";
            //                    cmbtone.Items.Clear();
            //                    cmbsize.Items.Clear();
            //                    cmbquality.Items.Clear();
            //                    cmbcode.Focus();
            //                    data = true;
            //                    if (data && cmb && sinvoice)
            //                    {
            //                        btninvoice.Enabled = true;
            //                    }
            //                }
            //                else
            //                {
            //                    MessageBox.Show("You Have Not Enough Quantity Against This Product Code", "AR Tranders", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            //                    txtqty.Focus();
            //                }
            //            }
            //        }
            //        else
            //        {
            //            dr.Close();
            //            MessageBox.Show("Record Not Found", "AR Tranders", MessageBoxButtons.OK, MessageBoxIcon.Stop);
            //        }
            //        Login.con.Close();
            //    }
            //}
            //if (ckbx2.Checked == true)
            //{
            if (txtprice2.Text != "")
            {
                Login.con.Open();
                user_qty = limit = 0;
                availableQty = available_qty(txtcode.Text);
                SqlCommand cmd = new SqlCommand("SELECT * from product2 where p_id='" + txtcode.Text + "'", Login.con);
                SqlDataReader dr = cmd.ExecuteReader();
                if (dr.HasRows)
                {
                    while (dr.Read())
                    {
                        user_qty = Convert.ToDecimal(txtqty2.Text);
                    }
                    dr.Close();
                    SqlCommand cmd1 = new SqlCommand("SELECT Quantity,Discount from cart_s where P_Code ='" + txtcode.Text + "'", Login.con);
                    SqlDataReader dr1 = cmd1.ExecuteReader();
                    if (dr1.HasRows)
                    {
                        MessageBox.Show("This Product Already Exist in Sales Cart", "AR Tranders", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                        dr1.Close();
                    }
                    else
                    {
                        dr1.Close();
                        //if (user_qty <= availableQty)
                        if (true)
                        {
                            SqlCommand cmd2 = new SqlCommand("INSERT into cart_s(Quantity,P_Code,P_Name,Price,Amount) values('" + user_qty + "','" + txtcode.Text + "','" + cmbname.Text + "', '" + Convert.ToDecimal(txtprice2.Text) + "', '" + (user_qty * Convert.ToDecimal(txtprice2.Text)) + "')", Login.con);
                            cmd2.ExecuteNonQuery();
                            SqlCommand cmd3 = new SqlCommand("SELECT isnull(sum(Amount),0) from cart_s", Login.con);
                            //SqlCommand cmd4 = new SqlCommand("SELECT isnull(sum(Amount),0) from cart", Login.con);
                            txttotal.Text = Convert.ToString((Decimal)cmd3.ExecuteScalar());
                            txtgtotal.Text = Convert.ToString(Convert.ToDecimal(txttotal.Text) - Convert.ToDecimal(txtdiscount.Text));
                            txtbalance.Text = Convert.ToString(Convert.ToDecimal(txtgtotal.Text) - Convert.ToDecimal(txtamount.Text));
                            SqlCommand cm = new SqlCommand("SELECT * from limit where id='1'", Login.con);
                            SqlDataReader d = cm.ExecuteReader();
                            while (d.Read())
                            {
                                limit = Convert.ToDecimal(d[1]);
                            }
                            d.Close();
                            if ((availableQty - (user_qty)) < limit)
                                MessageBox.Show("Remain Product Quantity is' " + (availableQty - user_qty) + "'", "AR Tranders", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            test.bindingcode("SELECT * from cart_s", dataGridView2, Login.con);
                            txtqty2.Text = "1";
                            txtcode.Text = "";
                            cmbname.Text = "";
                            txtprice2.Clear();
                            cmbname.Focus();
                            data = true;
                            if (txtsinvoiceno.Text != "")
                            {
                                cmb = sinvoice = true;
                            }
                            if (data && cmb && sinvoice)
                            {
                                btninvoice.Enabled = true;
                            }
                        }
                        else
                        {
                            txtqty2.Focus();
                            MessageBox.Show("You Have Not Enough Quantity Against This Product Code", "AR Tranders", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        }
                    }
                }
                else
                {
                    dr.Close();
                    MessageBox.Show("Record Not Found", "AR Tranders", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                Login.con.Close();
            }
            //}
        }

        private void cmbdc_Enter(object sender, EventArgs e)
        {
            test.cmbox_drop("SELECT CAST(ss_id as varchar(50)) from sale_stock order by ss_id", cmbdc, Login.con);
        }

        private void cmbdc_SelectedIndexChanged(object sender, EventArgs e)
        {
            txttotal.Text = txtgtotal.Text = txtbalance.Text = txtamount.Text = txtdiscount.Text = "0";
            Login.con.Open();
            //SqlCommand cmd112 = new SqlCommand("SELECT o_id from order_detail where o_id = '" + cmbdc.Text + "'", Login.con);
            //SqlDataReader dr112 = cmd112.ExecuteReader();
            //if (dr112.HasRows)
            //{
            //    dr112.Close();
            //    Login.con.Close();
            //    //ckbx.Checked = true;
            //    Login.con.Open();
            //    SqlCommand cmd = new SqlCommand("Delete cart", Login.con);
            //    cmd.ExecuteNonQuery();
            //    SqlDataAdapter a = new SqlDataAdapter("SELECT qty,p_id,quality,size,tone,tiles from order_detail where o_id='" + cmbdc.Text + "'", Login.con);
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
            //        SqlCommand cmd2 = new SqlCommand("INSERT into cart values('" + row["p_id"].ToString() + "','" + row["tone"].ToString() + "','" + row["quality"].ToString() + "','" + row["size"].ToString() + "', '" + Convert.ToDecimal(row["qty"]) + "', '" + (((Convert.ToDecimal(row["tiles"]) / db_tiles) + Convert.ToDecimal(row["qty"])) * meters_in_box) + "', '0', '0', '0','" + Convert.ToDecimal(row["tiles"]) + "')", Login.con);
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
            SqlCommand cmd113 = new SqlCommand("SELECT ss_id from sale_stock_detail2 where ss_id = '" + cmbdc.Text + "'", Login.con);
            SqlDataReader dr113 = cmd113.ExecuteReader();
            if (dr113.HasRows)
            {
                dr113.Close();
                Login.con.Close();
                //ckbx2.Checked = true;
                Login.con.Open();
                SqlCommand cmd22 = new SqlCommand("Delete cart_s", Login.con);
                cmd22.ExecuteNonQuery();
                SqlDataAdapter a2 = new SqlDataAdapter("SELECT qty,p_id,price from sale_stock_detail2 where ss_id='" + cmbdc.Text + "'", Login.con);
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
                    SqlCommand cmd2 = new SqlCommand("INSERT into cart_s values('" + row["p_id"].ToString() + "','" + p_name + "', '" + Convert.ToDecimal(row["qty"]) + "','" + Convert.ToDecimal(row["price"]) + "', '0', '" + Convert.ToDecimal(row["qty"]) * Convert.ToDecimal(row["price"]) + "')", Login.con);
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
            SqlCommand cmd5 = new SqlCommand("SELECT m_id,c_id from sale_stock where ss_id='" + cmbdc.Text + "'", Login.con);
            SqlDataReader dr5 = cmd5.ExecuteReader();
            while (dr5.Read())
            {
                if (Convert.ToString(dr5[0]) != DBNull.Value.ToString())
                {
                    customer_id = Convert.ToDecimal(dr5[0]);
                    dr5.Close();
                    SqlCommand cmd6 = new SqlCommand("SELECT name from manufacturer where m_id='" + customer_id + "'", Login.con);
                    SqlDataReader dr6 = cmd6.ExecuteReader();
                    while (dr6.Read())
                    {
                        cmbcustomername.Text = Convert.ToString(dr6[0]);
                        cmbquality2.SelectedItem = "Manufacturer";
                    }
                    dr6.Close();
                }
                else
                {
                    customer_id = Convert.ToDecimal(dr5[1]);
                    dr5.Close();
                    SqlCommand cmd6 = new SqlCommand("SELECT c_name,type from customer where c_id='" + customer_id + "'", Login.con);
                    SqlDataReader dr6 = cmd6.ExecuteReader();
                    while (dr6.Read())
                    {
                        cmbcustomername.Text = Convert.ToString(dr6[0]);
                        if (Convert.ToString(dr6[1]) == "FIXED")
                            cmbquality2.SelectedItem = "Customer";
                        else
                            cmbquality2.SelectedItem = "Stakeholder";
                    }
                    dr6.Close();
                }
                break;
            }
            SqlCommand cmd4 = new SqlCommand("SELECT isnull(sum(Amount),0) from purchase_cart_s", Login.con);
            txttotal.Text = Convert.ToString((Decimal)cmd4.ExecuteScalar());
            txtgtotal.Text = Convert.ToString(Convert.ToDecimal(txttotal.Text) - Convert.ToDecimal(txtdiscount.Text));
            txtbalance.Text = Convert.ToString(Convert.ToDecimal(txtgtotal.Text) - Convert.ToDecimal(txtamount.Text));
            Login.con.Close();
            //test.bindingcode("SELECT P_Code,Tone,Quality,Size,Boxes,Tiles,Meter,Price,Discount,Amount from cart", dataGridView, Login.con);

            test.bindingcode("SELECT  * from cart_s", dataGridView2, Login.con);
            cmbcustomername.Focus();
            cmbcustomername_KeyDown(this, new KeyEventArgs(Keys.Enter));
            //data = true;
            txtsinvoiceno.Text = cmbdc.Text;
            txtsinvoiceno.Enabled = false;
            cmbquality2.Enabled = cmbcustomername.Enabled = false;
            //= txtqty2.Enabled = txtcode.Enabled = cmbname.Enabled = txtprice2.Enabled
            btninvoice.Enabled = dataGridView2.Columns["P_Code"].ReadOnly = dataGridView2.Columns["P_Name"].ReadOnly = dataGridView2.Columns["Amount"].ReadOnly = true;
            //= dataGridView2.Columns["Quantity"].ReadOnly
        }

        private void ckbx_CheckedChanged(object sender, EventArgs e)
        {
            //if (ckbx2.Checked == false)
            //    ckbx.Checked = true;
            //if (ckbx.Checked == true)
            //{
            //    txtmeter.Enabled = txtqty.Enabled = label22.Enabled = txttiles.Enabled = cmbcode.Enabled = cmbsize.Enabled = cmbquality.Enabled = cmbtone.Enabled = txtprice.Enabled = lbname.Enabled = dataGridView.Enabled = label16.Enabled = label2.Enabled = label3.Enabled = label7.Enabled = label15.Enabled = label18.Enabled = label5.Enabled = true;
            //    cmbcode.Focus();
            //}
            //else
            //    txtmeter.Enabled = txtqty.Enabled = label22.Enabled = txttiles.Enabled = cmbcode.Enabled = cmbsize.Enabled = cmbquality.Enabled = cmbtone.Enabled = txtprice.Enabled = lbname.Enabled = dataGridView.Enabled = label16.Enabled = label2.Enabled = label3.Enabled = label7.Enabled = label15.Enabled = label18.Enabled = label5.Enabled = false;
            //txtqty.Text = txttiles.Text = "0";
            //lbname.Text = txtprice.Text = cmbcode.Text = txtmeter.Text = "";
            //cmbtone.Items.Clear();
            //cmbsize.Items.Clear();
            //cmbquality.Items.Clear();
            cmbquality2.SelectedIndex = 0;
            //Login.con.Open();
            //SqlCommand cmd = new SqlCommand("DELETE from cart", Login.con);
            //cmd.ExecuteNonQuery();
            //Login.con.Close();
            //test.bindingcode("SELECT P_Code,Tone,Quality,Size,Boxes,Tiles,Meter,Price,Discount,Amount From cart", dataGridView, Login.con);
        }

        //private void txtmeter_KeyDown(object sender, KeyEventArgs e)
        //{
        //    if (e.KeyCode == Keys.Enter)
        //    {
        //        if (cmbcode.Text != "" && cmbsize.Text != "")
        //        {
        //            Login.con.Open();
        //            SqlCommand cmd = new SqlCommand("SELECT meters from product where p_id='" + cmbcode.Text + "'AND size='" + cmbsize.Text + "' ", Login.con);
        //            Decimal meters = (Decimal)cmd.ExecuteScalar();
        //            Login.con.Close();
        //            txtqty.Text = Convert.ToString(Math.Ceiling(Convert.ToDecimal(txtmeter.Text) / meters));
        //        }
        //        else
        //            MessageBox.Show("Please Enter Code & Size First", "AR Tranders", MessageBoxButtons.OK, MessageBoxIcon.Stop);
        //    }
        //}

        //private void txtmeter_KeyPress(object sender, KeyPressEventArgs e)
        //{
        //    test.decimaldigit_correction(sender, e);
        //}

        //private void txtqty_Leave(object sender, EventArgs e)
        //{
        //    if (txtqty.Text == "" || Convert.ToDecimal(txtqty.Text) == 0)
        //        txtqty.Text = "0";
        //}

        //private void txtqty_KeyPress(object sender, KeyPressEventArgs e)
        //{
        //    test.digit_correction(sender, e);
        //}

        //private void txtqty_MouseDown(object sender, MouseEventArgs e)
        //{
        //    txtqty.SelectAll();
        //}

        //private void set_qty()
        //{
        //    if (cmbcode.Text != "" && cmbsize.Text != "")
        //    {
        //        txtqty.Text = (Convert.ToInt32(txtqty.Text) + (Convert.ToInt32(txttiles.Text) / db_tiles)).ToString();
        //        txttiles.Text = (Convert.ToInt32(txttiles.Text) % db_tiles).ToString();
        //    }
        //}

        //private void txttiles_Leave(object sender, EventArgs e)
        //{
        //    Login.con.Open();
        //    SqlCommand cmd = new SqlCommand("SELECT pieces from product where p_id='" + cmbcode.Text + "' AND size='" + cmbsize.Text + "'", Login.con);
        //    SqlDataReader dr = cmd.ExecuteReader();
        //    if (dr.HasRows)
        //    {
        //        while (dr.Read())
        //        {
        //            db_tiles = Convert.ToInt32(dr[0]);
        //        }
        //    }
        //    dr.Close();
        //    Login.con.Close();
        //    if (cmbcode.Text != "" && cmbsize.Text != "")
        //    {
        //        set_qty();
        //    }
        //    else if (txttiles.Text == "")
        //    {
        //        txttiles.Text = "0";
        //    }
        //}

        //private void txttiles_MouseClick(object sender, MouseEventArgs e)
        //{
        //    txttiles.SelectAll();
        //}

        //private void txttiles_KeyPress(object sender, KeyPressEventArgs e)
        //{
        //    test.digit_correction(sender, e);
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
        //        cmbcode.SelectAll();
        //        Login.con.Open();
        //        SqlCommand cmd = new SqlCommand("SELECT * from product where p_id='" + cmbcode.Text + "'", Login.con);
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

        //private void cmbcode_KeyDown(object sender, KeyEventArgs e)
        //{
        //    if (e.KeyCode == Keys.Enter)
        //    {
        //        if (cmbcode.Text != "")
        //        {
        //            cmbcode.Text = cmbcode.Text.ToUpper();
        //            cmbcode.SelectAll();
        //            cmbquality.Items.Clear();
        //            cmbtone.Items.Clear();
        //            cmbsize.Items.Clear();
        //            Login.con.Open();
        //            SqlCommand cmd = new SqlCommand("SELECT * from product where p_id='" + cmbcode.Text + "'", Login.con);
        //            SqlDataReader dr = cmd.ExecuteReader();
        //            if (!dr.HasRows)
        //            {
        //                dr.Close();
        //                MessageBox.Show("Record Not Found", "AR Tranders", MessageBoxButtons.OK, MessageBoxIcon.Stop);
        //                lbname.Text = "";
        //            }
        //            Login.con.Close();
        //        }
        //    }
        //}

        //private void cmbsize_Enter(object sender, EventArgs e)
        //{
        //    if (cmbcode.Text != "")
        //    {
        //        test.cmbox_drop("select DISTINCT size from purchase_stock_detail where p_id='" + cmbcode.Text + "'", cmbsize, Login.con);
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
        //        cmbquality.Items.Clear();
        //        cmbtone.Items.Clear();
        //    }
        //    Login.con.Close();
        //}

        //private void cmbquality_Enter(object sender, EventArgs e)
        //{
        //    if (cmbcode.Text != "" && cmbsize.Text != "")
        //    {
        //        test.cmbox_drop("select distinct quality from purchase_stock_detail where p_id='" + cmbcode.Text + "' AND size='" + cmbsize.Text + "'", cmbquality, Login.con);
        //    }
        //}

        //private void cmbquality_SelectedIndexChanged(object sender, EventArgs e)
        //{
        //    cmbtone.Items.Clear();
        //}

        //private void cmbtone_Enter(object sender, EventArgs e)
        //{
        //    if (cmbcode.Text != "" && cmbsize.Text != "" && cmbquality.Text != "")
        //    {
        //        test.cmbox_drop("select DISTINCT tone from purchase_stock_detail where p_id='" + cmbcode.Text + "' AND quality='" + cmbquality.Text + "' AND size='" + cmbsize.Text + "'", cmbtone, Login.con);
        //    }
        //}

        //private void txtprice_KeyDown(object sender, KeyEventArgs e)
        //{
        //    if (e.KeyCode == Keys.Enter)
        //    {
        //        if (txtprice.Text == "" || Convert.ToDecimal(txtprice.Text) == 0)
        //            txtprice.Text = "";
        //        entercartdata();
        //    }
        //}

        //private void txtprice_KeyPress(object sender, KeyPressEventArgs e)
        //{
        //    test.decimaldigit_correction(sender, e);
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
        //        SqlCommand cmd121 = new SqlCommand("SELECT isnull(pieces,0) from product where p_id = '" + p_code + "' AND size='"+p_size+"'", Login.con);
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
        //    availableQty = available_qty(p_code, tone, p_quality, p_size);
        //    SqlCommand cmd = new SqlCommand("SELECT isnull(meters,0) from product where p_id = '" + p_code + "' AND size='" + p_size + "' ", Login.con);
        //    meters_in_box = (Decimal)cmd.ExecuteScalar();
        //    if ((p_qty + (p_tiles / Convert.ToDecimal(db_tiles))) <= availableQty)
        //    {
        //        SqlCommand cmd2 = new SqlCommand("UPDATE cart set Discount ='" + p_disc + "', Amount='" + ((((meters_in_box) * (p_qty + (p_tiles / Convert.ToDecimal(db_tiles)))) * (p_price)) - ((((meters_in_box) * (p_qty + (p_tiles / Convert.ToDecimal(db_tiles)))) * (p_price) * (p_disc)) / 100)) + "' ,Price='" + p_price + "',Boxes='" + p_qty + "',Tiles='" + p_tiles + "',Meter= '" + (meters_in_box * (p_qty + (p_tiles / Convert.ToDecimal(db_tiles)))) + "' where P_Code='" + p_code + "' AND Quality='" + p_quality + "' AND Tone='" + tone + "'AND Size='" + p_size + "'", Login.con);
        //        cmd2.ExecuteNonQuery();
        //        test.bindingcode("select P_Code,Tone,Quality,Size,Boxes,Tiles,Meter,Price,Discount,Amount from cart", dataGridView, Login.con);
        //        SqlCommand cmd3 = new SqlCommand("SELECT isnull(sum(Amount),0) from cart_s", Login.con);
        //        SqlCommand cmd4 = new SqlCommand("SELECT isnull(sum(Amount),0) from cart", Login.con);
        //        txttotal.Text = Convert.ToString((Decimal)cmd4.ExecuteScalar() + (Decimal)cmd3.ExecuteScalar());
        //        txtgtotal.Text = Convert.ToString(Convert.ToDecimal(txttotal.Text) - Convert.ToDecimal(txtdiscount.Text));
        //        txtbalance.Text = Convert.ToString(Convert.ToDecimal(txtgtotal.Text) - Convert.ToDecimal(txtamount.Text));
        //        SqlCommand cmd5 = new SqlCommand("SELECT * from limit where id='1'", Login.con);
        //        SqlDataReader dr4 = cmd5.ExecuteReader();
        //        while (dr4.Read())
        //        {
        //            limit = Convert.ToDecimal(dr4[1]);
        //        }
        //        dr4.Close();
        //        if ((availableQty - (p_qty + (p_tiles / Convert.ToDecimal(db_tiles)))) < limit)
        //            MessageBox.Show("Remain Product Quantity is '" +  Convert.ToInt32(((availableQty - (p_qty + (p_tiles / Convert.ToInt32(db_tiles))))*db_tiles)/db_tiles) + " - "+Convert.ToInt32(((availableQty - (p_qty + (p_tiles / Convert.ToInt32(db_tiles)))) * db_tiles) % db_tiles)+"'", "AR Tranders", MessageBoxButtons.OK, MessageBoxIcon.Information);
        //    }
        //    else
        //    {
        //        MessageBox.Show("You Have Not Enough Quantity Against This Product Code", "AR Tranders", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
        //        test.bindingcode("select P_Code,Tone,Quality,Size,Boxes,Tiles,Meter,Price,Discount,Amount from cart", dataGridView, Login.con);
        //    }
        //    Login.con.Close();
        //    if (cmbdc.Text != "")
        //        dataGridView.Columns["Tiles"].ReadOnly = dataGridView.Columns["Boxes"].ReadOnly = dataGridView.Columns["P_Code"].ReadOnly = dataGridView.Columns["Tone"].ReadOnly = dataGridView.Columns["Quality"].ReadOnly = dataGridView.Columns["Size"].ReadOnly = dataGridView.Columns["Meter"].ReadOnly = dataGridView.Columns["Amount"].ReadOnly = true;
        //    else
        //        dataGridView.Columns["P_Code"].ReadOnly = dataGridView.Columns["Tone"].ReadOnly = dataGridView.Columns["Quality"].ReadOnly = dataGridView.Columns["Size"].ReadOnly = dataGridView.Columns["Meter"].ReadOnly = dataGridView.Columns["Amount"].ReadOnly = true;
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

        //private void dataGridView_Enter(object sender, EventArgs e)
        //{
        //    dataGridView.Columns["P_Code"].ReadOnly = dataGridView.Columns["Tone"].ReadOnly = dataGridView.Columns["Quality"].ReadOnly = dataGridView.Columns["Size"].ReadOnly = dataGridView.Columns["Meter"].ReadOnly = dataGridView.Columns["Amount"].ReadOnly = true;
        //}

        //private void dataGridView_KeyDown(object sender, KeyEventArgs e)
        //{
        //    if (e.KeyCode == Keys.Delete)
        //    {
        //        if (cmbdc.Text == "")
        //        {
        //            Login.con.Open();
        //            SqlCommand cmd = new SqlCommand("DELETE from cart where P_Code='" + delid + "' AND Quality='" + p_quality + "' AND Tone='" + tone + "'AND Size='" + p_size + "'", Login.con);
        //            cmd.ExecuteNonQuery();
        //            test.bindingcode("select P_Code,Tone,Quality,Size,Boxes,Tiles,Meter,Price,Discount,Amount from cart", dataGridView, Login.con);
        //            SqlCommand cmd3 = new SqlCommand("SELECT isnull(sum(Amount),0) from cart_s", Login.con);
        //            SqlCommand cmd4 = new SqlCommand("SELECT isnull(sum(Amount),0) from cart", Login.con);
        //            txttotal.Text = Convert.ToString((Decimal)cmd4.ExecuteScalar() + (Decimal)cmd3.ExecuteScalar());
        //            txtgtotal.Text = Convert.ToString(Convert.ToDecimal(txttotal.Text) - Convert.ToDecimal(txtdiscount.Text));
        //            txtbalance.Text = Convert.ToString(Convert.ToDecimal(txtgtotal.Text) - Convert.ToDecimal(txtamount.Text));
        //            Login.con.Close();
        //            if (dataGridView.RowCount == 0)
        //                if (dataGridView2.RowCount == 0)
        //                {
        //                    data = false;
        //                    btninvoice.Enabled = false;
        //                }
        //        }
        //    }
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
        //    else if (dataGridView.CurrentCell.ColumnIndex == 7)
        //    {
        //        TextBox tb = e.Control as TextBox;
        //        if (tb != null)
        //        {
        //            tb.KeyPress += new KeyPressEventHandler(test.decimaldigit_correction);
        //        }
        //    }
        //    else if (dataGridView.CurrentCell.ColumnIndex == 8)
        //    {
        //        TextBox tb = e.Control as TextBox;
        //        if (tb != null)
        //        {
        //            tb.KeyPress += new KeyPressEventHandler(test.decimaldigit_correction);
        //        }
        //    }
        //}

        //Add Stock Sanitary
        private void ckbx2_CheckedChanged(object sender, EventArgs e)
        {
            //if (ckbx.Checked == false)
            //    ckbx2.Checked = true;
            //if (ckbx2.Checked == true)
            //{
            //}
            //else
            //    txtqty2.Enabled = txtcode.Enabled = cmbname.Enabled = txtprice2.Enabled = dataGridView2.Enabled = label21.Enabled = label20.Enabled = label19.Enabled = label4.Enabled = false;
            txtqty2.Enabled = txtcode.Enabled = cmbname.Enabled = txtprice2.Enabled = dataGridView2.Enabled = label21.Enabled = label20.Enabled = label19.Enabled = label4.Enabled = true;
            cmbname.Focus();
            txtqty2.Text = "1";
            txtcode.Text = txtprice2.Text = cmbname.Text = "";
            Login.con.Open();
            SqlCommand cmd2 = new SqlCommand("DELETE from cart_s", Login.con);
            cmd2.ExecuteNonQuery();
            Login.con.Close();
            test.bindingcode("SELECT * From cart_s", dataGridView2, Login.con);
        }

        private void txtqty2_KeyPress(object sender, KeyPressEventArgs e)
        {
            test.digit_correction(sender, e);
        }

        private void txtqty2_Leave(object sender, EventArgs e)
        {
            if (txtqty2.Text == "" || Convert.ToDecimal(txtqty2.Text) == 0)
                txtqty2.Text = "1";
        }

        private void txtqty2_MouseDown(object sender, MouseEventArgs e)
        {
            txtqty2.SelectAll();
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

        private void toolStripMenuItem4_Click_1(object sender, EventArgs e)
        {
            Customer f = new Customer(this);
            f.Show();
        }

        private void toolStripMenuItem5_Click(object sender, EventArgs e)
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
                    txtprice2.Text = "";
                entercartdata();
            }
        }

        private void txtprice2_KeyPress(object sender, KeyPressEventArgs e)
        {
            test.decimaldigit_correction(sender, e);
        }

        private void dataGridView2_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = this.dataGridView2.Rows[e.RowIndex];
                delid = row.Cells[0].Value.ToString();
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
            availableQty = available_qty(p_code);
            //if (p_qty <= availableQty)
            if (true)
            {
                SqlCommand cmd2 = new SqlCommand("UPDATE cart_s set Discount ='" + p_disc + "', Amount='" + ((((p_qty)) * (p_price)) - ((((p_qty)) * (p_price) * (p_disc)) / 100)) + "' ,Price='" + p_price + "',Quantity='" + p_qty + "'where P_Code='" + p_code + "'", Login.con);
                cmd2.ExecuteNonQuery();
                test.bindingcode("select * from cart_s", dataGridView2, Login.con);
                SqlCommand cmd3 = new SqlCommand("SELECT isnull(sum(Amount),0) from cart_s", Login.con);
                //SqlCommand cmd4 = new SqlCommand("SELECT isnull(sum(Amount),0) from cart", Login.con);
                txttotal.Text = Convert.ToString((Decimal)cmd3.ExecuteScalar());
                txtgtotal.Text = Convert.ToString(Convert.ToDecimal(txttotal.Text) - Convert.ToDecimal(txtdiscount.Text));
                txtbalance.Text = Convert.ToString(Convert.ToDecimal(txtgtotal.Text) - Convert.ToDecimal(txtamount.Text));
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
                test.bindingcode("select * from cart_s", dataGridView2, Login.con);
            }
            Login.con.Close();
            if (cmbdc.Text != "")
                dataGridView2.Columns["P_Code"].ReadOnly = dataGridView2.Columns["P_Name"].ReadOnly = dataGridView2.Columns["Amount"].ReadOnly = dataGridView2.Columns["Quantity"].ReadOnly = true;
            else
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

        private void dataGridView2_Enter(object sender, EventArgs e)
        {
            dataGridView2.Columns["P_Code"].ReadOnly = dataGridView2.Columns["P_Name"].ReadOnly = dataGridView2.Columns["Amount"].ReadOnly = true;
        }

        private void dataGridView2_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Delete)
            {
                if (delid != null)
                {
                    Login.con.Open();
                    SqlCommand cmd = new SqlCommand("DELETE from cart_s where P_Code='" + delid + "' ", Login.con);
                    cmd.ExecuteNonQuery();
                    test.bindingcode("select * from cart_s", dataGridView2, Login.con);
                    SqlCommand cmd3 = new SqlCommand("SELECT isnull(sum(Amount),0) from cart_s", Login.con);
                    //SqlCommand cmd4 = new SqlCommand("SELECT isnull(sum(Amount),0) from cart", Login.con);
                    txttotal.Text = Convert.ToString((Decimal)cmd3.ExecuteScalar());
                    txtgtotal.Text = Convert.ToString(Convert.ToDecimal(txttotal.Text) - Convert.ToDecimal(txtdiscount.Text));
                    txtbalance.Text = Convert.ToString(Convert.ToDecimal(txtgtotal.Text) - Convert.ToDecimal(txtamount.Text));
                    Login.con.Close();
                    delid = null;
                    if (dataGridView2.RowCount == 0)
                    //if (dataGridView.RowCount == 0)
                    {
                        data = false;
                        btninvoice.Enabled = false;
                    }
                }
            }
        }

        private void cmbquality2_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbdc.Text == "")
            {
                cmbcustomername.Text = "";
                txtprebalance.Text = "0";
            }
        }

        private void cmbcustomername_Enter(object sender, EventArgs e)
        {
            if (cmbquality2.Text == "Customer" || cmbquality2.Text == "Stakeholder")
            {
                if (cmbquality2.Text == "Customer")
                    type = "FIXED";
                else if (cmbquality2.Text == "Stakeholder")
                    type = "STAKEHOLDER";
                string command = "select c_name from customer where type='" + type + "' order by c_name";
                test.cmbox_drop(command, cmbcustomername, Login.con);
            }
            else if (cmbquality2.Text == "Manufacturer")
            {
                string command = "select name from manufacturer order by name";
                test.cmbox_drop(command, cmbcustomername, Login.con);
            }
        }

        private void cmbcustomername_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                if (cmbcustomername.Text != "")
                {
                    cmbcustomername.Text = cmbcustomername.Text.ToUpper();
                    cmbcustomername.SelectAll();
                    sdebit = scredit = srdebit = prdebit = prcredit = srcredit = tdebit = pcredit = pdebit = tcredit = 0;
                    Login.con.Open();
                    if (cmbquality2.Text == "Customer" || cmbquality2.Text == "Stakeholder")
                    {
                        SqlCommand cmd = new SqlCommand("select (ss.total-ss.discount) Debit ,ss.receive Credit from customer c, cus_transaction cus,sale_stock ss where c.c_id=ss.c_id AND c.type='" + type + "' AND cus.cus_id=ss.cus_id AND c.c_name='" + cmbcustomername.Text.ToUpper() + "'AND cus.datetime <= '" + DateTime.Now + "'", Login.con);
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
                        SqlCommand cmd7 = new SqlCommand("select ps.paid Debit,ps.total Credit from customer c, cus_transaction cus,purchase_stock ps where c.c_id=ps.c_id AND cus.cus_id=ps.cus_id AND c.c_name = '" + cmbcustomername.Text.ToUpper() + "' AND cus.datetime <= '" + DateTime.Now + "'", Login.con);
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
                        SqlCommand cmd2 = new SqlCommand("select sr.Paid Debit, (sr.total-sr.deduct) Credit from customer c, cus_transaction cus, sale_return sr where c.c_id = sr.c_id AND c.type = '" + type + "' AND cus.cus_id = sr.cus_id AND c.c_name = '" + cmbcustomername.Text.ToUpper() + "'AND cus.datetime  <= '" + DateTime.Now + "'", Login.con);
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
                        SqlCommand cmd22 = new SqlCommand("select pr.total Debit, pr.receive Credit from customer c, cus_transaction cus, purchase_return pr where c.c_id = pr.c_id AND c.type = '" + type + "' AND cus.cus_id = pr.cus_id AND c.c_name = '" + cmbcustomername.Text.ToUpper() + "'AND cus.datetime  <= '" + DateTime.Now + "'", Login.con);
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
                        SqlCommand cmd3 = new SqlCommand("select ct.amount Debit from customer c, cus_transaction cus, customer_transaction ct where c.c_id = ct.c_id AND c.type = '" + type + "' AND cus.cus_id = ct.cus_id AND c.c_name = '" + cmbcustomername.Text.ToUpper() + "'AND cus.datetime <= '" + DateTime.Now + "' AND ct.status = 'PAID'", Login.con);
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
                        SqlCommand cmd4 = new SqlCommand("select ct.amount Credit from customer c, cus_transaction cus, customer_transaction ct where c.c_id = ct.c_id AND c.type = '" + type + "' AND cus.cus_id = ct.cus_id AND c.c_name = '" + cmbcustomername.Text.ToUpper() + "'AND cus.datetime <= '" + DateTime.Now + "' AND ct.status = 'RECEIVED'", Login.con);
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
                        SqlCommand cmd = new SqlCommand("select ps.paid Debit,ps.total Credit from manufacturer m, man_transaction man,purchase_stock ps where m.m_id=ps.m_id AND man.man_id=ps.man_id AND m.name = '" + cmbcustomername.Text.ToUpper() + "' AND man.datetime <= '" + DateTime.Now + "'", Login.con);
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
                        SqlCommand cmd7 = new SqlCommand("select (ss.total-ss.discount) Debit ,ss.receive Credit from manufacturer m, man_transaction man,sale_stock ss where m.m_id=ss.m_id AND man.man_id=ss.man_id AND m.name='" + cmbcustomername.Text.ToUpper() + "'AND man.datetime <= '" + DateTime.Now + "'", Login.con);
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
                        SqlCommand cmd2 = new SqlCommand("select sr.Paid Debit, (sr.total-sr.deduct) Credit from manufacturer m, man_transaction man, sale_return sr where m.m_id = sr.m_id AND man.man_id = sr.man_id AND m.name = '" + cmbcustomername.Text.ToUpper() + "'AND man.datetime  <= '" + DateTime.Now + "'", Login.con);
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
                        SqlCommand cmd22 = new SqlCommand("select pr.total Debit, pr.receive Credit from manufacturer m, man_transaction man, purchase_return pr where m.m_id = pr.m_id AND man.man_id = pr.man_id AND m.name = '" + cmbcustomername.Text.ToUpper() + "'AND man.datetime  <= '" + DateTime.Now + "'", Login.con);
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
                        SqlCommand cmd3 = new SqlCommand("select mt.amount Debit from Manufacturer m, man_transaction man, manu_transaction mt where m.m_id = mt.m_id AND man.man_id = mt.man_id AND m.name = '" + cmbcustomername.Text.ToUpper() + "'AND man.datetime <= '" + DateTime.Now + "' AND mt.status = 'PAID'", Login.con);
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
                        SqlCommand cmd4 = new SqlCommand("select mt.amount Credit from Manufacturer m, man_transaction man, manu_transaction mt where m.m_id = mt.m_id AND man.man_id = mt.man_id AND m.name = '" + cmbcustomername.Text.ToUpper() + "'AND man.datetime <= '" + DateTime.Now + "' AND mt.status = 'RECEIVED'", Login.con);
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

        private void cmbcustomername_Leave(object sender, EventArgs e)
        {
            if (cmbcustomername.Text == "")
            {
                txtprebalance.Text = "0";
                cmb = false;
                btninvoice.Enabled = false;
            }
            else
            {
                cmbcustomername.Text = cmbcustomername.Text.ToUpper();
                cmbcustomername.SelectAll();
                Login.con.Open();
                if (cmbquality2.Text == "Customer" || cmbquality2.Text == "Stakeholder")
                {
                    SqlCommand cmd8 = new SqlCommand("SELECT c_id from customer where c_name='" + cmbcustomername.Text.ToUpper() + "' AND type='" + type + "'", Login.con);
                    SqlDataReader dr8 = cmd8.ExecuteReader();
                    if (!dr8.HasRows)
                    {
                        dr8.Close();
                        MessageBox.Show("Record Not Found", "AR Tranders", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                        cmbcustomername.Focus();
                        txtprebalance.Text = "0";
                        cmb = false;
                        btninvoice.Enabled = false;
                    }
                    else
                    {
                        cmb = true;
                        if (data && cmb && sinvoice)
                        {
                            btninvoice.Enabled = true;
                        }
                    }
                }
                else if (cmbquality2.Text == "Manufacturer")
                {
                    SqlCommand cmd8 = new SqlCommand("SELECT m_id from manufacturer where name='" + cmbcustomername.Text.ToUpper() + "'", Login.con);
                    SqlDataReader dr8 = cmd8.ExecuteReader();
                    if (!dr8.HasRows)
                    {
                        dr8.Close();
                        MessageBox.Show("Record Not Found", "AR Tranders", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                        cmbcustomername.Focus();
                        txtprebalance.Text = "0";
                        cmb = false;
                        btninvoice.Enabled = false;
                    }
                    else
                    {
                        cmb = true;
                        if (data && cmb && sinvoice)
                        {
                            btninvoice.Enabled = true;
                        }
                    }
                }
                Login.con.Close();
            }
        }

        private void txtdiscount_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                txtamount.Focus();
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

        private void txtdiscount_MouseDown(object sender, MouseEventArgs e)
        {
            txtdiscount.SelectAll();
        }

        private void txtamount_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                txtsinvoiceno.Focus();
            }
        }

        private void txtamount_KeyPress(object sender, KeyPressEventArgs e)
        {
            test.decimaldigit_correction(sender, e);
        }

        private void txtamount_MouseDown(object sender, MouseEventArgs e)
        {
            txtamount.SelectAll();
        }

        private void txtamount_Leave(object sender, EventArgs e)
        {
            if (txtamount.Text == "")
                txtamount.Text = "0";
            else if (Convert.ToDecimal(txtamount.Text) == 0)
                txtamount.Text = "0";
            txtbalance.Text = Convert.ToString(Convert.ToDecimal(txtgtotal.Text) - Convert.ToDecimal(txtamount.Text));
        }

        private void dateTimePicker_Leave(object sender, EventArgs e)
        {
            if (DateTime.Now < dateTimePicker.Value)
                dateTimePicker.Value = DateTime.Now;
        }

        private void txtsinvoiceno_KeyPress(object sender, KeyPressEventArgs e)
        {
            test.digit_correction(sender, e);
        }

        private void txtsinvoiceno_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                if (txtsinvoiceno.Text != "")
                {
                    if (Convert.ToDecimal(txtsinvoiceno.Text) != 0)
                    {
                        Login.con.Open();
                        SqlCommand cmd = new SqlCommand("SELECT ss_id from sale_stock where ss_id='" + Convert.ToDecimal(txtsinvoiceno.Text) + "'", Login.con);
                        SqlDataReader dr = cmd.ExecuteReader();
                        if (dr.HasRows)
                        {
                            dr.Close();
                            MessageBox.Show("Sales Invoice No Already Exist", "AR Tranders", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                            txtsinvoiceno.Clear();
                            txtsinvoiceno.Focus();
                            sinvoice = false;
                            btninvoice.Enabled = false;
                        }
                        else
                        {
                            dr.Close();
                            sinvoice = true;
                            if (data && cmb && sinvoice)
                            {
                                btninvoice.Enabled = true;
                            }
                            btninvoice.Focus();
                        }
                        Login.con.Close();
                    }
                }
            }
        }

        private void btninvoice_Click(object sender, EventArgs e)
        {
            Decimal p_qty, p_price;
            Login.con.Open();
            if (cmbquality2.Text == "Customer" || cmbquality2.Text == "Stakeholder")
            {
                SqlCommand cmd8 = new SqlCommand("SELECT c_id from customer where c_name='" + cmbcustomername.Text.ToUpper() + "' AND type='" + type + "'", Login.con);
                SqlDataReader dr8 = cmd8.ExecuteReader();
                if (dr8.HasRows)
                {
                    while (dr8.Read())
                    {
                        customer_id = Convert.ToDecimal(dr8[0]);
                    }
                }
                dr8.Close();
            }
            else if (cmbquality2.Text == "Manufacturer")
            {
                SqlCommand cmd8 = new SqlCommand("SELECT m_id from manufacturer where name='" + cmbcustomername.Text.ToUpper() + "'", Login.con);
                SqlDataReader dr8 = cmd8.ExecuteReader();
                if (dr8.HasRows)
                {
                    while (dr8.Read())
                    {
                        customer_id = Convert.ToDecimal(dr8[0]);
                    }
                }
                dr8.Close();
            }
            //int flag = 0;
            //SqlCommand cmd11 = new SqlCommand("select count(*) from cart", Login.con);
            //int count = (int)cmd11.ExecuteScalar();
            //SqlCommand cmd12 = new SqlCommand("select Amount from cart", Login.con);
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
            SqlCommand cmd1123 = new SqlCommand("select count(*) from cart_s", Login.con);
            int count2 = (int)cmd1123.ExecuteScalar();
            SqlCommand cmd1223 = new SqlCommand("select Amount from cart_s", Login.con);
            SqlDataReader dr1223 = cmd1223.ExecuteReader();
            if (dr1223.HasRows)
            {
                while (dr1223.Read())
                {
                    if (Convert.ToDecimal(dr1223[0]) == 0)
                        flag2++;
                }
            }
            dr1223.Close();
            if (flag2 == count2)
            {
                MessageBox.Show("Please Set the Price in Cart First then Press 'Submit'", "AR Tranders", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Login.con.Close();
            }
            else if (flag2 == 0)
            {
                if (txtamount.Text == "0")
                {
                    if (txtsinvoiceno.Enabled != false)
                    {
                        if (cmbquality2.Text == "Customer" || cmbquality2.Text == "Stakeholder")
                        {

                            SqlCommand cmd2 = new SqlCommand("INSERT into cus_transaction (datetime) values('" + dateTimePicker.Value + "')", Login.con);
                            cmd2.ExecuteNonQuery();
                            SqlCommand cmd3 = new SqlCommand("SELECT cus_id from cus_transaction where datetime='" + dateTimePicker.Value + "'", Login.con);
                            Decimal cus_id = (Decimal)cmd3.ExecuteScalar();
                            SqlCommand cmd4 = new SqlCommand("INSERT into sale_stock(ss_id,c_id,datetime,total,discount,receive,cus_id) values('" + Convert.ToDecimal(txtsinvoiceno.Text) + "','" + customer_id + "', '" + dateTimePicker.Value + "', '" + Convert.ToDecimal(txttotal.Text) + "', '" + Convert.ToDecimal(txtdiscount.Text) + "','" + Convert.ToDecimal(txtamount.Text) + "','" + cus_id + "')", Login.con);
                            cmd4.ExecuteNonQuery();

                        }
                        else if (cmbquality2.Text == "Manufacturer")
                        {
                            SqlCommand cmd2 = new SqlCommand("INSERT into man_transaction (datetime) values('" + dateTimePicker.Value + "')", Login.con);
                            cmd2.ExecuteNonQuery();
                            SqlCommand cmd3 = new SqlCommand("SELECT man_id from man_transaction where datetime='" + dateTimePicker.Value + "'", Login.con);
                            Decimal cus_id = (Decimal)cmd3.ExecuteScalar();
                            SqlCommand cmd4 = new SqlCommand("INSERT into sale_stock(ss_id,m_id,datetime,total,discount,receive,man_id) values('" + Convert.ToDecimal(txtsinvoiceno.Text) + "','" + customer_id + "', '" + dateTimePicker.Value + "', '" + Convert.ToDecimal(txttotal.Text) + "', '" + Convert.ToDecimal(txtdiscount.Text) + "','" + Convert.ToDecimal(txtamount.Text) + "','" + cus_id + "')", Login.con);
                            cmd4.ExecuteNonQuery();
                        }
                    }
                    else
                    {
                        if (cmbquality2.Text == "Customer" || cmbquality2.Text == "Stakeholder")
                        {

                            SqlCommand cmd2 = new SqlCommand("INSERT into cus_transaction (datetime) values('" + dateTimePicker.Value + "')", Login.con);
                            cmd2.ExecuteNonQuery();
                            SqlCommand cmd3 = new SqlCommand("SELECT cus_id from cus_transaction where datetime='" + dateTimePicker.Value + "'", Login.con);
                            Decimal cus_id = (Decimal)cmd3.ExecuteScalar();
                            SqlCommand cmd4 = new SqlCommand("Update sale_stock set c_id = '" + customer_id + "',datetime='" + dateTimePicker.Value + "',total='" + Convert.ToDecimal(txttotal.Text) + "',discount='" + Convert.ToDecimal(txtdiscount.Text) + "',receive='" + Convert.ToDecimal(txtamount.Text) + "',cus_id='" + cus_id + "' where ss_id='" + Convert.ToDecimal(txtsinvoiceno.Text) + "'", Login.con);
                            cmd4.ExecuteNonQuery();

                        }
                        else if (cmbquality2.Text == "Manufacturer")
                        {
                            SqlCommand cmd2 = new SqlCommand("INSERT into man_transaction (datetime) values('" + dateTimePicker.Value + "')", Login.con);
                            cmd2.ExecuteNonQuery();
                            SqlCommand cmd3 = new SqlCommand("SELECT man_id from man_transaction where datetime='" + dateTimePicker.Value + "'", Login.con);
                            Decimal cus_id = (Decimal)cmd3.ExecuteScalar();
                            SqlCommand cmd4 = new SqlCommand("Update sale_stock set m_id='" + customer_id + "',datetime= '" + dateTimePicker.Value + "', total='" + Convert.ToDecimal(txttotal.Text) + "', discount='" + Convert.ToDecimal(txtdiscount.Text) + "',receive='" + Convert.ToDecimal(txtamount.Text) + "',man_id='" + cus_id + "' where ss_id='" + Convert.ToDecimal(txtsinvoiceno.Text) + "'", Login.con);
                            cmd4.ExecuteNonQuery();
                        }
                    }
                }
                else
                {
                    if (txtsinvoiceno.Enabled != false)
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
                            Decimal cus_id = (Decimal)cmd5.ExecuteScalar();
                            SqlCommand cmd6 = new SqlCommand("INSERT into sale_stock(ss_id,c_id,datetime,total,discount,receive,cat_id,cus_id) values('" + Convert.ToDecimal(txtsinvoiceno.Text) + "','" + customer_id + "', '" + dateTimePicker.Value + "', '" + Convert.ToDecimal(txttotal.Text) + "', '" + Convert.ToDecimal(txtdiscount.Text) + "', '" + Convert.ToDecimal(txtamount.Text) + "','" + cat_id + "','" + cus_id + "')", Login.con);
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
                            Decimal cus_id = (Decimal)cmd5.ExecuteScalar();
                            SqlCommand cmd6 = new SqlCommand("INSERT into sale_stock(ss_id,m_id,datetime,total,discount,receive,cat_id,man_id) values('" + Convert.ToDecimal(txtsinvoiceno.Text) + "','" + customer_id + "', '" + dateTimePicker.Value + "', '" + Convert.ToDecimal(txttotal.Text) + "', '" + Convert.ToDecimal(txtdiscount.Text) + "', '" + Convert.ToDecimal(txtamount.Text) + "','" + cat_id + "','" + cus_id + "')", Login.con);
                            cmd6.ExecuteNonQuery();
                        }
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
                            Decimal cus_id = (Decimal)cmd5.ExecuteScalar();
                            SqlCommand cmd6 = new SqlCommand("Update sale_stock set c_id='" + customer_id + "', datetime='" + dateTimePicker.Value + "', total='" + Convert.ToDecimal(txttotal.Text) + "', discount='" + Convert.ToDecimal(txtdiscount.Text) + "', receive='" + Convert.ToDecimal(txtamount.Text) + "',cat_id='" + cat_id + "',cus_id='" + cus_id + "' where ss_id='" + Convert.ToDecimal(txtsinvoiceno.Text) + "'", Login.con);
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
                            Decimal cus_id = (Decimal)cmd5.ExecuteScalar();
                            SqlCommand cmd6 = new SqlCommand("Update sale_stock set m_id='" + customer_id + "',datetime= '" + dateTimePicker.Value + "', total='" + Convert.ToDecimal(txttotal.Text) + "',discount= '" + Convert.ToDecimal(txtdiscount.Text) + "', receive='" + Convert.ToDecimal(txtamount.Text) + "',cat_id='" + cat_id + "',man_id='" + cus_id + "' where ss_id='" + Convert.ToDecimal(txtsinvoiceno.Text) + "'", Login.con);
                            cmd6.ExecuteNonQuery();
                        }
                    }
                }
                //if (cmbdc.Text != "")
                //{
                //    //SqlCommand cmd99 = new SqlCommand("select o_id from order_detail where o_id='" + cmbdc.Text + "'", Login.con);
                //    //SqlDataReader dr99 = cmd99.ExecuteReader();
                //    //if (dr99.HasRows)
                //    //{
                //    //    dr99.Close();
                //    //    SqlCommand cmd29 = new SqlCommand("DELETE from order_detail where o_id='" + cmbdc.Text + "'", Login.con);
                //    //    cmd29.ExecuteNonQuery();
                //    //}
                //    //dr99.Close();
                //    SqlCommand cmd19 = new SqlCommand("select o_id from order_detail2 where o_id='" + cmbdc.Text + "'", Login.con);
                //    SqlDataReader dr19 = cmd19.ExecuteReader();
                //    if (dr19.HasRows)
                //    {
                //        dr19.Close();
                //        SqlCommand cmd29 = new SqlCommand("DELETE from order_detail2 where o_id='" + cmbdc.Text + "'", Login.con);
                //        cmd29.ExecuteNonQuery();
                //    }
                //    dr19.Close();
                //    SqlCommand cmd39 = new SqlCommand("DELETE from order_stock where o_id='" + cmbdc.Text + "'", Login.con);
                //    cmd39.ExecuteNonQuery();
                //}
                //if (ckbx.Checked == true)
                //{
                //    if (dataGridView.Rows.Count != 0)
                //    {
                //while (true)
                //{
                //    SqlCommand cmd8 = new SqlCommand("SELECT Boxes,P_Code,Price,Quality,Tone,Discount,Size,Tiles from cart", Login.con);
                //    SqlDataReader dr8 = cmd8.ExecuteReader();
                //    if (dr8.HasRows)
                //    {
                //        if (dr8.Read())
                //        {
                //            p_qty = Convert.ToDecimal(dr8[0]);
                //            p_code = Convert.ToString(dr8[1]);
                //            p_price = Convert.ToDecimal(dr8[2]);
                //            p_disc = Convert.ToDecimal(dr8[5]);
                //            p_quality = Convert.ToString(dr8[3]);
                //            tone = Convert.ToString(dr8[4]);
                //            p_size = Convert.ToString(dr8[6]);
                //            p_tiles = Convert.ToDecimal(dr8[7]);
                //            dr8.Close();
                //            SqlCommand cmd9 = new SqlCommand("INSERT into sale_stock_detail values('" + Convert.ToDecimal(txtsinvoiceno.Text) + "','" + p_code + "','" + p_price + "','" + p_qty + "','" + p_disc + "','" + p_quality + "','" + tone + "','" + p_size + "','" + p_tiles + "')", Login.con);
                //            cmd9.ExecuteNonQuery();
                //            SqlCommand cmd112 = new SqlCommand("SELECT isnull(meters,0) from product where p_id = '" + p_code + "' AND size= '" + p_size + "'", Login.con);
                //            meters_in_box = (Decimal)cmd112.ExecuteScalar();
                //            SqlCommand cmd10 = new SqlCommand("DELETE cart where P_Code='" + p_code + "' AND Quality='" + p_quality + "'AND Tone='" + tone + "'AND Size='" + p_size + "' ", Login.con);
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
                if (txtsinvoiceno.Enabled != false)
                {
                    if (dataGridView2.Rows.Count != 0)
                    {
                        while (true)
                        {
                            SqlCommand cmd4 = new SqlCommand("SELECT * from cart_s", Login.con);
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
                                    SqlCommand cmd7 = new SqlCommand("INSERT into sale_stock_detail2 values('" + Convert.ToDecimal(txtsinvoiceno.Text) + "','" + p_code + "','" + p_price + "','" + p_qty + "','" + p_disc + "')", Login.con);
                                    cmd7.ExecuteNonQuery();
                                    SqlCommand cmd6 = new SqlCommand("DELETE cart_s where P_Code='" + p_code + "'", Login.con);
                                    cmd6.ExecuteNonQuery();
                                }
                            }
                            else
                            {
                                dr4.Close();
                                break;
                            }
                        }
                    }
                }
                else
                {
                    if (dataGridView2.Rows.Count != 0)
                    {
                        SqlCommand cmd100 = new SqlCommand("DELETE sale_stock_detail2 where ss_id='" + Convert.ToDecimal(txtsinvoiceno.Text) + "'", Login.con);
                        cmd100.ExecuteNonQuery();
                        while (true)
                        {
                            SqlCommand cmd4 = new SqlCommand("SELECT * from cart_s", Login.con);
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
                                    SqlCommand cmd7 = new SqlCommand("INSERT into sale_stock_detail2 values('" + Convert.ToDecimal(txtsinvoiceno.Text) + "','" + p_code + "','" + p_price + "','" + p_qty + "','" + p_disc + "')", Login.con);
                                    cmd7.ExecuteNonQuery();
                                    //SqlCommand cmd7 = new SqlCommand("Update sale_stock_detail2 set p_id='" + p_code + "', price='" + p_price + "', qty='" + p_qty + "', discount='" + p_disc + "' where ss_id='" + Convert.ToDecimal(txtsinvoiceno.Text) + "'", Login.con);
                                    //cmd7.ExecuteNonQuery();
                                    SqlCommand cmd6 = new SqlCommand("DELETE cart_s where P_Code='" + p_code + "'", Login.con);
                                    cmd6.ExecuteNonQuery();
                                }
                            }
                            else
                            {
                                dr4.Close();
                                break;
                            }
                        }
                    }
                }
                Login.con.Close();
                //
                DialogResult dresult = MessageBox.Show("Press 'OK' to Print GATE PASS", "AR Tranders", MessageBoxButtons.OKCancel, MessageBoxIcon.Question);
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
                DialogResult dresult2 = MessageBox.Show("Press 'OK' to Print SALES INVOICE", "AR Tranders", MessageBoxButtons.OKCancel, MessageBoxIcon.Question);
                if (dresult2 == DialogResult.OK)
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
                //
                reset_cart();
            }
            else
            {
                MessageBox.Show("Please Set the Price in Cart First then Press 'Submit'", "AR Tranders", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Login.con.Close();
            }
        }

        private void btnclear_Click(object sender, EventArgs e)
        {
            reset_cart();
        }

        private void linkfb_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            System.Diagnostics.Process.Start("https://www.facebook.com/geeksitsol");
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            txtdate.Text = DateTime.Now.ToShortDateString();
            txttime.Text = DateTime.Now.ToShortTimeString();
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
            e.Graphics.DrawString("Gate Pass", new Font("Calibri", 12, FontStyle.Underline), new SolidBrush(Color.Black), startX, startY + Offset);
            Offset = Offset + 30;
            e.Graphics.DrawString("Invoice No:", new Font("Calibri", 12), new SolidBrush(Color.Black), startX, startY + Offset);
            e.Graphics.DrawString(txtsinvoiceno.Text, new Font("Calibri", 12, FontStyle.Bold), new SolidBrush(Color.Black), startX + 85, startY + Offset);
            Offset = Offset + 30;
            e.Graphics.DrawString("Customer Name:", new Font("Calibri", 10, FontStyle.Bold), new SolidBrush(Color.Black), startX, startY + Offset);
            e.Graphics.DrawString(cmbcustomername.Text, new Font("Calibri", 10), new SolidBrush(Color.Black), startX + 105, startY + Offset);
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
                e.Graphics.DrawString("P_Code", new Font("Calibri", 10, FontStyle.Bold), new SolidBrush(Color.Black), startX + 45, startY + Offset);
                e.Graphics.DrawString("P_Name", new Font("Calibri", 10, FontStyle.Bold), new SolidBrush(Color.Black), startX + 160, startY + Offset);
                e.Graphics.DrawString("Qty", new Font("Calibri", 10, FontStyle.Bold), new SolidBrush(Color.Black), startX + 580, startY + Offset);
                Offset = Offset + 20;
                e.Graphics.DrawString(underLine, new Font("Courier New", 10, FontStyle.Bold), new SolidBrush(Color.Black), startX, startY + Offset);
                Offset = Offset + 20;
                int a = dataGridView2.Rows.Count;
                for (; j < a; j++)
                {
                    e.Graphics.DrawString(Convert.ToString(j + 1), new Font("Calibri", 9), new SolidBrush(Color.Black), startX, startY + Offset);
                    e.Graphics.DrawString(Convert.ToString(dataGridView2.Rows[j].Cells[0].Value), new Font("Calibri", 9), new SolidBrush(Color.Black), startX + 45, startY + Offset);
                    e.Graphics.DrawString(Convert.ToString(dataGridView2.Rows[j].Cells[1].Value), new Font("Calibri", 9), new SolidBrush(Color.Black), startX + 160, startY + Offset);
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

        decimal total;
        private void printDocument2_PrintPage(object sender, System.Drawing.Printing.PrintPageEventArgs e)
        {
            total = 0;
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
            e.Graphics.DrawString("Invoice No:", new Font("Calibri", 12), new SolidBrush(Color.Black), startX, startY + Offset);
            e.Graphics.DrawString(txtsinvoiceno.Text, new Font("Calibri", 12, FontStyle.Bold), new SolidBrush(Color.Black), startX + 85, startY + Offset);
            Offset = Offset + 30;
            e.Graphics.DrawString("A/c Name:", new Font("Calibri", 10, FontStyle.Bold), new SolidBrush(Color.Black), startX, startY + Offset);
            e.Graphics.DrawString(cmbcustomername.Text, new Font("Calibri", 10), new SolidBrush(Color.Black), startX + 85, startY + Offset);
            Offset = Offset + 20;
            string underLine = "=======================================================================================";
            //if (dataGridView.Rows.Count != 0)
            //{
            //    e.Graphics.DrawString(underLine, new Font("Courier New", 10, FontStyle.Bold), new SolidBrush(Color.Black), startX, startY + Offset);
            //    Offset = Offset + 20;
            //    e.Graphics.DrawString("Sr#", new Font("Calibri", 10, FontStyle.Bold), new SolidBrush(Color.Black), startX, startY + Offset);
            //    e.Graphics.DrawString("P_Code", new Font("Calibri", 10, FontStyle.Bold), new SolidBrush(Color.Black), startX + 45, startY + Offset);
            //    e.Graphics.DrawString("Tone", new Font("Calibri", 10, FontStyle.Bold), new SolidBrush(Color.Black), startX + 180, startY + Offset);
            //    e.Graphics.DrawString("Quality", new Font("Calibri", 10, FontStyle.Bold), new SolidBrush(Color.Black), startX + 230, startY + Offset);
            //    e.Graphics.DrawString("Size", new Font("Calibri", 10, FontStyle.Bold), new SolidBrush(Color.Black), startX + 300, startY + Offset);
            //    e.Graphics.DrawString("Box/T", new Font("Calibri", 10, FontStyle.Bold), new SolidBrush(Color.Black), startX + 355, startY + Offset);
            //    e.Graphics.DrawString("Meters", new Font("Calibri", 10, FontStyle.Bold), new SolidBrush(Color.Black), startX + 410, startY + Offset);
            //    e.Graphics.DrawString("Price", new Font("Calibri", 10, FontStyle.Bold), new SolidBrush(Color.Black), startX + 475, startY + Offset);
            //    e.Graphics.DrawString("Amount", new Font("Calibri", 10, FontStyle.Bold), new SolidBrush(Color.Black), startX + 550, startY + Offset);
            //    e.Graphics.DrawString("Disc%", new Font("Calibri", 10, FontStyle.Bold), new SolidBrush(Color.Black), startX + 625, startY + Offset);
            //    e.Graphics.DrawString("F_Amount", new Font("Calibri", 10, FontStyle.Bold), new SolidBrush(Color.Black), startX + 685, startY + Offset);
            //    Offset = Offset + 20;
            //    e.Graphics.DrawString(underLine, new Font("Courier New", 10, FontStyle.Bold), new SolidBrush(Color.Black), startX, startY + Offset);
            //    Offset = Offset + 20;
            //    int a = dataGridView.Rows.Count;
            //    for (; i < a; i++)
            //    {
            //        e.Graphics.DrawString(Convert.ToString(i + 1), new Font("Calibri", 9), new SolidBrush(Color.Black), startX, startY + Offset);
            //        e.Graphics.DrawString(Convert.ToString(dataGridView.Rows[i].Cells[0].Value), new Font("Calibri", 9), new SolidBrush(Color.Black), startX + 45, startY + Offset);
            //        e.Graphics.DrawString(Convert.ToString(dataGridView.Rows[i].Cells[1].Value), new Font("Calibri", 9), new SolidBrush(Color.Black), startX + 180, startY + Offset);
            //        e.Graphics.DrawString(Convert.ToString(dataGridView.Rows[i].Cells[2].Value), new Font("Calibri", 9), new SolidBrush(Color.Black), startX + 230, startY + Offset);
            //        e.Graphics.DrawString(Convert.ToString(dataGridView.Rows[i].Cells[3].Value), new Font("Calibri", 9), new SolidBrush(Color.Black), startX + 300, startY + Offset);
            //        e.Graphics.DrawString(Convert.ToString(dataGridView.Rows[i].Cells[4].Value + " - " + dataGridView.Rows[i].Cells[5].Value), new Font("Calibri", 9), new SolidBrush(Color.Black), startX + 355, startY + Offset);
            //        e.Graphics.DrawString(Convert.ToString(dataGridView.Rows[i].Cells[6].Value), new Font("Calibri", 9), new SolidBrush(Color.Black), startX + 410, startY + Offset);
            //        e.Graphics.DrawString(Convert.ToString(dataGridView.Rows[i].Cells[7].Value), new Font("Calibri", 9), new SolidBrush(Color.Black), startX + 475, startY + Offset);
            //        e.Graphics.DrawString(Convert.ToString(Math.Round(Convert.ToDecimal(dataGridView.Rows[i].Cells[7].Value) * Convert.ToDecimal(dataGridView.Rows[i].Cells[6].Value),2)), new Font("Calibri", 9), new SolidBrush(Color.Black), startX + 550, startY + Offset);
            //        e.Graphics.DrawString(Convert.ToString(dataGridView.Rows[i].Cells[8].Value), new Font("Calibri", 9), new SolidBrush(Color.Black), startX + 625, startY + Offset);
            //        e.Graphics.DrawString(Convert.ToString(dataGridView.Rows[i].Cells[9].Value), new Font("Calibri", 9), new SolidBrush(Color.Black), startX + 685, startY + Offset);
            //        total += Math.Round(Convert.ToDecimal(dataGridView.Rows[i].Cells[7].Value) * Convert.ToDecimal(dataGridView.Rows[i].Cells[6].Value), 2);
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
                e.Graphics.DrawString("Price", new Font("Calibri", 10, FontStyle.Bold), new SolidBrush(Color.Black), startX + 475, startY + Offset);
                e.Graphics.DrawString("Amount", new Font("Calibri", 10, FontStyle.Bold), new SolidBrush(Color.Black), startX + 550, startY + Offset);
                e.Graphics.DrawString("Disc%", new Font("Calibri", 10, FontStyle.Bold), new SolidBrush(Color.Black), startX + 625, startY + Offset);
                e.Graphics.DrawString("F_Amount", new Font("Calibri", 10, FontStyle.Bold), new SolidBrush(Color.Black), startX + 685, startY + Offset);
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
                    e.Graphics.DrawString(Convert.ToString(dataGridView2.Rows[j].Cells[3].Value), new Font("Calibri", 9), new SolidBrush(Color.Black), startX + 475, startY + Offset);
                    e.Graphics.DrawString(Convert.ToString(Math.Round(Convert.ToDecimal(dataGridView2.Rows[j].Cells[2].Value) * Convert.ToDecimal(dataGridView2.Rows[j].Cells[3].Value), 2)), new Font("Calibri", 9), new SolidBrush(Color.Black), startX + 550, startY + Offset);
                    e.Graphics.DrawString(Convert.ToString(dataGridView2.Rows[j].Cells[4].Value), new Font("Calibri", 9), new SolidBrush(Color.Black), startX + 625, startY + Offset);
                    e.Graphics.DrawString(Convert.ToString(dataGridView2.Rows[j].Cells[5].Value), new Font("Calibri", 9), new SolidBrush(Color.Black), startX + 685, startY + Offset);
                    total += Math.Round(Convert.ToDecimal(dataGridView2.Rows[j].Cells[2].Value) * Convert.ToDecimal(dataGridView2.Rows[j].Cells[3].Value), 2);
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
            e.Graphics.DrawString("Total:", new Font("Calibri", 10, FontStyle.Bold), new SolidBrush(Color.Black), startX + 475, startY + Offset);
            e.Graphics.DrawString(total.ToString(), new Font("Calibri", 10), new SolidBrush(Color.Black), startX + 550, startY + Offset);
            e.Graphics.DrawString(txttotal.Text, new Font("Calibri", 10), new SolidBrush(Color.Black), startX + 685, startY + Offset);
            Offset = Offset + 20;
            e.Graphics.DrawString(underLine, new Font("Courier New", 10, FontStyle.Bold), new SolidBrush(Color.Black), startX, startY + Offset);
            Offset = Offset + 20;
            e.Graphics.DrawString("Discount:", new Font("Calibri", 10, FontStyle.Bold), new SolidBrush(Color.Black), startX + 580, startY + Offset);
            e.Graphics.DrawString(txtdiscount.Text, new Font("Calibri", 10), new SolidBrush(Color.Black), startX + 685, startY + Offset);
            Offset = Offset + 20;
            e.Graphics.DrawString("Grand Total:", new Font("Calibri", 10, FontStyle.Bold), new SolidBrush(Color.Black), startX + 580, startY + Offset);
            e.Graphics.DrawString(txtgtotal.Text, new Font("Calibri", 10), new SolidBrush(Color.Black), startX + 685, startY + Offset);
            Offset = Offset + 20;
            e.Graphics.DrawString("Credit:", new Font("Calibri", 10, FontStyle.Bold), new SolidBrush(Color.Black), startX + 580, startY + Offset);
            e.Graphics.DrawString(txtamount.Text, new Font("Calibri", 10), new SolidBrush(Color.Black), startX + 685, startY + Offset);
            Offset = Offset + 20;
            e.Graphics.DrawString(underLine, new Font("Courier New", 10, FontStyle.Bold), new SolidBrush(Color.Black), startX, startY + Offset);
            Offset = Offset + 20;
            e.Graphics.DrawString("Balance:", new Font("Calibri", 10, FontStyle.Bold), new SolidBrush(Color.Black), startX + 580, startY + Offset);
            e.Graphics.DrawString(((Convert.ToDecimal(txtgtotal.Text)) - Convert.ToDecimal(txtamount.Text)).ToString(), new Font("Calibri", 10), new SolidBrush(Color.Black), startX + 685, startY + Offset);
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
