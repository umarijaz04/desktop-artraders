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
    public partial class Breakage_Items_Detail : Form
    {
        Int32 db_tiles = 0;
        Decimal b_id, p_tiles, dc_id, purchase_qty, breakage_qty, p_qty;
        String p_code, p_quality, p_size, tone;
        UF.Class1 test = new UF.Class1();

        public Breakage_Items_Detail(Decimal b_no, Decimal dc_no)
        {
            InitializeComponent();
            b_id = b_no;
            dc_id = dc_no;
        }

        private void order_detail_Load(object sender, EventArgs e)
        {
            string command = "select bd.p_id P_Code, bd.tone Tone, bd.quality Quality, bd.size Size, bd.qty Boxes,bd.tiles Tiles, CAST(ROUND((p.meters*((bd.tiles / p.pieces) + bd.qty)),2) as decimal(18,2)) Meters From breakage_detail bd, product p where bd.p_id=p.p_id AND bd.size=p.size AND bd.b_id='" + b_id + "'";
            //test.bindingcode(command, dataGridView, Login.con);
            string command2 = "select bd.p_id P_Code,p.p_name P_Name ,bd.qty Quantity From breakage_detail2 bd, product2 p where bd.b_id='" + b_id + "' AND bd.p_id=p.p_id ";
            test.bindingcode(command2, dataGridView2, Login.con);
            //dataGridView.Columns["P_Code"].ReadOnly = dataGridView.Columns["Tone"].ReadOnly = dataGridView.Columns["Quality"].ReadOnly = dataGridView.Columns["Size"].ReadOnly = dataGridView.Columns["Meters"].ReadOnly = true;
            dataGridView2.Columns["P_Code"].ReadOnly = dataGridView2.Columns["P_Name"].ReadOnly = true;
        }

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
        //        SqlCommand cmd121 = new SqlCommand("SELECT isnull(pieces,0) from product where p_id = '" + p_code + "' AND size='" + p_size + "' ", Login.con);
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
        //    SqlCommand cmd3 = new SqlCommand("select isnull(sum(bd.qty),0),isnull(sum(bd.tiles),0) from breakage_detail bd,breakage_stock bs where bd.b_id=bs.b_id AND bs.ps_id <>'" + dc_id + "' AND bd.p_id ='" + p_code + "'  AND bd.quality ='" + p_quality + "' AND bd.tone ='" + tone + "' AND bd.size ='" + p_size + "' ", Login.con);
        //    SqlDataReader dr3 = cmd3.ExecuteReader();
        //    if (dr3.HasRows)
        //    {
        //        while (dr3.Read())
        //        {
        //            breakage_qty = Convert.ToDecimal(dr3[0]) + (Convert.ToDecimal(dr3[1]) / Convert.ToDecimal(db_tiles));
        //        }
        //    }
        //    else
        //    {
        //        breakage_qty = 0;
        //    }
        //    dr3.Close();
        //    SqlCommand cmd4 = new SqlCommand("select qty,tiles from purchase_stock_detail where p_id='" + p_code + "' AND ps_id='" + dc_id + "' AND quality='" + p_quality + "' AND tone='" + tone + "' AND size='" + p_size + "' ", Login.con);
        //    SqlDataReader dr4 = cmd4.ExecuteReader();
        //    if (dr4.HasRows)
        //    {
        //        while (dr4.Read())
        //        {
        //            purchase_qty = Convert.ToDecimal(dr4[0]) + (Convert.ToDecimal(dr4[1]) / Convert.ToDecimal(db_tiles));
        //        }
        //    }
        //    else
        //    {
        //        purchase_qty = 0;
        //    }
        //    dr4.Close();
        //    if (((p_qty + (p_tiles / Convert.ToDecimal(db_tiles))) < (purchase_qty - breakage_qty)) || ((p_qty + (p_tiles / Convert.ToDecimal(db_tiles))) == (purchase_qty - breakage_qty)))
        //    {
        //        SqlCommand cmd1 = new SqlCommand("update breakage_detail set qty='" + p_qty + "',tiles='" + p_tiles + "' where b_id='" + b_id + "' AND p_id='" + p_code + "' AND quality='" + p_quality + "' AND tone='" + tone + "'AND size='" + p_size + "'", Login.con);
        //        cmd1.ExecuteNonQuery();
        //        string command = "select bd.p_id P_Code, bd.tone Tone, bd.quality Quality, bd.size Size, bd.qty Boxes,bd.tiles Tiles, CAST(ROUND((p.meters*(bd.qty + (bd.tiles / p.pieces))),2)as decimal(18,2)) Meters From breakage_detail bd, product p where bd.p_id=p.p_id AND bd.size=p.size AND bd.b_id='" + b_id + "'";
        //        test.bindingcode(command, dataGridView, Login.con);
        //    }
        //    else
        //    {
        //        MessageBox.Show("You Have Not Enough Quantity Against This Product Code in DC", "AR Tranders", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
        //        string command = "select bd.p_id P_Code, bd.tone Tone, bd.quality Quality, bd.size Size, bd.qty Boxes,bd.tiles Tiles, CAST(ROUND((p.meters*(bd.qty + (bd.tiles / p.pieces))),2)as decimal(18,2)) Meters From breakage_detail bd, product p where bd.p_id=p.p_id AND bd.size=p.size AND bd.b_id='" + b_id + "'";
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
            SqlCommand cmd3 = new SqlCommand("select isnull(sum(bd.qty),0) from breakage_detail2 bd,breakage_stock bs where bd.b_id=bs.b_id AND bs.ps_id <>'" + dc_id + "' AND bd.p_id ='" + p_code + "' ", Login.con);
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
            SqlCommand cmd4 = new SqlCommand("select qty from purchase_stock_detail2 where p_id='" + p_code + "' AND ps_id='" + dc_id + "'", Login.con);
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
                SqlCommand cmd1 = new SqlCommand("update breakage_detail2 set qty='" + p_qty + "' where p_id='" + p_code + "' AND b_id='" + b_id + "'", Login.con);
                cmd1.ExecuteNonQuery();
                string command2 = "select bd.p_id P_Code,p.p_name P_Name ,bd.qty Quantity From breakage_detail2 bd, product2 p where bd.b_id='" + b_id + "' AND bd.p_id=p.p_id ";
                test.bindingcode(command2, dataGridView2, Login.con);
            }
            else
            {
                MessageBox.Show("You Have Not Enough Quantity Against This Product Code in DC", "AR Tranders", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                string command2 = "select bd.p_id P_Code,p.p_name P_Name ,bd.qty Quantity From breakage_detail2 bd, product2 p where bd.b_id='" + b_id + "' AND bd.p_id=p.p_id ";
                test.bindingcode(command2, dataGridView2, Login.con);
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
    }
}
