using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CaterUI
{
    public partial class FormMain : Form
    {
        public FormMain()
        {
            InitializeComponent();

        }

        private void FormMain_Load(object sender, EventArgs e)
        {
            int type = Convert.ToInt32(Tag);
            //是经理
            if (type==1)
            {

            }
            else
            {
                menuManagerInfo.Visible = false;
            }
        }

        private void menuQuit_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void FormMain_FormClosed(object sender, FormClosedEventArgs e)
        {
            Application.Exit();
        }

        private void menuManagerInfo_Click(object sender, EventArgs e)
        {
            FormManagerInfo mi=FormManagerInfo.CreateFrom();
            mi.Show();
            mi.Focus();
        }




    }
}
