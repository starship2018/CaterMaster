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
    public partial class FormVIPTypeInfo : Form
    {
        public FormVIPTypeInfo()
        {
            InitializeComponent();
            this.dgvList.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            this.dgvList.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
        }

        private void ManagerTypeInfo_Load(object sender, EventArgs e)
        {
            Refresh();
        }

        VIPTypeInfoBll bll=new VIPTypeInfoBll();


        private void btnSave_Click(object sender, EventArgs e)
        {
            VIPTypeInfo vip=new VIPTypeInfo();
            vip.MTitle=txtTitle.Text;
            vip.MDiscount=Convert.ToDecimal(txtDiscount.Text);
            if (txtId.Text=="添加时无编号")
            {
                //添加功能
                btnSave.Text = "添加";
                if (bll.Add(vip))
                {
                    MessageBox.Show("添加成功！");
                    Refresh();
                }
                else
                {
                    MessageBox.Show("添加失败，请稍后重试！");
                    Refresh();
                }
            }
            else
            {
                //修改功能
                vip.MId = Convert.ToInt32(txtId.Text);
                if (bll.Edit(vip))
                {
                    MessageBox.Show("修改成功！");
                    Refresh();
                }
                else
                {
                    MessageBox.Show("修改失败！请稍后重试");
                    Refresh();
                }

                
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            Refresh();
        }

        void Refresh()
        {
            dgvList.AutoGenerateColumns = false;
            dgvList.DataSource = bll.GetList();
            txtDiscount.Text = "";
            txtTitle.Text = "";
            txtId.Text = "添加时无编号";
            btnSave.Text = "添加";
        }

        private void dgvList_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            btnSave.Text = "保存";
            //获取双击选中的一行
            var row=dgvList.Rows[e.RowIndex];
            txtId.Text = row.Cells[0].Value.ToString();
            txtTitle.Text = row.Cells[1].Value.ToString();
            txtDiscount.Text = row.Cells[2].Value.ToString();
        }

        private void btnRemove_Click(object sender, EventArgs e)
        {
            var rows = dgvList.SelectedRows;
            //根据选中的行数来判断是否执行删除
            if (rows.Count>0)
            {
                int deleteid = Convert.ToInt32(rows[0].Cells[0].Value);
                if (bll.Delete(deleteid))
                {
                    MessageBox.Show("删除成功！");
                    Refresh();
                }
                else
                {
                    MessageBox.Show("删除失败，请稍后重试！");
                    Refresh();
                }
            }
            else
            {
                MessageBox.Show("请选择要删除的数据！");
            }
        }
    }
}
