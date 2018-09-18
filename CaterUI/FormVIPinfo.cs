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
    public partial class FormVIPInfo : Form
    {
        public FormVIPInfo()
        {
            InitializeComponent();
            this.dgvList.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            this.dgvList.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

        }

        VIPInfoBll bll = new VIPInfoBll();


        /// <summary>
        /// 刷新和初始化查询
        /// </summary>
        void Refresh()
        {
            //这里将初始化dgv和查询功能做在一起
            Dictionary<string, string> dic = new Dictionary<string, string>();
            if (txtNameSearch.Text != "")
            {
                dic.Add("mname", txtNameSearch.Text);
            }
            if (txtPhoneSearch.Text != "")
            {
                dic.Add("mphone", txtPhoneSearch.Text);
            }

            txtId.Text = "添加时无编号";
            txtNameAdd.Text = "";
            txtPhoneAdd.Text = "";
            txtMoney.Text = "";
            btnSave.Text = "保存";

            dgvList.AutoGenerateColumns = false;
            dgvList.DataSource = bll.GetList(dic);
            dgvList.ClearSelection();
        }

        void LoadTypeList()
        {
            VIPTypeInfoBll bll = new VIPTypeInfoBll();
            ddlType.DataSource = bll.GetList();
            ddlType.DisplayMember = "mtitle";
            ddlType.ValueMember = "mid";
        }


        private void FormVIPInfo_Load(object sender, EventArgs e)
        {
            Refresh();
            LoadTypeList();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (txtId.Text=="添加时无编号")
            {
                //添加功能
                if (txtNameAdd.Text == "")
                {
                    MessageBox.Show("请输入用户名！");
                    return;
                }

                VIPInfo vi = new VIPInfo()
                {
                    MName = txtNameAdd.Text,
                    MCount = Convert.ToDecimal(txtMoney.Text),
                    MPhone = txtPhoneAdd.Text,
                    MTypeId = Convert.ToInt32(ddlType.SelectedValue)
                };
                if (bll.Add(vi))
                {
                    MessageBox.Show("添加成功！");
                    Refresh();
                }
                else
                {
                    MessageBox.Show("添加失败！请稍后重试");
                    Refresh();
                }
            }
            else
            {
                //修改功能
                VIPInfo vip=new VIPInfo();
                vip.MName = txtNameAdd.Text;
                vip.MTypeId = ddlType.SelectedIndex;
                vip.MPhone = txtPhoneAdd.Text;
                vip.MCount = Convert.ToDecimal(txtMoney.Text);
                if (bll.Edit(vip))
                {
                    MessageBox.Show("修改成功！");
                    Refresh();
                }
                else
                {
                    MessageBox.Show("修改失败！请稍后重试！");
                    Refresh();
                }

                
            }
            
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            txtNameAdd.Text = "";
            txtPhoneAdd.Text = "";
            txtMoney.Text = "";
        }

        private void dgvList_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            var row=dgvList.Rows[e.RowIndex];
            txtId.Text=row.Cells[0].Value.ToString();
            txtNameAdd.Text = row.Cells[1].Value.ToString();
            ddlType.Text = row.Cells[2].Value.ToString();
            txtPhoneAdd.Text = row.Cells[3].Value.ToString();
            txtMoney.Text = row.Cells[4].Value.ToString();
            btnSave.Text = "修改";
        }

        private void btnRemove_Click(object sender, EventArgs e)
        {
            //int index=Convert.ToInt32(txtId.Text);
            int index = Convert.ToInt32(dgvList.SelectedRows[0].Cells[0].Value);
            if (bll.Remove(index))
            {
                MessageBox.Show("删除成功！");
                Refresh();
            }
            else
            {
                MessageBox.Show("删除失败！请稍后重试！");
                Refresh();
            }

            
        }

        private void btnAddType_Click(object sender, EventArgs e)
        {
            FormVIPTypeInfo vti=new FormVIPTypeInfo();
            DialogResult result= vti.ShowDialog();
            if (result==DialogResult.OK)
            {
                LoadTypeList();
                Refresh();
            }
        }

        private void txtNameSearch_TextChanged(object sender, EventArgs e)
        {
            Refresh();
        }

        private void txtPhoneSearch_TextChanged(object sender, EventArgs e)
        {
            Refresh();
        }
    }
}
