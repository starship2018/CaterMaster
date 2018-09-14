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
    public partial class FormManagerInfo : Form
    {
        public FormManagerInfo()
        {
            InitializeComponent();
        }

        ManagerInfoBll miBll=new ManagerInfoBll();
        

        private void FormManagerInfo_Load(object sender, EventArgs e)
        {
            LoadList();
        }
        
        
        //加载列表
        void LoadList()
        {
            dgvList.AutoGenerateColumns = false;
            dgvList.DataSource = miBll.GetList();
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            ManagerInfo mi = new ManagerInfo();
            mi.MName = textBox2.Text;
            mi.MPwd = textBox3.Text;
            mi.MType = rbManager.Checked ? 1 : 0;
            if (TXT.Text.Equals("添加时无编号"))
            {
                //判断出是在添加还是修改保存
                if (miBll.Add(mi))
                {
                    MessageBox.Show("添加成功！");
                    LoadList();
                }
                else
                {
                    MessageBox.Show("添加失败，请稍候重试！");
                }
            }
            else
            {
                mi.MId = Convert.ToInt32(TXT.Text);
                if (miBll.Edit(mi))
                {
                    LoadList();
                }
            }

            textBox2.Text = "";
            textBox3.Text = "";
            TXT.Text = "添加时无编号";

            rbWaiter.Checked = true;
        }

        private void btnCalloff_Click(object sender, EventArgs e)
        {
            TXT.Text = "添加时无编号";
            textBox2.Text = "";
            textBox3.Text = "";
            rbWaiter.Checked = true;
        }

        private void dgvList_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            //找到是哪一行被双击了,并将这一行dgv对象取出来
            DataGridViewRow row= dgvList.Rows[e.RowIndex];
            TXT.Text=row.Cells[0].Value.ToString();
            textBox2.Text = row.Cells[1].Value.ToString();
            if (row.Cells[2].Value.ToString().Equals("1"))
            {
                rbManager.Checked = true;
            }
            else
            {
                rbWaiter.Checked = true;
            }

            textBox3.Text = "这是原来的密码吗";
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            var rows =dgvList.SelectedRows;
            if (rows.Count>0)
            {
                //删除前的确认提示
               DialogResult result= MessageBox.Show("确认要删除吗？","警告",MessageBoxButtons.OKCancel);
                if (result==DialogResult.Cancel)
                {
                    return;
                }
                //获取删除的编号
                int id = Convert.ToInt32(rows[0].Cells[0].Value.ToString());

                if (miBll.Remove(id))
                {
                    dgvList.CurrentCell = null;
                    LoadList();
                }
            }
            else
            {
                MessageBox.Show("请选择要删除的行！");
            }
        }
    }
}
