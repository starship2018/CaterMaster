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
    public partial class FormOrderPay : Form
    {
        public FormOrderPay()
        {
            InitializeComponent();
        }

        private OrderInfoBll oibll=new OrderInfoBll();

        private int orderid;

        public event Action Refresh;

        private void FormOrderPay_Load(object sender, EventArgs e)
        {
            //获取传递过来的订单编号
            orderid = Convert.ToInt32(this.Tag);


            gbMember.Enabled = false;

            GetMoneyByOrderId();
        }

        void GetMoneyByOrderId()
        {
            lblPayMoney.Text = lblPayMoneyDiscount.Text = oibll.GetTotalMoneyByOrderId(orderid).ToString();
        }

        private void cbkMember_CheckedChanged(object sender, EventArgs e)
        {
            gbMember.Enabled = true;
        }
        private void cbkMember_CheckedChanged_1(object sender, EventArgs e)
        {
            gbMember.Enabled = true;
        }
        private void LoadMember()
        {
            //根据会员编号和会员电话进行查询
            Dictionary<string, string> dic = new Dictionary<string, string>();
            if (txtId.Text != "")
            {
                dic.Add("mid", txtId.Text);
            }
            if (txtPhone.Text != "")
            {
                dic.Add("mPhone", txtPhone.Text);
            }

            VIPInfoBll miBll = new VIPInfoBll();
            var list = miBll.GetList(dic);
            if (list.Count > 0)
            {
                //根据信息查到了会员
                VIPInfo mi = list[0];
                lblMoney.Text = mi.MCount.ToString();
                lblTypeTitle.Text = mi.MTypeTitle;
                lblDiscount.Text = mi.MDiscount.ToString();

                //计算折扣价
                lblPayMoneyDiscount.Text =
                    (Convert.ToDecimal(lblPayMoney.Text) * Convert.ToDecimal(lblDiscount.Text)).ToString();
            }
            else
            {
                MessageBox.Show("会员信息有误");
            }
        }

        private void txtId_Leave(object sender, EventArgs e)
        {
            LoadMember();
        }

        private void txtPhone_Leave(object sender, EventArgs e)
        {
            LoadMember();
        }

        private void btnOrderPay_Click(object sender, EventArgs e)
        {
            //1、根据是否使用余额决定扣款方式
            //2、将订单状态为IsPage=1
            //3、将餐桌状态IsFree=1

            if (oibll.Pay(cbkMoney.Checked, int.Parse(txtId.Text), Convert.ToDecimal(lblPayMoneyDiscount.Text), orderid,
                Convert.ToDecimal(lblDiscount.Text)))
            {
                //MessageBox.Show("结账成功");
                Refresh();
                this.Close();
            }
            else
            {
                MessageBox.Show("结账失败");
            }

        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnOrderPay_Click_1(object sender, EventArgs e)
        {
            //1、根据是否使用余额决定扣款方式
            //2、将订单状态为IsPage=1
            //3、将餐桌状态IsFree=1

            if (oibll.Pay(cbkMoney.Checked, int.Parse(txtId.Text), Convert.ToDecimal(lblPayMoneyDiscount.Text), orderid,
                Convert.ToDecimal(lblDiscount.Text)))
            {
                //MessageBox.Show("结账成功");
                Refresh();
                this.Close();
            }
            else
            {
                MessageBox.Show("结账失败");
            }
        }


        private void txtId_Leave_1(object sender, EventArgs e)
        {
            LoadMember();
        }

        private void txtPhone_Leave_1(object sender, EventArgs e)
        {
            LoadMember();
        }

       

    }
}
