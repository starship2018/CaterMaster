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
using CaterBll;

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

            LoadHallInfo();
        }

        //退出按钮
        private void menuQuit_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
        //右上角关闭按钮
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

        //加载主界面信息
        void LoadHallInfo()
        {
            HallInfoBll bll=new HallInfoBll();
            var halllist=bll.GetList();
            foreach (var hall in halllist)
            {
                //根据创建标签页
                TabPage tp=new TabPage(hall.HTitle);
                tcHallInfo.Controls.Add(tp);
            }
        }



    }
}
