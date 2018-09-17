using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using CaterBll;
using CaterModel;

namespace CaterUI
{
    public partial class FormLogin : Form
    {
        public FormLogin()
        {
            InitializeComponent();
        }

        private void FormLogin_Load(object sender, EventArgs e)
        {
            label1.Parent = pictureBox1;
            label2.Parent = pictureBox1;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        ManagerInfoBll mb=new ManagerInfoBll();
        private void button1_Click(object sender, EventArgs e)
        {
            string name = txtName.Text;
            string pwd = txtPwd.Text;
            int type;
            LoginState state=mb.Login(name, pwd,out type);
            switch (state)
            {
                case LoginState.Ok: MessageBox.Show("登陆成功！");
                    FormMain main=new FormMain();
                    main.Tag = type;
                    main.Show();
                    this.Hide();
                    break;
                case LoginState.PwdERROR: MessageBox.Show("密码错误！");
                    txtPwd.Text = "";
                    txtPwd.Focus();
                    break;
                case LoginState.NameERROR: MessageBox.Show("用户名错误！");
                    txtName.Text = "";
                    txtPwd.Text = "";
                    txtName.Focus();
                    break;
            }
        }
    }
}
