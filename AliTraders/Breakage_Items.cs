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
    public partial class Breakage_Items : Form
    {
        Form opener;
        Int32 db_tiles = 0;
        Boolean data, cmb = false;
        String delid, p_quality, tone, p_size, p_code;
        Decimal meters_in_box, p_tiles, dc_id, purchase_qty, cartQty, breakage_qty, p_qty, b_id;
        UF.Class1 test = new UF.Class1();

        public Breakage_Items(Form form)
        {
            InitializeComponent();
            opener = form;
        }

        private void Breakage_Items_Load(object sender, EventArgs e)
        {
            opener.Enabled = false;
            cart_clear();
        }

        private void Breakage_Items_FormClosing(object sender, FormClosingEventArgs e)
        {
            opener.Enabled = true;
        }

        //private void set_qty()
        //{
        //    if (cmbcode.Text != "" && cmbsize.Text != "")
        //    {
        //        txtqty.Text = (Convert.ToInt32(txtqty.Text) + (Convert.ToInt32(txttiles.Text) / db_tiles)).ToString();
        //        txttiles.Text = (Convert.ToInt32(txttiles.Text) % db_tiles).ToString();
        //    }
        //}

        private void cart_clear()
        {
            ckbx_CheckedChanged(this, null);
            ckbx2_CheckedChanged(this, null);
            txtdcno.Text = "";
            btnsubmit.Enabled = data = cmb = false;
            txtdcno.Focus();
        }

        private void entercartdata()
        {
            //if (ckbx.Checked == true)
            //{
            //    if (cmbcode.Text != "" && cmbsize.Text != "" && cmbtone.Text != "" && cmbquality.Text != "" && (txtqty.Text != "0" || txttiles.Text != "0"))
            //    {
            //        if (txtdcno.Text != "" && txtdcno.Text != "0")
            //        {
            //            Login.con.Open();
            //            //Retreive Quantity from Purchases Invoice
            //            SqlCommand cmd6 = new SqlCommand("SELECT isnull(meters,0) from product where p_id = '" + cmbcode.Text + "' AND Size= '" + cmbsize.Text + "'", Login.con);
            //            meters_in_box = (Decimal)cmd6.ExecuteScalar();
            //            SqlCommand cmd121 = new SqlCommand("SELECT isnull(pieces,0) from product where p_id = '" + p_code + "' AND Size= '" + cmbsize.Text + "' ", Login.con);
            //            SqlDataReader dr121 = cmd121.ExecuteReader();
            //            while (dr121.Read())
            //            {
            //                db_tiles = Convert.ToInt16(dr121[0]);
            //            }
            //            dr121.Close();
            //            SqlCommand cmd = new SqlCommand("select qty,tiles from purchase_stock_detail where p_id='" + cmbcode.Text + "' AND ps_id='" + Convert.ToDecimal(txtdcno.Text) + "' AND quality='" + cmbquality.Text + "' AND tone='" + cmbtone.Text + "' AND size='" + cmbsize.Text + "' ", Login.con);
            //            SqlDataReader dr = cmd.ExecuteReader();
            //            if (dr.HasRows)
            //            {
            //                while (dr.Read())
            //                {
            //                    if (Convert.ToDecimal(dr[1]) != 0)
            //                        purchase_qty = Convert.ToDecimal(dr[0]) + (Convert.ToDecimal(dr[1]) / Convert.ToDecimal(db_tiles));
            //                    else
            //                        purchase_qty = Convert.ToDecimal(dr[0]);
            //                }
            //                dr.Close();
            //                //Retreive Cart Quantity
            //                SqlCommand cmd2 = new SqlCommand("select isnull(Boxes,0),isnull(Tiles,0) from breakage_cart where P_Code ='" + cmbcode.Text + "' AND Size ='" + cmbsize.Text + "' AND Tone ='" + cmbtone.Text + "' AND Quality='" + cmbquality.Text + "'", Login.con);
            //                SqlDataReader dr2 = cmd2.ExecuteReader();
            //                if (dr2.HasRows)
            //                {
            //                    while (dr2.Read())
            //                    {
            //                        cartQty = Convert.ToDecimal(dr2[0]) + (Convert.ToDecimal(dr2[1]) / Convert.ToDecimal(db_tiles));
            //                    }
            //                }
            //                else
            //                {
            //                    cartQty = 0;
            //                }
            //                dr2.Close();
            //                SqlCommand cmd3 = new SqlCommand("select isnull(sum(bd.qty),0),isnull(sum(bd.tiles),0) from breakage_detail bd,breakage_stock bs where bd.b_id=bs.b_id AND bs.ps_id='" + Convert.ToDecimal(txtdcno.Text) + "' AND bd.p_id ='" + cmbcode.Text + "'  AND bd.quality ='" + cmbquality.Text + "' AND bd.tone ='" + cmbtone.Text + "' AND bd.size ='" + cmbsize.Text + "' AND ps_id='" + Convert.ToDecimal(txtdcno.Text) + "'", Login.con);
            //                SqlDataReader dr3 = cmd3.ExecuteReader();
            //                if (dr3.HasRows)
            //                {
            //                    while (dr3.Read())
            //                    {
            //                        breakage_qty = Convert.ToDecimal(dr3[0]) + (Convert.ToDecimal(dr3[1]) / Convert.ToDecimal(db_tiles));
            //                    }
            //                }
            //                else
            //                {
            //                    breakage_qty = 0;
            //                }
            //                dr3.Close();
            //                if (((cartQty + (Convert.ToDecimal(txtqty.Text) + (Convert.ToDecimal(txttiles.Text) / Convert.ToDecimal(db_tiles)))) < (purchase_qty - breakage_qty)) || ((cartQty + (Convert.ToDecimal(txtqty.Text) + (Convert.ToDecimal(txttiles.Text) / Convert.ToDecimal(db_tiles)))) == (purchase_qty - breakage_qty)))
            //                {
            //                    SqlCommand cmd4 = new SqlCommand("select * from product where p_id='" + cmbcode.Text + "' AND size='" + cmbsize.Text + "'", Login.con);
            //                    SqlDataReader dr4 = cmd4.ExecuteReader();
            //                    if (dr4.HasRows)
            //                    {
            //                        while (dr4.Read())
            //                        {
            //                            lbname.Text = dr4.GetString(1);
            //                        }
            //                        dr4.Close();
            //                        SqlCommand cmd5 = new SqlCommand("select * from breakage_cart where P_Code ='" + cmbcode.Text + "' AND Tone ='" + cmbtone.Text + "' AND Size ='" + cmbsize.Text + "' AND Quality ='" + cmbquality.Text + "'", Login.con);
            //                        SqlDataReader dr5 = cmd5.ExecuteReader();
            //                        if (dr5.HasRows)
            //                        {
            //                            dr5.Close();
            //                            MessageBox.Show("This Product Already Exist in Breakage Cart", "AR Tranders", MessageBoxButtons.OK, MessageBoxIcon.Stop);
            //                        }
            //                        else
            //                        {
            //                            dr5.Close();
            //                            SqlCommand cmd9 = new SqlCommand("insert into breakage_cart(P_Code,Tone,Quality,Size,Boxes,Tiles,Meters) values('" + cmbcode.Text + "','" + cmbtone.Text + "','" + cmbquality.Text + "','" + cmbsize.Text + "','" + Convert.ToDecimal(txtqty.Text) + "','" + Convert.ToDecimal(txttiles.Text) + "','" + (((Convert.ToDecimal(txttiles.Text) / db_tiles) + Convert.ToDecimal(txtqty.Text)) * meters_in_box) + "')", Login.con);
            //                            cmd9.ExecuteNonQuery();
            //                            String command = "select P_Code,Tone,Quality,Size,Boxes,Tiles,Meters from breakage_cart";
            //                            test.bindingcode(command, dataGridView, Login.con);
            //                            txtqty.Text = "1";
            //                            txttiles.Text = "0";
            //                            lbname.Text = cmbcode.Text = "";
            //                            cmbtone.Items.Clear();
            //                            cmbsize.Items.Clear();
            //                            cmbquality.Items.Clear();
            //                            Login.con.Close();
            //                            cmbcode.Focus();
            //                            Login.con.Open();
            //                            data = true;
            //                            if (data && cmb)
            //                            {
            //                                btnsubmit.Enabled = true;
            //                            }
            //                        }
            //                    }
            //                    else
            //                    {
            //                        dr4.Close();
            //                        MessageBox.Show("Record Not Found", "AR Tranders", MessageBoxButtons.OK, MessageBoxIcon.Stop);
            //                    }

            //                }
            //                else
            //                {
            //                    MessageBox.Show("Your Quantity Increases from DC", "AR Tranders", MessageBoxButtons.OK, MessageBoxIcon.Question);
            //                }
            //            }
            //            else
            //            {
            //                dr.Close();
            //                MessageBox.Show("This Product is Not in the DC", "AR Tranders", MessageBoxButtons.OK, MessageBoxIcon.Stop);
            //            }
            //        }
            //        else
            //        {
            //            MessageBox.Show("Please Enter DC No", "AR Tranders", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            //        }
            //        Login.con.Close();
            //    }
            //}
            //if (ckbx2.Checked == true)
            //{
            if (txtcode.Text != "")
            {
                if (txtdcno.Text != "" && txtdcno.Text != "0")
                {
                    Login.con.Open();
                    SqlCommand cmd = new SqlCommand("select qty from purchase_stock_detail2 where p_id='" + txtcode.Text + "' AND ps_id='" + Convert.ToDecimal(txtdcno.Text) + "'", Login.con);
                    SqlDataReader dr = cmd.ExecuteReader();
                    if (dr.HasRows)
                    {
                        while (dr.Read())
                        {
                            purchase_qty = Convert.ToDecimal(dr[0]);
                        }
                        dr.Close();
                        //Retreive Cart Quantity
                        SqlCommand cmd2 = new SqlCommand("select isnull(Quantity,0) from breakage_cart_s where P_Code ='" + txtcode.Text + "'", Login.con);
                        SqlDataReader dr2 = cmd2.ExecuteReader();
                        if (dr2.HasRows)
                        {
                            while (dr2.Read())
                            {
                                cartQty = Convert.ToDecimal(dr2[0]);
                            }
                        }
                        else
                        {
                            cartQty = 0;
                        }
                        dr2.Close();
                        SqlCommand cmd3 = new SqlCommand("select isnull(sum(bd.qty),0) from breakage_detail2 bd,breakage_stock bs where bd.b_id=bs.b_id AND bs.ps_id='" + Convert.ToDecimal(txtdcno.Text) + "' AND bd.p_id ='" + txtcode.Text + "' AND ps_id='" + Convert.ToDecimal(txtdcno.Text) + "'", Login.con);
                        SqlDataReader dr3 = cmd3.ExecuteReader();
                        if (dr3.HasRows)
                        {
                            while (dr3.Read())
                            {
                                breakage_qty = Convert.ToDecimal(dr3[0]);
                            }
                        }
                        else
                        {
                            breakage_qty = 0;
                        }
                        dr3.Close();
                        if (((cartQty + Convert.ToDecimal(txtqty2.Text)) < (purchase_qty - breakage_qty)) || ((cartQty + Convert.ToDecimal(txtqty2.Text)) == (purchase_qty - breakage_qty)))
                        {
                            SqlCommand cmd5 = new SqlCommand("select * from breakage_cart_s where P_Code ='" + txtcode.Text + "'", Login.con);
                            SqlDataReader dr5 = cmd5.ExecuteReader();
                            if (dr5.HasRows)
                            {
                                dr5.Close();
                                MessageBox.Show("This Product Already Exist in Breakage Cart", "AR Tranders", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                            }
                            else
                            {
                                dr5.Close();
                                SqlCommand cmd9 = new SqlCommand("insert into breakage_cart_s(P_Code,P_Name,Quantity) values('" + txtcode.Text + "','" + cmbname.Text + "','" + Convert.ToDecimal(txtqty2.Text) + "')", Login.con);
                                cmd9.ExecuteNonQuery();
                                String command = "select * from breakage_cart_s";
                                test.bindingcode(command, dataGridView2, Login.con);
                                txtqty2.Text = "1";
                                cmbname.Text = txtcode.Text = "";
                                Login.con.Close();
                                cmbname.Focus();
                                Login.con.Open();
                                data = true;
                                if (data && cmb)
                                {
                                    btnsubmit.Enabled = true;
                                }
                            }

                        }
                        else
                        {
                            MessageBox.Show("Your Quantity Increases from DC", "AR Tranders", MessageBoxButtons.OK, MessageBoxIcon.Question);
                        }
                    }
                    else
                    {
                        dr.Close();
                        MessageBox.Show("This Product is Not in the DC", "AR Tranders", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                    }
                }
                else
                {
                    MessageBox.Show("Please Enter DC No", "AR Tranders", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                Login.con.Close();
            }
            //}
        }

        //Add Breakage Tiles
        private void addorderToolStripMenuItem_Click(object sender, EventArgs e)
        {
            panelavailorder.Visible = false;
            //ckbx.Checked = true;
            cart_clear();
        }

        private void ckbx_CheckedChanged(object sender, EventArgs e)
        {
            //if (ckbx2.Checked == false)
            //    ckbx.Checked = true;
            //if (ckbx.Checked == true)
            //{
            //    txtqty.Enabled = label1.Enabled = txttiles.Enabled = cmbcode.Enabled = cmbsize.Enabled = cmbquality.Enabled = cmbtone.Enabled = lbname.Enabled = dataGridView.Enabled = label2.Enabled = label3.Enabled = label6.Enabled = label15.Enabled = label18.Enabled = btnenter.Enabled = true;
            //    cmbcode.Focus();
            //}
            //else
            //    txtqty.Enabled = label1.Enabled = txttiles.Enabled = cmbcode.Enabled = cmbsize.Enabled = cmbquality.Enabled = cmbtone.Enabled = lbname.Enabled = dataGridView.Enabled = label2.Enabled = label3.Enabled = label6.Enabled = label15.Enabled = label18.Enabled = btnenter.Enabled = false;
            //txtqty.Text = txttiles.Text = "0";
            //lbname.Text = cmbcode.Text = "";
            //cmbtone.Items.Clear();
            //cmbsize.Items.Clear();
            //cmbquality.Items.Clear();
            //Login.con.Open();
            //SqlCommand cmd = new SqlCommand("delete from breakage_cart", Login.con);
            //cmd.ExecuteNonQuery();
            //Login.con.Close();
            //string command = "select P_Code,Tone,Quality,Size,Boxes,Tiles,Meters from breakage_cart";
            //test.bindingcode(command, dataGridView, Login.con);
        }

        private void txtdcno_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                btnsubmit.Enabled = data = cmb = false;
                ckbx_CheckedChanged(this, null);
                ckbx2_CheckedChanged(this, null);
            }
        }

        private void txtdcno_Leave(object sender, EventArgs e)
        {
            if (txtdcno.Text != "")
            {
                if (Convert.ToDecimal(txtdcno.Text) != 0)
                {
                    Login.con.Open();
                    SqlCommand cmd = new SqlCommand("select ps_id FROM purchase_stock where ps_id='" + Convert.ToDecimal(txtdcno.Text) + "'", Login.con);
                    SqlDataReader dr = cmd.ExecuteReader();
                    if (dr.HasRows)
                    {
                        dr.Close();
                        cmb = true;
                        if (data && cmb)
                        {
                            btnsubmit.Enabled = true;
                        }
                    }
                    else
                    {
                        MessageBox.Show("Please Enter Correct DC No", "AR Tranders", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                        dr.Close();
                        txtdcno.Clear();
                        txtdcno.Focus();
                        cmb = false;
                        btnsubmit.Enabled = false;
                    }
                    Login.con.Close();
                }
            }
        }

        private void txtdcno_KeyPress(object sender, KeyPressEventArgs e)
        {
            test.digit_correction(sender, e);
        }

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

        //private void txttiles_MouseClick(object sender, MouseEventArgs e)
        //{
        //    txttiles.SelectAll();
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

        //private void txttiles_KeyPress(object sender, KeyPressEventArgs e)
        //{
        //    test.digit_correction(sender, e);
        //}

        //private void cmbcode_Enter(object sender, EventArgs e)
        //{
        //    if (txtdcno.Text != "")
        //    {
        //        string command = "select Distinct p_id from purchase_stock_detail where ps_id='" + Convert.ToDecimal(txtdcno.Text) + "'";
        //        test.cmbox_drop(command, cmbcode, Login.con);
        //    }
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
        //        SqlCommand cmd = new SqlCommand("SELECT * from purchase_stock_detail where p_id='" + cmbcode.Text + "'", Login.con);
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
        //            SqlCommand cmd = new SqlCommand("SELECT * from purchase_stock_detail where p_id='" + cmbcode.Text + "'", Login.con);
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
        //    if (txtdcno.Text != "" && cmbcode.Text != "")
        //    {
        //        string command = "select Distinct size from purchase_stock_detail where ps_id='" + Convert.ToDecimal(txtdcno.Text) + "' AND p_id='" + cmbcode.Text + "'";
        //        test.cmbox_drop(command, cmbsize, Login.con);
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

        //private void cmbquality_SelectedIndexChanged(object sender, EventArgs e)
        //{
        //    cmbtone.Items.Clear();
        //}

        //private void cmbquality_Enter(object sender, EventArgs e)
        //{
        //    if (txtdcno.Text != "" && cmbcode.Text != "" && cmbsize.Text != "")
        //    {
        //        string command = "select Distinct quality from purchase_stock_detail where ps_id='" + Convert.ToDecimal(txtdcno.Text) + "' AND p_id='" + cmbcode.Text + "' AND size='" + cmbsize.Text + "'";
        //        test.cmbox_drop(command, cmbquality, Login.con);
        //    }
        //}

        //private void cmbtone_Enter(object sender, EventArgs e)
        //{
        //    if (txtdcno.Text != "" && cmbcode.Text != "" && cmbsize.Text != "" && cmbquality.Text != "")
        //    {
        //        string command = "select Distinct tone from purchase_stock_detail where ps_id='" + Convert.ToDecimal(txtdcno.Text) + "' AND p_id='" + cmbcode.Text + "' AND size='" + cmbsize.Text + "' AND quality='" + cmbquality.Text + "'";
        //        test.cmbox_drop(command, cmbtone, Login.con);
        //    }
        //}

        //private void btnenter_Click(object sender, EventArgs e)
        //{
        //    entercartdata();
        //}

        //private void dataGridView_KeyDown(object sender, KeyEventArgs e)
        //{
        //    if (e.KeyCode == Keys.Delete)
        //    {
        //        Login.con.Open();
        //        SqlCommand cmd = new SqlCommand("DELETE from breakage_cart where P_Code='" + delid + "' AND Quality='" + p_quality + "' AND Tone='" + tone + "'AND Size='" + p_size + "'", Login.con);
        //        cmd.ExecuteNonQuery();
        //        string command = "select P_Code,Tone,Quality,Size,Boxes,Tiles,Meters from breakage_cart";
        //        test.bindingcode(command, dataGridView, Login.con);
        //        Login.con.Close();
        //        if (dataGridView.RowCount == 0)
        //            if (dataGridView2.RowCount == 0)
        //            {
        //                data = false;
        //                btnsubmit.Enabled = false;
        //            }
        //    }
        //}

        //private void dataGridView_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        //{
        //    if (txtdcno.Text != "" && txtdcno.Text != "0")
        //    {
        //        if (e.RowIndex >= 0)
        //        {
        //            DataGridViewRow row = this.dataGridView.Rows[e.RowIndex];
        //            p_code = row.Cells[0].Value.ToString();
        //            p_quality = row.Cells[2].Value.ToString();
        //            tone = row.Cells[1].Value.ToString();
        //            p_size = row.Cells[3].Value.ToString();
        //            Login.con.Open();
        //            SqlCommand cmd121 = new SqlCommand("SELECT isnull(pieces,0) from product where p_id = '" + p_code + "' AND size='" + p_size + "'", Login.con);
        //            SqlDataReader dr121 = cmd121.ExecuteReader();
        //            while (dr121.Read())
        //            {
        //                db_tiles = Convert.ToInt16(dr121[0]);
        //            }
        //            dr121.Close();
        //            Login.con.Close();
        //            if (row.Cells[4].Value.ToString() == "0" && row.Cells[5].Value.ToString() == "0")
        //                p_qty = 1;
        //            else if (row.Cells[4].Value.ToString() != "")
        //                p_qty = Convert.ToDecimal(row.Cells[4].Value.ToString());
        //            else
        //                p_qty = 1;
        //            if (row.Cells[5].Value.ToString() != "")
        //            {
        //                p_tiles = Convert.ToDecimal(row.Cells[5].Value.ToString());
        //                p_qty = Convert.ToDecimal(Convert.ToInt32(p_qty) + (Convert.ToInt32(p_tiles) / db_tiles));
        //                p_tiles = Convert.ToDecimal(Convert.ToInt32(p_tiles) % db_tiles);
        //            }
        //            else
        //                p_tiles = 0;

        //        }
        //        Login.con.Open();
        //        SqlCommand cmd = new SqlCommand("SELECT isnull(meters,0) from product where p_id = '" + p_code + "' AND size='" + p_size + "' ", Login.con);
        //        meters_in_box = (Decimal)cmd.ExecuteScalar();
        //        SqlCommand cmd3 = new SqlCommand("select isnull(sum(bd.qty),0),isnull(sum(bd.tiles),0) from breakage_detail bd,breakage_stock bs where bd.b_id=bs.b_id AND bs.ps_id='" + Convert.ToDecimal(txtdcno.Text) + "' AND bd.p_id ='" + p_code + "'  AND bd.quality ='" + p_quality + "' AND bd.tone ='" + tone + "' AND bd.size ='" + p_size + "' ", Login.con);
        //        SqlDataReader dr3 = cmd3.ExecuteReader();
        //        if (dr3.HasRows)
        //        {
        //            while (dr3.Read())
        //            {
        //                breakage_qty = Convert.ToDecimal(dr3[0]) + (Convert.ToDecimal(dr3[1]) / Convert.ToDecimal(db_tiles));
        //            }
        //        }
        //        else
        //        {
        //            breakage_qty = 0;
        //        }
        //        dr3.Close();
        //        SqlCommand cmd4 = new SqlCommand("select qty,tiles from purchase_stock_detail where p_id='" + p_code + "' AND ps_id='" + Convert.ToDecimal(txtdcno.Text) + "' AND quality='" + p_quality + "' AND tone='" + tone + "' AND size='" + p_size + "' ", Login.con);
        //        SqlDataReader dr4 = cmd4.ExecuteReader();
        //        if (dr4.HasRows)
        //        {
        //            while (dr4.Read())
        //            {
        //                purchase_qty = Convert.ToDecimal(dr4[0]) + (Convert.ToDecimal(dr4[1]) / Convert.ToDecimal(db_tiles));
        //            }
        //        }
        //        else
        //        {
        //            purchase_qty = 0;
        //        }
        //        dr4.Close();
        //        if (((p_qty + (p_tiles / Convert.ToDecimal(db_tiles))) < (purchase_qty - breakage_qty)) || ((p_qty + (p_tiles / Convert.ToDecimal(db_tiles))) == (purchase_qty - breakage_qty)))
        //        {
        //            SqlCommand cmd1 = new SqlCommand("update breakage_cart set Boxes='" + p_qty + "',Tiles='" + p_tiles + "',Meters= '" + (meters_in_box * ((p_tiles / Convert.ToDecimal(db_tiles)) + p_qty)) + "' where P_Code='" + p_code + "' AND Quality='" + p_quality + "' AND Tone='" + tone + "'AND Size='" + p_size + "'", Login.con);
        //            cmd1.ExecuteNonQuery();
        //            String command = "select P_Code,Tone,Quality,Size,Boxes,Tiles,Meters from breakage_cart";
        //            test.bindingcode(command, dataGridView, Login.con);
        //        }
        //        else
        //        {
        //            MessageBox.Show("You Have Not Enough Quantity Against This Product Code in Purchases Invoice", "AR Tranders", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
        //            String command = "select P_Code,Tone,Quality,Size,Boxes,Tiles,Meters from breakage_cart";
        //            test.bindingcode(command, dataGridView, Login.con);
        //        }
        //        Login.con.Close();
        //    }
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

        //Add Breakage Sanitary
        private void ckbx2_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void txtqty2_MouseDown(object sender, MouseEventArgs e)
        {
            txtqty2.SelectAll();
        }

        private void txtqty2_Leave(object sender, EventArgs e)
        {
            if (txtqty2.Text == "" || Convert.ToDecimal(txtqty2.Text) == 0)
                txtqty2.Text = "1";
        }

        private void txtqty2_KeyPress(object sender, KeyPressEventArgs e)
        {
            test.digit_correction(sender, e);
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

        private void cmbname_Enter(object sender, EventArgs e)
        {
            if (txtdcno.Text != "")
            {
                string command = "select Distinct p.p_name from purchase_stock_detail2 ps, product2 p where ps.ps_id='" + Convert.ToDecimal(txtdcno.Text) + "' AND ps.p_id=p.p_id";
                test.cmbox_drop(command, cmbname, Login.con);
            }
        }

        private void btnenter2_Click(object sender, EventArgs e)
        {
            entercartdata();
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
            if (txtdcno.Text != "" && txtdcno.Text != "0")
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
                SqlCommand cmd3 = new SqlCommand("select isnull(sum(bd.qty),0) from breakage_detail2 bd,breakage_stock bs where bd.b_id=bs.b_id AND bs.ps_id='" + Convert.ToDecimal(txtdcno.Text) + "' AND bd.p_id ='" + p_code + "' ", Login.con);
                SqlDataReader dr3 = cmd3.ExecuteReader();
                if (dr3.HasRows)
                {
                    while (dr3.Read())
                    {
                        breakage_qty = Convert.ToDecimal(dr3[0]);
                    }
                }
                else
                {
                    breakage_qty = 0;
                }
                dr3.Close();
                SqlCommand cmd4 = new SqlCommand("select qty from purchase_stock_detail2 where p_id='" + p_code + "' AND ps_id='" + Convert.ToDecimal(txtdcno.Text) + "'", Login.con);
                SqlDataReader dr4 = cmd4.ExecuteReader();
                if (dr4.HasRows)
                {
                    while (dr4.Read())
                    {
                        purchase_qty = Convert.ToDecimal(dr4[0]);
                    }
                }
                else
                {
                    purchase_qty = 0;
                }
                dr4.Close();
                if ((p_qty < (purchase_qty - breakage_qty)) || (p_qty == (purchase_qty - breakage_qty)))
                {
                    SqlCommand cmd1 = new SqlCommand("update breakage_cart_s set Quantity='" + p_qty + "' where P_Code='" + p_code + "'", Login.con);
                    cmd1.ExecuteNonQuery();
                    String command = "select * from breakage_cart_s";
                    test.bindingcode(command, dataGridView2, Login.con);
                }
                else
                {
                    MessageBox.Show("You Have Not Enough Quantity Against This Product Code in Purchases Invoice", "AR Tranders", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    String command = "select * from breakage_cart_s";
                    test.bindingcode(command, dataGridView2, Login.con);
                }
                Login.con.Close();
            }
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

        private void dataGridView2_Enter(object sender, EventArgs e)
        {
            dataGridView2.Columns["P_Code"].ReadOnly = dataGridView2.Columns["P_Name"].ReadOnly = true;
        }

        private void dataGridView2_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Delete)
            {
                Login.con.Open();
                SqlCommand cmd = new SqlCommand("DELETE from breakage_cart_s where P_Code='" + delid + "'", Login.con);
                cmd.ExecuteNonQuery();
                string command = "select * from breakage_cart_s";
                test.bindingcode(command, dataGridView2, Login.con);
                Login.con.Close();
                if (dataGridView2.RowCount == 0)
                //if (dataGridView.RowCount == 0)
                {
                    data = false;
                    btnsubmit.Enabled = false;
                }
            }
        }

        private void btnsubmit_Click(object sender, EventArgs e)
        {

            Login.con.Open();
            SqlCommand cmd6 = new SqlCommand("INSERT into breakage_stock(ps_id,datetime) values('" + Convert.ToDecimal(txtdcno.Text) + "','" + DateTime.Now + "')", Login.con);
            cmd6.ExecuteNonQuery();
            SqlCommand cmd7 = new SqlCommand("SELECT b_id from breakage_stock where datetime= (select max(datetime) from breakage_stock) ", Login.con);
            SqlDataReader dr7 = cmd7.ExecuteReader();
            while (dr7.Read())
            {
                b_id = Convert.ToDecimal(dr7[0]);
            }
            dr7.Close();
            //if (ckbx.Checked == true)
            //{
            //    if (dataGridView.Rows.Count != 0)
            //    {
            //        while (true)
            //        {
            //            SqlCommand cmd8 = new SqlCommand("SELECT Boxes,P_Code,Quality,Tone,Size,Tiles from breakage_cart", Login.con);
            //            SqlDataReader dr8 = cmd8.ExecuteReader();
            //            if (dr8.HasRows)
            //            {
            //                if (dr8.Read())
            //                {
            //                    p_qty = Convert.ToDecimal(dr8[0]);
            //                    p_code = Convert.ToString(dr8[1]);
            //                    p_quality = Convert.ToString(dr8[2]);
            //                    tone = Convert.ToString(dr8[3]);
            //                    p_size = Convert.ToString(dr8[4]);
            //                    p_tiles = Convert.ToDecimal(dr8[5]);
            //                    dr8.Close();
            //                    SqlCommand cmd9 = new SqlCommand("INSERT into breakage_detail values('" + b_id + "','" + p_code + "','" + p_qty + "', '" + p_quality + "', '" + tone + "','" + p_size + "','" + p_tiles + "')", Login.con);
            //                    cmd9.ExecuteNonQuery();
            //                    SqlCommand cmd10 = new SqlCommand("DELETE breakage_cart where P_Code='" + p_code + "' AND Quality='" + p_quality + "' AND Tone='" + tone + "' AND Size='" + p_size + "'", Login.con);
            //                    cmd10.ExecuteNonQuery();
            //                }
            //            }
            //            else
            //            {
            //                dr8.Close();
            //                break;
            //            }
            //        }
            //    }
            //}
            //if (ckbx2.Checked == true)
            //{
            if (dataGridView2.Rows.Count != 0)
            {

                while (true)
                {
                    SqlCommand cmd8 = new SqlCommand("SELECT Quantity,P_Code from breakage_cart_s", Login.con);
                    SqlDataReader dr8 = cmd8.ExecuteReader();
                    if (dr8.HasRows)
                    {
                        if (dr8.Read())
                        {
                            p_qty = Convert.ToDecimal(dr8[0]);
                            p_code = Convert.ToString(dr8[1]);
                            dr8.Close();
                            SqlCommand cmd9 = new SqlCommand("INSERT into breakage_detail2 values('" + b_id + "','" + p_code + "','" + p_qty + "')", Login.con);
                            cmd9.ExecuteNonQuery();
                            SqlCommand cmd10 = new SqlCommand("DELETE breakage_cart_s where P_Code='" + p_code + "'", Login.con);
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
            Login.con.Close();
            MessageBox.Show("Breakage Inserted Successfully", "AR Tranders", MessageBoxButtons.OK, MessageBoxIcon.Information);
            cart_clear();
        }

        private void btnclear_Click(object sender, EventArgs e)
        {
            cart_clear();
        }

        //Delete Breakage
        private void avilableStockToolStripMenuItem_Click(object sender, EventArgs e)
        {
            panelavailorder.Visible = true;
            string command = "select b_id Breakage_ID, ps_id DC_No, datetime DateTime From breakage_stock";
            test.bindingcode(command, dataGridView3, Login.con);
            btndetail.Enabled = false;
        }

        private void dataGridView3_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = this.dataGridView3.Rows[e.RowIndex];
                b_id = Convert.ToDecimal(row.Cells["Breakage_ID"].Value.ToString());
                dc_id = Convert.ToDecimal(row.Cells["DC_No"].Value.ToString());
                btndetail.Enabled = true;
            }
        }

        private void btndetail_Click(object sender, EventArgs e)
        {
            Breakage_Items_Detail f = new Breakage_Items_Detail(b_id, dc_id);
            f.Show();
        }
    }
}
