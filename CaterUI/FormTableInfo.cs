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
using CaterDal;
using CaterModel;

namespace CaterUI
{
    public partial class FormTableInfo : Form
    {
        #region 单例模式的实现
        private FormTableInfo()
        {
            InitializeComponent();
            this.dgvList.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            this.dgvList.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
        }

        private static FormTableInfo _formTableInfo;

        public static FormTableInfo CreateFormTableInfo()
        {
            if (_formTableInfo == null)
            {
                _formTableInfo = new FormTableInfo();
            }

            return _formTableInfo;
        } 
        #endregion

        TableInfoBll bll=new TableInfoBll();

        public event Action Refresh;
        private void FormTableInfo_Load(object sender, EventArgs e)
        {
            
            LoadTypeList();
            LoadList();
        }

        void LoadList()
        {
            Dictionary<string,string> dic=new Dictionary<string, string>();

            if (ddlHallSearch.SelectedIndex>0)
            {
                dic.Add("thallid",ddlHallSearch.SelectedValue.ToString());
            }

            if (ddlFreeSearch.SelectedIndex>0)
            {
                dic.Add("tisfree",ddlFreeSearch.SelectedValue.ToString());
            }

            dgvList.AutoGenerateColumns = false;
            dgvList.DataSource = bll.GetList(dic);
        }

        void LoadTypeList()
        {
            HallInfoBll hbll=new HallInfoBll();
            List<HallInfo> list = hbll.GetList();
            list.Insert(0,new HallInfo(){HId = 0,HTitle = "全部"});

            ddlHallSearch.DataSource = list;
            ddlHallSearch.DisplayMember = "HTitle";
            ddlHallSearch.ValueMember = "HId";

            ddlHallAdd.DataSource = hbll.GetList();
            ddlHallAdd.DisplayMember = "HTitle";
            ddlHallAdd.ValueMember = "HId";

            List<DdlModel> listm=new List<DdlModel>()
            {
                new DdlModel(){Id = -1,Title = "全部"},
                new DdlModel(){Id = 1,Title = "空闲中"},
                new DdlModel(){Id = 0,Title = "使用中"}
            };
            ddlFreeSearch.DataSource = listm;
            ddlFreeSearch.DisplayMember = "Title";
            ddlFreeSearch.ValueMember = "Id";
        }

        private void dgvList_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (e.ColumnIndex==3)
            {
                e.Value = (bool) e.Value ? "√" : "×";
            }
        }

        private void ddlHallSearch_SelectedIndexChanged(object sender, EventArgs e)
        {
            //LoadList();
        }

        private void ddlFreeSearch_SelectedIndexChanged(object sender, EventArgs e)
        {
            LoadList();
        }

        private void btnSearchAll_Click(object sender, EventArgs e)
        {
            ddlHallSearch.SelectedIndex = 0;
            ddlFreeSearch.SelectedIndex = -1;
            LoadList();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (txtTitle.Text=="")
            {
                MessageBox.Show("请填写完整的信息！");
                return;
            }
            TableInfo ti=new TableInfo()
            {
                TTitle = txtTitle.Text,
                THallId =Convert.ToInt32(ddlHallAdd.SelectedValue),
                TIsFree = rbFree.Checked?true:false
            };


            if (txtId.Text=="添加时无编号")
            {
                //添加操作
                if (bll.Add(ti))
                {
                    MessageBox.Show("添加成功！");
                }
                else
                {
                    MessageBox.Show("添加失败！");
                }

                txtId.Text = "添加时无编号";
                txtTitle.Text = "";
                rbFree.Checked = true;
            }
            else
            {
                //修改操作
                ti.TId = Convert.ToInt32(txtId.Text);
                if (bll.Edit(ti))
                {
                    MessageBox.Show("修改成功！");
                }
                else
                {
                    MessageBox.Show("修改失败！");
                }

                txtId.Text = "添加时无编号";
                txtTitle.Text = "";
                rbFree.Checked = true;
            }

            LoadList();
            Refresh();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            txtId.Text = "添加时无编号";
            txtTitle.Text = "";
            btnSave.Text = "添加";
            rbFree.Checked = true;
            LoadList();
        }

        private void dgvList_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            var row = dgvList.Rows[e.RowIndex];
            txtId.Text = row.Cells[0].Value.ToString();
            txtTitle.Text = row.Cells[1].Value.ToString();
            ddlHallAdd.Text = row.Cells[2].Value.ToString();
            if (Convert.ToBoolean(row.Cells[3].Value)==true)
            {
                rbFree.Checked = true;
            }
            else
            {
                rbUnFree.Checked = true;
            }

            btnSave.Text = "修改";

        }

        private void btnRemove_Click(object sender, EventArgs e)
        {
            int index = Convert.ToInt32(dgvList.SelectedRows[0].Cells[0].Value);
            DialogResult result = MessageBox.Show("确认要删除吗？", "警告", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning);
            if (result==DialogResult.OK)
            {
                if (bll.Remove(index))
                {
                    MessageBox.Show("删除成功！");
                }
                else
                {
                    MessageBox.Show("删除失败!");
                }
            }
            else
            {
                
            }
            LoadList();
            Refresh();
        }

        private void btnAddHall_Click(object sender, EventArgs e)
        {
            FormHallInfo fhi=new FormHallInfo();
            fhi.UpdateForm += LoadList;
            fhi.UpdateForm += LoadTypeList;
            fhi.Show();
        }



    }
}
