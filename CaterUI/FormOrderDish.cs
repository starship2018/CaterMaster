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
    public partial class FormOrderDish : Form
    {
        public FormOrderDish()
        {
            InitializeComponent();
        }

        private void FormOrderDish_Load(object sender, EventArgs e)
        {
            LoadTypeList();
            LoadDishInfo();
            LoadDetailInfoList();
        }

        OrderInfoBll bll=new OrderInfoBll();

        void LoadDishInfo()
        {
            Dictionary<string,string> dic=new Dictionary<string, string>();
            if (txtTitle.Text!="")
            {
                dic.Add("DChar",txtTitle.Text);
            }

            if (ddlType.SelectedIndex!=0)
            {
                dic.Add("DTypeId",ddlType.SelectedValue.ToString());
            }

            DishInfoBll dibll=new DishInfoBll();
            dgvAllDish.AutoGenerateColumns = false;
            dgvAllDish.DataSource=dibll.GetList(dic);
        }
        void LoadTypeList()
        {
            DishTypeInfoBll dtibll=new DishTypeInfoBll();
            var list=dtibll.GetList();
            
            list.Insert(0,new DishTypeInfo()
            {
                DId = 0,
                DTitle = "全部"
            });

            ddlType.DisplayMember = "DTitle";
            ddlType.ValueMember = "DId";
            ddlType.DataSource = list;
        }

        void LoadDetailInfoList()
        {
            int orderid=Convert.ToInt32(this.Tag);
            dgvOrderDetail.AutoGenerateColumns = false;
            dgvOrderDetail.DataSource=bll.GetDetailInfoList(orderid);

            //实时显示总价格
            GetTotalMoneyByOrderId();
        }

        private void GetTotalMoneyByOrderId()
        {
            int orderid = Convert.ToInt32(this.Tag);
            lblMoney.Text = bll.GetTotalMoneyByOrderId(orderid).ToString();
        }

        private void txtTitle_TextChanged(object sender, EventArgs e)
        {
            LoadDishInfo();
        }

        private void ddlType_SelectedIndexChanged(object sender, EventArgs e)
        {
            LoadDishInfo();
        }

        private void dgvAllDish_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            //双击点菜
            //订单编号
            int orderid = Convert.ToInt32(this.Tag);
            //菜品（商品）编号
            int dishid = Convert.ToInt32(dgvAllDish.Rows[e.RowIndex].Cells[0].Value);

            if (bll.OrderDish(orderid,dishid))
            {
                LoadDetailInfoList();

            }
        }

        private void dgvOrderDetail_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            //针对商品数量做调整
            if (e.RowIndex==2)
            {
                var row = dgvOrderDetail.Rows[e.RowIndex];
                //获取订单号码
                int orderid = Convert.ToInt32(row.Cells[0].Value);
                //获取商品数量
                int count = Convert.ToInt32(row.Cells[2].Value);
                //在后台调整商品数量
                bll.UpdateCountByOrderId(orderid,count);

                //重新计算总价格

                GetTotalMoneyByOrderId();
            }
            LoadDetailInfoList();
        }

        private void btnOrder_Click(object sender, EventArgs e)
        {
            //获取订单号
            int orderid = Convert.ToInt32(this.Tag);
            //获取总价格
            int totalprice = Convert.ToInt32(lblMoney.Text);
            //更新订单
            if (bll.CommitOrder(orderid,totalprice))
            {
                MessageBox.Show("下单成功！");
            }

        }

        private void btnRemove_Click(object sender, EventArgs e)
        {
            
            DialogResult result = MessageBox.Show("确认要删除吗？", "警告", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning);
            if (result==DialogResult.OK)
            {
                int index=Convert.ToInt32(dgvOrderDetail.SelectedRows[0].Cells[0].Value);
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
            LoadDetailInfoList();
        }
    }
}
