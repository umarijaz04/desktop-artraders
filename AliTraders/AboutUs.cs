using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace AhmadSanitary
{
    public partial class AboutUs : Form
    {
        Form mainopener;
        public AboutUs(Main mainform)
        {
            InitializeComponent();
            mainopener = mainform;
        }

        private void AboutUs_Load(object sender, EventArgs e)
        {
            mainopener.Enabled = false;
        }

        private void AboutUs_FormClosing(object sender, FormClosingEventArgs e)
        {
            mainopener.Enabled = true;
        }

        private void btnfb_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start("https://www.facebook.com/geeksitsol");
        }
    }
}
