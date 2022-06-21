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
    public partial class Main : Form
    {
        Login opener;
        String name;

        public Main(Login form, String fname, String uname)
        {
            InitializeComponent();
            opener = form;
            lbfname.Text = fname;
            name = uname;
        }

        private void Main_Load(object sender, EventArgs e)
        {
            opener.Hide();
            if (lbfname.Text == "Imran")
            {
                menuStrip1.Visible = pictureBox4.Visible = btnexp.Visible = false;
            }
        }

        private void Main_FormClosing(object sender, FormClosingEventArgs e)
        {
            opener.Close();
        }

        private void btncart_Click(object sender, EventArgs e)
        {
            Cart f = new Cart(this);
            f.Show();
        }

        private void btnsalesreturn_Click(object sender, EventArgs e)
        {
            Sales_Return f = new Sales_Return(this);
            f.Show();
        }

        private void btnstock_Click(object sender, EventArgs e)
        {
            Stock f = new Stock(this);
            f.Show();
        }

        private void aboutUsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AboutUs f = new AboutUs(this);
            f.Show();
        }

        private void settingToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Settings f = new Settings(this, name);
            f.Show();
        }

        private void quantityLimitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Quantity_Limit f = new Quantity_Limit(this);
            f.Show();
        }

        private void toolStripMenuItem7_Click(object sender, EventArgs e)
        {
            //System.Diagnostics.Process.Start("http://cp.veronatiles.com");
            openFileDialog1.Filter = "All Files|*.bak";
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                string s = null;
                s = openFileDialog1.FileName;
                Login.con.Open();
                SqlCommand cmd = new SqlCommand("USE [master]", Login.con);
                cmd.ExecuteNonQuery();
                SqlCommand cmd2 = new SqlCommand("alter database [Inventory] set single_user with rollback immediate", Login.con);
                cmd2.ExecuteNonQuery();
                SqlCommand cmd3 = new SqlCommand("DROP DATABASE [Inventory]", Login.con);
                cmd3.ExecuteNonQuery();
                SqlCommand cmd4 = new SqlCommand("RESTORE DATABASE [Inventory] FROM DISK = '" + s + "'", Login.con);
                cmd4.ExecuteNonQuery();
                Login.con.Close();
                MessageBox.Show("Your DataBase Restore Successfully", "AR Tranders", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void toolStripMenuItem8_Click(object sender, EventArgs e)
        {
            //System.Diagnostics.Process.Start("http://cp.veronatiles.com");
            saveFileDialog1.FileName = DateTime.Now.ToString("dd-MM-yyyy");
            saveFileDialog1.Filter = "All Files|*.bak";
            if (saveFileDialog1.ShowDialog() == DialogResult.OK)
            {
                string s = null;
                s = saveFileDialog1.FileName;
                Login.con.Open();
                SqlCommand cmd = new SqlCommand("Backup database Inventory to disk='" + s + "'", Login.con);
                cmd.ExecuteNonQuery();
                Login.con.Close();
                MessageBox.Show("Your DataBase Backup Create in '" + s + "'", "AR Tranders", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void btnorder_Click(object sender, EventArgs e)
        {
            Order_List f = new Order_List(this);
            f.Show();
        }

        private void btnexp_Click(object sender, EventArgs e)
        {
            Office_Expenses f = new Office_Expenses(this);
            f.Show();
        }

        private void customerInvoiceToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Sales_Invoices f = new Sales_Invoices(this);
            f.Show();
        }

        private void customerAndInvoiceToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Sales_Return_Report f = new Sales_Return_Report(this);
            f.Show();
        }

        private void expensesDetailToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Expenses_Report f = new Expenses_Report(this);
            f.Show();
        }

        private void salesSummaryToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Sales_Summary f = new Sales_Summary(this);
            f.Show();
        }

        private void purchasesSummaryToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Purchases_Summary f = new Purchases_Summary(this);
            f.Show();
        }

        private void salesReturnSummaryToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Sales_Return_Summary f = new Sales_Return_Summary(this);
            f.Show();
        }

        private void productWiseSalesToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            Product_Wise_Sales f = new Product_Wise_Sales(this);
            f.Show();
        }

        private void manufacturerWisePurchasesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Product_Wise_Purchases f = new Product_Wise_Purchases(this);
            f.Show();
        }

        private void productWiseSalesReturnToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Product_Wise_Sales_Return f = new Product_Wise_Sales_Return(this);
            f.Show();
        }

        private void productWiseToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Product_Wise_Breakage f = new Product_Wise_Breakage(this);
            f.Show();
        }

        private void dCNoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DC_Wise_Breakage f = new DC_Wise_Breakage(this);
            f.Show();
        }

        private void toolStripMenuItem4_Click(object sender, EventArgs e)
        {
            Customer f = new Customer(this);
            f.Show();
        }

        private void toolStripMenuItem5_Click(object sender, EventArgs e)
        {
            Manufacturer f = new Manufacturer(this);
            f.Show();
        }

        private void manufacturerTransactionToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Customer_Transactions f = new Customer_Transactions(this);
            f.Show();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Purchases_Return f = new Purchases_Return(this);
            f.Show();
        }

        private void toolStripMenuItem3_Click(object sender, EventArgs e)
        {
            Purchases_Return_Summary f = new Purchases_Return_Summary(this);
            f.Show();
        }

        private void toolStripMenuItem2_Click(object sender, EventArgs e)
        {
            Product_Wise_Purchases_Return f = new Product_Wise_Purchases_Return(this);
            f.Show();
        }

        private void toolStripMenuItem1_Click(object sender, EventArgs e)
        {
            Purchases_Return_Report f = new Purchases_Return_Report(this);
            f.Show();
        }

        private void payableToolStripMenuItem_Click_2(object sender, EventArgs e)
        {
            Payable f = new Payable(this);
            f.Show();
        }

        private void reciveableToolStripMenuItem_Click_2(object sender, EventArgs e)
        {
            Receivable f = new Receivable(this);
            f.Show();
        }

        private void toolStripMenuItem9_Click_2(object sender, EventArgs e)
        {
            Available_Stock f = new Available_Stock(this);
            f.Show();
        }

        private void avilableDetailToolStripMenuItem_Click_1(object sender, EventArgs e)
        {
            Available_Detail f = new Available_Detail(this);
            f.Show();
        }

        private void ledgerToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            Customer_Report f = new Customer_Report(this);
            f.Show();
        }

        private void dayBookToolStripMenuItem_Click_1(object sender, EventArgs e)
        {
            Cash_Report f = new Cash_Report(this);
            f.Show();
        }

        private void othersToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Expenses f = new Expenses(this);
            f.Show();
        }

        private void manufacturerAndDCToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Purchases_Report f = new Purchases_Report(this);
            f.Show();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Breakage_Items f = new Breakage_Items(this);
            f.Show();
        }

        private void toolStripMenuItem6_Click(object sender, EventArgs e)
        {
            Direct_Expenses_Detail f = new Direct_Expenses_Detail(this);
            f.Show();
        }

        private void profitLossToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Profit_Report f = new Profit_Report(this);
            f.Show();
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            opener.Close();
        }

        private void subAccountsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Sub_Account f = new Sub_Account(this);
            f.Show();
        }
    }
}
