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
    public partial class Order_List : Form
    {
        Form opener;
        Boolean data, cmb = false;
        String delid, p_quality, tone, p_size, p_code, c_name, type;
        Decimal meters_in_box, user_qty, availableQty, purchase_return_qty, limit, sale_return_qty, breakage_qty, sale_qty, purchase_qty, p_qty, p_tiles, c_id, o_id, o_no;
        Int32 db_tiles = 0;
        UF.Class1 test = new UF.Class1();

        public Order_List(Form form)
        {
            InitializeComponent();
            opener = form;
        }

        private void Order_List_Load(object sender, EventArgs e)
        {
            opener.Enabled = false;
            //string command = "SELECT p_id from product order by p_id";
            //test.cmbox_drop(command, cmbcode, Login.con);
            string command3 = "SELECT p_name from product2 order by p_name";
            test.cmbox_drop(command3, cmbname, Login.con);
            string command2 = "SELECT c_name from customer order by c_name";
            test.cmbox_drop(command2, cmbcus, Login.con);
            ckbx_CheckedChanged(this, null);
            ckbx2_CheckedChanged(this, null);
        }

        private void Order_List_FormClosing(object sender, FormClosingEventArgs e)
        {
            opener.Enabled = true;
        }

        private void set_qty()
        {
            //if (cmbcode.Text != "" && cmbsize.Text != "")
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
            //    txtqty.Text = (Convert.ToInt32(txtqty.Text) + (Convert.ToInt32(txttiles.Text) / db_tiles)).ToString();
            //    txttiles.Text = (Convert.ToInt32(txttiles.Text) % db_tiles).ToString();
            //}
        }

        private void reset_cart()
        {
            cmbquality2.SelectedIndex = 0;
            cmbcus.Text = "";
            btnsubmit.Enabled = data = cmb = false;
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
            //    if (lbname.Text != "" && cmbtone.Text != "" && cmbsize.Text != "" && cmbquality.Text != "" && (txtqty.Text != "0" || txttiles.Text != "0"))
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
            //            SqlCommand cmd2 = new SqlCommand("SELECT Boxes,Tiles from order_cart where P_Code ='" + cmbcode.Text + "' AND Quality='" + cmbquality.Text + "' AND Tone='" + cmbtone.Text + "'AND Size= '" + cmbsize.Text + "' ", Login.con);
            //            SqlDataReader dr2 = cmd2.ExecuteReader();
            //            if (dr2.HasRows)
            //            {
            //                MessageBox.Show("This Product Already Exist in Gate Pass Cart", "AR Tranders", MessageBoxButtons.OK, MessageBoxIcon.Stop);
            //            }
            //            else
            //            {
            //                dr2.Close();
            //                if (user_qty <= availableQty)
            //                {
            //                    SqlCommand cmd3 = new SqlCommand("INSERT into order_cart(P_Code,Tone,Quality,Size,Boxes,Meters,Tiles) values('" + cmbcode.Text + "','" + cmbtone.Text + "','" + cmbquality.Text + "','" + cmbsize.Text + "','" + Convert.ToDecimal(txtqty.Text) + "','" + (((Convert.ToDecimal(txttiles.Text) / db_tiles) + Convert.ToDecimal(txtqty.Text)) * meters_in_box) + "','" + Convert.ToDecimal(txttiles.Text) + "')", Login.con);
            //                    cmd3.ExecuteNonQuery();
            //                    SqlCommand cmd5 = new SqlCommand("SELECT * from limit where id='1'", Login.con);
            //                    SqlDataReader dr5 = cmd5.ExecuteReader();
            //                    while (dr5.Read())
            //                    {
            //                        limit = Convert.ToDecimal(dr5[1]);
            //                    }
            //                    dr5.Close();
            //                    if ((availableQty - (user_qty)) < limit)
            //                        MessageBox.Show("Remain Product Quantity is '" + Convert.ToInt32((((availableQty - (user_qty)) * db_tiles) / db_tiles)) + " - " + Convert.ToInt32((((availableQty - (user_qty)) * db_tiles) % db_tiles)) + "'", "AR Tranders", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            //                    String command = "SELECT P_Code,Tone,Quality,Size,Boxes,Tiles,Meters from order_cart";
            //                    test.bindingcode(command, dataGridView, Login.con);
            //                    txtqty.Text = txttiles.Text = "0";
            //                    lbname.Text = cmbcode.Text = txtmeter.Text = "";
            //                    cmbtone.Items.Clear();
            //                    cmbsize.Items.Clear();
            //                    cmbquality.Items.Clear();
            //                    cmbcode.Focus();
            //                    data = true;
            //                    if (data && cmb)
            //                    {
            //                        btnsubmit.Enabled = true;
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
                SqlCommand cmd1 = new SqlCommand("SELECT Quantity from order_cart_s where P_Code ='" + txtcode.Text + "'", Login.con);
                SqlDataReader dr1 = cmd1.ExecuteReader();
                if (dr1.HasRows)
                {
                    MessageBox.Show("This Product Already Exist in Gate Pass Cart", "AR Tranders", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                    dr1.Close();
                }
                else
                {
                    dr1.Close();
                    if (user_qty <= availableQty)
                    {
                        SqlCommand cmd2 = new SqlCommand("INSERT into order_cart_s(Quantity,P_Code,P_Name) values('" + user_qty + "','" + txtcode.Text + "','" + cmbname.Text + "')", Login.con);
                        cmd2.ExecuteNonQuery();
                        SqlCommand cm = new SqlCommand("SELECT * from limit where id='1'", Login.con);
                        SqlDataReader d = cm.ExecuteReader();
                        while (d.Read())
                        {
                            limit = Convert.ToDecimal(d[1]);
                        }
                        d.Close();
                        if ((availableQty - (user_qty)) < limit)
                            MessageBox.Show("Remain Product Quantity is' " + (availableQty - user_qty) + "'", "AR Tranders", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        String command = "SELECT * from order_cart_s";
                        test.bindingcode(command, dataGridView2, Login.con);
                        txtqty2.Text = "1";
                        txtcode.Clear();
                        cmbname.Text = "";
                        cmbname.Focus();
                        data = true;
                        if (data && cmb)
                        {
                            btnsubmit.Enabled = true;
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
            //}
        }

        //Add Order
        private void addorderToolStripMenuItem_Click(object sender, EventArgs e)
        {
            panelavailorder.Visible = false;
            reset_cart();
        }

        private void ckbx_CheckedChanged(object sender, EventArgs e)
        {
            //if (ckbx2.Checked == false)
            //    ckbx.Checked = true;
            //if (ckbx.Checked == true)
            //{
            //    txtmeter.Enabled = txttiles.Enabled = label22.Enabled = txtqty.Enabled = cmbcode.Enabled = cmbsize.Enabled = cmbquality.Enabled = cmbtone.Enabled = lbname.Enabled = dataGridView.Enabled = label16.Enabled = label2.Enabled = label1.Enabled = label6.Enabled = label15.Enabled = label18.Enabled = true;
            //    cmbcode.Focus();
            //}
            //else
            //    txtmeter.Enabled = txttiles.Enabled = label22.Enabled = txtqty.Enabled = cmbcode.Enabled = cmbsize.Enabled = cmbquality.Enabled = cmbtone.Enabled = lbname.Enabled = dataGridView.Enabled = label16.Enabled = label2.Enabled = label1.Enabled = label6.Enabled = label15.Enabled = label18.Enabled = false;
            //txtqty.Text = txttiles.Text = "0";
            //lbname.Text = cmbcode.Text = txtmeter.Text = "";
            cmbquality2.SelectedIndex = 0;
            //cmbtone.Items.Clear();
            //cmbsize.Items.Clear();
            //cmbquality.Items.Clear();
            //Login.con.Open();
            //SqlCommand cmd = new SqlCommand("DELETE from order_cart", Login.con);
            //cmd.ExecuteNonQuery();
            //Login.con.Close();
            //test.bindingcode("SELECT P_Code,Tone,Quality,Size,Boxes,Tiles,Meters From order_cart", dataGridView, Login.con);
        }

        private void cmbquality2_SelectedIndexChanged(object sender, EventArgs e)
        {
            cmbcus.Text = "";
        }

        private void cmbcus_Enter(object sender, EventArgs e)
        {
            if (cmbquality2.Text == "Customer" || cmbquality2.Text == "Stakeholder")
            {
                if (cmbquality2.Text == "Customer")
                    type = "FIXED";
                else if (cmbquality2.Text == "Stakeholder")
                    type = "STAKEHOLDER";
                string command = "select c_name from customer where type='" + type + "' order by c_name";
                test.cmbox_drop(command, cmbcus, Login.con);
            }
            else if (cmbquality2.Text == "Manufacturer")
            {
                string command = "select name from manufacturer order by name";
                test.cmbox_drop(command, cmbcus, Login.con);
            }
        }

        private void cmbcus_Leave(object sender, EventArgs e)
        {
            if (cmbcus.Text == "")
            {
                cmb = false;
                btnsubmit.Enabled = false;
            }
            else
            {
                cmbcus.Text = cmbcus.Text.ToUpper();
                cmbcus.SelectAll();
                Login.con.Open();
                if (cmbquality2.Text == "Customer" || cmbquality2.Text == "Stakeholder")
                {
                    SqlCommand cmd8 = new SqlCommand("SELECT c_id from customer where c_name='" + cmbcus.Text.ToUpper() + "' AND type='" + type + "'", Login.con);
                    SqlDataReader dr8 = cmd8.ExecuteReader();
                    if (!dr8.HasRows)
                    {
                        dr8.Close();
                        MessageBox.Show("Record Not Found", "AR Tranders", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                        cmbcus.Focus();
                        cmb = false;
                        btnsubmit.Enabled = false;
                    }
                    else
                    {
                        cmb = true;
                        if (data && cmb)
                        {
                            btnsubmit.Enabled = true;
                        }
                    }
                }
                else if (cmbquality2.Text == "Manufacturer")
                {
                    SqlCommand cmd8 = new SqlCommand("SELECT m_id from manufacturer where name='" + cmbcus.Text.ToUpper() + "'", Login.con);
                    SqlDataReader dr8 = cmd8.ExecuteReader();
                    if (!dr8.HasRows)
                    {
                        dr8.Close();
                        MessageBox.Show("Record Not Found", "AR Tranders", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                        cmbcus.Focus();
                        cmb = false;
                        btnsubmit.Enabled = false;
                    }
                    else
                    {
                        cmb = true;
                        if (data && cmb)
                        {
                            btnsubmit.Enabled = true;
                        }
                    }
                }
                Login.con.Close();
            }
        }

        //private void txtmeter_KeyDown(object sender, KeyEventArgs e)
        //{
        //    if (e.KeyCode == Keys.Enter)
        //    {
        //        if (cmbcode.Text != "" && cmbsize.Text !="")
        //        {
        //            Login.con.Open();
        //            SqlCommand cmd = new SqlCommand("SELECT meters from product where p_id='" + cmbcode.Text + "' AND size='" + cmbsize.Text + "'", Login.con);
        //            Decimal meters = (Decimal)cmd.ExecuteScalar();
        //            Login.con.Close();
        //            txtqty.Text = Convert.ToString(Math.Ceiling(Convert.ToDecimal(txtmeter.Text) / meters));
        //        }
        //        else
        //            MessageBox.Show("Please Enter Code First", "AR Tranders", MessageBoxButtons.OK, MessageBoxIcon.Stop);
        //    }
        //}

        //private void txtmeter_KeyPress(object sender, KeyPressEventArgs e)
        //{
        //    test.decimaldigit_correction(sender, e);
        //}

        //private void txtqty_KeyPress(object sender, KeyPressEventArgs e)
        //{
        //    test.digit_correction(sender,e);
        //}

        //private void txtqty_MouseDown(object sender, MouseEventArgs e)
        //{
        //    txtqty.SelectAll();
        //}

        //private void txtqty_Leave(object sender, EventArgs e)
        //{
        //    if (txtqty.Text == "" || Convert.ToDecimal(txtqty.Text) == 0)
        //        txtqty.Text = "0";
        //}

        //private void txttiles_MouseClick(object sender, MouseEventArgs e)
        //{
        //    txttiles.SelectAll();
        //}

        //private void txttiles_Leave(object sender, EventArgs e)
        //{
        //    if (cmbcode.Text != "" && cmbsize.Text != "")
        //    {
        //        set_qty();
        //    }
        //    else if (txttiles.Text == "")
        //    {
        //        txttiles.Text = "0";
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
        //            cmbtone.Items.Clear();
        //            cmbsize.Items.Clear();
        //            cmbquality.Items.Clear();
        //            Login.con.Open();
        //            SqlCommand cmd = new SqlCommand("SELECT p_name from product where p_id='" + cmbcode.Text + "'", Login.con);
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
        //    string command = "select size from product where p_id='" + cmbcode.Text + "'";
        //    test.cmbox_drop(command, cmbsize, Login.con);
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
        //    if (cmbcode.Text != "" && cmbsize.Text != "")
        //    {
        //        string command = "select distinct quality from purchase_stock_detail where p_id='" + cmbcode.Text + "' AND size='" + cmbsize.Text + "'";
        //        test.cmbox_drop(command, cmbquality, Login.con);
        //    }
        //}

        //private void cmbtone_Enter(object sender, EventArgs e)
        //{
        //    string command = "select DISTINCT tone from purchase_stock_detail where p_id='" + cmbcode.Text + "' AND quality='" + cmbquality.Text + "' AND size='" + cmbsize.Text + "'";
        //    test.cmbox_drop(command, cmbtone, Login.con);
        //}

        private void btnenter_Click(object sender, EventArgs e)
        {
            entercartdata();
        }

        //private void dataGridView_KeyDown(object sender, KeyEventArgs e)
        //{
        //    if (e.KeyCode == Keys.Delete)
        //    {
        //        Login.con.Open();
        //        SqlCommand cmd = new SqlCommand("DELETE from order_cart where P_Code='" + delid + "' AND Quality='" + p_quality + "' AND Tone='" + tone + "'AND Size='"+p_size+"'", Login.con);
        //        cmd.ExecuteNonQuery();
        //        string command = "select P_Code,Tone,Quality,Size,Boxes,Tiles,Meters from order_cart";
        //        test.bindingcode(command, dataGridView, Login.con);
        //        Login.con.Close();
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
        //        SqlCommand cmd2 = new SqlCommand("UPDATE order_cart set Boxes='" + p_qty + "',Tiles='" + p_tiles + "',Meters= '" + (meters_in_box * (p_qty + (p_tiles / Convert.ToDecimal(db_tiles)))) + "' where P_Code='" + p_code + "' AND Quality='" + p_quality + "' AND Tone='" + tone + "'AND Size='" + p_size + "'", Login.con);
        //        cmd2.ExecuteNonQuery();
        //        String command = "select P_Code,Tone,Quality,Size,Boxes,Tiles,Meters from order_cart";
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
        //        String command = "select P_Code,Tone,Quality,Size,Boxes,Tiles,Meters from order_cart";
        //        test.bindingcode(command, dataGridView, Login.con);
        //    }
        //    Login.con.Close();
        //    dataGridView.Columns["P_Code"].ReadOnly = dataGridView.Columns["Tone"].ReadOnly = dataGridView.Columns["Quality"].ReadOnly = dataGridView.Columns["Size"].ReadOnly = dataGridView.Columns["Meters"].ReadOnly = true;
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
        //        if (dataGridView.RowCount == 0)
        //            if (dataGridView2.RowCount == 0)
        //            {
        //                data = false;
        //                btnsubmit.Enabled = false;
        //            }
        //    }
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

        //private void dataGridView_Enter(object sender, EventArgs e)
        //{
        //    dataGridView.Columns["P_Code"].ReadOnly = dataGridView.Columns["Tone"].ReadOnly = dataGridView.Columns["Quality"].ReadOnly = dataGridView.Columns["Size"].ReadOnly = dataGridView.Columns["Meters"].ReadOnly = true;
        //}

        //Add Order Sanitary
        private void ckbx2_CheckedChanged(object sender, EventArgs e)
        {
            //if (ckbx.Checked == false)
            //    ckbx2.Checked = true;
            //if (ckbx2.Checked == true)
            //{
            //}
            //else
            //    txtqty2.Enabled = txtcode.Enabled = cmbname.Enabled = dataGridView2.Enabled = label7.Enabled = label8.Enabled = label9.Enabled = btnenter2.Enabled=false;
            txtqty2.Enabled = txtcode.Enabled = cmbname.Enabled = dataGridView2.Enabled = label7.Enabled = label8.Enabled = label9.Enabled = btnenter2.Enabled = true;
            cmbname.Focus();
            txtqty2.Text = "1";
            txtcode.Text = cmbname.Text = "";
            Login.con.Open();
            SqlCommand cmd2 = new SqlCommand("DELETE from order_cart_s", Login.con);
            cmd2.ExecuteNonQuery();
            Login.con.Close();
            test.bindingcode("SELECT * From order_cart_s", dataGridView2, Login.con);
        }

        private void txtqty2_MouseClick(object sender, MouseEventArgs e)
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

        private void cmbname_Leave(object sender, EventArgs e)
        {
            if (cmbname.Text == "")
            {
                txtcode.Clear();
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
                    txtcode.Clear();
                    cmbname.Focus();
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
                    txtcode.Clear();
                    cmbname.Focus();
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

        private void dataGridView2_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = this.dataGridView2.Rows[e.RowIndex];
                delid = row.Cells[0].Value.ToString();
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

        private void txttiles_KeyPress(object sender, KeyPressEventArgs e)
        {
            test.digit_correction(sender, e);
        }

        private void dataGridView2_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Delete)
            {
                Login.con.Open();
                SqlCommand cmd = new SqlCommand("DELETE from order_cart_s where P_Code='" + delid + "' ", Login.con);
                cmd.ExecuteNonQuery();
                string command = "SELECT * from order_cart_s";
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
                SqlCommand cmd2 = new SqlCommand("UPDATE order_cart_s set Quantity='" + p_qty + "'where P_Code='" + p_code + "'", Login.con);
                cmd2.ExecuteNonQuery();
                String command = "select * from order_cart_s";
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
                String command = "select * from order_cart_s";
                test.bindingcode(command, dataGridView2, Login.con);
            }
            Login.con.Close();
            dataGridView2.Columns["P_Code"].ReadOnly = dataGridView2.Columns["P_Name"].ReadOnly = true;
        }

        private void btnsubmit_Click(object sender, EventArgs e)
        {
            Login.con.Open();
            if (cmbquality2.Text == "Customer" || cmbquality2.Text == "Stakeholder")
            {
                SqlCommand cmd8 = new SqlCommand("SELECT c_id from customer where c_name='" + cmbcus.Text.ToUpper() + "' AND type='" + type + "'", Login.con);
                SqlDataReader dr8 = cmd8.ExecuteReader();
                if (dr8.HasRows)
                {
                    while (dr8.Read())
                    {
                        c_id = Convert.ToDecimal(dr8[0]);
                    }
                }
                dr8.Close();
            }
            else if (cmbquality2.Text == "Manufacturer")
            {
                SqlCommand cmd8 = new SqlCommand("SELECT m_id from manufacturer where name='" + cmbcus.Text.ToUpper() + "'", Login.con);
                SqlDataReader dr8 = cmd8.ExecuteReader();
                if (dr8.HasRows)
                {
                    while (dr8.Read())
                    {
                        c_id = Convert.ToDecimal(dr8[0]);
                    }
                }
                dr8.Close();
            }
            if (cmbquality2.Text == "Customer" || cmbquality2.Text == "Stakeholder")
            {
                SqlCommand cmd6 = new SqlCommand("INSERT into order_stock(c_id,datetime,status) values('" + c_id + "','" + DateTime.Now + "', 'Wait')", Login.con);
                cmd6.ExecuteNonQuery();
            }
            else if (cmbquality2.Text == "Manufacturer")
            {
                SqlCommand cmd6 = new SqlCommand("INSERT into order_stock(m_id,datetime,status) values('" + c_id + "','" + DateTime.Now + "', 'Wait')", Login.con);
                cmd6.ExecuteNonQuery();
            }
            SqlCommand cmd7 = new SqlCommand("SELECT o_id from order_stock where datetime= (select max(datetime) from order_stock) ", Login.con);
            SqlDataReader dr7 = cmd7.ExecuteReader();
            while (dr7.Read())
            {
                o_id = Convert.ToDecimal(dr7[0]);
            }
            dr7.Close();
            //if (ckbx.Checked == true)
            //{
            //    if (dataGridView.Rows.Count != 0)
            //    {
            //        while (true)
            //        {
            //            SqlCommand cmd8 = new SqlCommand("SELECT Boxes,P_Code,Quality,Tone,Size,Tiles from order_cart", Login.con);
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
            //                    SqlCommand cmd9 = new SqlCommand("INSERT into order_detail values('" + o_id + "','" + p_code + "','" + p_qty + "', '" + p_quality + "', '" + tone + "','" + p_size + "','" + p_tiles + "')", Login.con);
            //                    cmd9.ExecuteNonQuery();
            //                    SqlCommand cmd10 = new SqlCommand("DELETE order_cart where P_Code='" + p_code + "' AND Quality='" + p_quality + "' AND Tone='" + tone + "' AND Size='" + p_size + "'", Login.con);
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
                    SqlCommand cmd4 = new SqlCommand("SELECT * from order_cart_s", Login.con);
                    SqlDataReader dr4 = cmd4.ExecuteReader();
                    if (dr4.HasRows)
                    {
                        if (dr4.Read())
                        {
                            p_qty = Convert.ToDecimal(dr4[2]);
                            p_code = Convert.ToString(dr4[0]);
                            dr4.Close();
                            SqlCommand cmd5 = new SqlCommand("INSERT into order_detail2 values('" + o_id + "','" + p_code + "','" + p_qty + "')", Login.con);
                            cmd5.ExecuteNonQuery();
                            SqlCommand cmd8 = new SqlCommand("DELETE order_cart_s where P_Code='" + p_code + "'", Login.con);
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
            //}
            Login.con.Close();
            DialogResult dresult = MessageBox.Show("Press 'OK' to Print Gate Pass", "AR Tranders", MessageBoxButtons.OKCancel, MessageBoxIcon.Question);
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
            reset_cart();
        }

        private void btnclear_Click(object sender, EventArgs e)
        {
            reset_cart();
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
            Login.con.Open();
            SqlCommand cmd = new SqlCommand("SELECT TOP 1 o_id FROM order_stock ORDER BY o_id DESC", Login.con);
            e.Graphics.DrawString(cmd.ExecuteScalar().ToString(), new Font("Calibri", 12, FontStyle.Bold), new SolidBrush(Color.Black), startX + 95, startY + Offset);
            Login.con.Close();
            Offset = Offset + 30;
            e.Graphics.DrawString("Customer Name:", new Font("Calibri", 10, FontStyle.Bold), new SolidBrush(Color.Black), startX, startY + Offset);
            e.Graphics.DrawString(cmbcus.Text, new Font("Calibri", 10), new SolidBrush(Color.Black), startX + 105, startY + Offset);
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
                    e.Graphics.DrawString(Convert.ToString(dataGridView2.Rows[j].Cells[1].Value), new Font("Calibri", 9), new SolidBrush(Color.Black), startX + 45, startY + Offset);
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

        //View Order
        private void avilableStockToolStripMenuItem_Click(object sender, EventArgs e)
        {
            panelavailorder.Visible = true;
            cmbavailname.Text = "";
            comboBox1.SelectedIndex = 0;
            comboBox1.Focus();
            string command2 = "select o.o_id Order_ID, case when c.type='FIXED' then 'Customer' else 'Stakeholder' end as AC_Type ,c.c_name AC_Name, o.datetime DateTime From order_stock o, customer c Where o.c_id=c.c_id UNION ALL select o.o_id Order_ID, 'Manufacturer' AC_Type ,c.name AC_Name, o.datetime DateTime From order_stock o, manufacturer c Where o.m_id=c.m_id";
            test.bindingcode(command2, dataGridView3, Login.con);
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            cmbavailname.Text = "";
        }

        private void cmbavailname_Enter(object sender, EventArgs e)
        {
            if (comboBox1.Text == "Customer" || comboBox1.Text == "Stakeholder")
            {
                if (comboBox1.Text == "Customer")
                    type = "FIXED";
                else if (comboBox1.Text == "Stakeholder")
                    type = "STAKEHOLDER";
                string command = "select c_name from customer where type='" + type + "' order by c_name";
                test.cmbox_drop(command, cmbavailname, Login.con);
            }
            else if (comboBox1.Text == "Manufacturer")
            {
                string command = "select name from manufacturer order by name";
                test.cmbox_drop(command, cmbavailname, Login.con);
            }
        }

        private void cmbavailname_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                cmbavailname.Text = cmbavailname.Text.ToUpper();
                cmbavailname.SelectAll();
                Login.con.Open();
                if (comboBox1.Text == "Customer" || comboBox1.Text == "Stakeholder")
                {
                    SqlCommand cmd8 = new SqlCommand("SELECT c_id from customer where c_name='" + cmbavailname.Text.ToUpper() + "' AND type='" + type + "'", Login.con);
                    SqlDataReader dr8 = cmd8.ExecuteReader();
                    if (dr8.HasRows)
                    {
                        while (dr8.Read())
                        {
                            c_id = Convert.ToDecimal(dr8[0]);
                        }
                    }
                    dr8.Close();
                }
                else if (comboBox1.Text == "Manufacturer")
                {
                    SqlCommand cmd8 = new SqlCommand("SELECT m_id from manufacturer where name='" + cmbavailname.Text.ToUpper() + "'", Login.con);
                    SqlDataReader dr8 = cmd8.ExecuteReader();
                    if (dr8.HasRows)
                    {
                        while (dr8.Read())
                        {
                            c_id = Convert.ToDecimal(dr8[0]);
                        }
                    }
                    dr8.Close();
                }
                Login.con.Close();
                string command2 = "select o.o_id Order_ID, '" + cmbquality2.Text + "' AC_Type ,c.c_name AC_Name, o.datetime DateTime From order_stock o, customer c Where o.c_id=c.c_id AND o.c_id='" + c_id + "' UNION ALL select o.o_id Order_ID, 'Manufacturer' AC_Type ,c.name AC_Name, o.datetime DateTime From order_stock o, manufacturer c Where o.m_id=c.m_id AND o.m_id='" + c_id + "'";
                test.bindingcode(command2, dataGridView3, Login.con);
            }
        }

        private void dataGridView3_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = this.dataGridView3.Rows[e.RowIndex];
                o_no = Convert.ToDecimal(row.Cells["Order_ID"].Value.ToString());
                c_name = row.Cells["AC_Name"].Value.ToString();
                delid = row.Cells["Order_ID"].Value.ToString();
                btndetail.Enabled = true;
                btndel.Enabled = true;
            }
        }

        private void btndel_Click(object sender, EventArgs e)
        {
            if (delid != "")
            {
                DialogResult dresult = MessageBox.Show("Are you sure you want to delete this Gate Pass", "AR Tranders", MessageBoxButtons.OKCancel, MessageBoxIcon.Question);
                if (dresult == DialogResult.OK)
                {
                    Login.con.Open();
                    SqlCommand cmd = new SqlCommand("select o_id from order_detail where o_id='" + delid + "'", Login.con);
                    SqlDataReader dr = cmd.ExecuteReader();
                    if (dr.HasRows)
                    {
                        dr.Close();
                        SqlCommand cmd2 = new SqlCommand("DELETE from order_detail where o_id='" + delid + "'", Login.con);
                        cmd2.ExecuteNonQuery();
                    }
                    dr.Close();
                    SqlCommand cmd1 = new SqlCommand("select o_id from order_detail2 where o_id='" + delid + "'", Login.con);
                    SqlDataReader dr1 = cmd1.ExecuteReader();
                    if (dr1.HasRows)
                    {
                        dr1.Close();
                        SqlCommand cmd2 = new SqlCommand("DELETE from order_detail2 where o_id='" + delid + "'", Login.con);
                        cmd2.ExecuteNonQuery();
                    }
                    dr1.Close();
                    SqlCommand cmd3 = new SqlCommand("DELETE from order_stock where o_id='" + delid + "'", Login.con);
                    cmd3.ExecuteNonQuery();
                    delid = "";
                    Login.con.Close();
                    MessageBox.Show("Gate Pass Deleted Successfully", "AR Tranders", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    avilableStockToolStripMenuItem_Click(this, null);
                }
            }
        }

        private void btndetail_Click(object sender, EventArgs e)
        {
            order_detail f = new order_detail(o_no, c_name);
            f.Show();
        }
    }
}
