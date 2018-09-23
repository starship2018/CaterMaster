using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using CaterBll;

namespace CaterUI
{
    public partial class FormMain : Form
    {
        public FormMain()
        {
            InitializeComponent();

        }

        private void FormMain_Load(object sender, EventArgs e)
        {
            int type = Convert.ToInt32(Tag);
            //是经理
            if (type==1)
            {

            }
            else
            {
                menuManagerInfo.Visible = false;
            }

            LoadHallInfo();
        }

        //退出按钮
        private void menuQuit_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
        //右上角关闭按钮
        private void FormMain_FormClosed(object sender, FormClosedEventArgs e)
        {
            Application.Exit();
        }

        private void menuManagerInfo_Click(object sender, EventArgs e)
        {
            FormManagerInfo mi=FormManagerInfo.CreateFrom();
            mi.Show();
            mi.Focus();
        }

        //加载主界面信息
        void LoadHallInfo()
        {
            HallInfoBll bll=new HallInfoBll();
            var halllist=bll.GetList();
            foreach (var hall in halllist)
            {
                //根据包间名创建标签页
                TabPage tp=new TabPage(hall.HTitle);
                //创建显示列表
                ListView lvtableinfo=new ListView();
                //给显示列表绑定图片表
                lvtableinfo.LargeImageList = imageList1;
                //给显示列表绑定双击事件
                lvtableinfo.DoubleClick+=lvtableinfo_DoubleClick;
                //获得当前包间内所有的餐桌对象
                Dictionary<string,string> dic=new Dictionary<string, string>();
                dic.Add("thallid",hall.HId.ToString());
                TableInfoBll tbll=new TableInfoBll();
                var tables = tbll.GetList(dic);
                //向显示列表中添加餐桌对象
                foreach (var table in tables)
                {
                    
                    ListViewItem item=new ListViewItem(table.TTitle,table.TIsFree?0:1);
                    item.Tag = table.TId;//绑定餐桌号码
                    lvtableinfo.Items.Add(item);
                }
                //将显示列表加入当前标签页中
                tp.Controls.Add(lvtableinfo);
                //将当前标签页加入tabtable中
                tcHallInfo.Controls.Add(tp);
            }
        }
        OrderInfoBll oiBll=new OrderInfoBll();
        private void lvtableinfo_DoubleClick(object sender, EventArgs e)
        {
            //获得被点击的餐桌对象
            var lv = sender as ListView;
            var lv1 = lv.SelectedItems[0];
            //获得餐桌的编号
            int tableid = Convert.ToInt32(lv1.Tag);//取到上面绑定的餐桌号码
            //判断餐桌是否空闲
            if (lv1.ImageIndex==0)
            {
                //空闲状态
                //1 根据餐桌号来开单 
                int orderid=oiBll.MakeOrder(tableid);
                lv1.Tag = orderid;
                //2 更改空闲图标为繁忙
                lv1.ImageIndex = 1;
            }
            else
            {
                //占用状态
                lv1.Tag= oiBll.GetOrderById(tableid);
                

            }

            //打开点菜窗体
            FormOrderDish fdi=new FormOrderDish();
            fdi.Tag = lv1.Tag;
            fdi.Show();
            
        }

        private void menuMemberInfo_Click(object sender, EventArgs e)
        {
            FormVIPInfo fvi = FormVIPInfo.CreateFormVipInfo();
            fvi.Show();
        }

        private void menuTableInfo_Click(object sender, EventArgs e)
        {
            FormTableInfo fti = FormTableInfo.CreateFormTableInfo();
            fti.Refresh += LoadHallInfo;
            fti.ShowDialog();

        }

        private void menuDishInfo_Click(object sender, EventArgs e)
        {
            FormDishInfo fdi=FormDishInfo.CreateFormDishInfo();
            fdi.ShowDialog();
        }

        private void menuOrder_Click(object sender, EventArgs e)
        {
            //先找到选中的标签页，再找到listView,再找到选中的项，项中存储了餐桌编号，由餐桌编号查到订单编号
            var listView = tcHallInfo.SelectedTab.Controls[0] as ListView;
            var lvtable=listView.SelectedItems[0];
            if (lvtable.ImageIndex==0)
            {
                MessageBox.Show("餐桌还未使用，无法结账！");
                return;
            }

            int tableid = Convert.ToInt32(lvtable.Tag);
            int orderid = oiBll.GetOrderById(tableid);

            //打开付款窗体
            FormOrderPay formOrderPay = new FormOrderPay();
            formOrderPay.Tag = orderid;
            formOrderPay.Refresh += LoadHallInfo;
            formOrderPay.Show();
        }



    }
}
