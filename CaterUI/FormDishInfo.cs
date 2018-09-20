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
    public partial class FormDishInfo : Form
    {
        public FormDishInfo()
        {
            InitializeComponent();
            this.dgvList.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            this.dgvList.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
        }

        DishInfoBll bll = new DishInfoBll();
        void LoadList()
        {
            Dictionary<string, string> dic = new Dictionary<string, string>();
            if (txtTitleSearch.Text != "")
            {
                dic.Add("dtitle", txtTitleSearch.Text);
            }

            if (ddlTypeSearch.SelectedValue.ToString() != "0")
            {
                dic.Add("dtypeid", ddlTypeSearch.SelectedValue.ToString());
            }
            dgvList.AutoGenerateColumns = false;
            dgvList.DataSource = bll.GetList(dic);
        }

        void LoadTypeList()
        {
            DishTypeInfoBll dtbll = new DishTypeInfoBll();
            List<DishTypeInfo> list = dtbll.GetList();
            list.Insert(0, new DishTypeInfo() { DId = 0, DTitle = "全部" });

            ddlTypeSearch.DataSource = list;
            ddlTypeSearch.DisplayMember = "DTitle";
            ddlTypeSearch.ValueMember = "DId";

            ddlTypeAdd.DataSource = list;
            ddlTypeAdd.DisplayMember = "DTitle";
            ddlTypeAdd.ValueMember = "DId";
        }

        private void FormDishInfo_Load(object sender, EventArgs e)
        {
            LoadTypeList();
            LoadList();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (txtTitleSave.Text == "" || txtPrice.Text == "" || ddlTypeAdd.SelectedIndex == 0)
            {
                MessageBox.Show("请完善信息后重试！");
                return;
            }
            DishInfo dish = new DishInfo()
            {
                DTitle = txtTitleSave.Text,
                DTypeId = Convert.ToInt32(ddlTypeAdd.SelectedValue),
                DPrice = Convert.ToInt32(txtPrice.Text),
                DChar = txtChar.Text
            };


            if (txtId.Text == "添加时无编号")
            {
                //添加逻辑
                if (bll.Add(dish))
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
                //修改逻辑
                dish.DId = Convert.ToInt32(txtId.Text);
                if (bll.Edit(dish))
                {
                    MessageBox.Show("修改成功！");
                }
                else
                {
                    MessageBox.Show("修改失败！请稍后重试");
                }
            }

            Clear();
            LoadList();
            LoadTypeList();
        }

        private void txtTitleSave_TextChanged(object sender, EventArgs e)
        {
            string str = txtTitleSave.Text;
            txtChar.Text = bll.GetChar(str);
        }

        void Clear()
        {
            txtId.Text = "添加时无编号";
            txtTitleSave.Text = "";
            txtChar.Text = "";
            txtPrice.Text = "";
            btnSave.Text = "添加";
            ddlTypeAdd.SelectedValue = 0;
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            Clear();
        }

        private void dgvList_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            var row = dgvList.Rows[e.RowIndex];
            txtId.Text = row.Cells[0].Value.ToString();
            txtTitleSave.Text = row.Cells[1].Value.ToString();
            ddlTypeAdd.Text = row.Cells[2].Value.ToString();
            txtPrice.Text = row.Cells[3].Value.ToString();
            btnSave.Text = "修改";
        }

        private void btnRemove_Click(object sender, EventArgs e)
        {

            var row = dgvList.SelectedRows;
            int index = Convert.ToInt32(row[0].Cells[0].Value);
            DialogResult resul = MessageBox.Show("确认要删除吗？", "警告", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning);
            if (resul == DialogResult.OK)
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

            Clear();
            LoadList();
        }

        private void txtTitleSearch_TextChanged(object sender, EventArgs e)
        {
            LoadList();
        }

        private void ddlTypeSearch_SelectedIndexChanged(object sender, EventArgs e)
        {
            LoadList();
        }

        private void btnSearchAll_Click(object sender, EventArgs e)
        {
            ddlTypeSearch.SelectedValue = 0;
            txtTitleSearch.Text = "";
            LoadList();
        }

        private void btnAddType_Click(object sender, EventArgs e)
        {
            FormDishTypeInfo fdti=new FormDishTypeInfo();
            DialogResult result = fdti.ShowDialog();
            if (result==DialogResult.OK)
            {
                LoadTypeList();
                LoadList();
            }
        }
    }
}
