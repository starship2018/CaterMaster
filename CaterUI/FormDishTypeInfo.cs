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
    public partial class FormDishTypeInfo : Form
    {
        public FormDishTypeInfo()
        {
            InitializeComponent();
            this.dgvList.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            this.dgvList.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
        }


        DishTypeInfoBll bll = new DishTypeInfoBll();
        /// <summary>
        /// 加载dgv列表
        /// </summary>
        void LoadList()
        {
            dgvList.AutoGenerateColumns = false;
            dgvList.DataSource = bll.GetList();
        }

        private void FormDishTypeInfo_Load(object sender, EventArgs e)
        {
            LoadList();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            //判断是添加还是修改！但是无论是添加还是修改，都要进行对象封装
            DishTypeInfo dti = new DishTypeInfo();
            dti.DTitle = txtTitle.Text;

            if (txtId.Text == "添加时无编号")
            {
                //添加
                if (bll.Add(dti))
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
                //修改
                dti.DId = Convert.ToInt32(txtId.Text);
                if (bll.Exit(dti))
                {
                    MessageBox.Show("修改成功！");
                }
                else
                {
                    MessageBox.Show("修改失败！请稍后重试");
                }
            }

            LoadList();
            btnSave.Text = "添加";
            txtId.Text = "添加时无编号";
            txtTitle.Text = "";
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            LoadList();
            btnSave.Text = "添加";
            txtTitle.Text = "";
            txtId.Text = "添加时无编号";
        }

        private void dgvList_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            btnSave.Text = "保存";
            var row = dgvList.Rows[e.RowIndex];
            txtId.Text = row.Cells[0].Value.ToString();
            txtTitle.Text = row.Cells[1].Value.ToString();
        }

        private void btnRemove_Click(object sender, EventArgs e)
        {
            int index = Convert.ToInt32(dgvList.SelectedRows[0].Cells[0].Value);
            if (bll.Remove(index))
            {
                MessageBox.Show("删除成功！");
            }
            else
            {
                MessageBox.Show("删除失败！请稍后重试");
            }

            LoadList();
            btnSave.Text = "添加";
            txtTitle.Text = "";
            txtId.Text = "添加时无编号";
        }
    }
}
