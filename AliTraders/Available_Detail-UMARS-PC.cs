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

namespace Verona
{
    public partial class Available_Detail : Form
    {
        Form opener;
        Decimal total,amt;
        UF.Class1 test = new UF.Class1();
        private int i = 0;

        public Available_Detail(Form form)
        {
            InitializeComponent();
            opener = form;
        }

        private void Available_Stock_Load(object sender, EventArgs e)
        {
            opener.Enabled = false;
            rdbtnavil_CheckedChanged(this, null);
            string command = "SELECT p_id from product order by p_id";
            test.cmbox_drop(command, cmbavailcode, Login.con);
            string command2 = "SELECT p_name from product2 order by p_name";
            test.cmbox_drop(command2, cmbavailname, Login.con);
        }

        private void Available_Stock_FormClosing(object sender, FormClosingEventArgs e)
        {
            opener.Enabled = true;
        }

        private void rdbtnavil_CheckedChanged(object sender, EventArgs e)
        {
            if (rdbtnavil.Checked == true)
            {
                amt = 0;
                dataGridView4.Enabled = label4.Enabled = label25.Enabled = btnprint2.Enabled = txtavailcode.Enabled = cmbavailname.Enabled = false;
                txtavailcode.Focus();
                Login.con.Open();
                SqlCommand cmd95 = new SqlCommand("IF EXISTS(select * FROM sys.views where name = 'total2') DROP VIEW total2", Login.con);
                cmd95.ExecuteNonQuery();
                //SqlCommand cmd = new SqlCommand("create view [total2] as select ps.p_id,ps.size,ps.tone,ps.quality,CAST(ROUND((((ps.qty + (ps.tiles / p.pieces)) * (ps.pprice)) - (((ps.qty + (ps.tiles / p.pieces)) * (ps.pprice) * (ps.discount))/100)),2) as decimal(18,2)) total from purchase_stock_detail ps, product p where ps.p_id=p.p_id AND ps.size=p.size AND p.p_name='MASTER TILE'", Login.con);
                SqlCommand cmd = new SqlCommand("create view [total2] as select ps.p_id,ps.size,ps.tone,ps.quality,CAST(ROUND((((ps.qty + (ps.tiles / p.pieces)) * (ps.pprice)) - (((ps.qty + (ps.tiles / p.pieces)) * (ps.pprice) * (ps.discount))/100)),2) as decimal(18,2)) total from purchase_stock_detail ps, product p where ps.p_id=p.p_id AND ps.size=p.size", Login.con);
                cmd.ExecuteNonQuery();
                Login.con.Close();
                string cmd5 = "SELECT Distinct psd.p_id Product_Code,psd.size Size, psd.quality Quality,((SELECT isnull(sum(ps.qty), 0) from purchase_stock_detail ps where ps.p_id = psd.p_id AND ps.size = psd.size AND ps.quality = psd.quality) + CAST(((SELECT isnull(sum(ps.tiles), 0) from purchase_stock_detail ps where ps.p_id = psd.p_id AND ps.size = psd.size AND ps.quality = psd.quality) / (SELECT isnull(p.pieces, 0) from product p where p.p_id = psd.p_id AND p.size = psd.size))as int)) P_Qty,((SELECT isnull(sum(ps.tiles), 0) from purchase_stock_detail ps where ps.p_id = psd.p_id AND ps.size = psd.size AND ps.quality = psd.quality)% (SELECT isnull(p.pieces, 0) from product p where p.p_id = psd.p_id AND p.size = psd.size)) P_Tiles,((SELECT isnull(sum(pr.qty), 0) from purchase_return_detail pr where pr.p_id = psd.p_id AND pr.size = psd.size AND pr.quality = psd.quality) +CAST(((SELECT isnull(sum(pr.tiles), 0) from purchase_return_detail pr where pr.p_id = psd.p_id AND pr.size = psd.size AND pr.quality = psd.quality) / (SELECT isnull(p.pieces, 0) from product p where p.p_id = psd.p_id AND p.size = psd.size))as int)) PR_Qty,((SELECT isnull(sum(pr.tiles), 0) from purchase_return_detail pr where pr.p_id = psd.p_id AND pr.size = psd.size AND pr.quality = psd.quality)% (SELECT isnull(p.pieces, 0) from product p where p.p_id = psd.p_id AND p.size = psd.size)) PR_Tiles,((SELECT isnull(sum(bd.qty), 0) from breakage_detail bd where bd.p_id = psd.p_id AND bd.size = psd.size AND bd.quality = psd.quality) +CAST(((SELECT isnull(sum(bd.tiles), 0) from breakage_detail bd where bd.p_id = psd.p_id AND bd.size = psd.size AND bd.quality = psd.quality) / (SELECT isnull(p.pieces, 0) from product p where p.p_id = psd.p_id AND p.size = psd.size))as int)) B_Qty,((SELECT isnull(sum(bd.tiles), 0) from breakage_detail bd where bd.p_id = psd.p_id AND bd.size = psd.size AND bd.quality = psd.quality)% (SELECT isnull(p.pieces, 0) from product p where p.p_id = psd.p_id AND p.size = psd.size)) B_Tiles,((SELECT isnull(sum(s.qty), 0) from sale_stock_detail s where s.p_id = psd.p_id AND s.size = psd.size AND s.quality = psd.quality) +CAST(((SELECT isnull(sum(s.tiles), 0) from sale_stock_detail s where s.p_id = psd.p_id AND s.size = psd.size AND s.quality = psd.quality) / (SELECT isnull(p.pieces, 0) from product p where p.p_id = psd.p_id AND p.size = psd.size))as int)) S_Qty,((SELECT isnull(sum(s.tiles), 0) from sale_stock_detail s where s.p_id = psd.p_id AND s.size = psd.size AND s.quality = psd.quality)% (SELECT isnull(p.pieces, 0) from product p where p.p_id = psd.p_id AND p.size = psd.size)) S_Tiles,((SELECT isnull(sum(sr.qty), 0) from sale_return_detail sr where sr.p_id = psd.p_id AND sr.size = psd.size AND sr.quality = psd.quality) +CAST(((SELECT isnull(sum(sr.tiles), 0) from sale_return_detail sr where sr.p_id = psd.p_id AND sr.size = psd.size AND sr.quality = psd.quality) / (SELECT isnull(p.pieces, 0) from product p where p.p_id = psd.p_id AND p.size = psd.size))as int)) SR_Qty,((SELECT isnull(sum(sr.tiles), 0) from sale_return_detail sr where sr.p_id = psd.p_id AND sr.size = psd.size AND sr.quality = psd.quality)% (SELECT isnull(p.pieces, 0) from product p where p.p_id = psd.p_id AND p.size = psd.size)) SR_Tiles,CAST(((((((SELECT isnull(sum(ps.qty), 0) from purchase_stock_detail ps where ps.p_id = psd.p_id AND ps.size = psd.size AND ps.quality = psd.quality) + CAST(((SELECT isnull(sum(ps.tiles), 0) from purchase_stock_detail ps where ps.p_id = psd.p_id AND ps.size = psd.size AND ps.quality = psd.quality) / (SELECT isnull(p.pieces, 0) from product p where p.p_id = psd.p_id AND p.size = psd.size))as float)) -((SELECT isnull(sum(bd.qty), 0) from breakage_detail bd where bd.p_id = psd.p_id AND bd.size = psd.size AND bd.quality = psd.quality) +CAST(((SELECT isnull(sum(bd.tiles), 0) from breakage_detail bd where bd.p_id = psd.p_id AND bd.size = psd.size AND bd.quality = psd.quality) / (SELECT isnull(p.pieces, 0) from product p where p.p_id = psd.p_id AND p.size = psd.size))as float)) - ((SELECT isnull(sum(pr.qty), 0) from purchase_return_detail pr where pr.p_id = psd.p_id AND pr.size = psd.size AND pr.quality = psd.quality) +CAST(((SELECT isnull(sum(pr.tiles), 0) from purchase_return_detail pr where pr.p_id = psd.p_id AND pr.size = psd.size AND pr.quality = psd.quality) / (SELECT isnull(p.pieces, 0) from product p where p.p_id = psd.p_id AND p.size = psd.size))as float))) -(((SELECT isnull(sum(s.qty), 0) from sale_stock_detail s where s.p_id = psd.p_id AND s.size = psd.size AND s.quality = psd.quality) +CAST(((SELECT isnull(sum(s.tiles), 0) from sale_stock_detail s where s.p_id = psd.p_id AND s.size = psd.size AND s.quality = psd.quality) / (SELECT isnull(p.pieces, 0) from product p where p.p_id = psd.p_id AND p.size = psd.size))as float)) -((SELECT isnull(sum(sr.qty), 0) from sale_return_detail sr where sr.p_id = psd.p_id AND sr.size = psd.size AND sr.quality = psd.quality) +CAST(((SELECT isnull(sum(sr.tiles), 0) from sale_return_detail sr where sr.p_id = psd.p_id AND sr.size = psd.size AND sr.quality = psd.quality) / (SELECT isnull(p.pieces, 0) from product p where p.p_id = psd.p_id AND p.size = psd.size))as float)))) *(SELECT isnull(p.pieces, 0) from product p where p.p_id = psd.p_id AND p.size = psd.size)) / (SELECT isnull(p.pieces, 0) from product p where p.p_id = psd.p_id AND p.size = psd.size))as int) Avail_Qty,(CAST((((((SELECT isnull(sum(ps.qty), 0) from purchase_stock_detail ps where ps.p_id = psd.p_id AND ps.size = psd.size AND ps.quality = psd.quality) + CAST(((SELECT isnull(sum(ps.tiles), 0) from purchase_stock_detail ps where ps.p_id = psd.p_id AND ps.size = psd.size AND ps.quality = psd.quality) / (SELECT isnull(p.pieces, 0) from product p where p.p_id = psd.p_id AND p.size = psd.size))as float)) -((SELECT isnull(sum(bd.qty), 0) from breakage_detail bd where bd.p_id = psd.p_id AND bd.size = psd.size AND bd.quality = psd.quality) +CAST(((SELECT isnull(sum(bd.tiles), 0) from breakage_detail bd where bd.p_id = psd.p_id AND bd.size = psd.size AND bd.quality = psd.quality) / (SELECT isnull(p.pieces, 0) from product p where p.p_id = psd.p_id AND p.size = psd.size))as float)) - ((SELECT isnull(sum(pr.qty), 0) from purchase_return_detail pr where pr.p_id = psd.p_id AND pr.size = psd.size AND pr.quality = psd.quality) +CAST(((SELECT isnull(sum(pr.tiles), 0) from purchase_return_detail pr where pr.p_id = psd.p_id AND pr.size = psd.size AND pr.quality = psd.quality) / (SELECT isnull(p.pieces, 0) from product p where p.p_id = psd.p_id AND p.size = psd.size))as float))) -(((SELECT isnull(sum(s.qty), 0) from sale_stock_detail s where s.p_id = psd.p_id AND s.size = psd.size AND s.quality = psd.quality) +CAST(((SELECT isnull(sum(s.tiles), 0) from sale_stock_detail s where s.p_id = psd.p_id AND s.size = psd.size AND s.quality = psd.quality) / (SELECT isnull(p.pieces, 0) from product p where p.p_id = psd.p_id AND p.size = psd.size))as float)) -((SELECT isnull(sum(sr.qty), 0) from sale_return_detail sr where sr.p_id = psd.p_id AND sr.size = psd.size AND sr.quality = psd.quality) +CAST(((SELECT isnull(sum(sr.tiles), 0) from sale_return_detail sr where sr.p_id = psd.p_id AND sr.size = psd.size AND sr.quality = psd.quality) / (SELECT isnull(p.pieces, 0) from product p where p.p_id = psd.p_id AND p.size = psd.size))as float)))) *(SELECT isnull(p.pieces, 0) from product p where p.p_id = psd.p_id AND p.size = psd.size)) as int) % (SELECT isnull(p.pieces, 0) from product p where p.p_id = psd.p_id AND p.size = psd.size)) Avail_Tiles,cast(isnull((select nullif(sum(t.total), 0) from total2 t where psd.p_id = t.p_id AND psd.size = t.size AND psd.quality = t.quality) / ((SELECT isnull(sum(ps.qty), 0) from purchase_stock_detail ps where ps.p_id = psd.p_id AND ps.size = psd.size AND ps.quality = psd.quality) +CAST(((SELECT isnull(sum(ps.tiles), 0) from purchase_stock_detail ps where ps.p_id = psd.p_id AND ps.size = psd.size AND ps.quality = psd.quality) / (SELECT isnull(p.pieces, 0) from product p where p.p_id = psd.p_id AND p.size = psd.size))as float)),0) as decimal(18,2)) Avg_Price,CAST((((CAST(((((((SELECT isnull(sum(ps.qty), 0) from purchase_stock_detail ps where ps.p_id = psd.p_id AND ps.size = psd.size AND ps.quality = psd.quality) + CAST(((SELECT isnull(sum(ps.tiles), 0) from purchase_stock_detail ps where ps.p_id = psd.p_id AND ps.size = psd.size AND ps.quality = psd.quality) / (SELECT isnull(p.pieces, 0) from product p where p.p_id = psd.p_id AND p.size = psd.size))as float)) -((SELECT isnull(sum(bd.qty), 0) from breakage_detail bd where bd.p_id = psd.p_id AND bd.size = psd.size AND bd.quality = psd.quality) +CAST(((SELECT isnull(sum(bd.tiles), 0) from breakage_detail bd where bd.p_id = psd.p_id AND bd.size = psd.size AND bd.quality = psd.quality) / (SELECT isnull(p.pieces, 0) from product p where p.p_id = psd.p_id AND p.size = psd.size))as float)) - ((SELECT isnull(sum(pr.qty), 0) from purchase_return_detail pr where pr.p_id = psd.p_id AND pr.size = psd.size AND pr.quality = psd.quality) +CAST(((SELECT isnull(sum(pr.tiles), 0) from purchase_return_detail pr where pr.p_id = psd.p_id AND pr.size = psd.size AND pr.quality = psd.quality) / (SELECT isnull(p.pieces, 0) from product p where p.p_id = psd.p_id AND p.size = psd.size))as float))) -(((SELECT isnull(sum(s.qty), 0) from sale_stock_detail s where s.p_id = psd.p_id AND s.size = psd.size AND s.quality = psd.quality) +CAST(((SELECT isnull(sum(s.tiles), 0) from sale_stock_detail s where s.p_id = psd.p_id AND s.size = psd.size AND s.quality = psd.quality) / (SELECT isnull(p.pieces, 0) from product p where p.p_id = psd.p_id AND p.size = psd.size))as float)) -((SELECT isnull(sum(sr.qty), 0) from sale_return_detail sr where sr.p_id = psd.p_id AND sr.size = psd.size AND sr.quality = psd.quality) +CAST(((SELECT isnull(sum(sr.tiles), 0) from sale_return_detail sr where sr.p_id = psd.p_id AND sr.size = psd.size AND sr.quality = psd.quality) / (SELECT isnull(p.pieces, 0) from product p where p.p_id = psd.p_id AND p.size = psd.size))as float)))) *(SELECT isnull(p.pieces, 0) from product p where p.p_id = psd.p_id AND p.size = psd.size)) / (SELECT isnull(p.pieces, 0) from product p where p.p_id = psd.p_id AND p.size = psd.size))as int)) +((CAST((((((SELECT isnull(sum(ps.qty), 0) from purchase_stock_detail ps where ps.p_id = psd.p_id AND ps.size = psd.size AND ps.quality = psd.quality) + CAST(((SELECT isnull(sum(ps.tiles), 0) from purchase_stock_detail ps where ps.p_id = psd.p_id AND ps.size = psd.size AND ps.quality = psd.quality) / (SELECT isnull(p.pieces, 0) from product p where p.p_id = psd.p_id AND p.size = psd.size))as float)) -((SELECT isnull(sum(bd.qty), 0) from breakage_detail bd where bd.p_id = psd.p_id AND bd.size = psd.size AND bd.quality = psd.quality) +CAST(((SELECT isnull(sum(bd.tiles), 0) from breakage_detail bd where bd.p_id = psd.p_id AND bd.size = psd.size AND bd.quality = psd.quality) / (SELECT isnull(p.pieces, 0) from product p where p.p_id = psd.p_id AND p.size = psd.size))as float)) - ((SELECT isnull(sum(pr.qty), 0) from purchase_return_detail pr where pr.p_id = psd.p_id AND pr.size = psd.size AND pr.quality = psd.quality) +CAST(((SELECT isnull(sum(pr.tiles), 0) from purchase_return_detail pr where pr.p_id = psd.p_id AND pr.size = psd.size AND pr.quality = psd.quality) / (SELECT isnull(p.pieces, 0) from product p where p.p_id = psd.p_id AND p.size = psd.size))as float))) -(((SELECT isnull(sum(s.qty), 0) from sale_stock_detail s where s.p_id = psd.p_id AND s.size = psd.size AND s.quality = psd.quality) +CAST(((SELECT isnull(sum(s.tiles), 0) from sale_stock_detail s where s.p_id = psd.p_id AND s.size = psd.size AND s.quality = psd.quality) / (SELECT isnull(p.pieces, 0) from product p where p.p_id = psd.p_id AND p.size = psd.size))as float)) -((SELECT isnull(sum(sr.qty), 0) from sale_return_detail sr where sr.p_id = psd.p_id AND sr.size = psd.size AND sr.quality = psd.quality) +CAST(((SELECT isnull(sum(sr.tiles), 0) from sale_return_detail sr where sr.p_id = psd.p_id AND sr.size = psd.size AND sr.quality = psd.quality) / (SELECT isnull(p.pieces, 0) from product p where p.p_id = psd.p_id AND p.size = psd.size))as float)))) *(SELECT isnull(p.pieces, 0) from product p where p.p_id = psd.p_id AND p.size = psd.size)) as int) % (SELECT isnull(p.pieces, 0) from product p where p.p_id = psd.p_id AND p.size = psd.size)) / (SELECT isnull(p.pieces, 0) from product p where p.p_id = psd.p_id AND p.size = psd.size))) *(cast(isnull((select nullif(sum(t.total), 0) from total2 t where psd.p_id = t.p_id AND psd.size = t.size AND psd.quality = t.quality) / ((SELECT isnull(sum(ps.qty), 0) from purchase_stock_detail ps where ps.p_id = psd.p_id AND ps.size = psd.size AND ps.quality = psd.quality) + CAST(((SELECT isnull(sum(ps.tiles), 0) from purchase_stock_detail ps where ps.p_id = psd.p_id AND ps.size = psd.size AND ps.quality = psd.quality) / (SELECT isnull(p.pieces, 0) from product p where p.p_id = psd.p_id AND p.size = psd.size))as float)),0) as decimal(18,2))))as decimal(18,2)) Total from total2 psd order by psd.p_id";
                test.bindingcode(cmd5, dataGridView3, Login.con);
                Login.con.Open();
                SqlCommand cmd6 = new SqlCommand("DROP VIEW total2", Login.con);
                cmd6.ExecuteNonQuery();
                Login.con.Close();
                for (int i = 0; i < dataGridView3.RowCount; i++)
                {
                    amt += Convert.ToDecimal(dataGridView3.Rows[i].Cells[16].Value);
                }
                lbtiles.Text = amt.ToString();
            }
            else
                dataGridView4.Enabled = label4.Enabled = label25.Enabled = btnprint2.Enabled = txtavailcode.Enabled = cmbavailname.Enabled = true;
            cmbavailcode.Text = txtavailcode.Text = cmbavailname.Text = "";
            cmbsize2.Items.Clear();
            cmbquality.Items.Clear();
        }

        private void cmbsize2_Enter(object sender, EventArgs e)
        {
            string command2 = "SELECT DISTINCT size from product order by size ASC";
            test.cmbox_drop(command2, cmbsize2, Login.con);
        }

        private void cmbquality_Enter(object sender, EventArgs e)
        {
            string command = "select DISTINCT quality from purchase_stock_detail";
            test.cmbox_drop(command, cmbquality, Login.con);
        }

        private void btnenter_Click(object sender, EventArgs e)
        {
            amt = 0;
            Login.con.Open();
            SqlCommand cmd95 = new SqlCommand("IF EXISTS(select * FROM sys.views where name = 'total2') DROP VIEW total2", Login.con);
            cmd95.ExecuteNonQuery();
            SqlCommand cmd = new SqlCommand("create view [total2] as select ps.p_id,ps.size,ps.tone,ps.quality,CAST(ROUND((((ps.qty + (ps.tiles / p.pieces)) * (ps.pprice)) - (((ps.qty + (ps.tiles / p.pieces)) * (ps.pprice) * (ps.discount))/100)),2) as decimal(18,2)) total from purchase_stock_detail ps, product p where ps.p_id=p.p_id AND ps.size=p.size", Login.con);
            cmd.ExecuteNonQuery();
            Login.con.Close();
            string cmd5 = "SELECT Distinct psd.p_id Product_Code,psd.size Size, psd.quality Quality,((SELECT isnull(sum(ps.qty), 0) from purchase_stock_detail ps where ps.p_id = psd.p_id AND ps.size = psd.size AND ps.quality = psd.quality) + CAST(((SELECT isnull(sum(ps.tiles), 0) from purchase_stock_detail ps where ps.p_id = psd.p_id AND ps.size = psd.size AND ps.quality = psd.quality) / (SELECT isnull(p.pieces, 0) from product p where p.p_id = psd.p_id AND p.size = psd.size))as int)) P_Qty,((SELECT isnull(sum(ps.tiles), 0) from purchase_stock_detail ps where ps.p_id = psd.p_id AND ps.size = psd.size AND ps.quality = psd.quality)% (SELECT isnull(p.pieces, 0) from product p where p.p_id = psd.p_id AND p.size = psd.size)) P_Tiles,((SELECT isnull(sum(pr.qty), 0) from purchase_return_detail pr where pr.p_id = psd.p_id AND pr.size = psd.size AND pr.quality = psd.quality) +CAST(((SELECT isnull(sum(pr.tiles), 0) from purchase_return_detail pr where pr.p_id = psd.p_id AND pr.size = psd.size AND pr.quality = psd.quality) / (SELECT isnull(p.pieces, 0) from product p where p.p_id = psd.p_id AND p.size = psd.size))as int)) PR_Qty,((SELECT isnull(sum(pr.tiles), 0) from purchase_return_detail pr where pr.p_id = psd.p_id AND pr.size = psd.size AND pr.quality = psd.quality)% (SELECT isnull(p.pieces, 0) from product p where p.p_id = psd.p_id AND p.size = psd.size)) PR_Tiles,((SELECT isnull(sum(bd.qty), 0) from breakage_detail bd where bd.p_id = psd.p_id AND bd.size = psd.size AND bd.quality = psd.quality) +CAST(((SELECT isnull(sum(bd.tiles), 0) from breakage_detail bd where bd.p_id = psd.p_id AND bd.size = psd.size AND bd.quality = psd.quality) / (SELECT isnull(p.pieces, 0) from product p where p.p_id = psd.p_id AND p.size = psd.size))as int)) B_Qty,((SELECT isnull(sum(bd.tiles), 0) from breakage_detail bd where bd.p_id = psd.p_id AND bd.size = psd.size AND bd.quality = psd.quality)% (SELECT isnull(p.pieces, 0) from product p where p.p_id = psd.p_id AND p.size = psd.size)) B_Tiles,((SELECT isnull(sum(s.qty), 0) from sale_stock_detail s where s.p_id = psd.p_id AND s.size = psd.size AND s.quality = psd.quality) +CAST(((SELECT isnull(sum(s.tiles), 0) from sale_stock_detail s where s.p_id = psd.p_id AND s.size = psd.size AND s.quality = psd.quality) / (SELECT isnull(p.pieces, 0) from product p where p.p_id = psd.p_id AND p.size = psd.size))as int)) S_Qty,((SELECT isnull(sum(s.tiles), 0) from sale_stock_detail s where s.p_id = psd.p_id AND s.size = psd.size AND s.quality = psd.quality)% (SELECT isnull(p.pieces, 0) from product p where p.p_id = psd.p_id AND p.size = psd.size)) S_Tiles,((SELECT isnull(sum(sr.qty), 0) from sale_return_detail sr where sr.p_id = psd.p_id AND sr.size = psd.size AND sr.quality = psd.quality) +CAST(((SELECT isnull(sum(sr.tiles), 0) from sale_return_detail sr where sr.p_id = psd.p_id AND sr.size = psd.size AND sr.quality = psd.quality) / (SELECT isnull(p.pieces, 0) from product p where p.p_id = psd.p_id AND p.size = psd.size))as int)) SR_Qty,((SELECT isnull(sum(sr.tiles), 0) from sale_return_detail sr where sr.p_id = psd.p_id AND sr.size = psd.size AND sr.quality = psd.quality)% (SELECT isnull(p.pieces, 0) from product p where p.p_id = psd.p_id AND p.size = psd.size)) SR_Tiles,CAST(((((((SELECT isnull(sum(ps.qty), 0) from purchase_stock_detail ps where ps.p_id = psd.p_id AND ps.size = psd.size AND ps.quality = psd.quality) + CAST(((SELECT isnull(sum(ps.tiles), 0) from purchase_stock_detail ps where ps.p_id = psd.p_id AND ps.size = psd.size AND ps.quality = psd.quality) / (SELECT isnull(p.pieces, 0) from product p where p.p_id = psd.p_id AND p.size = psd.size))as float)) -((SELECT isnull(sum(bd.qty), 0) from breakage_detail bd where bd.p_id = psd.p_id AND bd.size = psd.size AND bd.quality = psd.quality) +CAST(((SELECT isnull(sum(bd.tiles), 0) from breakage_detail bd where bd.p_id = psd.p_id AND bd.size = psd.size AND bd.quality = psd.quality) / (SELECT isnull(p.pieces, 0) from product p where p.p_id = psd.p_id AND p.size = psd.size))as float)) - ((SELECT isnull(sum(pr.qty), 0) from purchase_return_detail pr where pr.p_id = psd.p_id AND pr.size = psd.size AND pr.quality = psd.quality) +CAST(((SELECT isnull(sum(pr.tiles), 0) from purchase_return_detail pr where pr.p_id = psd.p_id AND pr.size = psd.size AND pr.quality = psd.quality) / (SELECT isnull(p.pieces, 0) from product p where p.p_id = psd.p_id AND p.size = psd.size))as float))) -(((SELECT isnull(sum(s.qty), 0) from sale_stock_detail s where s.p_id = psd.p_id AND s.size = psd.size AND s.quality = psd.quality) +CAST(((SELECT isnull(sum(s.tiles), 0) from sale_stock_detail s where s.p_id = psd.p_id AND s.size = psd.size AND s.quality = psd.quality) / (SELECT isnull(p.pieces, 0) from product p where p.p_id = psd.p_id AND p.size = psd.size))as float)) -((SELECT isnull(sum(sr.qty), 0) from sale_return_detail sr where sr.p_id = psd.p_id AND sr.size = psd.size AND sr.quality = psd.quality) +CAST(((SELECT isnull(sum(sr.tiles), 0) from sale_return_detail sr where sr.p_id = psd.p_id AND sr.size = psd.size AND sr.quality = psd.quality) / (SELECT isnull(p.pieces, 0) from product p where p.p_id = psd.p_id AND p.size = psd.size))as float)))) *(SELECT isnull(p.pieces, 0) from product p where p.p_id = psd.p_id AND p.size = psd.size)) / (SELECT isnull(p.pieces, 0) from product p where p.p_id = psd.p_id AND p.size = psd.size))as int) Avail_Qty,(CAST((((((SELECT isnull(sum(ps.qty), 0) from purchase_stock_detail ps where ps.p_id = psd.p_id AND ps.size = psd.size AND ps.quality = psd.quality) + CAST(((SELECT isnull(sum(ps.tiles), 0) from purchase_stock_detail ps where ps.p_id = psd.p_id AND ps.size = psd.size AND ps.quality = psd.quality) / (SELECT isnull(p.pieces, 0) from product p where p.p_id = psd.p_id AND p.size = psd.size))as float)) -((SELECT isnull(sum(bd.qty), 0) from breakage_detail bd where bd.p_id = psd.p_id AND bd.size = psd.size AND bd.quality = psd.quality) +CAST(((SELECT isnull(sum(bd.tiles), 0) from breakage_detail bd where bd.p_id = psd.p_id AND bd.size = psd.size AND bd.quality = psd.quality) / (SELECT isnull(p.pieces, 0) from product p where p.p_id = psd.p_id AND p.size = psd.size))as float)) - ((SELECT isnull(sum(pr.qty), 0) from purchase_return_detail pr where pr.p_id = psd.p_id AND pr.size = psd.size AND pr.quality = psd.quality) +CAST(((SELECT isnull(sum(pr.tiles), 0) from purchase_return_detail pr where pr.p_id = psd.p_id AND pr.size = psd.size AND pr.quality = psd.quality) / (SELECT isnull(p.pieces, 0) from product p where p.p_id = psd.p_id AND p.size = psd.size))as float))) -(((SELECT isnull(sum(s.qty), 0) from sale_stock_detail s where s.p_id = psd.p_id AND s.size = psd.size AND s.quality = psd.quality) +CAST(((SELECT isnull(sum(s.tiles), 0) from sale_stock_detail s where s.p_id = psd.p_id AND s.size = psd.size AND s.quality = psd.quality) / (SELECT isnull(p.pieces, 0) from product p where p.p_id = psd.p_id AND p.size = psd.size))as float)) -((SELECT isnull(sum(sr.qty), 0) from sale_return_detail sr where sr.p_id = psd.p_id AND sr.size = psd.size AND sr.quality = psd.quality) +CAST(((SELECT isnull(sum(sr.tiles), 0) from sale_return_detail sr where sr.p_id = psd.p_id AND sr.size = psd.size AND sr.quality = psd.quality) / (SELECT isnull(p.pieces, 0) from product p where p.p_id = psd.p_id AND p.size = psd.size))as float)))) *(SELECT isnull(p.pieces, 0) from product p where p.p_id = psd.p_id AND p.size = psd.size)) as decimal) % (SELECT isnull(p.pieces, 0) from product p where p.p_id = psd.p_id AND p.size = psd.size)) Avail_Tiles,cast(isnull((select nullif(sum(t.total), 0) from total2 t where psd.p_id = t.p_id AND psd.size = t.size AND psd.quality = t.quality) / ((SELECT isnull(sum(ps.qty), 0) from purchase_stock_detail ps where ps.p_id = psd.p_id AND ps.size = psd.size AND ps.quality = psd.quality) +CAST(((SELECT isnull(sum(ps.tiles), 0) from purchase_stock_detail ps where ps.p_id = psd.p_id AND ps.size = psd.size AND ps.quality = psd.quality) / (SELECT isnull(p.pieces, 0) from product p where p.p_id = psd.p_id AND p.size = psd.size))as float)),0) as decimal(18,2)) Avg_Price,CAST((((CAST(((((((SELECT isnull(sum(ps.qty), 0) from purchase_stock_detail ps where ps.p_id = psd.p_id AND ps.size = psd.size AND ps.quality = psd.quality) + CAST(((SELECT isnull(sum(ps.tiles), 0) from purchase_stock_detail ps where ps.p_id = psd.p_id AND ps.size = psd.size AND ps.quality = psd.quality) / (SELECT isnull(p.pieces, 0) from product p where p.p_id = psd.p_id AND p.size = psd.size))as float)) -((SELECT isnull(sum(bd.qty), 0) from breakage_detail bd where bd.p_id = psd.p_id AND bd.size = psd.size AND bd.quality = psd.quality) +CAST(((SELECT isnull(sum(bd.tiles), 0) from breakage_detail bd where bd.p_id = psd.p_id AND bd.size = psd.size AND bd.quality = psd.quality) / (SELECT isnull(p.pieces, 0) from product p where p.p_id = psd.p_id AND p.size = psd.size))as float)) - ((SELECT isnull(sum(pr.qty), 0) from purchase_return_detail pr where pr.p_id = psd.p_id AND pr.size = psd.size AND pr.quality = psd.quality) +CAST(((SELECT isnull(sum(pr.tiles), 0) from purchase_return_detail pr where pr.p_id = psd.p_id AND pr.size = psd.size AND pr.quality = psd.quality) / (SELECT isnull(p.pieces, 0) from product p where p.p_id = psd.p_id AND p.size = psd.size))as float))) -(((SELECT isnull(sum(s.qty), 0) from sale_stock_detail s where s.p_id = psd.p_id AND s.size = psd.size AND s.quality = psd.quality) +CAST(((SELECT isnull(sum(s.tiles), 0) from sale_stock_detail s where s.p_id = psd.p_id AND s.size = psd.size AND s.quality = psd.quality) / (SELECT isnull(p.pieces, 0) from product p where p.p_id = psd.p_id AND p.size = psd.size))as float)) -((SELECT isnull(sum(sr.qty), 0) from sale_return_detail sr where sr.p_id = psd.p_id AND sr.size = psd.size AND sr.quality = psd.quality) +CAST(((SELECT isnull(sum(sr.tiles), 0) from sale_return_detail sr where sr.p_id = psd.p_id AND sr.size = psd.size AND sr.quality = psd.quality) / (SELECT isnull(p.pieces, 0) from product p where p.p_id = psd.p_id AND p.size = psd.size))as float)))) *(SELECT isnull(p.pieces, 0) from product p where p.p_id = psd.p_id AND p.size = psd.size)) / (SELECT isnull(p.pieces, 0) from product p where p.p_id = psd.p_id AND p.size = psd.size))as int)) +((CAST((((((SELECT isnull(sum(ps.qty), 0) from purchase_stock_detail ps where ps.p_id = psd.p_id AND ps.size = psd.size AND ps.quality = psd.quality) + CAST(((SELECT isnull(sum(ps.tiles), 0) from purchase_stock_detail ps where ps.p_id = psd.p_id AND ps.size = psd.size AND ps.quality = psd.quality) / (SELECT isnull(p.pieces, 0) from product p where p.p_id = psd.p_id AND p.size = psd.size))as float)) -((SELECT isnull(sum(bd.qty), 0) from breakage_detail bd where bd.p_id = psd.p_id AND bd.size = psd.size AND bd.quality = psd.quality) +CAST(((SELECT isnull(sum(bd.tiles), 0) from breakage_detail bd where bd.p_id = psd.p_id AND bd.size = psd.size AND bd.quality = psd.quality) / (SELECT isnull(p.pieces, 0) from product p where p.p_id = psd.p_id AND p.size = psd.size))as float)) - ((SELECT isnull(sum(pr.qty), 0) from purchase_return_detail pr where pr.p_id = psd.p_id AND pr.size = psd.size AND pr.quality = psd.quality) +CAST(((SELECT isnull(sum(pr.tiles), 0) from purchase_return_detail pr where pr.p_id = psd.p_id AND pr.size = psd.size AND pr.quality = psd.quality) / (SELECT isnull(p.pieces, 0) from product p where p.p_id = psd.p_id AND p.size = psd.size))as float))) -(((SELECT isnull(sum(s.qty), 0) from sale_stock_detail s where s.p_id = psd.p_id AND s.size = psd.size AND s.quality = psd.quality) +CAST(((SELECT isnull(sum(s.tiles), 0) from sale_stock_detail s where s.p_id = psd.p_id AND s.size = psd.size AND s.quality = psd.quality) / (SELECT isnull(p.pieces, 0) from product p where p.p_id = psd.p_id AND p.size = psd.size))as float)) -((SELECT isnull(sum(sr.qty), 0) from sale_return_detail sr where sr.p_id = psd.p_id AND sr.size = psd.size AND sr.quality = psd.quality) +CAST(((SELECT isnull(sum(sr.tiles), 0) from sale_return_detail sr where sr.p_id = psd.p_id AND sr.size = psd.size AND sr.quality = psd.quality) / (SELECT isnull(p.pieces, 0) from product p where p.p_id = psd.p_id AND p.size = psd.size))as float)))) *(SELECT isnull(p.pieces, 0) from product p where p.p_id = psd.p_id AND p.size = psd.size)) as decimal) % (SELECT isnull(p.pieces, 0) from product p where p.p_id = psd.p_id AND p.size = psd.size)) / (SELECT isnull(p.pieces, 0) from product p where p.p_id = psd.p_id AND p.size = psd.size))) *(cast(isnull((select nullif(sum(t.total), 0) from total2 t where psd.p_id = t.p_id AND psd.size = t.size AND psd.quality = t.quality) / ((SELECT isnull(sum(ps.qty), 0) from purchase_stock_detail ps where ps.p_id = psd.p_id AND ps.size = psd.size AND ps.quality = psd.quality) + CAST(((SELECT isnull(sum(ps.tiles), 0) from purchase_stock_detail ps where ps.p_id = psd.p_id AND ps.size = psd.size AND ps.quality = psd.quality) / (SELECT isnull(p.pieces, 0) from product p where p.p_id = psd.p_id AND p.size = psd.size))as float)),0) as decimal(18,2))))as decimal(18,2)) Total from total2 psd where psd.p_id Like '" + cmbavailcode.Text+ "%' AND psd.size Like '" + cmbsize2.Text + "%' AND psd.quality Like '" + cmbquality.Text + "%'";
            test.bindingcode(cmd5, dataGridView3, Login.con);
            Login.con.Open();
            SqlCommand cmd6 = new SqlCommand("DROP VIEW total2", Login.con);
            cmd6.ExecuteNonQuery();
            Login.con.Close();
            for (int i = 0; i < dataGridView3.RowCount; i++)
            {
                amt += Convert.ToDecimal(dataGridView3.Rows[i].Cells[16].Value);
            }
            lbtiles.Text = amt.ToString();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            rdbtnavil_CheckedChanged(this, null);
        }

        private void btnprint_Click(object sender, EventArgs e)
        {
            total = 0;
            try
            {
                printDocument1.Print();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void printDocument1_PrintPage(object sender, PrintPageEventArgs e)
        {
            Image imgPerson = Image.FromFile(@"D:\VR.png");
            e.Graphics.DrawImage(imgPerson, 100, 60);
            e.Graphics.DrawString("Verona Tiles & Sanitary", new Font("Arial", 28, FontStyle.Bold), Brushes.Black, 230, 50);
            e.Graphics.DrawString("IMPORTERS & GENERAL ORDER SUPPLIER", new Font("Arial", 10), Brushes.Black, 280, 90);
            e.Graphics.DrawString("Deals in All Kinds of Imported Sanitary Items", new Font("Arial", 9, FontStyle.Italic), Brushes.Black, 300, 105);
            e.Graphics.DrawString("Kangni Wala ByPass Near PSO Pump G.T Road Gujranwala", new Font("Arial", 10, FontStyle.Regular), Brushes.Black, 265, 125);
            e.Graphics.DrawString("Contact: ", new Font("Arial", 9, FontStyle.Bold), Brushes.Black, 265, 140);
            e.Graphics.DrawString("(055) 4296080 - 4296090", new Font("Arial", 9, FontStyle.Italic), Brushes.Black, 325, 140);
            int startX = 50;
            int startY = 150;
            int Offset = 40;
            int realwidth = 50;
            int realheight = 20;
            e.Graphics.DrawString("Available Stock Detail", new Font("Calibri", 12, FontStyle.Underline), new SolidBrush(Color.Black), startX, startY + Offset);
            Offset = Offset + 30;
            e.Graphics.FillRectangle(Brushes.Black, realwidth, startY + Offset, startX + 120, realheight);
            e.Graphics.DrawRectangle(Pens.Black, realwidth, startY + Offset, startX + 120, realheight);
            e.Graphics.DrawString("P_Code", new Font("Calibri", 12, FontStyle.Bold), new SolidBrush(Color.White), startX, startY + Offset);
            realwidth = realwidth + startX + 120;
            e.Graphics.FillRectangle(Brushes.Black, realwidth, startY + Offset, startX + 11, realheight);
            e.Graphics.DrawRectangle(Pens.Black, realwidth, startY + Offset, startX + 11, realheight);
            e.Graphics.DrawString("Size", new Font("Calibri", 12, FontStyle.Bold), new SolidBrush(Color.White), startX + 170, startY + Offset);
            realwidth = realwidth + startX + 11;
            e.Graphics.FillRectangle(Brushes.Black, realwidth, startY + Offset, startX + 35, realheight);
            e.Graphics.DrawRectangle(Pens.Black, realwidth, startY + Offset, startX + 35, realheight);
            e.Graphics.DrawString("Quality", new Font("Calibri", 12, FontStyle.Bold), new SolidBrush(Color.White), startX + 230, startY + Offset);
            realwidth = realwidth + startX + 35;
            e.Graphics.FillRectangle(Brushes.Black, realwidth, startY + Offset, startX - 5, realheight);
            e.Graphics.DrawRectangle(Pens.Black, realwidth, startY + Offset, startX - 5, realheight);
            e.Graphics.DrawString("P_Qty", new Font("Calibri", 12, FontStyle.Bold), new SolidBrush(Color.White), startX + 315, startY + Offset);
            realwidth = realwidth + startX - 5;
            e.Graphics.FillRectangle(Brushes.Black, realwidth, startY + Offset, startX + 5, realheight);
            e.Graphics.DrawRectangle(Pens.Black, realwidth, startY + Offset, startX + 5, realheight);
            e.Graphics.DrawString("PR_Qty", new Font("Calibri", 12, FontStyle.Bold), new SolidBrush(Color.White), startX + 360, startY + Offset);
            realwidth = realwidth + startX + 5;
            e.Graphics.FillRectangle(Brushes.Black, realwidth, startY + Offset, startX - 5, realheight);
            e.Graphics.DrawRectangle(Pens.Black, realwidth, startY + Offset, startX - 5, realheight);
            e.Graphics.DrawString("B_Qty", new Font("Calibri", 12, FontStyle.Bold), new SolidBrush(Color.White), startX + 415, startY + Offset);
            realwidth = realwidth + startX - 5;
            e.Graphics.FillRectangle(Brushes.Black, realwidth, startY + Offset, startX - 5, realheight);
            e.Graphics.DrawRectangle(Pens.Black, realwidth, startY + Offset, startX - 5, realheight);
            e.Graphics.DrawString("S_Qty", new Font("Calibri", 12, FontStyle.Bold), new SolidBrush(Color.White), startX + 460, startY + Offset);
            realwidth = realwidth + startX - 5;
            e.Graphics.FillRectangle(Brushes.Black, realwidth, startY + Offset, startX + 5, realheight);
            e.Graphics.DrawRectangle(Pens.Black, realwidth, startY + Offset, startX + 5, realheight);
            e.Graphics.DrawString("SR_Qty", new Font("Calibri", 12, FontStyle.Bold), new SolidBrush(Color.White), startX + 505, startY + Offset);
            realwidth = realwidth + startX + 5;
            e.Graphics.FillRectangle(Brushes.Black, realwidth, startY + Offset, startX - 5, realheight);
            e.Graphics.DrawRectangle(Pens.Black, realwidth, startY + Offset, startX - 5, realheight);
            e.Graphics.DrawString("Avail", new Font("Calibri", 12, FontStyle.Bold), new SolidBrush(Color.White), startX + 560, startY + Offset);
            realwidth = realwidth + startX - 5;
            e.Graphics.FillRectangle(Brushes.Black, realwidth, startY + Offset, startX + 20, realheight);
            e.Graphics.DrawRectangle(Pens.Black, realwidth, startY + Offset, startX + 20, realheight);
            e.Graphics.DrawString("Avg_Price", new Font("Calibri", 12, FontStyle.Bold), new SolidBrush(Color.White), startX + 605, startY + Offset);
            realwidth = realwidth + startX + 20;
            e.Graphics.FillRectangle(Brushes.Black, realwidth, startY + Offset, startX + 30, realheight);
            e.Graphics.DrawRectangle(Pens.Black, realwidth, startY + Offset, startX + 30, realheight);
            e.Graphics.DrawString("Total", new Font("Calibri", 12, FontStyle.Bold), new SolidBrush(Color.White), startX + 675, startY + Offset);
            Offset = Offset + 20;
            int a = dataGridView3.Rows.Count;
            Boolean flag = true;
            for (; i < a; i++)
            {
                realwidth = 50;
                realheight = 20;
                if (flag)
                {
                    flag = false;
                    e.Graphics.DrawRectangle(Pens.Black, realwidth, startY + Offset, startX + 120, realheight);
                    e.Graphics.DrawString(Convert.ToString(dataGridView3.Rows[i].Cells[0].Value), new Font("Calibri", 12), new SolidBrush(Color.Black), startX, startY + Offset);
                    realwidth = realwidth + startX + 120;
                    e.Graphics.DrawRectangle(Pens.Black, realwidth, startY + Offset, startX + 11, realheight);
                    e.Graphics.DrawString(Convert.ToString(dataGridView3.Rows[i].Cells[1].Value), new Font("Calibri", 12), new SolidBrush(Color.Black), startX + 170, startY + Offset);
                    realwidth = realwidth + startX + 11;
                    e.Graphics.DrawRectangle(Pens.Black, realwidth, startY + Offset, startX + 35, realheight);
                    e.Graphics.DrawString(Convert.ToString(dataGridView3.Rows[i].Cells[2].Value), new Font("Calibri", 12), new SolidBrush(Color.Black), startX + 230, startY + Offset);
                    realwidth = realwidth + startX + 35;
                    e.Graphics.DrawRectangle(Pens.Black, realwidth, startY + Offset, startX - 5, realheight);
                    e.Graphics.DrawString(Convert.ToString(dataGridView3.Rows[i].Cells[3].Value +"-"+ dataGridView3.Rows[i].Cells[4].Value), new Font("Calibri", 12), new SolidBrush(Color.Black), startX + 315, startY + Offset);
                    realwidth = realwidth + startX - 5;
                    e.Graphics.DrawRectangle(Pens.Black, realwidth, startY + Offset, startX + 5, realheight);
                    e.Graphics.DrawString(Convert.ToString(dataGridView3.Rows[i].Cells[5].Value + "-" + dataGridView3.Rows[i].Cells[6].Value), new Font("Calibri", 12), new SolidBrush(Color.Black), startX + 360, startY + Offset);
                    realwidth = realwidth + startX + 5;
                    e.Graphics.DrawRectangle(Pens.Black, realwidth, startY + Offset, startX - 5, realheight);
                    e.Graphics.DrawString(Convert.ToString(dataGridView3.Rows[i].Cells[7].Value + "-" + dataGridView3.Rows[i].Cells[8].Value), new Font("Calibri", 12), new SolidBrush(Color.Black), startX + 415, startY + Offset);
                    realwidth = realwidth + startX - 5;
                    e.Graphics.DrawRectangle(Pens.Black, realwidth, startY + Offset, startX - 5, realheight);
                    e.Graphics.DrawString(Convert.ToString(dataGridView3.Rows[i].Cells[9].Value + "-" + dataGridView3.Rows[i].Cells[10].Value), new Font("Calibri", 12), new SolidBrush(Color.Black), startX + 460, startY + Offset);
                    realwidth = realwidth + startX - 5;
                    e.Graphics.DrawRectangle(Pens.Black, realwidth, startY + Offset, startX + 5, realheight);
                    e.Graphics.DrawString(Convert.ToString(dataGridView3.Rows[i].Cells[11].Value + "-" + dataGridView3.Rows[i].Cells[12].Value), new Font("Calibri", 12), new SolidBrush(Color.Black), startX + 505, startY + Offset);
                    realwidth = realwidth + startX + 5;
                    e.Graphics.DrawRectangle(Pens.Black, realwidth, startY + Offset, startX - 5, realheight);
                    e.Graphics.DrawString(Convert.ToString(dataGridView3.Rows[i].Cells[13].Value + "-" + dataGridView3.Rows[i].Cells[14].Value), new Font("Calibri", 12), new SolidBrush(Color.Black), startX + 560, startY + Offset);
                    realwidth = realwidth + startX - 5;
                    e.Graphics.DrawRectangle(Pens.Black, realwidth, startY + Offset, startX + 20, realheight);
                    e.Graphics.DrawString(Convert.ToString(dataGridView3.Rows[i].Cells[15].Value), new Font("Calibri", 12), new SolidBrush(Color.Black), startX + 605, startY + Offset);
                    realwidth = realwidth + startX + 20;
                    e.Graphics.DrawRectangle(Pens.Black, realwidth, startY + Offset, startX + 30, realheight);
                    e.Graphics.DrawString(Convert.ToString(dataGridView3.Rows[i].Cells[16].Value), new Font("Calibri", 12), new SolidBrush(Color.Black), startX + 675, startY + Offset);
                    total += Convert.ToDecimal(dataGridView3.Rows[i].Cells[16].Value);
                    Offset = Offset + 20;
                }
                else
                {
                    flag = true;
                    e.Graphics.FillRectangle(Brushes.Gray, realwidth, startY + Offset, startX + 120, realheight);
                    e.Graphics.DrawRectangle(Pens.Black, realwidth, startY + Offset, startX + 120, realheight);
                    e.Graphics.DrawString(Convert.ToString(dataGridView3.Rows[i].Cells[0].Value), new Font("Calibri", 12), new SolidBrush(Color.Black), startX, startY + Offset);
                    realwidth = realwidth + startX + 120;
                    e.Graphics.FillRectangle(Brushes.Gray, realwidth, startY + Offset, startX + 11, realheight);
                    e.Graphics.DrawRectangle(Pens.Black, realwidth, startY + Offset, startX + 11, realheight);
                    e.Graphics.DrawString(Convert.ToString(dataGridView3.Rows[i].Cells[1].Value), new Font("Calibri", 12), new SolidBrush(Color.Black), startX + 170, startY + Offset);
                    realwidth = realwidth + startX + 11;
                    e.Graphics.FillRectangle(Brushes.Gray, realwidth, startY + Offset, startX + 35, realheight);
                    e.Graphics.DrawRectangle(Pens.Black, realwidth, startY + Offset, startX + 35, realheight);
                    e.Graphics.DrawString(Convert.ToString(dataGridView3.Rows[i].Cells[2].Value), new Font("Calibri", 12), new SolidBrush(Color.Black), startX + 230, startY + Offset);
                    realwidth = realwidth + startX + 35;
                    e.Graphics.FillRectangle(Brushes.Gray, realwidth, startY + Offset, startX - 5, realheight);
                    e.Graphics.DrawRectangle(Pens.Black, realwidth, startY + Offset, startX - 5, realheight);
                    e.Graphics.DrawString(Convert.ToString(dataGridView3.Rows[i].Cells[3].Value + "-" + dataGridView3.Rows[i].Cells[4].Value), new Font("Calibri", 12), new SolidBrush(Color.Black), startX + 315, startY + Offset);
                    realwidth = realwidth + startX - 5;
                    e.Graphics.FillRectangle(Brushes.Gray, realwidth, startY + Offset, startX + 5, realheight);
                    e.Graphics.DrawRectangle(Pens.Black, realwidth, startY + Offset, startX + 5, realheight);
                    e.Graphics.DrawString(Convert.ToString(dataGridView3.Rows[i].Cells[5].Value + "-" + dataGridView3.Rows[i].Cells[6].Value), new Font("Calibri", 12), new SolidBrush(Color.Black), startX + 360, startY + Offset);
                    realwidth = realwidth + startX + 5;
                    e.Graphics.FillRectangle(Brushes.Gray, realwidth, startY + Offset, startX - 5, realheight);
                    e.Graphics.DrawRectangle(Pens.Black, realwidth, startY + Offset, startX - 5, realheight);
                    e.Graphics.DrawString(Convert.ToString(dataGridView3.Rows[i].Cells[7].Value + "-" + dataGridView3.Rows[i].Cells[8].Value), new Font("Calibri", 12), new SolidBrush(Color.Black), startX + 415, startY + Offset);
                    realwidth = realwidth + startX - 5;
                    e.Graphics.FillRectangle(Brushes.Gray, realwidth, startY + Offset, startX - 5, realheight);
                    e.Graphics.DrawRectangle(Pens.Black, realwidth, startY + Offset, startX - 5, realheight);
                    e.Graphics.DrawString(Convert.ToString(dataGridView3.Rows[i].Cells[9].Value + "-" + dataGridView3.Rows[i].Cells[10].Value), new Font("Calibri", 12), new SolidBrush(Color.Black), startX + 460, startY + Offset);
                    realwidth = realwidth + startX - 5;
                    e.Graphics.FillRectangle(Brushes.Gray, realwidth, startY + Offset, startX + 5, realheight);
                    e.Graphics.DrawRectangle(Pens.Black, realwidth, startY + Offset, startX + 5, realheight);
                    e.Graphics.DrawString(Convert.ToString(dataGridView3.Rows[i].Cells[11].Value + "-" + dataGridView3.Rows[i].Cells[12].Value), new Font("Calibri", 12), new SolidBrush(Color.Black), startX + 505, startY + Offset);
                    realwidth = realwidth + startX + 5;
                    e.Graphics.FillRectangle(Brushes.Gray, realwidth, startY + Offset, startX - 5, realheight);
                    e.Graphics.DrawRectangle(Pens.Black, realwidth, startY + Offset, startX - 5, realheight);
                    e.Graphics.DrawString(Convert.ToString(dataGridView3.Rows[i].Cells[13].Value + "-" + dataGridView3.Rows[i].Cells[14].Value), new Font("Calibri", 12), new SolidBrush(Color.Black), startX + 560, startY + Offset);
                    realwidth = realwidth + startX - 5;
                    e.Graphics.FillRectangle(Brushes.Gray, realwidth, startY + Offset, startX + 20, realheight);
                    e.Graphics.DrawRectangle(Pens.Black, realwidth, startY + Offset, startX + 20, realheight);
                    e.Graphics.DrawString(Convert.ToString(dataGridView3.Rows[i].Cells[15].Value), new Font("Calibri", 12), new SolidBrush(Color.Black), startX + 605, startY + Offset);
                    realwidth = realwidth + startX + 20;
                    e.Graphics.FillRectangle(Brushes.Gray, realwidth, startY + Offset, startX + 30, realheight);
                    e.Graphics.DrawRectangle(Pens.Black, realwidth, startY + Offset, startX + 30, realheight);
                    e.Graphics.DrawString(Convert.ToString(dataGridView3.Rows[i].Cells[16].Value), new Font("Calibri", 12), new SolidBrush(Color.Black), startX + 675, startY + Offset);
                    total += Convert.ToDecimal(dataGridView3.Rows[i].Cells[16].Value);
                    Offset = Offset + 20;
                }
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
            Offset = Offset + 20;
            e.Graphics.DrawString("Grand Total:", new Font("Calibri", 10), new SolidBrush(Color.Black), startX + 605, startY + Offset);
            e.Graphics.DrawString(total.ToString(), new Font("Calibri", 10), new SolidBrush(Color.Black), startX + 675, startY + Offset);
            Offset = Offset + 20;
            e.Graphics.DrawString("Signature:  __________________", new Font("Calibri", 10, FontStyle.Bold), new SolidBrush(Color.Black), startX, startY + Offset);
            e.Graphics.DrawString("Date & Time:  ", new Font("Calibri", 10, FontStyle.Bold), new SolidBrush(Color.Black), startX + 320, startY + Offset);
            e.Graphics.DrawString(DateTime.Now.ToString(), new Font("Calibri", 10), new SolidBrush(Color.Black), startX + 400, startY + Offset);
            Offset = Offset + 25;
            e.Graphics.DrawString("Developed By: Geeks IT Solutions 03126424443  ", new Font("Calibri", 8, FontStyle.Bold), new SolidBrush(Color.Black), startX + 280, startY + Offset + 5);
            e.Graphics.PageUnit = GraphicsUnit.Inch;
            i = 0;
        }
        private void btnsearch_Click(object sender, EventArgs e)
        {
            amt = 0;
            Login.con.Open();
            SqlCommand cmd65 = new SqlCommand("IF EXISTS(select * FROM sys.views where name = 'total') DROP VIEW total", Login.con);
            cmd65.ExecuteNonQuery();
            SqlCommand cmd = new SqlCommand("create view total as select ps.ps_id, psd.p_id, psd.pprice,psd.qty, CAST(ROUND((((psd.qty) * (psd.pprice)) - (((psd.qty) * (psd.pprice) * (isnull(psd.discount,0)))/100)),2) as decimal(18,2)) total from purchase_stock_detail2 psd, purchase_stock ps, product2 p where psd.p_id=p.p_id AND ps.ps_id=psd.ps_id AND ps.datetime Between '" + Convert.ToDateTime(dateTimePicker1.Text) + "' AND '" + Convert.ToDateTime(dateTimePicker2.Text).AddDays(1) + "'", Login.con);
            cmd.ExecuteNonQuery();
            Login.con.Close();
            string command1 = "SELECT p.p_id P_Code,p.p_name P_Name, (SELECT isnull(sum(ps.qty), 0) from purchase_stock_detail2 ps, purchase_stock pst where ps.p_id=p.p_id AND pst.ps_id=ps.ps_id AND pst.datetime Between '" + Convert.ToDateTime(dateTimePicker1.Text) + "' AND '" + Convert.ToDateTime(dateTimePicker2.Text).AddDays(1) + "') Purchases_Qty, (SELECT isnull(sum(pr.qty), 0) from purchase_return_detail2 pr, purchase_return prt where pr.p_id=p.p_id AND prt.pr_id=pr.pr_id AND prt.datetime Between '" + Convert.ToDateTime(dateTimePicker1.Text) + "' AND '" + Convert.ToDateTime(dateTimePicker2.Text).AddDays(1) + "') Purchases_Return_Qty,(select isnull(sum(bd.qty), 0) from breakage_detail2 bd, breakage_stock bdt where bd.p_id=p.p_id AND bdt.b_id=bd.b_id AND bdt.datetime Between '" + Convert.ToDateTime(dateTimePicker1.Text) + "' AND '" + Convert.ToDateTime(dateTimePicker2.Text).AddDays(1) + "') Breakage_Qty,(select isnull(sum(ss.qty), 0) from sale_stock_detail2 ss, sale_stock sst where ss.p_id=p.p_id AND sst.ss_id=ss.ss_id AND sst.datetime Between '" + Convert.ToDateTime(dateTimePicker1.Text) + "' AND '" + Convert.ToDateTime(dateTimePicker2.Text).AddDays(1) + "') Sales_Qty,  (select isnull(sum(sr.qty), 0) from sale_return_detail2 sr, sale_return srt where sr.p_id=p.p_id AND srt.sr_id=sr.sr_id AND srt.datetime Between '" + Convert.ToDateTime(dateTimePicker1.Text) + "' AND '" + Convert.ToDateTime(dateTimePicker2.Text).AddDays(1) + "') Sales_Return_Qty,(((SELECT isnull(sum(ps.qty), 0) from purchase_stock_detail2 ps, purchase_stock pst where ps.p_id=p.p_id AND pst.ps_id=ps.ps_id AND pst.datetime Between '" + Convert.ToDateTime(dateTimePicker1.Text) + "' AND '" + Convert.ToDateTime(dateTimePicker2.Text).AddDays(1) + "') + (select isnull(sum(sr.qty), 0) from sale_return_detail2 sr, sale_return srt where sr.p_id=p.p_id AND srt.sr_id=sr.sr_id AND srt.datetime Between '" + Convert.ToDateTime(dateTimePicker1.Text) + "' AND '" + Convert.ToDateTime(dateTimePicker2.Text).AddDays(1) + "')) - ((select isnull(sum(ss.qty), 0) from sale_stock_detail2 ss, sale_stock sst where ss.p_id=p.p_id AND sst.ss_id=ss.ss_id AND sst.datetime Between '" + Convert.ToDateTime(dateTimePicker1.Text) + "' AND '" + Convert.ToDateTime(dateTimePicker2.Text).AddDays(1) + "')+(select isnull(sum(bd.qty), 0) from breakage_detail2 bd, breakage_stock bdt where bd.p_id=p.p_id AND bdt.b_id=bd.b_id AND bdt.datetime Between '" + Convert.ToDateTime(dateTimePicker1.Text) + "' AND '" + Convert.ToDateTime(dateTimePicker2.Text).AddDays(1) + "') + (select isnull(sum(pr.qty), 0) from purchase_return_detail2 pr, purchase_return prt where pr.p_id=p.p_id AND prt.pr_id=pr.pr_id AND prt.datetime Between '" + Convert.ToDateTime(dateTimePicker1.Text) + "' AND '" + Convert.ToDateTime(dateTimePicker2.Text).AddDays(1) + "'))) Available, cast(round(isnull((select nullif(t.pprice,0)),0),2) as decimal(18,2)) Avg_Price, cast(round((isnull((select nullif(t.pprice,0)),0)*(((SELECT isnull(sum(ps.qty), 0) from purchase_stock_detail2 ps, purchase_stock pst where ps.p_id=p.p_id AND pst.ps_id=ps.ps_id AND pst.datetime Between '" + Convert.ToDateTime(dateTimePicker1.Text) + "' AND '" + Convert.ToDateTime(dateTimePicker2.Text).AddDays(1) + "') + (select isnull(sum(sr.qty), 0) from sale_return_detail2 sr, sale_return srt where sr.p_id=p.p_id AND srt.sr_id=sr.sr_id AND srt.datetime Between '" + Convert.ToDateTime(dateTimePicker1.Text) + "' AND '" + Convert.ToDateTime(dateTimePicker2.Text).AddDays(1) + "')) - ((select isnull(sum(ss.qty), 0) from sale_stock_detail2 ss, sale_stock sst where ss.p_id=p.p_id AND sst.ss_id=ss.ss_id AND sst.datetime Between '" + Convert.ToDateTime(dateTimePicker1.Text) + "' AND '" + Convert.ToDateTime(dateTimePicker2.Text).AddDays(1) + "')+(select isnull(sum(bd.qty), 0) from breakage_detail2 bd, breakage_stock bdt where bd.p_id=p.p_id AND bdt.b_id=bd.b_id AND bdt.datetime Between '" + Convert.ToDateTime(dateTimePicker1.Text) + "' AND '" + Convert.ToDateTime(dateTimePicker2.Text).AddDays(1) + "') + (select isnull(sum(pr.qty), 0) from purchase_return_detail2 pr, purchase_return prt where pr.p_id=p.p_id AND prt.pr_id=pr.pr_id AND prt.datetime Between '" + Convert.ToDateTime(dateTimePicker1.Text) + "' AND '" + Convert.ToDateTime(dateTimePicker2.Text).AddDays(1) + "')))),2) as decimal(18,2)) Total from product2 p, total t where p.p_id=t.p_id AND p.p_id Like'" + txtavailcode.Text + "%'";
             test.bindingcode(command1, dataGridView4, Login.con);
            Login.con.Open();
            SqlCommand cmd67 = new SqlCommand("DROP VIEW total", Login.con);
            cmd67.ExecuteNonQuery();
            Login.con.Close();
            for (int i = 0; i < dataGridView4.RowCount; i++)
            {
                amt += Convert.ToDecimal(dataGridView4.Rows[i].Cells[9].Value);
            }
            lbsanitary.Text = amt.ToString();
        }

        private void rdbtnavil2_CheckedChanged(object sender, EventArgs e)
        {
            if (rdbtnavil2.Checked == true)
            {
                amt = 0;
                dataGridView3.Enabled = label5.Enabled = label7.Enabled = btnprint.Enabled = btnenter.Enabled = cmbavailcode.Enabled = cmbsize2.Enabled = false;
                cmbavailname.Focus();
                Login.con.Open();
                SqlCommand cmd65 = new SqlCommand("IF EXISTS(select * FROM sys.views where name = 'total') DROP VIEW total", Login.con);
                cmd65.ExecuteNonQuery();
                SqlCommand cmd = new SqlCommand("create view total as select ps.ps_id, psd.p_id, psd.pprice,psd.qty, CAST(ROUND((((psd.qty) * (psd.pprice)) - (((psd.qty) * (psd.pprice) * (isnull(psd.discount,0)))/100)),2) as decimal(18,2)) total from purchase_stock_detail2 psd, purchase_stock ps, product2 p where psd.p_id=p.p_id AND ps.ps_id=psd.ps_id", Login.con);
                cmd.ExecuteNonQuery();
                Login.con.Close();
                string command1 = "SELECT Distinct p.p_id P_Code,p.p_name P_Name, (SELECT isnull(sum(ps.qty), 0) from purchase_stock_detail2 ps where ps.p_id=p.p_id) Purchases_Qty, (SELECT isnull(sum(pr.qty), 0) from purchase_return_detail2 pr where pr.p_id=p.p_id) Purchases_Return_Qty ,(select isnull(sum(bd.qty), 0) from breakage_detail2 bd where bd.p_id=p.p_id) Breakage_Qty,(select isnull(sum(ss.qty), 0) from sale_stock_detail2 ss where ss.p_id=p.p_id) Sales_Qty,  (select isnull(sum(sr.qty), 0) from sale_return_detail2 sr where sr.p_id=p.p_id) Sales_Return_Qty,(((SELECT isnull(sum(ps.qty), 0) from purchase_stock_detail2 ps where ps.p_id=p.p_id) + (select isnull(sum(sr.qty), 0) from sale_return_detail2 sr where sr.p_id=p.p_id)) - ((select isnull(sum(ss.qty), 0) from sale_stock_detail2 ss where ss.p_id=p.p_id)+(select isnull(sum(bd.qty), 0) from breakage_detail2 bd where bd.p_id=p.p_id) + (select isnull(sum(pr.qty), 0) from purchase_return_detail2 pr where pr.p_id=p.p_id))) Available, cast(round(isnull((select nullif(sum(t.total),0) from total t where p.p_id=t.p_id)/(SELECT nullif(sum(ps.qty), 0) from purchase_stock_detail2 ps where ps.p_id=p.p_id),0),2) as decimal(18,2)) Avg_Price, cast(round((isnull((select nullif(sum(t.total),0) from total t where p.p_id=t.p_id)/(SELECT nullif(sum(ps.qty), 0) from purchase_stock_detail2 ps where ps.p_id=p.p_id),0)*(((SELECT isnull(sum(ps.qty), 0) from purchase_stock_detail2 ps where ps.p_id=p.p_id) + (select isnull(sum(sr.qty), 0) from sale_return_detail2 sr where sr.p_id=p.p_id)) - ((select isnull(sum(ss.qty), 0) from sale_stock_detail2 ss where ss.p_id=p.p_id)+(select isnull(sum(bd.qty), 0) from breakage_detail2 bd where bd.p_id=p.p_id) + (select isnull(sum(pr.qty), 0) from purchase_return_detail2 pr where pr.p_id=p.p_id)))),2) as decimal(18,2)) Total from product2 p";
                test.bindingcode(command1, dataGridView4, Login.con);
                Login.con.Open();
                SqlCommand cmd67 = new SqlCommand("DROP VIEW total", Login.con);
                cmd67.ExecuteNonQuery();
                Login.con.Close();
                for (int i = 0; i < dataGridView4.RowCount; i++)
                {
                    amt += Convert.ToDecimal(dataGridView4.Rows[i].Cells[9].Value);
                }
                lbsanitary.Text = amt.ToString();
            }
            else
                dataGridView3.Enabled = label5.Enabled = label7.Enabled = btnprint.Enabled = btnenter.Enabled = cmbavailcode.Enabled = cmbsize2.Enabled = true;
            cmbavailcode.Text = txtavailcode.Text = cmbavailname.Text = "";
            cmbsize2.Items.Clear();
            cmbquality.Items.Clear();
        }

        private void txtavailcode_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                amt = 0;
                txtavailcode.Text = txtavailcode.Text.ToUpper();
                txtavailcode.SelectAll();
                Login.con.Open();
                SqlCommand cmd = new SqlCommand("SELECT p_name from product2 where p_id='" + txtavailcode.Text.ToUpper() + "'", Login.con);
                SqlDataReader dr = cmd.ExecuteReader();
                if (dr.HasRows)
                {
                    while (dr.Read())
                    {
                        cmbavailname.Text = Convert.ToString(dr[0]);
                    }
                    dr.Close();
                }
                else
                {
                    dr.Close();
                    MessageBox.Show("Record Not Found", "Verona Tiles & Sanitary", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                }
                Login.con.Close();
            }
        }


        private void txtavailcode_Leave(object sender, EventArgs e)
        {
            if (txtavailcode.Text == "")
            {
                cmbavailname.Text = "";
            }
            else
            {
                txtavailcode.Text = txtavailcode.Text.ToUpper();
                txtavailcode.SelectAll();
                Login.con.Open();
                SqlCommand cmd = new SqlCommand("SELECT p_name from product2 where p_id='" + txtavailcode.Text.ToUpper() + "'", Login.con);
                SqlDataReader dr = cmd.ExecuteReader();
                if (dr.HasRows)
                {
                    while (dr.Read())
                    {
                        cmbavailname.Text = Convert.ToString(dr[0]);
                    }
                    dr.Close();
                }
                else
                {
                    dr.Close();
                    MessageBox.Show("Record Not Found", "Verona Tiles & Sanitary", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                    cmbavailname.Text = "";
                    txtavailcode.Focus();
                }
                Login.con.Close();
            }
        }

        private void cmbavailname_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                amt = 0;
                cmbavailname.Text = cmbavailname.Text.ToUpper();
                cmbavailname.SelectAll();
                Login.con.Open();
                SqlCommand cmd = new SqlCommand("SELECT p_id from product2 where p_name='" + cmbavailname.Text.ToUpper() + "'", Login.con);
                SqlDataReader dr = cmd.ExecuteReader();
                if (dr.HasRows)
                {
                    while (dr.Read())
                    {
                        txtavailcode.Text = Convert.ToString(dr[0]);
                    }
                    dr.Close();
                }
                else
                {
                    dr.Close();
                    MessageBox.Show("Record Not Found", "Verona Tiles & Sanitary", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                }
                Login.con.Close();
            }
        }

        private void cmbavailname_Leave(object sender, EventArgs e)
        {
            if (cmbavailname.Text == "")
            {
                txtavailcode.Clear();
            }
            else
            {
                cmbavailname.Text = cmbavailname.Text.ToUpper();
                cmbavailname.SelectAll();
                Login.con.Open();
                SqlCommand cmd = new SqlCommand("SELECT p_id from product2 where p_name='" + cmbavailname.Text.ToUpper() + "'", Login.con);
                SqlDataReader dr = cmd.ExecuteReader();
                if (dr.HasRows)
                {
                    while (dr.Read())
                    {
                        txtavailcode.Text = Convert.ToString(dr[0]);
                    }
                    dr.Close();
                }
                else
                {
                    dr.Close();
                    MessageBox.Show("Record Not Found", "Verona Tiles & Sanitary", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                    txtavailcode.Clear();
                    cmbavailname.Focus();
                }
                Login.con.Close();
            }
        }

        private void btnprint2_Click(object sender, EventArgs e)
        {
            total = 0;
            try
            {
                printDocument2.Print();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            rdbtnavil2_CheckedChanged(this, null);
        }

        private void printDocument2_PrintPage(object sender, PrintPageEventArgs e)
        {
            Image imgPerson = Image.FromFile(@"D:\VR.png");
            e.Graphics.DrawImage(imgPerson, 100, 60);
            e.Graphics.DrawString("Verona Tiles & Sanitary", new Font("Arial", 28, FontStyle.Bold), Brushes.Black, 230, 50);
            e.Graphics.DrawString("IMPORTERS & GENERAL ORDER SUPPLIER", new Font("Arial", 10), Brushes.Black, 280, 90);
            e.Graphics.DrawString("Deals in All Kinds of Imported Sanitary Items", new Font("Arial", 9, FontStyle.Italic), Brushes.Black, 300, 105);
            e.Graphics.DrawString("Kangni Wala ByPass Near PSO Pump G.T Road Gujranwala", new Font("Arial", 10, FontStyle.Regular), Brushes.Black, 265, 125);
            e.Graphics.DrawString("Contact: ", new Font("Arial", 9, FontStyle.Bold), Brushes.Black, 265, 140);
            e.Graphics.DrawString("(055) 4296080 - 4296090", new Font("Arial", 9, FontStyle.Italic), Brushes.Black, 325, 140);
            int startX = 50;
            int startY = 150;
            int Offset = 40;
            e.Graphics.DrawString("Stock Detail", new Font("Calibri", 12, FontStyle.Underline), new SolidBrush(Color.Black), startX, startY + Offset);
            Offset = Offset + 30;
            string underLine = "-------------------------------------------------------------------------------------";
            e.Graphics.DrawString(underLine, new Font("Courier New", 10, FontStyle.Bold), new SolidBrush(Color.Black), startX, startY + Offset);
            Offset = Offset + 20;
            e.Graphics.DrawString("P_Code", new Font("Calibri", 10, FontStyle.Bold), new SolidBrush(Color.Black), startX, startY + Offset);
            e.Graphics.DrawString("P_Name", new Font("Calibri", 10, FontStyle.Bold), new SolidBrush(Color.Black), startX + 120, startY + Offset);
            e.Graphics.DrawString("P_Qty", new Font("Calibri", 10, FontStyle.Bold), new SolidBrush(Color.Black), startX + 310, startY + Offset);
            e.Graphics.DrawString("PR_Qty", new Font("Calibri", 10, FontStyle.Bold), new SolidBrush(Color.Black), startX + 360, startY + Offset);
            e.Graphics.DrawString("B_Qty", new Font("Calibri", 10, FontStyle.Bold), new SolidBrush(Color.Black), startX + 410, startY + Offset);
            e.Graphics.DrawString("S_Qty", new Font("Calibri", 10, FontStyle.Bold), new SolidBrush(Color.Black), startX + 460, startY + Offset);
            e.Graphics.DrawString("SR_Qty", new Font("Calibri", 10, FontStyle.Bold), new SolidBrush(Color.Black), startX + 510, startY + Offset);
            e.Graphics.DrawString("Avail", new Font("Calibri", 10, FontStyle.Bold), new SolidBrush(Color.Black), startX + 560, startY + Offset);
            e.Graphics.DrawString("Avg_Price", new Font("Calibri", 10, FontStyle.Bold), new SolidBrush(Color.Black), startX + 610, startY + Offset);
            e.Graphics.DrawString("Total", new Font("Calibri", 10, FontStyle.Bold), new SolidBrush(Color.Black), startX + 680, startY + Offset);
            Offset = Offset + 20;
            e.Graphics.DrawString(underLine, new Font("Courier New", 10, FontStyle.Bold), new SolidBrush(Color.Black), startX, startY + Offset);
            Offset = Offset + 20;
            int a = dataGridView4.Rows.Count;
            for (; i < a; i++)
            {
                e.Graphics.DrawString(Convert.ToString(dataGridView4.Rows[i].Cells[0].Value), new Font("Calibri", 9), new SolidBrush(Color.Black), startX, startY + Offset);
                e.Graphics.DrawString(Convert.ToString(dataGridView4.Rows[i].Cells[1].Value), new Font("Calibri", 9), new SolidBrush(Color.Black), startX + 120, startY + Offset);
                e.Graphics.DrawString(Convert.ToString(dataGridView4.Rows[i].Cells[2].Value), new Font("Calibri", 9), new SolidBrush(Color.Black), startX + 310, startY + Offset);
                e.Graphics.DrawString(Convert.ToString(dataGridView4.Rows[i].Cells[3].Value), new Font("Calibri", 9), new SolidBrush(Color.Black), startX + 360, startY + Offset);
                e.Graphics.DrawString(Convert.ToString(dataGridView4.Rows[i].Cells[4].Value), new Font("Calibri", 9), new SolidBrush(Color.Black), startX + 410, startY + Offset);
                e.Graphics.DrawString(Convert.ToString(dataGridView4.Rows[i].Cells[5].Value), new Font("Calibri", 9), new SolidBrush(Color.Black), startX + 460, startY + Offset);
                e.Graphics.DrawString(Convert.ToString(dataGridView4.Rows[i].Cells[6].Value), new Font("Calibri", 9), new SolidBrush(Color.Black), startX + 510, startY + Offset);
                e.Graphics.DrawString(Convert.ToString(dataGridView4.Rows[i].Cells[7].Value), new Font("Calibri", 9), new SolidBrush(Color.Black), startX + 560, startY + Offset);
                e.Graphics.DrawString(Convert.ToString(dataGridView4.Rows[i].Cells[8].Value), new Font("Calibri", 9), new SolidBrush(Color.Black), startX + 610, startY + Offset);
                e.Graphics.DrawString(Convert.ToString(dataGridView4.Rows[i].Cells[9].Value), new Font("Calibri", 9), new SolidBrush(Color.Black), startX + 680, startY + Offset);
                total += Convert.ToDecimal(dataGridView4.Rows[i].Cells[9].Value);
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
            e.Graphics.DrawString(total.ToString(), new Font("Calibri", 10), new SolidBrush(Color.Black), startX + 680, startY + Offset);
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
