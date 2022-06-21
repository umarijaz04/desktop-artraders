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
    public partial class Profit_Report : Form
    {
        Form opener;
        Decimal amt, tdebit, tcredit, pre_balance;

        public Profit_Report(Form from)
        {
            InitializeComponent();
            opener = from;
        }

        private void Profit_Report_Load(object sender, EventArgs e)
        {
            opener.Enabled = false;
        }

        private void Profit_Report_FormClosing(object sender, FormClosingEventArgs e)
        {
            opener.Enabled = true;
        }

        void prebalance()
        {
            amt = 0;
            SqlCommand cm2 = new SqlCommand("Delete prebalance", Login.con);
            cm2.ExecuteNonQuery();
            SqlCommand cm = new SqlCommand("SELECT c_id,c_name from customer where type='EXPENSE'", Login.con);
            SqlDataAdapter da = new SqlDataAdapter(cm);
            DataTable dt = new DataTable();
            da.Fill(dt);
            foreach (DataRow row in dt.Rows)
            {
                tdebit = tcredit = 0;
                SqlCommand cmd3 = new SqlCommand("select ct.amount Debit from customer c, cus_transaction cus, customer_transaction ct where c.c_id = ct.c_id AND c.type = 'EXPENSE' AND cus.cus_id = ct.cus_id AND c.c_id = '" + row[0] + "' AND cus.datetime Between '" + Convert.ToDateTime(dateTimePicker1.Text) + "' AND '" + Convert.ToDateTime(dateTimePicker2.Text).AddDays(1) + "' AND ct.status = 'PAID'", Login.con);
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
                SqlCommand cmd4 = new SqlCommand("select ct.amount Credit from customer c, cus_transaction cus, customer_transaction ct where c.c_id = ct.c_id AND c.type = 'EXPENSE' AND cus.cus_id = ct.cus_id AND c.c_id = '" + row[0] + "'AND cus.datetime Between '" + Convert.ToDateTime(dateTimePicker1.Text) + "' AND '" + Convert.ToDateTime(dateTimePicker2.Text).AddDays(1) + "' AND ct.status = 'RECEIVED'", Login.con);
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
                pre_balance = ((tdebit) - (tcredit));
                if (pre_balance > 0)
                {
                    SqlCommand cm3 = new SqlCommand("Insert into prebalance(name,balance) values('" + row[1].ToString() + "','" + pre_balance + "')", Login.con);
                    cm3.ExecuteNonQuery();
                }
            }
            SqlCommand cmd8 = new SqlCommand("Select isnull(sum(balance),0) From prebalance", Login.con);
            lbdirectexp.Text = (amt = (Decimal)cmd8.ExecuteScalar()).ToString();
        }

        private void btnsearch_Click(object sender, EventArgs e)
        {
            Login.con.Open();
            SqlCommand cmd65 = new SqlCommand("IF EXISTS(select * FROM sys.views where name = 'purchases_temp') DROP VIEW purchases_temp", Login.con);
            cmd65.ExecuteNonQuery();
            SqlCommand cmd75 = new SqlCommand("IF EXISTS(select * FROM sys.views where name = 'purchases') DROP VIEW purchases", Login.con);
            cmd75.ExecuteNonQuery();
            SqlCommand cmd85 = new SqlCommand("IF EXISTS(select * FROM sys.views where name = 'sales') DROP VIEW sales", Login.con);
            cmd85.ExecuteNonQuery();
            SqlCommand cmd95 = new SqlCommand("IF EXISTS(select * FROM sys.views where name = 'sales_return') DROP VIEW sales_return", Login.con);
            cmd95.ExecuteNonQuery();
            SqlCommand cmd = new SqlCommand("Create View [purchases_temp] AS select psd.p_id,psd.size,psd.tone,psd.quality,cast(psd.qty + (psd.tiles/p.pieces) as decimal(18,2)) qty, psd.pprice,psd.discount, cast(((psd.pprice*(psd.qty + (psd.tiles/p.pieces)))-((psd.pprice*(psd.qty + (psd.tiles/p.pieces)))*(psd.discount/100))) as decimal(18,2)) amount,row_number() over(partition by psd.p_id,psd.size,psd.quality order by ps.datetime) as rn from purchase_stock_detail psd, purchase_stock ps, product p where psd.ps_id=ps.ps_id AND p.p_id=psd.p_id AND p.size=psd.size", Login.con);
            cmd.ExecuteNonQuery();
            SqlCommand cmd3 = new SqlCommand("create view [purchases] As select distinct p.p_id,p.size,p.quality,(select Cast((sum(amount)/sum(qty)) as decimal(18,2)) from purchases_temp where purchases_temp.p_id=p.p_id and purchases_temp.size = p.size and purchases_temp.quality = p.quality) pprice from purchase_stock_detail p", Login.con);
            cmd3.ExecuteNonQuery();
            SqlCommand cmd5 = new SqlCommand("create view [sales] As select ssd.p_id,ssd.size,ssd.tone,ssd.quality,cast(((((ssd.price * p.meters * (ssd.qty + (ssd.tiles/p.pieces))) - (((ssd.price * p.meters * (ssd.qty + (ssd.tiles/p.pieces))) *ssd.discount)/100) ) / (ssd.qty + (ssd.tiles/p.pieces)))-((select ss.discount from sale_stock ss where ss.ss_id=ssd.ss_id)/(select (sum(s.qty + (s.tiles/pro.pieces))) from sale_stock_detail s,product pro where pro.p_id=s.p_id AND ssd.ss_id=s.ss_id))) as decimal(18,2)) price,cast(ssd.qty + (ssd.tiles/p.pieces) as decimal(18,2)) Qty from sale_stock_detail ssd,sale_stock ss,product p where p.p_id=ssd.p_id AND p.size=ssd.size AND ss.ss_id=ssd.ss_id AND ss.datetime Between '" + Convert.ToDateTime(dateTimePicker1.Text) + "' AND '" + Convert.ToDateTime(dateTimePicker2.Text).AddDays(1) + "'", Login.con);
            cmd5.ExecuteNonQuery();
            SqlCommand cmd1 = new SqlCommand("select cast(isnull(SUM((s.price * s.qty) - (p.pprice * s.qty)),0) as decimal(18,2)) Profit from sales s, purchases p where s.p_id=p.p_id AND s.size=p.size AND s.quality=p.quality", Login.con);
            SqlDataReader dr1 = cmd1.ExecuteReader();
            if (dr1.HasRows)
            {
                while (dr1.Read())
                {
                    lblprofit.Text = Convert.ToString(Math.Round(Convert.ToDecimal(dr1[0]), 2));
                }
            }
            else
            {
                lblprofit.Text = "0";
            }
            dr1.Close();
            SqlCommand cmd6 = new SqlCommand("create view [sales_return] As select srd.p_id,srd.size,srd.tone,srd.quality,cast(((((srd.price * p.meters * (srd.qty + (srd.tiles/p.pieces))) - (((srd.price * p.meters * (srd.qty + (srd.tiles/p.pieces))) *srd.deduct)/100)) / (srd.qty + (srd.tiles/p.pieces)))-((select sr.deduct from sale_return sr where sr.sr_id=srd.sr_id)/(select (sum(s.qty + (s.tiles/pro.pieces))) from sale_return_detail s,product pro where pro.p_id=s.p_id AND srd.sr_id=s.sr_id))) as decimal(18,2)) price,cast((srd.qty + (srd.tiles/p.pieces)) as decimal(18,2)) Qty from sale_return_detail srd,sale_return sr,product p where p.p_id=srd.p_id AND p.size=srd.size AND sr.sr_id=srd.sr_id AND sr.datetime Between '" + Convert.ToDateTime(dateTimePicker1.Text) + "' AND '" + Convert.ToDateTime(dateTimePicker2.Text).AddDays(1) + "'", Login.con);
            cmd6.ExecuteNonQuery();
            SqlCommand cmd8 = new SqlCommand("select cast(isnull(SUM((s.price * s.qty) - (p.pprice * s.qty)),0) as decimal(18,2)) Loss from sales_return s, purchases p where s.p_id=p.p_id AND s.size=p.size AND s.quality=p.quality", Login.con);
            SqlDataReader dr8 = cmd8.ExecuteReader();
            if (dr8.HasRows)
            {
                while (dr8.Read())
                {
                    lblsalesreturn.Text = Convert.ToString(Math.Round(Convert.ToDecimal(dr8[0]), 2));
                }
            }
            else
            {
                lblsalesreturn.Text = "0";
            }
            dr8.Close();
            SqlCommand cmd10 = new SqlCommand("select isnull(SUM(amount),0) Expense from expense where datetime Between '" + Convert.ToDateTime(dateTimePicker1.Text) + "' AND '" + Convert.ToDateTime(dateTimePicker2.Text).AddDays(1) + "'", Login.con);
            SqlDataReader dr10 = cmd10.ExecuteReader();
            if (dr10.HasRows)
            {
                while (dr10.Read())
                {
                    lblexpense.Text = Convert.ToString(Math.Round(Convert.ToDecimal(dr10[0]), 2));
                }
            }
            else
            {
                lblexpense.Text = "0";
            }
            dr10.Close();
            SqlCommand cmd2 = new SqlCommand("DROP VIEW purchases", Login.con);
            cmd2.ExecuteNonQuery();
            SqlCommand cmd4 = new SqlCommand("DROP view purchases_temp", Login.con);
            cmd4.ExecuteNonQuery();
            SqlCommand cmd7 = new SqlCommand("DROP view sales", Login.con);
            cmd7.ExecuteNonQuery();
            SqlCommand cmd9 = new SqlCommand("DROP view sales_return", Login.con);
            cmd9.ExecuteNonQuery();
            //Sanitary
            //SqlCommand cmd21 = new SqlCommand("Create View [purchases_temp] AS select psd.p_id, psd.qty, psd.pprice,psd.discount, cast(((psd.pprice*psd.qty)-(psd.discount)) as decimal(18,2)) amount,row_number() over(partition by psd.p_id order by ps.datetime) as rn from purchase_stock_detail2 psd, purchase_stock ps where psd.ps_id=ps.ps_id AND ps.datetime Between '" + Convert.ToDateTime(dateTimePicker1.Text) + "' AND '" + Convert.ToDateTime(dateTimePicker2.Text).AddDays(1) + "' ", Login.con);
            //SqlCommand cmd21 = new SqlCommand("Create View [purchases_temp] AS select psd.p_id, psd.qty, psd.pprice,psd.discount, cast(((psd.pprice*psd.qty)-((psd.pprice*psd.qty)*(psd.discount/100))) as decimal(18,2)) amount,row_number() over(partition by psd.p_id order by ps.datetime) as rn from purchase_stock_detail2 psd, purchase_stock ps where psd.ps_id=ps.ps_id ", Login.con);
            SqlCommand cmd21 = new SqlCommand("Create View [purchases_temp] AS select psd.p_id, psd.qty, psd.pprice, psd.discount, cast(((psd.pprice*psd.qty)-psd.discount) as decimal(18,2)) amount, row_number() over(partition by psd.p_id order by ps.datetime) as rn from purchase_stock_detail2 psd, purchase_stock ps where psd.ps_id=ps.ps_id AND ps.datetime <= '" + Convert.ToDateTime(dateTimePicker2.Text).AddDays(1) + "'", Login.con);
            cmd21.ExecuteNonQuery();
            SqlCommand cmd23 = new SqlCommand("create view [purchases] As select distinct p.p_id ,(select Cast((sum(amount)/sum(qty)) as decimal(18,2)) from purchases_temp where purchases_temp.p_id=p.p_id) avg from purchases_temp p", Login.con);
            cmd23.ExecuteNonQuery();
            SqlCommand cmd25 = new SqlCommand("create view [sales] As select ssd.p_id,ssd.ss_id,CAST(((((ssd.price * ssd.qty) - (((ssd.qty * ssd.price) * ssd.discount)/100)) / ssd.qty)-((select ss.discount from sale_stock ss where ss.ss_id=ssd.ss_id)/(select sum(s.qty) from sale_stock_detail2 s where ssd.ss_id=s.ss_id))) as decimal(18,2)) price,ssd.qty from sale_stock_detail2 ssd,sale_stock ss where ss.ss_id=ssd.ss_id AND ss.datetime Between '" + Convert.ToDateTime(dateTimePicker1.Text) + "' AND '" + Convert.ToDateTime(dateTimePicker2.Text).AddDays(1) + "'", Login.con);
            cmd25.ExecuteNonQuery();
            SqlCommand cmd26 = new SqlCommand("create view [profit] as select s.p_id,s.ss_id,s.qty,s.price,p.avg,(s.price * s.qty) s_amt,(p.avg * s.qty) p_amt,(s.price * s.qty) - (p.avg * s.qty) Profit from sales s, purchases p where s.p_id=p.p_id", Login.con);
            cmd26.ExecuteNonQuery();
            SqlCommand cmd31 = new SqlCommand("select sum(Profit) from Profit ", Login.con);
            SqlDataReader dr31 = cmd31.ExecuteReader();
            if (dr31.HasRows)
            {
                while (dr31.Read())
                {
                    lblprofit.Text = Convert.ToString(Convert.ToDecimal(lblprofit.Text) + (Math.Round(Convert.ToDecimal(dr31[0]), 2)));
                }
            }
            dr31.Close();
            SqlCommand cmd27 = new SqlCommand("create view [sales_return] As select srd.p_id,CAST(((((srd.price * srd.qty) - (((srd.qty * srd.price)* srd.deduct)/100)) / srd.qty)-((select sr.deduct from sale_return sr where sr.sr_id=srd.sr_id)/(select sum(s.qty) from sale_return_detail2 s where srd.sr_id=s.sr_id))) as decimal(18,2)) price,srd.qty from sale_return_detail2 srd,sale_return sr where sr.sr_id=srd.sr_id AND sr.datetime Between '" + Convert.ToDateTime(dateTimePicker1.Text) + "' AND '" + Convert.ToDateTime(dateTimePicker2.Text).AddDays(1) + "'", Login.con);
            cmd27.ExecuteNonQuery();
            SqlCommand cmd28 = new SqlCommand("select isnull(SUM((s.price * s.qty) - (p.avg * s.qty)),0) Loss from sales_return s, purchases p where s.p_id=p.p_id", Login.con);
            SqlDataReader dr28 = cmd28.ExecuteReader();
            if (dr28.HasRows)
            {
                while (dr28.Read())
                {
                    lblsalesreturn.Text = Convert.ToString(Convert.ToDecimal(lblsalesreturn.Text) + (Math.Round(Convert.ToDecimal(dr28[0]), 2)));
                }
            }
            dr28.Close();
            prebalance();
            if (((Convert.ToDecimal(lblprofit.Text) - Convert.ToDecimal(lblsalesreturn.Text)) - Convert.ToDecimal(lblexpense.Text) - Convert.ToDecimal(lbdirectexp.Text)) < 0)
            {
                lbltotal.Text = ((Convert.ToDecimal(lblprofit.Text) - Convert.ToDecimal(lblsalesreturn.Text)) - Convert.ToDecimal(lblexpense.Text) - Convert.ToDecimal(lbdirectexp.Text)).ToString().Substring(1);
                label5.Text = "Net Loss";
            }
            else
            {
                lbltotal.Text = ((Convert.ToDecimal(lblprofit.Text) - Convert.ToDecimal(lblsalesreturn.Text)) - Convert.ToDecimal(lblexpense.Text) - Convert.ToDecimal(lbdirectexp.Text)).ToString();
                label5.Text = "Net Profit";
            }
            SqlCommand cmd12 = new SqlCommand("DROP VIEW purchases", Login.con);
            cmd12.ExecuteNonQuery();
            SqlCommand cmd14 = new SqlCommand("DROP view purchases_temp", Login.con);
            cmd14.ExecuteNonQuery();
            SqlCommand cmd17 = new SqlCommand("DROP view sales", Login.con);
            cmd17.ExecuteNonQuery();
            SqlCommand cmd19 = new SqlCommand("DROP view sales_return", Login.con);
            cmd19.ExecuteNonQuery();
            SqlCommand cmd119 = new SqlCommand("DROP view profit", Login.con);
            cmd119.ExecuteNonQuery();
            Login.con.Close();
        }
    }
}
