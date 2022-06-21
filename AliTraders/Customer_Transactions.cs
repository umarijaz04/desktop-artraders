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

namespace AhmadSanitary
{
    public partial class Customer_Transactions : Form
    {
        Form opener;
        Decimal customer_id, sdebit, scredit, srdebit, prdebit, prcredit, srcredit, tdebit, tcredit, pcredit, pdebit;
        String status, type;
        String delid = "";
        UF.Class1 test = new UF.Class1();

        public Customer_Transactions(Form form)
        {
            InitializeComponent();
            opener = form;
        }

        private void Customer_Transactions_Load(object sender, EventArgs e)
        {
            opener.Enabled = false;
            cmbquality.SelectedIndex = 0;
            clear_data();
        }

        private void Customer_Transactions_FormClosing(object sender, FormClosingEventArgs e)
        {
            opener.Enabled = true;
        }

        private void dateTimePicker_Leave(object sender, EventArgs e)
        {
            if (DateTime.Now < dateTimePicker.Value)
                dateTimePicker.Value = DateTime.Now;
        }

        private void viewToolStripMenuItem_Click(object sender, EventArgs e)
        {
            cmbtype.SelectedIndex = 0;
            panelview.Visible = true;
            dataGridView.DataSource = null;
            cmbtype_SelectedIndexChanged(this, null);
        }

        private void addToolStripMenuItem_Click(object sender, EventArgs e)
        {
            panelview.Visible = false;
        }

        private void btndel_Click(object sender, EventArgs e)
        {
            if (delid != "")
            {
                DialogResult dresult = MessageBox.Show("Are you sure you want to delete this Transaction", "AR Tranders", MessageBoxButtons.OKCancel, MessageBoxIcon.Question);
                if (dresult == DialogResult.OK)
                {
                    try
                    {
                        Login.con.Open();
                        SqlCommand cmd = new SqlCommand("DELETE from customer_transaction where ct_id='" + delid + "'", Login.con);
                        cmd.ExecuteNonQuery();
                        cmbtype_SelectedIndexChanged(this, null);
                        delid = "";
                        MessageBox.Show("Transaction Deleted Successfully", "AR Tranders", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    catch (Exception)
                    {
                        delid = "";
                    }
                    finally
                    {
                        Login.con.Close();
                    }
                }
            }
        }

        String name, des, st, amt;
        private void dataGridView_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = this.dataGridView.Rows[e.RowIndex];
                delid = row.Cells[0].Value.ToString();
                name = row.Cells[1].Value.ToString();
                des = row.Cells[3].Value.ToString();
                st = row.Cells[5].Value.ToString();
                amt = row.Cells[4].Value.ToString();
            }
        }

        private void cmbtype_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbtype.SelectedIndex == 0)
            {
                string command = "SELECT ct.ct_id CT_ID, c.c_name Name, ct.datetime DateTime, ct.description Description, ct.amount Amount, case when ct.status = 'Paid' then 'Debit' else 'Credit' end Status, case when ct.type = 'True' then 'Enter' else 'Not Enter' end CashBook from customer_transaction ct, customer c where ct.c_id=c.c_id AND c.type='FIXED'";
                test.bindingcode(command, dataGridView, Login.con);
            }
            else if (cmbtype.SelectedIndex == 1)
            {
                string command = "SELECT mt.mt_id MT_ID, m.name Name, mt.datetime DateTime, mt.description Description, mt.amount Amount, case when mt.status = 'Paid' then 'Debit' else 'Credit' end Status, case when mt.type = 'True' then 'Enter' else 'Not Enter' end CashBook from manu_transaction mt, manufacturer m where mt.m_id=m.m_id";
                test.bindingcode(command, dataGridView, Login.con);
            }
            else if (cmbtype.SelectedIndex == 2)
            {
                string command = "SELECT ct.ct_id CT_ID, c.c_name Name, ct.datetime DateTime, ct.description Description, ct.amount Amount, case when ct.status = 'Paid' then 'Debit' else 'Credit' end Status, case when ct.type = 'True' then 'Enter' else 'Not Enter' end CashBook from customer_transaction ct, customer c where ct.c_id=c.c_id AND c.type='EXPENSE'";
                test.bindingcode(command, dataGridView, Login.con);
            }
            else if (cmbtype.SelectedIndex == 3)
            {
                string command = "SELECT ct.ct_id CT_ID, c.c_name Name, ct.datetime DateTime, ct.description Description, ct.amount Amount, case when ct.status = 'Paid' then 'Debit' else 'Credit' end Status, case when ct.type = 'True' then 'Enter' else 'Not Enter' end CashBook from customer_transaction ct, customer c where ct.c_id=c.c_id AND c.type='STAKEHOLDER'";
                test.bindingcode(command, dataGridView, Login.con);
            }
            else if (cmbtype.SelectedIndex == 4)
            {
                string command = "SELECT ct.ct_id CT_ID, c.c_name Name, ct.datetime DateTime, ct.description Description, ct.amount Amount, case when ct.status = 'Paid' then 'Debit' else 'Credit' end Status, case when ct.type = 'True' then 'Enter' else 'Not Enter' end CashBook from customer_transaction ct, customer c where ct.c_id=c.c_id AND c.type='CAPITAL'";
                test.bindingcode(command, dataGridView, Login.con);
            }
            else if (cmbtype.SelectedIndex == 5)
            {
                string command = "SELECT ct.ct_id CT_ID, c.c_name Name, ct.datetime DateTime, ct.description Description, ct.amount Amount, case when ct.status = 'Paid' then 'Debit' else 'Credit' end Status, case when ct.type = 'True' then 'Enter' else 'Not Enter' end CashBook from customer_transaction ct, customer c where ct.c_id=c.c_id AND c.type='OPENING'";
                test.bindingcode(command, dataGridView, Login.con);
            }
            else if (cmbtype.SelectedIndex == 6)
            {
                string command = "SELECT ct.ct_id CT_ID, c.c_name Name, ct.datetime DateTime, ct.description Description, ct.amount Amount, case when ct.status = 'Paid' then 'Debit' else 'Credit' end Status, case when ct.type = 'True' then 'Enter' else 'Not Enter' end CashBook from customer_transaction ct, customer c where ct.c_id=c.c_id AND c.type='CLOSING'";
                test.bindingcode(command, dataGridView, Login.con);
            }
        }

        private void btnprint_Click(object sender, EventArgs e)
        {
            DialogResult dresult = MessageBox.Show("Press 'OK' to Print Transaction Invoice", "AR Tranders", MessageBoxButtons.OKCancel, MessageBoxIcon.Question);
            if (dresult == DialogResult.OK)
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

            e.Graphics.DrawString("" + cmbtype.Text.ToUpper() + "", new Font("Calibri", 10, FontStyle.Bold), new SolidBrush(Color.Black), startX, startY + Offset);
            Offset = Offset + 20;
            e.Graphics.DrawString("" + name.ToString() + " A/c", new Font("Calibri", 10, FontStyle.Bold), new SolidBrush(Color.Black), startX, startY + Offset);
            Offset = Offset + 20;
            string underLine = "=======================================================================================";
            e.Graphics.DrawString(underLine, new Font("Courier New", 10, FontStyle.Bold), new SolidBrush(Color.Black), startX, startY + Offset);
            Offset = Offset + 20;
            e.Graphics.DrawString("Sr#", new Font("Calibri", 10, FontStyle.Bold), new SolidBrush(Color.Black), startX, startY + Offset);
            e.Graphics.DrawString("Description", new Font("Calibri", 10, FontStyle.Bold), new SolidBrush(Color.Black), startX + 45, startY + Offset);
            //e.Graphics.DrawString("Prevoius Balance", new Font("Calibri", 10, FontStyle.Bold), new SolidBrush(Color.Black), startX + 280, startY + Offset);
            e.Graphics.DrawString("Status", new Font("Calibri", 10, FontStyle.Bold), new SolidBrush(Color.Black), startX + 420, startY + Offset);
            e.Graphics.DrawString("Amount", new Font("Calibri", 10, FontStyle.Bold), new SolidBrush(Color.Black), startX + 550, startY + Offset);
            Offset = Offset + 20;
            e.Graphics.DrawString(underLine, new Font("Courier New", 10, FontStyle.Bold), new SolidBrush(Color.Black), startX, startY + Offset);
            Offset = Offset + 20;
            e.Graphics.DrawString(Convert.ToString("1"), new Font("Calibri", 9), new SolidBrush(Color.Black), startX, startY + Offset);
            e.Graphics.DrawString(Convert.ToString(des), new Font("Calibri", 9), new SolidBrush(Color.Black), startX + 45, startY + Offset);
            /*
            if (Convert.ToDecimal(2) < 0)
                e.Graphics.DrawString(Convert.ToString("Cr. " + ("prebalance").Substring(1)), new Font("Calibri", 9), new SolidBrush(Color.Black), startX + 280, startY + Offset);
            else
                e.Graphics.DrawString(Convert.ToString("Dr. " + "prebalance"), new Font("Calibri", 9), new SolidBrush(Color.Black), startX + 280, startY + Offset);
            */
            e.Graphics.DrawString(Convert.ToString(st), new Font("Calibri", 9), new SolidBrush(Color.Black), startX + 420, startY + Offset);
            e.Graphics.DrawString(Convert.ToString(amt), new Font("Calibri", 9), new SolidBrush(Color.Black), startX + 550, startY + Offset);
            /*Offset = Offset + 20;
            e.Graphics.DrawString(underLine, new Font("Courier New", 10, FontStyle.Bold), new SolidBrush(Color.Black), startX, startY + Offset);
            Offset = Offset + 20;
            e.Graphics.DrawString("Balance:", new Font("Calibri", 10, FontStyle.Bold), new SolidBrush(Color.Black), startX + 495, startY + Offset);            
            if (Convert.ToDecimal(2) < 0)
                e.Graphics.DrawString("Cr." + ("balance").Substring(1), new Font("Calibri", 10), new SolidBrush(Color.Black), startX + 550, startY + Offset);
            else
                e.Graphics.DrawString("Dr." + "balance", new Font("Calibri", 10), new SolidBrush(Color.Black), startX + 550, startY + Offset);
            */
            Offset = Offset + 20;
            e.Graphics.DrawString(underLine, new Font("Courier New", 10, FontStyle.Bold), new SolidBrush(Color.Black), startX, startY + Offset);
            Offset = Offset + 20;
            e.Graphics.DrawString("Signature:  __________________", new Font("Calibri", 10, FontStyle.Bold), new SolidBrush(Color.Black), startX, startY + Offset);
            e.Graphics.DrawString("Date & Time:  ", new Font("Calibri", 10, FontStyle.Bold), new SolidBrush(Color.Black), startX + 320, startY + Offset);
            e.Graphics.DrawString(DateTime.Now.ToString(), new Font("Calibri", 10), new SolidBrush(Color.Black), startX + 400, startY + Offset);
            Offset = Offset + 25;
            e.Graphics.DrawString("Developed By: Geeks IT Solutions 03126424443  ", new Font("Calibri", 8, FontStyle.Bold), new SolidBrush(Color.Black), startX + 280, startY + Offset + 5);
            e.Graphics.PageUnit = GraphicsUnit.Inch;
        }

        void clear_data()
        {
            cmbstatus.SelectedIndex = 0;
            cmbcus.Focus();
            cmbcus.Text = txtprebal.Text = txtamt.Text = txtbal.Text = "";
            txtdes.Clear();
            dateTimePicker.Value = DateTime.Now;
        }

        private void cmbcus_Enter(object sender, EventArgs e)
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
                test.cmbox_drop(command, cmbcus, Login.con);
            }
            else if (cmbquality.Text == "Manufacturer")
            {
                string command = "select name from manufacturer order by name";
                test.cmbox_drop(command, cmbcus, Login.con);
            }
        }

        private void cmbquality_SelectedIndexChanged(object sender, EventArgs e)
        {
            clear_data();
        }

        private void cmbcus_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                if (cmbcus.Text != "")
                {
                    sdebit = scredit = srdebit = prdebit = prcredit = srcredit = tdebit = pcredit = pdebit = tcredit = 0;
                    Login.con.Open();
                    if (cmbquality.Text == "Customer" || cmbquality.Text == "Capital" || cmbquality.Text == "Direct Expenses" || cmbquality.Text == "Stakeholder" || cmbquality.Text == "Opening Balance" || cmbquality.Text == "Closing Balance")
                    {
                        SqlCommand cmd = new SqlCommand("select (ss.total-ss.discount) Debit ,ss.receive Credit from customer c, cus_transaction cus,sale_stock ss where c.c_id=ss.c_id AND c.type='" + type + "' AND cus.cus_id=ss.cus_id AND c.c_name='" + cmbcus.Text.ToUpper() + "'AND cus.datetime <= '" + DateTime.Now + "'", Login.con);
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
                        SqlCommand cmd7 = new SqlCommand("select ps.paid Debit,ps.total Credit from customer c, cus_transaction cus,purchase_stock ps where c.c_id=ps.c_id AND cus.cus_id=ps.cus_id AND c.c_name = '" + cmbcus.Text.ToUpper() + "' AND cus.datetime <= '" + DateTime.Now + "'", Login.con);
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
                        SqlCommand cmd2 = new SqlCommand("select sr.Paid Debit, (sr.total-sr.deduct) Credit from customer c, cus_transaction cus, sale_return sr where c.c_id = sr.c_id AND c.type = '" + type + "' AND cus.cus_id = sr.cus_id AND c.c_name = '" + cmbcus.Text.ToUpper() + "'AND cus.datetime  <= '" + DateTime.Now + "'", Login.con);
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
                        SqlCommand cmd22 = new SqlCommand("select pr.total Debit, pr.receive Credit from customer c, cus_transaction cus, purchase_return pr where c.c_id = pr.c_id AND c.type = '" + type + "' AND cus.cus_id = pr.cus_id AND c.c_name = '" + cmbcus.Text.ToUpper() + "'AND cus.datetime  <= '" + DateTime.Now + "'", Login.con);
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
                        SqlCommand cmd3 = new SqlCommand("select ct.amount Debit from customer c, cus_transaction cus, customer_transaction ct where c.c_id = ct.c_id AND c.type = '" + type + "' AND cus.cus_id = ct.cus_id AND c.c_name = '" + cmbcus.Text.ToUpper() + "'AND cus.datetime <= '" + DateTime.Now + "' AND ct.status = 'PAID'", Login.con);
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
                        SqlCommand cmd4 = new SqlCommand("select ct.amount Credit from customer c, cus_transaction cus, customer_transaction ct where c.c_id = ct.c_id AND c.type = '" + type + "' AND cus.cus_id = ct.cus_id AND c.c_name = '" + cmbcus.Text.ToUpper() + "'AND cus.datetime <= '" + DateTime.Now + "' AND ct.status = 'RECEIVED'", Login.con);
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
                    else if (cmbquality.Text == "Manufacturer")
                    {
                        SqlCommand cmd = new SqlCommand("select ps.paid Debit,ps.total Credit from manufacturer m, man_transaction man,purchase_stock ps where m.m_id=ps.m_id AND man.man_id=ps.man_id AND m.name = '" + cmbcus.Text.ToUpper() + "' AND man.datetime <= '" + DateTime.Now + "'", Login.con);
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
                        SqlCommand cmd7 = new SqlCommand("select (ss.total-ss.discount) Debit ,ss.receive Credit from manufacturer m, man_transaction man,sale_stock ss where m.m_id=ss.m_id AND man.man_id=ss.man_id AND m.name='" + cmbcus.Text.ToUpper() + "'AND man.datetime <= '" + DateTime.Now + "'", Login.con);
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
                        SqlCommand cmd2 = new SqlCommand("select sr.Paid Debit, (sr.total-sr.deduct) Credit from manufacturer m, man_transaction man, sale_return sr where m.m_id = sr.m_id AND man.man_id = sr.man_id AND m.name = '" + cmbcus.Text.ToUpper() + "'AND man.datetime  <= '" + DateTime.Now + "'", Login.con);
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
                        SqlCommand cmd22 = new SqlCommand("select pr.total Debit, pr.receive Credit from manufacturer m, man_transaction man, purchase_return pr where m.m_id = pr.m_id AND man.man_id = pr.man_id AND m.name = '" + cmbcus.Text.ToUpper() + "'AND man.datetime  <= '" + DateTime.Now + "'", Login.con);
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
                        SqlCommand cmd3 = new SqlCommand("select mt.amount Debit from Manufacturer m, man_transaction man, manu_transaction mt where m.m_id = mt.m_id AND man.man_id = mt.man_id AND m.name = '" + cmbcus.Text.ToUpper() + "'AND man.datetime <= '" + DateTime.Now + "' AND mt.status = 'PAID'", Login.con);
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
                        SqlCommand cmd4 = new SqlCommand("select mt.amount Credit from Manufacturer m, man_transaction man, manu_transaction mt where m.m_id = mt.m_id AND man.man_id = mt.man_id AND m.name = '" + cmbcus.Text.ToUpper() + "'AND man.datetime <= '" + DateTime.Now + "' AND mt.status = 'RECEIVED'", Login.con);
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
                    txtprebal.Text = Convert.ToString(((sdebit + prdebit + tdebit + pdebit + srdebit) - (prcredit + scredit + tcredit + pcredit + srcredit)));
                }
            }
        }

        private void cmbcus_Leave(object sender, EventArgs e)
        {
            if (cmbcus.Text == "")
            {
                txtprebal.Text = "";
            }
            else
            {
                cmbcus.Text = cmbcus.Text.ToUpper();
                cmbcus.SelectAll();
                Login.con.Open();
                if (cmbquality.Text == "Customer" || cmbquality.Text == "Capital" || cmbquality.Text == "Direct Expenses" || cmbquality.Text == "Stakeholder" || cmbquality.Text == "Opening Balance" || cmbquality.Text == "Closing Balance")
                {
                    SqlCommand cmd8 = new SqlCommand("SELECT c_id from customer where c_name='" + cmbcus.Text.ToUpper() + "' AND type='" + type + "'", Login.con);
                    SqlDataReader dr8 = cmd8.ExecuteReader();
                    if (dr8.HasRows)
                    {
                        while (dr8.Read())
                        {
                            customer_id = Convert.ToDecimal(dr8[0]);
                        }
                        dr8.Close();
                    }
                    else
                    {
                        dr8.Close();
                        MessageBox.Show("Record Not Found", "AR Tranders", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                        cmbcus.Focus();
                        txtprebal.Text = "";
                    }
                }
                else if (cmbquality.Text == "Manufacturer")
                {
                    SqlCommand cmd8 = new SqlCommand("SELECT m_id from manufacturer where name='" + cmbcus.Text.ToUpper() + "'", Login.con);
                    SqlDataReader dr8 = cmd8.ExecuteReader();
                    if (dr8.HasRows)
                    {
                        while (dr8.Read())
                        {
                            customer_id = Convert.ToDecimal(dr8[0]);
                        }
                        dr8.Close();
                    }
                    else
                    {
                        dr8.Close();
                        MessageBox.Show("Record Not Found", "AR Tranders", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                        cmbcus.Focus();
                        txtprebal.Text = "";
                    }
                }
                Login.con.Close();
            }
        }

        private void txtamt_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                cmbstatus_SelectedIndexChanged(this, null);
            }
        }

        private void txtamt_KeyPress(object sender, KeyPressEventArgs e)
        {
            test.decimaldigit_correction(sender, e);
        }

        private void cmbstatus_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (txtprebal.Text != "" && txtamt.Text != "")
            {
                if (cmbstatus.SelectedIndex == 1)
                {
                    txtbal.Text = Convert.ToString(Convert.ToDecimal(txtprebal.Text) + Convert.ToDecimal(txtamt.Text));
                    status = "PAID";
                }
                else if (cmbstatus.SelectedIndex == 2)
                {
                    txtbal.Text = Convert.ToString(Convert.ToDecimal(txtprebal.Text) - Convert.ToDecimal(txtamt.Text));
                    status = "RECEIVED";
                }
            }
        }

        private void txtdes_KeyPress(object sender, KeyPressEventArgs e)
        {
            test.name_correction(txtdes, e);
        }


        private void btnadd_Click(object sender, EventArgs e)
        {
            if (cmbcus.Text != "" && txtprebal.Text != "" && cmbstatus.Text != "---Select Option---" && txtamt.Text != "" && txtbal.Text != "")
            {
                Login.con.Open();
                SqlCommand cmd5 = new SqlCommand("insert into cash_transaction (datetime) values('" + dateTimePicker.Value + "')", Login.con);
                cmd5.ExecuteNonQuery();
                SqlCommand cmd6 = new SqlCommand("select cat_id from cash_transaction where datetime='" + dateTimePicker.Value + "'", Login.con);
                Decimal cat_id = (Decimal)cmd6.ExecuteScalar();
                if (cmbquality.Text == "Customer" || cmbquality.Text == "Capital" || cmbquality.Text == "Direct Expenses" || cmbquality.Text == "Stakeholder" || cmbquality.Text == "Opening Balance" || cmbquality.Text == "Closing Balance")
                {
                    SqlCommand cmd7 = new SqlCommand("INSERT into cus_transaction (datetime) values('" + dateTimePicker.Value + "')", Login.con);
                    cmd7.ExecuteNonQuery();
                    SqlCommand cmd8 = new SqlCommand("SELECT cus_id from cus_transaction where datetime='" + dateTimePicker.Value + "'", Login.con);
                    Decimal cus_id = (Decimal)cmd8.ExecuteScalar();
                    if (txtdes.Text != "")
                    {
                        if (ckbx.Checked == true)
                        {
                            SqlCommand cmd9 = new SqlCommand("INSERT into customer_transaction (c_id, amount, datetime, status,cat_id,cus_id,description,type) values('" + customer_id + "','" + Convert.ToDecimal(txtamt.Text) + "','" + dateTimePicker.Value + "','" + status + "','" + cat_id + "','" + cus_id + "', '" + txtdes.Text + "', 'True')", Login.con);
                            cmd9.ExecuteNonQuery();
                        }
                        else
                        {
                            SqlCommand cmd9 = new SqlCommand("INSERT into customer_transaction (c_id, amount, datetime, status,cat_id,cus_id,description) values('" + customer_id + "','" + Convert.ToDecimal(txtamt.Text) + "','" + dateTimePicker.Value + "','" + status + "','" + cat_id + "','" + cus_id + "', '" + txtdes.Text + "')", Login.con);
                            cmd9.ExecuteNonQuery();
                        }
                    }
                    else
                    {
                        if (ckbx.Checked == true)
                        {
                            SqlCommand cmd9 = new SqlCommand("INSERT into customer_transaction (c_id, amount, datetime, status,cat_id,cus_id,description,type) values('" + customer_id + "','" + Convert.ToDecimal(txtamt.Text) + "','" + dateTimePicker.Value + "','" + status + "','" + cat_id + "','" + cus_id + "', 'NULL', 'True')", Login.con);
                            cmd9.ExecuteNonQuery();
                        }
                        else
                        {
                            SqlCommand cmd9 = new SqlCommand("INSERT into customer_transaction (c_id, amount, datetime, status,cat_id,cus_id,description) values('" + customer_id + "','" + Convert.ToDecimal(txtamt.Text) + "','" + dateTimePicker.Value + "','" + status + "','" + cat_id + "','" + cus_id + "', 'NULL')", Login.con);
                            cmd9.ExecuteNonQuery();
                        }
                    }
                }
                else if (cmbquality.Text == "Manufacturer")
                {
                    SqlCommand cmd7 = new SqlCommand("INSERT into man_transaction (datetime) values('" + dateTimePicker.Value + "')", Login.con);
                    cmd7.ExecuteNonQuery();
                    SqlCommand cmd8 = new SqlCommand("SELECT man_id from man_transaction where datetime='" + dateTimePicker.Value + "'", Login.con);
                    Decimal man_id = (Decimal)cmd8.ExecuteScalar();
                    if (txtdes.Text != "")
                    {
                        if (ckbx.Checked == true)
                        {
                            SqlCommand cmd9 = new SqlCommand("INSERT into manu_transaction (m_id, amount, datetime, status,cat_id,man_id,description,type) values('" + customer_id + "','" + Convert.ToDecimal(txtamt.Text) + "','" + dateTimePicker.Value + "','" + status + "','" + cat_id + "','" + man_id + "', '" + txtdes.Text + "','True')", Login.con);
                            cmd9.ExecuteNonQuery();
                        }
                        else
                        {
                            SqlCommand cmd9 = new SqlCommand("INSERT into manu_transaction (m_id, amount, datetime, status,cat_id,man_id,description) values('" + customer_id + "','" + Convert.ToDecimal(txtamt.Text) + "','" + dateTimePicker.Value + "','" + status + "','" + cat_id + "','" + man_id + "', '" + txtdes.Text + "')", Login.con);
                            cmd9.ExecuteNonQuery();
                        }
                    }
                    else
                    {
                        if (ckbx.Checked == true)
                        {
                            SqlCommand cmd9 = new SqlCommand("INSERT into manu_transaction (m_id, amount, datetime, status,cat_id,man_id,description,type) values('" + customer_id + "','" + Convert.ToDecimal(txtamt.Text) + "','" + dateTimePicker.Value + "','" + status + "','" + cat_id + "','" + man_id + "', 'NULL','True')", Login.con);
                            cmd9.ExecuteNonQuery();
                        }
                        else
                        {
                            SqlCommand cmd9 = new SqlCommand("INSERT into manu_transaction (m_id, amount, datetime, status,cat_id,man_id,description) values('" + customer_id + "','" + Convert.ToDecimal(txtamt.Text) + "','" + dateTimePicker.Value + "','" + status + "','" + cat_id + "','" + man_id + "', 'NULL')", Login.con);
                            cmd9.ExecuteNonQuery();
                        }
                    }
                }
                Login.con.Close();
                DialogResult dresult = MessageBox.Show("Press 'OK' to Print Transaction Invoice", "AR Tranders", MessageBoxButtons.OKCancel, MessageBoxIcon.Question);
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
                MessageBox.Show("Transaction Insert Successfully", "AR Tranders", MessageBoxButtons.OK, MessageBoxIcon.Information);
                clear_data();
            }
        }

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
            e.Graphics.DrawString("" + cmbquality.Text.ToUpper() + "", new Font("Calibri", 10, FontStyle.Bold), new SolidBrush(Color.Black), startX, startY + Offset);
            Offset = Offset + 20;
            e.Graphics.DrawString("" + cmbcus.Text + " A/c", new Font("Calibri", 10, FontStyle.Bold), new SolidBrush(Color.Black), startX, startY + Offset);
            Offset = Offset + 20;
            string underLine = "=======================================================================================";
            e.Graphics.DrawString(underLine, new Font("Courier New", 10, FontStyle.Bold), new SolidBrush(Color.Black), startX, startY + Offset);
            Offset = Offset + 20;
            e.Graphics.DrawString("Sr#", new Font("Calibri", 10, FontStyle.Bold), new SolidBrush(Color.Black), startX, startY + Offset);
            e.Graphics.DrawString("Description", new Font("Calibri", 10, FontStyle.Bold), new SolidBrush(Color.Black), startX + 45, startY + Offset);
            e.Graphics.DrawString("Prevoius Balance", new Font("Calibri", 10, FontStyle.Bold), new SolidBrush(Color.Black), startX + 280, startY + Offset);
            e.Graphics.DrawString("Status", new Font("Calibri", 10, FontStyle.Bold), new SolidBrush(Color.Black), startX + 420, startY + Offset);
            e.Graphics.DrawString("Amount", new Font("Calibri", 10, FontStyle.Bold), new SolidBrush(Color.Black), startX + 550, startY + Offset);
            Offset = Offset + 20;
            e.Graphics.DrawString(underLine, new Font("Courier New", 10, FontStyle.Bold), new SolidBrush(Color.Black), startX, startY + Offset);
            Offset = Offset + 20;
            e.Graphics.DrawString(Convert.ToString("1"), new Font("Calibri", 9), new SolidBrush(Color.Black), startX, startY + Offset);
            e.Graphics.DrawString(Convert.ToString(txtdes.Text), new Font("Calibri", 9), new SolidBrush(Color.Black), startX + 45, startY + Offset);
            if (Convert.ToDecimal(txtprebal.Text) < 0)
                e.Graphics.DrawString(Convert.ToString("Cr. " + (txtprebal.Text).Substring(1)), new Font("Calibri", 9), new SolidBrush(Color.Black), startX + 280, startY + Offset);
            else
                e.Graphics.DrawString(Convert.ToString("Dr. " + txtprebal.Text), new Font("Calibri", 9), new SolidBrush(Color.Black), startX + 280, startY + Offset);
            e.Graphics.DrawString(Convert.ToString(cmbstatus.Text), new Font("Calibri", 9), new SolidBrush(Color.Black), startX + 420, startY + Offset);
            e.Graphics.DrawString(Convert.ToString(txtamt.Text), new Font("Calibri", 9), new SolidBrush(Color.Black), startX + 550, startY + Offset);
            Offset = Offset + 20;
            e.Graphics.DrawString(underLine, new Font("Courier New", 10, FontStyle.Bold), new SolidBrush(Color.Black), startX, startY + Offset);
            Offset = Offset + 20;
            e.Graphics.DrawString("Balance:", new Font("Calibri", 10, FontStyle.Bold), new SolidBrush(Color.Black), startX + 495, startY + Offset);
            if (Convert.ToDecimal(txtbal.Text) < 0)
                e.Graphics.DrawString("Cr." + (txtbal.Text).Substring(1), new Font("Calibri", 10), new SolidBrush(Color.Black), startX + 550, startY + Offset);
            else
                e.Graphics.DrawString("Dr." + txtbal.Text, new Font("Calibri", 10), new SolidBrush(Color.Black), startX + 550, startY + Offset);
            Offset = Offset + 20;
            e.Graphics.DrawString(underLine, new Font("Courier New", 10, FontStyle.Bold), new SolidBrush(Color.Black), startX, startY + Offset);
            Offset = Offset + 20;
            e.Graphics.DrawString("Signature:  __________________", new Font("Calibri", 10, FontStyle.Bold), new SolidBrush(Color.Black), startX, startY + Offset);
            e.Graphics.DrawString("Date & Time:  ", new Font("Calibri", 10, FontStyle.Bold), new SolidBrush(Color.Black), startX + 320, startY + Offset);
            e.Graphics.DrawString(DateTime.Now.ToString(), new Font("Calibri", 10), new SolidBrush(Color.Black), startX + 400, startY + Offset);
            Offset = Offset + 25;
            e.Graphics.DrawString("Developed By: Geeks IT Solutions 03126424443  ", new Font("Calibri", 8, FontStyle.Bold), new SolidBrush(Color.Black), startX + 280, startY + Offset + 5);
            e.Graphics.PageUnit = GraphicsUnit.Inch;
        }
    }
}
