using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace Verona
{
    public partial class Login : Form
    {
        //public static SqlConnection con = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Program Files\Geeks IT Solutions\GITS\Database\VeronaTilesDB.mdf;Integrated Security=True;Connect Timeout=180");
        //public static SqlConnection con = new SqlConnection("server=Umars-PC\\sa,1433; database=veronati_hassan; Integrated Security=False; Connect Timeout=180; User ID=sa; Password=123456");
        public static SqlConnection con = new SqlConnection("server=69.162.125.10; database=veronati_hassan; Integrated Security=False; Connect Timeout=600; User ID=veronati_admin; Password=GalaxyA32016");
        String fname, uname, pass;
        DateTime sys_date, exp_date, daily_date;
        Boolean allow;
        UF.Class1 test = new UF.Class1();

        public Login()
        {
            InitializeComponent();
        }

        void valid_date()
        {
            allow = false;
            if (DateTime.Compare(sys_date, daily_date) >= 0 && DateTime.Compare(sys_date, exp_date) <= 0)
            {
                DateSettings.Default.daily_date = sys_date;
                DateSettings.Default.Save();
                allow = true;
            }
            else
                allow = false;
        }
        void validate()
        {
            sys_date = DateTime.Now;
            daily_date = DateSettings.Default.daily_date;
            exp_date = DateSettings.Default.exp_date;
            valid_date();
        }

        private void Login_Load(object sender, EventArgs e)
        {
            validate();
            if (!allow)
            {
                MessageBox.Show("Software period has expaired...!\n\nFor information how to upgarde your software please\ncontact: 03126424443", "Verona Tiles & Sanitary", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                this.Close();
            }
            labeldays.Text = " Day(s) Remaining = " + (exp_date - sys_date).Days.ToString();

        }

        private void btnlogin_Click(object sender, EventArgs e)
        {
            con.Open();
            SqlCommand cmd = new SqlCommand("SELECT fname, uname, pass from office_account where uname='"+ txtuname.Text +"'", con);
            SqlDataReader dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                fname = Convert.ToString(dr[0]);
                uname = Convert.ToString(dr[1]);
                pass = Convert.ToString(dr[2]);
            }
            dr.Close();
            con.Close();
            if (txtuname.Text == uname)
            {
                uwrg.Visible = false;
                if (test.MD5Hash(txtpass.Text) == pass)
                {
                    pwrg.Visible = false;
                    Main f = new Main(this, fname, uname);
                    f.Show();
                }
                else
                {
                    pwrg.Visible = true;
                    txtpass.Clear();
                    txtpass.Focus();
                }
            }
            else
            {
                uwrg.Visible = true;
                pwrg.Visible = false;
                txtuname.Clear();
                txtpass.Clear();
                txtuname.Focus();
            }
        }

        private void txtps_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                btnlogin_Click(this, null);
            }
        }
    }
}
