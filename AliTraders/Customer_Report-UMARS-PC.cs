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
using System.Globalization;

namespace Verona
{
    public partial class Customer_Report : Form
    {
        Form opener;
        Decimal debit, credit, balance, sdebit, scredit, srdebit, srcredit, tdebit, tcredit, pdebit, pcredit, prcredit, prdebit;
        String type;
        UF.Class1 test = new UF.Class1();

        public Customer_Report(Form from)
        {
            InitializeComponent();
            opener = from;
        }

        private void Customer_Report_Load(object sender, EventArgs e)
        {
            opener.Enabled = false;
            cmbquality.SelectedIndex = 0;
        }

        private void Customer_Report_FormClosing(object sender, FormClosingEventArgs e)
        {
            opener.Enabled = true;
        }

        private void cmbname_Enter(object sender, EventArgs e)
        {
            if (cmbquality.Text == "Customer" || cmbquality.Text == "Capital" || cmbquality.Text == "Direct Expenses" || cmbquality.Text == "Stakeholder" || cmbquality.Text == "Opening Balance" || cmbquality.Text == "Closing Balance")
            {
                if (cmbquality.Text == "Customer")
                    type = "FIXED";
                else if (cmbquality.Text == "Direct Expenses")
                    type = "EXPENSE";
                else if (cmbquality.Text == "Stakeholder")
                    type = "STAKEHOLDER";
                else if (cmbquality.Text == "Opening Balance")
                    type = "OPENING";
                else if (cmbquality.Text == "Closing Balance")
                    type = "CLOSING";
                else
                    type = "CAPITAL";
                string command = "select c_name from customer where type='" + type + "' order by c_name";
                test.cmbox_drop(command, cmbname, Login.con);
            }
            else if (cmbquality.Text == "Manufacturer")
            {
                string command = "select name from manufacturer order by name";
                test.cmbox_drop(command, cmbname, Login.con);
            }
        }

        private void cmbname_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                cmbname.Text = cmbname.Text.ToUpper();
                cmbname.SelectAll();
            }
        }

        private void btnsearch_Click(object sender, EventArgs e)
        {
            if (cmbname.Text != "" && cmbquality.Text != "")
            {
                if (cmbquality.Text == "Customer" || cmbquality.Text == "Capital" || cmbquality.Text == "Direct Expenses" || cmbquality.Text == "Stakeholder" || cmbquality.Text == "Opening Balance" || cmbquality.Text == "Closing Balance")
                {
                    balance = sdebit = scredit = srdebit = srcredit = tdebit = tcredit = pcredit = pdebit = prcredit = prdebit = 0;
                    Login.con.Open();
                    SqlCommand cmd5 = new SqlCommand("select cus.cus_id CT_ID,c.c_name Name,cus.datetime DateTime,'Sales' Account, 'Invc# ' + Convert(varchar(50),ss.ss_id) Description, (ss.total-ss.discount) Debit ,ss.receive Credit,'' Status, '' Balance from customer c, cus_transaction cus,sale_stock ss where c.c_id=ss.c_id AND c.type='" + type + "' AND cus.cus_id=ss.cus_id AND c.c_name='" + cmbname.Text.ToUpper() + "'AND(cus.datetime Between '" + Convert.ToDateTime(dateTimePicker1.Text) + "' AND '" + Convert.ToDateTime(dateTimePicker2.Text).AddDays(1) + "') UNION select cus.cus_id CT_ID,c.c_name Name,cus.datetime DateTime,'Purchases' Account,'DC# ' + Convert(varchar(50),ps.ps_id) Description, ps.paid Debit , ps.total Credit,'' Status, '' Balance from customer c, cus_transaction cus,purchase_stock ps where c.c_id=ps.c_id AND c.type='" + type + "' AND cus.cus_id=ps.cus_id AND c.c_name='" + cmbname.Text.ToUpper() + "'AND(cus.datetime Between '" + Convert.ToDateTime(dateTimePicker1.Text) + "' AND '" + Convert.ToDateTime(dateTimePicker2.Text).AddDays(1) + "') UNION select cus.cus_id CT_ID,c.c_name Name,cus.datetime DateTime,'Purchases Return' Account,'PR# ' + Convert(varchar(50),pr.pr_id) Description, pr.total Debit , pr.receive Credit,'' Status, '' Balance from customer c, cus_transaction cus,purchase_return pr where c.c_id=pr.c_id AND c.type='" + type + "' AND cus.cus_id=pr.cus_id AND c.c_name='" + cmbname.Text.ToUpper() + "'AND(cus.datetime Between '" + Convert.ToDateTime(dateTimePicker1.Text) + "' AND '" + Convert.ToDateTime(dateTimePicker2.Text).AddDays(1) + "') UNION select cus.cus_id CT_ID, c.c_name Name, cus.datetime DateTime,'Sales Return' Account,'SR# ' + Convert(varchar(50), sr.sr_id) Description, sr.Paid Debit, (sr.total-sr.deduct) Credit, '' Status, '' Balance from customer c, cus_transaction cus, sale_return sr where c.c_id = sr.c_id AND c.type = '" + type + "' AND cus.cus_id = sr.cus_id AND c.c_name = '" + cmbname.Text.ToUpper() + "'AND(cus.datetime Between '" + Convert.ToDateTime(dateTimePicker1.Text) + "' AND '" + Convert.ToDateTime(dateTimePicker2.Text).AddDays(1) + "') UNION (select cus.cus_id CT_ID, c.c_name Name, cus.datetime DateTime, 'Transaction' Account, ct.description Description, '0' Credit, ct.amount Debit, '' Status, '' Balance from customer c, cus_transaction cus, customer_transaction ct where c.c_id = ct.c_id AND c.type = '" + type + "' AND cus.cus_id = ct.cus_id AND c.c_name = '" + cmbname.Text.ToUpper() + "'AND(cus.datetime Between '" + Convert.ToDateTime(dateTimePicker1.Text) + "' AND '" + Convert.ToDateTime(dateTimePicker2.Text).AddDays(1) + "') AND ct.status = 'RECEIVED' UNION ALL select cus.cus_id CT_ID, c.c_name Name, cus.datetime DateTime, 'Transaction' Account, ct.description Description, ct.amount Credit, '0' Debit, '' Status, '' Balance from customer c, cus_transaction cus, customer_transaction ct where c.c_id = ct.c_id AND c.type = '" + type + "' AND cus.cus_id = ct.cus_id AND c.c_name = '" + cmbname.Text.ToUpper() + "'AND(cus.datetime Between '" + Convert.ToDateTime(dateTimePicker1.Text) + "' AND '" + Convert.ToDateTime(dateTimePicker2.Text).AddDays(1) + "') AND ct.status = 'PAID') order by DateTime", Login.con);
                    SqlDataReader dr5 = cmd5.ExecuteReader();
                    if (dr5.HasRows)
                    {
                        dr5.Close();
                        SqlCommand cmd = new SqlCommand("select (ss.total-ss.discount) Debit ,ss.receive Credit from customer c, cus_transaction cus,sale_stock ss where c.c_id=ss.c_id AND c.type='" + type + "' AND cus.cus_id=ss.cus_id AND c.c_name='" + cmbname.Text.ToUpper() + "'AND cus.datetime <= '" + Convert.ToDateTime(dateTimePicker1.Text) + "'", Login.con);
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
                        SqlCommand cmd7 = new SqlCommand("select ps.paid Debit,ps.total Credit from customer c, cus_transaction cus,purchase_stock ps where c.c_id=ps.c_id AND cus.cus_id=ps.cus_id AND c.c_name = '" + cmbname.Text.ToUpper() + "' AND cus.datetime <= '" + Convert.ToDateTime(dateTimePicker1.Text) + "'", Login.con);
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
                        SqlCommand cmd2 = new SqlCommand("select sr.Paid Debit, (sr.total-sr.deduct) Credit from customer c, cus_transaction cus, sale_return sr where c.c_id = sr.c_id AND c.type = '" + type + "' AND cus.cus_id = sr.cus_id AND c.c_name = '" + cmbname.Text.ToUpper() + "'AND cus.datetime  <= '" + Convert.ToDateTime(dateTimePicker1.Text) + "'", Login.con);
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
                        SqlCommand cmd22 = new SqlCommand("select pr.total Debit, pr.receive Credit from customer c, cus_transaction cus, purchase_return pr where c.c_id = pr.c_id AND c.type = '" + type + "' AND cus.cus_id = pr.cus_id AND c.c_name = '" + cmbname.Text.ToUpper() + "'AND cus.datetime  <= '" + Convert.ToDateTime(dateTimePicker1.Text) + "'", Login.con);
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
                        SqlCommand cmd3 = new SqlCommand("select ct.amount Debit from customer c, cus_transaction cus, customer_transaction ct where c.c_id = ct.c_id AND c.type = '" + type + "' AND cus.cus_id = ct.cus_id AND c.c_name = '" + cmbname.Text.ToUpper() + "'AND cus.datetime <= '" + Convert.ToDateTime(dateTimePicker1.Text) + "' AND ct.status = 'PAID'", Login.con);
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
                        SqlCommand cmd4 = new SqlCommand("select ct.amount Credit from customer c, cus_transaction cus, customer_transaction ct where c.c_id = ct.c_id AND c.type = '" + type + "' AND cus.cus_id = ct.cus_id AND c.c_name = '" + cmbname.Text.ToUpper() + "'AND cus.datetime <= '" + Convert.ToDateTime(dateTimePicker1.Text) + "' AND ct.status = 'RECEIVED'", Login.con);
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
                        balance = ((sdebit + tdebit + pdebit + srdebit + prdebit) - (scredit + tcredit + pcredit + srcredit + prcredit));
                        if (balance != 0 && balance < 0)
                        {
                            Boolean flag = true;
                            int col = 1;
                            SqlDataAdapter a = new SqlDataAdapter("select '1' CT_ID, '" + cmbname.Text + "' Name, '' DateTime,'Pre. A/c' Account,'' Description, '0' Debit, '0' Credit,'' Status, '' Balance Union select cus.cus_id CT_ID,c.c_name Name,cus.datetime DateTime,'Sales' Account,'Invc# ' +Convert(varchar(50),ss.ss_id) Description, (ss.total-ss.discount) Debit ,ss.receive Credit,'' Status, '' Balance from customer c, cus_transaction cus,sale_stock ss where c.c_id=ss.c_id AND c.type='" + type + "' AND cus.cus_id=ss.cus_id AND c.c_name='" + cmbname.Text.ToUpper() + "'AND(cus.datetime Between '" + Convert.ToDateTime(dateTimePicker1.Text) + "' AND '" + Convert.ToDateTime(dateTimePicker2.Text).AddDays(1) + "') UNION select cus.cus_id CT_ID,c.c_name Name,cus.datetime DateTime,'Purchases' Account,'DC# ' + Convert(varchar(50),ps.ps_id) Description, ps.paid Debit , ps.total Credit,'' Status, '' Balance from customer c, cus_transaction cus,purchase_stock ps where c.c_id=ps.c_id AND c.type='" + type + "' AND cus.cus_id=ps.cus_id AND c.c_name='" + cmbname.Text.ToUpper() + "'AND(cus.datetime Between '" + Convert.ToDateTime(dateTimePicker1.Text) + "' AND '" + Convert.ToDateTime(dateTimePicker2.Text).AddDays(1) + "') UNION select cus.cus_id CT_ID,c.c_name Name,cus.datetime DateTime,'Purchases Return' Account,'PR# ' + Convert(varchar(50),pr.pr_id) Description, pr.total Debit , pr.receive Credit,'' Status, '' Balance from customer c, cus_transaction cus,purchase_return pr where c.c_id=pr.c_id AND c.type='" + type + "' AND cus.cus_id=pr.cus_id AND c.c_name='" + cmbname.Text.ToUpper() + "'AND(cus.datetime Between '" + Convert.ToDateTime(dateTimePicker1.Text) + "' AND '" + Convert.ToDateTime(dateTimePicker2.Text).AddDays(1) + "') UNION select cus.cus_id CT_ID, c.c_name Name, cus.datetime DateTime,'Sales Return' Account,'SR# ' + Convert(varchar(50), sr.sr_id) Description, sr.Paid Debit, (sr.total-sr.deduct) Credit, '' Status, '' Balance from customer c, cus_transaction cus, sale_return sr where c.c_id = sr.c_id AND c.type = '" + type + "' AND cus.cus_id = sr.cus_id AND c.c_name = '" + cmbname.Text.ToUpper() + "'AND(cus.datetime Between '" + Convert.ToDateTime(dateTimePicker1.Text) + "' AND '" + Convert.ToDateTime(dateTimePicker2.Text).AddDays(1) + "') UNION (select cus.cus_id CT_ID, c.c_name Name, cus.datetime DateTime, 'Transaction' Account, ct.description Description, '0' Credit, ct.amount Debit, '' Status, '' Balance from customer c, cus_transaction cus, customer_transaction ct where c.c_id = ct.c_id AND c.type = '" + type + "' AND cus.cus_id = ct.cus_id AND c.c_name = '" + cmbname.Text.ToUpper() + "'AND(cus.datetime Between '" + Convert.ToDateTime(dateTimePicker1.Text) + "' AND '" + Convert.ToDateTime(dateTimePicker2.Text).AddDays(1) + "') AND ct.status = 'RECEIVED' UNION ALL select cus.cus_id CT_ID, c.c_name Name, cus.datetime DateTime, 'Transaction' Account, ct.description Description, ct.amount Credit, '0' Debit, '' Status, '' Balance from customer c, cus_transaction cus, customer_transaction ct where c.c_id = ct.c_id AND c.type = '" + type + "' AND cus.cus_id = ct.cus_id AND c.c_name = '" + cmbname.Text.ToUpper() + "'AND(cus.datetime Between '" + Convert.ToDateTime(dateTimePicker1.Text) + "' AND '" + Convert.ToDateTime(dateTimePicker2.Text).AddDays(1) + "') AND ct.status = 'PAID') order by DateTime", Login.con);
                            DataTable dataTable1 = new DataTable();
                            a.Fill(dataTable1);
                            foreach (DataRow row in dataTable1.Rows)
                            {
                                if (flag)
                                {
                                    dataTable1.Rows[0]["Balance"] = Convert.ToDecimal(Convert.ToString(balance).Substring(1));
                                    dataTable1.Rows[0]["Credit"] = Convert.ToDecimal(Convert.ToString(balance).Substring(1));
                                    dataTable1.Rows[0]["Status"] = "Credit";
                                    dataTable1.Rows[0]["DateTime"] = Convert.ToDateTime(dataTable1.Rows[1]["DateTime"]).AddDays(-1);
                                    flag = false;
                                }
                                for (; col < dataTable1.Rows.Count; col++)
                                {
                                    if (Convert.ToDecimal(dataTable1.Rows[col]["Debit"]) != 0 && Convert.ToDecimal(dataTable1.Rows[col]["Credit"]) == 0)
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
                                    else if (Convert.ToDecimal(dataTable1.Rows[col]["Credit"]) != 0 && Convert.ToDecimal(dataTable1.Rows[col]["Debit"]) == 0)
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
                                    else
                                    {
                                        balance += (Convert.ToDecimal(dataTable1.Rows[col]["Debit"]) - Convert.ToDecimal(dataTable1.Rows[col]["Credit"]));
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
                            SqlDataAdapter a = new SqlDataAdapter("select '1' CT_ID, '" + cmbname.Text + "' Name, '' DateTime,'Pre. A/c' Account,'' Description, '0' Debit, '0' Credit,'' Status, '' Balance Union select cus.cus_id CT_ID,c.c_name Name,cus.datetime DateTime,'Sales' Account,'Invc# ' +Convert(varchar(50),ss.ss_id) Description, (ss.total-ss.discount) Debit ,ss.receive Credit,'' Status, '' Balance from customer c, cus_transaction cus,sale_stock ss where c.c_id=ss.c_id AND c.type='" + type + "' AND cus.cus_id=ss.cus_id AND c.c_name='" + cmbname.Text.ToUpper() + "'AND(cus.datetime Between '" + Convert.ToDateTime(dateTimePicker1.Text) + "' AND '" + Convert.ToDateTime(dateTimePicker2.Text).AddDays(1) + "') UNION select cus.cus_id CT_ID,c.c_name Name,cus.datetime DateTime,'Purchases' Account,'DC# ' + Convert(varchar(50),ps.ps_id) Description, ps.paid Debit , ps.total Credit,'' Status, '' Balance from customer c, cus_transaction cus,purchase_stock ps where c.c_id=ps.c_id AND c.type='" + type + "' AND cus.cus_id=ps.cus_id AND c.c_name='" + cmbname.Text.ToUpper() + "'AND(cus.datetime Between '" + Convert.ToDateTime(dateTimePicker1.Text) + "' AND '" + Convert.ToDateTime(dateTimePicker2.Text).AddDays(1) + "') UNION select cus.cus_id CT_ID,c.c_name Name,cus.datetime DateTime,'Purchases Return' Account,'PR# ' + Convert(varchar(50),pr.pr_id) Description, pr.total Debit , pr.receive Credit,'' Status, '' Balance from customer c, cus_transaction cus,purchase_return pr where c.c_id=pr.c_id AND c.type='" + type + "' AND cus.cus_id=pr.cus_id AND c.c_name='" + cmbname.Text.ToUpper() + "'AND(cus.datetime Between '" + Convert.ToDateTime(dateTimePicker1.Text) + "' AND '" + Convert.ToDateTime(dateTimePicker2.Text).AddDays(1) + "') UNION select cus.cus_id CT_ID, c.c_name Name, cus.datetime DateTime,'Sales Return' Account,'SR# ' + Convert(varchar(50), sr.sr_id) Description, sr.Paid Debit, (sr.total-sr.deduct) Credit, '' Status, '' Balance from customer c, cus_transaction cus, sale_return sr where c.c_id = sr.c_id AND c.type = '" + type + "' AND cus.cus_id = sr.cus_id AND c.c_name = '" + cmbname.Text.ToUpper() + "'AND(cus.datetime Between '" + Convert.ToDateTime(dateTimePicker1.Text) + "' AND '" + Convert.ToDateTime(dateTimePicker2.Text).AddDays(1) + "') UNION (select cus.cus_id CT_ID, c.c_name Name, cus.datetime DateTime, 'Transaction' Account, ct.description Description, '0' Credit, ct.amount Debit, '' Status, '' Balance from customer c, cus_transaction cus, customer_transaction ct where c.c_id = ct.c_id AND c.type = '" + type + "' AND cus.cus_id = ct.cus_id AND c.c_name = '" + cmbname.Text.ToUpper() + "'AND(cus.datetime Between '" + Convert.ToDateTime(dateTimePicker1.Text) + "' AND '" + Convert.ToDateTime(dateTimePicker2.Text).AddDays(1) + "') AND ct.status = 'RECEIVED' UNION ALL select cus.cus_id CT_ID, c.c_name Name, cus.datetime DateTime, 'Transaction' Account, ct.description Description, ct.amount Credit, '0' Debit, '' Status, '' Balance from customer c, cus_transaction cus, customer_transaction ct where c.c_id = ct.c_id AND c.type = '" + type + "' AND cus.cus_id = ct.cus_id AND c.c_name = '" + cmbname.Text.ToUpper() + "'AND(cus.datetime Between '" + Convert.ToDateTime(dateTimePicker1.Text) + "' AND '" + Convert.ToDateTime(dateTimePicker2.Text).AddDays(1) + "') AND ct.status = 'PAID') order by DateTime", Login.con);
                            DataTable dataTable1 = new DataTable();
                            a.Fill(dataTable1);
                            foreach (DataRow row in dataTable1.Rows)
                            {
                                if (flag)
                                {
                                    dataTable1.Rows[0]["Balance"] = balance;
                                    dataTable1.Rows[0]["Debit"] = balance;
                                    dataTable1.Rows[0]["Status"] = "Debit";
                                    dataTable1.Rows[0]["DateTime"] = Convert.ToDateTime(dataTable1.Rows[1]["DateTime"]).AddDays(-1);
                                    flag = false;
                                }
                                for (; col < dataTable1.Rows.Count; col++)
                                {
                                    if (Convert.ToDecimal(dataTable1.Rows[col]["Debit"]) != 0 && Convert.ToDecimal(dataTable1.Rows[col]["Credit"]) == 0)
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
                                    else if (Convert.ToDecimal(dataTable1.Rows[col]["Credit"]) != 0 && Convert.ToDecimal(dataTable1.Rows[col]["Debit"]) == 0)
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
                                    else
                                    {
                                        balance += (Convert.ToDecimal(dataTable1.Rows[col]["Debit"]) - Convert.ToDecimal(dataTable1.Rows[col]["Credit"]));
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
                            SqlDataAdapter a = new SqlDataAdapter("select cus.cus_id CT_ID,c.c_name Name,cus.datetime DateTime,'Sales' Account,'Invc# ' + Convert(varchar(50),ss.ss_id) Description, (ss.total-ss.discount) Debit ,ss.receive Credit,'' Status, '' Balance from customer c, cus_transaction cus,sale_stock ss where c.c_id=ss.c_id AND c.type='" + type + "' AND cus.cus_id=ss.cus_id AND c.c_name='" + cmbname.Text.ToUpper() + "'AND(cus.datetime Between '" + Convert.ToDateTime(dateTimePicker1.Text) + "' AND '" + Convert.ToDateTime(dateTimePicker2.Text).AddDays(1) + "') UNION select cus.cus_id CT_ID,c.c_name Name,cus.datetime DateTime,'Purchases' Account,'DC# ' + Convert(varchar(50),ps.ps_id) Description, ps.paid Debit , ps.total Credit,'' Status, '' Balance from customer c, cus_transaction cus,purchase_stock ps where c.c_id=ps.c_id AND c.type='" + type + "' AND cus.cus_id=ps.cus_id AND c.c_name='" + cmbname.Text.ToUpper() + "'AND(cus.datetime Between '" + Convert.ToDateTime(dateTimePicker1.Text) + "' AND '" + Convert.ToDateTime(dateTimePicker2.Text).AddDays(1) + "') UNION select cus.cus_id CT_ID,c.c_name Name,cus.datetime DateTime,'Purchases Return' Account,'PR# ' + Convert(varchar(50),pr.pr_id) Description, pr.total Debit , pr.receive Credit,'' Status, '' Balance from customer c, cus_transaction cus,purchase_return pr where c.c_id=pr.c_id AND c.type='" + type + "' AND cus.cus_id=pr.cus_id AND c.c_name='" + cmbname.Text.ToUpper() + "'AND(cus.datetime Between '" + Convert.ToDateTime(dateTimePicker1.Text) + "' AND '" + Convert.ToDateTime(dateTimePicker2.Text).AddDays(1) + "') UNION select cus.cus_id CT_ID, c.c_name Name, cus.datetime DateTime,'Sales Return' Account,'SR# ' + Convert(varchar(50), sr.sr_id) Description, sr.Paid Debit, (sr.total-sr.deduct) Credit, '' Status, '' Balance from customer c, cus_transaction cus, sale_return sr where c.c_id = sr.c_id AND c.type = '" + type + "' AND cus.cus_id = sr.cus_id AND c.c_name = '" + cmbname.Text.ToUpper() + "'AND(cus.datetime Between '" + Convert.ToDateTime(dateTimePicker1.Text) + "' AND '" + Convert.ToDateTime(dateTimePicker2.Text).AddDays(1) + "') UNION (select cus.cus_id CT_ID, c.c_name Name, cus.datetime DateTime, 'Transaction' Account, ct.description Description, '0' Credit, ct.amount Debit, '' Status, '' Balance from customer c, cus_transaction cus, customer_transaction ct where c.c_id = ct.c_id AND c.type = '" + type + "' AND cus.cus_id = ct.cus_id AND c.c_name = '" + cmbname.Text.ToUpper() + "'AND(cus.datetime Between '" + Convert.ToDateTime(dateTimePicker1.Text) + "' AND '" + Convert.ToDateTime(dateTimePicker2.Text).AddDays(1) + "') AND ct.status = 'RECEIVED' UNION ALL select cus.cus_id CT_ID, c.c_name Name, cus.datetime DateTime, 'Transaction' Account, ct.description Description, ct.amount Credit, '0' Debit, '' Status, '' Balance from customer c, cus_transaction cus, customer_transaction ct where c.c_id = ct.c_id AND c.type = '" + type + "' AND cus.cus_id = ct.cus_id AND c.c_name = '" + cmbname.Text.ToUpper() + "'AND(cus.datetime Between '" + Convert.ToDateTime(dateTimePicker1.Text) + "' AND '" + Convert.ToDateTime(dateTimePicker2.Text).AddDays(1) + "') AND ct.status = 'PAID') order by DateTime", Login.con);
                            DataTable dataTable1 = new DataTable();
                            a.Fill(dataTable1);
                            foreach (DataRow row in dataTable1.Rows)
                            {
                                if (Convert.ToDecimal(row["Debit"]) != 0 && Convert.ToDecimal(row["Credit"]) == 0)
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
                                else if (Convert.ToDecimal(row["Credit"]) != 0 && Convert.ToDecimal(row["Debit"]) == 0)
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
                                else
                                {
                                    balance += (Convert.ToDecimal(row["Debit"]) - Convert.ToDecimal(row["Credit"]));
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
                else if (cmbquality.Text == "Manufacturer")
                {
                    balance = sdebit = scredit = srdebit = srcredit = tdebit = tcredit = pcredit = pdebit = prdebit = prcredit = 0;
                    Login.con.Open();
                    SqlCommand cmd5 = new SqlCommand("select man.man_id MT_ID,m.name Name,man.datetime DateTime,'Purchases' Account, 'DC# ' + CONVERT(varchar(50),ps.ps_id) Description, ps.paid Debit,ps.total Credit,'' Status, '' Balance from manufacturer m, man_transaction man,purchase_stock ps where m.m_id=ps.m_id AND man.man_id=ps.man_id AND m.name = '" + cmbname.Text.ToUpper() + "' AND(man.datetime Between '" + Convert.ToDateTime(dateTimePicker1.Text) + "' AND '" + Convert.ToDateTime(dateTimePicker2.Text).AddDays(1) + "') UNION select man.man_id MT_ID,m.name Name,man.datetime DateTime,'Purchases Return' Account, 'PR# ' + CONVERT(varchar(50),pr.pr_id) Description, pr.total Debit,pr.receive Credit,'' Status, '' Balance from manufacturer m, man_transaction man,purchase_return pr where m.m_id=pr.m_id AND man.man_id=pr.man_id AND m.name = '" + cmbname.Text.ToUpper() + "' AND(man.datetime Between '" + Convert.ToDateTime(dateTimePicker1.Text) + "' AND '" + Convert.ToDateTime(dateTimePicker2.Text).AddDays(1) + "') UNION select man.man_id MT_ID,m.name Name,man.datetime DateTime,'Sales' Account, 'Invc# ' + CONVERT(varchar(50),ss.ss_id) Description, (ss.total-ss.discount) Debit,ss.receive Credit,'' Status, '' Balance from manufacturer m, man_transaction man,sale_stock ss where m.m_id=ss.m_id AND man.man_id=ss.man_id AND m.name = '" + cmbname.Text.ToUpper() + "' AND(man.datetime Between '" + Convert.ToDateTime(dateTimePicker1.Text) + "' AND '" + Convert.ToDateTime(dateTimePicker2.Text).AddDays(1) + "') UNION select man.man_id MT_ID,m.name Name,man.datetime DateTime,'Sales Return' Account, 'SR# ' + CONVERT(varchar(50),sr.sr_id) Description, sr.Paid Debit, (sr.total-sr.deduct) Credit,'' Status, '' Balance from manufacturer m, man_transaction man,sale_return sr where m.m_id=sr.m_id AND man.man_id=sr.man_id AND m.name = '" + cmbname.Text.ToUpper() + "' AND(man.datetime Between '" + Convert.ToDateTime(dateTimePicker1.Text) + "' AND '" + Convert.ToDateTime(dateTimePicker2.Text).AddDays(1) + "') UNION (select man.man_id MT_ID, m.name Name, man.datetime DateTime, 'Transaction' Account, mt.description Description, '0' Debit, mt.amount Credit, '' Status, '' Balance from Manufacturer m, man_transaction man, manu_transaction mt where m.m_id = mt.m_id AND man.man_id = mt.man_id AND m.name = '" + cmbname.Text.ToUpper() + "'AND (man.datetime Between '" + Convert.ToDateTime(dateTimePicker1.Text) + "' AND '" + Convert.ToDateTime(dateTimePicker2.Text).AddDays(1) + "') AND mt.status = 'RECEIVED' UNION ALL select man.man_id MT_ID, m.name Name, man.datetime DateTime, 'Transaction' Account, mt.description Description, mt.amount Debit, '0' Credit, '' Status, '' Balance from Manufacturer m, man_transaction man, manu_transaction mt where m.m_id = mt.m_id AND man.man_id = mt.man_id AND m.name = '" + cmbname.Text.ToUpper() + "'AND (man.datetime Between '" + Convert.ToDateTime(dateTimePicker1.Text) + "' AND '" + Convert.ToDateTime(dateTimePicker2.Text).AddDays(1) + "') AND mt.status = 'PAID') order by DateTime", Login.con);
                    SqlDataReader dr5 = cmd5.ExecuteReader();
                    if (dr5.HasRows)
                    {
                        dr5.Close();
                        SqlCommand cmd = new SqlCommand("select ps.paid Debit,ps.total Credit from manufacturer m, man_transaction man,purchase_stock ps where m.m_id=ps.m_id AND man.man_id=ps.man_id AND m.name = '" + cmbname.Text.ToUpper() + "' AND man.datetime <= '" + Convert.ToDateTime(dateTimePicker1.Text) + "'", Login.con);
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
                        SqlCommand cmd7 = new SqlCommand("select (ss.total-ss.discount) Debit ,ss.receive Credit from manufacturer m, man_transaction man,sale_stock ss where m.m_id=ss.m_id AND man.man_id=ss.man_id AND m.name='" + cmbname.Text.ToUpper() + "'AND man.datetime <= '" + Convert.ToDateTime(dateTimePicker1.Text) + "'", Login.con);
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
                        SqlCommand cmd2 = new SqlCommand("select sr.Paid Debit, (sr.total-sr.deduct) Credit from manufacturer m, man_transaction man, sale_return sr where m.m_id = sr.m_id AND man.man_id = sr.man_id AND m.name = '" + cmbname.Text.ToUpper() + "'AND man.datetime  <= '" + Convert.ToDateTime(dateTimePicker1.Text) + "'", Login.con);
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
                        SqlCommand cmd22 = new SqlCommand("select pr.total Debit, pr.receive Credit from manufacturer m, man_transaction man, purchase_return pr where m.m_id = pr.m_id AND man.man_id = pr.man_id AND m.name = '" + cmbname.Text.ToUpper() + "'AND man.datetime  <= '" + Convert.ToDateTime(dateTimePicker1.Text) + "'", Login.con);
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
                        SqlCommand cmd3 = new SqlCommand("select mt.amount Debit from Manufacturer m, man_transaction man, manu_transaction mt where m.m_id = mt.m_id AND man.man_id = mt.man_id AND m.name = '" + cmbname.Text.ToUpper() + "'AND man.datetime <= '" + Convert.ToDateTime(dateTimePicker1.Text) + "' AND mt.status = 'PAID'", Login.con);
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
                        SqlCommand cmd4 = new SqlCommand("select mt.amount Credit from Manufacturer m, man_transaction man, manu_transaction mt where m.m_id = mt.m_id AND man.man_id = mt.man_id AND m.name = '" + cmbname.Text.ToUpper() + "'AND man.datetime <= '" + Convert.ToDateTime(dateTimePicker1.Text) + "' AND mt.status = 'RECEIVED'", Login.con);
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
                        balance = ((sdebit + tdebit + pdebit + srdebit + prdebit) - (scredit + tcredit + pcredit + srcredit + prcredit));
                        if (balance != 0 && balance < 0)
                        {
                            Boolean flag = true;
                            int col = 1;
                            SqlDataAdapter a = new SqlDataAdapter("select '1' MT_ID, '" + cmbname.Text + "' Name, '' DateTime,'Pre. A/c' Account,'' Description, '0' Debit, '0' Credit,'' Status, '' Balance Union select man.man_id MT_ID,m.name Name,man.datetime DateTime,'Purchases' Account, 'DC# ' + CONVERT(varchar(50),ps.ps_id) Description, ps.paid Debit,ps.total Credit,'' Status, '' Balance from manufacturer m, man_transaction man,purchase_stock ps where m.m_id=ps.m_id AND man.man_id=ps.man_id AND m.name = '" + cmbname.Text.ToUpper() + "' AND(man.datetime Between '" + Convert.ToDateTime(dateTimePicker1.Text) + "' AND '" + Convert.ToDateTime(dateTimePicker2.Text).AddDays(1) + "') UNION select man.man_id MT_ID,m.name Name,man.datetime DateTime,'Purchases Return' Account, 'PR# ' + CONVERT(varchar(50),pr.pr_id) Description, pr.total Debit,pr.receive Credit,'' Status, '' Balance from manufacturer m, man_transaction man,purchase_return pr where m.m_id=pr.m_id AND man.man_id=pr.man_id AND m.name = '" + cmbname.Text.ToUpper() + "' AND(man.datetime Between '" + Convert.ToDateTime(dateTimePicker1.Text) + "' AND '" + Convert.ToDateTime(dateTimePicker2.Text).AddDays(1) + "') UNION select man.man_id MT_ID,m.name Name,man.datetime DateTime,'Sales' Account, 'Invc# ' + CONVERT(varchar(50),ss.ss_id) Description, (ss.total-ss.discount) Debit,ss.receive Credit,'' Status, '' Balance from manufacturer m, man_transaction man,sale_stock ss where m.m_id=ss.m_id AND man.man_id=ss.man_id AND m.name = '" + cmbname.Text.ToUpper() + "' AND(man.datetime Between '" + Convert.ToDateTime(dateTimePicker1.Text) + "' AND '" + Convert.ToDateTime(dateTimePicker2.Text).AddDays(1) + "') UNION select man.man_id MT_ID,m.name Name,man.datetime DateTime,'Sales Return' Account, 'SR# ' + CONVERT(varchar(50),sr.sr_id) Description, sr.Paid Debit, (sr.total-sr.deduct) Credit,'' Status, '' Balance from manufacturer m, man_transaction man,sale_return sr where m.m_id=sr.m_id AND man.man_id=sr.man_id AND m.name = '" + cmbname.Text.ToUpper() + "' AND(man.datetime Between '" + Convert.ToDateTime(dateTimePicker1.Text) + "' AND '" + Convert.ToDateTime(dateTimePicker2.Text).AddDays(1) + "') UNION (select man.man_id MT_ID, m.name Name, man.datetime DateTime, 'Transaction' Account, mt.description Description, '0' Debit, mt.amount Credit, '' Status, '' Balance from Manufacturer m, man_transaction man, manu_transaction mt where m.m_id = mt.m_id AND man.man_id = mt.man_id AND m.name = '" + cmbname.Text.ToUpper() + "'AND (man.datetime Between '" + Convert.ToDateTime(dateTimePicker1.Text) + "' AND '" + Convert.ToDateTime(dateTimePicker2.Text).AddDays(1) + "') AND mt.status = 'RECEIVED' UNION ALL select man.man_id MT_ID, m.name Name, man.datetime DateTime, 'Transaction' Account, mt.description Description, mt.amount Debit, '0' Credit, '' Status, '' Balance from Manufacturer m, man_transaction man, manu_transaction mt where m.m_id = mt.m_id AND man.man_id = mt.man_id AND m.name = '" + cmbname.Text.ToUpper() + "'AND (man.datetime Between '" + Convert.ToDateTime(dateTimePicker1.Text) + "' AND '" + Convert.ToDateTime(dateTimePicker2.Text).AddDays(1) + "') AND mt.status = 'PAID') order by DateTime", Login.con);
                            DataTable dataTable1 = new DataTable();
                            a.Fill(dataTable1);
                            foreach (DataRow row in dataTable1.Rows)
                            {
                                if (flag)
                                {
                                    dataTable1.Rows[0]["Balance"] = Convert.ToDecimal(Convert.ToString(balance).Substring(1));
                                    dataTable1.Rows[0]["Credit"] = Convert.ToDecimal(Convert.ToString(balance).Substring(1));
                                    dataTable1.Rows[0]["Status"] = "Credit";
                                    dataTable1.Rows[0]["DateTime"] = Convert.ToDateTime(dataTable1.Rows[1]["DateTime"]).AddDays(-1);
                                    flag = false;            
                                }
                                for (; col < dataTable1.Rows.Count; col++)
                                {
                                    if (Convert.ToDecimal(dataTable1.Rows[col]["Debit"]) != 0 && Convert.ToDecimal(dataTable1.Rows[col]["Credit"]) == 0)
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
                                    else if (Convert.ToDecimal(dataTable1.Rows[col]["Credit"]) != 0 && Convert.ToDecimal(dataTable1.Rows[col]["Debit"]) == 0)
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
                                    else
                                    {
                                        balance += (Convert.ToDecimal(dataTable1.Rows[col]["Debit"]) - Convert.ToDecimal(dataTable1.Rows[col]["Credit"]));
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
                            SqlDataAdapter a = new SqlDataAdapter("select '1' MT_ID, '" + cmbname.Text + "' Name, '' DateTime,'Pre. A/c' Account,'' Description, '0' Debit, '0' Credit,'' Status, '' Balance Union select man.man_id MT_ID,m.name Name,man.datetime DateTime,'Purchases' Account, 'DC# ' + CONVERT(varchar(50),ps.ps_id) Description, ps.paid Debit,ps.total Credit,'' Status, '' Balance from manufacturer m, man_transaction man,purchase_stock ps where m.m_id=ps.m_id AND man.man_id=ps.man_id AND m.name = '" + cmbname.Text.ToUpper() + "' AND(man.datetime Between '" + Convert.ToDateTime(dateTimePicker1.Text) + "' AND '" + Convert.ToDateTime(dateTimePicker2.Text).AddDays(1) + "') UNION select man.man_id MT_ID,m.name Name,man.datetime DateTime,'Sales' Account, 'Invc# ' + CONVERT(varchar(50),ss.ss_id) Description, (ss.total-ss.discount) Debit,ss.receive Credit,'' Status, '' Balance from manufacturer m, man_transaction man,sale_stock ss where m.m_id=ss.m_id AND man.man_id=ss.man_id AND m.name = '" + cmbname.Text.ToUpper() + "' AND(man.datetime Between '" + Convert.ToDateTime(dateTimePicker1.Text) + "' AND '" + Convert.ToDateTime(dateTimePicker2.Text).AddDays(1) + "') UNION select man.man_id MT_ID,m.name Name,man.datetime DateTime,'Purchases Return' Account,'PR# ' + CONVERT(varchar(50),pr.pr_id) Description, pr.total Debit,pr.receive Credit,'' Status, '' Balance from manufacturer m, man_transaction man,purchase_return pr where m.m_id=pr.m_id AND man.man_id=pr.man_id AND m.name = '" + cmbname.Text.ToUpper() + "' AND(man.datetime Between '" + Convert.ToDateTime(dateTimePicker1.Text) + "' AND '" + Convert.ToDateTime(dateTimePicker2.Text).AddDays(1) + "') UNION select man.man_id MT_ID,m.name Name,man.datetime DateTime,'Sales Return' Account,'SR# ' + CONVERT(varchar(50),sr.sr_id) Description, sr.Paid Debit, (sr.total-sr.deduct) Credit,'' Status, '' Balance from manufacturer m, man_transaction man,sale_return sr where m.m_id=sr.m_id AND man.man_id=sr.man_id AND m.name = '" + cmbname.Text.ToUpper() + "' AND(man.datetime Between '" + Convert.ToDateTime(dateTimePicker1.Text) + "' AND '" + Convert.ToDateTime(dateTimePicker2.Text).AddDays(1) + "') UNION (select man.man_id MT_ID, m.name Name, man.datetime DateTime, 'Transaction' Account, mt.description Description, '0' Debit, mt.amount Credit, '' Status, '' Balance from Manufacturer m, man_transaction man, manu_transaction mt where m.m_id = mt.m_id AND man.man_id = mt.man_id AND m.name = '" + cmbname.Text.ToUpper() + "'AND (man.datetime Between '" + Convert.ToDateTime(dateTimePicker1.Text) + "' AND '" + Convert.ToDateTime(dateTimePicker2.Text).AddDays(1) + "') AND mt.status = 'RECEIVED' UNION ALL select man.man_id MT_ID, m.name Name, man.datetime DateTime, 'Transaction' Account, mt.description Description, mt.amount Debit, '0' Credit, '' Status, '' Balance from Manufacturer m, man_transaction man, manu_transaction mt where m.m_id = mt.m_id AND man.man_id = mt.man_id AND m.name = '" + cmbname.Text.ToUpper() + "'AND (man.datetime Between '" + Convert.ToDateTime(dateTimePicker1.Text) + "' AND '" + Convert.ToDateTime(dateTimePicker2.Text).AddDays(1) + "') AND mt.status = 'PAID') order by DateTime", Login.con);
                            DataTable dataTable1 = new DataTable();
                            a.Fill(dataTable1);
                            foreach (DataRow row in dataTable1.Rows)
                            {
                                if (flag)
                                {
                                    dataTable1.Rows[0]["Balance"] = balance;
                                    dataTable1.Rows[0]["Debit"] = balance;
                                    dataTable1.Rows[0]["Status"] = "Debit";
                                    dataTable1.Rows[0]["DateTime"] = Convert.ToDateTime(dataTable1.Rows[1]["DateTime"]).AddDays(-1);
                                    flag = false;
                                }
                                for (; col < dataTable1.Rows.Count; col++)
                                {
                                    if (Convert.ToDecimal(dataTable1.Rows[col]["Debit"]) != 0 && Convert.ToDecimal(dataTable1.Rows[col]["Credit"]) == 0)
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
                                    else if (Convert.ToDecimal(dataTable1.Rows[col]["Credit"]) != 0 && Convert.ToDecimal(dataTable1.Rows[col]["Debit"]) == 0)
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
                                    else
                                    {
                                        balance += (Convert.ToDecimal(dataTable1.Rows[col]["Debit"]) - Convert.ToDecimal(dataTable1.Rows[col]["Credit"]));
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
                            SqlDataAdapter a = new SqlDataAdapter("select man.man_id MT_ID,m.name Name,man.datetime DateTime,'Purchases' Account, 'DC# ' + CONVERT(varchar(50),ps.ps_id) Description, ps.paid Debit,ps.total Credit,'' Status, '' Balance from manufacturer m, man_transaction man,purchase_stock ps where m.m_id=ps.m_id AND man.man_id=ps.man_id AND m.name = '" + cmbname.Text.ToUpper() + "' AND(man.datetime Between '" + Convert.ToDateTime(dateTimePicker1.Text) + "' AND '" + Convert.ToDateTime(dateTimePicker2.Text).AddDays(1) + "') UNION select man.man_id MT_ID,m.name Name,man.datetime DateTime,'Sales' Account, 'Invc# ' + CONVERT(varchar(50),ss.ss_id) Description, (ss.total-ss.discount) Debit,ss.receive Credit,'' Status, '' Balance from manufacturer m, man_transaction man,sale_stock ss where m.m_id=ss.m_id AND man.man_id=ss.man_id AND m.name = '" + cmbname.Text.ToUpper() + "' AND(man.datetime Between '" + Convert.ToDateTime(dateTimePicker1.Text) + "' AND '" + Convert.ToDateTime(dateTimePicker2.Text).AddDays(1) + "') UNION select man.man_id MT_ID,m.name Name,man.datetime DateTime,'Purchases Return' Account, 'PR# ' + CONVERT(varchar(50),pr.pr_id) Description, pr.total Debit,pr.receive Credit,'' Status, '' Balance from manufacturer m, man_transaction man,purchase_return pr where m.m_id=pr.m_id AND man.man_id=pr.man_id AND m.name = '" + cmbname.Text.ToUpper() + "' AND(man.datetime Between '" + Convert.ToDateTime(dateTimePicker1.Text) + "' AND '" + Convert.ToDateTime(dateTimePicker2.Text).AddDays(1) + "') UNION select man.man_id MT_ID,m.name Name,man.datetime DateTime,'Sales Return' Account,'SR# ' + CONVERT(varchar(50),sr.sr_id) Description, sr.Paid Debit, (sr.total-sr.deduct) Credit,'' Status, '' Balance from manufacturer m, man_transaction man,sale_return sr where m.m_id=sr.m_id AND man.man_id=sr.man_id AND m.name = '" + cmbname.Text.ToUpper() + "' AND(man.datetime Between '" + Convert.ToDateTime(dateTimePicker1.Text) + "' AND '" + Convert.ToDateTime(dateTimePicker2.Text).AddDays(1) + "') UNION (select man.man_id MT_ID, m.name Name, man.datetime DateTime, 'Transaction' Account, mt.description Description, '0' Debit, mt.amount Credit, '' Status, '' Balance from Manufacturer m, man_transaction man, manu_transaction mt where m.m_id = mt.m_id AND man.man_id = mt.man_id AND m.name = '" + cmbname.Text.ToUpper() + "'AND (man.datetime Between '" + Convert.ToDateTime(dateTimePicker1.Text) + "' AND '" + Convert.ToDateTime(dateTimePicker2.Text).AddDays(1) + "') AND mt.status = 'RECEIVED' UNION ALL select man.man_id MT_ID, m.name Name, man.datetime DateTime, 'Transaction' Account, mt.description Description, mt.amount Debit, '0' Credit, '' Status, '' Balance from Manufacturer m, man_transaction man, manu_transaction mt where m.m_id = mt.m_id AND man.man_id = mt.man_id AND m.name = '" + cmbname.Text.ToUpper() + "'AND (man.datetime Between '" + Convert.ToDateTime(dateTimePicker1.Text) + "' AND '" + Convert.ToDateTime(dateTimePicker2.Text).AddDays(1) + "') AND mt.status = 'PAID') order by DateTime", Login.con);
                            DataTable dataTable1 = new DataTable();
                            a.Fill(dataTable1);
                            foreach (DataRow row in dataTable1.Rows)
                            {
                                if (Convert.ToDecimal(row["Debit"]) != 0 && Convert.ToDecimal(row["Credit"]) == 0)
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
                                else if (Convert.ToDecimal(row["Credit"]) != 0 && Convert.ToDecimal(row["Debit"]) == 0)
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
                                else
                                {
                                    balance += (Convert.ToDecimal(row["Debit"]) - Convert.ToDecimal(row["Credit"]));
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
            }
            else
            {
                MessageBox.Show("Please Enter A/c Name & Type First", "Verona Tiles & Sanitary", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
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
            e.Graphics.DrawString("Estimated Report", new Font("Arial", 28, FontStyle.Bold), Brushes.Black, 250, 50);
            int startX = 50;
            int startY = 150;
            int Offset = 40;
            e.Graphics.DrawString(""+cmbquality.Text+" Ledger", new Font("Calibri", 12, FontStyle.Underline), new SolidBrush(Color.Black), startX, startY + Offset);
            Offset = Offset + 30;
            e.Graphics.DrawString(""+ cmbname.Text + " A/c", new Font("Calibri", 10, FontStyle.Bold), new SolidBrush(Color.Black), startX, startY + Offset);
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
            e.Graphics.DrawString("Date", new Font("Calibri", 10, FontStyle.Bold), new SolidBrush(Color.Black), startX + 50, startY + Offset);
            e.Graphics.DrawString("Account", new Font("Calibri", 10, FontStyle.Bold), new SolidBrush(Color.Black), startX + 120, startY + Offset);
            e.Graphics.DrawString("Description", new Font("Calibri", 10, FontStyle.Bold), new SolidBrush(Color.Black), startX + 200, startY + Offset);
            e.Graphics.DrawString("Debit", new Font("Calibri", 10, FontStyle.Bold), new SolidBrush(Color.Black), startX + 515, startY + Offset);
            e.Graphics.DrawString("Credit", new Font("Calibri", 10, FontStyle.Bold), new SolidBrush(Color.Black), startX + 585, startY + Offset);
            e.Graphics.DrawString("St", new Font("Calibri", 10, FontStyle.Bold), new SolidBrush(Color.Black), startX + 655, startY + Offset);
            e.Graphics.DrawString("Balance", new Font("Calibri", 10, FontStyle.Bold), new SolidBrush(Color.Black), startX + 695, startY + Offset);
            Offset = Offset + 20;
            e.Graphics.DrawString(underLine, new Font("Courier New", 10, FontStyle.Bold), new SolidBrush(Color.Black), startX, startY + Offset);
            Offset = Offset + 20;
            int a = dataGridView.Rows.Count;
            for (; i < a; i++)
            {
                e.Graphics.DrawString(Convert.ToString(dataGridView.Rows[i].Cells[0].Value), new Font("Calibri", 8), new SolidBrush(Color.Black), startX, startY + Offset);
                e.Graphics.DrawString(Convert.ToString(Convert.ToDateTime(dataGridView.Rows[i].Cells[2].Value).ToString("dd-MM-yyyy")), new Font("Calibri", 8), new SolidBrush(Color.Black), startX + 50, startY + Offset);
                e.Graphics.DrawString(Convert.ToString(dataGridView.Rows[i].Cells[3].Value), new Font("Calibri", 8), new SolidBrush(Color.Black), startX + 120, startY + Offset);
                e.Graphics.DrawString(Convert.ToString(dataGridView.Rows[i].Cells[4].Value), new Font("Calibri", 8), new SolidBrush(Color.Black), startX + 200, startY + Offset);
                if (Convert.ToString(dataGridView.Rows[i].Cells[5].Value) != "0.00")
                {
                    e.Graphics.DrawString(Convert.ToString(dataGridView.Rows[i].Cells[5].Value), new Font("Calibri", 8), new SolidBrush(Color.Black), startX + 515, startY + Offset);
                    debit += Convert.ToDecimal(dataGridView.Rows[i].Cells[5].Value);
                }
                else
                    e.Graphics.DrawString("", new Font("Calibri", 8), new SolidBrush(Color.Black), startX + 515, startY + Offset);
                if (Convert.ToString(dataGridView.Rows[i].Cells[6].Value) != "0.00")
                {
                    e.Graphics.DrawString(Convert.ToString(dataGridView.Rows[i].Cells[6].Value), new Font("Calibri", 8), new SolidBrush(Color.Black), startX + 585, startY + Offset);
                    credit += Convert.ToDecimal(dataGridView.Rows[i].Cells[6].Value);
                }
                else
                    e.Graphics.DrawString("", new Font("Calibri", 8), new SolidBrush(Color.Black), startX + 585, startY + Offset);
                e.Graphics.DrawString(Convert.ToString(dataGridView.Rows[i].Cells[7].Value), new Font("Calibri", 8), new SolidBrush(Color.Black), startX + 655, startY + Offset);
                e.Graphics.DrawString(Convert.ToString(dataGridView.Rows[i].Cells[8].Value), new Font("Calibri", 8), new SolidBrush(Color.Black), startX + 695, startY + Offset);
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
            e.Graphics.DrawString(Convert.ToString(debit), new Font("Calibri", 8), new SolidBrush(Color.Black), startX + 515, startY + Offset);
            e.Graphics.DrawString(Convert.ToString(credit), new Font("Calibri", 8), new SolidBrush(Color.Black), startX + 585, startY + Offset);
            if (Convert.ToString(debit - credit).Contains("-"))
                e.Graphics.DrawString("Cr. " + Convert.ToString(debit - credit).Substring(1), new Font("Calibri", 8), new SolidBrush(Color.Black), startX + 679, startY + Offset);
            else
                e.Graphics.DrawString("Dr. " + Convert.ToString(debit - credit), new Font("Calibri", 8), new SolidBrush(Color.Black), startX + 679, startY + Offset);
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
