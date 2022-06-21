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
    public partial class DC_Wise_Breakage : Form
    {
        Form opener;
        Decimal invoice_no, amt;
        String delid;
        UF.Class1 test = new UF.Class1();
        public DC_Wise_Breakage(Form form)
        {
            InitializeComponent();
            opener = form;
        }

        private void DC_Wise_Breakage_Load(object sender, EventArgs e)
        {
            opener.Enabled = false;
        }

        private void DC_Wise_Breakage_FormClosing(object sender, FormClosingEventArgs e)
        {
            opener.Enabled = true;
        }

        private void txtinvoiceno_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                if (txtinvoiceno.Text != "")
                {
                    amt = 0;
                    string command = "select b_id Breakage_ID, ps_id DC_No, datetime DateTime From breakage_stock where ps_id='" + txtinvoiceno.Text + "'";
                    test.bindingcode(command, dataGridView, Login.con);
                    int col = 0;
                    SqlDataAdapter a = new SqlDataAdapter("select b_id Breakage_ID, ps_id DC_No, datetime DateTime, '' Amount From breakage_stock where ps_id='" + txtinvoiceno.Text + "'", Login.con);
                    DataTable dataTable1 = new DataTable();
                    a.Fill(dataTable1);
                    dataTable1 = dataTable1.DefaultView.ToTable();
                    Login.con.Open();
                    foreach (DataRow row in dataTable1.Rows)
                    {
                        for (; col < dataTable1.Rows.Count; col++)
                        {
                            SqlCommand cmd0 = new SqlCommand("select isnull(sum(CAST(ROUND((((s.qty + (s.tiles / p.pieces)) * (ps.pprice)) - (((s.qty + (s.tiles / p.pieces)) * (ps.pprice) * (ps.discount))/100)),2) as decimal(18,2))),0) Amount From breakage_detail s, breakage_stock bd , product p, purchase_stock_detail ps Where s.p_id=p.p_id AND ps.p_id=s.p_id AND ps.p_id=p.p_id AND s.size=p.size AND s.quality=ps.quality AND ps.ps_id=bd.ps_id AND s.b_id=bd.b_id AND s.b_id ='" + Convert.ToDecimal(dataTable1.Rows[col]["Breakage_ID"]) + "' AND bd.ps_id='" + Convert.ToDecimal(dataTable1.Rows[col]["DC_No"]) + "' UNION select isnull(sum(CAST(ROUND((((s.qty) * (ps.pprice)) - (((s.qty) * (ps.pprice) * (ps.discount))/100)),2) as decimal(18,2))),0) Amount From breakage_detail2 s, breakage_stock bd, product2 p, purchase_stock_detail2 ps Where s.p_id=p.p_id AND ps.p_id=s.p_id AND ps.p_id=p.p_id AND ps.ps_id=bd.ps_id AND s.b_id=bd.b_id AND s.b_id ='" + Convert.ToDecimal(dataTable1.Rows[col]["Breakage_ID"]) + "' AND bd.ps_id='" + Convert.ToDecimal(dataTable1.Rows[col]["DC_No"]) + "'", Login.con);
                            SqlDataReader dr0 = cmd0.ExecuteReader();
                            if (dr0.HasRows)
                            {
                                while (dr0.Read())
                                {
                                    dataTable1.Rows[col]["Amount"] = Convert.ToDecimal(dr0[0]);
                                }
                            }
                            dr0.Close();
                        }
                    }
                    Login.con.Close();
                    dataGridView.DataSource = dataTable1;
                    for (int i = 0; i < dataGridView.RowCount; i++)
                    {
                        amt += Convert.ToDecimal(dataGridView.Rows[i].Cells[3].Value);
                    }
                    lbsales.Text = amt.ToString();
                }
            }
        }

        private void btnsearch_Click(object sender, EventArgs e)
        {
            amt = 0;
            txtinvoiceno.Clear();
            string command = "select b_id Breakage_ID, ps_id DC_No, datetime DateTime From breakage_stock where (datetime Between '" + Convert.ToDateTime(dateTimePicker1.Text) + "' AND '" + Convert.ToDateTime(dateTimePicker2.Text).AddDays(1) + "')";
            test.bindingcode(command, dataGridView, Login.con);
            int col = 0;
            SqlDataAdapter a = new SqlDataAdapter("select b_id Breakage_ID, ps_id DC_No, datetime DateTime, '' Amount From breakage_stock where (datetime Between '" + Convert.ToDateTime(dateTimePicker1.Text) + "' AND '" + Convert.ToDateTime(dateTimePicker2.Text).AddDays(1) + "')", Login.con);
            DataTable dataTable1 = new DataTable();
            a.Fill(dataTable1);
            dataTable1 = dataTable1.DefaultView.ToTable();
            Login.con.Open();
            foreach (DataRow row in dataTable1.Rows)
            {
                for (; col < dataTable1.Rows.Count; col++)
                {
                    SqlCommand cmd0 = new SqlCommand("select isnull(sum(CAST(ROUND((((s.qty + (s.tiles / p.pieces)) * (ps.pprice)) - (((s.qty + (s.tiles / p.pieces)) * (ps.pprice) * (ps.discount))/100)),2) as decimal(18,2))),0) Amount From breakage_detail s, breakage_stock bd , product p, purchase_stock_detail ps Where s.p_id=p.p_id AND ps.p_id=s.p_id AND ps.p_id=p.p_id AND s.size=p.size AND s.quality=ps.quality AND ps.ps_id=bd.ps_id AND s.b_id=bd.b_id AND s.b_id ='" + Convert.ToDecimal(dataTable1.Rows[col]["Breakage_ID"]) + "' AND bd.ps_id='" + Convert.ToDecimal(dataTable1.Rows[col]["DC_No"]) + "' UNION select isnull(sum(CAST(ROUND((((s.qty) * (ps.pprice)) - (((s.qty) * (ps.pprice) * (ps.discount))/100)),2) as decimal(18,2))),0) Amount From breakage_detail2 s, breakage_stock bd, product2 p, purchase_stock_detail2 ps Where s.p_id=p.p_id AND ps.p_id=s.p_id AND ps.p_id=p.p_id AND ps.ps_id=bd.ps_id AND s.b_id=bd.b_id AND s.b_id ='" + Convert.ToDecimal(dataTable1.Rows[col]["Breakage_ID"]) + "' AND bd.ps_id='" + Convert.ToDecimal(dataTable1.Rows[col]["DC_No"]) + "'", Login.con);
                    SqlDataReader dr0 = cmd0.ExecuteReader();
                    if (dr0.HasRows)
                    {
                        while (dr0.Read())
                        {
                            dataTable1.Rows[col]["Amount"] = Convert.ToDecimal(dr0[0]);
                        }
                    }
                    dr0.Close();
                }
            }
            Login.con.Close();
            dataGridView.DataSource = dataTable1;
            for (int i = 0; i < dataGridView.RowCount; i++)
            {
                amt += Convert.ToDecimal(dataGridView.Rows[i].Cells[3].Value);
            }
            lbsales.Text = amt.ToString();
        }

        private void dataGridView_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = this.dataGridView.Rows[e.RowIndex];
                invoice_no = Convert.ToDecimal(row.Cells["Breakage_ID"].Value.ToString());
                delid = row.Cells["Breakage_ID"].Value.ToString();
                btndetail.Enabled = true;
                btndel.Enabled = true;
            }
        }

        private void btndel_Click(object sender, EventArgs e)
        {
            if (delid != "")
            {
                DialogResult dresult = MessageBox.Show("Are you sure you want to delete this Breakage", "AR Tranders", MessageBoxButtons.OKCancel, MessageBoxIcon.Question);
                if (dresult == DialogResult.OK)
                {
                    Login.con.Open();
                    //SqlCommand cmd = new SqlCommand("select b_id from breakage_detail where b_id='" + delid + "'", Login.con);
                    //SqlDataReader dr = cmd.ExecuteReader();
                    //if (dr.HasRows)
                    //{
                    //    dr.Close();
                    //    SqlCommand cmd2 = new SqlCommand("DELETE from breakage_detail where b_id='" + delid + "'", Login.con);
                    //    cmd2.ExecuteNonQuery();
                    //    SqlCommand cmd3 = new SqlCommand("DELETE from breakage_stock where b_id='" + delid + "'", Login.con);
                    //    cmd3.ExecuteNonQuery();
                    //}
                    //else
                    //{
                    //dr.Close();
                    SqlCommand cmd2 = new SqlCommand("DELETE from breakage_detail2 where b_id='" + delid + "'", Login.con);
                    cmd2.ExecuteNonQuery();
                    SqlCommand cmd3 = new SqlCommand("DELETE from breakage_stock where b_id='" + delid + "'", Login.con);
                    cmd3.ExecuteNonQuery();
                    //}
                    delid = "";
                    MessageBox.Show("Breakage Deleted Successfully", "AR Tranders", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    Login.con.Close();
                    btnsearch_Click(this, null);
                }
            }
        }

        private void btndetail_Click(object sender, EventArgs e)
        {
            Breakage_Detail f = new Breakage_Detail(invoice_no);
            f.Show();
        }

        private void txtinvoiceno_KeyPress(object sender, KeyPressEventArgs e)
        {
            test.digit_correction(sender, e);
        }

        private void button1_Click(object sender, EventArgs e)
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

        private int i = 0;
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
            e.Graphics.DrawString("DC Wise Breakage", new Font("Calibri", 12, FontStyle.Underline), new SolidBrush(Color.Black), startX, startY + Offset);
            Offset = Offset + 30;
            e.Graphics.DrawString("From:", new Font("Calibri", 10, FontStyle.Bold), new SolidBrush(Color.Black), startX, startY + Offset);
            e.Graphics.DrawString(dateTimePicker1.Text, new Font("Calibri", 10), new SolidBrush(Color.Black), startX + 40, startY + Offset);
            e.Graphics.DrawString("To:", new Font("Calibri", 10, FontStyle.Bold), new SolidBrush(Color.Black), startX + 380, startY + Offset);
            e.Graphics.DrawString(dateTimePicker2.Text, new Font("Calibri", 10), new SolidBrush(Color.Black), startX + 405, startY + Offset);
            Offset = Offset + 20;
            string underLine = "--------------------------------------------------------------------------------";
            e.Graphics.DrawString(underLine, new Font("Courier New", 10, FontStyle.Bold), new SolidBrush(Color.Black), startX, startY + Offset);
            Offset = Offset + 20;
            e.Graphics.DrawString("Breakage_ID", new Font("Calibri", 10, FontStyle.Bold), new SolidBrush(Color.Black), startX, startY + Offset);
            e.Graphics.DrawString("DC_No", new Font("Calibri", 10, FontStyle.Bold), new SolidBrush(Color.Black), startX + 120, startY + Offset);
            e.Graphics.DrawString("Date & Time", new Font("Calibri", 10, FontStyle.Bold), new SolidBrush(Color.Black), startX + 260, startY + Offset);
            e.Graphics.DrawString("Type", new Font("Calibri", 10, FontStyle.Bold), new SolidBrush(Color.Black), startX + 460, startY + Offset);
            Offset = Offset + 20;
            e.Graphics.DrawString(underLine, new Font("Courier New", 10, FontStyle.Bold), new SolidBrush(Color.Black), startX, startY + Offset);
            Offset = Offset + 20;
            int a = dataGridView.Rows.Count;
            for (; i < a; i++)
            {
                e.Graphics.DrawString(Convert.ToString(dataGridView.Rows[i].Cells[0].Value), new Font("Calibri", 9), new SolidBrush(Color.Black), startX, startY + Offset);
                e.Graphics.DrawString(Convert.ToString(dataGridView.Rows[i].Cells[1].Value), new Font("Calibri", 9), new SolidBrush(Color.Black), startX + 120, startY + Offset);
                e.Graphics.DrawString(Convert.ToString(dataGridView.Rows[i].Cells[2].Value), new Font("Calibri", 9), new SolidBrush(Color.Black), startX + 260, startY + Offset);
                e.Graphics.DrawString(Convert.ToString(dataGridView.Rows[i].Cells[3].Value), new Font("Calibri", 9), new SolidBrush(Color.Black), startX + 460, startY + Offset);
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
        }
    }
}
