using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.Drawing.Printing;
using System.Threading;

namespace AhmadSanitary
{
    public partial class Cash_Report : Form
    {
        Form opener;
        Decimal balance_purchase_stock, balance_purchase_return, balance_sale_stock, balance_customer_transaction, balance_customer_transaction_n, balance_manu_transaction2, balance_sale_return, balance_expense, balance_manu_transaction, debit, credit, balance, sdebit, srcredit, tdebit, tcredit, pcredit, mdebit, mcredit, exp, prcredit, prdebit;
        UF.Class1 test = new UF.Class1();

        public Cash_Report(Form from)
        {
            InitializeComponent();
            opener = from;
        }

        private void Cash_Report_Load(object sender, EventArgs e)
        {
            opener.Enabled = false;
            label2.Text = previous_bal();
        }

        private void Cash_Report_FormClosing(object sender, FormClosingEventArgs e)
        {
            opener.Enabled = true;
        }

        private string previous_bal()
        {
            Login.con.Open();
            SqlCommand cmd = new SqlCommand("select isnull(sum(receive),0) from sale_stock ", Login.con);
            SqlDataReader dr = cmd.ExecuteReader();
            if (dr.HasRows)
            {
                while (dr.Read())
                {
                    balance_sale_stock = (Convert.ToDecimal(dr[0]));
                }
            }
            dr.Close();
            SqlCommand cmd2 = new SqlCommand("select isnull(sum(paid),0) from purchase_stock ", Login.con);
            SqlDataReader dr2 = cmd2.ExecuteReader();
            if (dr2.HasRows)
            {
                while (dr2.Read())
                {
                    balance_purchase_stock = (Convert.ToDecimal(dr2[0]));
                }
            }
            dr2.Close();
            SqlCommand cmd3 = new SqlCommand("select isnull(sum(amount),0) from customer_transaction where status='Received' AND type='True'", Login.con);
            SqlDataReader dr3 = cmd3.ExecuteReader();
            if (dr3.HasRows)
            {
                while (dr3.Read())
                {
                    balance_customer_transaction = (Convert.ToDecimal(dr3[0]));
                }
            }
            dr3.Close();
            SqlCommand cmd4 = new SqlCommand("select isnull(sum(amount),0) from customer_transaction where status='Paid' AND type='True'", Login.con);
            SqlDataReader dr4 = cmd4.ExecuteReader();
            if (dr4.HasRows)
            {
                while (dr4.Read())
                {
                    balance_customer_transaction_n = (Convert.ToDecimal(dr4[0]));
                }
            }
            dr4.Close();
            SqlCommand cmd5 = new SqlCommand("select isnull(sum(Paid),0) from sale_return  ", Login.con);
            SqlDataReader dr5 = cmd5.ExecuteReader();
            if (dr5.HasRows)
            {
                while (dr5.Read())
                {
                    balance_sale_return = (Convert.ToDecimal(dr5[0]));
                }
            }
            dr5.Close();
            SqlCommand cmd55 = new SqlCommand("select isnull(sum(receive),0) from purchase_return  ", Login.con);
            SqlDataReader dr55 = cmd55.ExecuteReader();
            if (dr55.HasRows)
            {
                while (dr55.Read())
                {
                    balance_purchase_return = (Convert.ToDecimal(dr55[0]));
                }
            }
            dr55.Close();
            SqlCommand cmd6 = new SqlCommand("select isnull(sum(amount),0) from expense  ", Login.con);
            SqlDataReader dr6 = cmd6.ExecuteReader();
            if (dr6.HasRows)
            {
                while (dr6.Read())
                {
                    balance_expense = (Convert.ToDecimal(dr6[0]));
                }
            }
            dr6.Close();
            SqlCommand cmd7 = new SqlCommand("select isnull(sum(amount),0) from manu_transaction where status='Received' AND type='True'", Login.con);
            SqlDataReader dr7 = cmd7.ExecuteReader();
            if (dr7.HasRows)
            {
                while (dr7.Read())
                {
                    balance_manu_transaction = (Convert.ToDecimal(dr7[0]));
                }
            }
            dr7.Close();
            SqlCommand cmd8 = new SqlCommand("select isnull(sum(amount),0) from manu_transaction where status='Paid' AND type='True'", Login.con);
            SqlDataReader dr8 = cmd8.ExecuteReader();
            if (dr8.HasRows)
            {
                while (dr8.Read())
                {
                    balance_manu_transaction2 = (Convert.ToDecimal(dr8[0]));
                }
            }
            dr8.Close();
            Login.con.Close();
            return ((balance_sale_stock + balance_purchase_return + balance_customer_transaction + balance_manu_transaction) - (balance_sale_return + balance_customer_transaction_n + balance_expense + balance_purchase_stock + balance_manu_transaction2)).ToString() + " Rs";
        }

        private void btnsearch_Click(object sender, EventArgs e)
        {
            balance = sdebit = prcredit = prdebit = srcredit = tdebit = tcredit = pcredit = mdebit = mcredit = exp = 0;
            Login.con.Open();
            SqlCommand cmd5 = new SqlCommand("select * from (select cat.cat_id CAT_ID, cat.datetime DateTime,'Indirect Expenses' Account, e.description Description, '0' Debit ,e.amount Credit, '' Status, '' Balance from cash_transaction cat, expense e where cat.cat_id=e.cat_id AND (cat.datetime Between '" + Convert.ToDateTime(dateTimePicker1.Text) + "' AND '" + Convert.ToDateTime(dateTimePicker2.Text).AddDays(1) + "') union all  select cat.cat_id , cat.datetime ,'Sales','Invc# ' + CONVERT(varchar(50), s.ss_id) , s.receive,'0','','' from cash_transaction cat, sale_stock s where cat.cat_id=s.cat_id AND (cat.datetime Between '" + Convert.ToDateTime(dateTimePicker1.Text) + "' AND '" + Convert.ToDateTime(dateTimePicker2.Text).AddDays(1) + "') union all select cat.cat_id , cat.datetime ,'Purchases Return', 'PR# ' + CONVERT(varchar(50),pr.pr_id) , pr.receive,'0','','' from cash_transaction cat, purchase_return pr where cat.cat_id=pr.cat_id AND (cat.datetime Between '" + Convert.ToDateTime(dateTimePicker1.Text) + "' AND '" + Convert.ToDateTime(dateTimePicker2.Text).AddDays(1) + "') union all (select cat.cat_id , cat.datetime,CASE WHEN c.type = 'FIXED' THEN 'Customer' WHEN c.type='CAPITAL' THEN 'Capital'WHEN c.type='EXPENSE' THEN 'Direct Expenses' WHEN c.type='OPENING' THEN 'Opening Balance' WHEN c.type='CLOSING' THEN 'Closing Balance' ELSE 'Stakeholder' END, c.c_name, '0', ct.amount,'','' from cash_transaction cat, customer_transaction ct,customer c where cat.cat_id=ct.cat_id AND ct.c_id=c.c_id AND ct.status='PAID' AND ct.type='True' AND (cat.datetime Between '" + Convert.ToDateTime(dateTimePicker1.Text) + "' AND '" + Convert.ToDateTime(dateTimePicker2.Text).AddDays(1) + "') union select cat.cat_id , cat.datetime,CASE WHEN c.type = 'FIXED' THEN 'Customer' WHEN c.type='CAPITAL' THEN 'Capital'WHEN c.type='EXPENSE' THEN 'Direct Expenses' WHEN c.type='OPENING' THEN 'Opening Balance' WHEN c.type='CLOSING' THEN 'Closing Balance' ELSE 'Stakeholder' END, c.c_name, ct.amount , '0', '', '' from cash_transaction cat, customer_transaction ct,customer c where cat.cat_id=ct.cat_id AND ct.c_id=c.c_id AND ct.status='RECEIVED' AND ct.type='True' AND (cat.datetime Between '" + Convert.ToDateTime(dateTimePicker1.Text) + "' AND '" + Convert.ToDateTime(dateTimePicker2.Text).AddDays(1) + "')) union all select cat.cat_id , cat.datetime,'Sales Return', 'SR# ' + CONVERT(varchar(50),sr.sr_id),'0' , sr.Paid , '' , ''  from cash_transaction cat, sale_return sr where cat.cat_id=sr.cat_id AND (cat.datetime Between '" + Convert.ToDateTime(dateTimePicker1.Text) + "' AND '" + Convert.ToDateTime(dateTimePicker2.Text).AddDays(1) + "') union all select cat.cat_id , cat.datetime,'Purchases' , 'DC# ' + CONVERT(varchar(50),ps.ps_id), '0' , ps.Paid , '' , ''  from cash_transaction cat,purchase_stock ps where cat.cat_id=ps.cat_id AND (cat.datetime Between '" + Convert.ToDateTime(dateTimePicker1.Text) + "' AND '" + Convert.ToDateTime(dateTimePicker2.Text).AddDays(1) + "') union all (select cat.cat_id , cat.datetime,'Manufacturer' , m.name, '0' , mt.amount , '' , '' from cash_transaction cat, manu_transaction mt,manufacturer m where cat.cat_id=mt.cat_id AND mt.m_id=m.m_id AND mt.status='PAID' AND mt.type='True' AND (cat.datetime Between '" + Convert.ToDateTime(dateTimePicker1.Text) + "' AND '" + Convert.ToDateTime(dateTimePicker2.Text).AddDays(1) + "') union select cat.cat_id , cat.datetime,'Manufacturer' , m.name, mt.amount , '0' , '' , ''  from cash_transaction cat, manu_transaction mt,manufacturer m where cat.cat_id=mt.cat_id AND mt.m_id=m.m_id AND mt.status='RECEIVED' AND mt.type='True' AND (cat.datetime Between '" + Convert.ToDateTime(dateTimePicker1.Text) + "' AND '" + Convert.ToDateTime(dateTimePicker2.Text).AddDays(1) + "'))) as U where U.Account Like'" + cmbquality.Text + "%' order by U.DateTime", Login.con);
            SqlDataReader dr5 = cmd5.ExecuteReader();
            if (dr5.HasRows)
            {
                dr5.Close();
                SqlCommand cmd = new SqlCommand("select s.receive from cash_transaction cat, sale_stock s where cat.cat_id=s.cat_id AND cat.datetime < '" + Convert.ToDateTime(dateTimePicker1.Text) + "'", Login.con);
                SqlDataReader dr = cmd.ExecuteReader();
                if (dr.HasRows)
                {
                    while (dr.Read())
                    {
                        sdebit += Convert.ToDecimal(dr[0]);
                    }
                }
                else
                {
                    dr.Close();
                    sdebit = 0;
                }
                dr.Close();
                SqlCommand cmd91 = new SqlCommand("select pr.receive from cash_transaction cat, purchase_return pr where cat.cat_id=pr.cat_id AND cat.datetime < '" + Convert.ToDateTime(dateTimePicker1.Text) + "'", Login.con);
                SqlDataReader dr91 = cmd91.ExecuteReader();
                if (dr91.HasRows)
                {
                    while (dr91.Read())
                    {
                        prdebit += Convert.ToDecimal(dr91[0]);
                    }
                }
                else
                {
                    dr91.Close();
                    prdebit = 0;
                }
                dr91.Close();
                SqlCommand cmd2 = new SqlCommand("select sr.Paid from cash_transaction cat, sale_return sr where cat.cat_id=sr.cat_id AND cat.datetime < '" + Convert.ToDateTime(dateTimePicker1.Text) + "'", Login.con);
                SqlDataReader dr2 = cmd2.ExecuteReader();
                if (dr2.HasRows)
                {
                    while (dr2.Read())
                    {
                        srcredit += Convert.ToDecimal(dr2[0]);
                    }
                }
                else
                {
                    dr2.Close();
                    srcredit = 0;
                }
                dr2.Close();
                SqlCommand cmd3 = new SqlCommand("select ct.amount from cash_transaction cat, customer_transaction ct,customer c where cat.cat_id=ct.cat_id AND ct.c_id=c.c_id AND ct.status='PAID' AND cat.datetime < '" + Convert.ToDateTime(dateTimePicker1.Text) + "' AND ct.type='True'", Login.con);
                SqlDataReader dr3 = cmd3.ExecuteReader();
                if (dr3.HasRows)
                {
                    while (dr3.Read())
                    {
                        tcredit += Convert.ToDecimal(dr3[0]);
                    }
                }
                else
                {
                    dr3.Close();
                    tcredit = 0;
                }
                dr3.Close();
                SqlCommand cmd4 = new SqlCommand("select ct.amount from cash_transaction cat, customer_transaction ct,customer c where cat.cat_id=ct.cat_id AND ct.c_id=c.c_id AND ct.status='RECEIVED' AND cat.datetime < '" + Convert.ToDateTime(dateTimePicker1.Text) + "'AND ct.type='True'", Login.con);
                SqlDataReader dr4 = cmd4.ExecuteReader();
                if (dr4.HasRows)
                {
                    while (dr4.Read())
                    {
                        tdebit += Convert.ToDecimal(dr4[0]);
                    }
                }
                else
                {
                    dr4.Close();
                    tdebit = 0;
                }
                dr4.Close();
                SqlCommand cmd6 = new SqlCommand("select ps.Paid from cash_transaction cat,purchase_stock ps where cat.cat_id=ps.cat_id AND cat.datetime < '" + Convert.ToDateTime(dateTimePicker1.Text) + "'", Login.con);
                SqlDataReader dr6 = cmd6.ExecuteReader();
                if (dr6.HasRows)
                {
                    while (dr6.Read())
                    {
                        pcredit += Convert.ToDecimal(dr6[0]);
                    }
                }
                else
                {
                    dr6.Close();
                    pcredit = 0;
                }
                dr6.Close();
                SqlCommand cmd7 = new SqlCommand("select mt.amount from cash_transaction cat, manu_transaction mt,manufacturer m where cat.cat_id=mt.cat_id AND mt.m_id=m.m_id AND mt.status='PAID' AND cat.datetime < '" + Convert.ToDateTime(dateTimePicker1.Text) + "'AND mt.type='True'", Login.con);
                SqlDataReader dr7 = cmd7.ExecuteReader();
                if (dr7.HasRows)
                {
                    while (dr7.Read())
                    {
                        mcredit += Convert.ToDecimal(dr7[0]);
                    }
                }
                else
                {
                    dr7.Close();
                    mcredit = 0;
                }
                dr7.Close();
                SqlCommand cmd8 = new SqlCommand("select mt.amount from cash_transaction cat, manu_transaction mt,manufacturer m where cat.cat_id=mt.cat_id AND mt.m_id=m.m_id AND mt.status='RECEIVED' AND cat.datetime < '" + Convert.ToDateTime(dateTimePicker1.Text) + "'AND mt.type='True'", Login.con);
                SqlDataReader dr8 = cmd8.ExecuteReader();
                if (dr8.HasRows)
                {
                    while (dr8.Read())
                    {
                        mdebit += Convert.ToDecimal(dr8[0]);
                    }
                }
                else
                {
                    dr8.Close();
                    mdebit = 0;
                }
                dr8.Close();
                SqlCommand cmd9 = new SqlCommand("select e.amount from cash_transaction cat, expense e where cat.cat_id=e.cat_id AND cat.datetime < '" + Convert.ToDateTime(dateTimePicker1.Text) + "'", Login.con);
                SqlDataReader dr9 = cmd9.ExecuteReader();
                if (dr9.HasRows)
                {
                    while (dr9.Read())
                    {
                        exp += Convert.ToDecimal(dr9[0]);
                    }
                }
                else
                {
                    dr9.Close();
                    exp = 0;
                }
                dr9.Close();
                balance = ((sdebit + tdebit + mdebit + prdebit) - (srcredit + tcredit + pcredit + mcredit + exp));
                if (balance != 0 && balance < 0)
                {
                    Boolean flag = true;
                    int col = 1;
                    //SqlDataAdapter a = new SqlDataAdapter("select * from (select '1' CAT_ID, '' DateTime,'Pre. A/c' Account,'' Description, '0' Debit, '0' Credit,'' Status, '' Balance Union select * from (select cat.cat_id CAT_ID, cat.datetime DateTime,'Indirect Expenses' Account, e.description Description, '0' Debit ,e.amount Credit, '' Status, '' Balance from cash_transaction cat, expense e where cat.cat_id=e.cat_id AND (cat.datetime Between '" + Convert.ToDateTime(dateTimePicker1.Text) + "' AND '" + Convert.ToDateTime(dateTimePicker2.Text).AddDays(1) + "') union all  select cat.cat_id , cat.datetime ,'Sales','Invc# ' + CONVERT(varchar(50), s.ss_id) , s.receive,'0','','' from cash_transaction cat, sale_stock s where cat.cat_id=s.cat_id AND (cat.datetime Between '" + Convert.ToDateTime(dateTimePicker1.Text) + "' AND '" + Convert.ToDateTime(dateTimePicker2.Text).AddDays(1) + "') union all select cat.cat_id , cat.datetime ,'Purchases Return', 'PR# ' + CONVERT(varchar(50),pr.pr_id) , pr.receive,'0','','' from cash_transaction cat, purchase_return pr where cat.cat_id=pr.cat_id AND (cat.datetime Between '" + Convert.ToDateTime(dateTimePicker1.Text) + "' AND '" + Convert.ToDateTime(dateTimePicker2.Text).AddDays(1) + "') union all (select cat.cat_id , cat.datetime,CASE WHEN c.type = 'FIXED' THEN 'Customer' WHEN c.type='CAPITAL' THEN 'Capital'WHEN c.type='EXPENSE' THEN 'Direct Expenses' WHEN c.type='OPENING' THEN 'Opening Balance' WHEN c.type='CLOSING' THEN 'Closing Balance' ELSE 'Stakeholder' END, c.c_name, '0', ct.amount,'','' from cash_transaction cat, customer_transaction ct,customer c where cat.cat_id=ct.cat_id AND ct.c_id=c.c_id AND ct.status='PAID' AND ct.type='True' AND (cat.datetime Between '" + Convert.ToDateTime(dateTimePicker1.Text) + "' AND '" + Convert.ToDateTime(dateTimePicker2.Text).AddDays(1) + "') union select cat.cat_id , cat.datetime,CASE WHEN c.type = 'FIXED' THEN 'Customer' WHEN c.type='CAPITAL' THEN 'Capital'WHEN c.type='EXPENSE' THEN 'Direct Expenses' WHEN c.type='OPENING' THEN 'Opening Balance' WHEN c.type='CLOSING' THEN 'Closing Balance' ELSE 'Stakeholder' END, c.c_name, ct.amount , '0', '', '' from cash_transaction cat, customer_transaction ct,customer c where cat.cat_id=ct.cat_id AND ct.c_id=c.c_id AND ct.status='RECEIVED' AND ct.type='True' AND (cat.datetime Between '" + Convert.ToDateTime(dateTimePicker1.Text) + "' AND '" + Convert.ToDateTime(dateTimePicker2.Text).AddDays(1) + "')) union all select cat.cat_id , cat.datetime,'Sales Return', 'SR# ' + CONVERT(varchar(50),sr.sr_id),'0' , sr.Paid , '' , ''  from cash_transaction cat, sale_return sr where cat.cat_id=sr.cat_id AND (cat.datetime Between '" + Convert.ToDateTime(dateTimePicker1.Text) + "' AND '" + Convert.ToDateTime(dateTimePicker2.Text).AddDays(1) + "') union all select cat.cat_id , cat.datetime,'Purchases' , 'DC# ' + CONVERT(varchar(50),ps.ps_id), '0' , ps.Paid , '' , ''  from cash_transaction cat,purchase_stock ps where cat.cat_id=ps.cat_id AND (cat.datetime Between '" + Convert.ToDateTime(dateTimePicker1.Text) + "' AND '" + Convert.ToDateTime(dateTimePicker2.Text).AddDays(1) + "') union all (select cat.cat_id , cat.datetime,'Manufacturer' , m.name, '0' , mt.amount , '' , '' from cash_transaction cat, manu_transaction mt,manufacturer m where cat.cat_id=mt.cat_id AND mt.m_id=m.m_id AND mt.status='PAID' AND mt.type='True' AND (cat.datetime Between '" + Convert.ToDateTime(dateTimePicker1.Text) + "' AND '" + Convert.ToDateTime(dateTimePicker2.Text).AddDays(1) + "') union select cat.cat_id , cat.datetime,'Manufacturer' , m.name, mt.amount , '0' , '' , ''  from cash_transaction cat, manu_transaction mt,manufacturer m where cat.cat_id=mt.cat_id AND mt.m_id=m.m_id AND mt.status='RECEIVED' AND mt.type='True' AND (cat.datetime Between '" + Convert.ToDateTime(dateTimePicker1.Text) + "' AND '" + Convert.ToDateTime(dateTimePicker2.Text).AddDays(1) + "')))as U where U.Account Like'" + cmbquality.Text + "%'", Login.con);
                    SqlDataAdapter a = new SqlDataAdapter("select '1' CAT_ID, '' DateTime,'Pre. A/c' Account,'' Description, '0' Debit, '0' Credit,'' Status, '' Balance Union select * from ( select cat.cat_id CAT_ID, cat.datetime DateTime,'Indirect Expenses' Account, e.description Description, '0' Debit ,e.amount Credit, '' Status, '' Balance from cash_transaction cat, expense e where cat.cat_id=e.cat_id AND (cat.datetime Between '" + Convert.ToDateTime(dateTimePicker1.Text) + "' AND '" + Convert.ToDateTime(dateTimePicker2.Text).AddDays(1) + "') union all  select cat.cat_id , cat.datetime ,'Sales', 'Invc# ' + CONVERT(varchar(50), s.ss_id) , s.receive,'0','','' from cash_transaction cat, sale_stock s where cat.cat_id=s.cat_id AND (cat.datetime Between '" + Convert.ToDateTime(dateTimePicker1.Text) + "' AND '" + Convert.ToDateTime(dateTimePicker2.Text).AddDays(1) + "') union all select cat.cat_id , cat.datetime ,'Purchases Return', 'PR# ' + CONVERT(varchar(50),pr.pr_id) , pr.receive,'0','','' from cash_transaction cat, purchase_return pr where cat.cat_id=pr.cat_id AND (cat.datetime Between '" + Convert.ToDateTime(dateTimePicker1.Text) + "' AND '" + Convert.ToDateTime(dateTimePicker2.Text).AddDays(1) + "') union all (select cat.cat_id , cat.datetime,CASE WHEN c.type = 'FIXED' THEN 'Customer' WHEN c.type='CAPITAL' THEN 'Capital'WHEN c.type='EXPENSE' THEN 'Direct Expenses' WHEN c.type='OPENING' THEN 'Opening Balance' WHEN c.type='CLOSING' THEN 'Closing Balance' ELSE 'Stakeholder' END, c.c_name, '0', ct.amount,'','' from cash_transaction cat, customer_transaction ct,customer c where cat.cat_id=ct.cat_id AND ct.c_id=c.c_id AND ct.status='PAID' AND ct.type='True' AND (cat.datetime Between '" + Convert.ToDateTime(dateTimePicker1.Text) + "' AND '" + Convert.ToDateTime(dateTimePicker2.Text).AddDays(1) + "') union select cat.cat_id , cat.datetime,CASE WHEN c.type = 'FIXED' THEN 'Customer' WHEN c.type='CAPITAL' THEN 'Capital'WHEN c.type='EXPENSE' THEN 'Direct Expenses' WHEN c.type='OPENING' THEN 'Opening Balance' WHEN c.type='CLOSING' THEN 'Closing Balance' ELSE 'Stakeholder' END, c.c_name, ct.amount , '0', '', '' from cash_transaction cat, customer_transaction ct,customer c where cat.cat_id=ct.cat_id AND ct.c_id=c.c_id AND ct.status='RECEIVED' AND ct.type='True' AND (cat.datetime Between '" + Convert.ToDateTime(dateTimePicker1.Text) + "' AND '" + Convert.ToDateTime(dateTimePicker2.Text).AddDays(1) + "')) union all select cat.cat_id , cat.datetime,'Sales Return', 'SR# ' + CONVERT(varchar(50),sr.sr_id),'0' , sr.Paid , '' , ''  from cash_transaction cat, sale_return sr where cat.cat_id=sr.cat_id AND (cat.datetime Between '" + Convert.ToDateTime(dateTimePicker1.Text) + "' AND '" + Convert.ToDateTime(dateTimePicker2.Text).AddDays(1) + "') union all select cat.cat_id , cat.datetime,'Purchases' , 'DC# ' + CONVERT(varchar(50),ps.ps_id), '0' , ps.Paid , '' , ''  from cash_transaction cat,purchase_stock ps where cat.cat_id=ps.cat_id AND (cat.datetime Between '" + Convert.ToDateTime(dateTimePicker1.Text) + "' AND '" + Convert.ToDateTime(dateTimePicker2.Text).AddDays(1) + "') union all (select cat.cat_id , cat.datetime,'Manufacturer' , m.name, '0' , mt.amount , '' , '' from cash_transaction cat, manu_transaction mt,manufacturer m where cat.cat_id=mt.cat_id AND mt.m_id=m.m_id AND mt.status='PAID' AND mt.type='True' AND (cat.datetime Between '" + Convert.ToDateTime(dateTimePicker1.Text) + "' AND '" + Convert.ToDateTime(dateTimePicker2.Text).AddDays(1) + "') union select cat.cat_id , cat.datetime,'Manufacturer' , m.name, mt.amount , '0' , '' , ''  from cash_transaction cat, manu_transaction mt,manufacturer m where cat.cat_id=mt.cat_id AND mt.m_id=m.m_id AND mt.status='RECEIVED' AND mt.type='True' AND (cat.datetime Between '" + Convert.ToDateTime(dateTimePicker1.Text) + "' AND '" + Convert.ToDateTime(dateTimePicker2.Text).AddDays(1) + "'))) as U where U.Account Like'" + cmbquality.Text + "%'", Login.con);
                    DataTable dataTable1 = new DataTable();
                    a.Fill(dataTable1);
                    dataTable1.DefaultView.Sort = "DateTime";
                    dataTable1 = dataTable1.DefaultView.ToTable();
                    foreach (DataRow row in dataTable1.Rows)
                    {
                        if (flag)
                        {
                            dataTable1.Rows[0]["Balance"] = Convert.ToDecimal(Convert.ToString(balance).Substring(1));
                            dataTable1.Rows[0]["Credit"] = Convert.ToDecimal(Convert.ToString(balance).Substring(1));
                            dataTable1.Rows[0]["Status"] = "Credit";
                            dataTable1.Rows[0]["DateTime"] = Convert.ToDateTime(dateTimePicker1.Value).AddDays(-1);
                            flag = false;
                        }
                        for (; col < dataTable1.Rows.Count; col++)
                        {
                            if (Convert.ToDecimal(dataTable1.Rows[col]["Debit"]) != 0)
                            {
                                balance += Convert.ToDecimal(dataTable1.Rows[col]["Debit"]);
                                if (balance < 0)
                                {
                                    dataTable1.Rows[col]["Status"] = "Credit";
                                    dataTable1.Rows[col]["Balance"] = Convert.ToDecimal(Convert.ToString(balance).Substring(1));
                                }
                                else
                                {
                                    dataTable1.Rows[col]["Status"] = "Debit";
                                    dataTable1.Rows[col]["Balance"] = balance;
                                }
                            }
                            else
                            {
                                balance -= Convert.ToDecimal(dataTable1.Rows[col]["Credit"]);
                                if (balance < 0)
                                {
                                    dataTable1.Rows[col]["Status"] = "Credit";
                                    dataTable1.Rows[col]["Balance"] = Convert.ToDecimal(Convert.ToString(balance).Substring(1));
                                }
                                else
                                {
                                    dataTable1.Rows[col]["Status"] = "Debit";
                                    dataTable1.Rows[col]["Balance"] = balance;
                                }
                            }
                        }
                    }
                    dataGridView.DataSource = dataTable1;
                }
                else if (balance != 0 && balance > 0)
                {
                    Boolean flag = true;
                    int col = 1;
                    SqlDataAdapter a = new SqlDataAdapter("select '1' CAT_ID, '' DateTime,'Pre. A/c' Account,'' Description, '0' Debit, '0' Credit,'' Status, '' Balance Union select * from ( select cat.cat_id CAT_ID, cat.datetime DateTime,'Indirect Expenses' Account, e.description Description, '0' Debit ,e.amount Credit, '' Status, '' Balance from cash_transaction cat, expense e where cat.cat_id=e.cat_id AND (cat.datetime Between '" + Convert.ToDateTime(dateTimePicker1.Text) + "' AND '" + Convert.ToDateTime(dateTimePicker2.Text).AddDays(1) + "') union all  select cat.cat_id , cat.datetime ,'Sales', 'Invc# ' + CONVERT(varchar(50), s.ss_id) , s.receive,'0','','' from cash_transaction cat, sale_stock s where cat.cat_id=s.cat_id AND (cat.datetime Between '" + Convert.ToDateTime(dateTimePicker1.Text) + "' AND '" + Convert.ToDateTime(dateTimePicker2.Text).AddDays(1) + "') union all select cat.cat_id , cat.datetime ,'Purchases Return', 'PR# ' + CONVERT(varchar(50),pr.pr_id) , pr.receive,'0','','' from cash_transaction cat, purchase_return pr where cat.cat_id=pr.cat_id AND (cat.datetime Between '" + Convert.ToDateTime(dateTimePicker1.Text) + "' AND '" + Convert.ToDateTime(dateTimePicker2.Text).AddDays(1) + "') union all (select cat.cat_id , cat.datetime,CASE WHEN c.type = 'FIXED' THEN 'Customer' WHEN c.type='CAPITAL' THEN 'Capital'WHEN c.type='EXPENSE' THEN 'Direct Expenses' WHEN c.type='OPENING' THEN 'Opening Balance' WHEN c.type='CLOSING' THEN 'Closing Balance' ELSE 'Stakeholder' END, c.c_name, '0', ct.amount,'','' from cash_transaction cat, customer_transaction ct,customer c where cat.cat_id=ct.cat_id AND ct.c_id=c.c_id AND ct.status='PAID' AND ct.type='True' AND (cat.datetime Between '" + Convert.ToDateTime(dateTimePicker1.Text) + "' AND '" + Convert.ToDateTime(dateTimePicker2.Text).AddDays(1) + "') union select cat.cat_id , cat.datetime,CASE WHEN c.type = 'FIXED' THEN 'Customer' WHEN c.type='CAPITAL' THEN 'Capital'WHEN c.type='EXPENSE' THEN 'Direct Expenses' WHEN c.type='OPENING' THEN 'Opening Balance' WHEN c.type='CLOSING' THEN 'Closing Balance' ELSE 'Stakeholder' END, c.c_name, ct.amount , '0', '', '' from cash_transaction cat, customer_transaction ct,customer c where cat.cat_id=ct.cat_id AND ct.c_id=c.c_id AND ct.status='RECEIVED' AND ct.type='True' AND (cat.datetime Between '" + Convert.ToDateTime(dateTimePicker1.Text) + "' AND '" + Convert.ToDateTime(dateTimePicker2.Text).AddDays(1) + "')) union all select cat.cat_id , cat.datetime,'Sales Return', 'SR# ' + CONVERT(varchar(50),sr.sr_id),'0' , sr.Paid , '' , ''  from cash_transaction cat, sale_return sr where cat.cat_id=sr.cat_id AND (cat.datetime Between '" + Convert.ToDateTime(dateTimePicker1.Text) + "' AND '" + Convert.ToDateTime(dateTimePicker2.Text).AddDays(1) + "') union all select cat.cat_id , cat.datetime,'Purchases' , 'DC# ' + CONVERT(varchar(50),ps.ps_id), '0' , ps.Paid , '' , ''  from cash_transaction cat,purchase_stock ps where cat.cat_id=ps.cat_id AND (cat.datetime Between '" + Convert.ToDateTime(dateTimePicker1.Text) + "' AND '" + Convert.ToDateTime(dateTimePicker2.Text).AddDays(1) + "') union all (select cat.cat_id , cat.datetime,'Manufacturer' , m.name, '0' , mt.amount , '' , '' from cash_transaction cat, manu_transaction mt,manufacturer m where cat.cat_id=mt.cat_id AND mt.m_id=m.m_id AND mt.status='PAID' AND mt.type='True' AND (cat.datetime Between '" + Convert.ToDateTime(dateTimePicker1.Text) + "' AND '" + Convert.ToDateTime(dateTimePicker2.Text).AddDays(1) + "') union select cat.cat_id , cat.datetime,'Manufacturer' , m.name, mt.amount , '0' , '' , ''  from cash_transaction cat, manu_transaction mt,manufacturer m where cat.cat_id=mt.cat_id AND mt.m_id=m.m_id AND mt.status='RECEIVED' AND mt.type='True' AND (cat.datetime Between '" + Convert.ToDateTime(dateTimePicker1.Text) + "' AND '" + Convert.ToDateTime(dateTimePicker2.Text).AddDays(1) + "'))) as U where U.Account Like'" + cmbquality.Text + "%'", Login.con);
                    DataTable dataTable1 = new DataTable();
                    a.Fill(dataTable1);
                    dataTable1.DefaultView.Sort = "DateTime";
                    dataTable1 = dataTable1.DefaultView.ToTable();
                    foreach (DataRow row in dataTable1.Rows)
                    {
                        if (flag)
                        {
                            dataTable1.Rows[0]["Debit"] = balance;
                            dataTable1.Rows[0]["Balance"] = balance;
                            dataTable1.Rows[0]["Status"] = "Debit";
                            dataTable1.Rows[0]["DateTime"] = Convert.ToDateTime(dateTimePicker1.Value).AddDays(-1);
                            flag = false;
                        }
                        for (; col < dataTable1.Rows.Count; col++)
                        {
                            if (Convert.ToDecimal(dataTable1.Rows[col]["Debit"]) != 0)
                            {
                                balance += Convert.ToDecimal(dataTable1.Rows[col]["Debit"]);
                                if (balance < 0)
                                {
                                    dataTable1.Rows[col]["Status"] = "Credit";
                                    dataTable1.Rows[col]["Balance"] = Convert.ToDecimal(Convert.ToString(balance).Substring(1));
                                }
                                else
                                {
                                    dataTable1.Rows[col]["Status"] = "Debit";
                                    dataTable1.Rows[col]["Balance"] = balance;
                                }
                            }
                            else if (Convert.ToDecimal(dataTable1.Rows[col]["Credit"]) != 0)
                            {
                                balance -= Convert.ToDecimal(dataTable1.Rows[col]["Credit"]);
                                if (balance < 0)
                                {
                                    dataTable1.Rows[col]["Status"] = "Credit";
                                    dataTable1.Rows[col]["Balance"] = Convert.ToDecimal(Convert.ToString(balance).Substring(1));
                                }
                                else
                                {
                                    dataTable1.Rows[col]["Status"] = "Debit";
                                    dataTable1.Rows[col]["Balance"] = balance;
                                }
                            }
                        }
                    }
                    dataGridView.DataSource = dataTable1;
                }
                else
                {
                    SqlDataAdapter a = new SqlDataAdapter("select * from (select cat.cat_id CAT_ID, cat.datetime DateTime,'Indirect Expenses' Account, e.description Description, '0' Debit ,e.amount Credit, '' Status, '' Balance from cash_transaction cat, expense e where cat.cat_id=e.cat_id AND (cat.datetime Between '" + Convert.ToDateTime(dateTimePicker1.Text) + "' AND '" + Convert.ToDateTime(dateTimePicker2.Text).AddDays(1) + "') union all  select cat.cat_id , cat.datetime ,'Sales','Invc# ' + CONVERT(varchar(50), s.ss_id) , s.receive,'0','','' from cash_transaction cat, sale_stock s where cat.cat_id=s.cat_id AND (cat.datetime Between '" + Convert.ToDateTime(dateTimePicker1.Text) + "' AND '" + Convert.ToDateTime(dateTimePicker2.Text).AddDays(1) + "') union all select cat.cat_id , cat.datetime ,'Purchases Return', 'PR# ' + CONVERT(varchar(50),pr.pr_id) , pr.receive,'0','','' from cash_transaction cat, purchase_return pr where cat.cat_id=pr.cat_id AND (cat.datetime Between '" + Convert.ToDateTime(dateTimePicker1.Text) + "' AND '" + Convert.ToDateTime(dateTimePicker2.Text).AddDays(1) + "') union all (select cat.cat_id , cat.datetime,CASE WHEN c.type = 'FIXED' THEN 'Customer' WHEN c.type='CAPITAL' THEN 'Capital'WHEN c.type='EXPENSE' THEN 'Direct Expenses' WHEN c.type='OPENING' THEN 'Opening Balance' WHEN c.type='CLOSING' THEN 'Closing Balance' ELSE 'Stakeholder' END, c.c_name, '0', ct.amount,'','' from cash_transaction cat, customer_transaction ct,customer c where cat.cat_id=ct.cat_id AND ct.c_id=c.c_id AND ct.status='PAID' AND ct.type='True' AND (cat.datetime Between '" + Convert.ToDateTime(dateTimePicker1.Text) + "' AND '" + Convert.ToDateTime(dateTimePicker2.Text).AddDays(1) + "') union select cat.cat_id , cat.datetime,CASE WHEN c.type = 'FIXED' THEN 'Customer' WHEN c.type='CAPITAL' THEN 'Capital'WHEN c.type='EXPENSE' THEN 'Direct Expenses' WHEN c.type='OPENING' THEN 'Opening Balance' WHEN c.type='CLOSING' THEN 'Closing Balance' ELSE 'Stakeholder' END, c.c_name, ct.amount , '0', '', '' from cash_transaction cat, customer_transaction ct,customer c where cat.cat_id=ct.cat_id AND ct.c_id=c.c_id AND ct.status='RECEIVED' AND ct.type='True' AND (cat.datetime Between '" + Convert.ToDateTime(dateTimePicker1.Text) + "' AND '" + Convert.ToDateTime(dateTimePicker2.Text).AddDays(1) + "')) union all select cat.cat_id , cat.datetime,'Sales Return', 'SR# '+CONVERT(varchar(50),sr.sr_id),'0' , sr.Paid , '' , ''  from cash_transaction cat, sale_return sr where cat.cat_id=sr.cat_id AND (cat.datetime Between '" + Convert.ToDateTime(dateTimePicker1.Text) + "' AND '" + Convert.ToDateTime(dateTimePicker2.Text).AddDays(1) + "') union all select cat.cat_id , cat.datetime,'Purchases' , 'DC# ' + CONVERT(varchar(50),ps.ps_id), '0' , ps.Paid , '' , ''  from cash_transaction cat,purchase_stock ps where cat.cat_id=ps.cat_id AND (cat.datetime Between '" + Convert.ToDateTime(dateTimePicker1.Text) + "' AND '" + Convert.ToDateTime(dateTimePicker2.Text).AddDays(1) + "') union all (select cat.cat_id , cat.datetime,'Manufacturer' , m.name, '0' , mt.amount , '' , '' from cash_transaction cat, manu_transaction mt,manufacturer m where cat.cat_id=mt.cat_id AND mt.m_id=m.m_id AND mt.status='PAID' AND mt.type='True' AND (cat.datetime Between '" + Convert.ToDateTime(dateTimePicker1.Text) + "' AND '" + Convert.ToDateTime(dateTimePicker2.Text).AddDays(1) + "') union select cat.cat_id , cat.datetime,'Manufacturer' , m.name, mt.amount , '0' , '' , ''  from cash_transaction cat, manu_transaction mt,manufacturer m where cat.cat_id=mt.cat_id AND mt.m_id=m.m_id AND mt.status='RECEIVED' AND mt.type='True' AND (cat.datetime Between '" + Convert.ToDateTime(dateTimePicker1.Text) + "' AND '" + Convert.ToDateTime(dateTimePicker2.Text).AddDays(1) + "'))) as U where U.Account Like'" + cmbquality.Text + "%' order by U.DateTime", Login.con);
                    DataTable dataTable1 = new DataTable();
                    a.Fill(dataTable1);
                    foreach (DataRow row in dataTable1.Rows)
                    {
                        if (Convert.ToDecimal(row["Debit"]) != 0)
                        {
                            balance += Convert.ToDecimal(row["Debit"]);
                            if (balance < 0)
                            {
                                row["Status"] = "Credit";
                                row["Balance"] = Convert.ToDecimal(Convert.ToString(balance).Substring(1));
                            }
                            else
                            {
                                row["Status"] = "Debit";
                                row["Balance"] = balance;
                            }
                        }
                        else
                        {
                            balance -= Convert.ToDecimal(row["Credit"]);
                            if (balance < 0)
                            {
                                row["Status"] = "Credit";
                                row["Balance"] = Convert.ToDecimal(Convert.ToString(balance).Substring(1));
                            }
                            else
                            {
                                row["Status"] = "Debit";
                                row["Balance"] = balance;
                            }
                        }
                    }
                    dataGridView.DataSource = dataTable1;
                }
            }
            else
            {
                dr5.Close();
                dataGridView.DataSource = null;
            }
            Login.con.Close();
        }

        private void dataGridView_CellContentDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            string str = dataGridView.Rows[e.RowIndex].Cells["Account"].FormattedValue.ToString();
            if (str == "Sales")
            {
                string str2 = dataGridView.Rows[e.RowIndex].Cells["Description"].FormattedValue.ToString();
                string invc = str2.Substring(6);
                Invoice_Detail f = new Invoice_Detail(Convert.ToDecimal(invc));
                f.Show();
            }
            else if (str == "Sales Return")
            {
                string str2 = dataGridView.Rows[e.RowIndex].Cells["Description"].FormattedValue.ToString();
                string invc = str2.Substring(4);
                Sales_Return_Detail f = new Sales_Return_Detail(Convert.ToDecimal(invc));
                f.Show();
            }
            else if (str == "Purchases")
            {
                string str2 = dataGridView.Rows[e.RowIndex].Cells["Description"].FormattedValue.ToString();
                string invc = str2.Substring(4);
                Purchases_Detail f = new Purchases_Detail(Convert.ToDecimal(invc));
                f.Show();
            }
            else if (str == "Purchases Return")
            {
                string str2 = dataGridView.Rows[e.RowIndex].Cells["Description"].FormattedValue.ToString();
                string invc = str2.Substring(4);
                Purchases_Return_Detail f = new Purchases_Return_Detail(Convert.ToDecimal(invc));
                f.Show();
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                debit = credit = 0;
                printDocument1.Print();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private int i = 0;
        private void printDocument1_PrintPage(object sender, PrintPageEventArgs e)
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
            e.Graphics.DrawString("Cash Book", new Font("Calibri", 12, FontStyle.Underline), new SolidBrush(Color.Black), startX, startY + Offset);
            Offset = Offset + 30;
            e.Graphics.DrawString("From:", new Font("Calibri", 10, FontStyle.Bold), new SolidBrush(Color.Black), startX, startY + Offset);
            e.Graphics.DrawString(dateTimePicker1.Text, new Font("Calibri", 10), new SolidBrush(Color.Black), startX + 40, startY + Offset);
            e.Graphics.DrawString("To:", new Font("Calibri", 10, FontStyle.Bold), new SolidBrush(Color.Black), startX + 380, startY + Offset);
            e.Graphics.DrawString(dateTimePicker2.Text, new Font("Calibri", 10), new SolidBrush(Color.Black), startX + 405, startY + Offset);
            Offset = Offset + 20;
            string underLine = "=======================================================================================";
            e.Graphics.DrawString(underLine, new Font("Courier New", 10, FontStyle.Bold), new SolidBrush(Color.Black), startX, startY + Offset);
            Offset = Offset + 20;
            e.Graphics.DrawString("CT_ID", new Font("Calibri", 10, FontStyle.Bold), new SolidBrush(Color.Black), startX, startY + Offset);
            e.Graphics.DrawString("Date & Time", new Font("Calibri", 10, FontStyle.Bold), new SolidBrush(Color.Black), startX + 60, startY + Offset);
            e.Graphics.DrawString("Account", new Font("Calibri", 10, FontStyle.Bold), new SolidBrush(Color.Black), startX + 330 - 135, startY + Offset);
            e.Graphics.DrawString("Description", new Font("Calibri", 10, FontStyle.Bold), new SolidBrush(Color.Black), startX + 280, startY + Offset);
            e.Graphics.DrawString("Debit", new Font("Calibri", 10, FontStyle.Bold), new SolidBrush(Color.Black), startX + 425, startY + Offset);
            e.Graphics.DrawString("Credit", new Font("Calibri", 10, FontStyle.Bold), new SolidBrush(Color.Black), startX + 515, startY + Offset);
            e.Graphics.DrawString("Status", new Font("Calibri", 10, FontStyle.Bold), new SolidBrush(Color.Black), startX + 605, startY + Offset);
            e.Graphics.DrawString("Balance", new Font("Calibri", 10, FontStyle.Bold), new SolidBrush(Color.Black), startX + 670, startY + Offset);
            Offset = Offset + 20;
            e.Graphics.DrawString(underLine, new Font("Courier New", 10, FontStyle.Bold), new SolidBrush(Color.Black), startX, startY + Offset);
            Offset = Offset + 20;
            int a = dataGridView.Rows.Count;
            for (; i < a; i++)
            {
                e.Graphics.DrawString(Convert.ToString(dataGridView.Rows[i].Cells[0].Value), new Font("Calibri", 8), new SolidBrush(Color.Black), startX, startY + Offset);
                e.Graphics.DrawString(Convert.ToString(dataGridView.Rows[i].Cells[1].Value), new Font("Calibri", 8), new SolidBrush(Color.Black), startX + 60, startY + Offset);
                e.Graphics.DrawString(Convert.ToString(dataGridView.Rows[i].Cells[2].Value), new Font("Calibri", 8), new SolidBrush(Color.Black), startX + 330 - 135, startY + Offset);
                e.Graphics.DrawString(Convert.ToString(dataGridView.Rows[i].Cells[3].Value), new Font("Calibri", 8), new SolidBrush(Color.Black), startX + 280, startY + Offset);
                if (Convert.ToString(dataGridView.Rows[i].Cells[4].Value) != "0.00")
                {
                    e.Graphics.DrawString(Convert.ToString(dataGridView.Rows[i].Cells[4].Value), new Font("Calibri", 8), new SolidBrush(Color.Black), startX + 425, startY + Offset);
                    debit += Convert.ToDecimal(dataGridView.Rows[i].Cells[4].Value);
                }
                else
                    e.Graphics.DrawString("", new Font("Calibri", 8), new SolidBrush(Color.Black), startX + 425, startY + Offset);
                if (Convert.ToString(dataGridView.Rows[i].Cells[5].Value) != "0.00")
                {
                    e.Graphics.DrawString(Convert.ToString(dataGridView.Rows[i].Cells[5].Value), new Font("Calibri", 8), new SolidBrush(Color.Black), startX + 515, startY + Offset);
                    credit += Convert.ToDecimal(dataGridView.Rows[i].Cells[5].Value);
                }
                else
                    e.Graphics.DrawString("", new Font("Calibri", 8), new SolidBrush(Color.Black), startX + 515, startY + Offset);
                e.Graphics.DrawString(Convert.ToString(dataGridView.Rows[i].Cells[6].Value), new Font("Calibri", 8), new SolidBrush(Color.Black), startX + 605, startY + Offset);
                e.Graphics.DrawString(Convert.ToString(dataGridView.Rows[i].Cells[7].Value), new Font("Calibri", 8), new SolidBrush(Color.Black), startX + 670, startY + Offset);
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
            e.Graphics.DrawString(Convert.ToString(debit), new Font("Calibri", 8), new SolidBrush(Color.Black), startX + 425, startY + Offset);
            e.Graphics.DrawString(Convert.ToString(credit), new Font("Calibri", 8), new SolidBrush(Color.Black), startX + 515, startY + Offset);
            if (Convert.ToString(debit - credit).Contains("-"))
                e.Graphics.DrawString("Cr. " + Convert.ToString(debit - credit).Substring(1), new Font("Calibri", 8), new SolidBrush(Color.Black), startX + 670, startY + Offset);
            else
                e.Graphics.DrawString("Dr. " + Convert.ToString(debit - credit), new Font("Calibri", 8), new SolidBrush(Color.Black), startX + 670, startY + Offset);
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
