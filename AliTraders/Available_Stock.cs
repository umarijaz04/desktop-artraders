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
    public partial class Available_Stock : Form
    {
        Form opener;
        UF.Class1 test = new UF.Class1();
        private int i = 0;
        public Available_Stock(Form form)
        {
            InitializeComponent();
            opener = form;
        }

        private void Available_Stock_Load(object sender, EventArgs e)
        {
            opener.Enabled = false;
            rdbtnavil2_CheckedChanged(this, null);
            string command = "SELECT Cast(p_id as varchar(50)) from product2 order by p_id";
            test.cmbox_drop(command, txtavailcode, Login.con);
            string command2 = "SELECT p_name from product2 order by p_name";
            test.cmbox_drop(command2, cmbavailname, Login.con);
        }

        private void Available_Stock_FormClosing(object sender, FormClosingEventArgs e)
        {
            opener.Enabled = true;
        }

        //private void rdbtnavil_CheckedChanged(object sender, EventArgs e)
        //{
        //if (rdbtnavil.Checked == true)
        //{
        //dataGridView4.Enabled = label4.Enabled = label25.Enabled = btnprint2.Enabled = txtavailcode.Enabled = cmbavailname.Enabled = false;
        //txtavailcode.Focus();
        //Login.con.Open();
        //SqlCommand cmd65 = new SqlCommand("IF EXISTS(select * FROM sys.views where name = 'avail_qty_sb_temp') DROP VIEW avail_qty_sb_temp", Login.con);
        //cmd65.ExecuteNonQuery();
        //SqlCommand cmd75 = new SqlCommand("IF EXISTS(select * FROM sys.views where name = 'avail_qty_st_temp') DROP VIEW avail_qty_st_temp", Login.con);
        //cmd75.ExecuteNonQuery();
        //SqlCommand cmd85 = new SqlCommand("IF EXISTS(select * FROM sys.views where name = 'avail_qty_cm_temp') DROP VIEW avail_qty_cm_temp", Login.con);
        //cmd85.ExecuteNonQuery();
        //SqlCommand cmd95 = new SqlCommand("IF EXISTS(select * FROM sys.views where name = 'avail_qty_pj_temp') DROP VIEW avail_qty_pj_temp", Login.con);
        //cmd95.ExecuteNonQuery();
        //SqlCommand cmd165 = new SqlCommand("IF EXISTS(select * FROM sys.views where name = 'avail_qty_sb') DROP VIEW avail_qty_sb", Login.con);
        //cmd165.ExecuteNonQuery();
        //SqlCommand cmd175 = new SqlCommand("IF EXISTS(select * FROM sys.views where name = 'avail_qty_st') DROP VIEW avail_qty_st", Login.con);
        //cmd175.ExecuteNonQuery();
        //SqlCommand cmd185 = new SqlCommand("IF EXISTS(select * FROM sys.views where name = 'avail_qty_cm') DROP VIEW avail_qty_cm", Login.con);
        //cmd185.ExecuteNonQuery();
        //SqlCommand cmd195 = new SqlCommand("IF EXISTS(select * FROM sys.views where name = 'avail_qty_pj') DROP VIEW avail_qty_pj", Login.con);
        //cmd195.ExecuteNonQuery();
        //SqlCommand cmd395 = new SqlCommand("IF EXISTS(select * FROM sys.views where name = 'stock') DROP VIEW stock", Login.con);
        //cmd395.ExecuteNonQuery();
        //SqlCommand cmd = new SqlCommand("Create VIEW [avail_qty_sb_temp] AS SELECT DISTINCT p.p_id P_Code,psd.tone Tone,psd.quality Quality,p.size Size, CAST(((((((SELECT isnull(sum(ps.qty), 0) from purchase_stock_detail ps where ps.p_id=psd.p_id AND ps.size=psd.size AND ps.quality='Superb' AND ps.tone=psd.tone) + CAST(((SELECT isnull(sum(ps.tiles), 0) from purchase_stock_detail ps where ps.p_id=psd.p_id AND ps.size=psd.size AND ps.quality='Superb' AND ps.tone=psd.tone)/(SELECT isnull(p.pieces, 0) from product p where p.p_id=psd.p_id AND p.size=psd.size))as float)) - ((SELECT isnull(sum(bd.qty), 0) from breakage_detail bd where bd.p_id=psd.p_id AND bd.size=psd.size AND bd.quality='Superb' AND bd.tone=psd.tone) + CAST(((SELECT isnull(sum(bd.tiles), 0) from breakage_detail bd where bd.p_id=psd.p_id AND bd.size=psd.size AND bd.quality='Superb' AND bd.tone=psd.tone)/(SELECT isnull(p.pieces, 0) from product p where p.p_id=psd.p_id AND p.size=psd.size))as float)) - ((SELECT isnull(sum(pr.qty), 0) from purchase_return_detail pr where pr.p_id=psd.p_id AND pr.size=psd.size AND pr.quality='Superb' AND pr.tone=psd.tone) + CAST(((SELECT isnull(sum(pr.tiles), 0) from purchase_return_detail pr where pr.p_id=psd.p_id AND pr.size=psd.size AND pr.quality='Superb' AND pr.tone=psd.tone)/(SELECT isnull(p.pieces, 0) from product p where p.p_id=psd.p_id AND p.size=psd.size))as float))) - (((SELECT isnull(sum(s.qty), 0) from sale_stock_detail s where s.p_id=psd.p_id AND s.size=psd.size AND s.quality='Superb' AND s.tone=psd.tone) + CAST(((SELECT isnull(sum(s.tiles), 0) from sale_stock_detail s where s.p_id=psd.p_id AND s.size=psd.size AND s.quality='Superb' AND s.tone=psd.tone)/(SELECT isnull(p.pieces, 0) from product p where p.p_id=psd.p_id AND p.size=psd.size))as float)) - ((SELECT isnull(sum(sr.qty), 0) from sale_return_detail sr where sr.p_id=psd.p_id AND sr.size=psd.size AND sr.quality='Superb' AND sr.tone=psd.tone) + CAST(((SELECT isnull(sum(sr.tiles), 0) from sale_return_detail sr where sr.p_id=psd.p_id AND sr.size=psd.size AND sr.quality='Superb' AND sr.tone=psd.tone )/(SELECT isnull(p.pieces, 0) from product p where p.p_id=psd.p_id AND p.size=psd.size))as float)))) * (SELECT isnull(p.pieces, 0) from product p where p.p_id=psd.p_id AND p.size=psd.size)) / (SELECT isnull(p.pieces, 0) from product p where p.p_id=psd.p_id AND p.size=psd.size))as int) SB_Qty, (CAST((((((SELECT isnull(sum(ps.qty), 0) from purchase_stock_detail ps where ps.p_id=psd.p_id AND ps.size=psd.size AND ps.quality='Superb' AND ps.tone=psd.tone) + CAST(((SELECT isnull(sum(ps.tiles), 0) from purchase_stock_detail ps where ps.p_id=psd.p_id AND ps.size=psd.size AND ps.quality='Superb' AND ps.tone=psd.tone)/(SELECT isnull(p.pieces, 0) from product p where p.p_id=psd.p_id AND p.size=psd.size))as float)) - ((SELECT isnull(sum(bd.qty), 0) from breakage_detail bd where bd.p_id=psd.p_id AND bd.size=psd.size AND bd.quality='Superb' AND bd.tone=psd.tone) + CAST(((SELECT isnull(sum(bd.tiles), 0) from breakage_detail bd where bd.p_id=psd.p_id AND bd.size=psd.size AND bd.quality='Superb' AND bd.tone=psd.tone)/(SELECT isnull(p.pieces, 0) from product p where p.p_id=psd.p_id AND p.size=psd.size))as float)) - ((SELECT isnull(sum(pr.qty), 0) from purchase_return_detail pr where pr.p_id=psd.p_id AND pr.size=psd.size AND pr.quality='Superb' AND pr.tone=psd.tone) + CAST(((SELECT isnull(sum(pr.tiles), 0) from purchase_return_detail pr where pr.p_id=psd.p_id AND pr.size=psd.size AND pr.quality='Superb' AND pr.tone=psd.tone)/(SELECT isnull(p.pieces, 0) from product p where p.p_id=psd.p_id AND p.size=psd.size))as float))) - (((SELECT isnull(sum(s.qty), 0) from sale_stock_detail s where s.p_id=psd.p_id AND s.size=psd.size AND s.quality='Superb' AND s.tone=psd.tone) + CAST(((SELECT isnull(sum(s.tiles), 0) from sale_stock_detail s where s.p_id=psd.p_id AND s.size=psd.size AND s.quality='Superb' AND s.tone=psd.tone)/(SELECT isnull(p.pieces, 0) from product p where p.p_id=psd.p_id AND p.size=psd.size))as float)) - ((SELECT isnull(sum(sr.qty), 0) from sale_return_detail sr where sr.p_id=psd.p_id AND sr.size=psd.size AND sr.quality='Superb' AND sr.tone=psd.tone) + CAST(((SELECT isnull(sum(sr.tiles), 0) from sale_return_detail sr where sr.p_id=psd.p_id AND sr.size=psd.size AND sr.quality='Superb' AND sr.tone=psd.tone)/(SELECT isnull(p.pieces, 0) from product p where p.p_id=psd.p_id AND p.size=psd.size))as float)))) * (SELECT isnull(p.pieces, 0) from product p where p.p_id=psd.p_id AND p.size=psd.size)) as int) % (SELECT isnull(p.pieces, 0) from product p where p.p_id=psd.p_id AND p.size=psd.size)) SB_Tiles ,row_number() over(partition by psd.tone,psd.size,psd.quality,psd.p_id Order by psd.size) as rn  from product p, purchase_stock_detail psd where psd.p_id=p.p_id AND psd.size=p.size", Login.con);
        //cmd.ExecuteNonQuery();
        //SqlCommand cmd2 = new SqlCommand("Create VIEW [avail_qty_st_temp] AS SELECT DISTINCT p.p_id P_Code,psd.tone Tone,psd.quality Quality,p.size Size, CAST(((((((SELECT isnull(sum(ps.qty), 0) from purchase_stock_detail ps where ps.p_id=psd.p_id AND ps.size=psd.size AND ps.quality='Standard' AND ps.tone=psd.tone) + CAST(((SELECT isnull(sum(ps.tiles), 0) from purchase_stock_detail ps where ps.p_id=psd.p_id AND ps.size=psd.size AND ps.quality='Standard' AND ps.tone=psd.tone)/(SELECT isnull(p.pieces, 0) from product p where p.p_id=psd.p_id AND p.size=psd.size))as float)) - ((SELECT isnull(sum(bd.qty), 0) from breakage_detail bd where bd.p_id=psd.p_id AND bd.size=psd.size AND bd.quality='Standard' AND bd.tone=psd.tone) + CAST(((SELECT isnull(sum(bd.tiles), 0) from breakage_detail bd where bd.p_id=psd.p_id AND bd.size=psd.size AND bd.quality='Standard' AND bd.tone=psd.tone)/(SELECT isnull(p.pieces, 0) from product p where p.p_id=psd.p_id AND p.size=psd.size))as float)) - ((SELECT isnull(sum(pr.qty), 0) from purchase_return_detail pr where pr.p_id=psd.p_id AND pr.size=psd.size AND pr.quality='Standard' AND pr.tone=psd.tone) + CAST(((SELECT isnull(sum(pr.tiles), 0) from purchase_return_detail pr where pr.p_id=psd.p_id AND pr.size=psd.size AND pr.quality='Standard' AND pr.tone=psd.tone)/(SELECT isnull(p.pieces, 0) from product p where p.p_id=psd.p_id AND p.size=psd.size))as float))) - (((SELECT isnull(sum(s.qty), 0) from sale_stock_detail s where s.p_id=psd.p_id AND s.size=psd.size AND s.quality='Standard' AND s.tone=psd.tone) + CAST(((SELECT isnull(sum(s.tiles), 0) from sale_stock_detail s where s.p_id=psd.p_id AND s.size=psd.size AND s.quality='Standard' AND s.tone=psd.tone)/(SELECT isnull(p.pieces, 0) from product p where p.p_id=psd.p_id AND p.size=psd.size))as float)) - ((SELECT isnull(sum(sr.qty), 0) from sale_return_detail sr where sr.p_id=psd.p_id AND sr.size=psd.size AND sr.quality='Standard' AND sr.tone=psd.tone) + CAST(((SELECT isnull(sum(sr.tiles), 0) from sale_return_detail sr where sr.p_id=psd.p_id AND sr.size=psd.size AND sr.quality='Standard' AND sr.tone=psd.tone )/(SELECT isnull(p.pieces, 0) from product p where p.p_id=psd.p_id AND p.size=psd.size))as float)))) * (SELECT isnull(p.pieces, 0) from product p where p.p_id=psd.p_id AND p.size=psd.size)) / (SELECT isnull(p.pieces, 0) from product p where p.p_id=psd.p_id AND p.size=psd.size))as int) ST_Qty, (CAST((((((SELECT isnull(sum(ps.qty), 0) from purchase_stock_detail ps where ps.p_id=psd.p_id AND ps.size=psd.size AND ps.quality='Standard' AND ps.tone=psd.tone) + CAST(((SELECT isnull(sum(ps.tiles), 0) from purchase_stock_detail ps where ps.p_id=psd.p_id AND ps.size=psd.size AND ps.quality='Standard' AND ps.tone=psd.tone)/(SELECT isnull(p.pieces, 0) from product p where p.p_id=psd.p_id AND p.size=psd.size))as float)) - ((SELECT isnull(sum(bd.qty), 0) from breakage_detail bd where bd.p_id=psd.p_id AND bd.size=psd.size AND bd.quality='Standard' AND bd.tone=psd.tone) + CAST(((SELECT isnull(sum(bd.tiles), 0) from breakage_detail bd where bd.p_id=psd.p_id AND bd.size=psd.size AND bd.quality='Standard' AND bd.tone=psd.tone)/(SELECT isnull(p.pieces, 0) from product p where p.p_id=psd.p_id AND p.size=psd.size))as float)) - ((SELECT isnull(sum(pr.qty), 0) from purchase_return_detail pr where pr.p_id=psd.p_id AND pr.size=psd.size AND pr.quality='Standard' AND pr.tone=psd.tone) + CAST(((SELECT isnull(sum(pr.tiles), 0) from purchase_return_detail pr where pr.p_id=psd.p_id AND pr.size=psd.size AND pr.quality='Standard' AND pr.tone=psd.tone)/(SELECT isnull(p.pieces, 0) from product p where p.p_id=psd.p_id AND p.size=psd.size))as float))) - (((SELECT isnull(sum(s.qty), 0) from sale_stock_detail s where s.p_id=psd.p_id AND s.size=psd.size AND s.quality='Standard' AND s.tone=psd.tone) + CAST(((SELECT isnull(sum(s.tiles), 0) from sale_stock_detail s where s.p_id=psd.p_id AND s.size=psd.size AND s.quality='Standard' AND s.tone=psd.tone)/(SELECT isnull(p.pieces, 0) from product p where p.p_id=psd.p_id AND p.size=psd.size))as float)) - ((SELECT isnull(sum(sr.qty), 0) from sale_return_detail sr where sr.p_id=psd.p_id AND sr.size=psd.size AND sr.quality='Standard' AND sr.tone=psd.tone) + CAST(((SELECT isnull(sum(sr.tiles), 0) from sale_return_detail sr where sr.p_id=psd.p_id AND sr.size=psd.size AND sr.quality='Standard' AND sr.tone=psd.tone)/(SELECT isnull(p.pieces, 0) from product p where p.p_id=psd.p_id AND p.size=psd.size))as float)))) * (SELECT isnull(p.pieces, 0) from product p where p.p_id=psd.p_id AND p.size=psd.size)) as int) % (SELECT isnull(p.pieces, 0) from product p where p.p_id=psd.p_id AND p.size=psd.size)) ST_Tiles ,row_number() over(partition by psd.tone,psd.size,psd.quality,psd.p_id Order by psd.size) as rn  from product p, purchase_stock_detail psd where psd.p_id=p.p_id AND psd.size=p.size", Login.con);
        //cmd2.ExecuteNonQuery();
        //SqlCommand cmd3 = new SqlCommand("Create VIEW [avail_qty_cm_temp] AS SELECT DISTINCT p.p_id P_Code,psd.tone Tone,psd.quality Quality,p.size Size, CAST(((((((SELECT isnull(sum(ps.qty), 0) from purchase_stock_detail ps where ps.p_id=psd.p_id AND ps.size=psd.size AND ps.quality='Commercial' AND ps.tone=psd.tone) + CAST(((SELECT isnull(sum(ps.tiles), 0) from purchase_stock_detail ps where ps.p_id=psd.p_id AND ps.size=psd.size AND ps.quality='Commercial' AND ps.tone=psd.tone)/(SELECT isnull(p.pieces, 0) from product p where p.p_id=psd.p_id AND p.size=psd.size))as float)) - ((SELECT isnull(sum(bd.qty), 0) from breakage_detail bd where bd.p_id=psd.p_id AND bd.size=psd.size AND bd.quality='Commercial' AND bd.tone=psd.tone) + CAST(((SELECT isnull(sum(bd.tiles), 0) from breakage_detail bd where bd.p_id=psd.p_id AND bd.size=psd.size AND bd.quality='Commercial' AND bd.tone=psd.tone)/(SELECT isnull(p.pieces, 0) from product p where p.p_id=psd.p_id AND p.size=psd.size))as float)) - ((SELECT isnull(sum(pr.qty), 0) from purchase_return_detail pr where pr.p_id=psd.p_id AND pr.size=psd.size AND pr.quality='Commercial' AND pr.tone=psd.tone) + CAST(((SELECT isnull(sum(pr.tiles), 0) from purchase_return_detail pr where pr.p_id=psd.p_id AND pr.size=psd.size AND pr.quality='Commercial' AND pr.tone=psd.tone)/(SELECT isnull(p.pieces, 0) from product p where p.p_id=psd.p_id AND p.size=psd.size))as float))) - (((SELECT isnull(sum(s.qty), 0) from sale_stock_detail s where s.p_id=psd.p_id AND s.size=psd.size AND s.quality='Commercial' AND s.tone=psd.tone) + CAST(((SELECT isnull(sum(s.tiles), 0) from sale_stock_detail s where s.p_id=psd.p_id AND s.size=psd.size AND s.quality='Commercial' AND s.tone=psd.tone)/(SELECT isnull(p.pieces, 0) from product p where p.p_id=psd.p_id AND p.size=psd.size))as float)) - ((SELECT isnull(sum(sr.qty), 0) from sale_return_detail sr where sr.p_id=psd.p_id AND sr.size=psd.size AND sr.quality='Commercial' AND sr.tone=psd.tone) + CAST(((SELECT isnull(sum(sr.tiles), 0) from sale_return_detail sr where sr.p_id=psd.p_id AND sr.size=psd.size AND sr.quality='Commercial' AND sr.tone=psd.tone )/(SELECT isnull(p.pieces, 0) from product p where p.p_id=psd.p_id AND p.size=psd.size))as float)))) * (SELECT isnull(p.pieces, 0) from product p where p.p_id=psd.p_id AND p.size=psd.size)) / (SELECT isnull(p.pieces, 0) from product p where p.p_id=psd.p_id AND p.size=psd.size))as int) CM_Qty, (CAST((((((SELECT isnull(sum(ps.qty), 0) from purchase_stock_detail ps where ps.p_id=psd.p_id AND ps.size=psd.size AND ps.quality='Commercial' AND ps.tone=psd.tone) + CAST(((SELECT isnull(sum(ps.tiles), 0) from purchase_stock_detail ps where ps.p_id=psd.p_id AND ps.size=psd.size AND ps.quality='Commercial' AND ps.tone=psd.tone)/(SELECT isnull(p.pieces, 0) from product p where p.p_id=psd.p_id AND p.size=psd.size))as float)) - ((SELECT isnull(sum(bd.qty), 0) from breakage_detail bd where bd.p_id=psd.p_id AND bd.size=psd.size AND bd.quality='Commercial' AND bd.tone=psd.tone) + CAST(((SELECT isnull(sum(bd.tiles), 0) from breakage_detail bd where bd.p_id=psd.p_id AND bd.size=psd.size AND bd.quality='Commercial' AND bd.tone=psd.tone)/(SELECT isnull(p.pieces, 0) from product p where p.p_id=psd.p_id AND p.size=psd.size))as float)) - ((SELECT isnull(sum(pr.qty), 0) from purchase_return_detail pr where pr.p_id=psd.p_id AND pr.size=psd.size AND pr.quality='Commercial' AND pr.tone=psd.tone) + CAST(((SELECT isnull(sum(pr.tiles), 0) from purchase_return_detail pr where pr.p_id=psd.p_id AND pr.size=psd.size AND pr.quality='Commercial' AND pr.tone=psd.tone)/(SELECT isnull(p.pieces, 0) from product p where p.p_id=psd.p_id AND p.size=psd.size))as float))) - (((SELECT isnull(sum(s.qty), 0) from sale_stock_detail s where s.p_id=psd.p_id AND s.size=psd.size AND s.quality='Commercial' AND s.tone=psd.tone) + CAST(((SELECT isnull(sum(s.tiles), 0) from sale_stock_detail s where s.p_id=psd.p_id AND s.size=psd.size AND s.quality='Commercial' AND s.tone=psd.tone)/(SELECT isnull(p.pieces, 0) from product p where p.p_id=psd.p_id AND p.size=psd.size))as float)) - ((SELECT isnull(sum(sr.qty), 0) from sale_return_detail sr where sr.p_id=psd.p_id AND sr.size=psd.size AND sr.quality='Commercial' AND sr.tone=psd.tone) + CAST(((SELECT isnull(sum(sr.tiles), 0) from sale_return_detail sr where sr.p_id=psd.p_id AND sr.size=psd.size AND sr.quality='Commercial' AND sr.tone=psd.tone)/(SELECT isnull(p.pieces, 0) from product p where p.p_id=psd.p_id AND p.size=psd.size))as float)))) * (SELECT isnull(p.pieces, 0) from product p where p.p_id=psd.p_id AND p.size=psd.size)) as int) % (SELECT isnull(p.pieces, 0) from product p where p.p_id=psd.p_id AND p.size=psd.size)) CM_Tiles ,row_number() over(partition by psd.tone,psd.size,psd.quality,psd.p_id Order by psd.size) as rn  from product p, purchase_stock_detail psd where psd.p_id=p.p_id AND psd.size=p.size", Login.con);
        //cmd3.ExecuteNonQuery();
        //SqlCommand cmd4 = new SqlCommand("Create VIEW [avail_qty_pj_temp] AS SELECT DISTINCT p.p_id P_Code,psd.tone Tone,psd.quality Quality,p.size Size, CAST(((((((SELECT isnull(sum(ps.qty), 0) from purchase_stock_detail ps where ps.p_id=psd.p_id AND ps.size=psd.size AND ps.quality='Project' AND ps.tone=psd.tone) + CAST(((SELECT isnull(sum(ps.tiles), 0) from purchase_stock_detail ps where ps.p_id=psd.p_id AND ps.size=psd.size AND ps.quality='Project' AND ps.tone=psd.tone)/(SELECT isnull(p.pieces, 0) from product p where p.p_id=psd.p_id AND p.size=psd.size))as float)) - ((SELECT isnull(sum(bd.qty), 0) from breakage_detail bd where bd.p_id=psd.p_id AND bd.size=psd.size AND bd.quality='Project' AND bd.tone=psd.tone) + CAST(((SELECT isnull(sum(bd.tiles), 0) from breakage_detail bd where bd.p_id=psd.p_id AND bd.size=psd.size AND bd.quality='Project' AND bd.tone=psd.tone)/(SELECT isnull(p.pieces, 0) from product p where p.p_id=psd.p_id AND p.size=psd.size))as float)) - ((SELECT isnull(sum(pr.qty), 0) from purchase_return_detail pr where pr.p_id=psd.p_id AND pr.size=psd.size AND pr.quality='Project' AND pr.tone=psd.tone) + CAST(((SELECT isnull(sum(pr.tiles), 0) from purchase_return_detail pr where pr.p_id=psd.p_id AND pr.size=psd.size AND pr.quality='Project' AND pr.tone=psd.tone)/(SELECT isnull(p.pieces, 0) from product p where p.p_id=psd.p_id AND p.size=psd.size))as float))) - (((SELECT isnull(sum(s.qty), 0) from sale_stock_detail s where s.p_id=psd.p_id AND s.size=psd.size AND s.quality='Project' AND s.tone=psd.tone) + CAST(((SELECT isnull(sum(s.tiles), 0) from sale_stock_detail s where s.p_id=psd.p_id AND s.size=psd.size AND s.quality='Project' AND s.tone=psd.tone)/(SELECT isnull(p.pieces, 0) from product p where p.p_id=psd.p_id AND p.size=psd.size))as float)) - ((SELECT isnull(sum(sr.qty), 0) from sale_return_detail sr where sr.p_id=psd.p_id AND sr.size=psd.size AND sr.quality='Project' AND sr.tone=psd.tone) + CAST(((SELECT isnull(sum(sr.tiles), 0) from sale_return_detail sr where sr.p_id=psd.p_id AND sr.size=psd.size AND sr.quality='Project' AND sr.tone=psd.tone )/(SELECT isnull(p.pieces, 0) from product p where p.p_id=psd.p_id AND p.size=psd.size))as float)))) * (SELECT isnull(p.pieces, 0) from product p where p.p_id=psd.p_id AND p.size=psd.size)) / (SELECT isnull(p.pieces, 0) from product p where p.p_id=psd.p_id AND p.size=psd.size))as int) PJ_Qty, (CAST((((((SELECT isnull(sum(ps.qty), 0) from purchase_stock_detail ps where ps.p_id=psd.p_id AND ps.size=psd.size AND ps.quality='Project' AND ps.tone=psd.tone) + CAST(((SELECT isnull(sum(ps.tiles), 0) from purchase_stock_detail ps where ps.p_id=psd.p_id AND ps.size=psd.size AND ps.quality='Project' AND ps.tone=psd.tone)/(SELECT isnull(p.pieces, 0) from product p where p.p_id=psd.p_id AND p.size=psd.size))as float)) - ((SELECT isnull(sum(bd.qty), 0) from breakage_detail bd where bd.p_id=psd.p_id AND bd.size=psd.size AND bd.quality='Project' AND bd.tone=psd.tone) + CAST(((SELECT isnull(sum(bd.tiles), 0) from breakage_detail bd where bd.p_id=psd.p_id AND bd.size=psd.size AND bd.quality='Project' AND bd.tone=psd.tone)/(SELECT isnull(p.pieces, 0) from product p where p.p_id=psd.p_id AND p.size=psd.size))as float)) - ((SELECT isnull(sum(pr.qty), 0) from purchase_return_detail pr where pr.p_id=psd.p_id AND pr.size=psd.size AND pr.quality='Project' AND pr.tone=psd.tone) + CAST(((SELECT isnull(sum(pr.tiles), 0) from purchase_return_detail pr where pr.p_id=psd.p_id AND pr.size=psd.size AND pr.quality='Project' AND pr.tone=psd.tone)/(SELECT isnull(p.pieces, 0) from product p where p.p_id=psd.p_id AND p.size=psd.size))as float))) - (((SELECT isnull(sum(s.qty), 0) from sale_stock_detail s where s.p_id=psd.p_id AND s.size=psd.size AND s.quality='Project' AND s.tone=psd.tone) + CAST(((SELECT isnull(sum(s.tiles), 0) from sale_stock_detail s where s.p_id=psd.p_id AND s.size=psd.size AND s.quality='Project' AND s.tone=psd.tone)/(SELECT isnull(p.pieces, 0) from product p where p.p_id=psd.p_id AND p.size=psd.size))as float)) - ((SELECT isnull(sum(sr.qty), 0) from sale_return_detail sr where sr.p_id=psd.p_id AND sr.size=psd.size AND sr.quality='Project' AND sr.tone=psd.tone) + CAST(((SELECT isnull(sum(sr.tiles), 0) from sale_return_detail sr where sr.p_id=psd.p_id AND sr.size=psd.size AND sr.quality='Project' AND sr.tone=psd.tone)/(SELECT isnull(p.pieces, 0) from product p where p.p_id=psd.p_id AND p.size=psd.size))as float)))) * (SELECT isnull(p.pieces, 0) from product p where p.p_id=psd.p_id AND p.size=psd.size)) as int) % (SELECT isnull(p.pieces, 0) from product p where p.p_id=psd.p_id AND p.size=psd.size)) PJ_Tiles ,row_number() over(partition by psd.tone,psd.size,psd.quality,psd.p_id Order by psd.size) as rn  from product p, purchase_stock_detail psd where psd.p_id=p.p_id AND psd.size=p.size", Login.con);
        //cmd4.ExecuteNonQuery();
        //SqlCommand cmd11 = new SqlCommand("Create VIEW [avail_qty_sb] AS SELECT DISTINCT ROW_NUMBER() OVER (ORDER BY sb.P_Code, sb.Size) sr,sb.P_Code,sb.Tone,sb.Quality,sb.Size,sb.SB_Qty, sb.SB_Tiles, CAST(ROUND((p.meters*(sb.SB_Qty + (sb.SB_Tiles / p.pieces))),2) as decimal(18,2)) SB_Mtrs from avail_qty_sb_temp sb ,product p where sb.p_Code=p.p_id AND sb.Size=p.size", Login.con);
        //cmd11.ExecuteNonQuery();
        //SqlCommand cmd12 = new SqlCommand("Create VIEW [avail_qty_st] AS SELECT DISTINCT ROW_NUMBER() OVER (ORDER BY st.P_Code, st.Size) sr,st.P_Code,st.Tone,st.Quality,st.Size,st.ST_Qty,st.ST_Tiles,CAST(ROUND((p.meters*(st.ST_Qty + (st.ST_Tiles / p.pieces))),2) as decimal(18,2)) ST_Mtrs from avail_qty_st_temp st ,product p where st.p_Code=p.p_id AND st.Size=p.size", Login.con);
        //cmd12.ExecuteNonQuery();
        //SqlCommand cmd13 = new SqlCommand("Create VIEW [avail_qty_cm] AS SELECT DISTINCT ROW_NUMBER() OVER (ORDER BY cm.P_Code, cm.Size) sr,cm.P_Code,cm.Tone,cm.Quality,cm.Size,cm.CM_Qty,cm.CM_Tiles,CAST(ROUND((p.meters*(cm.CM_Qty + (cm.CM_Tiles / p.pieces))),2) as decimal(18,2)) CM_Mtrs from avail_qty_cm_temp cm ,product p where cm.p_Code=p.p_id AND cm.Size=p.size ", Login.con);
        //cmd13.ExecuteNonQuery();
        //SqlCommand cmd14 = new SqlCommand("Create VIEW [avail_qty_pj] AS SELECT DISTINCT ROW_NUMBER() OVER (ORDER BY pj.P_Code, pj.Size) sr,pj.P_Code,pj.Tone,pj.Quality,pj.Size,pj.PJ_Qty,pj.PJ_Tiles,CAST(ROUND((p.meters*(pj.PJ_Qty + (pj.PJ_Tiles / p.pieces))),2) as decimal(18,2)) PJ_Mtrs from avail_qty_pj_temp pj ,product p where pj.p_Code=p.p_id AND pj.Size=p.size ", Login.con);
        //cmd14.ExecuteNonQuery();
        //SqlCommand cmd51 = new SqlCommand("Create VIEW [stock] AS SELECT DISTINCT sb.P_Code,sb.Tone,sb.Size,sb.SB_Qty,sb.SB_Tiles,sb.SB_Mtrs,st.ST_Qty,st.ST_Tiles,st.ST_Mtrs,cm.CM_Qty,cm.CM_Tiles,cm.CM_Mtrs,pj.PJ_Qty,pj.PJ_Tiles,pj.PJ_Mtrs from avail_qty_sb sb ,avail_qty_st st,avail_qty_cm cm,avail_qty_pj pj where sb.sr=st.sr AND sb.sr=cm.sr AND sb.sr=pj.sr", Login.con);
        //cmd51.ExecuteNonQuery();
        //Login.con.Close();
        //}
        //else
        //dataGridView4.Enabled = label4.Enabled = label25.Enabled = btnprint2.Enabled = txtavailcode.Enabled = cmbavailname.Enabled = true;
        //txtavailcode.Text = cmbavailname.Text = "";
        //cmbsize2.Items.Clear();
        //cmbtone.Items.Clear();
        //}

        //private void cmbsize2_Enter(object sender, EventArgs e)
        //{
        //    string command2 = "SELECT DISTINCT size from product order by size";
        //    test.cmbox_drop(command2, cmbsize2, Login.con);
        //}

        //private void cmbtone_Enter(object sender, EventArgs e)
        //{
        //    string command = "select DISTINCT tone from purchase_stock_detail";
        //    test.cmbox_drop(command, cmbtone, Login.con);
        //}

        //private void btnenter_Click(object sender, EventArgs e)
        //{
        //    Login.con.Open();
        //    string cmd5 = "SELECT DISTINCT ROW_NUMBER() OVER (ORDER BY SUBSTRING(P_Code, 5, 10)) Sr, P_Code,Tone,Size,SB_Qty,SB_Tiles,SB_Mtrs,ST_Qty,ST_Tiles,ST_Mtrs,CM_Qty,CM_Tiles,CM_Mtrs,PJ_Qty,PJ_Tiles,PJ_Mtrs from stock where SIZE like '" + cmbsize2.Text + "%' AND TONE like '" + cmbtone.Text + "%' AND P_CODE like '" + cmbavailcode.Text.ToUpper() + "%' AND ((SB_Mtrs <> 0.0) OR (ST_Mtrs <> 0.0) OR (CM_Mtrs <> 0.0) OR (PJ_Mtrs <> 0.0))";
        //    test.bindingcode(cmd5, dataGridView3, Login.con);
        //    Login.con.Close();
        //    if (cmbavailcode.Text == "" && cmbtone.Text == "" && cmbsize2.Text != "")
        //            btnprint.Enabled = true;
        //        else        
        //            btnprint.Enabled = false;
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

        private void printDocument1_PrintPage(object sender, PrintPageEventArgs e)
        {
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
            //int realwidth = 50;
            //int realheight = 20;
            //e.Graphics.DrawString("Stock Report", new Font("Calibri", 12, FontStyle.Underline), new SolidBrush(Color.Black), startX, startY + Offset);
            //Offset = Offset + 30;
            //if (cmbsize2.Text != "")
            //{
            //    e.Graphics.FillRectangle(Brushes.Black, realwidth + 320, startY + Offset + 1, startX + 32, realheight);
            //    e.Graphics.DrawRectangle(Pens.Black, realwidth + 320, startY + Offset + 1, startX + 32, realheight);
            //    e.Graphics.DrawString("Size:", new Font("Calibri", 12, FontStyle.Bold), new SolidBrush(Color.White), startX + 320, startY + Offset);
            //    e.Graphics.DrawString(Convert.ToString(cmbsize2.Text), new Font("Calibri", 12, FontStyle.Bold), new SolidBrush(Color.White), startX + 355, startY + Offset);
            //}
            //Offset = Offset + 25;
            //e.Graphics.FillRectangle(Brushes.Black, realwidth, startY + Offset, startX + 120, realheight);
            //e.Graphics.DrawRectangle(Pens.Black, realwidth, startY + Offset, startX + 120, realheight);
            //e.Graphics.DrawString("P_Code", new Font("Calibri", 12, FontStyle.Bold), new SolidBrush(Color.White), startX, startY + Offset);
            //realwidth = realwidth + startX + 120;
            //e.Graphics.FillRectangle(Brushes.Black, realwidth, startY + Offset, startX + 11, realheight);
            //e.Graphics.DrawRectangle(Pens.Black, realwidth, startY + Offset, startX + 11, realheight);
            //e.Graphics.DrawString("Tone", new Font("Calibri", 12, FontStyle.Bold), new SolidBrush(Color.White), startX + 170, startY + Offset);
            //realwidth = realwidth + startX + 11;
            //e.Graphics.FillRectangle(Brushes.Black, realwidth, startY + Offset, startX, realheight);
            //e.Graphics.DrawRectangle(Pens.Black, realwidth, startY + Offset, startX, realheight);
            //e.Graphics.DrawString("SB", new Font("Calibri", 12, FontStyle.Bold), new SolidBrush(Color.White), startX + 230, startY + Offset);
            //realwidth = realwidth + startX;
            //e.Graphics.FillRectangle(Brushes.Black, realwidth, startY + Offset, startX + 20, realheight);
            //e.Graphics.DrawRectangle(Pens.Black, realwidth, startY + Offset, startX + 20, realheight);
            //e.Graphics.DrawString("SB_Mtrs", new Font("Calibri", 12, FontStyle.Bold), new SolidBrush(Color.White), startX + 280, startY + Offset);
            //realwidth = realwidth + startX + 20;
            //e.Graphics.FillRectangle(Brushes.Black, realwidth, startY + Offset, startX, realheight);
            //e.Graphics.DrawRectangle(Pens.Black, realwidth, startY + Offset, startX, realheight);
            //e.Graphics.DrawString("ST", new Font("Calibri", 12, FontStyle.Bold), new SolidBrush(Color.White), startX + 350, startY + Offset);
            //realwidth = realwidth + startX;
            //e.Graphics.FillRectangle(Brushes.Black, realwidth, startY + Offset, startX + 20, realheight);
            //e.Graphics.DrawRectangle(Pens.Black, realwidth, startY + Offset, startX + 20, realheight);
            //e.Graphics.DrawString("ST_Mtrs", new Font("Calibri", 12, FontStyle.Bold), new SolidBrush(Color.White), startX + 400, startY + Offset);
            //realwidth = realwidth + startX + 20;
            //e.Graphics.FillRectangle(Brushes.Black, realwidth, startY + Offset, startX, realheight);
            //e.Graphics.DrawRectangle(Pens.Black, realwidth, startY + Offset, startX, realheight);
            //e.Graphics.DrawString("CM", new Font("Calibri", 12, FontStyle.Bold), new SolidBrush(Color.White), startX + 470, startY + Offset);
            //realwidth = realwidth + startX;
            //e.Graphics.FillRectangle(Brushes.Black, realwidth, startY + Offset, startX + 20, realheight);
            //e.Graphics.DrawRectangle(Pens.Black, realwidth, startY + Offset, startX + 20, realheight);
            //e.Graphics.DrawString("CM_Mtrs", new Font("Calibri", 12, FontStyle.Bold), new SolidBrush(Color.White), startX + 520, startY + Offset);
            //realwidth = realwidth + startX + 20;
            //e.Graphics.FillRectangle(Brushes.Black, realwidth, startY + Offset, startX, realheight);
            //e.Graphics.DrawRectangle(Pens.Black, realwidth, startY + Offset, startX, realheight);
            //e.Graphics.DrawString("PJ", new Font("Calibri", 12, FontStyle.Bold), new SolidBrush(Color.White), startX + 590, startY + Offset);
            //realwidth = realwidth + startX;
            //e.Graphics.FillRectangle(Brushes.Black, realwidth, startY + Offset, startX + 30, realheight);
            //e.Graphics.DrawRectangle(Pens.Black, realwidth, startY + Offset, startX + 30, realheight);
            //e.Graphics.DrawString("PJ_Mtrs", new Font("Calibri", 12, FontStyle.Bold), new SolidBrush(Color.White), startX + 640, startY + Offset);
            //Offset = Offset + 20;
            //int a = dataGridView3.Rows.Count;
            //Boolean flag = true;
            //for (; i < a; i++)
            //{
            //    realwidth = 50;
            //    realheight = 20;
            //    if (flag)
            //    {
            //        flag = false;
            //        e.Graphics.DrawRectangle(Pens.Black, realwidth, startY + Offset, startX + 120, realheight);
            //        e.Graphics.DrawString(Convert.ToString(dataGridView3.Rows[i].Cells[1].Value), new Font("Calibri", 12), new SolidBrush(Color.Black), startX, startY + Offset);
            //        realwidth = realwidth + startX + 120;
            //        e.Graphics.DrawRectangle(Pens.Black, realwidth, startY + Offset, startX + 11, realheight);
            //        e.Graphics.DrawString(Convert.ToString(dataGridView3.Rows[i].Cells[2].Value), new Font("Calibri", 12), new SolidBrush(Color.Black), startX + 170, startY + Offset);
            //        realwidth = realwidth + startX + 11;
            //        e.Graphics.DrawRectangle(Pens.Black, realwidth, startY + Offset, startX, realheight);
            //        e.Graphics.DrawString(Convert.ToString(dataGridView3.Rows[i].Cells[4].Value +"-"+ dataGridView3.Rows[i].Cells[5].Value), new Font("Calibri", 12), new SolidBrush(Color.Black), startX + 230, startY + Offset);
            //        realwidth = realwidth + startX;
            //        e.Graphics.DrawRectangle(Pens.Black, realwidth, startY + Offset, startX + 20, realheight);
            //        e.Graphics.DrawString(Convert.ToString(dataGridView3.Rows[i].Cells[6].Value), new Font("Calibri", 12), new SolidBrush(Color.Black), startX + 280, startY + Offset);
            //        realwidth = realwidth + startX + 20;
            //        e.Graphics.DrawRectangle(Pens.Black, realwidth, startY + Offset, startX, realheight);
            //        e.Graphics.DrawString(Convert.ToString(dataGridView3.Rows[i].Cells[7].Value +"-"+ dataGridView3.Rows[i].Cells[8].Value), new Font("Calibri", 12), new SolidBrush(Color.Black), startX + 350, startY + Offset);
            //        realwidth = realwidth + startX;
            //        e.Graphics.DrawRectangle(Pens.Black, realwidth, startY + Offset, startX + 20, realheight);
            //        e.Graphics.DrawString(Convert.ToString(dataGridView3.Rows[i].Cells[9].Value), new Font("Calibri", 12), new SolidBrush(Color.Black), startX + 400, startY + Offset);
            //        realwidth = realwidth + startX + 20;
            //        e.Graphics.DrawRectangle(Pens.Black, realwidth, startY + Offset, startX, realheight);
            //        e.Graphics.DrawString(Convert.ToString(dataGridView3.Rows[i].Cells[10].Value +"-"+ dataGridView3.Rows[i].Cells[11].Value), new Font("Calibri", 12), new SolidBrush(Color.Black), startX + 470, startY + Offset);
            //        realwidth = realwidth + startX;
            //        e.Graphics.DrawRectangle(Pens.Black, realwidth, startY + Offset, startX + 20, realheight);
            //        e.Graphics.DrawString(Convert.ToString(dataGridView3.Rows[i].Cells[12].Value), new Font("Calibri", 12), new SolidBrush(Color.Black), startX + 520, startY + Offset);
            //        realwidth = realwidth + startX + 20;
            //        e.Graphics.DrawRectangle(Pens.Black, realwidth, startY + Offset, startX, realheight);
            //        e.Graphics.DrawString(Convert.ToString(dataGridView3.Rows[i].Cells[13].Value +"-"+ dataGridView3.Rows[i].Cells[14].Value), new Font("Calibri", 12), new SolidBrush(Color.Black), startX + 590, startY + Offset);
            //        realwidth = realwidth + startX;
            //        e.Graphics.DrawRectangle(Pens.Black, realwidth, startY + Offset, startX + 30, realheight);
            //        e.Graphics.DrawString(Convert.ToString(dataGridView3.Rows[i].Cells[15].Value), new Font("Calibri", 12), new SolidBrush(Color.Black), startX + 640, startY + Offset);
            //        Offset = Offset + 20;
            //    }
            //    else
            //    {
            //        flag = true;
            //        e.Graphics.FillRectangle(Brushes.Gray, realwidth, startY + Offset, startX + 120, realheight);
            //        e.Graphics.DrawRectangle(Pens.Black, realwidth, startY + Offset, startX + 120, realheight);
            //        e.Graphics.DrawString(Convert.ToString(dataGridView3.Rows[i].Cells[1].Value), new Font("Calibri", 12), new SolidBrush(Color.Black), startX, startY + Offset);
            //        realwidth = realwidth + startX + 120;
            //        e.Graphics.FillRectangle(Brushes.Gray, realwidth, startY + Offset, startX + 11, realheight);
            //        e.Graphics.DrawRectangle(Pens.Black, realwidth, startY + Offset, startX + 11, realheight);
            //        e.Graphics.DrawString(Convert.ToString(dataGridView3.Rows[i].Cells[2].Value), new Font("Calibri", 12), new SolidBrush(Color.Black), startX + 170, startY + Offset);
            //        realwidth = realwidth + startX + 11;
            //        e.Graphics.FillRectangle(Brushes.Gray, realwidth, startY + Offset, startX, realheight);
            //        e.Graphics.DrawRectangle(Pens.Black, realwidth, startY + Offset, startX, realheight);
            //        e.Graphics.DrawString(Convert.ToString(dataGridView3.Rows[i].Cells[4].Value +"-"+ dataGridView3.Rows[i].Cells[5].Value), new Font("Calibri", 12), new SolidBrush(Color.Black), startX + 230, startY + Offset);
            //        realwidth = realwidth + startX;
            //        e.Graphics.FillRectangle(Brushes.Gray, realwidth, startY + Offset, startX + 20, realheight);
            //        e.Graphics.DrawRectangle(Pens.Black, realwidth, startY + Offset, startX + 20, realheight);
            //        e.Graphics.DrawString(Convert.ToString(dataGridView3.Rows[i].Cells[6].Value), new Font("Calibri", 12), new SolidBrush(Color.Black), startX + 280, startY + Offset);
            //        realwidth = realwidth + startX + 20;
            //        e.Graphics.FillRectangle(Brushes.Gray, realwidth, startY + Offset, startX, realheight);
            //        e.Graphics.DrawRectangle(Pens.Black, realwidth, startY + Offset, startX, realheight);
            //        e.Graphics.DrawString(Convert.ToString(dataGridView3.Rows[i].Cells[7].Value + "-" + dataGridView3.Rows[i].Cells[8].Value), new Font("Calibri", 12), new SolidBrush(Color.Black), startX + 350, startY + Offset);
            //        realwidth = realwidth + startX;
            //        e.Graphics.FillRectangle(Brushes.Gray, realwidth, startY + Offset, startX + 20, realheight);
            //        e.Graphics.DrawRectangle(Pens.Black, realwidth, startY + Offset, startX + 20, realheight);
            //        e.Graphics.DrawString(Convert.ToString(dataGridView3.Rows[i].Cells[9].Value), new Font("Calibri", 12), new SolidBrush(Color.Black), startX + 400, startY + Offset);
            //        realwidth = realwidth + startX + 20;
            //        e.Graphics.FillRectangle(Brushes.Gray, realwidth, startY + Offset, startX, realheight);
            //        e.Graphics.DrawRectangle(Pens.Black, realwidth, startY + Offset, startX, realheight);
            //        e.Graphics.DrawString(Convert.ToString(dataGridView3.Rows[i].Cells[10].Value + "-" + dataGridView3.Rows[i].Cells[11].Value), new Font("Calibri", 12), new SolidBrush(Color.Black), startX + 470, startY + Offset);
            //        realwidth = realwidth + startX;
            //        e.Graphics.FillRectangle(Brushes.Gray, realwidth, startY + Offset, startX + 20, realheight);
            //        e.Graphics.DrawRectangle(Pens.Black, realwidth, startY + Offset, startX + 20, realheight);
            //        e.Graphics.DrawString(Convert.ToString(dataGridView3.Rows[i].Cells[12].Value), new Font("Calibri", 12), new SolidBrush(Color.Black), startX + 520, startY + Offset);
            //        realwidth = realwidth + startX + 20;
            //        e.Graphics.FillRectangle(Brushes.Gray, realwidth, startY + Offset, startX, realheight);
            //        e.Graphics.DrawRectangle(Pens.Black, realwidth, startY + Offset, startX, realheight);
            //        e.Graphics.DrawString(Convert.ToString(dataGridView3.Rows[i].Cells[13].Value + "-" + dataGridView3.Rows[i].Cells[14].Value), new Font("Calibri", 12), new SolidBrush(Color.Black), startX + 590, startY + Offset);
            //        realwidth = realwidth + startX;
            //        e.Graphics.FillRectangle(Brushes.Gray, realwidth, startY + Offset, startX + 30, realheight);
            //        e.Graphics.DrawRectangle(Pens.Black, realwidth, startY + Offset, startX + 30, realheight);
            //        e.Graphics.DrawString(Convert.ToString(dataGridView3.Rows[i].Cells[15].Value), new Font("Calibri", 12), new SolidBrush(Color.Black), startX + 640, startY + Offset);
            //        Offset = Offset + 20;
            //    }
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
            //if (rdbtnavil2.Checked == true)
            //{
            //dataGridView3.Enabled = label5.Enabled = label7.Enabled = label17.Enabled = btnprint.Enabled = btnenter.Enabled = cmbavailcode.Enabled = cmbsize2.Enabled = cmbtone.Enabled = false;
            cmbavailname.Focus();
            string command1 = "SELECT p.p_id P_Code, p.p_name P_Name,(((SELECT isnull(sum(ps.qty), 0) from purchase_stock_detail2 ps where ps.p_id=p.p_id) + (select isnull(sum(sr.qty), 0) from sale_return_detail2 sr where sr.p_id=p.p_id)) - ((select isnull(sum(ss.qty), 0) from sale_stock_detail2 ss where ss.p_id=p.p_id)+(select isnull(sum(bd.qty), 0) from breakage_detail2 bd where bd.p_id=p.p_id) + (select isnull(sum(pr.qty), 0) from purchase_return_detail2 pr where pr.p_id=p.p_id))) Available from product2 p where (((SELECT isnull(sum(ps.qty), 0) from purchase_stock_detail2 ps where ps.p_id=p.p_id) + (select isnull(sum(sr.qty), 0) from sale_return_detail2 sr where sr.p_id=p.p_id)) - ((select isnull(sum(ss.qty), 0) from sale_stock_detail2 ss where ss.p_id=p.p_id)+(select isnull(sum(bd.qty), 0) from breakage_detail2 bd where bd.p_id=p.p_id) + (select isnull(sum(pr.qty), 0) from purchase_return_detail2 pr where pr.p_id=p.p_id))) <> 0 order by p_name";
            test.bindingcode(command1, dataGridView4, Login.con);
            //}
            //else
            //    dataGridView3.Enabled = label5.Enabled = label7.Enabled = label17.Enabled = btnprint.Enabled = btnenter.Enabled = cmbavailcode.Enabled = cmbsize2.Enabled = cmbtone.Enabled = true;
            txtavailcode.Text = cmbavailname.Text = "";
            //cmbsize2.Items.Clear();
            //cmbtone.Items.Clear();
        }

        private void txtavailcode_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
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
                    MessageBox.Show("Record Not Found", "AR Tranders", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                }
                Login.con.Close();
                string command1 = "SELECT p.p_id P_Code, p.p_name P_Name,(((SELECT isnull(sum(ps.qty), 0) from purchase_stock_detail2 ps where ps.p_id=p.p_id) + (select isnull(sum(sr.qty), 0) from sale_return_detail2 sr where sr.p_id=p.p_id)) - ((select isnull(sum(ss.qty), 0) from sale_stock_detail2 ss where ss.p_id=p.p_id)+(select isnull(sum(bd.qty), 0) from breakage_detail2 bd where bd.p_id=p.p_id) + (select isnull(sum(pr.qty), 0) from purchase_return_detail2 pr where pr.p_id=p.p_id))) Available from product2 p where p.p_id='" + txtavailcode.Text + "'";
                test.bindingcode(command1, dataGridView4, Login.con);
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
                    MessageBox.Show("Record Not Found", "AR Tranders", MessageBoxButtons.OK, MessageBoxIcon.Stop);
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
                    MessageBox.Show("Record Not Found", "AR Tranders", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                }
                Login.con.Close();
                string command1 = "SELECT p.p_id P_Code, p.p_name P_Name,(((SELECT isnull(sum(ps.qty), 0) from purchase_stock_detail2 ps where ps.p_id=p.p_id) + (select isnull(sum(sr.qty), 0) from sale_return_detail2 sr where sr.p_id=p.p_id)) - ((select isnull(sum(ss.qty), 0) from sale_stock_detail2 ss where ss.p_id=p.p_id)+(select isnull(sum(bd.qty), 0) from breakage_detail2 bd where bd.p_id=p.p_id) + (select isnull(sum(pr.qty), 0) from purchase_return_detail2 pr where pr.p_id=p.p_id))) Available from product2 p where p.p_name='" + cmbavailname.Text + "'";
                test.bindingcode(command1, dataGridView4, Login.con);
            }
        }

        private void cmbavailname_Leave(object sender, EventArgs e)
        {
            if (cmbavailname.Text == "")
            {
                txtavailcode.Text = "";
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
                    MessageBox.Show("Record Not Found", "AR Tranders", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                    txtavailcode.Text = "";
                    cmbavailname.Focus();
                }
                Login.con.Close();
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            rdbtnavil2_CheckedChanged(this, null);
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
            e.Graphics.DrawString("Stock Report", new Font("Calibri", 12, FontStyle.Underline), new SolidBrush(Color.Black), startX, startY + Offset);
            Offset = Offset + 30;
            string underLine = "--------------------------------------------------------------------------------";
            e.Graphics.DrawString(underLine, new Font("Courier New", 10, FontStyle.Bold), new SolidBrush(Color.Black), startX, startY + Offset);
            Offset = Offset + 20;
            e.Graphics.DrawString("P_Code", new Font("Calibri", 10, FontStyle.Bold), new SolidBrush(Color.Black), startX, startY + Offset);
            e.Graphics.DrawString("P_Name", new Font("Calibri", 10, FontStyle.Bold), new SolidBrush(Color.Black), startX + 120, startY + Offset);
            e.Graphics.DrawString("Available", new Font("Calibri", 10, FontStyle.Bold), new SolidBrush(Color.Black), startX + 550, startY + Offset);
            Offset = Offset + 20;
            e.Graphics.DrawString(underLine, new Font("Courier New", 10, FontStyle.Bold), new SolidBrush(Color.Black), startX, startY + Offset);
            Offset = Offset + 20;
            int a = dataGridView4.Rows.Count;
            for (; i < a; i++)
            {
                e.Graphics.DrawString(Convert.ToString(dataGridView4.Rows[i].Cells[0].Value), new Font("Calibri", 9), new SolidBrush(Color.Black), startX, startY + Offset);
                e.Graphics.DrawString(Convert.ToString(dataGridView4.Rows[i].Cells[1].Value), new Font("Calibri", 9), new SolidBrush(Color.Black), startX + 120, startY + Offset);
                e.Graphics.DrawString(Convert.ToString(dataGridView4.Rows[i].Cells[2].Value), new Font("Calibri", 9), new SolidBrush(Color.Black), startX + 550, startY + Offset);
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
