using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CaterDal;
using CaterModel;

namespace CaterBll
{
    public class OrderInfoBll
    {
        OrderInfoDal dal=new OrderInfoDal();
        public bool OrderDish(int orderid,int dishid)
        {
            return dal.OrderDish(orderid, dishid) > 0;
        }

        public List<OrderDetailInfo> GetDetailInfoList(int orderid)
        {
            return dal.GetDetailList(orderid);
        }

        public decimal GetTotalMoneyByOrderId(int orderid)
        {
            return dal.GetTotalMoneyByOrderId(orderid);
        }

        public bool UpdateCountByOrderId(int orderid, int count)
        {
            return dal.UpdateCountByOrderId(orderid, count) > 0;
        }

        public bool CommitOrder(int orderid, int totalprice)
        {
            return dal.CommitOrder(orderid, totalprice) > 0;
        }

        public bool Remove(int index)
        {
            return dal.Delete(index) > 0;
        }

        public int MakeOrder(int tableid)
        {
            return dal.MakeOrder(tableid);
        }

        public int GetOrderById(int tableid)
        {
            return dal.GetOrderById(tableid);
        }

        public bool Pay(bool isUseMoney, int memberId, decimal payMoney, int orderid, decimal discount)
        {
            return dal.Pay(isUseMoney, memberId, payMoney, orderid, discount) > 0;
        }
    }
}
