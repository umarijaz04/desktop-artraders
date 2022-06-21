using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.IO;

namespace AhmadSanitary
{
    public partial class Products : Form
    {
        Stock opener;
        String delid = "";
        UF.Class1 test = new UF.Class1();

        public Products(Stock form)
        {
            InitializeComponent();
            opener = form;
        }

        private void Menu_Load(object sender, EventArgs e)
        {
            opener.Enabled = false;
            //rdbtn_CheckedChanged(this, null);
            //Set AutoGenerateColumns False.
            dataGridView2.AutoGenerateColumns = false;

            //Set Columns Count.
            dataGridView2.ColumnCount = 2;

            //Add a Image Column to the DataGridView at the third position.
            DataGridViewImageColumn imageColumn = new DataGridViewImageColumn();
            imageColumn.Name = "Image";
            imageColumn.DataPropertyName = "Image";
            imageColumn.HeaderText = "Image";
            imageColumn.ImageLayout = DataGridViewImageCellLayout.Normal;
            dataGridView2.Columns.Insert(0, imageColumn);
            dataGridView2.RowTemplate.Height = 100;
            dataGridView2.Columns[0].Width = 100;

            ////Add Columns.
            dataGridView2.Columns[1].Name = "P_Code";
            dataGridView2.Columns[1].DataPropertyName = "P_Code";
            dataGridView2.Columns[1].HeaderText = "P_Code";

            dataGridView2.Columns[2].HeaderText = "P_Name";
            dataGridView2.Columns[2].Name = "P_Name";
            dataGridView2.Columns[2].DataPropertyName = "P_Name";
        }

        private void Menu_FormClosing(object sender, FormClosingEventArgs e)
        {
            opener.Enabled = true;
        }

        //Add Product Tiles
        private void addToolStripMenuItem_Click(object sender, EventArgs e)
        {
            panelupdate.Visible = panelview.Visible = false;
            //rdbtn.Checked = true;
            rdbtn2_CheckedChanged(this, null);
        }

        //private void rdbtn_CheckedChanged(object sender, EventArgs e)
        //{
        //    if (rdbtn.Checked == true)
        //    {
        //        txtcodeadd2.Enabled = txtnameadd2.Enabled = label18.Enabled = label17.Enabled = false;              
        //        txtcodeadd.Focus();
        //    }
        //    else
        //        txtcodeadd2.Enabled = txtnameadd2.Enabled = label18.Enabled = label17.Enabled = true;
        //    txtcodeadd.Text = txtnameadd.Text = txtsize.Text = txtmtr.Text = txtpieces.Text = "";
        //    cmbfinish.SelectedIndex = 0;
        //}

        //private void txtnameaddproduct_KeyPress(object sender, KeyPressEventArgs e)
        //{
        //    test.name_correction(txtnameadd, e);
        //}

        //private void txtsize_KeyPress(object sender, KeyPressEventArgs e)
        //{
        //    test.xdigit_correction(sender, e);
        //}

        //private void txtmeters_KeyPress(object sender, KeyPressEventArgs e)
        //{
        //    test.decimaldigit_correction(sender, e);
        //}

        //private void txtpieces_KeyPress(object sender, KeyPressEventArgs e)
        //{
        //    test.digit_correction(sender, e);
        //}

        //private void txtpieces_KeyDown(object sender, KeyEventArgs e)
        //{
        //    if (e.KeyCode == Keys.Enter)
        //    {
        //        btnadd_Click(this, e);
        //    }
        //}

        //Add Product Sanitary
        private void rdbtn2_CheckedChanged(object sender, EventArgs e)
        {
            //if (rdbtn2.Checked == true)
            //{
            //    txtcodeadd.Enabled = txtnameadd.Enabled = cmbfinish.Enabled = txtsize.Enabled = txtmtr.Enabled = txtpieces.Enabled = label1.Enabled = label2.Enabled = label3.Enabled = label4.Enabled = label5.Enabled = label12.Enabled = false;
            txtcodeadd2.Focus();
            //}
            //else
            //    txtcodeadd.Enabled = txtnameadd.Enabled = cmbfinish.Enabled = txtsize.Enabled = txtmtr.Enabled = txtpieces.Enabled = label1.Enabled = label2.Enabled = label3.Enabled = label4.Enabled = label5.Enabled = label12.Enabled = true;
            txtcodeadd2.Text = txtnameadd2.Text = "";
            setPath = null;
            pictureBox1.Image = null;
        }

        private void txtnameadd2_KeyPress(object sender, KeyPressEventArgs e)
        {
            test.name_correction(txtnameadd2, e);
        }

        private void txtnameadd2_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                btnadd_Click(this, null);
            }
        }

        private void btnadd_Click(object sender, EventArgs e)
        {
            //if (rdbtn.Checked == true)
            //{
            //    if (txtcodeadd.Text != "" && txtnameadd.Text != "" && txtsize.Text != "" && txtmtr.Text != "" && txtpieces.Text != "" && cmbfinish.Text != "---Select Option---")
            //    {
            //        Login.con.Open();
            //        SqlCommand cmd = new SqlCommand("SELECT p_id from product where p_id='" + txtcodeadd.Text.ToUpper() + "' AND size='" + txtsize.Text + "'", Login.con);
            //        SqlDataReader dr = cmd.ExecuteReader();
            //        if (dr.HasRows)
            //        {
            //            MessageBox.Show("This Product is Already Exist", "AR Tranders", MessageBoxButtons.OK, MessageBoxIcon.Stop);
            //            txtsize.Clear();
            //            txtcodeadd.Clear();
            //            txtcodeadd.Focus();
            //            dr.Close();
            //        }
            //        else
            //        {
            //            dr.Close();
            //            SqlCommand cmd2 = new SqlCommand("INSERT into product (p_id,p_name,size,meters,finish,pieces) values('" + txtcodeadd.Text.ToUpper() + "','" + txtnameadd.Text.ToUpper() + "','" + txtsize.Text + "','" + Convert.ToDecimal(txtmtr.Text) + "','" + cmbfinish.Text + "','" + Convert.ToDecimal(txtpieces.Text) + "')", Login.con);
            //            cmd2.ExecuteNonQuery();
            //            MessageBox.Show("Product Inserted Successfully", "AR Tranders", MessageBoxButtons.OK, MessageBoxIcon.Information);
            //            rdbtn_CheckedChanged(this, null);
            //        }
            //        Login.con.Close();
            //    }
            //}
            //else
            //{
            if (txtcodeadd2.Text != "" && txtnameadd2.Text != "")
            {
                Login.con.Open();
                SqlCommand cmd = new SqlCommand("SELECT * from product2 where p_id='" + txtcodeadd2.Text.ToUpper() + "' OR p_name='" + txtnameadd2.Text.ToUpper() + "'", Login.con);
                SqlDataReader dr = cmd.ExecuteReader();
                if (dr.HasRows)
                {
                    MessageBox.Show("This Product is Already Exist", "AR Tranders", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                    txtnameadd2.Text = txtcodeadd2.Text = "";
                    txtcodeadd2.Focus();
                    dr.Close();
                }
                else
                {
                    dr.Close();
                    SqlCommand cmd2 = new SqlCommand("INSERT into product2 (p_id,p_name,path) values('" + txtcodeadd2.Text.ToUpper() + "','" + txtnameadd2.Text.ToUpper() + "', '" + setPath + "')", Login.con);
                    cmd2.ExecuteNonQuery();
                    MessageBox.Show("Product Inserted Successfully", "AR Tranders", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    rdbtn2_CheckedChanged(this, null);
                }
                Login.con.Close();
            }
            //}
        }

        //Update Product Tiles
        private void updateToolStripMenuItem_Click(object sender, EventArgs e)
        {
            panelupdate.Visible = true;
            panelview.Visible = false;
            //rdbtnup.Checked = true;
            rdbtnup2_CheckedChanged(this, null);
        }

        //private void rdbtnup_CheckedChanged(object sender, EventArgs e)
        //{
        //    if (rdbtnup.Checked == true)
        //    {
        //        txtcodeupdate2.Enabled = txtnameupdate2.Enabled = label20.Enabled = label19.Enabled = false;
        //        txtcodeupdate.Enabled = txtnameupdate.Enabled = cmbfinish2.Enabled = txtsize2.Enabled = txtmtr2.Enabled = txtpieces2.Enabled = false;
        //        txtsr.Focus();
        //    }
        //    else
        //        txtcodeupdate2.Enabled = txtnameupdate2.Enabled = label20.Enabled = label19.Enabled = true;
        //    txtsr.Text =txtcodeupdate.Text = txtnameupdate.Text = txtsize2.Text = txtmtr2.Text = txtpieces2.Text = "";
        //    cmbfinish2.SelectedIndex = 0;
        //}

        //private void txtsr_KeyDown(object sender, KeyEventArgs e)
        //{
        //    if (e.KeyCode == Keys.Enter)
        //    {
        //        if (txtsr.Text != "")
        //        {
        //            txtsr.SelectAll();
        //            Login.con.Open();
        //            SqlCommand cmd = new SqlCommand("SELECT * from product where sr='" + txtsr.Text + "'", Login.con);
        //            SqlDataReader dr = cmd.ExecuteReader();
        //            if (dr.HasRows)
        //            {
        //                while (dr.Read())
        //                {
        //                    txtnameupdate.Enabled = cmbfinish2.Enabled = txtsize2.Enabled = txtmtr2.Enabled = txtcodeupdate.Enabled = txtpieces2.Enabled = true;
        //                    txtnameupdate.Text = Convert.ToString(dr[1]);
        //                    txtcodeupdate.Text = Convert.ToString(dr[0]);
        //                    txtsize2.Text = Convert.ToString(dr[3]);
        //                    txtmtr2.Text = Convert.ToString(dr[4]);
        //                    cmbfinish2.Text = Convert.ToString(dr[2]);
        //                    txtpieces2.Text = Convert.ToString(dr[6]);
        //                }
        //            }
        //            else
        //            {
        //                dr.Close();
        //                txtnameupdate.Enabled = cmbfinish2.Enabled = txtsize2.Enabled = txtmtr2.Enabled = txtcodeupdate.Enabled = txtpieces2.Enabled = false;
        //            }
        //            dr.Close();
        //            Login.con.Close();
        //        }
        //    }
        //}

        //private void txtpieces2_KeyDown(object sender, KeyEventArgs e)
        //{
        //    if(e.KeyCode == Keys.Enter)
        //    {
        //        btnupdate_Click(this, null);
        //    }
        //}

        //private void txtsr_KeyPress(object sender, KeyPressEventArgs e)
        //{
        //    test.digit_correction(sender, e);
        //}

        //private void txtnameupdate_KeyPress(object sender, KeyPressEventArgs e)
        //{
        //    test.name_correction(txtnameupdate, e);
        //}

        //private void txtsize2_KeyPress(object sender, KeyPressEventArgs e)
        //{
        //    test.xdigit_correction(sender, e);
        //}

        //private void txtmtr2_KeyPress(object sender, KeyPressEventArgs e)
        //{
        //    test.decimaldigit_correction(sender, e);
        //}

        //private void txtpieces2_KeyPress(object sender, KeyPressEventArgs e)
        //{
        //    test.digit_correction(sender, e);
        //}

        //Update Product Sanitary
        private void rdbtnup2_CheckedChanged(object sender, EventArgs e)
        {
            //if (rdbtnup2.Checked == true)
            //{
            //    txtsr.Enabled = txtcodeupdate.Enabled = txtnameupdate.Enabled = cmbfinish2.Enabled = txtsize2.Enabled = txtmtr2.Enabled = txtpieces2.Enabled = label11.Enabled = label14.Enabled = label13.Enabled = label6.Enabled = label9.Enabled = label10.Enabled = label15.Enabled = false;
            txtcodeupdate2.Focus();
            //}
            //else
            //    txtsr.Enabled = txtcodeupdate.Enabled = txtnameupdate.Enabled = cmbfinish2.Enabled = txtsize2.Enabled = txtmtr2.Enabled = txtpieces2.Enabled = label11.Enabled = label14.Enabled = label13.Enabled = label6.Enabled = label9.Enabled = label10.Enabled = label15.Enabled = true;
            txtcodeupdate2.Text = txtnameupdate2.Text = "";
            setPath = null;
            pictureBox2.Image = null;
        }

        private void txtcodeupdate2_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                if (txtcodeupdate2.Text != "")
                {
                    txtcodeupdate2.SelectAll();
                    txtnameupdate2.Text = "";
                    Login.con.Open();
                    SqlCommand cmd = new SqlCommand("SELECT * from product2 where p_id='" + txtcodeupdate2.Text.ToUpper() + "'", Login.con);
                    SqlDataReader dr = cmd.ExecuteReader();
                    if (dr.HasRows)
                    {
                        while (dr.Read())
                        {
                            txtnameupdate2.Enabled = true;
                            txtnameupdate2.Text = Convert.ToString(dr[1]);
                            pictureBox2.ImageLocation = Convert.ToString(dr[2]);
                            setPath = Convert.ToString(dr[2]);
                        }
                    }
                    else
                    {
                        dr.Close();
                        txtnameupdate2.Enabled = false;
                    }
                    dr.Close();
                    Login.con.Close();
                }
            }
        }

        private void txtnameupdate2_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                btnupdate_Click(this, null);
            }
        }

        private void btnupdate_Click(object sender, EventArgs e)
        {
            //if (rdbtnup.Checked == true)
            //{
            //    if (txtcodeupdate.Text != "" && txtnameupdate.Text != "" && txtsize2.Text != "" && txtmtr2.Text != "" && txtpieces2.Text != "")
            //    {
            //        Boolean flag = true;
            //        Login.con.Open();
            //        SqlCommand cmd = new SqlCommand("Select * from product where sr <> '" + Convert.ToDecimal(txtsr.Text) + "'", Login.con);
            //        SqlDataReader dr = cmd.ExecuteReader();
            //        if (dr.HasRows)
            //        {
            //            while (dr.Read())
            //            {
            //                if ((txtcodeupdate.Text.ToUpper() == Convert.ToString(dr[0])) && (txtsize2.Text == Convert.ToString(dr[3])))
            //                {
            //                    flag = false;
            //                    dr.Close();
            //                    MessageBox.Show("This Product is Already Exist", "AR Tranders", MessageBoxButtons.OK, MessageBoxIcon.Stop);
            //                    break;
            //                }
            //            }
            //        }
            //        if (flag)
            //        {
            //            dr.Close();
            //            try
            //            {
            //                SqlCommand cmd2 = new SqlCommand("UPDATE product set p_id='" + txtcodeupdate.Text.ToUpper() + "',  p_name= '" + txtnameupdate.Text.ToUpper() + "', size= '" + txtsize2.Text + "', meters= '" + txtmtr2.Text + "', finish= '" + cmbfinish2.Text + "', pieces='" + txtpieces2.Text + "' where sr='" + txtsr.Text + "'", Login.con);
            //                cmd2.ExecuteNonQuery();
            //                MessageBox.Show("Product Updated Successfully", "AR Tranders", MessageBoxButtons.OK, MessageBoxIcon.Information);
            //                rdbtnup_CheckedChanged(this, null);
            //            }
            //            catch (Exception)
            //            {
            //                MessageBox.Show("This Product is Already Used in Purchases Stock", "AR Tranders", MessageBoxButtons.OK, MessageBoxIcon.Stop);
            //            }
            //        }
            //        Login.con.Close();
            //    }
            //}
            //else
            //{
            if (txtcodeupdate2.Text != "" && txtnameupdate2.Text != "")
            {
                Login.con.Open();
                Boolean flag = true;
                SqlCommand cmd = new SqlCommand("Select * from product2 where p_id <> '" + txtcodeupdate2.Text.ToUpper() + "'", Login.con);
                SqlDataReader dr = cmd.ExecuteReader();
                if (dr.HasRows)
                {
                    while (dr.Read())
                    {
                        if ((txtnameupdate2.Text.ToUpper() == Convert.ToString(dr[1])))
                        {
                            flag = false;
                            dr.Close();
                            MessageBox.Show("This Product is Already Exist", "AR Tranders", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                            break;
                        }
                    }
                }
                if (flag)
                {
                    dr.Close();
                    try
                    {
                        SqlCommand cmd2 = new SqlCommand("UPDATE product2 set  p_name= '" + txtnameupdate2.Text.ToUpper() + "', path= '" + setPath + "' where p_id='" + txtcodeupdate2.Text.ToUpper() + "'", Login.con);
                        cmd2.ExecuteNonQuery();
                        MessageBox.Show("Product Updated Successfully", "AR Tranders", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        rdbtnup2_CheckedChanged(this, null);
                    }
                    catch (Exception)
                    {
                        MessageBox.Show("This Product is Already Used in Purchases Stock", "AR Tranders", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                    }
                }
                Login.con.Close();
            }
            //}
        }

        //Delete Product
        private void viewToolStripMenuItem_Click(object sender, EventArgs e)
        {
            panelupdate.Visible = false;
            panelview.Visible = true;
            //rdbtnview.Checked = true;
            rdbtnview2_CheckedChanged(this, null);
        }

        //private void rdbtnview_CheckedChanged(object sender, EventArgs e)
        //{
        //    if (rdbtnview.Checked == true)
        //    {
        //        dataGridView2.Enabled = false;
        //        dataGridView.DataSource = null;
        //        string command = "SELECT sr Sr#, p_id Code, p_name Name,size Size,meters Meters,finish Finish,pieces Pieces from product";
        //        test.bindingcode(command, dataGridView, Login.con);
        //    }
        //    else
        //        dataGridView2.Enabled = true;
        //}

        private void rdbtnview2_CheckedChanged(object sender, EventArgs e)
        {
            //if (rdbtnview2.Checked == true)
            //{
            //dataGridView.Enabled = false;
            dataGridView2.DataSource = null;
            //string command2 = "SELECT p_id ID, p_name Name from product2 order by p_name";
            //test.bindingcode(command2, dataGridView2, Login.con);
            //}
            //else
            //    dataGridView.Enabled = true;
            //Bind DataGridView.
            this.BindDataGridView();
        }

        private void BindDataGridView()
        {

            using (SqlDataAdapter sda = new SqlDataAdapter("SELECT path, p_id, p_name FROM product2 order by p_name", Login.con))
            {
                DataTable dt = new DataTable();
                sda.Fill(dt);

                //Add a new Byte[] Column.
                dt.Columns.Add("Image", Type.GetType("System.Byte[]"));
                dt.Columns.Add("P_Code", Type.GetType("System.String"));
                dt.Columns.Add("P_Name", Type.GetType("System.String"));

                //Convert all Images to Byte[] and copy to DataTable.
                foreach (DataRow row in dt.Rows)
                {
                    if (row["path"].ToString() != "")
                        row["Image"] = File.ReadAllBytes(row["path"].ToString());
                    row["P_Code"] = row["p_id"].ToString();
                    row["P_Name"] = row["p_name"].ToString();
                }

                dataGridView2.DataSource = dt;
            }

        }

        private void btndel_Click(object sender, EventArgs e)
        {
            //if (rdbtnview.Checked == true)
            //{
            //    if (delid != "")
            //    {
            //        DialogResult dresult = MessageBox.Show("Are you sure you want to delete this Product", "AR Tranders", MessageBoxButtons.OKCancel, MessageBoxIcon.Question);
            //        if (dresult == DialogResult.OK)
            //        {
            //            try
            //            {
            //                Login.con.Open();
            //                SqlCommand cmd = new SqlCommand("DELETE from product where sr='" + delid + "'", Login.con);
            //                cmd.ExecuteNonQuery();
            //                rdbtnview_CheckedChanged(this, null);
            //                delid = "";
            //                MessageBox.Show("Product Deleted Successfully", "AR Tranders", MessageBoxButtons.OK, MessageBoxIcon.Information);
            //            }
            //            catch (Exception)
            //            {
            //                MessageBox.Show("This Product is Already Used in Purchases Stock", "AR Tranders", MessageBoxButtons.OK, MessageBoxIcon.Stop);
            //                delid = "";
            //            }
            //            finally
            //            {
            //                Login.con.Close();
            //            }
            //        }
            //    }
            //}
            //else
            //{
            if (delid != "")
            {
                DialogResult dresult = MessageBox.Show("Are you sure you want to delete this Product", "AR Tranders", MessageBoxButtons.OKCancel, MessageBoxIcon.Question);
                if (dresult == DialogResult.OK)
                {
                    try
                    {
                        Login.con.Open();
                        SqlCommand cmd = new SqlCommand("DELETE from product2 where p_id='" + delid + "'", Login.con);
                        cmd.ExecuteNonQuery();
                        rdbtnview2_CheckedChanged(this, null);
                        delid = "";
                        MessageBox.Show("Product Deleted Successfully", "AR Tranders", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    catch (Exception)
                    {
                        MessageBox.Show("This Product is Already Used in Purchases Stock", "AR Tranders", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                        delid = "";
                    }
                    finally
                    {
                        Login.con.Close();
                    }
                }
            }
            //}
        }

        //private void dataGridView_CellClick(object sender, DataGridViewCellEventArgs e)
        //{
        //    if (e.RowIndex >= 0)
        //    {
        //        DataGridViewRow row = this.dataGridView.Rows[e.RowIndex];
        //        delid = row.Cells[0].Value.ToString();
        //    }
        //}

        private void dataGridView2_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = this.dataGridView2.Rows[e.RowIndex];
                delid = row.Cells[1].Value.ToString();
            }
        }

        string setPath;
        private void btnAddPic_Click(object sender, EventArgs e)
        {
            Image File;
            OpenFileDialog f = new OpenFileDialog();
            f.Filter = "Image files (*.jpg, *.png) | *.jpg; *.png";

            if (f.ShowDialog() == DialogResult.OK)
            {
                File = Image.FromFile(f.FileName);
                pictureBox1.Image = File;
                pictureBox1.Image.Save("C:\\Images\\" + f.SafeFileName);
                setPath = @"C:\Images\" + f.SafeFileName;
            }
        }

        private void btnUpdatePic_Click(object sender, EventArgs e)
        {
            Image File;
            OpenFileDialog f = new OpenFileDialog();
            f.Filter = "Image files (*.jpg, *.png) | *.jpg; *.png";

            if (f.ShowDialog() == DialogResult.OK)
            {
                File = Image.FromFile(f.FileName);
                pictureBox2.Image = File;
                pictureBox2.Image.Save("C:\\Images\\" + f.SafeFileName);
                setPath = @"C:\Images\" + f.SafeFileName;
            }
        }
    }
}
