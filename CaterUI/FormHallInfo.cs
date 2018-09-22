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
    public partial class FormHallInfo : Form
    {
        private HallInfoBll bll;
        public FormHallInfo()
        {
            InitializeComponent();
            this.dgvList.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            this.dgvList.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            bll = new HallInfoBll();
        }

        public event Action UpdateForm;
        private void FormHallInfo_Load(object sender, EventArgs e)
        {
            LoadList();
        }

        void LoadList()
        {
            dgvList.AutoGenerateColumns = false;
            dgvList.DataSource = bll.GetList();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            HallInfo hi = new HallInfo() { HTitle = txtTitle.Text };

            //判断添加操作还是修改操作
            if (txtId.Text == "添加时无编号")
            {
                //添加操作
                if (bll.Add(hi))
                {
                    MessageBox.Show("添加成功！");
                }
                else
                {
                    MessageBox.Show("添加失败！请稍后重试");
                }
            }
            else
            {
                //修改操作
                hi.HId = Convert.ToInt32(txtId.Text);
                if (bll.Edit(hi))
                {
                    MessageBox.Show("修改成功！");
                }
                else
                {
                    MessageBox.Show("修改失败！请稍后重试");
                }
            }

            txtId.Text = "添加时无编号";
            txtTitle.Text = "";
            LoadList();
            UpdateForm();

        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            txtId.Text = "添加时无编号";
            txtTitle.Text = "";
            LoadList();
        }

        private void dgvList_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            var row = dgvList.Rows[e.RowIndex];
            btnSave.Text = "修改";
            txtId.Text = row.Cells[0].Value.ToString();
            txtTitle.Text = row.Cells[1].Value.ToString();
        }

        private void btnRemove_Click(object sender, EventArgs e)
        {
            int index = Convert.ToInt32(dgvList.SelectedRows[0].Cells[0].Value);
            DialogResult result =
                   MessageBox.Show("确认要删除吗？", "警告", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning);
            if (result==DialogResult.OK)
            {
                if (bll.Remove(index))
                {
                    MessageBox.Show("删除成功！");
                }
                else
                {
                    MessageBox.Show("删除失败！请稍后重试");
                }
            }
            else
            {
                
            }

            txtId.Text = "添加时无编号";
            txtTitle.Text = "";
            LoadList();
            UpdateForm();
        }
    }
}
